<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupAddProcess.ascx.cs"
    Inherits="UserControls_ModelPopupAddProcess" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<script language="javascript">

    function fnClickUpdate(sender, e) {
        __doPostBack(sender, e);
    }

</script>
<asp:Button ID="Button2" runat="server" Style="display: none" />
<asp:ModalPopupExtender ID="ModelPopupProcess" PopupControlID="pnlProcessAdd" TargetControlID="Button2"
    runat="server" BackgroundCssClass="AjaxLoaderOuter" CancelControlID="imgClosepr">
</asp:ModalPopupExtender>
<%-- <asp:UpdatePanel ID="UpPnlAddProcess" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
<asp:Panel ID="pnlProcessAdd" runat="server" Style="display: none; z-index: 9999999!important">
    <div class="AttributeWrpPoupProcess" id="divAttr" style="z-index: 5000 !important">
        <div class="AttribWrPoupProcess">
            <a href="#" class="CloseBtnP" id="imgClosepr"></a>
            <h2>
                Add Node</h2>
            <div class="AttribMid">
                <div id="LeftAtProcess">
                    <ul class="LeftFrm">
                        <li>
                            <label>
                                Node Name</label>
                            <asp:TextBox ID="txtProcessName" MaxLength="150" runat="server" CssClass="AttrTxtFild"
                                TabIndex="1" Width="245px"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="reqtxtProcessName" runat="server" InitialValue=""
                                ValidationGroup="addValue" ControlToValidate="txtProcessName" ErrorMessage="*"
                                ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                Type Name</label>
                            <asp:DropDownList ID="ddlType" runat="server" CssClass="AttrSeFild">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                ValidationGroup="addValue" ControlToValidate="ddlType" ErrorMessage="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <asp:Button ID="addProcessBtn" runat="server" CssClass="BlueBtnLe" Text="Add Process"
                                ValidationGroup="addValue" CausesValidation="true" OnClick="addProcessBtn_Click1" />
                        </li>
                    </ul>
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
</asp:Panel>
<%--  </ContentTemplate>
          <Triggers>
        <asp:AsyncPostBackTrigger ControlID="addProcessBtn" EventName="Click" />
    </Triggers>
        </asp:UpdatePanel>--%>