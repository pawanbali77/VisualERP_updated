<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupAttributeUC.ascx.cs"
    Inherits="UserControls_ModelPopupAttributeUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="mopoExUser" PopupControlID="UpdatePnl" TargetControlID="Button1"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" BehaviorID="mopoExUser"
    CancelControlID="imgCloseAttribute">
</asp:ModalPopupExtender>
<asp:Button ID="Button1" runat="server" Style="display: none" />
<asp:UpdatePanel ID="UpdatePnl" runat="server" UpdateMode="Always" Style="display: none;
    z-index: 99999!important;">
    <ContentTemplate>
        <asp:Panel ID="pnlist" runat="server" Style="z-index: 99999!important">
            <div class="AttributeWrp" id="divAttr" style="z-index: 5000 !important;">
                <div class="AttribWrpIn">
                    <%-- <a href="#" class="CloseBtnP" id="imgClosepp" onclick="window.location.href = 'ProcessManager.aspx'"></a>--%>
                    <a href="#" class="CloseBtnP" id="imgCloseAttribute" onclick="window.location.reload();">
                    </a>
                    <%--<a href="#" class="CloseBtnP" id="imgCloseAttribute" onclick='return ZoomRefreshAttribute()'></a>--%>
                    <%-- <asp:ImageButton ID="imgClosepp" runat="server" ImageUrl="~/images/close_btn.png" OnClick="imgClosepp_Click" CssClass="CloseBtnP" ToolTip="Close"/>--%>
                    <h2>
                        Attributes</h2>
                    <asp:Label ID="lblMsg" runat="server"></asp:Label>
                    <div class="AttribMid">
                        <div id="LeftAt">
                            <ul class="LeftFrm">
                                <li>
                                    <label>
                                        Include on Map?</label>
                                    <asp:RadioButton ID="radioBtnYes" runat="server" GroupName="Map" CssClass="radioBtn"
                                        Checked="true" />
                                    <span>Yes</span>
                                    <asp:RadioButton ID="radioBtnNo" runat="server" GroupName="Map" CssClass="radioBtn" />
                                    <span>No</span></li>
                                <li>
                                    <label>
                                        Attribute Name</label>
                                    <asp:TextBox ID="txtAttributeName" MaxLength="100" runat="server" CssClass="AttrTxtFild"
                                        TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtAttribute" runat="server" InitialValue="" ValidationGroup="addValue"
                                        ControlToValidate="txtAttributeName" ErrorMessage="Please enter attribute name"
                                        ForeColor="Red" Text="*">
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" Style="width: 100%;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s.]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addValue" ControlToValidate="txtAttributeName"
                                        ForeColor="Red"></asp:RegularExpressionValidator>
                                </li>
                                <li>
                                    <label>
                                        Attribute Value</label>
                                    <asp:TextBox ID="txtAttrivalue" MaxLength="100" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtAttrivalue" runat="server" InitialValue=""
                                        ValidationGroup="addValue" ControlToValidate="txtAttrivalue" ErrorMessage="Please enter attribute value"
                                        ForeColor="Red" Text="*">
                                    </asp:RequiredFieldValidator>
                                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator1" Style="width: 100%;"
                                        Display="Dynamic" ValidationExpression="^[a-zA-Z0-9\s]{1,100}$" runat="server"
                                        ErrorMessage="Special symbols not allowed" ValidationGroup="addValue" ControlToValidate="txtAttrivalue"
                                        ForeColor="Red"></asp:RegularExpressionValidator>--%>

                                      <asp:CompareValidator ID="CompareValidator1" 
                                 Operator="DataTypeCheck"   Display="Dynamic" Style="width: 100%;" ForeColor="Red"
                                 ControlToValidate="txtAttrivalue" Type="Integer" ErrorMessage="Only numeric allowed"
                                runat="server" ValidationGroup="addValue" ></asp:CompareValidator>

                                </li>
                                <li>
                                    <label>
                                        Units</label>
                                    <asp:DropDownList ID="ddlUnits" runat="server" CssClass="AttrSeFild">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlUnits" runat="server" InitialValue="0" ValidationGroup="addValue"
                                        ControlToValidate="ddlUnits" ErrorMessage="Please select attribute unit" ForeColor="Red"
                                        Text="*">
                                    </asp:RequiredFieldValidator>
                                </li>
                                <asp:ValidationSummary ID="ValidationSummaryAttribute" runat="server" HeaderText="Following error occurs:"
                                    ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addValue"
                                    ForeColor="Red" />
                                <li>
                                    <asp:Button ID="addAttributeBtn" runat="server" CssClass="BlueBtnLe" Text="Add Attribute"
                                        ValidationGroup="addValue" CausesValidation="true" OnClick="addAttributeBtn_Click1" />
                                </li>
                            </ul>
                        </div>
                        <div class="RightAt">
                            <asp:GridView ID="gridAttribute" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                                AutoGenerateColumns="false" AllowPaging="true" OnSorting="gridAttribute_Sorting"
                                OnSelectedIndexChanging="gridAttribute_SelectedIndexChanging" AllowSorting="true"
                                OnPageIndexChanging="gridAttribute_PageIndexChanging" OnRowEditing="gridAttribute_RowEditing"
                                OnRowDeleting="gridAttribute_RowDeleting" OnRowCreated="gridAttribute_RowCreated">
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
                                    <asp:TemplateField SortExpression="AttributeName" HeaderText="Attribute Name" HeaderStyle-ForeColor="#43494F">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "AttributeName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Attribute Value" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "AttributeValue")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit Name" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "UnitName")%>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                        <ItemTemplate>
                                            <asp:Literal ID="litAttributeID" runat="server" Visible="false" Text='<%#Eval("AttributeMenuID") %>' />
                                            <asp:ImageButton ID="editBtn" runat="server" CommandName="Edit" CommandArgument='<%#Eval("AttributeMenuID")%>'
                                                ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                            <asp:ImageButton ID="deleteBtn" runat="server" CommandName="Delete" AlternateText="Delete"
                                                ToolTip="Delete" CommandArgument='<%#Eval("AttributeMenuID") %>' ImageUrl="~/images/delete.png" />
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
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="addAttributeBtn" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
