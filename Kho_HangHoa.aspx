<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Kho_HangHoa.aspx.cs" Inherits="Kho_HangHoa" %>

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
                                <%--<h4 class="card-title">Phân quyền quản lý kho</h4>--%>
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
                                    <h2 class="card-title">Thông tin hàng hóa và tổng số lượng</h2>
                                    <asp:Button Text="Thêm mới" Visible="false" CssClass="btn btn-primary" ID="btn_NewObject" OnClick="btn_NewObject_Click" runat="server" />
                                    
                                   <asp:Button Text="Export"  CssClass="btn btn-primary" ID="Export" OnClick="ExportExcel" runat="server" />

                                    <div class="table-responsive">
                                        <table class="table">
                                            <thead>
                                                <tr>
                                                    <th>#</th>
                                                    <th>Tên kho</th>
                                                    <th>Tên hàng</th>
                                                    <th>Tổng số lượng</th>
                                                    <th>Đơn vị tính</th>
                                                    <th>Đơn giá</th>
                                                    <%--<th>Thao tác</th>--%>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                <asp:Repeater runat="server" ID="Repeater_ThuKho_List">
                                                    <ItemTemplate>
                                                        <tr>
                                                            <th scope="row"><%#Eval("STT") %></th>
                                                            <td><%#Eval("TENKHO") %></td>
                                                            <td><%#Eval("TENHANG") %></td>
                                                            <td><%#Eval("TONGSOLUONG") %></td>
                                                            <td><%#Eval("DONVITINH") %></td>
                                                            <td><%#Eval("DONGIA") %></td>
                                                            <%--<td><%# Eval("ISADMIN").ToString() == "True" ? "Quản lý" : "Người dùng" %></td>--%>
                                                            <td style="display: none">
                                                                <asp:LinkButton CssClass="col-md-6" ForeColor="DarkBlue" Text='<i class="la la-pencil-square"></i>' Visible="false" CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton1" OnClick="EditObject_Click" />
                                                                <asp:LinkButton CssClass="col-md-6" ForeColor="Red" Text=' <i class="la la-trash"></i>' Visible="false" CommandName='<%#Eval("ID") %>' runat="server" ID="LinkButton2" OnClick="DeleteObject_Click" />
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:Repeater>

                                            </tbody>
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
                            <%--  <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                            <span aria-hidden="true">&times;</span>
                        </button>--%>
                        </div>
                        <div class="modal-body" style="display: flex">
                            <div class="col-md-12">
                                <div>
                                    <h4 class="pull-left">
                                        <asp:Label Text="Kho hiện tại:" ID="lb_KhoHienTai" runat="server" />
                                    </h4>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="KhoHang_MaThuKho_Dropdown">
                                        <asp:ListItem Text="Chọn kho" Selected="True" Value="" />
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

