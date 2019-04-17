<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AddBomProcessUC.ascx.cs" Inherits="UserControls_AddBomProcessUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:ModalPopupExtender ID="ModelBOMProcess" PopupControlID="UpdatePnlBomProcess" TargetControlID="ButtonBomProcess"
    runat="server" BackgroundCssClass="AjaxLoaderOuter" BehaviorID="ModelBOMProcess" CancelControlID="popupBoxClose_addBom">
</asp:ModalPopupExtender>
<asp:Button ID="ButtonBomProcess" runat="server" Style="display: none" />

<asp:UpdatePanel ID="UpdatePnlBomProcess" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
<div class="AttributeWrpPoupProcess" id="popup_box_addBom" runat="server" style="display:none">
        <div class="AttribWrPoupProcess">
            <a href="#" class="CloseBtnP" id="popupBoxClose_addBom"></a>
            <h2>
                Add Node</h2>
            <div class="AttribMid">
                <div id="LeftAtProcess">
                    <ul class="LeftFrm">
                        <li>
                            <label>
                                Node Name</label>
                            <asp:TextBox ID="txtProcessNodeName" runat="server" CssClass="AttrTxtFild" TabIndex="1"
                                Width="245px"></asp:TextBox>
                         <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtProcessNodeName" ErrorMessage="Please enter the node name" InitialValue="" ValidationGroup="addBom" ForeColor="Red" EnableClientScript="true" Text="*">
                         
                         </asp:RequiredFieldValidator>
                        </li>
                           <li>
                                    <label>
                                        Node Type</label>
                                    <asp:DropDownList ID="ddlBomProcessType" runat="server" CssClass="AttrSeFild"  Width="245px">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqddlType" runat="server" InitialValue="0" ValidationGroup="addBom"
                                        ControlToValidate="ddlBomProcessType" ErrorMessage="Please select the type" ForeColor="Red" EnableClientScript="true" Text="*">
                                    </asp:RequiredFieldValidator>
                                </li>
                        <li>
                            <asp:Button ID="addBomProcessBtn" ValidationGroup="addBom"  
                                runat="server" CssClass="BlueBtnLe" Text="Add"
                                 onclick="addProcessBtn_Click"  />
                        </li>
                    </ul>
                </div>
                <asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="Following error occurs:" ShowMessageBox="true" DisplayMode="BulletList" ShowSummary="false" ValidationGroup="addBom" ForeColor="Red" /> 
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
        <asp:AsyncPostBackTrigger ControlID="addBomProcessBtn" EventName="Click" />
    </Triggers>
</asp:UpdatePanel>
