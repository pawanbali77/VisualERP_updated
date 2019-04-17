<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModelPopupActivity.ascx.cs"
    Inherits="UserControls_ModelPopupActivity" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="ModelPopupActivity" PopupControlID="UpdatePnlActivity"
    TargetControlID="Button8" runat="server" BackgroundCssClass="AjaxLoaderOuter1"
    BehaviorID="ModelPopupActivity" CancelControlID="imgCloseActivity">
</asp:ModalPopupExtender>
<asp:Button ID="Button8" runat="server" Style="display: none" />
<asp:UpdatePanel ID="UpdatePnlActivity" runat="server" UpdateMode="Conditional" Style="display: none;
    z-index: 99999!important;">
    <ContentTemplate>
        <div class="AttributeWrpPoupInventery" id="popup_box_Activity" runat="server">
            <div class="AttribWrPoupInventery" style="width: 300px; float: left; border: 1px solid #ccc;
                background: #fff; position: relative; margin: 40px 40px 0;">
                <%--<a href="#" class="CloseBtnPInventery" id="imgCloseActivity" onclick="window.location.href = 'ProcessManager.aspx'"></a>--%>
                <a href="#" class="CloseBtnPInventery" id="imgCloseActivity" onclick="window.location.reload();"></a>
                <h2>
                    Update Activity Name</h2>
                <asp:Label ID="lblMsg" runat="server" style="padding:6px 0px 5px 30px!important; width:100%!important"></asp:Label>
                <div class="AttribMid">
                    <div id="LeftAtInventery">
                        <ul class="LeftFrm">
                            <li>
                                <label>
                                    Activity Name</label>
                                <asp:TextBox ID="txtActivityName" runat="server" MaxLength="50" CssClass="AttrTxtFild"
                                    TabIndex="1" Width="245px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtActivityName"
                                    ErrorMessage="Please enter activity name" ValidationGroup="addActivity" ForeColor="Red"
                                    EnableClientScript="true" Text="*">                         
                                </asp:RequiredFieldValidator>                                
                             <%--   <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtActivityName"
                                       FilterType="LowercaseLetters, UppercaseLetters" ValidChars="" BehaviorID="txtWeight">
                                    </asp:FilteredTextBoxExtender>--%>
                            </li>
                            <li>
                                <asp:Button ID="btnUpdateActivity" ValidationGroup="addActivity" runat="server"
                                    CssClass="BlueBtnLe" Text="Update" TabIndex="4" OnClick="btnUpdateActivity_Click" />
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
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnUpdateActivity" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
