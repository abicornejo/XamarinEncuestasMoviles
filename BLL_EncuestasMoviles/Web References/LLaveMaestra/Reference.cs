﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.269.
// 
#pragma warning disable 1591

namespace BLL_EncuestasMoviles.LLaveMaestra {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.ComponentModel;
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="ServiceSoap", Namespace="http://tempuri.org/")]
    public partial class Service : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback Azt_Serv1OperationCompleted;
        
        private System.Threading.SendOrPostCallback Azt_Serv2OperationCompleted;
        
        private System.Threading.SendOrPostCallback DatosEmpleadoLlavMaestOperationCompleted;
        
        private System.Threading.SendOrPostCallback ValidarDataFingerOperationCompleted;
        
        private System.Threading.SendOrPostCallback buscaNombresOperationCompleted;
        
        private System.Threading.SendOrPostCallback gsLlaveMaestraOperationCompleted;
        
        private System.Threading.SendOrPostCallback gsc_llaveOperationCompleted;
        
        private System.Threading.SendOrPostCallback regresaDatosEmpleadoOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public Service() {
            this.Url = global::BLL_EncuestasMoviles.Properties.Settings.Default.BLL_EncuestasMoviles_LLaveMaestra_Service;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event Azt_Serv1CompletedEventHandler Azt_Serv1Completed;
        
        /// <remarks/>
        public event Azt_Serv2CompletedEventHandler Azt_Serv2Completed;
        
        /// <remarks/>
        public event DatosEmpleadoLlavMaestCompletedEventHandler DatosEmpleadoLlavMaestCompleted;
        
        /// <remarks/>
        public event ValidarDataFingerCompletedEventHandler ValidarDataFingerCompleted;
        
        /// <remarks/>
        public event buscaNombresCompletedEventHandler buscaNombresCompleted;
        
        /// <remarks/>
        public event gsLlaveMaestraCompletedEventHandler gsLlaveMaestraCompleted;
        
        /// <remarks/>
        public event gsc_llaveCompletedEventHandler gsc_llaveCompleted;
        
        /// <remarks/>
        public event regresaDatosEmpleadoCompletedEventHandler regresaDatosEmpleadoCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Azt_Serv1", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Azt_Serv1(string Param1, string Param2) {
            object[] results = this.Invoke("Azt_Serv1", new object[] {
                        Param1,
                        Param2});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void Azt_Serv1Async(string Param1, string Param2) {
            this.Azt_Serv1Async(Param1, Param2, null);
        }
        
        /// <remarks/>
        public void Azt_Serv1Async(string Param1, string Param2, object userState) {
            if ((this.Azt_Serv1OperationCompleted == null)) {
                this.Azt_Serv1OperationCompleted = new System.Threading.SendOrPostCallback(this.OnAzt_Serv1OperationCompleted);
            }
            this.InvokeAsync("Azt_Serv1", new object[] {
                        Param1,
                        Param2}, this.Azt_Serv1OperationCompleted, userState);
        }
        
        private void OnAzt_Serv1OperationCompleted(object arg) {
            if ((this.Azt_Serv1Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Azt_Serv1Completed(this, new Azt_Serv1CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/Azt_Serv2", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string Azt_Serv2(string Param1, string Param2) {
            object[] results = this.Invoke("Azt_Serv2", new object[] {
                        Param1,
                        Param2});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void Azt_Serv2Async(string Param1, string Param2) {
            this.Azt_Serv2Async(Param1, Param2, null);
        }
        
        /// <remarks/>
        public void Azt_Serv2Async(string Param1, string Param2, object userState) {
            if ((this.Azt_Serv2OperationCompleted == null)) {
                this.Azt_Serv2OperationCompleted = new System.Threading.SendOrPostCallback(this.OnAzt_Serv2OperationCompleted);
            }
            this.InvokeAsync("Azt_Serv2", new object[] {
                        Param1,
                        Param2}, this.Azt_Serv2OperationCompleted, userState);
        }
        
        private void OnAzt_Serv2OperationCompleted(object arg) {
            if ((this.Azt_Serv2Completed != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.Azt_Serv2Completed(this, new Azt_Serv2CompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/DatosEmpleadoLlavMaest", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string DatosEmpleadoLlavMaest(string LlavMaest) {
            object[] results = this.Invoke("DatosEmpleadoLlavMaest", new object[] {
                        LlavMaest});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void DatosEmpleadoLlavMaestAsync(string LlavMaest) {
            this.DatosEmpleadoLlavMaestAsync(LlavMaest, null);
        }
        
        /// <remarks/>
        public void DatosEmpleadoLlavMaestAsync(string LlavMaest, object userState) {
            if ((this.DatosEmpleadoLlavMaestOperationCompleted == null)) {
                this.DatosEmpleadoLlavMaestOperationCompleted = new System.Threading.SendOrPostCallback(this.OnDatosEmpleadoLlavMaestOperationCompleted);
            }
            this.InvokeAsync("DatosEmpleadoLlavMaest", new object[] {
                        LlavMaest}, this.DatosEmpleadoLlavMaestOperationCompleted, userState);
        }
        
        private void OnDatosEmpleadoLlavMaestOperationCompleted(object arg) {
            if ((this.DatosEmpleadoLlavMaestCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.DatosEmpleadoLlavMaestCompleted(this, new DatosEmpleadoLlavMaestCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ValidarDataFinger", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ValidarDataFinger(string Param1) {
            object[] results = this.Invoke("ValidarDataFinger", new object[] {
                        Param1});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ValidarDataFingerAsync(string Param1) {
            this.ValidarDataFingerAsync(Param1, null);
        }
        
        /// <remarks/>
        public void ValidarDataFingerAsync(string Param1, object userState) {
            if ((this.ValidarDataFingerOperationCompleted == null)) {
                this.ValidarDataFingerOperationCompleted = new System.Threading.SendOrPostCallback(this.OnValidarDataFingerOperationCompleted);
            }
            this.InvokeAsync("ValidarDataFinger", new object[] {
                        Param1}, this.ValidarDataFingerOperationCompleted, userState);
        }
        
        private void OnValidarDataFingerOperationCompleted(object arg) {
            if ((this.ValidarDataFingerCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ValidarDataFingerCompleted(this, new ValidarDataFingerCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/buscaNombres", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string buscaNombres(string strNombre) {
            object[] results = this.Invoke("buscaNombres", new object[] {
                        strNombre});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void buscaNombresAsync(string strNombre) {
            this.buscaNombresAsync(strNombre, null);
        }
        
        /// <remarks/>
        public void buscaNombresAsync(string strNombre, object userState) {
            if ((this.buscaNombresOperationCompleted == null)) {
                this.buscaNombresOperationCompleted = new System.Threading.SendOrPostCallback(this.OnbuscaNombresOperationCompleted);
            }
            this.InvokeAsync("buscaNombres", new object[] {
                        strNombre}, this.buscaNombresOperationCompleted, userState);
        }
        
        private void OnbuscaNombresOperationCompleted(object arg) {
            if ((this.buscaNombresCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.buscaNombresCompleted(this, new buscaNombresCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/gsLlaveMaestra", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string gsLlaveMaestra(string strXMLencript) {
            object[] results = this.Invoke("gsLlaveMaestra", new object[] {
                        strXMLencript});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void gsLlaveMaestraAsync(string strXMLencript) {
            this.gsLlaveMaestraAsync(strXMLencript, null);
        }
        
        /// <remarks/>
        public void gsLlaveMaestraAsync(string strXMLencript, object userState) {
            if ((this.gsLlaveMaestraOperationCompleted == null)) {
                this.gsLlaveMaestraOperationCompleted = new System.Threading.SendOrPostCallback(this.OngsLlaveMaestraOperationCompleted);
            }
            this.InvokeAsync("gsLlaveMaestra", new object[] {
                        strXMLencript}, this.gsLlaveMaestraOperationCompleted, userState);
        }
        
        private void OngsLlaveMaestraOperationCompleted(object arg) {
            if ((this.gsLlaveMaestraCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.gsLlaveMaestraCompleted(this, new gsLlaveMaestraCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/gsc_llave", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string gsc_llave(string Param1, string Param2) {
            object[] results = this.Invoke("gsc_llave", new object[] {
                        Param1,
                        Param2});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void gsc_llaveAsync(string Param1, string Param2) {
            this.gsc_llaveAsync(Param1, Param2, null);
        }
        
        /// <remarks/>
        public void gsc_llaveAsync(string Param1, string Param2, object userState) {
            if ((this.gsc_llaveOperationCompleted == null)) {
                this.gsc_llaveOperationCompleted = new System.Threading.SendOrPostCallback(this.Ongsc_llaveOperationCompleted);
            }
            this.InvokeAsync("gsc_llave", new object[] {
                        Param1,
                        Param2}, this.gsc_llaveOperationCompleted, userState);
        }
        
        private void Ongsc_llaveOperationCompleted(object arg) {
            if ((this.gsc_llaveCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.gsc_llaveCompleted(this, new gsc_llaveCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/regresaDatosEmpleado", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string regresaDatosEmpleado(string LlavMaest) {
            object[] results = this.Invoke("regresaDatosEmpleado", new object[] {
                        LlavMaest});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void regresaDatosEmpleadoAsync(string LlavMaest) {
            this.regresaDatosEmpleadoAsync(LlavMaest, null);
        }
        
        /// <remarks/>
        public void regresaDatosEmpleadoAsync(string LlavMaest, object userState) {
            if ((this.regresaDatosEmpleadoOperationCompleted == null)) {
                this.regresaDatosEmpleadoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnregresaDatosEmpleadoOperationCompleted);
            }
            this.InvokeAsync("regresaDatosEmpleado", new object[] {
                        LlavMaest}, this.regresaDatosEmpleadoOperationCompleted, userState);
        }
        
        private void OnregresaDatosEmpleadoOperationCompleted(object arg) {
            if ((this.regresaDatosEmpleadoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.regresaDatosEmpleadoCompleted(this, new regresaDatosEmpleadoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Azt_Serv1CompletedEventHandler(object sender, Azt_Serv1CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Azt_Serv1CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Azt_Serv1CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void Azt_Serv2CompletedEventHandler(object sender, Azt_Serv2CompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class Azt_Serv2CompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal Azt_Serv2CompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void DatosEmpleadoLlavMaestCompletedEventHandler(object sender, DatosEmpleadoLlavMaestCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class DatosEmpleadoLlavMaestCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal DatosEmpleadoLlavMaestCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void ValidarDataFingerCompletedEventHandler(object sender, ValidarDataFingerCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ValidarDataFingerCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ValidarDataFingerCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void buscaNombresCompletedEventHandler(object sender, buscaNombresCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class buscaNombresCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal buscaNombresCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void gsLlaveMaestraCompletedEventHandler(object sender, gsLlaveMaestraCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class gsLlaveMaestraCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal gsLlaveMaestraCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void gsc_llaveCompletedEventHandler(object sender, gsc_llaveCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class gsc_llaveCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal gsc_llaveCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    public delegate void regresaDatosEmpleadoCompletedEventHandler(object sender, regresaDatosEmpleadoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class regresaDatosEmpleadoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal regresaDatosEmpleadoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591