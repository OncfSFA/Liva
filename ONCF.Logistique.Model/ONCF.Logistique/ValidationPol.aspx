<%@ Page Title="" Language="C#" MasterPageFile="~/MasterONCF.master" AutoEventWireup="true" CodeBehind="ValidationPol.aspx.cs" Inherits="ONCF.Logistique.ValidationPol" %>
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
            Validation des prévisions</h4>
    </div>
    <div class="contenu-right" style="display: block; background-color:White;">

       <div class="left-form">
      

       <div class="left-form">
<table width="100%">
        <%--<tr>
                   
                          <td width="50%" style="height: 37px">
                                <p>
                                    <asp:RadioButton ID="RdbVal" runat="server" GroupName="rdb" Text="Valider" 
                                        Checked ="true" AutoPostBack="true" TextAlign="Left" 
                                        oncheckedchanged="RdbVal_CheckedChanged"/>
                                </p> 
                                <p></p>
                                </td>
                                <td width="50%" style="height: 37px">
                                <p>
                                <asp:RadioButton ID="RdbModif" runat="server" GroupName="rdb" Text="Modifier" 
                                        AutoPostBack="true" TextAlign="Left" 
                                        oncheckedchanged="RdbModif_CheckedChanged"/>
                                </p> 
                                     <p></p>
                                </td>
                                </tr>--%>
                    <tr>
                   
                          <td width="50%">
                             <p>
                                    
                                   
                                    <label runat="server" id="Label6">
                                        Direction&nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                     <asp:DropDownList ID="DDLDirection" runat="server" width="150px" 
                                        AutoPostBack="True" AppendDataBoundItems="True" DataSourceID="SDS_DIRECTION" 
                                        DataTextField="libelle_direction" DataValueField="cod_org_d" onselectedindexchanged="DDLDirection_SelectedIndexChanged" 
                                    >
                                <asp:ListItem Text="- Sélectionner -" Value="0" />
                            </asp:DropDownList>
                               
                                    <asp:SqlDataSource ID="SDS_DIRECTION" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetListEtabByIdEtab" SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:SessionParameter DefaultValue="0" Name="Id_EtablissementMere" 
                                                SessionField="idPole" Type="String" />
                                            <asp:Parameter DefaultValue="0" Name="Type" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                               
                                </p>
                                   </td>
                                    <td width="50%" style="height: 37px">
                                <p>
                                    
                                   
                                    <label >
                                        Etablissement Mere <b style="color: Red">*</b>&nbsp;:</label>
                                         <asp:DropDownList ID="DDLEtablissementMere" runat="server" width="150px" AutoPostBack="True"
                                        DataTextField="LIBELLE_ORGANISATION"
                                DataValueField="CODE_ORGANISATION" AppendDataBoundItems="True" 
                                        onselectedindexchanged="DDLEtablissementMere_SelectedIndexChanged" DataSourceID="SDS_ETABLISSEMENT_MERE"  
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
                                   </p>
                            </td>
                            </tr>
                            <tr>
                    <td width="50%" style="height: 37px">
                                <p>
                                    
                                   
                                    <label >
                                        Etablissement Fille &nbsp;<b style="color: Red">*</b>&nbsp;:</label>
                                         <asp:DropDownList ID="DDL_Etablissement_Fille" runat="server" DataSourceID="SDS_ETABLISSEMENT_FILLE" 
                                        width="150px" AutoPostBack="True"   DataTextField="LIBELLE_ORGANISATION"
                                DataValueField="CODE_ORGANISATION"  AppendDataBoundItems="True" 
                                        onselectedindexchanged="DDL_Etablissement_Fille_SelectedIndexChanged" >
                                <asp:ListItem Text="- Sélectionner -" Value="0000" />
                            </asp:DropDownList>
                                    <asp:SqlDataSource ID="SDS_ETABLISSEMENT_FILLE" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetListEtabByIdEtab" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DDLEtablissementMere" Name="Id_EtablissementMere" 
                                                PropertyName="SelectedValue" Type="String" />
                                            <asp:Parameter DefaultValue="1" Name="Type" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                  <asp:CompareValidator ID="CompareValidator3" runat="server" 
            ControlToValidate="DDLEtablissementMere" Display="None" 
            ErrorMessage="&lt;b&gt;Texte manquant&lt;/b&gt;&lt;br /&gt;Veuillez Choisir une étabelissement" 
            Operator="NotEqual" ValidationGroup="v" ValueToCompare="0"></asp:CompareValidator>
        <cc1:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" 
            Enabled="True" PopupPosition="right" TargetControlID="CompareValidator1">
        </cc1:ValidatorCalloutExtender>
                                    <asp:SqlDataSource ID="SDS_GetPrivision" runat="server" 
                                        ConnectionString="<%$ ConnectionStrings:CNX_Logistique %>" 
                                        SelectCommand="SGPL_GetListArticleBy_Date_Etab_Dir_Pol" 
                                        SelectCommandType="StoredProcedure">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="DDL_Etablissement_Fille" Name="IdValue" 
                                                PropertyName="SelectedValue" Type="Int32" />
                                            <asp:Parameter DefaultValue="1" Name="Value" Type="Int32" />
                                            <asp:SessionParameter DefaultValue="" Name="IdModule" SessionField="Modele" 
                                                Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                   </p>
                                        
                            </td>
                    

                       <td  width="50%" style="height: 37px">
                                
                                 <asp:Button ID="BTN_Valider" runat="server" Text="Valider" class="bts" 
                                     Visible="False" onclick="BTN_Valider_Click" />
                             
                                <asp:Button ID="BtnModification" runat="server" class="bts" 
                                     Text="Modification" Visible="false" onclick="BtnModification_Click" />
                                   
                            </td>
                            </tr>
                          
                        </table>
                        <asp:GridView ID="GDVArticle" runat="server" AutoGenerateColumns="False" 
               Width="100%"   AllowPaging ="true"
                                        BorderStyle="None"  >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                           
                                                             <th width="25%">
                                                                <asp:Label ID="LblNomEnClature1" runat="server" Text="Nom En Clature" Font-Size="15px" ForeColor="White"></asp:Label>
                                                            </th>
                                                             <th width="25%">
                                                                <asp:Label ID="LblDesignation1" runat="server" Text="Désignation" Font-Size="15px" ForeColor="White"></asp:Label>
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
                                                               <asp:Label ID="LblNomEnClature" runat="server" Text='<%# Eval("Article_Id") %>' ></asp:Label>
                                                            </td>
                                                            <td width="25%">                   
                                                                <asp:Label ID="LblDesignation" runat="server" Text='<%# Eval("Article_Designation") %>' ></asp:Label>
                                                             </td> 
                                                             <td width="25%">                   
                                                                <asp:Label ID="LblTail" runat="server" Text='<%# Eval("ArticlePrevision_QtePrevision") %>' ></asp:Label>
                                                                   <asp:Label ID="LBL_IdPrevision" runat="server" Text='<%# Eval("Prevision_Id") %>' Visible = "false"></asp:Label>
                                                         
                                                             </td> 
                                                             
                                                             </tr>
                                                    </table>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
 <asp:GridView ID="GDValid" runat="server" AutoGenerateColumns="False" 
               Width="100%" AllowPaging ="true" BorderStyle="None"  >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                        <tr style="background-color: #fbb17f; width: 100%; height: 35px" align="center">
                                                          

                                                             <th width="25%">
                                                                <asp:Label ID="LblNumArticle" runat="server" Text="Article" Font-Size="15px" ForeColor="White"></asp:Label>
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
                                                                  <asp:Label ID="IdArticle" runat="server" Text='<%# Eval("ArticlePrevision_ArticleId") %>'  ></asp:Label>
                                                      
                                                            </td>
                                                           
                                                            <td width="25%">
                                                            
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
                                    </asp:GridView>
             <asp:HiddenField ID="HdnAgent" runat="server" />
                       <asp:HiddenField ID="HdnPrevision" runat="server" />


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
