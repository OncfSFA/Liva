<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="GenererEscalaHab.aspx.cs" Inherits="ONCF.Logistique.GenererEscalaHab" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Génération fichier ESCALA  </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
         <div class="left-form">
      
      <table width="100%">
       
                    <tr>
                        
                          <td width="50%" style="height: 37px">
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
                   
                           <td width="50%" style="height: 37px">
                                <p>
                                    <asp:Button ID="BTN_Enregistrer" runat="server" Text="Enregistrer" class="bts"  
                                        OnClientClick="return confirm('Êtes-vous sûr de Comptabiliser cet article ! ');" 
                                        onclick="BTN_Enregistrer_Click" />       </p>
                            </td>
                           
                           </tr>
                        </table>
                         
<asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%"   AllowPaging ="True"
                                        BorderStyle="None"  EmptyDataText="Pas de données pour cette article"  >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                           
                                                             <th width="16%">
                                                                <asp:Label ID="LblArticle_" runat="server" Text="Article" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                               <th width="20%">
                                                                <asp:Label ID="LblNomenclature" runat="server" Text="Désignation" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             
                                                              <th width="16%">
                                                                <asp:Label ID="Lbl_Mag_" runat="server" Text="Magasin" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                                <th width="16%">
                                                                <asp:Label ID="LblQte_" runat="server" Text="Quantité Commandée" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                               <th width="16%">
                                                                <asp:Label ID="LblQteR_" runat="server" Text="Quantité Recue" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="16%">
                                                                <asp:Label ID="Lbl_Etat_" runat="server" Text="Etat" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                           
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="100%" align="center">
                                                            <td width="16%">
                                                             <asp:Label ID="Lbl_Ordre" runat="server" Visible="false" Text='<%# Eval("OrdreLivraison_Id") %>' ></asp:Label>
                                                              
                                                            <asp:Label ID="ArticleId" runat="server" Text='<%# getchamp(Eval("Reception_ArticleId").ToString(),0) %>'  ></asp:Label>
                                                            </td>
                                                           <td width="20%">
                                                               <asp:Label ID="Nmcloture" runat="server" Text='<%# getchamp(Eval("Reception_ArticleId").ToString(),1) %>' ></asp:Label>
                                                            </td>
                                                             <td width="16%">
                                                                <asp:Label ID="Lbl_Mag" runat="server" Text='<%# Eval("Magasin_Libelle") %>'></asp:Label>
                                                           </td>
                                                             <td width="16%">
                                                                <asp:Label ID="LblQte" runat="server" Text='<%# Eval("Qte_P_Mag") %>'></asp:Label>
                                                           </td>
                                                           <td width="16%">
                                                                <asp:Label ID="LblRecu" runat="server" Text='<%# Eval("Qte_R") %>'></asp:Label>
                                                           </td>
                                                            <td width="16%">
                                                                <asp:Label ID="LblEtat" runat="server" Text='<%# Eval("Etat") %>'></asp:Label>
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
