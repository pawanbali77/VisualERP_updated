<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProcessObjAerrow.ascx.cs" Inherits="UserControls_ProcessObjAerrow" %>
<td style=" width:auto;">
    <div class="nxt_arrow">
        <img src="images/nxt_arrow.png" /></div>
</td>
<td>
    <table class="activity_block" cellpadding="0" cellspacing="0">
        <tr class="activity_block_top">
            <td class="th_fieldsi left">
                <asp:LinkButton ID="lnkbtn" runat="server" PostBackUrl="~/ProcessManager.aspx?ProcessObjectId=1&action=attribute" >Attributes</asp:LinkButton>
            </td>
            <td class="th_fieldsi">
                <asp:LinkButton ID="lnkBtnInput" runat="server" PostBackUrl="~/ProcessManager.aspx?ProcessObjectId=2&action=Input">Inputs</asp:LinkButton>
            </td>
            <td class="th_fieldsi">
                <asp:LinkButton ID="lnkBtnBOM" runat="server" PostBackUrl="~/ProcessManager.aspx?ProcessObjectId=3&action=BOM">BOM</asp:LinkButton>
            </td>
            <td class="th_fieldsi">
                <asp:LinkButton ID="lnkBtnTFG" runat="server" PostBackUrl="~/ProcessManager.aspx?ProcessObjectId=4&action=TFG">TFG</asp:LinkButton>
            </td>
            <td class="th_fieldsi right last">
                <asp:LinkButton ID="lnkBtnMachine" runat="server" PostBackUrl="~/ProcessManager.aspx?ProcessObjectId=5&action=Machine"
                   >Machine</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="5" class="activity_txt">
                Activity 1
            </td>
        </tr>
        <tr>
            <td colspan="5">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr class="block_1_top">
                        <td class="attributes">
                            <a class="block_arrow" href="#">Attribute</a>
                        </td>
                        <td class="value">
                            <a class="block_arrow" href="#">Value</a>
                        </td>
                        <td class="unit">
                            <a class="block_arrow" href="#">Unit</a>
                        </td>
                    </tr>
                    <tr class="field_row bg_white">
                        <td class="attributes_td">
                            VA Time:
                        </td>
                        <td class="value_td">
                            12
                        </td>
                        <td class="unit_td">
                            Sec
                        </td>
                    </tr>
                    <tr class="field_row ">
                        <td class="attributes_td">
                            NVA Time
                        </td>
                        <td class="value_td">
                            15
                        </td>
                        <td class="unit_td">
                            Min
                        </td>
                    </tr>
                    <tr class="field_row bg_white">
                        <td class="attributes_td">
                            VA Time
                        </td>
                        <td class="value_td">
                            12
                        </td>
                        <td class="unit_td">
                            Min
                        </td>
                    </tr>
                    <tr class="field_row ">
                        <td class="attributes_td">
                            NVA Time
                        </td>
                        <td class="value_td">
                            15
                        </td>
                        <td class="unit_td">
                            Min
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</td>