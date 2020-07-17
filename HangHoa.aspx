<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="HangHoa.aspx.cs" Inherits="HangHoa" %>

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
                                <h4 class="card-title">Quản lý hàng hóa</h4>
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
                                                    <th>Mã hàng hóa</th>
                                                    <th>Tên hàng hóa</th>
                                                    <th>Đơn giá</th>
                                                    <th>Đơn vị tính</th>
                                                    <th>Nhóm hàng</th>
                                                    <th>Nhà sản xuất</th>
                                                    <th>Thao tác</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_Data_List">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th scope="row"><%#Eval("STT") %></th>
                                                            <td><%#Eval("MAHANG") %></td>

                                                            <td><%#Eval("TENHANG") %></td>
                                                            <td><%# Eval("DONGIA") %></td>
                                                            <td><%# Eval("DONVITINH") %></td>
                                                            <td><%# Eval("TENNHOMHANG") %></td>
                                                            <td><%# Eval("TENNHASANXUAT") %></td>
                                                            <td>
                                                                <asp:LinkButton ToolTip="Chỉnh sửa dữ liệu" CssClass="col-md-6" ForeColor="DarkBlue" Text='<i class="la la-pencil-square"></i>' Visible='<%# bool.Parse(SessionUtility.AdminOid.ToString() == "" ? "False" : "True")%>' 
                                                                     CommandName='<%#Eval("MAHANG") %>' runat="server" ID="LinkButton1" OnClick="EditObject_Click" />
                                                                <asp:LinkButton ToolTip="Xóa dữ liệu" CssClass="col-md-6" ForeColor="Red" Text=' <i class="la la-trash"></i>'  Visible='<%# bool.Parse(SessionUtility.AdminOid.ToString() == "" ? "False" : "True")%>' 
                                                                    CommandName='<%#Eval("MAHANG") %>' runat="server" ID="LinkButton2" OnClick="DeleteObject_Click" />
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
                                     <asp:DropDownList runat="server"  CssClass="form-control"  ID="NhomHang">
                                         <asp:ListItem Text="Chọn nhóm hàng" Selected ="True"  Value=""/>
                                     </asp:DropDownList>
                                </div>
                                 <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Nhà sản xuất(*):" runat="server" />
                                    </h4>
                                      <asp:DropDownList runat="server" CssClass="form-control"  ID="NhaSanXuat">
                                         <asp:ListItem Text="Chọn nhà sản xuất" Selected ="True"  Value=""/>
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

