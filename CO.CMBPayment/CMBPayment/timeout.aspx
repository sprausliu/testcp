<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="timeout.aspx.cs" Inherits="CMBPayment.timeout" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>CMB Payment Tool | Log in</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />

    <script type="text/javascript">
        function GotoLogin() {
            window.location.replace("<% = Common.ComUtil.GetPageURL(Constant.PagesURL.URL_LOGIN).ToLower() %>");
            return false;
        }
    </script>
</head>
<body class="hold-transition skin-blue layout-top-nav">
    <header class="main-header">
        <nav class="navbar navbar-static-top">
            <div class="container">
                <div class="navbar-header">
                    <a class="navbar-brand">CMB Payment Tool</a>
                </div>
            </div>
        </nav>
    </header>
    <form id="form1" runat="server">
        <!-- Main content -->
        <section class="content">
            <div class="error-page">

                <div class="error-content">
                    <h3><i class="fa fa-warning text-yellow"></i>    session timeout.</h3>
                </div>
                <!-- /.error-content -->
            </div>
            <table style="width: 100%; height: 100%">
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:LinkButton ID="lbtnRelogin" runat="server" Text=">>    login page" PostBackUrl="#" OnClientClick="return GotoLogin();"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </section>
    </form>

    <!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
