<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="GenererEscalaImp.aspx.cs" Inherits="ONCF.Logistique.GenererEscalaImp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MidContent">
     <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            Comptabilisation des Ordres de Livraison  </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
         <div class="left-form">
      <table width = "100%">
      
      <tr    >
                        
                          <td width="50%" style="height: 37px">
                                <p>
                                        <label runat="server" id="Label1">
                                            Ordre de Livraison En cours :&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                        <asp:DropDownList ID="DDL_Article" runat="server"  OnSelectedIndexChanged="DDL_Article_SelectedIndexChanged"
                                            AutoPostBack="True" DataSourceID="SDS_Article" DataTextField="OrdreLivraison_Id"
                                            DataValueField="OrdreLivraison_Numero" AppendDataBoundItems="true" 
                                            Height="23px" Width="155px" >
                                            <asp:ListItem Value ="0" Text ="Selectionner"></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:SqlDataSource ID="SDS_Article" runat="server" ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>"
                                            SelectCommand="SELECT * FROM [V_OL_EN_COURS]"></asp:SqlDataSource>
                                    </p>
                            </td>
                   
                           <td width="50%" style="height: 37px">
                                <p>
                                    <asp:Button ID="BTN_Enregistrer" runat="server" Text="Comptabilisé" class="bts"  
                                        OnClientClick="return confirm('Êtes-vous sûr de Comptabiliser cet article ! ');" 
                                        onclick="BTN_Enregistrer_Click" />       </p>
                            </td>
                           
                           </tr>
                  
            </tr>
      </table>
      <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%"   AllowPaging ="false"
                                        BorderStyle="None"  EmptyDataText="Pas de données a comptabilisé"  >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                             <th width="8%">
                                                           
                                                            </th>
                                                             <th width="16%">
                                                                <asp:Label ID="LblArticle_" runat="server" Text="N° ARTICLE" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                               <th width="20%">
                                                                <asp:Label ID="LblNomenclature" runat="server" Text="DESIGNATION" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             
                                                              <th width="14%">
                                                                <asp:Label ID="Lbl_Mag_" runat="server" Text="QTE PREVISION" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                                <th width="14%">
                                                                <asp:Label ID="LblQte_" runat="server" Text="MG/PMM" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                               <th width="14%">
                                                                <asp:Label ID="LblQteR_" runat="server" Text="MG/PIC" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="14%">
                                                                <asp:Label ID="Lbl_Etat_" runat="server" Text="MG/DRHAJ" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                           
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="100%" align="center">
                                                            <td width="8%">
                                                                <asp:CheckBox ID="Check_Article" runat="server" />
                                                            </td>
                                                            <td width="16%">
                                                            
                                                            <asp:Label ID="ArticleId" runat="server" Text='<%# Eval("Article_Id") %>'  ></asp:Label>
                                                            </td>
                                                           <td width="20%">
                                                               <asp:Label ID="Nmcloture" runat="server" Text='<%# Eval("Article_Designation") %>' ></asp:Label>
                                                            </td>
                                                             <td width="14%">
                                                                <asp:Label ID="Lbl_Mag" runat="server" Text='<%# Eval("QtePrevision") %>'></asp:Label>
                                                           </td>
                                                             <td width="14%">
                                                                <asp:Label ID="LblQte" runat="server" Text='<%# Eval("PMM") %>'></asp:Label>
                                                           </td>
                                                           <td width="14%">
                                                                <asp:Label ID="LblRecu" runat="server" Text='<%# Eval("PIC") %>'></asp:Label>
                                                           </td>
                                                            <td width="14%">
                                                                <asp:Label ID="LblEtat" runat="server" Text='<%# Eval("DRHAJ") %>'></asp:Label>
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
