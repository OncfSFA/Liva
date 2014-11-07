<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="CreationLotImp.aspx.cs" Inherits="CreationLotImp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Génération des Ordres de livraisons </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
         <div class="left-form">
      
      <table width="100%">
       
                    <tr>
                    <td width="50%" style="height: 37px">
                                <p>
                                    
                                   
                                    <label >
                                        N° Dordre de Livraison<b style="color: Red"> *</b>&nbsp;:</label>
                                         <asp:TextBox ID="TxtLot" runat="server" class="bts-form" 
                                        Width="150px"  />
                                                      
       
                                   </p>
                            </td>
                          <td width="50%" style="height: 37px">
                                <p>
                                    
                                   
                                    <label runat="server" id="Label6">
                                        Marché&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                    <asp:TextBox ID="Txt_Marcher" runat="server" Width="107px"></asp:TextBox>
                                </p>
                            </td>
                    
                    </tr>
                    <tr>
                            <td width="50%" >
                                <p>
                                                                       
                                    <label >
                                        Date &nbsp;<b style="color: Red">*</b>&nbsp;:</label>

                                 <asp:TextBox runat="server" ID="TXTDate" class="bts-form" Text="" Width="150px" 
                                        ReadOnly="True"></asp:TextBox>
                                   </p>
                            </td>
                         

                       
                             <td width="50%">
                                <p>
                                    
                                   
                                    &nbsp;</p>
                               
                              
                            </td>
                          </tr>
                        </table>
                        <div id="Div1" runat ="server"  Width="100%" overflow="auto">
                        <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%" AllowPaging ="false" BorderStyle="None"  EmptyDataText="Pas d'enregistrement, la liste est Vide">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                          
                                                        
                                                            <th width="25%">
                                                                <asp:Label ID="Label1" runat="server" Text="N° Article" Font-Size="15px" ForeColor="White" Visible = "false"></asp:Label>
                                                            </th>
                                                          
                                                             <th width="25%">
                                                                <asp:Label ID="LblNumArticle" runat="server" Text="N° Article" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="25%">
                                                                <asp:Label ID="LblDesignation" runat="server" Text="Désignation" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                            
                                                                <th width="25%">
                                                                <asp:Label ID="LblQte1" runat="server" Text="Quantité" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                              
                                                           
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="100%" align="center" height= "35px">
                                                             <td width="25%">
                                                                  <asp:CheckBox  runat="server" ID="ValidArticle" />
                                                      
                                                            </td>
                                                            <%--   <td width="25%">  
                                                                     
                                                                         
                                                              <asp:Label ID="LblNArticle" runat="server" Text='<%# Eval("Article_Nomenclature") %>' ></asp:Label>
                                                             </td> --%>
                                                            <td width="25%">
                                                             <asp:Label ID="IdArticle" runat="server" Text='<%# Eval("ArticlePrevision_ArticleId") %>'  ></asp:Label>
                                                             </td>
                                                             <td  width="25%">
                                                              <asp:Label ID="LblDesignation" runat="server" Text='<%# Eval("ArticlePrevision_ArticleDesing") %>' ></asp:Label>
                                                            </td>
                                                         
                                                             <td width="25%">
                                                                <asp:Label ID="LblQte" runat="server" Text='<%# Eval("Qte") %>'></asp:Label>
                                                           </td>
                                                           </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView></div>
<table width="100%">
   <tr>
                        <td colspan="2" align="center" width="100%">
                         <p>
                          <asp:Button ID="BtnAnnuler" class="bts" runat="server" Text="Annuler" onclick="BtnAnnuler_Click" 
                                  />
                         <asp:Button ID="BtnEnregistrer" class="bts" runat="server" Text="Valider" 
                                 ValidationGroup="v" onclick="BtnEnregistrer_Click"  />
                        
                </p>
                        </td>
                        </tr>
                    </table>

                    
                                   
       

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