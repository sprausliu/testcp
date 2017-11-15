<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="batch.aspx.cs" Inherits="CMBPayment.batch" %>

<%@ Register Src="GPG0000.ascx" TagName="GPG0000" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h1>批处理列表画面
                    <small></small>
    </h1>
    <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i>home</a></li>
        <li class="active">Here</li>
    </ol>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="box">
                <div class="box-header">
                    <asp:Label ID="lblError" runat="server" Style="color: #FF0033; font-weight: bold;"></asp:Label>
                    <br />

                    <div class="box box-success">
                        <div class="box-header with-border">
                            <h3 class="box-title">查询条件</h3>
                        </div>
                        <div class="box-body">
                            <div class="form-group">
                                <table>
                                    <tr>
                                        <th class="left" style="width: 70px;">
                                            <asp:Label ID="lblBsType" runat="server" Text="状态:"></asp:Label>
                                        </th>
                                        <td class="left">
                                            <asp:DropDownList ID="ddlStatusType" runat="server" CssClass="form-control">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">
                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="<%$ Resources:GlobalResource,INFO_BUTTON_SEARCH%>" OnClick="btnSearch_Click" />
                            <table style="float: right;">
                                <tr>
                                    <th class="text-left" style="width: 120px;">
                                        <asp:Label ID="Label1" runat="server" Text="选择前置机:"></asp:Label>
                                    </th>
                                    <td class="text-left" style="width: 300px;">
                                        <asp:DropDownList runat="server" ID="ddlQZJ" CssClass="select2"></asp:DropDownList>
                                        <%--<asp:TextBox ID="txtIpAddr" runat="server" MaxLength="15" Width="120px" Text=""></asp:TextBox>--%>
                                        <asp:TextBox ID="txtPort" runat="server" MaxLength="5" Width="40px" Text="8080" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-left" style="width: 120px;">
                                        <asp:Label ID="Label2" runat="server" Text="查询日期:" Visible="true"></asp:Label>
                                    </th>
                                    <td class="text-left">
                                        <asp:TextBox ID="txtDayStart" runat="server" MaxLength="8" Width="70px" Text=""  Visible="true"></asp:TextBox>&nbsp;&nbsp;<asp:TextBox ID="txtDayEnd" runat="server" MaxLength="8" Width="70px" Text="" Visible="true"></asp:TextBox>&nbsp;&nbsp;
                                        <asp:Button ID="btnPayResult" Style="width: 110px;" runat="server" CssClass="btn btn-success" Text="查询支付结果" OnClick="btnPayResult_Click" Visible="true" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>


                <div class="box-body">
                    <div class="box box-primary">
                        <div class="box-header with-border">
                            <h3 class="box-title">查询结果</h3>
                        </div>
                        <uc1:GPG0000 ID="GPG00001" runat="server" />
                        <asp:GridView ID="gvBatchInfo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvBatchInfo_RowDataBound" OnRowCommand="gvBatchInfo_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="批处理参考">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle CssClass="text-left" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblEmptyData" runat="server" Visible="false"></asp:Label>
                                        <asp:LinkButton ID="lkBatchId" runat="server" Text='<%#Eval("batch_id") %>' CommandName="id"></asp:LinkButton>
                                        <asp:Label ID="lblUpdateDateKey" runat="server" Visible="false"></asp:Label>
                                        <asp:HiddenField ID="hidUpdateDateKey" runat="server" Value='<%#Eval("update_time") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="pay_cnt" HeaderText="指令数">
                                    <HeaderStyle Width="6%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="status" HeaderText="状态">
                                    <HeaderStyle Width="10%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="ttl_amt" HeaderText="合计金额">
                                    <HeaderStyle Width="12%" />
                                    <ItemStyle CssClass="text-right" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="maker_id" HeaderText="批处理执行人">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="maker_time" HeaderText="批处理日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="授权人1" HeaderStyle-Width="7%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprId1" runat="server" Text='<%#Eval("appr1_id") %>' Visible="false"></asp:Label>
                                        <asp:Button CssClass="btn btn-block btn-success btn-sm" CommandName="Appr1" ID="btnAppr1" runat="server" Text="授权1" Visible="false" OnClientClick="javascript:return fn.ApprConfirm();"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="appr1_time" HeaderText="授权人1处理日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="授权人2" HeaderStyle-Width="7%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblApprId2" runat="server" Text='<%#Eval("appr2_id") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblOver" runat="server" Text='超上限' Visible="false"></asp:Label>
                                        <asp:Button CssClass="btn btn-block btn-success btn-sm" CommandName="Appr2" ID="btnAppr2" runat="server" Text="授权2" Visible="false" OnClientClick="javascript:return fn.ApprConfirm();"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:BoundField DataField="appr2_time" HeaderText="授权人2处理日期">
                                    <HeaderStyle Width="8%" />
                                    <ItemStyle CssClass="text-center" />
                                </asp:BoundField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="拒绝" HeaderStyle-Width="7%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRejcId" runat="server" Text='<%#Eval("rejc_id") %>' Visible="false"></asp:Label>
                                        <asp:Button CssClass="btn btn-block btn-warning btn-sm" CommandName="Rejt" ID="btnRejt" runat="server" Text="拒绝" Visible="false" OnClientClick="javascript:return fn.RectConfirm();"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <Columns>
                                <asp:TemplateField HeaderText="支付" HeaderStyle-Width="7%" ItemStyle-CssClass="text-center">
                                    <ItemTemplate>
                                        <asp:Button CssClass="btn btn-block btn-info btn-sm" CommandName="Payment" ID="btnPayment" runat="server" Text="支付" Enabled="false"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
                <!-- /.box-body -->
            </div>
            <!-- /.box -->
        </ContentTemplate>
    </asp:UpdatePanel>


    <script type="text/javascript">
        $.fn.ApprConfirm = function () {
            return confirm("确定要对当前的数据授权吗？");
        };
        $.fn.RectConfirm = function () {
            return confirm("确定拒绝当前的数据吗？");
        };
    </script>
</asp:Content>
