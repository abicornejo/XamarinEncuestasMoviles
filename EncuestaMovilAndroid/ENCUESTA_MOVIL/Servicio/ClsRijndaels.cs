using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System.Security.Cryptography;
using System.IO;

namespace ENCUESTA_MOVIL.Servicio
{
    public class ClsRijndaels
    {
        // Fields
        private int _intKeySize = 0x100;
        private int _intPassIterations = 2;
        private string _strHashAlgorithm = "SHA1";
        private string _strInitVector = "TV@z$ec@#2%06#07";
        private string _strPassPhrase = "kocQX/EpZ2/6Ve6R4s/EMpPakJ3Q0DKMIqQA+S+naxm8WV5BunG/jhOQTdmH+BotHqyXjplz5fcZrLbPzL26fj1vKvvXpFzYwdb64OSHTIA=";
        private string _strSaltValue = "+26M/jtkjlkItrQzvp6NSVX1CtMdu6NUy6BBXeHgvH038STlx9f0cWA3YyXktVZ+j16/CjQIbQ37qITj6JOf523rii39e1c9Sj2aElqp90o=";
        private string mstrKEY = "M@\x00e9*t-o/d+o|R#i&j?n\x00a1d\x00bf@0e0l";
        public enum enmTransformType
        {
            intEncrypt,
            intDecrypt
        }
        /// <summary>
        /// Metodo que encripta una cadena usando un algoritmo propio de TV Azteca
        /// </summary>
        /// <param name="pstrText">Palabra a encriptar</param>
        /// <param name="pstrPassPhrase">Palabra clave para encriptar</param>
        /// <param name="pstrSaltValue">Palabra clave para la llave de seguridad</param>
        /// <param name="pstrHashAlgorithm">Algoritmo de encriptamiento</param>
        /// <param name="pintPassIterations">Numero de iteraciones que tiene que hacer el encriptado</param>
        /// <param name="pstrInitVector">Nombre Clave para desencriptar</param>
        /// <param name="pintKeySize">Tamaño de la clave</param>
        /// <param name="penmTransformType">Indicador para encriptar o desencriptar</param>
        /// <returns>Retorna frase encriptada o desencriptada</returns>
        private string Transform(string pstrText, string pstrPassPhrase, string pstrSaltValue, string pstrHashAlgorithm, int pintPassIterations, string pstrInitVector, int pintKeySize, enmTransformType penmTransformType = 0)
        {
            byte[] rgbIV = null;
            rgbIV = Encoding.ASCII.GetBytes(pstrInitVector);
            byte[] rgbSalt = null;
            rgbSalt = Encoding.ASCII.GetBytes(pstrSaltValue);
            byte[] buffer = null;
            if (penmTransformType == enmTransformType.intEncrypt)
            {
                buffer = Encoding.UTF8.GetBytes(pstrText);
            }
            else
            {
                buffer = Convert.FromBase64String(pstrText);
            }
            PasswordDeriveBytes bytes = new PasswordDeriveBytes(pstrPassPhrase, rgbSalt, pstrHashAlgorithm, pintPassIterations);
            byte[] rgbKey = null;
            rgbKey = bytes.GetBytes(pintKeySize / 8);
            RijndaelManaged managed = new RijndaelManaged
            {
                Mode = CipherMode.CBC
            };
            ICryptoTransform transform = null;
            if (penmTransformType == enmTransformType.intEncrypt)
            {
                transform = managed.CreateEncryptor(rgbKey, rgbIV);
            }
            else
            {
                transform = managed.CreateDecryptor(rgbKey, rgbIV);
            }
            MemoryStream stream = null;
            CryptoStream stream2 = null;
            string str = null;
            if (penmTransformType == enmTransformType.intEncrypt)
            {
                stream = new MemoryStream();
                stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Write);
                stream2.Write(buffer, 0, buffer.Length);
                stream2.FlushFinalBlock();
                str = Convert.ToBase64String(stream.ToArray());
            }
            else
            {
                stream = new MemoryStream(buffer);
                stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
                byte[] buffer6 = null;
                buffer6 = new byte[buffer.Length + 1];
                int count = 0;
                count = stream2.Read(buffer6, 0, buffer6.Length);
                str = Encoding.UTF8.GetString(buffer6, 0, count);
            }
            stream.Close();
            stream2.Close();
            return str;
        }

        /// <summary>
        /// Metodo que se manda a ejecutar desde otras clases para encriptar o desencriptar cadenas
        /// </summary>
        /// <param name="pstrText">Frase a encriptar o desencriptar</param>
        /// <param name="penmTransformType">Enumeracion para indicar si se encripta o decripta</param>
        /// <returns></returns>
        public string Transmute(string pstrText, enmTransformType penmTransformType)
        {
            string str = string.Empty;
            try
            {
                str = this.Transform(pstrText.Trim(), this.PassPhrase, this.SaltValue, this.HashAlgorithm, this.PassIterations, this.InitVector, this.KeySize, penmTransformType);
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str;
        }

        // Properties
        private string HashAlgorithm
        {
            get
            {
                return this._strHashAlgorithm;
            }
        }

        private string InitVector
        {
            get
            {
                return this._strInitVector;
            }
        }

        private int KeySize
        {
            get
            {
                return this._intKeySize;
            }
        }

        private int PassIterations
        {
            get
            {
                return this._intPassIterations;
            }
        }

        private string PassPhrase
        {
            get
            {
                return this._strPassPhrase;
            }
            set
            {
                this._strPassPhrase = value;
            }
        }

        private string SaltValue
        {
            get
            {
                return this._strSaltValue;
            }
        }

    }
}