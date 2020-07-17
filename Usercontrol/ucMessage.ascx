<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucMessage.ascx.cs" Inherits="ucMessage" %>


<style>
    .alert {
        padding: 15px;
        margin-bottom: 20px;
        border: 1px solid transparent;
        border-radius: 4px;
    }

        .alert h4 {
            margin-top: 0;
            color: inherit;
        }

        .alert .alert-link {
            font-weight: bold;
        }

        .alert > p,
        .alert > ul {
            margin-bottom: 0;
        }

            .alert > p + p {
                margin-top: 5px;
            }

    .alert-dismissable {
        padding-right: 35px;
    }

        .alert-dismissable .close {
            position: relative;
            top: -2px;
            right: -21px;
            color: inherit;
        }

    .alert-success {
        color: #3c763d;
        background-color: #dff0d8;
        border-color: #d6e9c6;
    }

        .alert-success hr {
            border-top-color: #c9e2b3;
        }

        .alert-success .alert-link {
            color: #2b542c;
        }

    /*.alert-error {
        color: #ffffff;
        background-color: #ff3535;
        border-color: #ff8080;
    }

        .alert-error hr {
            border-top-color: #ff0f0f;
        }

        .alert-error .alert-link {
            color: #ffffff;
        }*/

    .alert-info {
        color: #31708f;
        background-color: #d9edf7;
        border-color: #bce8f1;
    }

        .alert-info hr {
            border-top-color: #a6e1ec;
        }

        .alert-info .alert-link {
            color: #245269;
        }

    .alert-warning {
        color: #8a6d3b;
        background-color: #fcf8e3;
        border-color: #faebcc;
    }

        .alert-warning hr {
            border-top-color: #f7e1b5;
        }

        .alert-warning .alert-link {
            color: #66512c;
        }

    .alert-error {
        color: #a94442;
        background-color: #f2dede;
        border-color: #ebccd1;
    }

        .alert-error hr {
            border-top-color: #e4b9c0;
        }

        .alert-error .alert-link {
            color: #843534;
        }
</style>
<div runat="server" id="ErrorBox" class="alert alert-error no-margin">
    <button type="button" class="close" data-dismiss="alert">&times;</button>
    <span runat="server" id="ErrorMessage">Vui lòng nhập mật khẩu mới 2 lần giống nhau.</span>
</div>

<div runat="server" id="SuccessBox" class="alert alert-success no-margin">
    <button type="button" class="close" data-dismiss="alert">&times;</button>
    <span runat="server" id="SuccessMessage">Đã lưu mật khẩu mới suss.</span>
</div>
<div runat="server" id="WarningBox" class="alert alert-warning no-margin">
    <button type="button" class="close" data-dismiss="alert">&times;</button>
    <span runat="server" id="WarningMessage">Đã lưu mật khẩu mới warning.</span>
</div>
<div runat="server" id="InfoBox" class="alert alert-info no-margin">
    <button type="button" class="close" data-dismiss="alert">&times;</button>
    <span runat="server" id="InfoMessage">Đã lưu mật khẩu mới info.</span>
</div>
