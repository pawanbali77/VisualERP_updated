<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .btnNextNew {
            width: 127px;
            font-size: 11px;
            line-height: 14px;
            height: 24px;
            float:none!important;
            margin:unset!important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdnProcessId" runat="server" />
    <asp:UpdatePanel ID="Uppnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="right_container">
                <div class="right_container_top" id="Title">
                    <h1 style="font: 20px Arial, Helvetica, sans-serif;">Edit Header</h1>
                    <div id="divErrorMsg" runat="server" style="font: bold 12px Arial, Helvetica, sans-serif; color: #555; padding: 7px 0px 0px 10px; height: 30px; float: left; min-width: 450px; max-width: 500px;">
                        <asp:Label ID="lblMsg" runat="server" />
                    </div>
        

                    <div class="grey_strip" id="divDefaultCol" runat="server">
                        <table class="activity_block" cellpadding="0" cellspacing="0" style="width: 500px;">
                            <tr class="activity_block_top">
                                <td class="th_fieldsi left" id="attribute" runat="server">
                                    <asp:LinkButton ID="lnkbtn" runat="server">Attributes</asp:LinkButton>
                                </td>
                                <td class="th_fieldsi" id="Input" runat="server">
                                    <asp:LinkButton ID="lnkBtnInput" runat="server">Inputs</asp:LinkButton>
                                </td>
                                <td class="th_fieldsi" id="BOM" runat="server">
                                    <asp:LinkButton ID="lnkBtnBOM" runat="server">BOM</asp:LinkButton>
                                </td>
                                <td class="th_fieldsi" id="TFG" runat="server">
                                    <asp:LinkButton ID="lnkBtnTFG" runat="server">TFG</asp:LinkButton>
                                </td>
                                <td class="th_fieldsi" id="Machine" runat="server">
                                    <asp:LinkButton ID="lnkBtnMachine" runat="server">Machine</asp:LinkButton>
                                </td>
                                <td class="th_fieldsi right last" id="ErrorReport" runat="server">
                                    <asp:LinkButton ID="lnkbtnErrorReport" runat="server">Error Report</asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="padding-top: 105px;">
                        <asp:GridView ID="gridActivityOrder" runat="server" RowStyle-CssClass="field_row"
                            AlternatingRowStyle-CssClass="field_row bg_white" AutoGenerateColumns="false"
                            AllowSorting="true" HeaderStyle-CssClass="block_1_top" CellSpacing="0"
                            CellPadding="0" Width="100%" GridLines="None">
                            <Columns>
                                 <asp:TemplateField HeaderText="Sr No." HeaderStyle-HorizontalAlign="left" >
                                    <ItemTemplate>
                                        <asp:Label ID="lblSOrderNo" runat="server" Text='<%# Eval("SequanceOrder") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                           
                                 <asp:TemplateField HeaderText="Current Label Name" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCName" runat="server" Text='<%# Eval("Headerlblname") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Change Label Name" HeaderStyle-HorizontalAlign="left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtchangename" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div>
                        <asp:Button ID="btnshcolApply" CssClass="btnNextNew" runat="server" OnClick="btnshcolApply_Click" Text="Apply" />
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

