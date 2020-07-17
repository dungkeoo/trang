using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;

public partial class Kho_HangHoa_ChiTiet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_Luu);
            LoadData();
           
            Visible(false);
        }
    }

    public void LoadKhoHang()
    {
        DataProvider dt = new DataProvider();
        KhoHang_MaThuKho_Dropdown.DataSource = dt.Kho_List_MaThuKho_Dropdown(SessionUtility.AdminOid == "" ? SessionUtility.UserOid : "ADMIN");
        KhoHang_MaThuKho_Dropdown.DataValueField = "MAKHO";
        KhoHang_MaThuKho_Dropdown.DataTextField = "TENKHO";
        KhoHang_MaThuKho_Dropdown.DataBind();
    }
    public void LoadKhoHangAll(string maKho)
    {
        DataProvider dt = new DataProvider();
        KhoHang_Dropdown.DataSource = dt.Kho_List_Dropdown(maKho);
        KhoHang_Dropdown.DataValueField = "MAKHO";
        KhoHang_Dropdown.DataTextField = "TENKHO";
        KhoHang_Dropdown.DataBind();
    }

    public void LoadHangHoa(string maNhomHang)
    {
        DataProvider dt = new DataProvider();
        HangHoa_Dropdown.DataSource = dt.HangHoa_List_Dropdown_MaNhomHang(maNhomHang);
        HangHoa_Dropdown.DataValueField = "MAHANG";
        HangHoa_Dropdown.DataTextField = "TENHANG";
        HangHoa_Dropdown.DataBind();
    }
    public void LoadNhomHangHoa()
    {
        DataProvider dt = new DataProvider();
        NhomHang_Dropdown.DataSource = dt.NhomHang_List_Dropdown();
        NhomHang_Dropdown.DataValueField = "MANHOMHANG";
        NhomHang_Dropdown.DataTextField = "TENNHOMHANG";
        NhomHang_Dropdown.DataBind();
    }
    public void LoadLoaiPhieu()
    {
        DataProvider dt = new DataProvider();
        LoaiPhieu_Dropdown.DataSource = dt.LoaiPhieu_List_Dropdown();
        LoaiPhieu_Dropdown.DataValueField = "ID";
        LoaiPhieu_Dropdown.DataTextField = "TENPHIEU";
        LoaiPhieu_Dropdown.DataBind();
    }
    public void LoadData()
    {
        DataProvider dt = new DataProvider();
        Repeater_Data_List.DataSource = dt.Kho_HangHoa_ChiTiet_List(SessionUtility.AdminUsername != "" ? "ADMIN" : SessionUtility.UserOid);
        Repeater_Data_List.DataBind();
    }
    protected void EditObject_Click(object sender, EventArgs e)
    {
        SessionUtility.OidObject = ((LinkButton)sender).CommandName;
        SessionUtility.EventMode = "Edit";

        ucMessage.HideAll();
        OpenModal("NewObjectModal");

        LoadEdit();
    }

    public void LoadEdit()
    {
        ClearForm();
        DataProvider dtp = new DataProvider();
        DataTable tb = dtp.Kho_HangHoa_ChiTiet_Find(SessionUtility.OidObject.ToInt());
        if (tb.Rows.Count > 0)
        {
            DataRow row = tb.Rows[0];
            Id_Phieu.Value = row["ID"].ToSafetyString();
            NhomHang_Dropdown.SelectedItem.Value = row["MANHOMHANG"].ToSafetyString();
            NhomHang_Dropdown.SelectedItem.Text = row["TENNHOMHANG"].ToSafetyString();

            HangHoa_Dropdown.SelectedItem.Value = row["MAHANG"].ToSafetyString();
            HangHoa_Dropdown.SelectedItem.Text = row["TENHANG"].ToSafetyString();

            LoaiPhieu_Dropdown.SelectedItem.Value = row["LOAIPHIEU"].ToSafetyString();
            LoaiPhieu_Dropdown.SelectedItem.Text = row["TENPHIEU"].ToSafetyString();

            KhoHang_MaThuKho_Dropdown.SelectedItem.Text = row["TENKHO"].ToSafetyString();
            KhoHang_MaThuKho_Dropdown.SelectedItem.Value = row["MAKHO"].ToSafetyString();

            KhoHang_Dropdown.SelectedItem.Text = row["TENKHONHAN"].ToSafetyString();
            KhoHang_Dropdown.SelectedItem.Value = row["MAKHONHAN"].ToSafetyString();
            SoLuong.Value = row["SOLUONG"].ToSafetyString();

            // phiếu nhập kho
            if (row["LOAIPHIEU"].ToSafetyString() == "1")
            {
                Visible(false);
            }

            // phiếu nhập xuất
            if (row["LOAIPHIEU"].ToSafetyString() == "2")
            {
                Visible(true);
                lb_KhoNhan.Visible = false;
                KhoHang_Dropdown.Visible = false;
                //lb_NgayNhan.Visible = false;
                //NgayNhan.Visible = false;

            }
            // phiếu nhập chuyển
            if (row["LOAIPHIEU"].ToSafetyString() == "3")
            {
                Visible(true);
            }
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
        LoadNhomHangHoa();
        LoadLoaiPhieu();
        LoadKhoHang();
        LoadKhoHangAll("");
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
        //NgayLap.Value = DateTime.Today.ToString("yyyy-MM-dd");
        //NgayNhan.Value = DateTime.Today.ToString("yyyy-MM-dd");
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
                if (LoaiPhieu_Dropdown.SelectedItem.Value.ToString() == "1")
                    if (dtp.Kho_HangHoa_ChiTiet_Insert(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()
                                                        , KhoHang_Dropdown.SelectedItem.Value.ToString()
                                                        , HangHoa_Dropdown.SelectedItem.Value.ToString()
                                                        , SoLuong.Value.ToInt(), SessionUtility.UserOid, LoaiPhieu_Dropdown.SelectedItem.Value.ToInt()))
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
                else
                {
                    DataTable tb = dtp.Kho_HangHoa_Check_SoLuong(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()
                                                    , HangHoa_Dropdown.SelectedItem.Value.ToString()
                                                    , SoLuong.Value.ToInt());
                    if (tb.Rows.Count > 0)

                        if (dtp.Kho_HangHoa_ChiTiet_Insert(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()
                                                       , KhoHang_Dropdown.SelectedItem.Value.ToString()
                                                       , HangHoa_Dropdown.SelectedItem.Value.ToString()
                                                       , SoLuong.Value.ToInt(), SessionUtility.UserOid, LoaiPhieu_Dropdown.SelectedItem.Value.ToInt()))
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
                    else
                    {
                        ucMessage.ShowError("Số lượng hàng hóa trong kho hiện tại không đủ. Vui lòng kiểm tra lại số lượng");
                    }
                }
            }
            if (SessionUtility.EventMode == "Edit")
            {
                DataProvider dtp = new DataProvider();
                if (LoaiPhieu_Dropdown.SelectedItem.Value.ToString() == "1")
                    if (dtp.Kho_HangHoa_ChiTiet_Update(Id_Phieu.Value.ToInt(), KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()
                                                    , KhoHang_Dropdown.SelectedItem.Value.ToString()
                                                    , HangHoa_Dropdown.SelectedItem.Value.ToString()
                                                    , SoLuong.Value.ToInt(), LoaiPhieu_Dropdown.SelectedItem.Value.ToInt()))
                    {
                        ucMessage.ShowSuccess("Cập nhật thành công.");
                        LoadData();
                        UpdatePanel_View.Update();
                    }
                    else
                    {
                        ucMessage.ShowError("Lỗi hệ thống!"); return;
                    }
                else
                {
                    DataTable tb = dtp.Kho_HangHoa_Check_SoLuong(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()
                                                   , HangHoa_Dropdown.SelectedItem.Value.ToString()
                                                   , SoLuong.Value.ToInt());
                    if (tb.Rows.Count > 0)

                        if (dtp.Kho_HangHoa_ChiTiet_Update(Id_Phieu.Value.ToInt(), KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString()
                                                   , KhoHang_Dropdown.SelectedItem.Value.ToString()
                                                   , HangHoa_Dropdown.SelectedItem.Value.ToString()
                                                   , SoLuong.Value.ToInt(), LoaiPhieu_Dropdown.SelectedItem.Value.ToInt()))
                        {
                            ucMessage.ShowSuccess("Cập nhật thành công.");
                            LoadData();
                            UpdatePanel_View.Update();
                        }
                        else
                        {
                            ucMessage.ShowError("Lỗi hệ thống!"); return;
                        }
                    else
                    {
                        ucMessage.ShowError("Số lượng hàng hóa trong kho hiện tại không đủ. Vui lòng kiểm tra lại số lượng");
                    }
                }
            }
        }
        //}
    }

    public bool CheckInfo()
    {
        bool result = true;
        if (string.IsNullOrEmpty(NhomHang_Dropdown.SelectedItem.Value.ToString()))
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn nhóm hàng hóa");
            return result;
        }
        if (string.IsNullOrEmpty(HangHoa_Dropdown.SelectedItem.Value.ToString()))
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn hàng hóa");
            return result;
        }
        if (SoLuong.Value.ToInt() < 1)
        {
            result = false;
            ucMessage.ShowError("Vui lòng nhập số lượng lớn hơn 0");
            return result;
        }

        if (string.IsNullOrEmpty(LoaiPhieu_Dropdown.SelectedItem.Value.ToString()))
        {
            result = false;
            ucMessage.ShowError("Vui lòng chọn loại phiếu");
            return result;
        }
        if (LoaiPhieu_Dropdown.SelectedItem.Value.ToString() == "3")
        {
            if (string.IsNullOrEmpty(KhoHang_Dropdown.SelectedItem.Value.ToString()))
            {
                result = false;
                ucMessage.ShowError("Vui lòng chọn kho nhận hàng");
                return result;
            }
        }


        //if (string.IsNullOrEmpty(DonViTinh.Value))
        //{
        //    result = false;
        //    ucMessage.ShowError("Vui lòng nhập dơn vị tính");
        //    return result;
        //}
        //if (string.IsNullOrEmpty(DonGia.Value))
        //{
        //    result = false;
        //    ucMessage.ShowError("Vui lòng nhập dơn giá");
        //    return result;
        //}
        //if (!string.IsNullOrEmpty(DonGia.Value))
        //{
        //    int dongia = DonGia.Value.ToInt();
        //    if (dongia == 0)
        //    {
        //        result = false;
        //        ucMessage.ShowError("Vui lòng nhập dơn giá là số và lớn hơn 0");
        //    }
        //    return result;
        //}
        //if (string.IsNullOrEmpty(NhomHang.SelectedItem.Value))
        //{
        //    result = false;
        //    ucMessage.ShowError("Vui lòng chọn nhóm hàng");
        //    return result;
        //}

        //if (string.IsNullOrEmpty(NhaSanXuat.SelectedItem.Value))
        //{
        //    result = false;
        //    ucMessage.ShowError("Vui lòng chọn nhà sản xuất");
        //    return result;
        //}
        return result;
    }

    public void ClearForm()
    {

        NhomHang_Dropdown.ClearSelection();
        NhomHang_Dropdown.SelectedItem.Text = "Chọn nhóm hàng";

        HangHoa_Dropdown.ClearSelection();
        HangHoa_Dropdown.SelectedItem.Text = "Chọn hàng hóa";

        LoaiPhieu_Dropdown.ClearSelection();
        LoaiPhieu_Dropdown.SelectedItem.Text = "Chọn loại phiếu";

        KhoHang_MaThuKho_Dropdown.ClearSelection();
        KhoHang_MaThuKho_Dropdown.SelectedItem.Text = "Chọn kho";

        KhoHang_Dropdown.ClearSelection();
        KhoHang_Dropdown.SelectedItem.Text = "Chọn kho";
        SoLuong.Value = "";
        //NgayNhan.Value = DateTime.Today.ToString("yyyy-MM-dd");
    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }

    protected void LoaiPhieu_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(LoaiPhieu_Dropdown.SelectedItem.Value.ToString()))
        {
            // phiếu nhập kho
            if (LoaiPhieu_Dropdown.SelectedItem.Value.ToString() == "1")
            {
                Visible(false);
            }

            // phiếu nhập xuất
            if (LoaiPhieu_Dropdown.SelectedItem.Value.ToString() == "2")
            {
                Visible(true);
                lb_KhoNhan.Visible = false;
                KhoHang_Dropdown.Visible = false;
                //lb_NgayNhan.Visible = false;
                //NgayNhan.Visible = false;

            }
            // phiếu nhập chuyển
            if (LoaiPhieu_Dropdown.SelectedItem.Value.ToString() == "3")
            {
                Visible(true);
                LoadKhoHangAll(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString() == "" ? "" : KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString());
            }
        }
    }


    public void Visible(bool giaTri)
    {
        //lb_NgayNhan.Visible = giaTri;
        //NgayNhan.Visible = giaTri;


        lb_KhoNhan.Visible = giaTri;
        KhoHang_Dropdown.Visible = giaTri;
    }

    protected void KhoHang_MaThuKho_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadKhoHangAll(KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString() == "" ? "" : KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToString());
    }

    protected void NhomHang_Dropdown_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadHangHoa(NhomHang_Dropdown.SelectedItem.Value.ToString() == "" ? "" : NhomHang_Dropdown.SelectedItem.Value.ToString());
    }
}