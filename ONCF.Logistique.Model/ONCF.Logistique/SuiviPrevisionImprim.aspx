<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true"
    CodeBehind="SuiviPrevisionImprim.aspx.cs" Inherits="ONCF.Logistique.SuiviPrevisionImprim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MidContent" runat="server">
    <asp:UpdatePanel ID="uptPanel" runat="server">
        <ContentTemplate>
            <div id="divTF" class="top-right">
                <h4>
                    Suisie des prévisions imprimés
                </h4>
            </div>
            <div class="contenu-right" style="display: block; background-color: White;">
                <div class="left-form">
                <table  width="100%">

                <tr id="Tr_Utilisateur" runat ="server" visible = "false"  width="100%">
                    <td width="70%">
                        <label>
                           &nbsp; Saisie pour&nbsp; &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                        <asp:DropDownList ID="DDL_Etablissement_Fille" runat="server" AppendDataBoundItems="True"
                            AutoPostBack="True" DataTextField="LIBELLE_ORGANISATION" DataValueField="CODE_ORGANISATION"
                            Width="150px" OnSelectedIndexChanged="DDL_Etablissement_Fille_SelectedIndexChanged"
                            DataSourceID="SDS_Etab_User">
                            <asp:ListItem Text="- Sélectionner -" Value="0000" />
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SDS_Etab_User" runat="server" ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>"
                            SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:SessionParameter Name="Id_EtablissementMere" SessionField="idEtablissement" 
                                    Type="String" DefaultValue="" />
                                <asp:Parameter DefaultValue="3" Name="Type" Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                   </td> 
                   <td> <asp:Label id="LBLRectifier" runat = "server"  Visible = "false" Text ="Cette Prévision a était réctifier" ForeColor = "red"></asp:Label></td>
                </tr>
                <tr id ="Tr_Liva" runat ="server" visible = "false" width="100%">
                <td>
                    <label>
                  &nbsp;  Saisie pour&nbsp; &nbsp;<b style="color: Red">*</b>&nbsp;:</label><asp:TextBox ID="Txt_IdEtb" 
                        runat="server" Width="159px"></asp:TextBox>
                      <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ControlToValidate="Txt_IdEtb" ClientIDMode="AutoID"
                                                                    ValidationGroup="v" Display="None" ErrorMessage="<b>Texte manquant</b>" />
                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender31" ClientIDMode="AutoID" TargetControlID="RequiredFieldValidator18"
                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                         <asp:RegularExpressionValidator ID="RangeValidator13" runat="server" ClientIDMode="AutoID"
                                                                     ControlToValidate="Txt_IdEtb" Display="None" 
                                                                     ErrorMessage="&lt;b&gt;seul les valeurs numériques autorisées&lt;/b&gt;" 
                                                                     SetFocusOnError="True" ValidationExpression="([0-9]+(?:[.,][0-9]+)?)" 
                                                                     ValidationGroup="v"></asp:RegularExpressionValidator>
                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender32" ClientIDMode="AutoID" TargetControlID="RangeValidator13"
                                                                    HighlightCssClass="validatorCalloutHighlight" />
                </td>
                <td>
                    <asp:Button ID="Btn_Recherche" class="bts"  runat="server" Text="Recherche" 
                        onclick="Btn_Recherche_Click" />
                </td>
                
                </tr>
                </table>
                <table width="100%">

<tr >
 <td >
     <label>
                  &nbsp;  Statut Prévision&nbsp; &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
     <asp:Label ID="LblStatut" runat="server" Font-Bold="True" Font-Size="14pt" 
         ForeColor="#FF6600" ></asp:Label>
 </td> 
                    
        </tr>
        <tr>
        <td width="100%" align="center">
        <asp:GridView ID="GDVArticleConsultation" runat="server" AutoGenerateColumns="false"
             BorderStyle="None"  width="100%" >
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%" border="1" cellspacing="0" cellpadding="0">
                                        <tr style="background-color: #fbb17f; width: 90%; height: 30px" align="center">
                                            <th width="30%">
                                                <asp:Label ID="L1" runat="server" Text="Nomencloture" Font-Size="13px"></asp:Label>
                                            </th>
                                            <th width="30%">
                                                <asp:Label ID="Label1" runat="server" Text="Désignation" Font-Size="13px"></asp:Label>
                                            </th>
                                            <th width="40%">
                                                <asp:Label ID="L9" runat="server" Text="Statut" Font-Size="13px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr width="90%" align="center">
                                            <td width="30%">
                                                <asp:Label ID="LblIdArticle" runat="server" Text='<%# Eval("ArticlePrevision_ArticleId") %>'></asp:Label>
                                            </td>
                                            <td width="30%">
                                                <asp:Label ID="LblLibEffet" runat="server" Text='<%# Eval("ArticlePrevision_ArticleDesing") %>'></asp:Label>
                                            </td>
                                            <td width="40%" valign="bottom">
                                                <asp:Label ID="LblQteArticle" runat="server" Text='<%# ReturnStatut(Eval("ArticlePrevision_OrdreLivraisonId").ToString()) %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
        
        </td>
        
        </tr>
        </table>
                
                </div>
                <br />
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
