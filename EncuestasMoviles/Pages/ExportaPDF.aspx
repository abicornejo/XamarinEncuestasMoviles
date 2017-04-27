<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExportaPDF.aspx.cs" Inherits="EncuestasMoviles.Pages.ExportaPDF" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Charting" Assembly="Telerik.Web.UI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow: auto; width: 100%; height: 500px; position: static">
        <table id="TablaGraficas" runat="server" cellspacing="0" cellpadding="0" border="0"
            summary="" style="text-align: center">
        </table>
    </div> 
    <input type="hidden" id="hdnIdEncuesta" runat="server" />
    <asp:HiddenField ID="idtemporal" runat="server" EnableViewState="true" ViewStateMode="Enabled" ClientIDMode="Static" />
    </form>
</body>
</html>
