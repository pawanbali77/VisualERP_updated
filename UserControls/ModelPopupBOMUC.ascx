<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupBOMUC.ascx.cs"
    Inherits="UserControls_ModelPopupDataUc" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<%@ Register Src="AddBomProcessUC.ascx" TagName="AddBomProcessUC" TagPrefix="uc1" %>--%>
<script type="text/javascript" language="javascript">
    function ReturnFalse(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode == 8 || charCode == 46)
            return true;
        else
            return false;

    }

    function hideCalendar(cb) { cb.hide(); }
</script>
<%--Add BOM--%>
<asp:ModalPopupExtender ID="ModelBOM" PopupControlID="pnllist3" TargetControlID="Button3"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" BehaviorID="ModelBOM" CancelControlID="imgClose3">
</asp:ModalPopupExtender>
<asp:Button ID="Button3" runat="server" Style="display: none" />
<%--<asp:UpdatePanel ID="UpdatePnlBom" runat="server" UpdateMode="Conditional" Style="z-index: 9999999!important">
    <ContentTemplate>--%>
<asp:Panel ID="pnllist3" runat="server" Style="z-index: 9999!important; display:none">
    <div class="AttributeWrp">
        <div class="AttribWrpIn">
            <a href="#" class="CloseBtnP" id="imgClose3" onclick='return ZoomRefresh()'></a>

            <%--<asp:ImageButton ID="imgClose3" runat="server" ImageUrl="~/images/close_btn.png" OnClick="imgClose3_Click" CssClass="CloseBtnP" ToolTip="Close" />--%>
            <h2>
                Add BOM</h2>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="AttribMid1">
                <div style="padding: 5px 0px 0px 0px; background: #ccc; width: 100%; float: right;">
                    <div style="float: left; margin-top: 7px; font-size: 16px; font-weight: bold; text-transform: uppercase;
                        margin-left: 13px">
                        Process Name -
                        <asp:Literal ID="ltrProcessName" runat="server"></asp:Literal>
                    </div>
                    <div style="float: right;">
                        <%--  <asp:Button ID="btnUpBom" runat="server" CssClass="button" OnClientClick="return loadPopupBoxBom();"/>
                         <asp:Button ID="btnDownBom" runat="server" CssClass="button1" OnClientClick="return loadPopupBoxBom();" />--%>
                        <input type="button" name="button" onclick="return ImgbtnUpProcess_click()" class="button" />
                        <input type="button" name="button" onclick="return ImgbtnDwnProcess_click()" class="button1" />
                        <input type="button" name="button" onclick="return ImgbtnCopy_click()" class="button2"
                            runat="server" id="copyBtn" />
                        <%-- <asp:Button ID="copyBtn" runat="server" CssClass="button2" OnClick="copyBtn_Clcik" OnClientClick="return ImgbtnCopy_click()"></asp:Button>--%>
                        <asp:TextBox ID="txtBomProcess" Style="display: none" runat="server"></asp:TextBox>
                    </div>
                    <div class="Clear">
                    </div>
                </div>
                <div id="LeftAt">
                    <ul class="LeftFrm1">
                        <li>
                            <asp:UpdatePanel ID="UpdatepnlBomTree" runat="server" UpdateMode="Always">
                                <ContentTemplate>
                                    <asp:TreeView ID="TreeViewBom" runat="server" CssClass="TreeView1_0" ShowLines="true"
                                        ForeColor="#555555" HoverNodeStyle-ForeColor="#000" SelectedNodeStyle-ForeColor="#000"
                                        SelectedNodeStyle-Font-Bold="true" SelectedNodeStyle-Font-Size="Larger" OnSelectedNodeChanged="TreeViewBom_SelectedNodeChanged">
                                    </asp:TreeView>
                                </ContentTemplate>
                                <%--<Triggers>
                 <asp:AsyncPostBackTrigger ControlID="addProcessBtn" EventName="Click" />
                </Triggers>--%>
                            </asp:UpdatePanel>
                        </li>
                    </ul>
                </div>
                <div style="height: 100px">
                    <div class="RightAt1" id="divBom" runat="server">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" style="margin-top: 20px">
                            <tr>
                                <td>
                                    <label>
                                        Process Object Name</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtProcesObjName" runat="server" CssClass="AttrTxtFild" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td>
                                    <label>
                                        BOM Name</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBomName" runat="server" CssClass="AttrTxtFild" ReadOnly="true"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtBomName" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtBomName"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        BOM Description</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBomDescription" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtBomDescription" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtBomDescription"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    BOM Level
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBomLevel" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtBomLevel" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtBomLevel"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <label>
                                        BOM Revision</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtBomRevision" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtBomRevision" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtBomRevision"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Weight
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWeight" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtWeight" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtWeight"
                                        ValidChars="0123456789" BehaviorID="txtWeight">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Uom
                                </td>
                                <td>
                                    <asp:TextBox ID="txtUom" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtUom" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtUom"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Standard Cost
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStandardCost" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtStandardCost" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtStandardCost"
                                        ValidChars="0123456789" BehaviorID="txtStandardCost">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Standard Pack Qty
                                </td>
                                <td>
                                    <asp:TextBox ID="txtStandardPackQty" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtStandardPackQty" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtStandardPackQty"
                                        ValidChars="0123456789" BehaviorID="txtStandardPackQty">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    <label>
                                        Working Cost</label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtWorkingCost" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtWorkingCost" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtWorkingCost"
                                        ValidChars="0123456789" BehaviorID="txtWorkingCost">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Max Pack Length
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaxPackLength" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMaxPackLength" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtMaxPackLength"
                                        ValidChars="0123456789" BehaviorID="txtMaxPackLength">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    Max Pack Width
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaxPackWidth" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMaxPackWidth" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtMaxPackWidth"
                                        ValidChars="0123456789" BehaviorID="txtMaxPackWidth">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Max Pack Height
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMaxPackHeight" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMaxPackHeight" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtMaxPackHeight"
                                        ValidChars="0123456789" BehaviorID="txtMaxPackHeight">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                                <td>
                                    Container Qty
                                </td>
                                <td>
                                    <asp:TextBox ID="txtContainerQty" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtContainerQty" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtContainerQty"
                                        ValidChars="0123456789" BehaviorID="txtContainerQty">
                                    </asp:FilteredTextBoxExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Median Relinishment LT
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMedianLT" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMedianLT" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator6" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtMedianLT"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Min RLT
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMinRLT" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMinRLT" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator7" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtMinRLT"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Rolling 12 Mnth Usage
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRollingUsage" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtRollingUsage" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator8" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtRollingUsage"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Max RLT
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMxRLT" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMxRLT" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator9" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtMxRLT"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Avg Monthly Usage
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAvgMnthUsage" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtAvgMnthUsage" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator10" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtAvgMnthUsage"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Monthly Std Dev
                                </td>
                                <td>
                                    <asp:TextBox ID="txtMnthStdDev" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtMnthStdDev" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator11" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtMnthStdDev"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Risk Factor
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRiskFactor" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtRiskFactor" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator12" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtRiskFactor"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Kanban Qty
                                </td>
                                <td>
                                    <asp:TextBox ID="txtKanbanQty" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator27" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtKanbanQty" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator13" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtKanbanQty"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    In Service?Yes/No
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlServices" runat="server" CssClass="AttrSeFild">
                                        <asp:ListItem Value="True">Yes</asp:ListItem>
                                        <asp:ListItem Value="False">No</asp:ListItem>
                                    </asp:DropDownList>
                                    <%--<asp:RequiredFieldValidator ID="reqddlServices" runat="server" InitialValue="0" ValidationGroup="addBOM"
                                        ControlToValidate="ddlServices" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    In-Service Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInServiceDate" runat="server" CssClass="AttrTxtFild" autocomplete="off"
                                        onKeyPress="javascript:return ReturnFalse(event);"></asp:TextBox>
                                    <asp:CalendarExtender ID="Cal1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtInServiceDate">
                                    </asp:CalendarExtender>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtInServiceDate" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Obsolescence Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtObslncDate" runat="server" CssClass="AttrTxtFild" autocomplete="off"
                                        onKeyPress="javascript:return ReturnFalse(event);"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtObslncDate">
                                    </asp:CalendarExtender>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtObslncDate" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    On-Hand Inventory
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOnHandInventory" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtOnHandInventory" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator14" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtOnHandInventory"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    On-Order
                                </td>
                                <td>
                                    <asp:TextBox ID="txtOnOrder" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtOnOrder" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator15" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtOnOrder"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Next Shipment Due
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNextShipDue" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtNextShipDue" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                     <asp:RegularExpressionValidator ID="RegularExpressionValidator16" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtNextShipDue"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Next Qty Due
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNextQtyDue" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator34" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtNextQtyDue" ErrorMessage="*" ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator17" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtNextQtyDue"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                                <td>
                                    Parts Require in Next Period
                                </td>
                                <td>
                                    <asp:TextBox ID="txtPartsReqNxtPeriod" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtPartsReqNxtPeriod" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator18" Style="width: 100%; float:left;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addBOM" ControlToValidate="txtPartsReqNxtPeriod"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Current Purchasing Owner
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentPurOwner" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtCurrentPurOwner"
                                        FilterType="Numbers" />
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtCurrentPurOwner" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                                <td>
                                    Current Design Owner
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCurrentDesgOwner" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtCurrentDesgOwner"
                                        FilterType="Numbers" />
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server" InitialValue=""
                                        ValidationGroup="addBOM" ControlToValidate="txtCurrentDesgOwner" ErrorMessage="*"
                                        ForeColor="Red">
                                    </asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="text-align: center">
                                </td>
                                <td style="text-align: center">
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td style="text-align: center">
                                    <%--   <input name="" type="button" class="BlueBtnLe" value="Submit" />--%>
                                    <asp:Button ID="submitBtn" runat="server" ValidationGroup="addBOM" CssClass="BlueBtnLe"
                                        Text="Submit" OnClick="submitBtn_Click" />
                                </td>
                                <td style="text-align: center">
                                    <asp:Button ID="resetBtn" runat="server" CssClass="BlueBtnLe" Text="Reset" OnClick="resetBtn_Click" />
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="Clear">
                </div>
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
<%-- </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnUpBom" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>--%>
<div class="AttributeWrpPoupProcess" id="popup_box_addBom">
    <div class="AttribWrPoupProcess">
        <%-- <asp:Button ID="btnCloseBom" runat="server" CssClass="CloseBtnP" OnClick="btnCloseBom_Click" style=" cursor:pointer; border:0 0 0 0"/>--%>
        <a href="#" class="CloseBtnP" id="CloseBom" onclick="return PopupClose()"></a>
        <h2>
            Add Node</h2>
        <div class="AttribMid">
            <div id="LeftAtProcess">
                <ul class="LeftFrm">
                    <li>
                        <label>
                            Node Name</label>
                        <asp:TextBox ID="txtBomProcessNode" runat="server" CssClass="AttrTxtFild" TabIndex="1"
                            Width="245px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtBomProcessNode"
                            ErrorMessage="Please enter the node name" InitialValue="" ValidationGroup="addBom"
                            ForeColor="Red" EnableClientScript="true" Text="*">
                         
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <label>
                            Node Type</label>
                        <asp:DropDownList ID="ddlBomProcessType" runat="server" CssClass="AttrSeFild" Width="245px">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="reqddlType" runat="server" InitialValue="0" ValidationGroup="addBom"
                            ControlToValidate="ddlBomProcessType" ErrorMessage="Please select the type" ForeColor="Red"
                            EnableClientScript="true" Text="*">
                        </asp:RequiredFieldValidator>
                    </li>
                    <li>
                        <asp:Button ID="addBomProcessBtn" ValidationGroup="addBom" runat="server" CssClass="BlueBtnLe"
                            Text="Add" OnClick="addBomProcessBtn_Click" />
                    </li>
                </ul>
            </div>
            <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:"
                ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addBom"
                ForeColor="Red" />
            <div class="Clear">
            </div>
        </div>
        <div class="Clear">
        </div>
    </div>
    <div class="Clear">
    </div>
</div>

 
<%--End Add BOM--%>
