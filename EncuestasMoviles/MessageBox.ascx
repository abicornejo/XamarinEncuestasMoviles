<%@ Control Language="C#" ClassName="MessageBox" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs" Inherits="EncuestasMoviles.MessageBox" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<style type="text/css">
.ModalPopupBG
{
    position:absolute;
    background-color:Silver;
	filter: alpha(opacity=50);
	opacity: 0.7;
	height: 120px;
	width: 50px;
	z-index:20;
}

.popup_Container {
	background-color:#fffeb2;
	border:2px solid #000000;
	padding: 0px 0px 0px 0px;
}

.popupConfirmation
{
	height: 120px;
	width: 50px;
	background-color:Yellow;
}

.popup_Titlebar {
	background: url(../Images/titlebar_bg.jpg);
	height: 29px;
}

.popup_Body
{
	padding:15px 15px 15px 15px;
	font-family:Arial;
	font-weight:bold;
	font-size:12px;
	color:Black;
	line-height:15pt;
	clear:both;
	padding:5px;
	background-color:#A9E2F3;
	text-align:center;
}

.mpHd
{
    color:Black;
    background-color:#A9E2F3;
}

.gvEstiloMen
{
    padding:15px 15px 15px 15px;
	font-family:Arial;
	font-weight:bold;
	font-size:12px;
	color:Black;
	line-height:15pt;
	clear:both;
	padding:5px;
    background-color:#A9E2F3;
	height: 120px;
	width: 80px;
}

.mpClose
{
    color:Black;
    background-color:#A9E2F3;
    text-align:right;
}
</style>

<link href="Styles/Site.css"  rel="stylesheet" type="text/css"/>
<asp:UpdatePanel ID="udpMsj" runat="server" UpdateMode="Conditional" RenderMode="Inline">
    <ContentTemplate>
        <asp:Button ID="btnD" runat="server" Text="" Style="display: none" Width="0" Height="0" />
        <asp:Button ID="btnD2" runat="server" Text="" Style="display: none" Width="0" Height="0" />
       <%-- <table width="550" style="display: none" class="ContenedorGeneral" id="pnlMsg">
            <tr>
                <td class="MasterTituloContenedor">
                    <h2><asp:Label runat="server" ID="lblTituMensaje"></asp:Label></h2>    
                </td>                
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="grvMsg" runat="server" ShowHeader="false" Width="100%" AutoGenerateColumns="false">
                        <Columns>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Image ID="imgErr" runat="server" ImageUrl="~/Images/err.png"
                                                    Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Error) ? true : false %>' />
                                                <asp:Image ID="imgSuc" runat="server" ImageUrl= "~/Images/suc.png"
                                                    Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Success) ? true : false %>' />
                                                <asp:Image ID="imgAtt" runat="server" ImageUrl= "~/Images/att.png"
                                                    Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Attention) ? true : false %>' />
                                                <asp:Image ID="imgInf" runat="server" ImageUrl= "~/Images/inf.png"
                                                    Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Info) ? true : false %>' />
                                            </td>
                                            <td>
                                                 <div>
                                                 <%# Eval("MessageText")%>
                                                 </div>                                        
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:Button ID="btnOK" CssClass="btnMasterRectangular" runat="server" Text="OK" CausesValidation="false" Width="60px" />
                    <asp:Button ID="btnPostOK" CssClass="btnMasterRectangular" runat="server" Text="OK" CausesValidation="false" OnClick="btnPostOK_Click"
                        Visible="false" Width="60px" />
                    <asp:Button ID="btnPostCancel" CssClass="btnMasterRectangular" runat="server" Text="Cancel" CausesValidation="false"
                        OnClick="btnPostCancel_Click" Visible="false" Width="60px" />
                </td>
            </tr>
        </table>--%>
        <asp:Panel ID="pnlMsg" runat="server" CssClass="ContenedorGeneral" Style="display: none" Width="550px">
            <asp:Panel ID="pnlMsgHD" runat="server" CssClass="MasterTituloContenedor">
             
                <h2><asp:Label runat="server" ID="lblTituMensaje"></asp:Label></h2>
            </asp:Panel>
            <asp:GridView ID="grvMsg" runat="server" ShowHeader="false" Width="100%" AutoGenerateColumns="false">
                <Columns>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Image ID="imgErr" runat="server" ImageUrl="~/Images/err.png"
                                            Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Error) ? true : false %>' />
                                        <asp:Image ID="imgSuc" runat="server" ImageUrl= "~/Images/suc.png"
                                            Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Success) ? true : false %>' />
                                        <asp:Image ID="imgAtt" runat="server" ImageUrl= "~/Images/att.png"
                                            Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Attention) ? true : false %>' />
                                        <asp:Image ID="imgInf" runat="server" ImageUrl= "~/Images/inf.png"
                                            Visible=' <%# (((Message)Container.DataItem).MessageType == enmMessageType.Info) ? true : false %>' />
                                    </td>
                                    <td>
                                         <div>
                                         <%# Eval("MessageText")%>
                                         </div>                                        
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <div style="text-align:center; padding:10px 0 10px 0">
                <asp:Button ID="btnOK" CssClass="btnMasterRectangular" runat="server" Text="OK" CausesValidation="false" Width="60px" />
                <asp:Button ID="btnPostOK" CssClass="btnMasterRectangular" runat="server" Text="OK" CausesValidation="false" OnClick="btnPostOK_Click"
                    Visible="false" Width="60px" />
                <asp:Button ID="btnPostCancel" CssClass="btnMasterRectangular" runat="server" Text="Cancel" CausesValidation="false"
                    OnClick="btnPostCancel_Click" Visible="false" Width="60px" />
            </div>
        </asp:Panel>
        <asp:ModalPopupExtender ID="mpeMsg" runat="server" TargetControlID="btnD"
            PopupControlID="pnlMsg" PopupDragHandleControlID="pnlMsgHD" BackgroundCssClass="ModalPopupBG"
            DropShadow="true" OkControlID="btnOK">
        </asp:ModalPopupExtender>
    </ContentTemplate>
</asp:UpdatePanel>