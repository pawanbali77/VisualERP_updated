<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ImageControl.ascx.cs"
    Inherits="UserControls_ImageControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<style type="text/css">
    .ImageControl
    {
        width: 200px!important;
        height: 200px!important;
        padding: 0.5em;
        color: #0000;
        position: absolute;
        float: left;
        background-size: 100% 100%;
        padding-top: 91px;
        padding-left: 68px;
    }
</style>
<asp:LinkButton ID="lnkbtnDeleteImageControl" runat="server" CssClass="ControlCloseSmall"></asp:LinkButton>
<div id="divSelectedControl" runat="server" class="ImageControl">
</div>
