<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ArrowControl.ascx.cs"
    Inherits="UserControls_ArrowControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .ArrowControl
    {
        width: 50px;
        height: 50px;
        padding: 0.5em;
        color: #0000;
        position: absolute;
        float: left;
        background-size: 100% 100%;
        padding-top: 91px;
        padding-left: 68px;
    }
</style>
    <asp:LinkButton ID="lnkbtnDeleteArrowControl" runat="server" CssClass="ControlCloseSmall"></asp:LinkButton>
<div id="divSelectedArrow" runat="server" class="ArrowControl">
</div>

