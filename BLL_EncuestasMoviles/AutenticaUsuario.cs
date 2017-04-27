using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Xml;
using System.IO;
using System.Security.Cryptography;
using System.Data;
using Entidades_EncuestasMoviles;
using TVAzteca.Common;
using TVAzteca.Common.Authentication;

namespace BLL_EncuestasMoviles
{
    public class AutenticaUsuario
    {
        public static string ValidaUsuario(string Usuario, string Pass)
        {
            return string.Empty;
        }

        
        public static XmlDocument ConsultaUsuario(string numEmpl, string usuaEmpl)
        {
            return MngNegocioEmpleadoRol.GetUserDataByNumEmpleado(numEmpl, usuaEmpl);
        }

        public static bool Validar(string numEmpl, string usuaEmpl, string Password)
        {
            if (ConsultaUsuario(numEmpl, usuaEmpl).InnerXml.ToString().Trim().Length > 0)
                return true;
            else
                return false;
        }


        public static string Decriptar(string strCadenaBase64)
        {
            return EncryptedString.DecryptString(strCadenaBase64, "1234567891123456");
        }

        public static string Encriptar(string strCadena)
        {
            return EncryptedString.EncryptString(strCadena, "1234567891123456");
        }

        class EncryptedString
        {
            /// <summary>
            /// Encrpyts the sourceString, returns this result as an Aes encrpyted, BASE64 encoded string
            /// </summary>
            /// <param name="plainSourceStringToEncrypt">a plain, Framework string (ASCII, null terminated)</param>
            /// <param name="passPhrase">The pass phrase.</param>
            /// <returns>
            /// returns an Aes encrypted, BASE64 encoded string
            /// </returns>
            public static string EncryptString(string plainSourceStringToEncrypt, string passPhrase)
            {
                try
                {
                    //Set up the encryption objects
                    using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(passPhrase)))
                    {
                        byte[] sourceBytes = Encoding.ASCII.GetBytes(plainSourceStringToEncrypt);
                        ICryptoTransform ictE = acsp.CreateEncryptor();

                        //Set up stream to contain the encryption
                        MemoryStream msS = new MemoryStream();

                        //Perform the encrpytion, storing output into the stream
                        CryptoStream csS = new CryptoStream(msS, ictE, CryptoStreamMode.Write);
                        csS.Write(sourceBytes, 0, sourceBytes.Length);
                        csS.FlushFinalBlock();

                        //sourceBytes are now encrypted as an array of secure bytes
                        byte[] encryptedBytes = msS.ToArray(); //.ToArray() is important, don't mess with the buffer

                        //return the encrypted bytes as a BASE64 encoded string
                        return Convert.ToBase64String(encryptedBytes);
                    }
                }
                catch
                {
                    return "";
                }
            }

            /// <summary>
            /// Decrypts a BASE64 encoded string of encrypted data, returns a plain string
            /// </summary>
            /// <param name="base64StringToDecrypt">an Aes encrypted AND base64 encoded string</param>
            /// <param name="passphrase">The passphrase.</param>
            /// <returns>returns a plain string</returns>
            public static string DecryptString(string base64StringToDecrypt, string passphrase)
            {
                try
                {
                    //Set up the encryption objects
                    using (AesCryptoServiceProvider acsp = GetProvider(Encoding.Default.GetBytes(passphrase)))
                    {
                        byte[] RawBytes = Convert.FromBase64String(base64StringToDecrypt);
                        ICryptoTransform ictD = acsp.CreateDecryptor();

                        //RawBytes now contains original byte array, still in Encrypted state

                        //Decrypt into stream
                        MemoryStream msD = new MemoryStream(RawBytes, 0, RawBytes.Length);
                        CryptoStream csD = new CryptoStream(msD, ictD, CryptoStreamMode.Read);
                        //csD now contains original byte array, fully decrypted

                        //return the content of msD as a regular string
                        return (new StreamReader(csD)).ReadToEnd();
                    }
                }
                catch
                {
                    return "";
                }
            }

            private static AesCryptoServiceProvider GetProvider(byte[] key)
            {
                AesCryptoServiceProvider result = new AesCryptoServiceProvider();
                result.BlockSize = 128;
                result.KeySize = 128;
                result.Mode = CipherMode.CBC;
                result.Padding = PaddingMode.PKCS7;

                result.GenerateIV();
                result.IV = new byte[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

                byte[] RealKey = GetKey(key, result);
                result.Key = RealKey;
                return result;
            }

            private static byte[] GetKey(byte[] suggestedKey, SymmetricAlgorithm p)
            {
                byte[] kRaw = suggestedKey;
                List<byte> kList = new List<byte>();

                for (int i = 0; i < p.LegalKeySizes[0].MinSize; i += 8)
                {
                    kList.Add(kRaw[(i / 8) % kRaw.Length]);
                }
                byte[] k = kList.ToArray();
                return k;
            }
        }

        public static bool ValidausuarioIpad(string Usuario_TVA, string Pass_Desencript)
        {
            XmlDocument xml = new XmlDocument();
            DataSet ds = new DataSet();
            string Pass_Desencriptado = "";
            string userDesencriptado = "";
            string usuario = "";
           
            string UsuarioTVA = string.Empty;
            bool isUserName = false;
            try
            {
                Pass_Desencriptado = Pass_Desencript;
                userDesencriptado = Usuario_TVA;

                string TipoUsuario = userDesencriptado.ToUpper().Replace("TVA", "").Replace("PTV", "");

                if (isNumeric(TipoUsuario))
                {
                    isUserName = false;
                    UsuarioTVA = userDesencriptado.ToUpper();
                    usuario = userDesencriptado.ToUpper();
                }
                else
                {
                    isUserName = true;
                    usuario = userDesencriptado.ToUpper();
                    XmlDocument DatosUsua = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado("", usuario);
                    string NumUsua = (DatosUsua.GetElementsByTagName("NUMUSUA").Count > 0) ? DatosUsua.GetElementsByTagName("NUMUSUA")[0].InnerText : "";
                    if (userDesencriptado.ToUpper().Contains("TVA"))
                        UsuarioTVA = "TVA" + NumUsua;
                    else if (userDesencriptado.ToUpper().Contains("PTV"))
                        UsuarioTVA = "PTV" + NumUsua;
                    else
                        UsuarioTVA = "TVA" + NumUsua;
                }

              

                string respuesta = string.Empty;


                LDAPUser ldapUser = new LDAPUser();

                try
                {
                    if (isUserName)
                        ldapUser = ActiveDirectory.GetCurrentUser2(userDesencriptado.ToUpper(), Pass_Desencriptado);
                    else
                    {
                        ldapUser = ActiveDirectory.GetCurrentUser(userDesencriptado.ToUpper(), Pass_Desencriptado);
                    }
                }
                catch { ldapUser = null; }


                if (ldapUser != null)
                {
                    if (AutenticaUsuario.Validar("", userDesencriptado, Pass_Desencriptado))
                    {
                        XmlDocument UserData = new XmlDocument();
                        if (isUserName)
                            UserData = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado("", ldapUser.LoginName);
                        else
                        {
                            UserData = MngNegocioEmpleadoRol.GetUserDataByNumEmpleado(ldapUser.EmployeeID, "");
                            isUserName = true;
                        }
                    }
                    else
                    {

                    }
                }
                else
                {

                    try
                    {
                        if (isNumeric(TipoUsuario))
                            respuesta = Llave.validaEmpleado(userDesencriptado, Pass_Desencriptado);
                        else
                            respuesta = Llave.validaEmpleado(UsuarioTVA, Pass_Desencriptado);
                        xml.LoadXml(respuesta);
                    }
                    catch (Exception ex)
                    {
                        THE_LogError oLogErrores = new THE_LogError();
                        oLogErrores.EmplUsua = UsuarioTVA.Replace("TVA", "").Replace("PTV", "");
                        oLogErrores.DirIP = "";
                        oLogErrores.Error = ex.Message + "\n" + ex.StackTrace.ToString();
                        oLogErrores.Pantalla = "Autenticausuario";
                        oLogErrores.MachineName = "";
                        oLogErrores.FechaCreacion = DateTime.Now;
                        oLogErrores.Dominio = "";
                        MngNegocioLogErrores.GuardarLogErrores(oLogErrores);
                    }

                    if ((respuesta.IndexOf("Respuesta=\"[OK]\"") != -1) || respuesta.IndexOf("0 - [") != -1)
                    {

                        
                        string numeroUsuario = xml.FirstChild.ChildNodes[0].Attributes["NumEmp"].Value;

                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool isNumeric(string expression)
        {
            if (expression == null)
                return false;
            try
            {
                int number = int.Parse(expression);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
