<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MarketForcast.ascx.cs" Inherits="UserControls_MarketForcast" %>


<style type="text/css">
        .Arrow2
        {
            background-image:url("images/arrow2.png")!important;
            background-repeat:no-repeat;
            width: 100px;
            height: 100px;
            padding: 0.5em;            
            color: #0000;
            position:absolute;
        }
    </style>
    <asp:LinkButton ID="lnkbtnDeleteForcast" runat="server" CssClass="ControlCloseSmall" 
    onclick="lnkbtnDeleteForcast_Click1"></asp:LinkButton>
<div class="Arrow2" id="divForcast" runat="server"></div>
