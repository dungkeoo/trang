<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DangNhap.aspx.cs" Inherits="DangNhap" %>

<%@ Register Src="~/Usercontrol/ucMessage.ascx" TagPrefix="uc1" TagName="ucMessage" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập</title>
    <%--<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, user-scalable=0, minimal-ui" />
    <meta name="description" content="Chameleon Admin is a modern Bootstrap 4 webapp &amp; admin dashboard html template with a large number of components, elegant design, clean and organized code." />
    <meta name="keywords" content="admin template, Chameleon admin template, dashboard template, gradient admin template, responsive admin template, webapp, eCommerce dashboard, analytic dashboard" />
    <meta name="author" content="ThemeSelect" />

    <link rel="apple-touch-icon" href="theme-assets/images/ico/apple-icon-120.png" />
    <link rel="shortcut icon" type="image/x-icon" href="theme-assets/images/ico/favicon.ico" />
    <link href="https://fonts.googleapis.com/css?family=Muli:300,300i,400,400i,600,600i,700,700i%7CComfortaa:300,400,700" rel="stylesheet" />
    <link href="https://maxcdn.icons8.com/fonts/line-awesome/1.1/css/line-awesome.min.css" rel="stylesheet" />
    <!-- BEGIN VENDOR CSS-->
    <link rel="stylesheet" type="text/css" href="theme-assets/css/vendors.css" />
    <!-- END VENDOR CSS-->
    <!-- BEGIN CHAMELEON  CSS-->
    <link rel="stylesheet" type="text/css" href="theme-assets/css/app-lite.css" />
    <!-- END CHAMELEON  CSS-->
    <!-- BEGIN Page Level CSS-->
    <link rel="stylesheet" type="text/css" href="theme-assets/css/core/menu/menu-types/vertical-menu.css" />
    <link rel="stylesheet" type="text/css" href="theme-assets/css/core/colors/palette-gradient.css" />
    <script type="text/javascript" src="js/jquery.min.js"></script>
    <!-- END Page Level CSS-->
    <!-- BEGIN Custom CSS-->
    <!-- END Custom CSS-->
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <section class="basic-inputs">
                <div class="row match-height">
                    <div class="col-sm-1 col-md-2 col-lg-3">
                        <div class="card">
                        </div>
                    </div>
                    <div class="col-sm-10 col-md-8 col-lg-6">
                        <div class="card">
                            <div class="container">
                                <div class="row">
                                    <div class="col-xs-5 col-sm-5">
                                        <img src="images/logotdmu.jpg" />
                                    </div>
                                    <div class="col-sm-2">
                                    </div>
                                    <div class="col-xs-7 col-sm-5" style="padding-top:10px">
                                        <p style="margin-bottom: 5px"><b>TRƯỜNG ĐẠI HỌC THỦ DẦU MỘT</b></p>
                                        <p style="margin-bottom: 5px;text-align:center">KHOA KỸ THUẬT CÔNG NGHỆ</p>
                                        <p style="margin-bottom: 5px">SV: HOÀNG THỊ TRANG</p>
                                        <p style="margin-bottom: 5px">MSSV: 1834801040030</p>
                                        <p style="margin-bottom: 5px">LỚP: KLB18HT101</p>
                                        <p style="margin-bottom: 5px">GVHD: TS. BÙI THANH HÙNG</p>
                                    </div>
                                    
                                </div>
                            </div>
                             <div class="col-xs-12" style="text-align:center;font-size:26px;color:#000">
                                             ỨNG DỤNG QUẢN LÝ KHO HÀNG
                                    </div>
                            <div class="card-header" style="padding:0PX">
                                <hr />
                                <h2 class="card-title" style="text-align:center">ĐĂNG NHẬP</h2>
                            </div>
                            <div>
                            </div>
                            <div class="card-block">
                                <div class="card-body">

                                    <div>
                                        <h4 class="pull-left">
                                            <asp:Label Text="Tài khoản:" runat="server" />
                                        </h4>
                                        <input type="text" runat="server" class="form-control" id="TaiKhoan" value="" />
                                    </div>

                                    <div>
                                        <h4 class="pull-left">
                                            <asp:Label Text="Mật khẩu:" runat="server" />
                                        </h4>
                                        <input type="password" runat="server" class="form-control" id="MatKhau" value="" />
                                    </div>

                                    <div style="text-align: center">
                                        <asp:Button CssClass="btn btn-primary" Text="Đăng nhập" ID="btn_DangNhap" OnClick="btn_DangNhap_Click" runat="server" />
                                        <!-- Button trigger modal -->
                                        <button type="button" class="btn btn-info" data-toggle="modal" data-target="#exampleModal">
                                            Quên mật khẩu
                                        </button>

                                    </div>
                                    <div>
                                        <uc1:ucMessage runat="server" ID="ucMessage" />

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-sm-1 col-md-2 col-lg-3">
                        <div class="card">
                        </div>
                    </div>

                    <!-- Modal -->
                    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="exampleModalLabel">THÔNG BÁO</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    LIÊn HỆ QUẢN TRỊ VIÊN QUA EMAIL:
                                    <p><b>hoangthitrang@gmail.com</b></p>
                                </div>
                                <div class="modal-footer">
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Ok</button>

                                </div>
                            </div>
                        </div>
                    </div>



                </div>
            </section>
        </div>
        <!-- BEGIN VENDOR JS-->
        <script src="theme-assets/vendors/js/vendors.min.js" type="text/javascript"></script>
        <!-- BEGIN VENDOR JS-->
        <!-- BEGIN PAGE VENDOR JS-->
        <!-- END PAGE VENDOR JS-->
        <!-- BEGIN CHAMELEON  JS-->
        <script src="theme-assets/js/core/app-menu-lite.js" type="text/javascript"></script>
        <script src="theme-assets/js/core/app-lite.js" type="text/javascript"></script>
        <!-- END CHAMELEON  JS-->
        <!-- BEGIN PAGE LEVEL JS-->
        <!-- END PAGE LEVEL JS-->
    </form>
</body>
</html>
