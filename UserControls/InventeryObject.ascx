<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventeryObject.ascx.cs" Inherits="UserControls_InventeryObject" %>
<td>
    <table class="small_block" cellpadding="0" cellspacing="0">
        <tr>

            <td class="small_block_top" colspan="3">
                
                <%--<img src="images/small_block_top.png" />--%>
                <div class="FirstCloseBtnTab">
                    <div style="cursor: pointer;">
                        <asp:LinkButton ID="deleteBtnTriangleid" runat="server" CssClass="SecondCloseBtnTabTri"></asp:LinkButton>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="activity_txt">
              <asp:Label runat="server" ID="txtInventoryName" Style="color: #555555!important"></asp:Label>
            </td>
        </tr>
        <tr class="block_1_top">
            <td class="Small_b3" style="padding-left: 2%; width: 32%; border-left: 1px solid #CCCCCC;">
                <a class="block_arrow" href="#">CT</a>
            </td>
            <td class="Small_b3" style="padding-left: 2%; width: 29.9%;">
                <a class="block_arrow" href="#">$</a>
            </td>
            <td class="Small_b3" style="border-right: 1px solid #CCCCCC;">
                <a class="block_arrow" href="#">Time</a>
            </td>
        </tr>
        <tr class="bg_white" style="box-shadow: 4px 5px 4px #F3F3F3;">
            <td class="Small_b3" style="padding-left: 2%; width: 32%; border-left: 1px solid #CCCCCC; border-right: 1px solid #CCCCCC;">
                <asp:Literal ID="ltrCT" runat="server"></asp:Literal>
            </td>
            <td class="Small_b3" style="padding-left: 2%; width: 26.8%; border-right: 1px solid #CCCCCC;">
                <asp:Literal ID="ltrDoller" runat="server"></asp:Literal>
            </td>
            <td class="Small_b3" style="border-right: 1px solid #CCCCCC; padding-left: 2%;">
                <asp:Literal ID="ltrTime" runat="server"></asp:Literal>
            </td>
        </tr>
    </table>
</td>
