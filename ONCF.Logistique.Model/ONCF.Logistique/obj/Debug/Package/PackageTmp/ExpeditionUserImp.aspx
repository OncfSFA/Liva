<%@ Page Title="Page d'accueil" Language="C#" MasterPageFile="~/MasterONCF.master" MaintainScrollPositionOnPostback="true"
    AutoEventWireup="true"  CodeBehind="ExpeditionUserImp.aspx.cs" Inherits="ExpeditionUserImp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Réception Utilisateur  </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
      
      <table width="100%">
          <asp:HiddenField ID="HdnEncFonc" runat="server" />
                    
                    <tr><td width="50%">
                            <p>
                                <label>
                                Saisie pour&nbsp; &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                <asp:DropDownList ID="DDL_Etablissement_Fille" runat="server" 
                                    AppendDataBoundItems="True" AutoPostBack="True" 
                                    DataTextField="LIBELLE_ORGANISATION" DataValueField="CODE_ORGANISATION" 
                                 onselectedindexchanged="DDL_Etablissement_Fille_SelectedIndexChanged" 
                                    width="150px" DataSourceID="SDS_Etab_User">
                                    <asp:ListItem Text="- Sélectionner -" Value="0000" />
                                </asp:DropDownList>
                                <asp:SqlDataSource ID="SDS_Etab_User" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                    SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="Id_EtablissementMere" SessionField="idEtablissement" 
                                            Type="String" />
                                        <asp:Parameter DefaultValue="3" Name="Type" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                                     </p>
                        </td>
                        
                     
                        </tr>
                        <tr>
                        <td  align="center"  >
                         <br />  
                            <asp:SqlDataSource ID="SDS_GetPrivision" runat="server" 
               ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
               SelectCommand="SGPL_GetArticlePrevImp" 
               SelectCommandType="StoredProcedure">
               <SelectParameters>
                   <asp:ControlParameter ControlID="DDL_Etablissement_Fille" Name="IdValue" 
                       PropertyName="SelectedValue" Type="Int32" />
                  
                   
               </SelectParameters>
           </asp:SqlDataSource>
                            <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" Width="95%"  
                                        BorderStyle="None">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="1" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 90%; height: 30px" align="center">
                                                            <th width="20%">
                                                                <asp:Label ID="L1" runat="server" Text="Nomencloture" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                            
                                                              <th width="40%">
                                                                <asp:Label ID="Label3" runat="server" Text="Désignation" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                            <th width="10%" >
                                                                <asp:Label ID="Label2" runat="server" Text="Quantité" Font-Size="13px" oreColor="White"></asp:Label>
                                                            </th>
                                                            <th width="30%">
                                                                <asp:Label ID="Label1" runat="server" Text="Qte Recue" Font-Size="13px" ></asp:Label>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="90%" align="center">
                                                            <td width="20%">
                                                                                                              
                                                                <asp:Label ID="LblPrevisionartic" runat="server" Text='<%# Eval("Article_Id") %>'></asp:Label>
                                                                 <asp:Label ID="LblPrevisionid" runat="server" Visible ="false" Text='<%# Eval("ArticlePrevision_Id") %>' ></asp:Label>
                                                                
                                                            </td>
                                                             <td width="40%">
                                                             <asp:Label ID="LblLibEffet" runat="server" Text='<%# Eval("Article_Designation") %>'></asp:Label>
                                                             </td>
                                                           
                                                            
                                                             <td width="10%">
                                                             <asp:Label ID="LblQte" runat="server" Text='<%# Eval("ArticlePrevision_QtePrevision") %>'></asp:Label> 
                                                             </td>
                                                            <td width="30%">
                                                                <asp:TextBox ID="TXTQteRecu" runat="server" Text='<%# Eval("ArticlePrevision_QteRecue") %>'></asp:TextBox>
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="TXTQteRecu" ClientIDMode="AutoID"
                                                                    ValidationGroup="v" Display="None" ErrorMessage="<b>Texte manquant</b>" />
                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender1" ClientIDMode="AutoID" TargetControlID="RequiredFieldValidator1" HighlightCssClass="validatorCalloutHighlight" />
                                                                
                                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ClientIDMode="AutoID"
                                                                     ControlToValidate="TXTQteRecu" Display="None" 
                                                                     ErrorMessage="&lt;b&gt;seul les valeurs numériques autorisées&lt;/b&gt;" 
                                                                     SetFocusOnError="True" ValidationExpression="([0-9]+(?:[.,][0-9]+)?)" 
                                                                     ValidationGroup="v"></asp:RegularExpressionValidator>
                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender2" ClientIDMode="AutoID" TargetControlID="RegularExpressionValidator1"
                                                                    HighlightCssClass="validatorCalloutHighlight" />
                                                               
                                                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TXTQteRecu" Display="None" 
                                                                     ErrorMessage="La quantité reçue doit être inférieur ou égale à la quantité Commandée" 
                                                                     SetFocusOnError="True" ValidationExpression="([0-9]+(?:[.,][0-9]+)?)" 
                                                                     ValidationGroup="v"
                                                                 Operator="LessThanEqual" Type="Integer" ValueToCompare='<%# Eval("ArticlePrevision_QtePrevision") %>'></asp:CompareValidator>

                                                                <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender3" ClientIDMode="AutoID" TargetControlID="CompareValidator1"
                                                                    HighlightCssClass="validatorCalloutHighlight" />
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
                         <asp:Button ID="BtnEnregistrer" class="bts" runat="server" Text="Enregistrer" 
                                 ValidationGroup="v" onclick="BtnEnregistrer_Click"  />
                        
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
