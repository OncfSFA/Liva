<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="ConsultationArticleCompt.aspx.cs" Inherits="ONCF.Logistique.ValidationPolHab" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Consultation des articles comptabilisé</h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
         <div class="left-form">
      
      <table width="100%">
                    <tr>
                   
                          <td width="50%" style="height: 37px">
                             <label >
                                      &nbsp;  Ordre de Livraison<b style="color: Red"> *</b>&nbsp;:</label><asp:DropDownList ID="DropDownList1"
                                            runat="server" AutoPostBack="True" Width="162px" 
                                  DataSourceID="SDS_OL" DataTextField="OrdreLivraison_Numero" 
                                  DataValueField="OrdreLivraison_DateCreation" AppendDataBoundItems = "True" 
                                  onselectedindexchanged="DropDownList1_SelectedIndexChanged" >
                                  <asp:ListItem Text ="- Selectionné -"></asp:ListItem>
                                        </asp:DropDownList>  
                              <asp:SqlDataSource ID="SDS_Compt" runat="server" 
                                  ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                  SelectCommand="SGPL_GetArticle_Comptabilise" 
                                  SelectCommandType="StoredProcedure">
                                  <SelectParameters>
                                      <asp:ControlParameter ControlID="DropDownList1" Name="date_" 
                                          PropertyName="SelectedValue" Type="DateTime" />
                                  </SelectParameters>
                              </asp:SqlDataSource>
                          </td>
                    <td width="50%">
                                <asp:SqlDataSource ID="SDS_OL" runat="server" 
                                    ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                    SelectCommand="SELECT * FROM [V_OL_EN_COURS]"></asp:SqlDataSource>
                          </td>
                    </tr>
  
                        </table>
                        <div>
                           <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%" AllowPaging ="true" BorderStyle="None"  EmptyDataText="Aucun Article trouvé">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                          
                                                        
                                                                                                                    
                                                             <th width="33%">
                                                                <asp:Label ID="LblNumArticle" runat="server" Text="N° Article" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="33%">
                                                                <asp:Label ID="LblDesignation" runat="server" Text="Désignation" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="33%">
                                                                <asp:Label ID="Label1" runat="server" Text="Etat" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                                                         
                                                           
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="100%" align="center" height= "35px">
                                                            
                                                            <td width="33%">
                                                             <asp:Label ID="IdArticle" runat="server" Text='<%# Eval("trf_nmcl") %>'  ></asp:Label>
                                                             </td>
                                                             <td  width="33%">
                                                              <asp:Label ID="LblDesignation" runat="server" Text='<%# Eval("gst_des_suc") %>' ></asp:Label>
                                                            </td>
                                                          <td  width="33%">
                                                              <asp:Label ID="Label2" runat="server" Text='Comptabilisé' ></asp:Label>
                                                            </td>
                                                           
                                                           </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                        
                        
                        
                        </div>
                        
                 
       

      </div>
        </div>
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
