<%@ Page Title="" Language="C#" MasterPageFile="~/MainMaster.master" AutoEventWireup="true" CodeFile="Player.aspx.cs" Inherits="Player" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="margin-top: 70px; margin-bottom: 20px; text-align: center;">
        <%--<h1>Watch Videos</h1>--%>
    </div>
       <div align="center">
            <div style="width: 60%; height: 500px; margin: 0 auto;">
            <a class="player" runat="server" id="triggerclick" style="height: 300px;"></a>
        </div>

        <script type="text/javascript" src="Scripts/jquery.js"></script>
        <script src="FlowPlayer/flowplayer-3.2.12.min.js" type="text/javascript"></script>
        <script type="text/javascript">
            $(document).ready(function () {
                var obj = $("#triggerclick").attr("href");
                $("#triggerclick").trigger("click");

                flowplayer("a.player", "FlowPlayer/flowplayer-3.2.16.swf", {
                    plugins: {
                        pseudo: { url: "FlowPlayer/flowplayer.pseudostreaming-3.2.12.swf" }
                    },
                    clip: { provider: 'pseudo', autoPlay: false },
                });

            });
        </script>
       </div>
</asp:Content>


