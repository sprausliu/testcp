<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="CMBPayment.login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <title>CMB Payment Tool | Log in</title>
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport" />
    <link rel="stylesheet" href="bootstrap/css/bootstrap.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="dist/css/AdminLTE.css" />
    <link rel="stylesheet" href="dist/css/skins/_all-skins.min.css" />
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
    <div class="login-box">
        <div class="login-logo">
            CMB Payment tool
        </div>
        <!-- /.login-logo -->
        <div class="login-box-body">

            <form id="form1" runat="server">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <p class="login-box-msg">Sign in to start your session</p>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <asp:Label ID="lblError" runat="server" Style="color: #FF0033; font-weight: bold;"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="PWID"></asp:TextBox>
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control" placeholder="Password"></asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8">
                        <div class="checkbox icheck">
                            <label>
                            </label>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-4">
                        <asp:Button ID="btnLogin" runat="server" Text="Sign In" class="btn btn-primary btn-block btn-flat" OnClick="btnLogin_Click" />
                    </div>
                    <!-- /.col -->
                </div>
            </form>
        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->

    <!-- jQuery 2.2.3 -->
    <script src="plugins/jQuery/jquery-2.2.3.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
