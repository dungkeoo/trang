using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class ThuKho : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_Luu);
            btn_NewObject.Visible = SessionUtility.AdminOid.ToSafetyString() == "" ? false : true;
            LoadData();
        }
    }
        
    public void LoadData()
    {
        DataProvider dt = new DataProvider();
        Repeater_ThuKho_List.DataSource = dt.ThuKho_List();
        Repeater_ThuKho_List.DataBind();
    }
    protected void EditObject_Click(object sender, EventArgs e)
    {
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        SessionUtility.EventMode = "Edit";

        ucMessage.HideAll();
        LoadEdit();
        OpenModal("NewObjectModal");
    }

    public void LoadEdit()
    {
        DataProvider dtp = new DataProvider();
        DataTable tb = dtp.ThuKho_Find(SessionUtility.OidObject.ToSafetyString());
        // DataTable tb = dtp.Kho_GetObject("CD6CE778-0CA0-4D8C-AD49-52BE3BF7AB6B");
        if (tb.Rows.Count > 0)
        {
            DataRow row = tb.Rows[0];

            MaQuanLy.Value = row["MATHUKHO"].ToSafetyString();
            MaQuanLy.Disabled = true;
            TenThuKho.Value = row["TENTHUKHO"].ToSafetyString();
            MatKhau.Value = row["MATKHAU"].ToSafetyString();
            LoaiTaiKhoan.Value = row["ISADMIN"].ToSafetyString() == "True" ? "1" : "0";
            //SessionUtility.OidObject = "";
            UpdatePanel_Object.Update();
        }
        else
        {
            ucMessage.ShowError("Lỗi hệ thống!"); return;
        }
    }
    public void OpenModal(string idmodal)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), idmodal, "$('#" + idmodal + "').modal();", true);
        UpdatePanel_Object.Update();
    }

    public void CloseModal(string idmodal)
    {
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), idmodal, "$('#" + idmodal + "').hideModal();", true);
        UpdatePanel_Object.Update();
    }
    protected void DeleteObject_Click(object sender, EventArgs e)
    {

        OpenModal("DeleteObjectModal");
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
    }

    protected void btn_Delete_Click(object sender, EventArgs e)
    {
        DataProvider dtp = new DataProvider();
        if (dtp.ThuKho_Delete(SessionUtility.OidObject))
        {
            CloseModal("DeleteObjectModal");
            LoadData();
            UpdatePanel_View.Update();
        }
    }
    protected void btn_NewObject_Click(object sender, EventArgs e)
    {
        ClearForm();
        ucMessage.HideAll();
        OpenModal("NewObjectModal");

        SessionUtility.EventMode = "Create";
        if (SessionUtility.EventMode == "Create")
            SessionUtility.AvatarImg = "";
        UpdatePanel_Object.Update();
    }

    protected void btn_Luu_Click(object sender, EventArgs e)
    {
        //string oidUser = SessionUtility.AdminOid;
        string oidUser = SessionUtility.UserOid;
        //if (!string.IsNullOrEmpty(oidUser))
        //{
            if (CheckInfo())
            {
                if (SessionUtility.EventMode == "Create")
                {

                    DataProvider dtp = new DataProvider();
                    if (dtp.ThuKho_Find(MaQuanLy.Value).Rows.Count <= 0)
                    {
                        if (dtp.ThuKho_Insert(MaQuanLy.Value, TenThuKho.Value, MatKhau.Value, LoaiTaiKhoan.Value))
                        {
                            ucMessage.ShowSuccess("Đăng ký thành công.");
                            LoadData();
                            ClearForm();
                            UpdatePanel_View.Update();
                        }
                        else
                        {
                            ucMessage.ShowError("Lỗi hệ thống!"); return;
                        }
                    }
                    else
                    {
                        ucMessage.ShowError("Mã quản lý thủ kho đã tồn tại!"); return;
                    }
                }
                if (SessionUtility.EventMode == "Edit")
                {
                    DataProvider dtp = new DataProvider();
                    if (dtp.ThuKho_Update(MaQuanLy.Value, TenThuKho.Value, MatKhau.Value, LoaiTaiKhoan.Value))
                    {
                        ucMessage.ShowSuccess("Cập nhật thành công.");
                        LoadData();
                        UpdatePanel_View.Update();
                    }
                    else
                    {
                        ucMessage.ShowError("Lỗi hệ thống!"); return;
                    }
                }
            }
        //}
    }

    public bool CheckInfo()
    {
        bool result = true;
        if (string.IsNullOrEmpty(MaQuanLy.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập mã quản lý");
            return result;
        }

        if (string.IsNullOrEmpty(TenThuKho.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập tên thủ kho");
            return result;
        }

        if (string.IsNullOrEmpty(MatKhau.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập mật khẩu");
            return result;
        }
        if (LoaiTaiKhoan.Value == "-1")
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn loại tài khoản");
            return result;
        }
        return result;
    }

    public void ClearForm()
    {
        MaQuanLy.Value = "";
        MaQuanLy.Disabled = false;
        TenThuKho.Value = "";
        MatKhau.Value = "";
        LoaiTaiKhoan.Value = "-1";
    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }
}