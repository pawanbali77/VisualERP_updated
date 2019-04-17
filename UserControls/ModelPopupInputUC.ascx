<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupInputUC.ascx.cs"
    Inherits="UserControls_ModelPopupInputUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="modelInput" PopupControlID="pnllist2" TargetControlID="Button2"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" BehaviorID="modelInput" CancelControlID="imgClose1">
</asp:ModalPopupExtender>
<asp:Button ID="Button2" runat="server" Style="display: none" />
<asp:Panel ID="pnllist2" runat="server" Style="display: none; z-index: 9999999!important; display:none">
    <div class="AttributeWrp" id="InputDiv">
        <div class="AttribWrpIn">
            <a href="#" class="CloseBtnP" id="imgClose1" onclick='return ZoomRefresh()'></a>
            <h2>
                Information Inputs</h2>
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
                                Link Name</label>
                            <asp:TextBox ID="txtLinkName" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtInput" runat="server" InitialValue="" ValidationGroup="addInputLink"
                                ControlToValidate="txtLinkName" ErrorMessage="Please enter the link name" ForeColor="Red"
                                Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addInputLink" ControlToValidate="txtLinkName"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                            <%-- <input name="" type="text" class="AttrTxtFild" />--%>
                        </li>
                        <li>
                            <label>
                                Link Value</label>
                            <asp:TextBox ID="txtLinkValue" runat="server" MaxLength="100" CssClass="AttrTxtFild"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtInputivalue" runat="server" InitialValue=""
                                ValidationGroup="addInputLink" ControlToValidate="txtLinkValue" ErrorMessage="Please enter the link value"
                                ForeColor="Red" Text="*">
                            </asp:RequiredFieldValidator>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" Style="width: 100%;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addInputLink" ControlToValidate="txtLinkValue"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                        </li>
                        <li>
                            <label>
                                Link Type</label>
                            <%--  <asp:DropDownList ID="ddlLinktype" CssClass="AttrSeFild" runat="server">
                                <asp:ListItem>None</asp:ListItem>
                                <asp:ListItem>Doc</asp:ListItem>
                                <asp:ListItem>STD</asp:ListItem>
                                <asp:ListItem>Photo</asp:ListItem>
                                <asp:ListItem>DWG</asp:ListItem>
                                <asp:ListItem>BOM</asp:ListItem>
                            </asp:DropDownList>--%>
                            <asp:DropDownList ID="ddlTypes" runat="server" CssClass="AttrSeFild">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="reqddlTypes" runat="server" InitialValue="0" ValidationGroup="addInputLink"
                                ControlToValidate="ddlTypes" ErrorMessage="Please select input type" ForeColor="Red"
                                Text="*">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <asp:ValidationSummary ID="ValidationSummaryInput" runat="server" HeaderText="Following error occurs:"
                                ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addInputLink"
                                ForeColor="Red" />
                            <asp:Button ID="addlinkBtn" runat="server" CssClass="BlueBtnLe" Text="Add link" ValidationGroup="addInputLink"
                                OnClick="addlinkBtn_Click" />
                        </li>
                    </ul>
                </div>
                <div class="RightAt">
                    <%--<table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <th>
                                Include on Map
                            </th>
                            <th>
                                Link Name
                            </th>
                            <th>
                                Link Id
                            </th>
                            <th>
                                Link Type
                            </th>
                        </tr>
                        <tr>
                            <td>
                                Yes
                            </td>
                            <td>
                                No. of Employees
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr class="GrayBg">
                            <td>
                                No
                            </td>
                            <td>
                                Name of Employee 1
                            </td>
                            <td>
                                15
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Yes
                            </td>
                            <td>
                                No. of Employees
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr class="GrayBg">
                            <td>
                                No
                            </td>
                            <td>
                                Name of Employee 1
                            </td>
                            <td>
                                15
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Yes
                            </td>
                            <td>
                                No. of Employees
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr class="GrayBg">
                            <td>
                                No
                            </td>
                            <td>
                                Name of Employee 1
                            </td>
                            <td>
                                15
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Yes
                            </td>
                            <td>
                                No. of Employees
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr class="GrayBg">
                            <td>
                                No
                            </td>
                            <td>
                                Name of Employee 1
                            </td>
                            <td>
                                15
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Yes
                            </td>
                            <td>
                                No. of Employees
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr class="GrayBg">
                            <td>
                                No
                            </td>
                            <td>
                                Name of Employee 1
                            </td>
                            <td>
                                15
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Yes
                            </td>
                            <td>
                                No. of Employees
                            </td>
                            <td>
                                12
                            </td>
                            <td>
                                Count
                            </td>
                        </tr>
                    </table>--%>
                    <asp:GridView ID="gridInput" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gridInput_PageIndexChanging"
                        OnRowCreated="gridInput_RowCreated" OnRowDeleting="gridInput_RowDeleting" OnRowEditing="gridInput_RowEditing"
                        OnSelectedIndexChanging="gridInput_SelectedIndexChanging" OnSorting="gridInput_Sorting">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Include on Map" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%--   <%#DataBinder.Eval(Container.DataItem, "IncludeOnMap")%>--%>
                                    <%#Convert.ToString(Eval("IncludeOnMap")) == "True" ? "Yes" : "No"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="InputName" HeaderText="Link Name" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "InputName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Link Id" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "InputID")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Link Type" ItemStyle-CssClass="GrayBg"
                                HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "TypeName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                <ItemTemplate>
                                    <asp:Literal ID="litLinkId" runat="server" Visible="false" Text='<%#Eval("InputID") %>' />
                                    <asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("InputID")%>'
                                        ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                    <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                        ToolTip="Delete" CommandArgument='<%#Eval("InputID") %>' ImageUrl="~/images/delete.png" />
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
