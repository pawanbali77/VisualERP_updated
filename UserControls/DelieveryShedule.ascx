<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DelieveryShedule.ascx.cs" Inherits="UserControls_DelieveryShedule" %>

<style type="text/css">
        .Delievery
        {
            background-image:url("images/shedule1.png")!important;
            background-repeat:no-repeat;
            width: 157px;
            height: 243px;
            padding: 0.5em;           
            color: #000;
            font: 12px Arial, Helvetica, sans-serif;
            line-height: 375px;
            position:absolute;
        }
    </style>
    <asp:LinkButton ID="lnkbtnDeleteDelievery" runat="server" CssClass="ControlCloseSmall" 
    onclick="lnkbtnDeleteDelievery_Click1"></asp:LinkButton>
<div class="Delievery" id="divDSchedule" runat="server"></div>
