<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupErrorReportUC.ascx.cs" Inherits="UserControls_ModelPopupErrorReportUC" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script type="text/javascript">

    function HideModalPopup() {
        $("#ContentPlaceHolder1_ModelPopupErrorReportUC1_pnlListErrorRecord").hide();
        $("#ModelErrorReport_backgroundElement").hide();
        return false;
    }
</script>
<asp:ModalPopupExtender ID="ModelErrorReport" PopupControlID="pnlListErrorRecord" TargetControlID="ErrorPopupCloseBTN"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" BehaviorID="ModelErrorReport" CancelControlID="imgClose4">
</asp:ModalPopupExtender>
<asp:Button ID="ErrorPopupCloseBTN" runat="server" Style="display: none" />


<asp:Panel ID="pnlListErrorRecord" runat="server" Style="display: none; z-index: 9999999!important; display: none">
    <div class="AttributeWrp" id="InputDiv">
        <div class="AttribWrpIn">
            <a href="#" class="CloseBtnP" id="imgClose4" onclick='return HideModalPopup()'></a>
            <h2>Information Error Report</h2>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="AttribMid">
                <div id="Div2" style="width: 221px; float: left; border-right: 1px solid #ccc; margin: 0 1px 0 0;">
                    <ul class="LeftFrm">
                        <li>
                            <label>
                                Include on Map?</label>
                            <asp:RadioButton ID="radioBtnYes" runat="server" CssClass="radioBtn" GroupName="Map"
                                Checked="true" />
                            <%-- <input name="" type="radio" value="" class="radioBtn" />--%>
                            <span>Yes</span>
                            <asp:RadioButton ID="radioBtnNo" runat="server" CssClass="radioBtn" GroupName="Map" />
                            <%--<input name="" type="radio" value="" class="radioBtn" />--%>
                            <span>No</span></li>
                        <li>
                            <label>
                                Error Name</label>
                            <asp:TextBox ID="txtErrorName" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtInput" runat="server" InitialValue="" ValidationGroup="addErrorLink"
                                ControlToValidate="txtErrorName" ErrorMessage="Please enter the Error Name" ForeColor="Red"
                                Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%;"
                                Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                ErrorMessage="Special symbols not allowed" ValidationGroup="addErrorLink" ControlToValidate="txtErrorName"
                                ForeColor="Red"></asp:RegularExpressionValidator>

                        </li>
                        <li>
                            <label>
                                Cycle Time</label>
                            <asp:TextBox ID="txtCycleTimeValue" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtInputivalue" runat="server" InitialValue=""
                                ValidationGroup="addErrorLink" ControlToValidate="txtCycleTimeValue" ErrorMessage="Please enter the Cycle Time"
                                ForeColor="Red" Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%;"
                                Display="Dynamic" ValidationExpression="^[0-9]{1,100}$" runat="server"
                                ErrorMessage="Only numeric allowed" ValidationGroup="addErrorLink" ControlToValidate="txtCycleTimeValue"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <label>
                                Work Content</label>
                            <asp:TextBox ID="txtWorkContentValue" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                ValidationGroup="addErrorLink" ControlToValidate="txtWorkContentValue" ErrorMessage="Please enter the Work Content"
                                ForeColor="Red" Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" Style="width: 100%;"
                                Display="Dynamic" ValidationExpression="^[0-9]{1,100}$" runat="server"
                                ErrorMessage="Only numeric allowed" ValidationGroup="addErrorLink" ControlToValidate="txtWorkContentValue"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <label>
                                Counter Measure</label>
                            <asp:TextBox ID="txtCounterMeasureValue" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                ValidationGroup="addErrorLink" ControlToValidate="txtCounterMeasureValue" ErrorMessage="Please enter the Counter Measure"
                                ForeColor="Red" Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" Style="width: 100%;"
                                Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                ErrorMessage="Special symbols not allowed" ValidationGroup="addErrorLink" ControlToValidate="txtCounterMeasureValue"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <label>
                                CounterMeasure Strength</label>
                            <asp:TextBox ID="txtCounterMeasureStrengthValue" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue=""
                                ValidationGroup="addErrorLink" ControlToValidate="txtCounterMeasureStrengthValue" ErrorMessage="Please enter the CounterMeasure Strength"
                                ForeColor="Red" Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="width: 100%;"
                                Display="Dynamic" ValidationExpression="^[0-9]{1,100}$" runat="server"
                                ErrorMessage="Only numeric allowed" ValidationGroup="addErrorLink" ControlToValidate="txtCounterMeasureStrengthValue"
                                ForeColor="Red"></asp:RegularExpressionValidator>
                        </li>



                        <li>
                            <asp:ValidationSummary ID="ValidationSummaryInput" runat="server" HeaderText="Following error occurs:"
                                ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addErrorLink"
                                ForeColor="Red" />
                            <asp:Button ID="addlinkBtn" runat="server" CssClass="BlueBtnLe" Text="Add Error" ValidationGroup="addErrorLink"
                                OnClick="addlinkBtn_Click" />
                        </li>
                    </ul>
                </div>
                <div class="RightAt">
                    <asp:GridView ID="gridErrorReport" runat="server" AlternatingRowStyle-CssClass="GrayBg" AutoGenerateColumns="false"
                        AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gridErrorReport_PageIndexChanging"
                        OnRowCreated="gridErrorReport_RowCreated" OnRowDeleting="gridErrorReport_RowDeleting" OnRowEditing="gridErrorReport_RowEditing"
                        OnSelectedIndexChanging="gridErrorReport_SelectedIndexChanging" OnSorting="gridErrorReport_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Error" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%# Eval("Error") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cycle Time" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%# Eval("CycleTime") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Work Content" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%# Eval("WorkContent") %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Counter Measure" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%# Eval("CounterMeasure") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Counter Measure Strength" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%# Eval("CounterMeasureStrength") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                <ItemTemplate>
                                    <asp:Literal ID="litErrorReportId" runat="server" Visible="false" Text='<%#Eval("ErrorID") %>' />
                                    <asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("ErrorID")%>'
                                        ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                    <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                        ToolTip="Delete" CommandArgument='<%#Eval("ErrorID") %>' ImageUrl="~/images/delete.png" />
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


