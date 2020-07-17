<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ThongKe.aspx.cs" Inherits="ThongKe" %>

<%@ Register Src="~/Usercontrol/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel_View">
        <ContentTemplate>
            <div class="content-body">
                <!-- Basic Tables start -->
                <div class="row">
                    <div class="col-12">
                        <div class="card">
                            <div class="card-header">
                                <h4 class="card-title">Thông kế</h4>
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
                                    <asp:Button Text="Thêm mới" CssClass="btn btn-primary" Visible="false" ID="btn_NewObject" OnClick="btn_NewObject_Click" runat="server" />
                                    <div class="row">
                                        <div class="col-xs-12 col-sm-6">
                                            <h4 class="pull-left">
                                                <asp:Label Text="Phiếu hàng hóa:" runat="server" />
                                            </h4>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="LoaiPhieu_Dropdown" AutoPostBack="true">
                                                <asp:ListItem Text="Chọn loại phiếu" Selected="True" Value="" />
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-12 col-sm-6">
                                            <h4 class="pull-left">
                                                <asp:Label Text="Kho hiện tại:" runat="server" />
                                            </h4>
                                            <asp:DropDownList runat="server" CssClass="form-control" ID="KhoHang_MaThuKho_Dropdown" AutoPostBack="true">
                                                <asp:ListItem Text="Chọn kho" Selected="True" Value="" />
                                            </asp:DropDownList>
                                        </div>
                                        
                                        <div class="col-xs-6 col-sm-6">
                                            <h4 class="pull-left">
                                                <asp:Label Text="Từ ngày:" runat="server" />
                                            </h4>
                                            <asp:TextBox CssClass="form-control" runat="server" ID="TuNgay" Text="" TextMode="Date" />
                                        </div>
                                        <div class="col-xs-6 col-sm-6">
                                            <h4 class="pull-left">
                                                <asp:Label Text="Đến ngày:" runat="server" />
                                            </h4>
                                            <asp:TextBox CssClass="form-control" runat="server" ID="DenNgay" Text="" TextMode="Date" />
                                        </div>
                                    </div>
                                    <asp:UpdatePanel runat="server">
                                        <ContentTemplate>
                                            <asp:Button Text="Thống kê" CssClass="btn btn-primary" ID="btn_ThongKe" OnClick="ThongKe_Click" runat="server" />
                                            <asp:Button Text="Export" CssClass="btn btn-primary" ID="Export" OnClick="ExportExcel" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btn_ThongKe" />
                                            <asp:PostBackTrigger ControlID="Export" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                    <br />
                                    <div class="table-responsive">
                                        <table class="table" id="table_ThongKe">
                                            <thead>
                                                <tr>
                                                    <th>STT</th>
                                                    <th>Loại phiếu</th>
                                                    <th>Tên hàng hóa</th>
                                                    <th>Số lượng</th>
                                                    <th>Ngày lập</th>
                                                    <th>Kho nhận</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_Data_List">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th scope="row"><%#Eval("STT") %></th>
                                                            <td><%#Eval("TENPHIEU") %></td>
                                                            <td><%#Eval("TENHANG") %></td>
                                                            <td><%# Eval("SOLUONG") %></td>
                                                            <td><%# Eval("NGAYLAP") %></td>
                                                            <td><%# Eval("TENKHONHAN") %></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>
                                        </table>
                                        <div class="container">
                                            <div class="row">
                                                <%--<div class="col-md-6">
                                                </div>--%>
                                                <div class="col-md-3">
                                                    <b>
                                                        <asp:Label CssClass="col-md-12" Text="Tổng số lượng" runat="server" />

                                                    </b>
                                                </div>

                                                <div class="col-md-3">
                                                    <asp:Repeater runat="server" ID="TongSoLuong">
                                                        <ItemTemplate>
                                                            <asp:Label Text='<%#Eval("TongSoLuong") %>' runat="server" />
                                                        </ItemTemplate>
                                                    </asp:Repeater>
                                                </div>
                                            </div>
                                        </div>

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
        <div class="modal-dialog" role="document">
            <asp:UpdatePanel ID="UpdatePanel_Object" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h5 class="modal-title" id="NewObjectModalLabel">Thêm mới</h5>
                        </div>
                        <div class="modal-body" style="display: flex">
                            <div class="col-md-12">
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Mã quản lý(*):" runat="server" />

                                    </h4>
                                    <input class="form-control" value="" id="MaQuanLy" runat="server" placeholder="Mã quản lý" />
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Tên hàng hóa(*):" runat="server" />
                                    </h4>
                                    <input class="form-control" value="" id="TenHangHoa" runat="server" placeholder="Tên hàng hóa" />
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Đơn giá(*):" runat="server" />
                                    </h4>
                                    <input class="form-control" value="" id="DonGia" runat="server" placeholder="Đơn giá" />
                                </div>

                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Đơn vị tính(*):" runat="server" />
                                    </h4>
                                    <input class="form-control" value="" id="DonViTinh" runat="server" placeholder="Đơn vị tính" />
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Nhóm hàng(*):" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="NhomHang">
                                        <asp:ListItem Text="Chọn nhóm hàng" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Nhà sản xuất(*):" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="NhaSanXuat">
                                        <asp:ListItem Text="Chọn nhà sản xuất" Selected="True" Value="" />
                                    </asp:DropDownList>
                                </div>
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

