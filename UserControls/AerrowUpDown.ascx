<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AerrowUpDown.ascx.cs" Inherits="UserControls_AerrowUpDown" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--<script language="javascript">

    function fnClickUpdate(sender, e) {
        __doPostBack(sender, e);
    }

</script>--%>
<script type="text/javascript">

    function ZoomRefreshWithClearCntrl() {
        $("#ddlfrmName").prop('selectedIndex', 0);
        $("#ddlToName").prop('selectedIndex', 0);
        $("#ddlfrmActivity").prop('selectedIndex', 0);
        $("#ddltoActivity").prop('selectedIndex', 0);
        $("#lblMsg").hide();
        ZoomRefresh();
    }

</script>
<asp:Button ID="ButtonHide" runat="server" Style="display: none" />
<asp:ModalPopupExtender ID="ModelPopupAerrow" PopupControlID="pnlAerrow" TargetControlID="ButtonHide"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" CancelControlID="imgcloseUPDwn">
</asp:ModalPopupExtender>
<%-- <asp:UpdatePanel ID="UpPnlAddProcess" runat="server" UpdateMode="Conditional">
            <ContentTemplate>--%>
<asp:Panel ID="pnlAerrow" runat="server" Style="z-index: 9999999!important; display:none">
    <div class="AttributeWrpPoupProcess" id="divAerrow" style="z-index: 2 !important">
        <div class="AttribWrPoupProcess" style="margin:0px 0px 0;">
            
           <%-- <asp:ImageButton ID="imgcloseUPDwn" runat="server" CssClass="CloseBtnP" ImageUrl="~/images/close_btn.png" OnClick="imgcloseUPDwn_Click"/>--%>

            <a href="#" class="CloseBtnP" id="imgcloseUPDwn" onclick='return ZoomRefresh()'></a>
            <h2>
                Add  I/O Process</h2>
                  <asp:Label ID="lblMsg" runat="server" style="width: 91%;"></asp:Label>
            <div class="AttribMid">
                <div id="LeftAtProcess">
                    <ul class="LeftFrm">
                        <li>
                            <label>
                               From Process Name</label>
                             <asp:DropDownList ID="ddlfrmName" runat="server" CssClass="AttrSeFild12" OnSelectedIndexChanged="ddlfrmName_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                ValidationGroup="addSubmit" ControlToValidate="ddlfrmName" ErrorMessage="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </li>
                          <li>
                            <label>
                               From Activity Name</label>
                             <asp:DropDownList ID="ddlfrmActivity" runat="server" CssClass="AttrSeFild12">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                ValidationGroup="addSubmit" ControlToValidate="ddlfrmActivity" ErrorMessage="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <label>
                                To Process Name</label>
                            <asp:DropDownList ID="ddlToName" runat="server" CssClass="AttrSeFild12" OnSelectedIndexChanged="ddlToName_OnSelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                ValidationGroup="addSubmit" ControlToValidate="ddlToName" ErrorMessage="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </li>
                          <li>
                            <label>
                                To Activity Name</label>
                            <asp:DropDownList ID="ddltoActivity" runat="server" CssClass="AttrSeFild12">
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                ValidationGroup="addSubmit" ControlToValidate="ddltoActivity" ErrorMessage="*" ForeColor="Red">
                            </asp:RequiredFieldValidator>
                        </li>
                        <li>
                            <asp:Button ID="addBtnHandOff" runat="server" CssClass="BlueBtnLe" Text="Add HandOff"
                                ValidationGroup="addSubmit" CausesValidation="true" OnClick="addBtnHandOff_Click1" />
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
<%--  </ContentTemplate>
          <Triggers>
        <asp:AsyncPostBackTrigger ControlID="addProcessBtn" EventName="Click" />
    </Triggers>
        </asp:UpdatePanel>--%>
