<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="maker.aspx.cs" Inherits="CMBPayment.maker" %>

<%@ Register Src="GPG0000.ascx" TagName="GPG0000" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <h1>批处理上传及检查画面
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

            <asp:Panel ID="pnlFileupload" runat="server">
                <div class="box box-primary">
                    <div class="box-header with-border">
                        <h3 class="box-title">支付文件上传</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->
                    <div class="box-body">
                        <div class="form-group">
                            <label for="exampleInputFile">手动上传文件</label>
                            <asp:FileUpload ID="fileLoad"  runat="server" />
                        </div>
                    </div>
                    <!-- /.box-body -->
                    <asp:Button ID="btnManualUpload" runat="server" Text="手动上传" CssClass="btn btn-primary" OnClick="btnManualUpload_Click" />&nbsp;&nbsp;<asp:Button ID="btnAutoUpload" runat="server" Text="自动上传" CssClass="btn btn-primary" OnClick="btnAutoUpload_Click" />
                </div>
            </asp:Panel>

            <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Always">
                <ContentTemplate>

                    <div class="box-body">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">成功结果</h3>
                            </div>
                            <uc1:GPG0000 ID="GPG00001" runat="server" />
                            <asp:GridView ID="gvSucBatchInfo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvSucBatchInfo_RowDataBound" OnRowCommand="gvSucBatchInfo_RowCommand">
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
                                        <HeaderStyle Width="10%" />
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
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="备注" HeaderStyle-Width="40%" ItemStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                    <div class="box-body">
                        <div class="box box-primary">
                            <div class="box-header with-border">
                                <h3 class="box-title">未成功结果</h3>
                            </div>
                            <uc1:GPG0000 ID="GPG00002" runat="server" />
                            <asp:GridView ID="gvFailBatchInfo" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped" OnRowDataBound="gvFailBatchInfo_RowDataBound" OnRowCommand="gvFailBatchInfo_RowCommand">
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
                                        <HeaderStyle Width="10%" />
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
                                        <HeaderStyle Width="10%" />
                                        <ItemStyle CssClass="text-center" />
                                    </asp:BoundField>
                                </Columns>
                                <Columns>
                                    <asp:TemplateField HeaderText="备注" HeaderStyle-Width="40%" ItemStyle-CssClass="text-left">
                                        <ItemTemplate>
                                            <asp:Label ID="lblComments" runat="server"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
