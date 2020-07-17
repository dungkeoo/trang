using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class HangHoa : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_Luu);
            LoadData();
            LoadNhaSanXuat();
            LoadNhomHang();
        }
    }

    public void LoadNhaSanXuat()
    {
        DataProvider dt = new DataProvider();
        NhaSanXuat.DataSource = dt.NhaSanXuat_List_Dropdown();
        NhaSanXuat.DataValueField = "MANHASANXUAT";
        NhaSanXuat.DataTextField = "TENNHASANXUAT";
        NhaSanXuat.DataBind();
    }

    public void LoadNhomHang()
    {
        DataProvider dt = new DataProvider();
        NhomHang.DataSource = dt.NhomHang_List_Dropdown();
        NhomHang.DataValueField = "MANHOMHANG";
        NhomHang.DataTextField = "TENNHOMHANG";
        NhomHang.DataBind();
    }
    public void LoadData()
    {   
        DataProvider dt = new DataProvider();
        Repeater_Data_List.DataSource = dt.HangHoa_List();
        Repeater_Data_List.DataBind();
    }
    protected void EditObject_Click(object sender, EventArgs e)
    {
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        SessionUtility.EventMode = "Edit";

        ClearForm();
        ucMessage.HideAll();
        LoadEdit();
        OpenModal("NewObjectModal");
    }

    public void LoadEdit()
    {
        DataProvider dtp = new DataProvider();
        DataTable tb = dtp.HangHoa_Find(SessionUtility.OidObject.ToSafetyString());
        if (tb.Rows.Count > 0)
        {
            DataRow row = tb.Rows[0];

            MaQuanLy.Value = row["MAHANG"].ToSafetyString();
            MaQuanLy.Disabled = true;
            TenHangHoa.Value = row["TENHANG"].ToSafetyString();
            DonGia.Value = row["DONGIA"].ToSafetyString();
            DonViTinh.Value = row["DONVITINH"].ToSafetyString();
            NhomHang.SelectedItem.Text = row["TENNHOMHANG"].ToSafetyString();
            NhomHang.SelectedItem.Value = row["MANHOMHANG"].ToSafetyString();
            NhaSanXuat.SelectedItem.Text = row["TENNHASANXUAT"].ToSafetyString();
            NhaSanXuat.SelectedItem.Value = row["MANHASANXUAT"].ToSafetyString();

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
        if (dtp.NhaSanXuat_Delete(SessionUtility.OidObject))
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
                    if (dtp.HangHoa_Find(MaQuanLy.Value).Rows.Count <= 0)
                    {
                        if (dtp.HangHoa_Insert(MaQuanLy.Value, TenHangHoa.Value, DonGia.Value.ToInt(), DonViTinh.Value, NhomHang.SelectedItem.Value, NhaSanXuat.SelectedItem.Value))
                        {
                            ucMessage.ShowSuccess("Thêm mới thành công.");
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
                        ucMessage.ShowError("Mã quản lý đã tồn tại!"); return;
                    }
                }
                if (SessionUtility.EventMode == "Edit")
                {
                    DataProvider dtp = new DataProvider();
                    if (dtp.HangHoa_Update(MaQuanLy.Value, TenHangHoa.Value, DonGia.Value.ToInt(), DonViTinh.Value, NhomHang.SelectedItem.Value, NhaSanXuat.SelectedItem.Value))
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

        if (string.IsNullOrEmpty(TenHangHoa.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập tên hàng hóa");
            return result;
        }

        if (string.IsNullOrEmpty(DonViTinh.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập dơn vị tính");
            return result;
        }
        if (string.IsNullOrEmpty(DonGia.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập dơn giá");
            return result;
        }
        if (!string.IsNullOrEmpty(DonGia.Value))
        {
            int dongia = DonGia.Value.ToInt();
            if (dongia == 0)
            {
                result = false;
                ucMessage.ShowError("Vui lòng nhập dơn giá là số và lớn hơn 0");
            }
            return result;
        }
        if (string.IsNullOrEmpty(NhomHang.SelectedItem.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn nhóm hàng");
            return result;
        }

        if (string.IsNullOrEmpty(NhaSanXuat.SelectedItem.Value))
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn nhà sản xuất");
            return result;
        }
        return result;
    }

    public void ClearForm()
    {
        MaQuanLy.Value = "";
        MaQuanLy.Disabled = false;
        TenHangHoa.Value = "";
        DonViTinh.Value = "";
        DonGia.Value = "";
        NhomHang.ClearSelection();
        NhomHang.SelectedItem.Text = "Chọn nhóm hàng";
        NhaSanXuat.ClearSelection();
        NhaSanXuat.SelectedItem.Text = "Chọn nhà sản xuất";
    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }
}