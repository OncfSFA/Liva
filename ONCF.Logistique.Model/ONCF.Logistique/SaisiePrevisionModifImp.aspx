<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="SaisiePrevisionModifImp.aspx.cs" Inherits="ONCF.Logistique.SaisiePrevisionModifImp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Modification des prévisions imprimés </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
      
      <table width="100%">
       
                    
                    
                        
                        <tr>
                        <td colspan="2" align="center"  >
                          
                             
                            <br />                  
                           
                                 
                            <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" Width="95%"  DataKeyNames="ArticlePrevision_Id"
                                        BorderStyle="None"   onrowcommand="gdvAlerteTolTechn_RowCommand" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="1" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 90%; height: 35px" align="center">
                                                           <th width="24%">
                                                                <asp:Label ID="Label2" runat="server" Text="Nomenclature" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                          
                                                            <th width="25%">
                                                                <asp:Label ID="L1" runat="server" Text="Désignation" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                           
                                                           
                                                            <th width="25%" >
                                                                <asp:Label ID="L9" runat="server" Text="Prévision" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                              <th width="25%" >
                                                                <asp:Label ID="Label1" runat="server" Text="Supprimer" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                         
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" >
                                                        <tr width="99%" align="center">
                                                           <td width="24%"> <asp:Label ID="Label3" runat="server" Text='<%# Eval("Article_Id") %>' ></asp:Label>  </td>
                                                            <td width="25%">
                                                                                                              
                                                                <asp:Label ID="TblIdArticle" runat="server" Text='<%# Eval("ArticlePrevision_Id") %>' Visible = "false" ></asp:Label>
                                                           
                                                                <asp:Label ID="LblNomVT" runat="server" Text='<%# Eval("Article_Designation") %>'></asp:Label>
                                                               <%-- <asp:Label ID="LblOL" runat="server" Visible="false"  Text='<%# Eval("ArticlePrevision_OrdreLivraisonId") %>'></asp:Label>--%>
                                                               </td>
                                                                                                                     
                                                            <td width="25%" >
                                                               <asp:TextBox ID="Txtprevision" runat="server" class="bts-form" Width="40%" Text='<%# getchamp(Eval("ArticlePrevision_QtePrevision").ToString(),Eval("ArticlePrevision_Taille").ToString()) %>'></asp:TextBox>
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
                                                                </td>
                                                            <td>
                                                          <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" 
                                                 CommandName="supprimer" CommandArgument="<%#((GridViewRow) Container).RowIndex %>"
                                                  Text="supprimer" OnClientClick="return confirm('Êtes-vous sûr de vouloir supprimer cet utilisateur ?');"></asp:LinkButton>
                                     
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
                        <td colspan="2" align="center" width="100%">
                         <p>
                         <asp:Button ID="BtnAnnuler" class="bts" runat="server" Text="Retour" 
                                 onclick="BtnAnnuler_Click" />
                         <asp:Button ID="BtnEnregistrer" class="bts" runat="server" Text="Enregistrer" 
                                 ValidationGroup="v" onclick="BtnEnregistrer_Click" />
                        
                </p>
                        </td>
                        </tr>
                    </table>
       

      </div>
        
        <br />
        <br />
        
      
    </div>
      <!-- Un bloc pour affiche le message de confirmation d'ajout-->
                       <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="Panel1"
                        BackgroundCssClass="modalBackground" TargetControlID="Label92"
                        RepositionMode="RepositionOnWindowResize">
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
                                        <asp:Label ID="msg" runat ="server" Text=""></asp:Label>    </p>
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

