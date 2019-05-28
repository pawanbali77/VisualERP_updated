<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HelpVideos.aspx.cs" Inherits="HelpVideos" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        td {
            text-align: center;
            border: 1px solid black;
        }
    </style>
</head>
<body>
    <table style="width: 100%; border: 1px solid black;">
        <tr style="background: black; color: white;">
            <th>Video's Name</th>
            <th>Video</th>
        </tr>
        <tr>
            <td>Registration</td>
            <td>
                <video width="320" height="240" controls="controls">
                    <source src="HelpVideos/Registration.mp4" type="video/mp4" />
                </video>
            </td>
        </tr>
        <tr>
            
            <td>Forget Password
            </td>
            <td>
                <video width="320" height="240" controls="controls" title="Forget Password">
                    <source src="HelpVideos/ForgetPassword.mp4" type="video/mp4" />
                </video>
            </td>
        </tr>
        <tr>
            <td>User's Registration</td>
            <td>
                <video width="320" height="240" controls="controls">
                    <source src="HelpVideos/UserRegistration.mp4" type="video/mp4" />
                </video>
            </td>
        </tr>
    </table>
    <div>
    </div>
</body>
</html>
