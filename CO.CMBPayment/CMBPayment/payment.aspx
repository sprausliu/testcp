<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="payment.aspx.cs" Inherits="CMBPayment.payment" %>

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
                    <asp:Button ID="btnDelete" runat="server" Style="float: right;" CssClass="btn btn-success" Text="批量删除" OnClick="btnDelete_Click" OnClientClick="javascript:return fn.DeleteConfirm();" />
                </div>
                <uc1:GPG0000 ID="GPG00001" runat="server" />
                <asp:GridView ID="gvPaymentInfo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvPaymentInfo_RowDataBound" OnRowCommand="gvPaymentInfo_RowCommand">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle Width="2%" />
                            <ItemStyle CssClass="text-left" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelected" runat="server" Enabled="false" CssClass="selectitem" />
                                <asp:HiddenField ID="hidPaymentId" runat="server" Value='<%#Eval("YURREF") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="付款参考">
                            <HeaderStyle Width="8%" />
                            <ItemStyle CssClass="text-left" />
                            <ItemTemplate>
                                <asp:Label ID="lblEmptyData" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblPaymentId" runat="server" Text='<%#Eval("YURREF") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="status" HeaderText="状态">
                            <HeaderStyle Width="7%" />
                            <ItemStyle CssClass="text-center" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="TRSAMT" HeaderText="金额">
                            <HeaderStyle Width="8%" />
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
                            <HeaderStyle Width="5%" />
                            <ItemStyle CssClass="text-center" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="OPRDAT" HeaderText="付款日">
                            <HeaderStyle Width="5%" />
                            <ItemStyle CssClass="text-center" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="用途">
                            <HeaderStyle Width="10%" />
                            <ItemStyle CssClass="text-left" />
                            <ItemTemplate>
                                <%--<asp:TextBox ID="txtUsage" runat="server" Text='<%#Eval("NUSAGE") %>' MaxLength="30" TextMode="MultiLine" Rows="3" Enabled="false"></asp:TextBox>--%>
                                <asp:Label ID="lblNUSAGE" runat="server" Text='<%#Eval("NUSAGE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <Columns>
                        <asp:BoundField DataField="comment" HeaderText="错误信息">
                            <HeaderStyle Width="10%" />
                            <ItemStyle CssClass="text-left" />
                        </asp:BoundField>
                    </Columns>
                    <Columns>
                        <asp:TemplateField HeaderText="补充信息">
                            <HeaderStyle Width="5%" />
                            <ItemStyle CssClass="text-left" />
                            <ItemTemplate>
                                <asp:Button CssClass="btn btn-block btn-success btn-sm" CommandName="Edit" ID="btnEdit" runat="server" Text="编辑" Visible="false"></asp:Button>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <!-- /.box-body -->
    </div>
    <!-- /.box -->


    <script type="text/javascript">
        $.fn.DeleteConfirm = function () {
            if (!IsHaveChecked()) {
                $.fn.AlertMsg("请选择要删除的支付数据。");
                return false;
            }
            return confirm("确定要删除选中的数据吗？");
        };

        function IsHaveChecked() {
            var itemClass = ".selectitem input";
            var haveChecked = false;
            $(itemClass).each(function () {
                if ($(this).attr('checked') == 'checked') {
                    haveChecked = true;
                    return;
                }
            });
            return haveChecked;
        }
    </script>
</asp:Content>
