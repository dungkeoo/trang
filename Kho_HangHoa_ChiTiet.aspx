<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kho_HangHoa_ChiTiet.aspx.cs" Inherits="Kho_HangHoa_ChiTiet" %>

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
                                <h4 class="card-title">Quản lý phiếu(nhập - xuất - chuyển) cá nhân</h4>
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
                                    <asp:Button Text="Thêm mới" CssClass="btn btn-primary" ID="btn_NewObject" OnClick="btn_NewObject_Click" runat="server" />
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
                                                                <asp:LinkButton ToolTip="Chỉnh sửa dữ liệu" CssClass="col-md-6" ForeColor="DarkBlue" Visible='<%# bool.Parse(Eval("TRANGTHAITHAOTAC").ToString())%>' Text='<i class="la la-pencil-square"></i>' CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton1" OnClick="EditObject_Click" />
                                                                <asp:LinkButton ToolTip="Xóa dữ liệu" CssClass="col-md-6" ForeColor="Red" Visible='<%# bool.Parse(Eval("TRANGTHAITHAOTAC").ToString())%>' Text=' <i class="la la-trash"></i>'  CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton2" OnClick="DeleteObject_Click" />
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
                            <div class="col-md-12">
                                  <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Nhóm hàng(*):" runat="server" />
                                    </h4>
                                      <input class="form-control" value="" id="Id_Phieu" visible="false" runat="server"/>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="NhomHang_Dropdown" OnSelectedIndexChanged="NhomHang_Dropdown_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Chọn nhóm hàng" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Hàng hóa(*):" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="HangHoa_Dropdown">
                                        <asp:ListItem Text="Chọn hàng hóa" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Số lượng(*):" runat="server" />

                                    </h4>
                                    <input class="form-control" value="" id="SoLuong" type="number" runat="server" placeholder="Nhập số lượng" />
                                </div>
                            <%--    <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Ngày lập(*):" runat="server" />
                                    </h4>
                                    <input class="form-control" value="" id="NgayLap" runat="server" type="date" />
                                </div>--%>

                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Phiếu hàng hóa:" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="LoaiPhieu_Dropdown" OnSelectedIndexChanged="LoaiPhieu_Dropdown_SelectedIndexChanged" AutoPostBack="true"> 
                                        <asp:ListItem Text="Chọn loại phiếu" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
                                 <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Kho hiện tại:" ID="lb_KhoHienTai" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="KhoHang_MaThuKho_Dropdown" OnSelectedIndexChanged="KhoHang_MaThuKho_Dropdown_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="Chọn kho" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Kho nhận:" ID="lb_KhoNhan" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="KhoHang_Dropdown">
                                        <asp:ListItem Text="Chọn kho nhận" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
                                 <%--<div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Ngày nhận:" ID="lb_NgayNhan" runat="server" />
                                    </h4>
                                    <input class="form-control" value="" id="NgayNhan" runat="server" type="date" />
                                </div>--%>

                            </div>

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

                                <asp:Button ID="btn_Luu" CssClass="btn btn-primary" Text="Lưu" runat="server"
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

