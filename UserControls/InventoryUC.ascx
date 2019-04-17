<%@ Control Language="C#" AutoEventWireup="true" CodeFile="InventoryUC.ascx.cs" Inherits="UserControls_InventoryUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="ModelPopupInventery" PopupControlID="UpdatePnlInventory"
    TargetControlID="Button8" runat="server" BackgroundCssClass="AjaxLoaderOuter"
    BehaviorID="ModelPopupInventery" CancelControlID="imgCloseInventory">
</asp:ModalPopupExtender>
<asp:Button ID="Button8" runat="server" Style="display: none" />
<asp:UpdatePanel ID="UpdatePnlInventory" runat="server" UpdateMode="Conditional" style="display:none;">
    <ContentTemplate>
        <div class="AttributeWrpPoupInventery" id="popup_box_Inventery" runat="server">
            <div class="AttribWrPoupInventery" style="width: 330px; float: left; border: 1px solid #ccc;
                background: #fff; position: relative; margin: 180px 493px 0;">
                <a href="#" class="CloseBtnPInventery" id="imgCloseInventory"></a>
                <h2>
                    Add Inventory</h2>
                <div class="AttribMid">
                    <div id="LeftAtInventery">
                        <ul class="LeftFrm">
                            <li>
                                <label>
                                    CT</label>
                                <asp:TextBox ID="txtCT" runat="server" MaxLength="50" CssClass="AttrTxtFild" TabIndex="1" Width="245px"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="Numbers" runat="server" TargetControlID="txtCT"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtCT" ErrorMessage="Please enter the CT name"
                                    InitialValue="" ValidationGroup="addInventery" ForeColor="Red" EnableClientScript="true"
                                    Text="*">
                         
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Doller $</label>
                                <asp:TextBox ID="txtdoller" MaxLength="50" runat="server" CssClass="AttrTxtFild" TabIndex="2" Width="245px"></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtdoller"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="reqtxtdoller" runat="server" InitialValue="" ValidationGroup="addInventery"
                                    ControlToValidate="txtdoller" ErrorMessage="Please enter the doller" ForeColor="Red"
                                    EnableClientScript="true" Text="*">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <label>
                                    Time</label>
                                <asp:TextBox ID="txttime" MaxLength="50" runat="server" CssClass="AttrTxtFild" TabIndex="3" Width="245px"></asp:TextBox>
                                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txttime"
                                    FilterType="Numbers">
                                </asp:FilteredTextBoxExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldtxttime" runat="server" InitialValue=""
                                    ValidationGroup="addInventery" ControlToValidate="txttime" ErrorMessage="Please enter the time"
                                    ForeColor="Red" EnableClientScript="true" Text="*">
                                </asp:RequiredFieldValidator>
                            </li>
                            <li>
                                <asp:Button ID="addInventeryBtn" ValidationGroup="addInventery" runat="server" CssClass="BlueBtnLe"
                                    Text="Add" TabIndex="4" OnClick="addInventeryBtn_Click" />
                            </li>
                        </ul>
                    </div>
                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:"
                        ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addInventery"
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
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="addInventeryBtn" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
