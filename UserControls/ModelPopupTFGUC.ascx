<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupTFGUC.ascx.cs"
    Inherits="UserControls_ModelPopupTFGUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script language="javascript" type="text/javascript">

    function OpenPopup(div) {

        document.getElementById(div).style.display = 'block';
        document.getElementById('hrefAdd').style.display = 'none';
        document.getElementById('DivGrid').style.display = 'none';

    }


</script>
<asp:ModalPopupExtender ID="ModelTFG" PopupControlID="pnllist4" TargetControlID="Button4"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" BehaviorID="ModelTFG" CancelControlID="imgClose4">
</asp:ModalPopupExtender>
<asp:Button ID="Button4" runat="server" Style="display: none" />
<asp:Panel ID="pnllist4" runat="server" Style="display: none; z-index: 9999999!important; display: none">
    <div class="AttributeWrp">
        <div class="AttribWrpIn">
            <a href="#" class="CloseBtnP" id="imgClose4" onclick='return ZoomRefresh()'></a>
            <h2>TFG Manager</h2>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="AttribMid1">
                <%--<div style="height: 30px; font-weight: bold; text-decoration: underline;">--%>
                <div style="height: 50px; font-weight: bold; text-decoration: underline; font-size: 20px;">
                    <br />
                    <a href="javascript:OpenPopup('divTFG');" id="hrefAdd" style="margin: 0px 0px 0px 15px; display: none">Add TFG Information</a>
                    <asp:LinkButton ID="lnkbtnOpenDiv" runat="server" Text="Add TFG Information" Style="margin: 0px 0px 0px 15px; color: Orange"
                        OnClick="lnkbtnOpenDiv_Click" />
                    <div id="divTFG" style="display: none">
                        <div class="RightAt1" style="margin: 15px 0px 0px 92px; margin-top: 40px">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <label>
                                            Process Object Name</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtprocessObj" runat="server" CssClass="AttrTxtFild" ReadOnly="True"
                                            Text='<%#Request.QueryString["ProcessObjectId"]%>'></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="reqtxtInputivalue" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtprocessObj" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>
                                            TFG Qty</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTFGQty" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="Numbers" runat="server" TargetControlID="txtTFGQty"
                                            FilterType="Numbers">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtTFGQty" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Tool/Fixture/Gage Name</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTool" MaxLength="50" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtTool" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator18" Style="width: 100%; float: left;"
                                            Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                            ErrorMessage="Special symbols not allowed" ValidationGroup="addTFG" ControlToValidate="txtTool"
                                            ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <label>
                                            Calibration Cycle</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCycle" runat="server" MaxLength="50" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtCycle" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%; float: left;"
                                            Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                            ErrorMessage="Special symbols not allowed" ValidationGroup="addTFG" ControlToValidate="txtCycle"
                                            ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            TFG Description</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTFGDesc" runat="server" MaxLength="500" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtTFGDesc" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%; float: left;"
                                            Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                            ErrorMessage="Special symbols not allowed" ValidationGroup="addTFG" ControlToValidate="txtTFGDesc"
                                            ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <label>
                                            Time Calibrate</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txttimeCali" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txttimeCali" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="width: 100%; float: left;"
                                            Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                            ErrorMessage="Special symbols not allowed" ValidationGroup="addTFG" ControlToValidate="txttimeCali"
                                            ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            Cost To Calibrate</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtcostCali" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtcostCali" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtcostCali"
                                            ValidChars="0123456789." BehaviorID="txtcostCali">
                                        </asp:FilteredTextBoxExtender>
                                    </td>
                                    <td>
                                        <label>
                                            Calibration Vendor</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlcaliVender" runat="server" CssClass="AttrSeFild" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlcaliVender_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" InitialValue="0"
                                            ValidationGroup="addTFG" ControlToValidate="ddlcaliVender" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            TFG Vendor Part#</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtvenderPart" runat="server" MaxLength="50" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtvenderPart" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="width: 100%; float: left;"
                                            Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                            ErrorMessage="Special symbols not allowed" ValidationGroup="addTFG" ControlToValidate="txtvenderPart"
                                            ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                    <td>
                                        <label>
                                            Calibration Date1</label>
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="txtCaliDate" runat="server" CssClass="AttrTxtFild"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtCaliDate" runat="server" CssClass="AttrTxtFild" autocomplete="off"></asp:TextBox>
                                        <asp:CalendarExtender ID="Cal1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtCaliDate">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtCaliDate" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            TFG Vendor</label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlTFGvender" runat="server" CssClass="AttrSeFild">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" InitialValue="0"
                                            ValidationGroup="addTFG" ControlToValidate="ddlTFGvender" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>

                                    </td>
                                    <td>
                                        <label>
                                            Calibration Vendor Info</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtVenderInfo" runat="server" CssClass="AttrTxtFild" ReadOnly="true"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtVenderInfo" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            TFG Cost</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTFGCost" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtTFGCost"
                                            ValidChars="0123456789." BehaviorID="txtTFGCost">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" InitialValue=""
                                            ValidationGroup="addTFG" ControlToValidate="txtTFGCost" ErrorMessage="*" ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="text-align: center"></td>
                                    <td style="text-align: center"></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td style="text-align: right">
                                        <asp:Button ID="submitBtn" runat="server" ValidationGroup="addTFG" CssClass="BlueBtnLe"
                                            Text="Submit" OnClick="submitBtn_Click" />
                                    </td>
                                    <td style="text-align: center">
                                        <button type="reset" value="Reset" class="BlueBtnLe">Reset</button>
                                        <%--   <asp:Button ID="ResetBtn" runat="server" CssClass="BlueBtnLe" Text="Reset" OnClick="ResetBtn_Click" />--%>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Button ID="bckBtn" runat="server" CssClass="BlueBtnLe" Text="Back" OnClick="bckBtn_Click" />
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            <div class="RightAt2" id="DivGrid">
                <div>
                </div>
                <asp:GridView ID="gridTFG" runat="server" AlternatingRowStyle-CssClass="GrayBg" AutoGenerateColumns="false"
                    AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gridTFG_PageIndexChanging"
                    OnRowCreated="gridTFG_RowCreated" OnRowDeleting="gridTFG_RowDeleting" OnRowEditing="gridTFG_RowEditing"
                    OnSelectedIndexChanging="gridTFG_SelectedIndexChanging" OnSorting="gridTFG_Sorting">
                    <Columns>
                        <asp:TemplateField HeaderText="S No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--  <asp:TemplateField HeaderText="Process Name" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "ProcessObjectID")%>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Tool/Fixture/Gage Name" ItemStyle-CssClass="GrayBg" SortExpression="Tool_Fixture_GageName"
                            HeaderStyle-ForeColor="#43494F">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "Tool_Fixture_GageName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TFG Description" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TFGDescription")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TFG Cost" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TFGCost")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time To Calibrate" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "TimeToCailbrate")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cost TO Calibrate" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CostToCalibrate")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Calibration Vendor" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "CalibrationVendor")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                            <ItemTemplate>
                                <asp:Literal ID="litTFGId" runat="server" Visible="false" Text='<%#Eval("TFGID") %>' />
                                <asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("TFGID")%>'
                                    ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                    ToolTip="Delete" CommandArgument='<%#Eval("TFGID") %>' ImageUrl="~/images/delete.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <div>
                            &nbsp;
                        </div>
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
