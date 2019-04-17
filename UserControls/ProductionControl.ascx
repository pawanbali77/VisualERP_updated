<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductionControl.ascx.cs" Inherits="UserControls_ProductionControl" %>

<style type="text/css">
        .ProductionC
        {
            background-image:url("images/value_steam.png")!important;
            background-repeat:no-repeat;
            width: 120px;
            height: 84px;
            position:absolute;
        }
        .ProductionC h2
        {
            font: bold 11px Arial, Helvetica, sans-serif;
            color: #000;
            text-align: center;
            padding-top: 2px;
        }
        .ProductionC h3
        {
            font: bold 13px Arial, Helvetica, sans-serif;
            color: #000;
            text-align: center;
            padding-top: 15px;
        }
    </style>
    <asp:LinkButton ID="lnkbtnDeleteProductionC" runat="server" CssClass="ControlCloseSmall"  
    onclick="lnkbtnDeleteProductionC_Click1"></asp:LinkButton>
<div class="ProductionC" id="divValueStream" runat="server">
<h2>Value Stream</h2>
<h3>Production <br /> Control</h3>
</div>
