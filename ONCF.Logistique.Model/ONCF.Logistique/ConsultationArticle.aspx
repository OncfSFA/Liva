<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true"
    CodeBehind="ConsultationArticle.aspx.cs" Inherits="ONCF.Logistique.ConsultationArticleHabiement" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
    <asp:UpdatePanel runat="server" ID="Panel">
        <ContentTemplate>
            <div runat="server" id="divTF" class="top-right">
                <h4>
                    statistique des articles&nbsp;
                </h4>
            </div>
            <div class="contenu-right" style="display: block; background-color: White;">
                <div class="left-form">
                    <div class="left-form">
                        <rsweb:ReportViewer Width = "100%" ID="RP_Article" runat="server" >
                        </rsweb:ReportViewer>
                    </div>
                </div>
                <br />
            </div>
            <!-- Un bloc pour affiche le message de confirmation d'ajout-->
            <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel1"
                BackgroundCssClass="modalBackground" TargetControlID="Label92" RepositionMode="RepositionOnWindowResize">
            </cc1:ModalPopupExtender>
            <asp:Label ID="Label92" runat="server" Text=""></asp:Label>
            <asp:Panel ID="Panel1" runat="server" Height="150px" Width="379px">
                <table class="modal">
                    <tr>
                        <td class="modal_header">
                            <h3 id="title" runat="server">
                            </h3>
                        </td>
                    </tr>
                    <tr>
                        <td class="modal_body" align="center">
                            <p>
                                <asp:Label ID="msg" runat="server" Text=""></asp:Label>
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" valign="top">
                            <div class="btn-ConfirmRight1">
                                <asp:Button ID="Button1" class="bts" runat="server" Text="ok" CausesValidation="False" />
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!------------------------------------------------------------>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
