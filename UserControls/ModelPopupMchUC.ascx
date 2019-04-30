<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupMchUC.ascx.cs"
    Inherits="UserControls_ModelPopupMchUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script language="javascript" type="text/javascript">
//    jQuery(document).ready(function () {
//        jQuery(".lightbox").lightbox({
//            fileLoadingImage: '<%= this.RootPath() + "App_Shared/LightBox/images/loading.gif" %>',
//            fileBottomNavCloseImage: '<%= this.RootPath() + "App_Shared/LightBox/images/closelabel.gif" %>'
//        });
//    });
    
</script>
<asp:ModalPopupExtender ID="ModelMachine" PopupControlID="pnllist5" TargetControlID="Button5"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" 
    CancelControlID="imgClose5">
</asp:ModalPopupExtender>
<asp:Button ID="Button5" runat="server" Style="display: none" />
<asp:Panel ID="pnllist5" runat="server" Style="display: none; z-index: 9999999!important; display:none">
    <div class="AttributeWrp">
        <div class="AttribWrpIn">
            <a href="#" class="CloseBtnP" id="imgClose5" onclick='return ZoomRefresh()'></a>
            <%--<h2 id="head1">
                Machine Manager > Machine List</h2>--%>
                <h2 id="head1">
                Machine Manager > <asp:Literal ID="ltrMachine" runat="server" /></h2>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="AttribMid1">
                <asp:Panel ID="PanalMch" runat="server">
                    <%--<div style="height: 30px; font-weight: bold; text-decoration: underline;">--%>
                <div style="height: 50px; font-weight: bold; text-decoration: underline; font-size: 20px;">
                    <br />
                        <asp:LinkButton ID="lnkbtnOpenDivMachine" runat="server" Text="Add Machine Information"
                            Style="margin: 0px 0px 0px 15px; color: Orange" OnClick="lnkbtnOpenDivMachine_Click" />
                        <div style="height: 100px; display: none;" id="divMachineAdd">
                        <asp:UpdatePanel ID="UpdateFileUp" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div class="RightAt1" style="margin: 15px 0px 0px 92px; margin-top: 40px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <label>
                                                Machine Name</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMchName" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtMchName" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMch" ControlToValidate="txtMchName"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Process Object Name</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtProcessObjName" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtProcessObjName" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMch" ControlToValidate="txtProcessObjName"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Machine Type</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMchType" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtMchType" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMch" ControlToValidate="txtMchType"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>


                                        <td>
                                        <label>
                                            Machine Photo</label>
                                    </td>
                                    <td> 
                                    
                                    <a target="_blank" runat="server" id="hyview" style="cursor: pointer">View Image </a>
                                   <%-- <asp:HyperLink ID="hyview" CssClass="lightbox" runat="server" Text="View Image" />--%>
                                        <asp:FileUpload ID="fileUpMch" runat="server" />
                                        
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ValidationExpression="^.*([^\.][\.](([gG][iI][fF])|([Jj][pP][Gg])|[Jj][Pp][Ee]|([Jj][pP][Ee][Gg])|([Bb][mM][pP])|([Pp][nN][Gg])))"
                                            ValidationGroup="addMch" Display="Dynamic" ErrorMessage="Invalid File" SetFocusOnError="true"
                                            ControlToValidate="fileUpMch" Style="color: Red;"></asp:RegularExpressionValidator>
                                       
                                        <asp:RequiredFieldValidator runat="server" ID="imagerequiredField" Display="Dynamic"
                                            ErrorMessage="*" ValidationGroup="addMch" ControlToValidate="fileUpMch" Style="margin: 5px;
                                            color: Red" />
                                    </td>


                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                PM Schdule ID</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPMId" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtPMId" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPMId"
                                            ValidChars="0123456789">
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <label>
                                                MTBF</label>
                                        </td>
                                        <td>
                                        
                                            <asp:TextBox ID="txtMtbf" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtMtbf" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMch" ControlToValidate="txtMtbf"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                MTTR</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMttr" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtMttr" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMch" ControlToValidate="txtMttr"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Maintenance Cost</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMainCost" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtMainCost" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtMainCost"
                                            ValidChars="0123456789.">
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Purchase Price</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPurchasePrice" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtPurchasePrice" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                               <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtPurchasePrice"
                                            ValidChars="0123456789.">
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <label>
                                                Book Value</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtbookVal" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtbookVal" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtbookVal"
                                            ValidChars="0123456789">
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Remaining Life</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRemainLife" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtRemainLife" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMch" ControlToValidate="txtRemainLife"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Manual ID</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtManualId" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtManualId" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtManualId"
                                            ValidChars="0123456789" >
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Parts List ID</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPartlistId" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" InitialValue=""
                                                ValidationGroup="addMch" ControlToValidate="txtPartlistId" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtPartlistId"
                                            ValidChars="0123456789" >
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="text-align:right">
                                            <%--   <input name="" type="button" class="BlueBtnLe" value="Submit" />--%>
                                            <asp:Button ID="submitBtn" runat="server" CssClass="BlueBtnLe" Text="Submit" ValidationGroup="addMch"
                                                OnClick="submitBtn_Click" />
                                        </td>
                                        <td style="text-align: center">
                                            <button type="reset" value="Reset" class="BlueBtnLe">
                                                Reset</button>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="bckBtnMch" runat="server" CssClass="BlueBtnLe" Text="Back" OnClick="bckBtnMch_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            </ContentTemplate>
                            <Triggers>
                             <asp:PostBackTrigger  ControlID="submitBtn" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="RightAt2" id="DivGridMch">
                        <div>
                        </div>
                        <asp:GridView ID="gridMch" runat="server" AlternatingRowStyle-CssClass="GrayBg" AutoGenerateColumns="false"
                            AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gridMch_PageIndexChanging"
                            OnRowCreated="gridMch_RowCreated" OnRowDeleting="gridMch_RowDeleting" OnRowEditing="gridMch_RowEditing"
                            OnSelectedIndexChanging="gridMch_SelectedIndexChanging" OnSorting="gridMch_Sorting" OnRowCommand="gridMch_OnRowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="S No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Machine Name" HeaderStyle-ForeColor="#43494F" SortExpression="MachineName">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "MachineName")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Machine Type" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "MachineType")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="MTTR" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "MTTR")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Maintenance Cost" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "MaintenanceCost")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Purchase Price" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "PurchasePrice")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Book Value" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F" >
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "BookValue")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Repair Log" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgbtnLog" runat="server" CommandArgument='<%# Eval("MachineID") %>'
                                            CommandName="Repairlog" ImageUrl="~/images/calender.jpg" Width="15" Height="15" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                    <ItemTemplate>
                                        <asp:Literal ID="litMachineId" runat="server" Visible="false" Text='<%#Eval("MachineID") %>' />
                                        <asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("MachineID")%>'
                                            ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                        <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                            ToolTip="Delete" CommandArgument='<%#Eval("MachineID") %>' ImageUrl="~/images/delete.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div>
                                    &nbsp;</div>
                                <div class="msgSucess12">
                                    <p>
                                        No records found !
                                    </p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
                <asp:Panel ID="PanelMchRepair" runat="server">
                    <div style="height: 30px; font-weight: bold; text-decoration: underline;">
                        <asp:LinkButton ID="lnkBtnOpenDivMchRepair" runat="server" Text="Add Machine Repair Information" 
                            Style="margin: 0px 0px 0px 15px; color: Orange" OnClick="lnkBtnOpenDivMchRepair_Click" />
                        <div style="height: 100px; display: none;" id="divMachineAddRepair">
                            <div class="RightAt1" style="margin: 15px 0px 0px 92px; margin-top: 40px">
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <label>
                                                Critical</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCritical" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtCritical" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtCritical"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                TTR</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTTR" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtTTR" ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtTTR"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                SkillType Of Repair</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSkillTypeOfRepair" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtSkillTypeOfRepair" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator10" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtSkillTypeOfRepair"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Counter Measure</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCounterMeasure" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtCounterMeasure" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator11" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtCounterMeasure"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Type Of Repair</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtTypeOfRepair" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtTypeOfRepair" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator12" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtTypeOfRepair"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Actual Repair</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtActualRepair" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtActualRepair" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator13" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtActualRepair"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Cost Of RepairParts</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCostOfRepairParts" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                              <asp:FilteredTextBoxExtender ID="Numbers" runat="server" TargetControlID="txtCostOfRepairParts"
                                            ValidChars="0123456789." BehaviorID="txtCostOfRepairParts">
                                        </asp:FilteredTextBoxExtender>

                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtCostOfRepairParts" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Cost Of RepairLabor</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCostOfRepairLabor" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtCostOfRepairLabor" ErrorMessage="*"
                                                ForeColor="Red">
                                                
                                            </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCostOfRepairLabor"
                                            ValidChars="0123456789." BehaviorID="txtCostOfRepairLabor">
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Cost Of RepairOutsource</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCostOfRepairOutsource" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtPurchasePrice" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCostOfRepairOutsource"
                                            ValidChars="0123456789." BehaviorID="txtCostOfRepairOutsource">
                                        </asp:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <label>
                                                Scheduled Unscheduled</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtScheduledUnscheduled" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtScheduledUnscheduled" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator14" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtScheduledUnscheduled"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Down Time</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDownTime" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtDownTime" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator15" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtDownTime"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                        <td>
                                            <label>
                                                Preventive Predictive Reactive</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPreventivePredictiveReactive" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtPreventivePredictiveReactive"
                                                ErrorMessage="*" ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator16" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtPreventivePredictiveReactive"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <label>
                                                Root Cause</label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRootCause" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" InitialValue=""
                                                ValidationGroup="addMchRepair" ControlToValidate="txtRootCause" ErrorMessage="*"
                                                ForeColor="Red">
                                            </asp:RequiredFieldValidator>
                                             <asp:RegularExpressionValidator ID="RegularExpressionValidator17" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addMchRepair" ControlToValidate="txtRootCause"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td style="text-align: right">
                                            <%--   <input name="" type="button" class="BlueBtnLe" value="Submit" />--%>
                                            <asp:Button ID="btnSubmitMchRepair" runat="server" CssClass="BlueBtnLe" Text="Submit" ValidationGroup="addMchRepair"
                                                OnClick="btnSubmitMchRepair_Click" />
                                        </td>
                                        <td style="text-align: center">
                                            <button type="reset" value="Reset" class="BlueBtnLe">
                                                Reset</button>
                                        </td>
                                        <td style="text-align: left">
                                            <asp:Button ID="bckBtnMchRepair" runat="server" CssClass="BlueBtnLe" Text="Back"
                                                OnClick="bckBtnMchRepair_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="RightAt2" id="DivGridMchRepair" runat="server">
                        <div>
                        </div>
                        <asp:GridView ID="gridMchRepair" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                            AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gridMchRepair_PageIndexChanging"
                            OnRowCreated="gridMchRepair_RowCreated" OnRowDeleting="gridMchRepair_RowDeleting"
                            OnRowEditing="gridMchRepair_RowEditing" OnSelectedIndexChanging="gridMchRepair_SelectedIndexChanging"
                            OnSorting="gridMchRepair_Sorting">
                            <Columns>
                                <asp:TemplateField HeaderText="S No.">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Critical" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Critical")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Repair" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "ActualRepair")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cost Of RepairParts" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "CostOfRepairParts")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Down Time" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "DownTime")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Root Cause" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "RootCause")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Counter Measure" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "Countermeasure")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                    <ItemTemplate>
                                        <asp:Literal ID="litMchRepairId" runat="server" Visible="false" Text='<%#Eval("MachineRepairID") %>' />
                                        <asp:ImageButton ID="editBtnMchRepair" runat="server" CommandName="Edit" CommandArgument='<%#Eval("MachineRepairID")%>'
                                            ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                        <asp:ImageButton ID="deleteBtnMchRepair" runat="server" CommandName="Delete" AlternateText="Delete"
                                            ToolTip="Delete" CommandArgument='<%#Eval("MachineRepairID") %>' ImageUrl="~/images/delete.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                              <EmptyDataTemplate>
                                <div>
                                    &nbsp;</div>
                                <div class="msgSucess12">
                                    <p>
                                        No records found !
                                    </p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                    <div class="Clear">
                    </div>
                </asp:Panel>
            </div>
            <div class="Clear">
            </div>
        </div>
        <div class="Clear">
        </div>
    </div>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
                <ProgressTemplate>
                    <div class="AjaxLoaderOuter123">
                        <div class="AjaxLoaderInner123">
                            <img src='<%= ResolveUrl("~/images/ajax-loading11.gif") %>' alt="Wait..." style="background-color: Transparent;" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
</asp:Panel>
