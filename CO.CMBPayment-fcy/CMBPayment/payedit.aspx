<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="payedit.aspx.cs" Inherits="CMBPayment.payedit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h1>支付信息补充画面
                    <small></small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i>home</a></li>
        <li class="active">Here</li>
    </ol>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="box">
        <div class="box-header">
            <asp:Label ID="lblError" runat="server" Style="color: #FF0033; font-weight: bold;"></asp:Label>
            <br />
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">交易信息：</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <asp:Label ID="lblBRDNBR" runat="server" Text="收方行号:"></asp:Label>
                        <asp:TextBox ID="txtBRDNBR" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblTRSCD1" runat="server" Text="交易编码:">
                        </asp:Label>
                        <asp:TextBox ID="txtTRSCD1" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="lblCNTNBR" runat="server" Text="合同号:">
                        </asp:Label>
                        <asp:TextBox ID="txtCNBNBR" runat="server"  CssClass="form-control"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <asp:Label ID="Label1" runat="server" Text="附言:"></asp:Label>
                        <asp:TextBox ID="txtUsage" CssClass="form-control" runat="server" Text='<%#Eval("NUSAGE") %>' MaxLength="30" TextMode="MultiLine" Rows="3" Width="257px"></asp:TextBox>
                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:GlobalResource,INFO_BUTTON_UPDATE%>" OnClick="btnSave_Click" />&nbsp;&nbsp;
                    <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-primary" Text="<%$ Resources:GlobalResource,INFO_BUTTON_RETURN%>" OnClick="btnReturn_Click" />
                </div>
            </div>
        </div>


    </div>

</asp:Content>
