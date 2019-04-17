<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HandOffUC.ascx.cs" Inherits="UserControls_HandOffUC" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:ModalPopupExtender ID="ModelPopupHandOff123" PopupControlID="pnlHandOff123" TargetControlID="ButtonHand123"
    runat="server" BackgroundCssClass="AjaxLoaderOuter1" CancelControlID="imgCloseHandOff123">
</asp:ModalPopupExtender>
<asp:Button ID="ButtonHand123" runat="server" Style="display: none" />
<asp:Panel ID="pnlHandOff123" runat="server" Style="display: none; z-index: 9999999!important">
    <div class="AttributeWrp">
        <div class="AttribWrpIn">
            <a href="#" class="CloseBtnP" id="imgCloseHandOff123" onclick='return ZoomRefresh()'></a>            
            <h2>
                Enterprise Manager > Handoff <asp:Label ID="HoHeadlbl" runat="server"></asp:Label></h2>
            <asp:Label ID="lblMsg" runat="server"></asp:Label>
            <div class="AttribMid1">
                <div style="height: 30px; font-weight: bold; text-decoration: underline;">
                    <asp:LinkButton ID="lnkbtnOpenDivHandOff" runat="server" Text="Add HandOff Information"
                        Style="margin: 0px 0px 0px 15px; color: Orange" OnClick="lnkbtnOpenDivHandOff_Click" />
                    <div id="divHandOff" style="display: none">
                        <div class="RightAt1" style="margin: 15px 0px 0px 92px; margin-top: 40px">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <label>
                                            Include On Map</label>
                                    </td>
                                    <td style="padding: -2px 0 0 12px">
                                        <asp:RadioButton ID="radioBtnYes" runat="server" GroupName="Map" CssClass="radioBtn"
                                            Style="padding: 0 13px 0 0px;" Checked="true" Text="Yes" />
                                        <asp:RadioButton ID="radioBtnNo" runat="server" GroupName="Map" CssClass="radioBtn"
                                            Text="No" />
                                    </td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            HO From ID
                                        </label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtFromName" runat="server" MaxLength="500" CssClass="AttrTxtFild"
                                            ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label id="HoNamelbl" runat="server">
                                           </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHoOutputName" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                            ValidationGroup="addHandOff" ControlToValidate="txtHoOutputName" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <label>
                                            HO TO ID</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHotoName" runat="server" CssClass="AttrTxtFild" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td>
                                        <label>
                                            HO Input ID</label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtHoInputId" runat="server" CssClass="AttrTxtFild"></asp:TextBox>
                                          <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtHoInputId"
                                            ValidChars="0123456789">
                                        </asp:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                            ValidationGroup="addHandOff" ControlToValidate="txtHoInputId" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label id="HoTypelbl" runat="server">
                                          </asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtOutputType" runat="server" MaxLength="50" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" InitialValue=""
                                            ValidationGroup="addHandOff" ControlToValidate="txtOutputType" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                    <td>
                                        <label>
                                            HO Input Link</label>
                                    </td>
                                    <td>
                                        <%--<asp:TextBox ID="txtCaliDate" runat="server" CssClass="AttrTxtFild"></asp:TextBox>--%>
                                        <asp:TextBox ID="txtHOInputLink" runat="server" MaxLength="50" CssClass="AttrTxtFild"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                            ValidationGroup="addHandOff" ControlToValidate="txtHOInputLink" ErrorMessage="*"
                                            ForeColor="Red">
                                        </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Button ID="BtnSubmitHandOff" runat="server" ValidationGroup="addHandOff" CssClass="BlueBtnLe"
                                            Text="Submit" OnClick="BtnSubmitHandOff_Click" />
                                    </td>
                                    <td style="text-align: center">
                                        <button type="reset" value="Reset" class="BlueBtnLe">
                                            Reset</button>
                                        <%--   <asp:Button ID="ResetBtn" runat="server" CssClass="BlueBtnLe" Text="Reset" OnClick="ResetBtn_Click" />--%>
                                    </td>
                                    <td style="text-align: left">
                                        <asp:Button ID="bckBtnHandOff" runat="server" CssClass="BlueBtnLe" Text="Back" OnClick="bckBtnHandOff_Click" />
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
                <div class="RightAt2" id="DivGrid">
                    <div>
                    </div>
                    <asp:GridView ID="gridHandOff" runat="server" AlternatingRowStyle-CssClass="GrayBg"
                        AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="gridHandOff_PageIndexChanging"
                        OnRowCreated="gridHandOff_RowCreated" OnRowDeleting="gridHandOff_RowDeleting"
                        OnRowEditing="gridHandOff_RowEditing" OnSelectedIndexChanging="gridHandOff_SelectedIndexChanging"
                        OnSorting="gridHandOff_Sorting" OnRowDataBound="gridHandOff_OnRowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="S No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Include on Map" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%--   <%#DataBinder.Eval(Container.DataItem, "IncludeOnMap")%>--%>
                                    <%#Convert.ToString(Eval("IncludeonMap")) == "True" ? "Yes" : "No"%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HO Output Type" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "HOOutputType")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HO Output Name" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "HOOutputName")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HO Input ID" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "HOInputID")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="HO Input Link" ItemStyle-CssClass="GrayBg" HeaderStyle-ForeColor="#43494F">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "HOInputLink")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Action" ItemStyle-CssClass="GrayBg">
                                <ItemTemplate>
                                    <asp:Literal ID="litHOID" runat="server" Visible="false" Text='<%#Eval("HOID") %>' />
                                    <asp:ImageButton ID="editBtnHandOff" runat="server" CommandName="Edit" CommandArgument='<%#Eval("HOID")%>'
                                        ImageUrl="~/images/Edit.png" AlternateText="Edit" ToolTip="Edit" />
                                    <asp:ImageButton ID="deleteBtnHandOff" runat="server" CommandName="Delete" AlternateText="Delete"
                                        ToolTip="Delete" CommandArgument='<%#Eval("HOID") %>' ImageUrl="~/images/delete.png" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <div>
                                &nbsp;</div>
                            <div class="msgSucess12">
                                <p>
                                    No records found !
                                </p>
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
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
