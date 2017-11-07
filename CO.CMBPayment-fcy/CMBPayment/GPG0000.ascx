<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GPG0000.ascx.cs" Inherits="CMBPayment.GPG0000" %>






<div class="row">
    <div class="col-sm-5">
        <table class="pagination" style="float: left;" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td nowrap>
                    <asp:Label ID="lblSumTitle" runat="server" Text="<%$ Resources:GlobalResource,PAGING_SUMTITLE  %>"></asp:Label>
                </td>
                <td style="text-align: left; width: 10px;">&nbsp;</td>
                <td>
                    <asp:Label ID="lblSumCNT" runat="server" Text="0"></asp:Label></td>
                <td nowrap>
                    <asp:Label ID="lblUnit" runat="server" Text="<%$ Resources:GlobalResource,PAGING_UNITNAME  %>"></asp:Label></td>
            </tr>
        </table>
    </div>
    <div class="col-sm-7">
        <div class="dataTables_paginate">
            <table style="float: right;" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td class="pagination" style="text-align: center;">
                        <asp:LinkButton ID="lnkStart" runat="server" CssClass="GPG0000_LNK" OnClick="lnkStart_Click">
                            <span class="GPG0000_SPAN ui-icon ui-icon-seek-start">首页</span>
                        </asp:LinkButton>
                    </td>
                    <td class="pagination" style="text-align: center;">
                        <asp:LinkButton ID="lnkPrev" runat="server" CssClass="GPG0000_LNK" OnClick="lnkPrev_Click">
                            <span class="GPG0000_SPAN ui-icon ui-icon-seek-prev">前</span>
                        </asp:LinkButton>
                    </td>
                    <td style="text-align: left; width: 10px;">&nbsp;</td>
                    <td style="text-align: right; width: 100px">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:DropDownList ID="ddlPages" runat="server" Width="60" CssClass="GPG0000_PAGING" AutoPostBack="True" OnSelectedIndexChanged="ddlPages_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>&nbsp;/&nbsp;</td>
                                <td nowrap>
                                    <asp:Label ID="lblPageCount" runat="server" Text="0"></asp:Label></td>
                                <td nowrap>&nbsp;<asp:Label ID="lblPageUnit" runat="server" Text="<%$ Resources:GlobalResource,PAGING_PAGEUNITNAME  %>"></asp:Label></td>
                            </tr>
                        </table>
                    </td>
                    <td style="text-align: left; width: 5px;">&nbsp;</td>
                    <td class="pagination" style="text-align: center;">
                        <asp:LinkButton ID="lnlNext" runat="server" CssClass="GPG0000_LNK" OnClick="lnlNext_Click">
                            <span class="GPG0000_SPAN ui-icon ui-icon-seek-next">后</span>
                        </asp:LinkButton>
                    </td>
                    <td class="pagination" style="text-align: center;">
                        <asp:LinkButton ID="lnkEnd" runat="server" CssClass="GPG0000_LNK" OnClick="lnkEnd_Click">
                            <span class="GPG0000_SPAN ui-icon ui-icon-seek-end">尾页</span>
                        </asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>

