using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Charting;
using Telerik.Web.UI;
using Telerik.Charting.Styles;
using System.Drawing;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace EncuestasMoviles.Pages.Reportes
{
    public partial class ReportePrueba : System.Web.UI.Page
    {
        ChartSeries series1G4;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Session["UserName"] == null || Session["UserName"].ToString() == "")
                {
                    Response.Redirect("~/Default.aspx");
                }

                if (!Page.IsPostBack)
                {
                    InitComponents();
                }
            }
            catch (Exception ms)
            {

            }
            finally { 
            
            }
                    
        }

        void MuestraGraficas()
        {
 
        }


		void SetRandomSalaries()
		{
            
		}

		void SetXAxis(string Pregunta, int NumeroDePreguntas, RadChart Grafica)
		{
            
                Grafica.ChartTitle.TextBlock.Text = Pregunta; 
                Grafica.PlotArea.XAxis.AutoScale = false;
                Grafica.PlotArea.XAxis.Clear();
                Grafica.PlotArea.XAxis.AddItem(NumeroDePreguntas.ToString("#,###") + " Encuestas"); 
           

		}

		void SetYAxis(int minSalary)
		{	
			
                RadChart1.PlotArea.YAxis.AddRange(minSalary, minSalary + 1000, 200);
                RadChart2.PlotArea.YAxis.AddRange(minSalary, minSalary + 1000, 200);
                RadChart3.PlotArea.YAxis.AddRange(minSalary, minSalary + 1000, 200);
                RadChart4.PlotArea.YAxis.AddRange(minSalary, minSalary + 1000, 200);
		}

	    static void SetSeriesAppearance(ChartSeries series)
		{
			series.Appearance.Border.Color = System.Drawing.Color.Transparent;
            series.Appearance.LabelAppearance.Border.Color = System.Drawing.Color.Black;
            series.Appearance.LabelAppearance.FillStyle.MainColor = System.Drawing.Color.White;
            series.Appearance.TextAppearance.TextProperties.Font = new System.Drawing.Font("Arial", 8);
            series.Appearance.TextAppearance.TextProperties.Color = System.Drawing.Color.Black;
            series.Appearance.LabelAppearance.Dimensions.Margins = new ChartMargins(5);
            series.DefaultLabelValue = "#Y{N0}";			
		}

		void InitRadChart()
        {
            #region pregunta 1
            
            RadChart1.Clear();
            SetXAxis("1.- ¿Te Gustan Los VideoJuegos??", 3500, RadChart1);
            RadChart1.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;

            RadChart1.PlotArea.YAxis.AutoScale = false;
            SetYAxis(500);
            
            ChartSeries series1 = new ChartSeries("Si", ChartSeriesType.Bar);
            series1.ActiveRegionUrl = "#P2";
            SetSeriesAppearance(series1);
            RadChart1.Series.Add(series1);

            ChartSeries series2 = new ChartSeries("No", ChartSeriesType.Bar);
            series2.ActiveRegionUrl = "#P3";
            SetSeriesAppearance(series2);
            RadChart1.Series.Add(series2);

            ChartSeries series3 = new ChartSeries("Un poco", ChartSeriesType.Bar);
            series3.ActiveRegionUrl = "#P4";
            SetSeriesAppearance(series3);
            RadChart1.Series.Add(series3);

            series1.AddItem(1300);
            series2.AddItem(1200);
            series3.AddItem(1000);

            #endregion


            #region pregunta 2

            RadChart2.Clear();
            SetXAxis(" 2.- ¿Que tiempo te la pasas jugando al día?", 3500, RadChart2);
            RadChart2.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;

            RadChart2.PlotArea.YAxis.AutoScale = false;
            SetYAxis(500);

            ChartSeries series1G2 = new ChartSeries("2 hrs al día", ChartSeriesType.Bar);
            SetSeriesAppearance(series1G2);
            RadChart2.Series.Add(series1G2);

            ChartSeries series2G2 = new ChartSeries("Entre 4 y 6 horas", ChartSeriesType.Bar);
            SetSeriesAppearance(series2G2);
            RadChart2.Series.Add(series2G2);

            ChartSeries series3G2 = new ChartSeries("Todo el día", ChartSeriesType.Bar);
            SetSeriesAppearance(series3G2);
            RadChart2.Series.Add(series3G2);

            series1G2.AddItem(1100);
            series2G2.AddItem(1300);
            series3G2.AddItem(1100);

            #endregion



            #region pregunta 2

            RadChart3.Clear();
            SetXAxis("3.- ¿Por que no te gustan?", 3500, RadChart3);
            RadChart3.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;

            RadChart3.PlotArea.YAxis.AutoScale = false;
            SetYAxis(500);

            ChartSeries series1G3 = new ChartSeries("Es una perdida de tiempo", ChartSeriesType.Bar);
            SetSeriesAppearance(series1G3);
            RadChart3.Series.Add(series1G3);

            ChartSeries series2G3 = new ChartSeries("No tengo una consola", ChartSeriesType.Bar);
            SetSeriesAppearance(series2G3);
            RadChart3.Series.Add(series2G3);

            ChartSeries series3G3 = new ChartSeries("No me gustan", ChartSeriesType.Bar);
            SetSeriesAppearance(series3G3);
            RadChart3.Series.Add(series3G3);

            series1G3.AddItem(700);
            series2G3.AddItem(800);
            series3G3.AddItem(2000);

            #endregion


            #region pregunta 2

            RadChart4.Clear();
            SetXAxis(" 4.- ¿Que Juego te gusta mas?", 3500, RadChart4);
            RadChart4.PlotArea.XAxis.LayoutMode = ChartAxisLayoutMode.Between;

            RadChart4.PlotArea.YAxis.AutoScale = false;
            SetYAxis(500);

            series1G4 = new ChartSeries("Mario", ChartSeriesType.Bar);
            SetSeriesAppearance(series1G4);
            RadChart4.Series.Add(series1G4);

            ChartSeries series2G4 = new ChartSeries("Fifa 11", ChartSeriesType.Bar);
            SetSeriesAppearance(series2G4);
            RadChart4.Series.Add(series2G4);

            ChartSeries series3G4 = new ChartSeries("Stars wars", ChartSeriesType.Bar);
            SetSeriesAppearance(series3G4);
            RadChart4.Series.Add(series3G4);

            series1G4.AddItem(600);
            series2G4.AddItem(800);
            series3G4.AddItem(2100);

            #endregion

        }

		void InitComponents()
		{
			InitRadChart();			
		}

		

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
		{
			//SetXAxis(dropDownQuarter.SelectedIndex);

			SetRandomSalaries();
		}

		protected void btnAddEmployee_Click(object sender, EventArgs e)
		{	
            
		}

		protected void btnApplyMinSalary_Click(object sender, EventArgs e)
		{
            
		}

       

		private void dropDownEmployeeName_SelectedIndexChanged(object sender, EventArgs e)
		{

            

		}

        protected void exportPDF_Click(object sender, EventArgs e)
        {
           // Document pdfDoc = new Document();
           // PdfWriter.GetInstance(pdfDoc, Response.OutputStream);

           // Phrase phrase = new iTextSharp.text.Phrase("Encuesta: ", new iTextSharp.text.Font(iTextSharp.text.Font.NORMAL, 12));
           // HeaderFooter header = new HeaderFooter(phrase, false);
           // pdfDoc.Header = header;

            

           // iTextSharp.text.Table table = new iTextSharp.text.Table(TablaGraficas.Rows.Count - 2);
           // table.Cellpadding = 3;

           
           // table.Width = 100;
           
           // HeaderFooter footer = new HeaderFooter(new Phrase("Pagina: "), true);
           // footer.Alignment = Element.ALIGN_CENTER;
           // footer.Border = iTextSharp.text.Rectangle.ALIGN_CENTER;
           // pdfDoc.Footer = footer;

           // iTextSharp.text.Table tableheader = new iTextSharp.text.Table(2, 1);
           // tableheader.Border = iTextSharp.text.Rectangle.NO_BORDER;
           // tableheader.Width = 100;
           // tableheader.SetWidths(new int[2] { 1, 3 });
           // iTextSharp.text.Cell cellheader = new iTextSharp.text.Cell();
           // cellheader.Border = iTextSharp.text.Rectangle.NO_BORDER;
           // cellheader.HorizontalAlignment = Element.ALIGN_CENTER;
           // cellheader.VerticalAlignment = Element.ALIGN_CENTER;
           // string map = Server.MapPath("~/Images/Header.png");
           // iTextSharp.text.Image VMgif = iTextSharp.text.Image.GetInstance(map);
           // VMgif.ScalePercent(50f);
           // VMgif.SpacingAfter = 10f;
           // VMgif.SpacingBefore = 10f;
           // cellheader.Add(VMgif);
           // tableheader.AddCell(cellheader);
           // cellheader = new Cell();
           //   cellheader.HorizontalAlignment = Element.ALIGN_TOP;
           // cellheader.VerticalAlignment = Element.ALIGN_LEFT;
           // tableheader.AddCell(cellheader);
           // tableheader.Spacing = 15;

            

           // pdfDoc.Open();

           //for (int i = 0; i < TablaGraficas.Rows.Count; i++)
           //{
           //    try
           //    {
           //        iTextSharp.text.Table tableimg = new iTextSharp.text.Table(1, 1);
           //        tableimg.Spacing = 50;
           //        tableimg.Border = iTextSharp.text.Rectangle.ALIGN_CENTER;
           //        tableimg.Width = 100;
           //        iTextSharp.text.Cell cellimg = new Cell();
           //        cellimg.HorizontalAlignment = Element.ALIGN_BOTTOM;
           //        cellimg.VerticalAlignment = Element.ALIGN_CENTER;

           //        RadChart rads = ((RadChart)TablaGraficas.Rows[i].Controls[0].FindControl("RadChart" + i));
           //        var imgStream = new MemoryStream();
           //        rads.Save(imgStream, System.Drawing.Imaging.ImageFormat.Png);
           //        iTextSharp.text.Image img = iTextSharp.text.Image.GetInstance(imgStream.ToArray());

           //        string imgMap = Server.MapPath("~/Images/encuestaMovilLogo.png");
           //        iTextSharp.text.Image gif = iTextSharp.text.Image.GetInstance(img);
           //        gif.ScalePercent(100f);
           //        cellimg.Add(gif);
           //        tableimg.AddCell(cellimg);
           //        pdfDoc.Add(tableheader);
           //        pdfDoc.Add(tableimg);
           //        pdfDoc.NewPage();
           //        pdfDoc.Add(table);
           //    }
           //    catch
           //    {
           //    }
           //}
            
           // pdfDoc.Close();
           // Response.ContentType = "application/pdf";
           // Response.AddHeader("content-disposition", "attachment;" + "filename=Monitoreo.pdf");
           // Response.Cache.SetCacheability(HttpCacheability.NoCache);
           // Response.Write(pdfDoc);
           // Response.End();
        }
    }
}