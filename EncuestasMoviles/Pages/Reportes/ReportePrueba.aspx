<%@ Page Title="" Language="C#" MasterPageFile="~/MasterEncuesta.Master" AutoEventWireup="true" CodeBehind="ReportePrueba.aspx.cs" Inherits="EncuestasMoviles.Pages.Reportes.ReportePrueba" 
%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Body" runat="server" >
        
        <%--<div id="GraficasAspx" runat="server"/>--%>
        <asp:Button runat="server" ID="exportPDF" Text="Exportar PDF" 
            onclick="exportPDF_Click" />
        <!-- content start -->
        <div style="overflow:auto; width:100%; height:500px; position:static">
        <table ID="TablaGraficas" runat="server" cellspacing="0" cellpadding="0" border="0" summary="" style="text-align:center">
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart1" runat="server"/> <H1 id="P1">.</H1> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart5" runat="server"/> <H1 id="P2"> .</H1></td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart2" runat="server"/>  </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart11" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart12" runat="server"/><H1 id="P3">.</H1> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart3" runat="server"/>   </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart13" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart14" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart15" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart16" runat="server"/><H1 id="P4">. </H1> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart4" runat="server"/>  </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart6" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart7" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart8" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart9" runat="server"/> </td> </tr>
            <tr> <td style="vertical-align: top; width: 682px;"> <telerik:RadChart ID="RadChart10" runat="server"/> </td> </tr>
        </table>
        </div>
        <!-- content end -->
</asp:Content>
