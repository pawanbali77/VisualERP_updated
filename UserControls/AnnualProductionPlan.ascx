<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AnnualProductionPlan.ascx.cs" Inherits="UserControls_AnnualProductionPlan" %>

<style type="text/css">
        .Annual
        {
            background-image:url("images/arrow1.png")!important;
            background-repeat:no-repeat;
            width: 87px;
            height: 67px;
            padding: 0.5em;            
            color: #0000;
            position:absolute;
        }
    </style>

    <asp:LinkButton ID="lnkbtnDeleteAnnual" runat="server" CssClass="ControlCloseSmall" 
    onclick="lnkbtnDeleteAnnual_Click1"></asp:LinkButton>
<div class="Annual" id="divArrow" runat="server"></div>
