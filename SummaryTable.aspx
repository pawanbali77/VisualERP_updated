<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true"
    CodeFile="SummaryTable.aspx.cs" Inherits="SummaryTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlSummaryList" runat="server">
                <div class="right_container">
                    <div class="right_container_top" id="Title">
                        <h1>Summary Table</h1>
                        <div class="right_nav">
                            <ul>
                                <li style="margin-right: 1px">
                                    <asp:LinkButton ID="lnkbtnSaveSummary" runat="server" Text="Save Record" ToolTip="Save Record" CssClass="DesignBtn"
                                        OnClick="lnkbtnSaveSummary_Click" ValidationGroup="addValue" />
                                </li>
                                <li style="margin-right: 1px">
                                    <asp:LinkButton ID="lnkbtnCancelSummary" runat="server" Text="Cancel" ToolTip="Cancel Record" CssClass="DesignBtn" OnClick="lnkbtnCancelSummary_Click" />
                                </li>
                            </ul>
                        </div>
                    </div>
                    <%--<div class="RightAt" style="margin-left: 417px; margin-top: 50px;">--%>
                    <div id="divSummaryTable" runat="server" class="RightAt" style="margin-left: 80px; margin-top: 130px; overflow-y: scroll; max-height: 450px;">
                        <asp:GridView ID="gridProcessSummary" runat="server" AlternatingRowStyle-CssClass="field_row bg_white"
                            CssClass="" AutoGenerateColumns="false" CellSpacing="0" CellPadding="0" RowStyle-CssClass="field_row"
                            GridLines="None" HeaderStyle-CssClass="block_1_top" OnRowDataBound="gridProcessSummary_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="Function">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAttributeName" runat="server" Text='<%# Eval("AttributeName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:TemplateField HeaderText="Function">
                            <ItemTemplate>
                                <asp:Label ID="lblAttributeValueSum" runat="server" Text='<%# Eval("AttributeValueSum") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Function" HeaderStyle-Width="322px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFunctionID" runat="server" Text='<%# Eval("FunctionID") %>' Visible="false" />
                                        <asp:DropDownList ID="ddlSelectFunction" runat="server" AutoPostBack="false">
                                        </asp:DropDownList>

                                     <%--   <asp:RequiredFieldValidator ID="reqddlFunction" runat="server" InitialValue="0" ValidationGroup="addValue"
                                            ControlToValidate="ddlSelectFunction" ErrorMessage="Please associate a function" ForeColor="Red">
                                        </asp:RequiredFieldValidator>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUnitName" runat="server" Text='<%# Eval("UnitName") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div>
                                    &nbsp;
                                </div>
                                <div class="msgSucess12">
                                    <p>
                                        Empty Summary record!
                                    </p>
                                </div>
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div class="AjaxLoaderOuter123">
                <div class="AjaxLoaderInner123">
                    <img src='<%= ResolveUrl("~/images/ajax-loading11.gif") %>' alt="Wait..." style="background-color: Transparent;" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>
