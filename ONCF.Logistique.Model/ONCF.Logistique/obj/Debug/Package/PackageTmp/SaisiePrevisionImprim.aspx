<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true"
    CodeBehind="SaisiePrevisionImprim.aspx.cs" Inherits="ONCF.Logistique.SaisiePrevisionImprim" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MidContent" runat="server">
    <asp:UpdatePanel ID="uptPanel" runat="server">
        <ContentTemplate>
            <div id="divTF" class="top-right">
                <h4>
                    Saisie des prévisions imprimés
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

<tr width="100%">
 <td width="100%">
                    <asp:GridView ID="GDVArticleConsultatoin" runat="server" AutoGenerateColumns="false"
                        Visible="false" DataKeyNames="Article_Id" BorderStyle="None" AllowPaging="false"
                        ShowFooter="true" ShowHeaderWhenEmpty="true" width="100%" EmptyDataText="Pas d'enregistrement pour cette etablissement">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <table width="100%" border="1" cellspacing="0" cellpadding="0">
                                        <tr style="background-color: #fbb17f; width: 90%; height: 30px" align="center">
                                            <th width="40%">
                                                <asp:Label ID="L1" runat="server" Text="Nomencloture" Font-Size="13px"></asp:Label>
                                            </th>
                                            <th width="30%">
                                                <asp:Label ID="Label1" runat="server" Text="Désignation" Font-Size="13px"></asp:Label>
                                            </th>
                                            <th width="20%">
                                                <asp:Label ID="L9" runat="server" Text="Prévision" Font-Size="13px"></asp:Label>
                                            </th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                        <tr width="90%" align="center">
                                            <td width="40%">
                                                <asp:Label ID="LblIdArticle" runat="server" Text='<%# Eval("Article_Id") %>'></asp:Label>
                                            </td>
                                            <td width="30%">
                                                <asp:Label ID="LblLibEffet" runat="server" Text='<%# Eval("libelle_effet") %>' ></asp:Label>
                                            </td>
                                            <td width="20%" valign="bottom">
                                                <asp:Label ID="LblQteArticle" runat="server" Text='<%# Eval("Qte_Article") %>'></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="false" DataKeyNames="Article_Id"
                        BorderStyle="None" AllowPaging="false" ShowFooter="true" ShowHeaderWhenEmpty="true"
                        Visible="false" OnRowDataBound="GDVArticle_DataBound" width="100%">
                        <EmptyDataTemplate>
                            <table width="100%" border="1" cellpadding="0" cellspacing="0">
                                <tr width="90%" align="center">
                                    <td width="40%">
                                        <asp:DropDownList ID="DDL_Article" runat="server" AppendDataBoundItems="True" AutoPostBack="True"
                                            Width="150px" DataSourceID="SDSGetArticle" DataTextField="Article_Id" DataValueField="libelle_effet"
                                            OnSelectedIndexChanged="DDL_Article_SelectedIndexChanged">
                                            <asp:ListItem Text="- Sélectionner -" Value="0000" />
                                        </asp:DropDownList>
                                          <cc1:ListSearchExtender ID="listeSerchRapide" runat="server" PromptText ="Recherche rapide ..." TargetControlID ="DDL_Article">
                                        </cc1:ListSearchExtender>
                                        <asp:SqlDataSource ID="SDSGetArticle" runat="server" ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>"
                                            SelectCommand="SGPL_GetArticleValidMarcher" SelectCommandType="StoredProcedure">
                                        </asp:SqlDataSource>
                                    </td>
                                    <td width="30%">
                                        <asp:Label ID="LblLibEffet" runat="server"></asp:Label>
                                    </td>
                                    <td width="20%" valign="bottom">
                                        <asp:TextBox ID="Txtprevision" runat="server" class="bts-form" Width="40%" Text=""></asp:TextBox>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator18" ControlToValidate="Txtprevision" ClientIDMode="AutoID"
                                                                    ValidationGroup="v" Display="None" ErrorMessage="<b>Texte manquant</b>" />
                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender31" ClientIDMode="AutoID" TargetControlID="RequiredFieldValidator18"
                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                         <asp:RegularExpressionValidator ID="RangeValidator13" runat="server" ClientIDMode="AutoID"
                                                                     ControlToValidate="Txtprevision" Display="None" 
                                                                     ErrorMessage="&lt;b&gt;seul les valeurs numériques autorisées&lt;/b&gt;" 
                                                                     SetFocusOnError="True" ValidationExpression="([0-9]+(?:[.,][0-9]+)?)" 
                                                                     ValidationGroup="v"></asp:RegularExpressionValidator>
                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender32" ClientIDMode="AutoID" TargetControlID="RangeValidator13"
                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                   <asp:ImageButton ID="imgBtnAddNew" runat="server" Width="50px" Height ="20px" ToolTip="Ajouter Article" ImageUrl="images/add-icon.png"  OnClick="imgBtnAddNew_Click"  ValidationGroup="v" />
                     
                                    </td>
                                </tr>
                            </table>
                               </EmptyDataTemplate>
                    </asp:GridView>
                    </td> 
                    
        </tr>
        </table>
                <table width="100%">
                 <tr width="100%">
                 <td width="20%">
                   
                    </td>
                    <td width="60%">
                    
                <asp:Button ID="BtnEnregistrer" class="bts" runat="server" Text="Enregistrer" visible = "false"
                    onclick="BtnEnregistrer_Click" /><asp:Button ID="BtnModification" class="bts" runat="server" Text="Modifier" 
                    Visible="false" onclick="BtnModification_Click"  />
                    </td>
                    <td width="20%">
                 <asp:Button ID="BtnValider" class="bts" runat="server" Text="Valider" visible = "false"
                    onclick="BtnValider_Click"/>
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
