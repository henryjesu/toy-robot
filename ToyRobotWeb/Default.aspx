<%@ Page Language="C#" Inherits="ToyRobotWeb.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
	<title>Default</title>
</head>
<body>
	<form id="form1" runat="server">
		<table>
            <tr>
                <td colspan="3">
                    <asp:Label ID="LabelAPIURL" runat="server" Text="Robot API URL"></asp:Label>
                    <asp:TextBox ID="TextBoxAPIURL" runat="server" Text="https://localhost:44346/"></asp:TextBox>
                </td>
            </tr>
        <tr>
            <td style="padding:5px">
                <asp:FileUpload ID="FileUploadInput" AllowMultiple="false"  runat="server" />
            </td>
            <td style="padding:5px">
                <asp:Button ID="ButtonUpload" runat="server" Text="Upload" OnClick="ButtonUpload_Click" />
            </td>
           <td style="padding:5px">
                <asp:Button ID="ButtonSubmit" OnClick="ButtonSubmit_Click" runat="server" Text="Submit" />
            </td>
        </tr>
    </table>
    <table >
        <tr>
            <td style="padding:5px"><asp:Label ID="LabelInput" runat="server" Text="Input"></asp:Label></td>
             
            <td style="padding:5px"><asp:Label ID="LabelOutput" runat="server" Text="Output"></asp:Label></td>
        </tr>
        <tr>
            <td style="padding:5px">
                <asp:TextBox ID="TextBoxInput" TextMode="MultiLine" Width="250px" Height="500px" runat="server"></asp:TextBox>
            </td>
            <td style="padding:5px">
                <asp:TextBox ID="TextBoxOutput" ReadOnly="true" Width="250px" Height="500px" TextMode="MultiLine" BackColor="Black" ForeColor="WhiteSmoke" runat="server"></asp:TextBox>
            </td>
        </tr>
    </table>
	</form>
</body>
</html>
