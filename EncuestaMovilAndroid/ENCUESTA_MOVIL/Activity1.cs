using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using System.Collections.Generic;
using Android.Telephony;
using System.IO;
using Mono.Data.Sqlite;
using Android.Database.Sqlite;
using System.Threading;
using Java.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Collections;
using System.IO.IsolatedStorage;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace ENCUESTA_MOVIL
{
    public class TrustAllCertificatePolicy : System.Net.ICertificatePolicy 
    { 
        public TrustAllCertificatePolicy() 
        { } 
        public bool CheckValidationResult(ServicePoint sp, X509Certificate cert, WebRequest req, int problem) 
        { 
            return true; 
        } 
    }

    [Activity(Label = "Encuestas", MainLauncher = false, Icon = "@drawable/icon", Theme = "@style/Theme.Shelves", NoHistory = true)]
    public class Activity1 : Activity, ILocationListener
    {
        #region Variables globales
        string IsFinal = "";
        int cuentaArrayId=0;
        bool tieneEncuesta = false;
        int IdDispositivo = 0;
        ImageButton btnAceptar;
        ImageButton btnAtras;        
        ImageButton btnInicio;
        ListView lista;         
        SqliteConnection connection;
        private LocationManager _locationManager;
        TextView pregunta;
        int IDSeleccionado = 0;
        String[] resp;
        string[] arrayIds;
        TextView preguntasTot;
        int NumPreguntas = 1;        
        int indicePG = 0;
        int semaforo = 0;
        string IdRespuesta;
        string IdPreguntaEnviada;        
        private Hashtable ht;
        tva.WSEncuestaMovil asmxEM = null;
        public static List<Encuesta> FillEncuesta = new List<Encuesta>();
        tva.TDI_EncuestaDispositivo[] TDI_EncDisp = null;
        Encuesta Preguntas;
        int IdEncuesta = 0;
        string DescEncuesta;
        string IdPregunta;
        string DescPregunta;

        public struct Encuesta
        {
            public string Id;
            public string Pregunta;
            public string[] Respuesta;
        } 
        #endregion
        
        protected override void OnDestroy()
        {
            base.OnDestroy();         
        }
        /// <summary>
        /// Metodo que se ejecuta toda vez que se manda a llamar al Activity ActividadInicial
        /// Este metodo inicia el renderizado de la pantalla con sus controles.
        /// </summary>
        /// <param name="bundle"></param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Main);
            Servicio.ClsVariables.cuentaArrayId = 0;
            tieneEncuesta = Intent.GetBooleanExtra("tieneEncuesta", false);
            IdDispositivo = Intent.GetIntExtra("IdDispositivo", 0);
            Servicio.ClsVariables.IdDispositivo = IdDispositivo;
            arrayIds = Intent.GetStringArrayExtra("arrayIds");

            //LOCALIZACION POR GPS Ó TRIANGULACIÓN POR REDES
            //InicializaLocalizacion();
            preguntasTot = FindViewById<TextView>(Resource.Id.CountPregunta);
            pregunta = FindViewById<TextView>(Resource.Id.lblPregunta);

            
            btnAceptar = FindViewById<ImageButton>(Resource.Id.btnAceptar);
            btnAtras = FindViewById<ImageButton>(Resource.Id.btnAtras);            
            btnInicio = FindViewById<ImageButton>(Resource.Id.btnInicio);
            lista = FindViewById<ListView>(Resource.Id.List);

            btnInicio.Click += (o, e) =>
            {
                this.Finish();
            };
            lista.ItemClick += (o, e) =>
            {               
                IdRespuesta = ((CheckedTextView)e.View).ContentDescription;
                //Obtenemos el ID del RadioButton checkeado
                IDSeleccionado = int.Parse(((CheckedTextView)e.View).Hint.Split('|')[1]);
                semaforo = 0;
            };
            btnAtras.Click += (o, e) =>
            {
                if (NumPreguntas > 1)
                {
                    indicePG--;                    
                    semaforo = 1;
                    int numceros = 0;               
                    MuestraPreguntas(int.Parse(arrayIds[indicePG].ToString().Split('|')[0]), out IsFinal);
                    string[] arrayAux = new string[arrayIds.Length];
                    for (int iaux = 0; iaux < arrayAux.Length; iaux++)
                    {
                        if (arrayIds[iaux] != null)
                        {
                            numceros++;
                            if (indicePG != iaux)
                            {
                                arrayAux[iaux] = arrayIds[iaux];
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    if (numceros > 1)
                    {
                        arrayIds = arrayAux;
                    }
                    else
                    {
                        arrayIds=new string[FillEncuesta.Count];
                    }
                        NumPreguntas--;
                        preguntasTot.Text = "Pregunta:" + NumPreguntas + "/" + (FillEncuesta.Count - 1);
                }
                if (NumPreguntas == 1)
                {
                    btnAtras.Visibility = ViewStates.Invisible;                    
                }
                else
                {
                    btnAtras.Visibility = ViewStates.Visible;                    
                }
            };
            btnAceptar.Click += new EventHandler(btnAceptar_Click);

            try
            {
                asmxEM = new tva.WSEncuestaMovil();
            }
            catch
            { }

            try
            {
                tva.AuthHeader Credentials = new tva.AuthHeader();

                string settings = "";
                string userpassword = "";
                try
                {
                    using (StreamReader sr = new StreamReader(Assets.Open("Validator.archive")))
                    {
                        settings = sr.ReadToEnd();
                        sr.Close();
                    }
                }
                catch (Exception exep)
                {
                }
                Servicio.ClsRijndaels _encript = new Servicio.ClsRijndaels();
                userpassword = _encript.Transmute(settings, Servicio.ClsRijndaels.enmTransformType.intDecrypt);
                Credentials.UserName = userpassword;
                asmxEM.PreAuthenticate = true;
                Credentials.Password = userpassword;
                asmxEM.SoapVersion = System.Web.Services.Protocols.SoapProtocolVersion.Soap11;
                asmxEM.UnsafeAuthenticatedConnectionSharing = true;
                asmxEM.AuthHeaderValue = Credentials;

                System.Net.ServicePointManager.CertificatePolicy = new TrustAllCertificatePolicy();
                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;

                System.Net.ServicePointManager.ServerCertificateValidationCallback += (se, cert, chain, sslerror) =>
                {
                    return true;
                };
            }
            catch
            {}

            connection = Servicio.Comun.AbreBD();
            CargaEncuesta();
        }
        /// <summary>
        /// Evento que se ejecuta cuando el telefono esta en modo de espera o bloqueado y se desbloquea
        /// </summary>
        protected override void OnResume() 
        {         
            base.OnResume();          
            //_locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 60000, 0,this);         
            //_locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 60000, 0,this);     
        }
        /// <summary>
        /// Metodo que se ejecuta cuando el dispositivo se bloquea
        /// </summary>
        protected override void OnPause() 
        {         
            base.OnPause();
            //_locationManager.RemoveUpdates(this);
            //_locationManager.RemoveUpdates(this);     
        }
        
        /// <summary>
        /// Evento que cierra la aplicacion
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnAceptaAccion_Click(object sender, EventArgs e)
        {
            this.Finish();
        }
        
        
        /// <summary>
        /// Metodo que carga en pantalla la encuesta asignada, con sus preguntas y respuestas.
        /// </summary>
        private void CargaEncuesta()
        {
            try
            {
                //Se leen los registros almadenados en las bases de datos
                using (SqliteCommand contents = connection.CreateCommand())
                {
                    contents.CommandText = "SELECT [IdEncuesta],[DescEncuesta],[IdPregunta],[DescPregunta],[Respuestas] from [DatosEncuesta]";
                    var r = contents.ExecuteReader();
                    bool RowsExist = false;
                    while (r.Read())//Por cada registro pintamos la información en la pantalla en alguna etiqueta
                        RowsExist = true;
                    if (RowsExist == true)
                    {
                        FillEncuesta = new List<Encuesta>();
                        SqliteCommand leedatos = connection.CreateCommand();

                        leedatos.CommandText = "SELECT [IdEncuesta],[DescEncuesta],[IdPregunta],[DescPregunta],[Respuestas] from [DatosEncuesta]";
                        try
                        {
                            var rr = leedatos.ExecuteReader();                            
                                while (rr.Read())
                                {
                                    Preguntas = new Encuesta();
                                    IdEncuesta = int.Parse(rr["IdEncuesta"].ToString());
                                    Preguntas.Id = rr["IdPregunta"].ToString();
                                    Preguntas.Pregunta = rr["DescPregunta"].ToString();
                                    string[] strRespuesta = rr["Respuestas"].ToString().Split('&');
                                    if (strRespuesta.Length > 0)
                                    {
                                        int NumResp = strRespuesta.Length;
                                        if (NumResp == 1 && strRespuesta[0]=="")
                                        {
                                            NumResp = 0;
                                            strRespuesta = new string[0];
                                        }                                        
                                            Preguntas.Respuesta = new string[NumResp];
                                            for (int ini = 0; ini < strRespuesta.Length; ini++)
                                            {
                                                Preguntas.Respuesta[ini] = strRespuesta[ini].ToString();
                                            }                                        
                                    }                                    
                                    FillEncuesta.Add(Preguntas);
                                }
                                arrayIds = new string[FillEncuesta.Count];
                                MuestraPreguntas(IDSeleccionado,out IsFinal);                                
                                btnAtras.Visibility = ViewStates.Invisible;
                            }                        
                        catch (Exception exep)
                        {
                        }
                    }
                    else
                    {
                        if (tieneEncuesta == false)
                        {
                            Dialog dialog = new Dialog(this);
                            dialog.SetContentView(Resource.Layout.MensajeSINO);
                            dialog.SetTitle("ENCUESTAS ACTIVAS");
                            dialog.SetCancelable(true);
                            ImageButton btnAceptaAccion = ((ImageButton)dialog.FindViewById(Resource.Id.btnAceptaAccion));
                            btnAceptaAccion.Click += new EventHandler(btnAceptaAccion_Click);
                            dialog.Show();
                        }
                        else
                        {
                            try
                            {
                                //WEB SERVICES
                                TDI_EncDisp = asmxEM.ObtieneEncuestaPorDispositivo(IdDispositivo, Servicio.ClsVariables.NumeroTel);
                                //Almacenamos el Id del Dispositivo para el guardado en la base de datos

                                //Este apartado es para presentar las encuestas en la pantalla 
                                FillEncuesta = new List<Encuesta>();
                                if (TDI_EncDisp != null)
                                    for (int disp = 0; disp < TDI_EncDisp.Length; disp++)
                                    {
                                        if (TDI_EncDisp[disp].ListaEncuesta != null)
                                            for (int enc = 0; enc < TDI_EncDisp[disp].ListaEncuesta.Length; enc++)
                                            {
                                                //Almacenamos el Id de la Encuesta para el guardado en base de datos
                                                IdEncuesta = TDI_EncDisp[disp].ListaEncuesta[enc].IdEncuesta;
                                                DescEncuesta = TDI_EncDisp[disp].ListaEncuesta[enc].NombreEncuesta;
                                                if (TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg != null)
                                                    for (int preg = 0; preg < TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg.Length; preg++)
                                                    {
                                                        arrayIds = new string[TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg.Length];

                                                        Encuesta Preguntas = new Encuesta();
                                                        IdPregunta = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].IdPregunta.ToString();
                                                        Preguntas.Id = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].IdPregunta.ToString();
                                                        DescPregunta = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].PreguntaDesc;
                                                        Preguntas.Pregunta = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].PreguntaDesc;
                                                        Preguntas.Respuesta = new string[TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas.Length]; //{ "" + TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].IdRespuesta + "|" + TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].RespuestaDescripcion + "|3", "4|No|4" };

                                                        if (TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas != null)
                                                            for (int resp = 0; resp < TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas.Length; resp++)
                                                            {
                                                                Preguntas.Respuesta[resp] = TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].IdRespuesta + "|" +
                                                                                            TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].RespuestaDescripcion + "|" +
                                                                                            TDI_EncDisp[disp].ListaEncuesta[enc].LstPreg[preg].ListaRespuestas[resp].IdSiguientePregunta;

                                                            }

                                                        FillEncuesta.Add(Preguntas);
                                                        string RespuestasInserta = "";
                                                        int cuenta = 0;
                                                        foreach (string presp in Preguntas.Respuesta)
                                                        {
                                                            cuenta++;
                                                            int totalResp = Preguntas.Respuesta.Length;
                                                            if (totalResp == cuenta)
                                                            {
                                                                RespuestasInserta += presp.ToString();
                                                            }
                                                            else
                                                            {
                                                                RespuestasInserta += presp.ToString() + "&";
                                                            }

                                                        }
                                                        using (SqliteCommand com = connection.CreateCommand())
                                                        {
                                                            String strQuery = "INSERT INTO [DatosEncuesta] ([IdEncuesta],[DescEncuesta],[IdPregunta],[DescPregunta],[Respuestas]) values('" +
                                                                IdEncuesta + "','" + DescEncuesta + "','" + IdPregunta + "','" + DescPregunta + "','" + RespuestasInserta + "');";
                                                            com.CommandText = strQuery;
                                                            com.ExecuteNonQuery();
                                                        }
                                                    }
                                            }
                                    }
                            }
                            catch (Exception error)
                            {
                                var Err = error.Message;
                            }
                            MuestraPreguntas(IDSeleccionado,out IsFinal);                            
                            btnAtras.Visibility = ViewStates.Invisible;
                        }
                    }
                }
            }
            catch (Exception exep)
            {
                 var exe = exep.Message;
            }
            preguntasTot.Text = "Pregunta: " + NumPreguntas + "/" + (FillEncuesta.Count - 1);
        }
        
        /// <summary>
        /// Envia encuestas que se respondieron desde el dispositivo.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnAceptar_Click(object sender, EventArgs e)
        {
            LinearLayout lin = FindViewById<LinearLayout>(Resource.Id.linearLayout1);
            int contadorId = cuentaArrayId;
            if (NumPreguntas != FillEncuesta.Count && IDSeleccionado != 0 && IsFinal != "SI" && IsFinal != "")
            {
                arrayIds[indicePG] = IdPreguntaEnviada + "|" + IdRespuesta;
                indicePG++;
                
                MuestraPreguntas(IDSeleccionado, out IsFinal);
                NumPreguntas++;
                if (IsFinal == "SI")
                {
                    Intent Final = new Intent(this, typeof(ActividadFinal));
                    Final.SetFlags(ActivityFlags.ClearTop);
                    Final.PutExtra("IdDispositivo", IdDispositivo);
                    Final.PutExtra("IdEncuesta", IdEncuesta);
                    Final.PutExtra("arrayIds", arrayIds);
                    Servicio.ClsVariables.IdDispositivo = IdDispositivo;
                    Servicio.ClsVariables.IdEncuesta = IdEncuesta;
                    Servicio.ClsVariables.arrayIds = arrayIds;
                    tieneEncuesta = false;
                    StartActivity(Final);
                    this.SetVisible(false);
                    this.Finish();
                    
                }
                else
                {
                    lin.LayoutParameters = new LinearLayout.LayoutParams(WindowManagerLayoutParams.FillParent, WindowManagerLayoutParams.WrapContent, 1f);
                    pregunta.SetTextSize(Android.Util.ComplexUnitType.Dip, 20);
                    lista.Visibility = ViewStates.Visible;
                    preguntasTot.Text = "Pregunta:" + NumPreguntas + "/" + (FillEncuesta.Count - 1);
                }
                if (NumPreguntas > 0 && NumPreguntas < FillEncuesta.Count)
                {
                    btnAtras.Visibility = ViewStates.Visible;                    
                }
                if (NumPreguntas == FillEncuesta.Count)
                {
                    btnAtras.Visibility = ViewStates.Visible;                    
                }
                cuentaArrayId++;               
            }
            else
            {
            }            
        }       
        string proveedor;
        /// <summary>
        /// Funcion que crear objetos de tipo LocationManager para obtener coordenadas en el dispositivo
        /// </summary>
        private void InicializaLocalizacion()
        {
            try
            {
                _locationManager = (LocationManager)GetSystemService(LocationService);
                IList<String> listaProviders = _locationManager.GetProviders(true);

                LocationProvider provider = _locationManager.GetProvider(listaProviders[0]);
                Criteria criteria = new Criteria() { Accuracy = Accuracy.Fine };
                criteria.AltitudeRequired = true;
                String bestProvider = _locationManager.GetBestProvider(criteria, false);

                if (!_locationManager.IsProviderEnabled(LocationManager.GpsProvider))
                {
                    Toast.MakeText(this, "El GPS esta deshabilitado", ToastLength.Short).Show();
                }
                else
                {
                    Location lastKnownLocation = _locationManager.GetLastKnownLocation("network");
                    if (lastKnownLocation != null)
                    {
                        proveedor = "network";                        
                    }
                    else
                    {
                        lastKnownLocation = _locationManager.GetLastKnownLocation("gps");

                        if (lastKnownLocation != null)
                        {
                            proveedor = "gps";                            
                        }
                        else
                        {
                            proveedor = "network";
                        }
                    }
                }
                if (proveedor == "network")
                    _locationManager.RequestLocationUpdates(LocationManager.NetworkProvider, 60000, 0,this);
                else
                    _locationManager.RequestLocationUpdates(LocationManager.GpsProvider, 60000, 0, this);
            }
            catch
            { 
            }
        }
        private void mostrarPosicion(Location loc)
        {
            if (loc != null)
            {
                //Envia Coordenada
                try
                {
                    
                    
                    asmxEM.GuardaCoordenadasDispositivo(Servicio.ClsVariables.NumeroTel, loc.Latitude.ToString(), loc.Longitude.ToString(), loc.Bearing.ToString());
                }
                catch (Exception exep)
                { }
            }
            else
            {

            }
        }
        public void OnLocationChanged(Location location)
        {
            mostrarPosicion(location);
        }
        public void OnProviderDisabled(string provider)
        {

        }
        public void OnProviderEnabled(string provider)
        {
        }
        public void OnStatusChanged(string provider, Availability status, Bundle extras)
        {
        }
        private int IndiceArreglo(int idPreguntaSiguiente)
        {
            int indice = 0;
            for (int ini = 0; ini < FillEncuesta.Count; ini++)
            {              
                if (int.Parse(FillEncuesta[ini].Id.ToString()) == idPreguntaSiguiente)
                {
                    indice = ini;
                    break;
                }             
            }
            return indice;
        }
        private void MuestraPreguntas(int inicia,out string EsFinal)
        {          
            int pregsig = 0;
            if (inicia != 0)
            {
                pregsig = IndiceArreglo(inicia);
            }
            else
            {
                pregsig = 1;
            }
            IdPreguntaEnviada = FillEncuesta[pregsig].Id;
            pregunta.Text = FillEncuesta[pregsig].Pregunta;
            pregunta.Id = int.Parse(FillEncuesta[pregsig].Id);
            resp = new String[FillEncuesta[pregsig].Respuesta.Length];
            int tamañoRespuestas=resp.Length;
            string Final = "NO";
            if (tamañoRespuestas == 0)
            {
                Final = "SI";
            }
            for (int i = 0; i < FillEncuesta[pregsig].Respuesta.Length; i++)
            {
                resp[i] = FillEncuesta[pregsig].Respuesta[i].ToString() + "|" + FillEncuesta[pregsig].Id.ToString();
            }
            ListViewPersonalizado_Adapter ad = new ListViewPersonalizado_Adapter(this, resp);
            ListView lv = FindViewById<ListView>(Resource.Id.List);
            lv.Adapter = ad;
            #if __ANDROID_11__
                                    lv.ChoiceMode = Android.Widget.ChoiceMode.Single; // 1 
            #else
                        //lv.ChoiceMode = 1;
            #endif
            EsFinal = Final;
        }
    }
    public class ListViewPersonalizado_Adapter : ArrayAdapter<string>
    {
        Activity context;
        string[] objects;
        public ListViewPersonalizado_Adapter(Activity context, string[] objects)
            : base(context, Resource.Layout.list_personalizado, objects)
        {
            this.context = context;
            this.objects = objects;
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            ViewHolder vh;
            var view = convertView;
            if (view == null)
            {
                view = context.LayoutInflater.Inflate(Resource.Layout.list_personalizado, parent, false);
                vh = new ViewHolder();
                vh.Initialize(view);
                view.Tag = vh;
            }
            string dataObject = this.objects[position];
            vh = (ViewHolder)view.Tag;
            vh.Bind(dataObject);
            return view;
        }
        private class ViewHolder : Java.Lang.Object
        {
            CheckedTextView Datos;
            public void Initialize(View view)
            {
                Datos = view.FindViewById(Resource.Id.text1) as CheckedTextView;
               
            }
            public void Bind(string data)
            {
                Datos.ContentDescription = Datos.Hint = data.Split('|')[0];
                Datos.Text = data.Split('|')[1];
                Datos.Hint = data.Split('|')[3] + "|" + int.Parse(data.Split('|')[2]);
            }
        }
    }
}

