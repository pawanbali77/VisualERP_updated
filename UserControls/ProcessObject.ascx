<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcessObject.ascx.cs" Inherits="UserControls_ProcessObject" %>

<table class="activity_block" cellpadding="0" cellspacing="0">
    <tr class="activity_block_top">
        <td class="th_fieldsi left" id="attribute" runat="server">
            <asp:LinkButton ID="lnkbtn" runat="server" OnClick="lnkbtn_Click1">Attributes</asp:LinkButton>
        </td>
        <td class="th_fieldsi" id="Input" runat="server">
            <asp:LinkButton ID="lnkBtnInput" runat="server" OnClick="lnkBtnInput_Click">Inputs</asp:LinkButton>
        </td>
        <td class="th_fieldsi" id="BOM" runat="server">
            <asp:LinkButton ID="lnkBtnBOM" runat="server" OnClick="lnkBtnBOM_Click">BOM</asp:LinkButton>
        </td>
        <td class="th_fieldsi" id="TFG" runat="server">
            <asp:LinkButton ID="lnkBtnTFG" runat="server" OnClick="lnkBtnTFG_Click">TFG</asp:LinkButton>
        </td>
        <td class="th_fieldsi" id="Machine" runat="server">
            <asp:LinkButton ID="lnkBtnMachine" runat="server"
                OnClick="lnkBtnMachine_Click">Machine</asp:LinkButton>

        </td>
        <td class="th_fieldsi right last" id="ErrorReport" runat="server">
            <asp:LinkButton ID="lnkbtnErrorReport" runat="server" OnClick="lnkbtnErrorReport_Click">Error Report</asp:LinkButton>
            <div class="FirstCloseBtnTab">
                <div style="cursor: pointer;">
                    <asp:LinkButton ID="deleteBtnPoid" runat="server" CssClass="SecondCloseBtnTab"></asp:LinkButton>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td colspan="6" class="activity_txt">
            <asp:LinkButton ID="lblOrderNo" runat="server" OnClick="lnkActivityName_Click" Style="color: #555555!important"></asp:LinkButton>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <div style="overflow: auto; height: 200px">
                <asp:GridView ID="gridActivityOrder" runat="server" RowStyle-CssClass="field_row" AlternatingRowStyle-CssClass="field_row bg_white" AutoGenerateColumns="false" OnSorting="gridActivityOrder_Sorting"
                    AllowSorting="true" OnRowCreated="gridActivityOrder_RowCreated" HeaderStyle-CssClass="block_1_top" CellSpacing="0" CellPadding="0" Width="100%" GridLines="None">
                    <Columns>
                        <asp:TemplateField SortExpression="AttributeName" HeaderStyle-CssClass="attributes" ItemStyle-CssClass="attributes_td">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Attribute</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "AttributeName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="AttributeValue"
                            ItemStyle-CssClass="value_td" HeaderStyle-CssClass="value">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Value</a>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <%#DataBinder.Eval(Container.DataItem, "AttributeValue")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="UnitName"
                            ItemStyle-CssClass="unit_td" HeaderStyle-CssClass="unit">
                            <HeaderTemplate>
                                <a class="block_arrow" href="#">Unit</a>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <%#DataBinder.Eval(Container.DataItem, "UnitName")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <RowStyle CssClass="field_row" />
                </asp:GridView>
            </div>
        </td>
    </tr>
</table>
