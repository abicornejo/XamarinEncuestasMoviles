using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EncuestasMoviles
{
    public partial class MessageBox : System.Web.UI.UserControl
    {

        public event EventHandler Acepta_Evento;
        private static int z;


        protected void Page_Load(object sender, EventArgs e)
        {            
        }
        public class MsgBoxEventArgs : System.EventArgs
        {
            public enmAnswer Answer;
            public string Args;

            public MsgBoxEventArgs(enmAnswer answer, string args)
            {
                Answer = answer;
                Args = args;
            }
        }

        public class Message
        {
            public Message(string messageText, enmMessageType messageType)
            {
                _messageText = messageText;
                _messageType = messageType;
            }

            private enmMessageType _messageType = enmMessageType.Info;
            private string _messageText = "";

            public enmMessageType MessageType
            {
                get { return _messageType; }
                set { _messageType = value; }
            }

            public string MessageText
            {
                get { return _messageText; }
                set { _messageText = value; }
            }
        }

        public enum enmAnswer
        {
            OK = 0,
            Cancel = 1
        }
        public enum enmMessageType
        {
            Error = 0,
            Success = 1,
            Attention = 2,
            Info = 3
        }

        public delegate void MsgBoxEventHandler(object sender, MsgBoxEventArgs e);
        public event MsgBoxEventHandler MsgBoxAnswered;

        public  string Args
        {
            get
            {
                if (ViewState["Args"] == null)
                    ViewState["Args"] = "";

                return (Convert.ToString(ViewState["Args"]));
            }
            set
            {
                ViewState["Args"] = value;
            }
        }

        protected int MessageNumber
        {
            get
            {
                return Messages.Count;
            }
        }

        private  List<Message> Messages = new List<Message>();

        public void AddMessage(string msgText, enmMessageType type, string TituloMensaje)
        {
            Messages.Add(new Message(msgText, type));

            Args = "";
            if (TituloMensaje != string.Empty)
            {
                this.lblTituMensaje.Text = TituloMensaje;
            }
            else
            {
                this.lblTituMensaje.Text = "Mensaje: ";
            }
            this.btnPostCancel.Visible = false;
            this.btnPostOK.Visible = false;
            this.btnOK.Visible = true;
        }

        public void AddMessage(string msgText, enmMessageType type, bool postPage, bool showCancelButton, string args, string TituloMensaje)
        {
            Messages.Add(new Message(msgText, type));

            if (!string.IsNullOrEmpty(args))
                Args = args;
            if (TituloMensaje != string.Empty)
            {
                this.lblTituMensaje.Text = TituloMensaje;
            }
            else
            {
                this.lblTituMensaje.Text = "Mensaje: ";
            }
            btnPostCancel.Visible = showCancelButton;
            btnPostOK.Visible = postPage;
            btnOK.Visible = !postPage;
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (btnOK.Visible == false)
                mpeMsg.OkControlID = "btnD2";
            else
                mpeMsg.OkControlID = "btnOK";

            if (Messages.Count > 0)
            {
                btnOK.Focus();
                grvMsg.DataSource = Messages;
                grvMsg.DataBind();

                mpeMsg.Show();
                udpMsj.Update();
            }
            else
            {
                grvMsg.DataBind();
                udpMsj.Update();
            }
            if (this.Parent.GetType() == typeof(UpdatePanel))
            {
                UpdatePanel containerUpdatepanel = this.Parent as UpdatePanel;

                containerUpdatepanel.Update();
            }
        }

        protected void btnPostOK_Click(object sender, EventArgs e)
        {
            if (MsgBoxAnswered != null)
            {
                MsgBoxAnswered(this, new MsgBoxEventArgs(enmAnswer.OK, Args));
                Args = "";
            }

            if (Acepta_Evento != null)
            {
                Acepta_Evento(sender, e);
            }
        }

        protected void btnPostCancel_Click(object sender, EventArgs e)
        {
            if (MsgBoxAnswered != null)
            {
                MsgBoxAnswered(this, new MsgBoxEventArgs(enmAnswer.Cancel, Args));
                Args = "";
            }
          
        
        }
    }
}