using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entidades_EncuestasMoviles;
using Telerik.Charting;
using Telerik.Web.UI;
using Telerik.Charting.Styles;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO; 
using System.Web.UI.HtmlControls;
using BLL_EncuestasMoviles;
using System.Web.Services;

namespace EncuestasMoviles.Pages
{
    public partial class ExportaPDF : System.Web.UI.Page
    {
       
        string NombEncu = "";
        String FechCreaEncu = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("~/Default.aspx");
                   
                }

                List<THE_SesionUsuario> existeSesion =MngNegocioUsuarioSesion.VerExisteSesionUsuario(Int32.Parse(Session["numeroUsuario"].ToString()), Session["UserIP"].ToString());
                if (existeSesion.Count == 0)
                {
                    return;
                }

                if (!IsPostBack)
                {

                    using (MemoryStream myMemoryStream = new MemoryStream())
                    {
                        Document document = new Document(iTextSharp.text.PageSize.LEGAL.Rotate());
                        PdfWriter PDFWriter = PdfWriter.GetInstance(document, myMemoryStream);
                        string map = Server.MapPath("~/Images/Header.png");
                        iTextSharp.text.Image encabezado = iTextSharp.text.Image.GetInstance(map);
                        encabezado.ScaleAbsoluteWidth(document.PageSize.Width - 70);
                        encabezado.ScaleAbsoluteHeight(80);
                        var mySize = document.PageSize;
                        document.AddHeader("Content-Disposition", "attachment; filename=wissalReport.pdf");

                        document.Open();
                        int recorre =Convert.ToInt32(HttpContext.Current.Application.Get("contador").ToString());

                        for (int i = 0; i < recorre; i++)
                        {

                            Chunk chunk = new Chunk(DateTime.Now.ToString(), FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.NORMAL));
                            document.Add(new Paragraph(chunk));

                            document.Add(encabezado);

                            Byte[] imagebyte = Convert.FromBase64String(HttpContext.Current.Application.Get(i.ToString()).ToString());
                            iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(imagebyte);
                            float wid = 700;
                            float heg = 420;
                            myImage.ScaleAbsolute(wid, heg);
                            myImage.Alignment = Element.ALIGN_CENTER;
                            document.Add(myImage);
                        }

                        document.Close();
                        byte[] content = myMemoryStream.ToArray();
                        HttpContext.Current.Response.Buffer = false;
                        HttpContext.Current.Response.Clear();
                        HttpContext.Current.Response.ClearContent();
                        HttpContext.Current.Response.ClearHeaders();
                        HttpContext.Current.Response.AppendHeader("content-disposition", "attachment;filename=" + "my_report.pdf");
                        HttpContext.Current.Response.ContentType = "Application/pdf";                          
                        HttpContext.Current.Response.BinaryWrite(content);
                        HttpContext.Current.Response.Flush();
                        HttpContext.Current.Response.End();
                    }
                }
            }catch(Exception ms){
            }finally{}
        }
        int indexCharts = 0;
 

        [WebMethod()]
        public static string crearImagenes(List<string> imageData)
        {            
            string result = "";         
            HttpContext.Current.Application.RemoveAll();
            try
            {              
                for (int i = 0; i < imageData.Count; i++)
                {
                    HttpContext.Current.Application.Add(i.ToString(), imageData[i]);                    
                }
                result = "{'nImagenes':'"+imageData.Count.ToString()+"'}";
                HttpContext.Current.Application.Add("contador", imageData.Count);
                return result;

            }catch(Exception ex){
                result="";
                return result;
            }         
            
        }       
    }
}
