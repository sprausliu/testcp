<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="payments.aspx.cs" Inherits="CMBPayment.payments" %>

<%@ Register Src="GPG0000.ascx" TagName="GPG0000" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h1>支付列表画面
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

            <div class="box box-success">
                <div class="box-header with-border">
                    <h3 class="box-title">批处理信息</h3>
                </div>
                <div class="box-body">
                    <div class="form-group">
                        <table>
                            <tr>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label1" runat="server" Text="批处理参考:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblBatchId" runat="server"></asp:Label>
                                </td>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label2" runat="server" Text="状态:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label3" runat="server" Text="批处理执行人:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblMakerId" runat="server"></asp:Label>
                                </td>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label4" runat="server" Text="批处理日期:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblMakerTime" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label5" runat="server" Text="授权人:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblAppr1Id" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblAppr2Id" runat="server"></asp:Label>
                                </td>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label7" runat="server" Text="授权日期:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblAppr1Time" runat="server"></asp:Label><br />
                                    <asp:Label ID="lblAppr2Time" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label6" runat="server" Text="金额:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblTtlAmt" runat="server"></asp:Label>
                                </td>
                                <th class="tesx-left" style="width: 15%;">
                                    <asp:Label ID="Label9" runat="server" Text="指令数:"></asp:Label>
                                </th>
                                <td class="tesx-left" style="width: 35%;">
                                    <asp:Label ID="lblPayCnt" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:Button ID="btnReturn" runat="server" CssClass="btn btn-success" Text="<%$ Resources:GlobalResource,INFO_BUTTON_RETURN%>" OnClick="btnReturn_Click" />
                </div>
            </div>
        </div>

        <div class="box-body">
            <div class="box box-primary">
                <div class="box-header with-border">
                    <h3 class="box-title">支付列表</h3>
                </div>
                <uc1:GPG0000 ID="GPG00001" runat="server" />
                <asp:GridView ID="gvPaymentInfo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvPaymentInfo_RowDataBound" OnRowCommand="gvPaymentInfo_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="付款参考">
                            <HeaderStyle Width="10%" />
                            <ItemStyle CssClass="text-left" />
                            <ItemTemplate>
                                <asp:Label ID="lblEmptyData" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPaymentId" runat="server" Text='<%#Eval("YURREF") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="status" HeaderText="状态">
                            <HeaderStyle Width="10%" />
                            <ItemStyle CssClass="text-center" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="TRSAMT" HeaderText="金额">
                            <HeaderStyle Width="10%" />
                            <ItemStyle CssClass="text-right" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="收款人账号、名称" HeaderStyle-Width="20%" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblCrtAcc" runat="server" Text='<%#Eval("CRTACC") %>'></asp:Label><br />
                                <asp:Label ID="lblCtrNam" runat="server" Text='<%#Eval("CRTNAM") %>'></asp:Label>
                                <asp:Label ID="lblLrvean" runat="server" Text='<%#Eval("LRVEAN") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="收方联行号、开户行" HeaderStyle-Width="20%" ItemStyle-CssClass="text-left">
                            <ItemTemplate>
                                <asp:Label ID="lblBrdnbr" runat="server" Text='<%#Eval("BRDNBR") %>'></asp:Label><br />
                                <asp:Label ID="lblCrtbnk" runat="server" Text='<%#Eval("CRTBNK") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="EPTDAT" HeaderText="借记日">
                            <HeaderStyle Width="8%" />
                            <ItemStyle CssClass="text-center" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="OPRDAT" HeaderText="付款日">
                            <HeaderStyle Width="8%" />
                            <ItemStyle CssClass="text-center" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="comment" HeaderText="备注">
                            <HeaderStyle Width="14%" />
                            <ItemStyle CssClass="text-left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->

</asp:Content>
