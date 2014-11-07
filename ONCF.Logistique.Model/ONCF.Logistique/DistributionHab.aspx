<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true"
    CodeBehind="DistributionHab.aspx.cs" Inherits="ONCF.Logistique.DistributionHab" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
    <asp:UpdatePanel runat="server" ID="Panel" UpdateMode="Conditional">
        <ContentTemplate>
            <div runat="server" id="divTF" class="top-right">
                <h4>
                    Distribution des Articles
                </h4>
            </div>
            <div class="contenu-right" style="display: block; background-color: White;">
                <div class="left-form">
                    <div class="left-form">
                        <table id="Table1" width="100%" runat="server">
                            <tr id="TableZom" runat="server">
                                <td>
                                    <p>
                                        <label runat="server" id="Label1">
                                            Article En cours :&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                        <asp:DropDownList ID="DDL_Article" runat="server" Height="16px" Width="142px" OnSelectedIndexChanged="DDL_Article_SelectedIndexChanged"
                                            AutoPostBack="True" DataSourceID="SDS_Article" DataTextField="ArticlePrevision_ArticleId"
                                            DataValueField="ArticlePrevision_ArticleDesing">
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SDS_Article" runat="server" ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>"
                                            SelectCommand="SGPL_GetArticle_By_OL" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                    </p>
                                </td>
                                <td>
                                    <asp:Button ID="BtnModif" class="bts" runat="server" Text="Modification" ValidationGroup="v"
                                        Visible="false" OnClick="BtnModif_Click" />
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="GDVArticle_1" runat="server" AutoGenerateColumns="False" Width="100%"
                            AllowPaging="false" BorderStyle="None" EmptyDataText="Walou">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                            <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                <th width="10%">
                                                    <asp:Label ID="lbl_Pole" runat="server" Text="Pole" Font-Size="15px" ForeColor="White"></asp:Label>
                                                </th>
                                                <th width="10%">
                                                    <asp:Label ID="lbl_direction" runat="server" Text="Direction" Font-Size="15px" ForeColor="White"></asp:Label>
                                                </th>
                                                <th width="30%">
                                                    <asp:Label ID="lbl_Etb" runat="server" Text="Etablissement" Font-Size="15px" ForeColor="White"></asp:Label>
                                                </th>
                                                <th width="20%">
                                                    <asp:Label ID="lbl_qte_P" runat="server" Text="Qte prévision" Font-Size="15px" ForeColor="White"></asp:Label>
                                                </th>
                                                <th width="10%">
                                                    <asp:Label ID="lbl_qte_L" runat="server" Text="Qte Livré" Font-Size="15px" ForeColor="White"></asp:Label>
                                                </th>
                                                <th width="20%">
                                                    <asp:Label ID="lbl_qte_D" runat="server" Text="Qte à Distribué" Font-Size="15px"
                                                        ForeColor="White"></asp:Label>
                                                </th>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr width="100%" align="center" height="35px">
                                                <td width="10%">
                                                    <asp:Label ID="lbl_pole_" runat="server" Text='<%# Eval("COD_DIR_C") %>'></asp:Label>
                                                </td>
                                                <td width="10%">
                                                    <asp:Label ID="lbl_direction_" runat="server" Text='<%# Eval("COD_DIR") %>'></asp:Label>
                                                    <asp:Label ID="LblEtb" runat="server" Text='<%# Eval("CODE_ORGANISATION") %>' Visible="false"></asp:Label>
                                                    <asp:Label ID="LblArticlePrevisionId" runat="server" Text='<%# Eval("ArticlePrevision_PrevisionId") %>'
                                                        Visible="false"></asp:Label>
                                                    <asp:Label ID="lblLivId" runat="server" Text='<%# Eval("Livraison_Id") %>' Visible="false"></asp:Label>
                                                </td>
                                                <td width="30%">
                                                    <asp:Label ID="lbl_Etb_" runat="server" Text='<%# Eval("Lbl_Etb") %>'></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:Label ID="lbl_qte_P_" runat="server" Text='<%# Eval("Qte") %>'></asp:Label>
                                                </td>
                                                <td width="10%">
                                                    <asp:Label ID="lbl_qte_L_" runat="server" Text='<%# Eval("Livraison_QteLivraison") %>'></asp:Label>
                                                </td>
                                                <td width="20%">
                                                    <asp:TextBox ID="lbl_qte_D_" runat="server" Text="0"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TXTQteLivre"
                                                        ClientIDMode="AutoID" ValidationGroup="v" Display="None" ErrorMessage="<b>Texte manquant</b>" />
                                                    <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" ClientIDMode="AutoID"
                                                        TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ClientIDMode="AutoID"
                                                        ControlToValidate="TXTQteLivre" Display="None" ErrorMessage="&lt;b&gt;seul les valeurs numériques autorisées&lt;/b&gt;"
                                                        SetFocusOnError="True" ValidationExpression="([0-9]+(?:[.,][0-9]+)?)" ValidationGroup="v"></asp:RegularExpressionValidator>
                                                    <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" ClientIDMode="AutoID"
                                                        TargetControlID="RegularExpressionValidator1" HighlightCssClass="validatorCalloutHighlight" />
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TXTQteLivre"
                                                        Display="None" ErrorMessage="La quantité livré doit être inférieur ou égale à la quantité Commandée"
                                                        SetFocusOnError="True" ValidationExpression="([0-9]+(?:[.,][0-9]+)?)" ValidationGroup="v"
                                                        Operator="LessThanEqual" Type="Integer" ValueToCompare='<%# Eval("Qte") %>'></asp:CompareValidator>
                                                    <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" ClientIDMode="AutoID"
                                                        TargetControlID="CompareValidator1" HighlightCssClass="validatorCalloutHighlight" />
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
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
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="DDL_Article" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="BtnModif" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
