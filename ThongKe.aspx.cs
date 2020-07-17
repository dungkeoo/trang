using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodeUtility;
using Commons;
using EnumType;
using System.IO;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;

public partial class ThongKe : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.btn_Luu);
            ScriptManager.GetCurrent(this).RegisterPostBackControl(this.Export);


            LoadNhaSanXuat();
            LoadNhomHang();
            LoadLoaiPhieu();
            LoadKhoHang();
            TuNgay.Text = Common.SetTime(DateTime.Today, SetTimeEnum.StartMonth).ToString("yyyy-MM-dd");
            DenNgay.Text = Common.SetTime(DateTime.Today, SetTimeEnum.EndMonth).ToString("yyyy-MM-dd");
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
    public void LoadLoaiPhieu()
    {
        DataProvider dt = new DataProvider();
        LoaiPhieu_Dropdown.DataSource = dt.LoaiPhieu_List_Dropdown();
        LoaiPhieu_Dropdown.DataValueField = "ID";
        LoaiPhieu_Dropdown.DataTextField = "TENPHIEU";
        LoaiPhieu_Dropdown.DataBind();
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
        string mathukho = SessionUtility.AdminOid == "" ? SessionUtility.UserOid : "ADMIN";
        string loaiphieu = LoaiPhieu_Dropdown.SelectedItem.Value.ToSafetyString();
        string makho = KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString() == "" ? SessionUtility.AdminOid == "" ? "" : "ADMIN" : KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString();
        string tungay = TuNgay.Text.ToDateTime().ToShortDateString();
        string denngay = DenNgay.Text.ToDateTime().ToShortDateString();

        //conver datetime
        //DateTime theDate  = DateTime.ParseExact(TuNgay.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        //string dateToInsert=theDate.ToString("dd/MM/yyyy") ;
        
 
        DataProvider dt = new DataProvider();
        Repeater_Data_List.DataSource = dt.ThongKe(mathukho, loaiphieu, makho, tungay, denngay);
        Repeater_Data_List.DataBind();

    }

    public void LoadDataTongSoLuong()
    {
        string mathukho = SessionUtility.AdminOid == "" ? SessionUtility.UserOid : "ADMIN";
        string loaiphieu = LoaiPhieu_Dropdown.SelectedItem.Value.ToSafetyString();
        string makho = KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString() == "" ? SessionUtility.AdminOid == "" ? "" : "ADMIN" : KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString();
        string tungay = TuNgay.Text.ToDateTime().ToShortDateString();
        string denngay = DenNgay.Text.ToDateTime().ToShortDateString();
        DataProvider dt = new DataProvider();
        TongSoLuong.DataSource = dt.ThongKe_TongSoLuong(mathukho, loaiphieu, makho, tungay, denngay);
        TongSoLuong.DataBind();
    }
    public DataTable ReturnData()
    {
        string mathukho = SessionUtility.AdminOid == "" ? SessionUtility.UserOid : "ADMIN";
        string loaiphieu = LoaiPhieu_Dropdown.SelectedItem.Value.ToSafetyString();
        string makho = KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString() == "" ? SessionUtility.AdminOid == "" ? "" : "ADMIN" : KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString();
        string tungay = TuNgay.Text.ToDateTime().ToShortDateString();
        string denngay = DenNgay.Text.ToDateTime().ToShortDateString();
        DataProvider dt = new DataProvider();
        return dt.ThongKe(mathukho, loaiphieu, makho, tungay, denngay);
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
            NhaSanXuat.SelectedItem.Text = row["TENNHASANXUAT"].ToSafetyString();

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
        NhomHang.SelectedItem.Text = "Chọn nhóm hàng";
        NhaSanXuat.SelectedItem.Text = "Chọn nhà sản xuất";
    }


    protected void btn_Huy_Click(object sender, EventArgs e)
    {
        CloseModal("DeleteObjectModal");
        LoadData();
    }

    protected void ExportExcel(object sender, EventArgs e)
    {
        //conver datetime
        DateTime tuNgay = DateTime.ParseExact(TuNgay.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        string tu = tuNgay.ToString("dd-MM-yyyy");

        DateTime denNgay = DateTime.ParseExact(TuNgay.Text, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        string den = denNgay.ToString("dd-MM-yyyy");

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(ReturnData(), "Sheet1");

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=ThongKe_"+ tu+ "_"+den+".xlsx");
            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                MyMemoryStream.WriteTo(Response.OutputStream);
                Response.Flush();
                Response.End();
            }
        }
        //string id = Request.QueryString["makho"].ToSafetyString();
        //string constr = ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        //using (SqlConnection con = new SqlConnection(constr))
        //{
        //    using (SqlCommand cmd = new SqlCommand("SPD_THONGKE_PHIEUHANGHOA"))
        //    {
        //        using (SqlDataAdapter sda = new SqlDataAdapter())
        //        {
        //            string mathukho = SessionUtility.AdminOid == "" ? SessionUtility.UserOid : "ADMIN";
        //            string loaiphieu = LoaiPhieu_Dropdown.SelectedItem.Value.ToSafetyString();
        //            string makho = KhoHang_MaThuKho_Dropdown.SelectedItem.Value.ToSafetyString();
        //            DateTime tungay = TuNgay.Text.ToDateTime();
        //            DateTime denngay = DenNgay.Text.ToDateTime();
        //            cmd.Connection = con;
        //            cmd.CommandType = CommandType.StoredProcedure;
        //            cmd.Parameters.AddWithValue("@MATHUKHO", mathukho);
        //            cmd.Parameters.AddWithValue("@LOAIPHIEU", loaiphieu);
        //            cmd.Parameters.AddWithValue("@MAKHO", makho);
        //            cmd.Parameters.AddWithValue("@TUNGAY", tungay);
        //            cmd.Parameters.AddWithValue("@DENGAY", denngay);

        //            sda.SelectCommand = cmd;
        //            using (DataTable dt = new DataTable())
        //            {
        //                sda.Fill(dt);
        //                using (XLWorkbook wb = new XLWorkbook())
        //                {
        //                    wb.Worksheets.Add(dt, "Sheet1");

        //                    Response.Clear();
        //                    Response.Buffer = true;
        //                    Response.Charset = "";
        //                    Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //                    Response.AddHeader("content-disposition", "attachment;filename=ThongKe.xlsx");
        //                    using (MemoryStream MyMemoryStream = new MemoryStream())
        //                    {
        //                        wb.SaveAs(MyMemoryStream);
        //                        MyMemoryStream.WriteTo(Response.OutputStream);
        //                        Response.Flush();
        //                        Response.End();
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
    }

    protected void ThongKe_Click(object sender, EventArgs e)
    {
        LoadData();
        LoadDataTongSoLuong();
        UpdatePanel_View.Update();
    }
}