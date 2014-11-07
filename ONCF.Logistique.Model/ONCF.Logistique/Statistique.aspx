<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true"
 CodeBehind="Statistique.aspx.cs" Inherits="ActiveDesactive" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Statistique des prévisions</h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">
    <div id="TXT_Date_Div">
    <table >
    <tr>
    <td>
        <label >
                                        <b style="color: Red"> Année&nbsp; *</b>&nbsp;:</label>

    </td>
    <td>
     <asp:TextBox ID="Txt_Annee" runat="server" ></asp:TextBox>
                                   <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="Txt_Annee"
                                        ValidationGroup="v" Display="None" ErrorMessage="<b>Texte manquant</b><br />Veuillez entrer l'année ." />
                                 <cc1:ValidatorCalloutExtender runat="Server" ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator5"
                                        HighlightCssClass="validatorCalloutHighlight" />                               
                                  
                                          
    </td>
    
    </tr>
    
    </table>
     
    </div>
      <table width="100%">
       
                    <tr>
                    <td style="height: 30px; width: 15px;">
                      
                    </td>
                          <td width="40%" style="height: 30px">
                            
                                    
                                   
                                    <label >
                                        <b style="color: Red"> Etablissement&nbsp; *</b>&nbsp;:</label>
                                                                        
                                    <asp:RadioButton ID="RD_Etab" runat="server" 
                                        oncheckedchanged="RD_Etab_CheckedChanged" AutoPostBack ="true" />
                                                                        
                            
                            </td>
                             <td style="height: 30px; width: 5px;">
                    
                    </td>
                         <td width="40%" style="height: 30px">
                           
                                    
                                   
                                    <label >
                                        <b style="color: Red"> Article *</b>&nbsp;:</label>
                                                                        
                                    <asp:RadioButton ID="RD_Article" runat="server" 
                                        oncheckedchanged="RD_Article_CheckedChanged" AutoPostBack ="true" />
                           
                            </td>
                     </tr>     
                       <tr>
                          <td style="height: 30px; width: 15px;">
                       
                                    
                                   
                                    &nbsp;<asp:RadioButton ID="RD_Pole" runat="server" 
                                        oncheckedchanged="RD_Pole_CheckedChanged" AutoPostBack ="true"/>
                                                                        
                                </td>
                               <td width="40%" style="height: 30px">
                                 
                                        &nbsp;<label>Pole<b style="color: Red"> </b>&nbsp;:</label><asp:DropDownList ID="DDLPole" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" DataSourceID="SDS_POLE" DataTextField="libelle_direction" 
                                            DataValueField="code_dc" 
                                            width="150px">
                                            <asp:ListItem Text="- Sélectionner -" Value="0" />
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SDS_POLE" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                            SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="Id_EtablissementMere" Type="Int32" />
                                                <asp:Parameter DefaultValue="2" Name="Type" Type="Int32" />
                                            </SelectParameters>
                                        </asp:SqlDataSource>
                                
                                </td>
                                 <td style="height: 30px; width: 5px;">
                       
                                    
                                   
                                    &nbsp;<asp:RadioButton ID="RD_Art" runat="server" 
                                         oncheckedchanged="RD_Art_CheckedChanged" AutoPostBack ="true"/>
                                                                        
                                </td>
                               <td width="40%" style="height: 30px">
                                  
                                      <label>
                                      Article&nbsp;:</label>
                                      <asp:DropDownList ID="DDL_Article" runat="server" AppendDataBoundItems="True" 
                                          AutoPostBack="True" DataSourceID="SqlDataSource1" 
                                          DataTextField="Article_Id" 
                                          DataValueField="Article_Id" 
                                         width="150px">
                                          <asp:ListItem Text="- Sélectionner -" Value="0" />
                                      </asp:DropDownList>
                                      <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                          ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                          SelectCommand="SGPL_GetArticleValidMarcher" 
                                          SelectCommandType="StoredProcedure">
                                      </asp:SqlDataSource>
                             
                              </td>
                              
                          </tr>
                          <tr>
                             <td style="height: 30px; width: 15px;">
                          
                                    
                                   
                                    &nbsp;<asp:RadioButton ID="RD_Dir" runat="server" 
                                        oncheckedchanged="RD_Dir_CheckedChanged" AutoPostBack ="true"/>
                                                                        
                             
                            </td>
                               <td width="40%" style="height: 30px">
                               
                                        <label ID="Label7" runat="server">
                                        Direction&nbsp;&nbsp;:</label>
                                        <asp:DropDownList ID="DDLDirection" runat="server" AppendDataBoundItems="True" 
                                            AutoPostBack="True" DataSourceID="SDS_DIRECTION" 
                                            DataTextField="LIBELLE_ORGANISATION" DataValueField="dir_code" 
                                             width="150px">
                                            <asp:ListItem Text="- Sélectionner -" Value="0" />
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SDS_DIRECTION" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                            SelectCommand="SELECT * FROM [V_Direction]">
                                        </asp:SqlDataSource>
                               
                                </td>
                             <td style="height: 30px; width: 5px;">
                          
                                    
                                   
                                    &nbsp;<asp:RadioButton ID="RD_Oncf" runat="server" 
                                        oncheckedchanged="RD_Oncf_CheckedChanged" AutoPostBack ="true" />
                                                                        
                             
                            </td>
                               <td width="40%" style="height: 30px">
                          
                                      <label>
                                      Tout le réseau ONCF</label>
                                                                  
                              </td> 
                          </tr>
                          <tr>
                            <td style="height: 30px; width: 15px;">
                           
                                    
                                   
                                    &nbsp;<asp:RadioButton ID="RD_Etb" runat="server" 
                                        oncheckedchanged="RD_Etb_CheckedChanged" AutoPostBack ="true" />
                                                                        
                              
                            </td>
                              <td width="40%" style="height: 30px">
                               
                                      <label>
                                      Etablissement &nbsp;&nbsp;:</label>
                                      <asp:TextBox ID="Txt_EtbCode" runat="server" Width="150px"></asp:TextBox>
                            
                              </td>
                              <td style="height: 30px; width: 5px;">
                           
                                    
                                   
                                    &nbsp;</td>
                            <td width="40%" style="height: 30px">
                                  <asp:Button ID="Btn_LanceRv" runat="server" Text="Lancé" ValidationGroup="v"  class="bts" onclick="Btn_LanceRv_Click" 
                                     />
                                  </td> 
                          </tr>
                              

                        </tr>
                        </table>
                     
      
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