<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="ConsultationModifPrevision.aspx.cs" Inherits="ONCF.Logistique.ConsultationModifPrevision" %>
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
            Consultation des prévisions </h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
      

       <div class="left-form">
   <table width="100%">
       
                    
                    <tr>
                          <td width="50%" style="height: 37px">
                                <p>
                                    
                                   
                                    <label >
                                        Consultation pour Pole<b style="color: Red"> *</b>&nbsp;:</label>
                                         <asp:DropDownList ID="DDLPole" runat="server" width="150px" 
                                        AutoPostBack="True" AppendDataBoundItems="True" DataSourceID="SDS_POLE" 
                                        DataTextField="libelle_direction" DataValueField="code_dc" onselectedindexchanged="DDLPole_SelectedIndexChanged"
                                    >
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
                               
                                </p>
                            </td>

                            <td width="50%" style="height: 37px">
                                <p>
                                    
                                   
                                    <label runat="server" id="Label7">
                                        Direction&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                         <asp:DropDownList ID="DDLDirection" runat="server" width="150px" AutoPostBack="True"
                                       AppendDataBoundItems="True" DataSourceID="SDS_DIRECTION" 
                                        DataTextField="libelle_direction" DataValueField="cod_org_d" onselectedindexchanged="DDLDirection_SelectedIndexChanged" 
                                        >
                                <asp:ListItem Text="- Sélectionner -" Value="0" />
                            </asp:DropDownList>
                           
       
                                    <asp:SqlDataSource ID="SDS_DIRECTION" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DDLPole" Name="Id_EtablissementMere" 
                                                PropertyName="SelectedValue" Type="String" />
                                            <asp:Parameter DefaultValue="0" Name="Type" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                           
       
                                   </p>
                            </td>
                              

                        </tr>
                        <tr>
                            <td width="50%">
                                <p>
                                    
                                   
                                    <label >
                                       Etablissement Mere &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                    <asp:DropDownList ID="DDLEtablissementMere" runat="server" width="150px" AutoPostBack="True"
                                       AppendDataBoundItems="True" DataSourceID="SDS_ETABLISSEMENT_MERE" 
                                        DataTextField="LIBELLE_ORGANISATION" DataValueField="CODE_ORGANISATION" 
                                        onselectedindexchanged="DDLEtablissementMere_SelectedIndexChanged" 
                                         >
                                <asp:ListItem Text="- Sélectionner -" Value="0" />
                            </asp:DropDownList>
                       
                                    <asp:SqlDataSource ID="SDS_ETABLISSEMENT_MERE" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DDLDirection" Name="Id_EtablissementMere" 
                                                PropertyName="SelectedValue" Type="String" />
                                            <asp:Parameter DefaultValue="1" Name="Type" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" 
                                        ControlToValidate="DDLEtablissementmere" Display="None" 
                                        ErrorMessage="&lt;b&gt;Texte manquant&lt;/b&gt;&lt;br /&gt;Veuillez Choisir une étabelissement" 
                                        Operator="NotEqual" ValidationGroup="v" ValueToCompare="0"></asp:CompareValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" 
                                        Enabled="True" PopupPosition="right" TargetControlID="CompareValidator1">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
                                        Enabled="True" PopupPosition="right" TargetControlID="CompareValidator1">
                                    </cc1:ValidatorCalloutExtender>
                       
                                </p>
                            </td>
                         
                        
                       
                            <td width="50%">
                                <p>
                                    <label>
                                    Etablissement Fille &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                    <asp:DropDownList ID="DDL_Etablissement_Fille" runat="server" 
                                        AppendDataBoundItems="True" AutoPostBack="True" 
                                        DataTextField="LIBELLE_ORGANISATION" DataValueField="CODE_ORGANISATION" 
                                        onselectedindexchanged="DDL_Etablissement_Fille_SelectedIndexChanged" 
                                        width="150px">
                                        <asp:ListItem Text="- Sélectionner -" Value="0000" />
                                    </asp:DropDownList>
                                    <asp:SqlDataSource ID="SDS_ETABLISSEMENT_FILLE" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DDLEtablissementMere" 
                                                Name="Id_EtablissementMere" PropertyName="SelectedValue" Type="String" />
                                            <asp:Parameter DefaultValue="1" Name="Type" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                    <asp:CompareValidator ID="CompareValidator3" runat="server" 
                                        ControlToValidate="DDLEtablissementMere" Display="None" 
                                        ErrorMessage="&lt;b&gt;Texte manquant&lt;/b&gt;&lt;br /&gt;Veuillez Choisir une étabelissement" 
                                        Operator="NotEqual" ValidationGroup="v" ValueToCompare="0"></asp:CompareValidator>
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
                                 <tr runat ="server" id="R_Article" visible = "false">
                            <td width="50%">
                                <p>
                                    
                                   
                                    <label >
                                       Recherche Par Article &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                    <asp:DropDownList ID="DDL_Article" runat="server" width="150px" AutoPostBack="True"
                                       AppendDataBoundItems="True" DataSourceID="SqlDataSource1" 
                                        DataTextField="ArticlePrevision_ArticleId" 
                                        DataValueField="ArticlePrevision_ArticleId" onselectedindexchanged="DDL_Article_SelectedIndexChanged" 
                                     
                                         >
                                <asp:ListItem Text="- Sélectionner -" Value="0" />
                            </asp:DropDownList>
                       
                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetArticle_By_OL" SelectCommandType="StoredProcedure">
                                    </asp:SqlDataSource>
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" 
                                        ControlToValidate="DDLEtablissementmere" Display="None" 
                                        ErrorMessage="&lt;b&gt;Texte manquant&lt;/b&gt;&lt;br /&gt;Veuillez Choisir une étabelissement" 
                                        Operator="NotEqual" ValidationGroup="v" ValueToCompare="0"></asp:CompareValidator>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" 
                                        Enabled="True" PopupPosition="right" TargetControlID="CompareValidator1">
                                    </cc1:ValidatorCalloutExtender>
                                    <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" 
                                        Enabled="True" PopupPosition="right" TargetControlID="CompareValidator1">
                                    </cc1:ValidatorCalloutExtender>
                       
                                </p>
                            </td>
                         
                        
                       
                            <td width="50%">
                                
                            </td>
                         
                         
                       
                        </tr>
                        </table>
                        <asp:ImageButton  runat="server" ID ="exporter" ImageUrl="~/images/btn_exporter-excel.gif" Height="30px" Width="100px" Visible="false" 
                 onclick="exporter_Click" />
<asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%"   AllowPaging ="true"
                                        BorderStyle="None"  >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                           
                                                             <th width="33%">
                                                                <asp:Label ID="LblNomEnClature1" runat="server" Text="Nomencloture" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="33%">
                                                                <asp:Label ID="LblDesignation1" runat="server" Text="Désignation" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                                <th width="33%">
                                                                <asp:Label ID="LblQte1" runat="server" Text="Quantité" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                              
                                                           
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                                        <tr width="100%" align="center">
                                                            <td width="33%">
                                                               <asp:Label ID="LblNomEnClature" runat="server" Text='<%# Eval("Article_Id") %>' ></asp:Label>
                                                            </td>
                                                            <td width="33%">                   
                                                                <asp:Label ID="LblDesignation" runat="server" Text='<%# Eval("Article_Designation") %>' ></asp:Label>
                                                             </td>  
                                                             <td width="33%">
                                                                <asp:Label ID="LblQte" runat="server" Text='<%# Eval("ArticlePrevision_QtePrevision") %>'></asp:Label>
                                                           </td>
                                                           </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
<asp:SqlDataSource ID="SDS_GetPrivision" runat="server" 
               ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
               SelectCommand="SGPL_GetListArticleBy_Date_Etab_Dir_Pol" 
               SelectCommandType="StoredProcedure">
               <SelectParameters>
                   <asp:ControlParameter ControlID="DDL_Etablissement_Fille" Name="IdValue" 
                       PropertyName="SelectedValue" Type="Int32" />
                   <asp:Parameter DefaultValue="1" Name="Value" Type="Int32" />
                   <asp:SessionParameter Name="IdModule" SessionField="Modele" 
                       Type="Int32" DefaultValue="" />
               </SelectParameters>
           </asp:SqlDataSource>

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

