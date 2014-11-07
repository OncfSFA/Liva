﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" 
CodeBehind="ConsultationDirecAchat.aspx.cs" Inherits="ConsultationDirecAchat" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="AjaxPannel" runat="server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="MidContent">
    <asp:UpdatePanel runat="server" id="Panel">
            <ContentTemplate>
    <div runat="server" id="divTF" class="top-right">
        <h4>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            Consultation des Ordres de livraisons </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
      

       <div class="left-form">
   <br />
   <div align="left">
    <p>                                    
                                   
                        <table>
                        <tr>
<td>  <label >
                                        Année de OL&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                    <asp:DropDownList ID="DDl_Annes" runat="server" 
        width="150px" AutoPostBack="True" AppendDataBoundItems="True" 
        DataSourceID="SDS_Annes" DataTextField="Date_OL" DataValueField="Date_OL" onselectedindexchanged="DDl_Annes_SelectedIndexChanged"
                                       
                                         >
                                <asp:ListItem Text=" - Séléction - " Value="0"></asp:ListItem>
                                </asp:DropDownList>
                          
                           <asp:SqlDataSource ID="SDS_Annes" runat="server" ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>"                                
                                        
                                        
        SelectCommand="SELECT * FROM [V_PartYYYY_OL]">
                                    </asp:SqlDataSource>
                                </td>
                        <td>  <label >
                                        Ordre de livraison N°&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                    <asp:DropDownList ID="DDLOL" runat="server" width="150px" AutoPostBack="true" AppendDataBoundItems="true"
                                        DataSourceID="SQLOL" DataTextField="OrdreLivraison_Numero"
                                DataValueField="OrdreLivraison_Id" 
                                        onselectedindexchanged="DDLOL_SelectedIndexChanged" >
                                <asp:ListItem Text=" - Séléction - " Value="0"></asp:ListItem>
                                </asp:DropDownList>
                          
                           <asp:SqlDataSource ID="SQLOL" runat="server" ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>"                                
                                        
                                        
                                SelectCommand="SELECT OrdreLivraison_Id, OrdreLivraison_Numero FROM SGPL_ORDRELIVRAISON WHERE (OrdreLivraison_ModuleId = @OrdreLivraison_ModuleId) AND (DATEPART(yyyy, OrdreLivraison_DateCreation) = @Date)">
                               <SelectParameters>
                                   <asp:SessionParameter Name="OrdreLivraison_ModuleId" SessionField="Modele" 
                                       Type="Int32" />
                                   <asp:ControlParameter ControlID="DDl_Annes" Name="Date" 
                                       PropertyName="SelectedValue" />
                               </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                        
                        </tr>
                        
                        
                        </table>          
                                </p>
   </div>
        <asp:ImageButton  runat="server" ID ="exporter" ImageUrl="~/images/btn_exporter-excel.gif" Height="30px" Width="100px" Visible="false" 
                 onclick="exporter_Click" />
<asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%"   BorderStyle="None" EmptyDataText="Aucune Données n'existe" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 30px" align="center">
                                                           
                                                             <th width="25%">
                                                                <asp:Label ID="LblArticle_" runat="server" Text="Nomencloture" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                              <th width="25%">
                                                                <asp:Label ID="LblDesignation" runat="server" Text="Désignation" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                            
                                                                <th width="25%">
                                                                <asp:Label ID="LblQte_" runat="server" Text="Quantité" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                              
                                                           
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="100%" align="center">
                                                            <td width="25%">
                                                               
                                                           <asp:Label ID="IdArticle" runat="server" Text='<%# getchamp(Eval("Reception_ArticleId").ToString(),0) %>'  ></asp:Label>
                                                            </td>
                                                                <td width="25%" >
                                                            <asp:Label ID="LblArticle" runat="server" Text='<%# getchamp(Eval("Reception_ArticleId").ToString(),2) %>' ></asp:Label>
                                                              
                                                            </td>   
                                                                                                           
                                                             <td width="25%">
                                                                <asp:Label ID="LblQte" runat="server" Text='<%# Eval("Qte") %>'></asp:Label>
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
      
    </ContentTemplate>
      <Triggers >
    <asp:PostBackTrigger ControlID ="exporter" />
    </Triggers>
    </asp:UpdatePanel>
    


</asp:Content>
