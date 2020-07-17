<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>

<%@ Register Src="~/Usercontrol/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel_View">
        <ContentTemplate>
            <style>
                @media(min-width: 576px) {
                    .modal-dialog {
                        max-width: 800px !important;
                        margin: 1.75rem auto;
                    }
                }
            </style>
            <div class="content-body">
                <!-- Basic Tables start -->
                <div class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Bàn làm việc</h4>
                                <a class="heading-elements-toggle"><i class="la la-ellipsis-v font-medium-3"></i></a>
                                <div class="heading-elements">
                                    <ul class="list-inline mb-0">
                                        <li><a data-action="collapse"><i class="ft-minus"></i></a></li>
                                        <li><a data-action="reload"><i class="ft-rotate-cw"></i></a></li>
                                        <li><a data-action="expand"><i class="ft-maximize"></i></a></li>
                                        <li><a data-action="close"><i class="ft-x"></i></a></li>
                                    </ul>
                                </div>
                            </div>

                            <div class="card-content collapse show">
                                <div class="card-body">
                                    <asp:Button Text="Thêm mới" CssClass="btn btn-primary" ID="btn_NewObject" OnClick="btn_NewObject_Click" Visible="false" runat="server" />
                                    <h2>Phiếu chuyển kho</h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Tên phiếu</th>
                                                    <th>Tên kho</th>
                                                    <th>Tên hàng hóa</th>
                                                    <th>Số lượng</th>
                                                    <th>Kho nhận</th>
                                                    <th>Trạng thái</th>
                                                    <th>Ngày lập</th>
                                                    <th>Người lập</th>
                                                    <th>Thao tác</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_Data_List">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th scope="row"><%#Eval("STT") %></th>
                                                            <td><%#Eval("TENPHIEU") %></td>
                                                            <td><%#Eval("TENKHO") %></td>
                                                            <td><%#Eval("TENHANG") %></td>
                                                            <td><%# Eval("SOLUONG") %></td>
                                                            <td><%# Eval("TENKHONHAN") %></td>
                                                            <td><%# Eval("TRANGTHAI") %></td>
                                                            <td><%# Eval("NGAYTAO") %></td>
                                                            <td><%# Eval("TENTHUKHO") %></td>
                                                            <td>
                                                                <asp:LinkButton ToolTip="Phiếu chuyển kho" CssClass="col-md-6"
                                                                    ForeColor="DarkBlue" Text='<i class="la la-check"></i>'
                                                                    CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton_PhieuChuyen" OnClick="LinkButton_PhieuChuyen_Click" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                        </table>
                                    </div>
                                    <h2>Phiếu nhập kho</h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Tên phiếu</th>
                                                    <th>Tên kho</th>
                                                    <th>Tên hàng hóa</th>
                                                    <th>Số lượng</th>
                                                    <%--<th>Kho nhận</th>--%>
                                                    <th>Trạng thái</th>
                                                    <th>Ngày lập</th>
                                                    <th>Người lập</th>
                                                    <th>Thao tác</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_Data_List_NhapKho">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th scope="row"><%#Eval("STT") %></th>
                                                            <td><%#Eval("TENPHIEU") %></td>
                                                            <td><%#Eval("TENKHO") %></td>
                                                            <td><%#Eval("TENHANG") %></td>
                                                            <td><%# Eval("SOLUONG") %></td>
                                                            <%--<td><%# Eval("TENKHONHAN") %></td>--%>
                                                            <td><%# Eval("TRANGTHAI") %></td>
                                                            <td><%# Eval("NGAYTAO") %></td>
                                                            <td><%# Eval("TENTHUKHO") %></td>
                                                            <td>
                                                                <asp:LinkButton ToolTip="Xác nhận phiếu nhập" CssClass="col-md-6"
                                                                    ForeColor="DarkBlue" Text='<i class="la la-check"></i>'
                                                                    CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton_PhieuNhap" OnClick="LinkButton_PhieuNhap_Click" /></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                        </table>
                                    </div>
                                    <h2>Phiếu xuất kho</h2>
                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Tên phiếu</th>
                                                    <th>Tên kho</th>
                                                    <th>Tên hàng hóa</th>
                                                    <th>Số lượng</th>
                                                    <%--<th>Kho nhận</th>--%>
                                                    <th>Trạng thái</th>
                                                    <th>Ngày lập</th>
                                                    <th>Người lập</th>
                                                    <th>Thao tác</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_Data_List_XuatKho">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th scope="row"><%#Eval("STT") %></th>
                                                            <td><%#Eval("TENPHIEU") %></td>
                                                            <td><%#Eval("TENKHO") %></td>
                                                            <td><%#Eval("TENHANG") %></td>
                                                            <td><%# Eval("SOLUONG") %></td>
                                                            <%--<td><%# Eval("TENKHONHAN") %></td>--%>
                                                            <td><%# Eval("TRANGTHAI") %></td>
                                                            <td><%# Eval("NGAYTAO") %></td>
                                                            <td><%# Eval("TENTHUKHO") %></td>
                                                            <td>
                                                                <asp:LinkButton ToolTip="Xác nhận phiếu xuất" CssClass="col-md-6"
                                                                    ForeColor="DarkBlue" Text='<i class="la la-check"></i>'
                                                                    CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton_PhieuXuât" OnClick="LinkButton_PhieuXuât_Click" /></td>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                        </table>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="NewObjectModal" tabindex="-1" role="dialog"
        data-backdrop="static" data-keyboard="false"
        aria-labelledby="NewObjectModalLabel" aria-hidden="true">
        <div class="modal-dialog  col-md-12" role="document">
            <asp:UpdatePanel ID="UpdatePanel_Object" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h5 class="modal-title" id="NewObjectModalLabel">Thêm mới</h5>
                        </div>
                        <div class="modal-body" style="display: flex">
                        </div>
                        <div class="modal-footer" style="display: inline-block">
                            <div class="col-md-12">

                                <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <uc1:ucMessage runat="server" ID="ucMessage" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-md-12 pull-right">

                                <asp:Button ID="btn_Luu" CssClass="btn btn-primary" Text="Lưu" runat="server" Visible="false"
                                    OnClick="btn_Luu_Click" />
                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                            </div>
                        </div>

                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="modal fade" id="DeleteObjectModal" tabindex="-1" role="dialog"
        data-backdrop="static" data-keyboard="false"
        aria-labelledby="DeleteModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">

            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="DeleteModalLabel">Thông báo</h5>
                </div>
                <div class="modal-body">
                    <div class="col-md-12">
                        <h4>Bạn có thật sự muốn xóa không?</h4>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btn_Delete" UseSubmitBehavior="false" CssClass="btn btn-primary" Text="OK" runat="server"
                        OnClick="btn_Delete_Click" />
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Hủy</button>
                </div>
            </div>

        </div>
    </div>
</asp:Content>

