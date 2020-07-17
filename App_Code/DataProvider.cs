using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Services;
using CodeUtility;
/// <summary>
/// Summary description for DataProvider
/// </summary>
//namespace DataProvider
//{
public class DataProvider
{
    ConnectionData conect = new ConnectionData();
    SqlConnection cnn = new SqlConnection();
    SqlDataAdapter da = new SqlDataAdapter();
    SqlCommand cmd = new SqlCommand();
    #region Quản lý kho
    #region Thủ kho
    public DataTable ThuKho_List()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_THUKHO_LIST", CommandType.StoredProcedure);
        return dtb;
    }

    public DataTable ThuKho_Find(string maThuKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_THUKHO_FIND", CommandType.StoredProcedure,param);
        return dtb;
    }

    public bool ThuKho_Insert(string maThuKho,string tenThuKho,string matKhau, string loaiTaiKhoan)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        param[1] = new SqlParameter("@TENTHUKHO", tenThuKho);
        param[2] = new SqlParameter("@MATKHAU", matKhau);
        param[3] = new SqlParameter("@ISADMIN", loaiTaiKhoan);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_THUKHO_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool ThuKho_Update(string maThuKho, string tenThuKho, string matKhau, string loaiTaiKhoan)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        param[1] = new SqlParameter("@TENTHUKHO", tenThuKho);
        param[2] = new SqlParameter("@MATKHAU", matKhau);
        param[3] = new SqlParameter("@ISADMIN", loaiTaiKhoan);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_THUKHO_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool ThuKho_Delete(string maThuKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_THUKHO_DELETE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }
    public bool ThuKho_Login(string user, string pass)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@MATHUKHO", user);
        param[1] = new SqlParameter("@MATKHAU", pass);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_THUKHO_FIND_LOGIN", CommandType.StoredProcedure, param);
        if (dtb.Rows.Count > 0)
            return true;
        else
            return false;
    }

    public bool ThuKho_CheckAdmin(string user)
    {
       
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from ThuKho WHERE ISADMIN = 1 AND MATHUKHO  = '" + user + "'", CommandType.Text);
        if (dtb.Rows.Count > 0)
            return true;
        else
            return false;
    }
    #endregion

    #region Thủ kho - kho 

    public bool ThuKho_Kho_Insert(string maThuKho, string makho)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        param[1] = new SqlParameter("@MAKHO", makho);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_THUKHO_KHO_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }
    public bool ThuKho_Kho_Update(int id,string makho)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ID", id);
        param[1] = new SqlParameter("@MAKHO", makho);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_THUKHO_KHO_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool ThuKho_Kho_Delete(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ID", id);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_THUKHO_KHO_DELETE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }
    
    public DataTable ThuKho_Kho_List(string maThuKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_THUKHO_KHO_LIST", CommandType.StoredProcedure, param);
        return dtb;
    }
    public DataTable ThuKho_Kho_List_Kho(string maThuKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", maThuKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_THUKHO_KHO_LIST_MATHUKHO", CommandType.StoredProcedure, param);
        return dtb;
    }
    public DataTable ThuKho_Kho_Find(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ID", id);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_THUKHO_KHO_FIND", CommandType.StoredProcedure, param);
        return dtb;
    }
    #endregion
    #region kho - hàng hóa
  
    public DataTable Kho_HangHoa_List(string maKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAKHO", maKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_HANGHOA_LIST", CommandType.StoredProcedure, param);
        return dtb;
    }
    
    #endregion
    #region Kho
    public DataTable Kho_List(string mathukho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", mathukho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_LIST", CommandType.StoredProcedure,param);
        return dtb;
    }
    //public DataTable Kho_List()
    //{
    //    DataTable dtb = new DataTable();
    //    dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_LIST", CommandType.StoredProcedure);
    //    return dtb;
    //}
    public DataTable Kho_List_MaThuKho_Dropdown(string MaThuKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", MaThuKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_LIST_MATHUKHO", CommandType.StoredProcedure,param);
        return dtb;
    }
    public DataTable Kho_List_Dropdown( string maKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAKHO", maKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_LIST_Dropdown", CommandType.StoredProcedure,param);
        return dtb;
    }
    
    public DataTable Kho_Find(string maKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAKHO", maKho);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_FIND", CommandType.StoredProcedure, param);
        return dtb;
    }

    public bool Kho_Insert(string maKho, string tenKho, string diaDiem)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@MAKHO", maKho);
        param[1] = new SqlParameter("@TENKHO", tenKho);
        param[2] = new SqlParameter("@DIADIEM", diaDiem);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool Kho_Update(string maKho, string tenKho, string diaDiem)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@MAKHO", maKho);
        param[1] = new SqlParameter("@TENKHO", tenKho);
        param[2] = new SqlParameter("@DIADIEM", diaDiem);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool Kho_Delete(string maKho)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAKHO", maKho);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_DELETE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }


    #endregion

    #region Nhà sản xuất
    public DataTable NhaSanXuat_List()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_NHASANXUAT_LIST", CommandType.StoredProcedure);
        return dtb;
    }
     public DataTable NhaSanXuat_List_Dropdown()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_NHASANXUAT_LIST_DROPDOWN", CommandType.StoredProcedure);
        return dtb;
    }
    
    public DataTable NhaSanXuat_Find(string maNhaSanXuat)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MANHASANXUAT", maNhaSanXuat);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_NHASANXUAT_FIND", CommandType.StoredProcedure, param);
        return dtb;
    }

    public bool NhaSanXuat_Insert(string maNhaSanXuat, string tenNhaSanXuat, string diaChi, string soDienThoai)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@MANHASANXUAT", maNhaSanXuat);
        param[1] = new SqlParameter("@TENNHASANXUAT", tenNhaSanXuat);
        param[2] = new SqlParameter("@DIACHI", diaChi);
        param[3] = new SqlParameter("@SODIENTHOAI", soDienThoai);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_NHASANXUAT_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool NhaSanXuat_Update(string maNhaSanXuat, string tenNhaSanXuat, string diaChi, string soDienThoai)
    {
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@MANHASANXUAT", maNhaSanXuat);
        param[1] = new SqlParameter("@TENNHASANXUAT", tenNhaSanXuat);
        param[2] = new SqlParameter("@DIACHI", diaChi);
        param[3] = new SqlParameter("@SODIENTHOAI", soDienThoai);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_NHASANXUAT_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool NhaSanXuat_Delete(string maNhaSanXuat)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MANHASANXUAT", maNhaSanXuat);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_NHASANXUAT_DELETE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }


    #endregion

    #region NHÓM HÀNG
    public DataTable NhomHang_List()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_NHOMHANG_LIST", CommandType.StoredProcedure);
        return dtb;
    }
    public DataTable NhomHang_List_Dropdown()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_NHOMHANG_LIST_DROPDOWN", CommandType.StoredProcedure);
        return dtb;
    }
    public DataTable NhomHang_Find(string maQuanLy)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MANHOMHANG", maQuanLy);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_NHOMHANG_FIND", CommandType.StoredProcedure, param);
        return dtb;
    }

    public bool Nhomhang_Insert(string maQuanLy, string tenMaHang, string ghiChu)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@MANHOMHANG", maQuanLy);
        param[1] = new SqlParameter("@TENNHOMHANG", tenMaHang);
        param[2] = new SqlParameter("@GHICHU", ghiChu);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_NHOMHANG_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool NhomHang_Update(string maQuanLy, string tenMaHang, string ghiChu)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@MANHOMHANG", maQuanLy);
        param[1] = new SqlParameter("@TENNHOMHANG", tenMaHang);
        param[2] = new SqlParameter("@GHICHU", ghiChu);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_NHOMHANG_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool NhomHang_Delete(string maQuanLy)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MANHOMHANG", maQuanLy);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_NHOMHANG_DELETE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }


    #endregion

    #region HÀNG HÓA
    public DataTable HangHoa_List()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_LIST", CommandType.StoredProcedure);
        return dtb;
    }

    public DataTable HangHoa_List_Dropdown()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_LIST_DROPDOWN", CommandType.StoredProcedure);
        return dtb;
    }

    public DataTable HangHoa_List_Dropdown_MaNhomHang(string manhomHang)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MANHOMHANG", manhomHang);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_LIST_DROPDOWN_NHOMHANG", CommandType.StoredProcedure,param);
        return dtb;
    }

    public DataTable HangHoa_Find(string maNhaSanXuat)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAHANG", maNhaSanXuat);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_FIND", CommandType.StoredProcedure, param);
        return dtb;
    }
    public DataTable HangHoa_Find_Check(string maNhaSanXuat)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAHANG", maNhaSanXuat);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_FIND", CommandType.StoredProcedure, param);
        return dtb;
    }
    public bool HangHoa_Insert(string maQuanLy, string tenHangHoa, int donGia, string donViTinh,string nhomHang, string nhaSanXuat)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@MAHANG", maQuanLy);
        param[1] = new SqlParameter("@TENHANG", tenHangHoa);
        param[2] = new SqlParameter("@DONVITINH", donViTinh);
        param[3] = new SqlParameter("@DONGIA", donGia);
        param[4] = new SqlParameter("@MANHOMHANG", nhomHang);
        param[5] = new SqlParameter("@MANHASANXUAT", nhaSanXuat);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_HANGHOA_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool HangHoa_Update(string maQuanLy, string tenHangHoa, int donGia, string donViTinh, string nhomHang, string nhaSanXuat)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@MAHANG", maQuanLy);
        param[1] = new SqlParameter("@TENHANG", tenHangHoa);
        param[2] = new SqlParameter("@DONVITINH", donViTinh);
        param[3] = new SqlParameter("@DONGIA", donGia);
        param[4] = new SqlParameter("@MANHOMHANG", nhomHang);
        param[5] = new SqlParameter("@MANHASANXUAT", nhaSanXuat);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_HANGHOA_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    public bool HangHoa_Delete(string maQuanLy)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MAHANG", maQuanLy);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_HANGHOA_DELETE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }


    #endregion

    #region KHO HÀNG HÓA CHI TIẾT
    public DataTable Kho_HangHoa_ChiTiet_List(string MATHUKHO)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@MATHUKHO", MATHUKHO);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_HANGHOA_CHITIET_List", CommandType.StoredProcedure, param);
        return dtb;
    }

    public DataTable Kho_HangHoa_ChiTiet_List(string MATHUKHO,string LOAIPHIEU)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@MATHUKHO", MATHUKHO);
        param[1] = new SqlParameter("@LOAIPHIEU", LOAIPHIEU);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_HANGHOA_CHITIET_LIST_LOAIPHIEU", CommandType.StoredProcedure, param);
        return dtb;
    }
    public DataTable Kho_HangHoa_Check_SoLuong(string MAKHO, string MAHANG,int SOLUONG)
    {
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@MAKHO", MAKHO);
        param[1] = new SqlParameter("@MAHANG", MAHANG);
        param[2] = new SqlParameter("@SOLUONG", SOLUONG);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_KHOHANG_CHECK_SOLUONG", CommandType.StoredProcedure, param);
        return dtb;
    }

    public DataTable LoaiPhieu_List_Dropdown()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_LOAIPHIEU_LIST_DROPDOWN", CommandType.StoredProcedure);
        return dtb;
    }
    public bool Kho_HangHoa_ChiTiet_Insert(string makho, string khonhan, string mahang,int soluong,string mathukho, int loaiphieu)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@MAKHO", makho);
        param[1] = new SqlParameter("@MAKHONHAN", khonhan);
        param[2] = new SqlParameter("@MAHANG", mahang);
        param[3] = new SqlParameter("@SOLUONG", soluong);
        param[4] = new SqlParameter("@MATHUKHO", mathukho);
        param[5] = new SqlParameter("@LOAIPHIEU", loaiphieu);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_HANGHOA_CHITIET_INSERT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    
    public bool Kho_HangHoa_ChiTiet_Accpet(int id, string thukho)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@ID", id);
        param[1] = new SqlParameter("@THUKHONHAN", thukho);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_KHOHANG_ACCEPT", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }
    public bool Kho_HangHoa_ChiTiet_Update(int id,string makho, string khonhan, string mahang, int soluong, int loaiphieu)
    {
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@ID", id);
        param[1] = new SqlParameter("@MAKHO", makho);
        param[2] = new SqlParameter("@MAKHONHAN", khonhan);
        param[3] = new SqlParameter("@MAHANG", mahang);
        param[4] = new SqlParameter("@SOLUONG", soluong);
        param[5] = new SqlParameter("@LOAIPHIEU", loaiphieu);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_HANGHOA_CHITIET_UPDATE", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }
    public DataTable Kho_HangHoa_ChiTiet_Find(int id)
    {
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@ID", id);
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHO_HANGHOA_CHITIET_FIND_CHECK", CommandType.StoredProcedure, param);
        return dtb;
    }
    public bool Kho_HangHoa_Find_Check(string maKho, string maHang)
    {
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@MAKHO",maKho);
        param[1] = new SqlParameter("@MAHANG", maHang);
        bool result = false;
        if (conect.ExeCuteNonQuery_bool("SPD_KHO_HANGHOA_FIND_CHECK", CommandType.StoredProcedure, param))
            result = true;
        return result;
    }

    //public DataTable HangHoa_Find(string maNhaSanXuat)
    //{
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@", maNhaSanXuat);
    //    DataTable dtb = new DataTable();
    //    dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_FIND", CommandType.StoredProcedure, param);
    //    return dtb;
    //}
    //public DataTable HangHoa_Find_Check(string maNhaSanXuat)
    //{
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@MAHANG", maNhaSanXuat);
    //    DataTable dtb = new DataTable();
    //    dtb = conect.ExeCuteNonQuery_Table("SPD_HANGHOA_FIND", CommandType.StoredProcedure, param);
    //    return dtb;
    //}
    //public bool HangHoa_Insert(string maQuanLy, string tenHangHoa, int donGia, string donViTinh, string nhomHang, string nhaSanXuat)
    //{
    //    SqlParameter[] param = new SqlParameter[6];
    //    param[0] = new SqlParameter("@MAHANG", maQuanLy);
    //    param[1] = new SqlParameter("@TENHANG", tenHangHoa);
    //    param[2] = new SqlParameter("@DONVITINH", donViTinh);
    //    param[3] = new SqlParameter("@DONGIA", donGia);
    //    param[4] = new SqlParameter("@MANHOMHANG", nhomHang);
    //    param[5] = new SqlParameter("@MANHASANXUAT", nhaSanXuat);
    //    bool result = false;
    //    if (conect.ExeCuteNonQuery_bool("SPD_HANGHOA_INSERT", CommandType.StoredProcedure, param))
    //        result = true;
    //    return result;
    //}

    //public bool HangHoa_Update(string maQuanLy, string tenHangHoa, int donGia, string donViTinh, string nhomHang, string nhaSanXuat)
    //{
    //    SqlParameter[] param = new SqlParameter[6];
    //    param[0] = new SqlParameter("@MAHANG", maQuanLy);
    //    param[1] = new SqlParameter("@TENHANG", tenHangHoa);
    //    param[2] = new SqlParameter("@DONVITINH", donViTinh);
    //    param[3] = new SqlParameter("@DONGIA", donGia);
    //    param[4] = new SqlParameter("@MANHOMHANG", nhomHang);
    //    param[5] = new SqlParameter("@MANHASANXUAT", nhaSanXuat);
    //    bool result = false;
    //    if (conect.ExeCuteNonQuery_bool("SPD_HANGHOA_UPDATE", CommandType.StoredProcedure, param))
    //        result = true;
    //    return result;
    //}

    //public bool HangHoa_Delete(string maQuanLy)
    //{
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@MAHANG", maQuanLy);
    //    bool result = false;
    //    if (conect.ExeCuteNonQuery_bool("SPD_HANGHOA_DELETE", CommandType.StoredProcedure, param))
    //        result = true;
    //    return result;
    //}


    #endregion
    #endregion


    #region Sản phẩm
    public DataTable DS_LOAISANPHAMCHA()
    {
        DataTable dtb = new DataTable();
        string sql = "Select * from LoaiSanPhamCha order by TenLoaiSanPhamCha";
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.Text);
        return dtb;
    }
    public DataTable DS_LOAISANPHAM(string LOAISANPHAMCHA)
    {
        DataTable dtb = new DataTable();
        string sql = "SPD_SANPHAM_LOAISANPHAM";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@LOAISANPHAMCHA", LOAISANPHAMCHA);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }

    public DataTable DS_SANPHAM(string LOAISANPHAM)
    {
        DataTable dtb = new DataTable();
        string sql = "SPD_SANPHAM_SANPHAM";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@LOAISANPHAM", LOAISANPHAM);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }

    #endregion

    #region Địa chỉ
    public DataTable TinhThanh()
    {
        DataTable dtb = new DataTable();
        string sql = "SPD_DIACHI_TINHTHANH";
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure);

        return dtb;
    }

    public DataTable QuanHuyen(string tinhThanh)
    {
        DataTable dtb = new DataTable();
        string sql = "SPD_DIACHI_QUANHUYEN";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@TinhThanh", tinhThanh);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }
    public DataTable XaPhuong(string quanHuyen)
    {
        DataTable dtb = new DataTable();
        string sql = "SPD_DIACHI_XAPHUONG";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@QuanHuyen", quanHuyen);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }


    #endregion


    #region Đơn hàng - khách hàng
    public bool KhachHang_Insert(string oid, string ten, string sodienthoai, string diachi)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@OID", oid);
        param[1] = new SqlParameter("@TENKHACHHANG", ten);
        param[2] = new SqlParameter("@SODIENTHOAI", sodienthoai);
        param[3] = new SqlParameter("@DIACHI", diachi);
        if (conect.ExeCuteNonQuery_bool("SPD_KHACHHANG_INSERT", CommandType.StoredProcedure, param))
        {
            return true;
        }
        return false;
    }

    public DataTable KhachHang_GetObject(string oid)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@SODIENTHOAI", oid);
        dtb = conect.ExeCuteNonQuery_Table("SPD_KHACHHANG_GETOBJECT", CommandType.StoredProcedure, param);
        return dtb;
    }

    public bool DonHang_Insert(string oid, string khachhang)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@OID", oid);
        param[1] = new SqlParameter("@KHACHHANG", khachhang);
        param[2] = new SqlParameter("@NGAYLAP", DateTime.Now);
        if (conect.ExeCuteNonQuery_bool("SPD_DONHANG_INSERT", CommandType.StoredProcedure, param))
        {
            return true;
        }
        return false;
    }

    public bool ChiTietDonHang_Insert(string donhang, string sanpham, int soluong, decimal thanhtien)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@DONHANG", donhang);
        param[1] = new SqlParameter("@SANPHAM", sanpham);
        param[2] = new SqlParameter("@SOLUONG", soluong);
        param[3] = new SqlParameter("@THANHTIEN", thanhtien);
        if (conect.ExeCuteNonQuery_bool("SPD_CHITIETDONHANG_INSERT", CommandType.StoredProcedure, param))
        {
            return true;
        }
        return false;
    }
    public DataTable ChiTietDonHang_DanhSach(string oid)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@TAIKHOAN", oid);
        dtb = conect.ExeCuteNonQuery_Table("SPD_CHITIETDONHANG_DANHSACH", CommandType.StoredProcedure, param);
        return dtb;
    }

    #endregion
    #region Chi tiêu cá nhân
    public DataTable DS_ChiTieuCaNhan(Guid nguoiLap)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_DanhSach_DanhSachChiTieuCaNhan";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@NguoiLap", nguoiLap);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }

    public DataTable DS_ThongKeChiTieuCaNhan(Guid nguoiLap, DateTime tuNgay, DateTime denNgay)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_ThongKe_DanhSachKhoanChi";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@NguoiLap", nguoiLap);
        param[1] = new SqlParameter("@TuNgay", tuNgay);
        param[2] = new SqlParameter("@DenNgay", denNgay);


        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }
    public bool Delete_ChiTieuCaNhan(int ma)
    {
        try
        {
            string sql = "DELETE ChiTieuCaNhan WHERE ID = '" + ma + "'";
            conect.ExeCuteNonQuery(sql);
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }

    }
    public DataTable Fine_KeHoach(Guid nguoiLap, DateTime ngayChi)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_Find_KeHoach";
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@NguoiLap", nguoiLap);
        param[1] = new SqlParameter("@Ngay", ngayChi);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);
        return dtb;
    }
    public bool Update_ChiTieuCaNhan(int id, DateTime ngayChi, decimal soTien, string ghiChu)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_Update_ChiTieuCaNhan";
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@ID", id);
        param[1] = new SqlParameter("@NgayChi", ngayChi);
        param[2] = new SqlParameter("@SoTien", soTien);
        param[3] = new SqlParameter("@GhiChu", ghiChu);
        if (conect.ExeCuteNonQuery_bool(sql, CommandType.StoredProcedure, param))
            return true;
        else
            return false;
    }
    public bool Insert_ChiTieuCaNhan(Guid khoanChi, decimal soTien, DateTime ngayChi, string ghiChu, Guid keHoach, Guid nguoiLap)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_Insert_ChiTieuCaNhan";
        SqlParameter[] param = new SqlParameter[6];
        param[0] = new SqlParameter("@KhoanChi", khoanChi);
        param[1] = new SqlParameter("@SoTien", soTien);
        param[2] = new SqlParameter("@NgayChi", ngayChi);
        param[3] = new SqlParameter("@GhiChu", ghiChu);
        param[4] = new SqlParameter("@KeHoach", keHoach);
        param[5] = new SqlParameter("@NguoiLap", nguoiLap);
        if (conect.ExeCuteNonQuery_bool(sql, CommandType.StoredProcedure, param))
            return true;
        else
            return false;
    }
    #endregion
    #region Kế hoạch
    public DataTable Check_KeHoach(DateTime NgayBatDau, Guid nguoilap)
    {
        DataTable dtb = new DataTable();
        string ngay = NgayBatDau.ToShortDateString();
        string sql = "SELECT * FROM dbo.KeHoachChiTieu  WHERE (NgayBatDau <=  '" + ngay + "' AND NgayKetThuc >= '" + ngay + "') and NguoiLap = '" + nguoilap + "'";
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.Text);
        return dtb;
    }
    public bool Delete_KeHoach(int ma)
    {
        try
        {
            string sql = "DELETE KeHoachChiTieu WHERE ID = '" + ma + "'";
            conect.ExeCuteNonQuery(sql);
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }

    }
    public DataTable Find_KeHoach(int ID)
    {
        DataTable dtb = new DataTable();
        string sql = "Select Oid from  KeHoachChiTieu WHERE ID = '" + ID + "'";
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.Text);
        return dtb;

    }
    public DataTable Find_ChiTieuCaNhan_KeHoach(Guid ma)
    {
        DataTable dtb = new DataTable();
        string sql = "Select * from  ChiTieuCaNhan WHERE KeHoach = '" + ma + "'";
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.Text);
        return dtb;

    }
    public DataTable DS_ChiTietKeHoach(Guid nguoiLap)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_DanhSach_DanhSachChiTietKeHoach";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@NguoiLap", nguoiLap);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }
    public DataTable DS_KeHoach(Guid nguoiLap)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_DanhSach_DanhSachKeHoach";
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@NguoiLap", nguoiLap);
        dtb = conect.ExeCuteNonQuery_Table(sql, CommandType.StoredProcedure, param);

        return dtb;
    }

    public bool Update_KeHoach(int id, decimal tongKeHoach, string ghiChu)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_Update_KeHoach";
        SqlParameter[] param = new SqlParameter[3];
        param[0] = new SqlParameter("@ID", id);
        param[1] = new SqlParameter("@Tien", tongKeHoach);
        param[2] = new SqlParameter("@GhiChu", ghiChu);
        if (conect.ExeCuteNonQuery_bool(sql, CommandType.StoredProcedure, param))
            return true;
        else
            return false;
    }
    public bool Insert_KeHoach(Guid oid, string tenKeHoach, DateTime ngayBatDau, DateTime ngayKeThuc, decimal tongKeHoach, string ghiChu, Guid nguoiLap)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_Insert_KeHoach";
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@Oid", oid);
        param[1] = new SqlParameter("@TenKeHoach", tenKeHoach);
        param[2] = new SqlParameter("@NgayBatDau", ngayBatDau);
        param[3] = new SqlParameter("@NgayKetThuc", ngayKeThuc);
        param[4] = new SqlParameter("@TienKeHoach", tongKeHoach);
        param[5] = new SqlParameter("@GhiChu", ghiChu);
        param[6] = new SqlParameter("@NguoiLap", nguoiLap);
        if (conect.ExeCuteNonQuery_bool(sql, CommandType.StoredProcedure, param))
            return true;
        else
            return false;
    }
    public bool Insert_ChiTietKeHoach(Guid khoanChi, decimal tienKeHoach, Guid keHoach, Guid nguoiLap, string ghiChu)
    {
        DataTable dtb = new DataTable();
        string sql = "spd_Insert_ChiTietKeHoach";
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@KhoanChi", khoanChi);
        param[1] = new SqlParameter("@Tien", tienKeHoach);
        param[2] = new SqlParameter("@KeHoach", keHoach);
        param[3] = new SqlParameter("@NguoiLap", nguoiLap);
        param[4] = new SqlParameter("@GhiChu", ghiChu);
        if (conect.ExeCuteNonQuery_bool(sql, CommandType.StoredProcedure, param))
            return true;
        else
            return false;
    }
    #endregion
    #region Tài khoản
    public DataTable TaiKhoan_DanhSachTaiKhoan()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from TAIKHOAN", CommandType.Text);
        return dtb;
    }
    public DataTable TaiKhoan_CheckUser(string ma)
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from TAIKHOAN where Oid = '" + ma + "'", CommandType.Text);
        return dtb;
    }

    public bool TaiKhoan_Login(string user, string pass)
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from TAIKHOAN where TAIKHOAN = '" + user + "' and MATKHAU = N'" + pass + "'", CommandType.Text);
        if (dtb.Rows.Count > 0)
            return true;
        else
            return false;
    }
    public string TaiKhoan_LayOid(string user)
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from TAIKHOAN where TAIKHOAN = '" + user + "'", CommandType.Text);
        if (dtb.Rows.Count > 0)
        {
            foreach (DataRow item in dtb.Rows)
            {
                if (!string.IsNullOrEmpty(item.ItemArray[0].ToString()))
                {
                    return item.ItemArray[0].ToString();
                }
                else
                    return Guid.Empty.ToString();
            }
        }
        else
            return Guid.Empty.ToString();
        return Guid.Empty.ToString();

    }
    public bool TaiKhoan_LoaiTaiKhoan(string user)
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select IsAdmin from TaiKhoanSuDung where UserName = '" + user + "'", CommandType.Text);
        if (dtb.Rows.Count > 0)
        {
            foreach (DataRow item in dtb.Rows)
            {
                if (!string.IsNullOrEmpty(item.ItemArray[0].ToString()))
                {
                    return item.ItemArray[0].ToBool();
                }
                else
                    return false;
            }
        }
        else
            return false;
        return false;

    }
    public bool TaiKhoan_Insert(string user, string pass, string email, string sodienthoai)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[4];
        param[0] = new SqlParameter("@TAIKHOAN", user);
        param[1] = new SqlParameter("@MATKHAU", pass);
        param[2] = new SqlParameter("@EMAIL", email);
        param[3] = new SqlParameter("@SODIENTHOAI", sodienthoai);
        if (conect.ExeCuteNonQuery_bool("SPD_TAIKHOAN_INSERT", CommandType.StoredProcedure, param))
        {
            return true;
        }
        return false;
    }
    public bool TaiKhoan_Update(string ma, string mk, string email, string sodienthoai)
    {
        try
        {
            conect.ExeCuteNonQuery("Update TAIKHOAN set MATKHAU = N'" + mk + "',EMAIL = N'" + email + "' ,SODIENTHOAI = N'" + sodienthoai + "' where OID = N'" + ma + "'");
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public bool TaiKhoan_DoiMatKhau(Guid oid, string mk, string oldmk)
    {
        try
        {
            conect.ExeCuteNonQuery("Update TaiKhoanSuDung set OldPassword = N'" + oldmk + "', Password = N'" + mk + "' where Oid = N'" + oid + "'");
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
    public bool Delete_TaiKhoan(string ma)
    {
        try
        {
            string sql = "DELETE TaiKhoanSuDung WHERE ID = '" + ma + "'";
            conect.ExeCuteNonQuery(sql);
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }

    }
    #endregion
    #region Khoản chi
    public DataTable DS_KhoanChi()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from KhoanChi", CommandType.Text);
        return dtb;
    }

    public DataTable Check_KhoanChi(string ma)
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("Select * from KhoanChi where MaQuanLy = '" + ma + "'", CommandType.Text);
        return dtb;
    }
    public bool Insert_KhoanChi(string ma, string ten)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[2];
        param[0] = new SqlParameter("@MaQuanLy", ma);
        param[1] = new SqlParameter("@TenKhoanChi", ten);
        if (conect.ExeCuteNonQuery_bool("spd_Insert_KhoanChi", CommandType.StoredProcedure, param))
        {
            return true;
        }
        return false;
    }
    public bool Update_KhoanChi(string ma, string ten)
    {
        try
        {
            conect.ExeCuteNonQuery("Update KhoanChi set TenKhoanChi = N'" + ten + "' where MaQuanLy = N'" + ma + "'");
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }

    }
    public bool Delete_KhoanChi(string ma)
    {
        try
        {
            string sql = "DELETE KhoanChi WHERE MaQuanLy = '" + ma + "'";
            conect.ExeCuteNonQuery(sql);
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }

    }
    #endregion
    #region Old


    public bool Create_DatMuaTapChi(string Oid, string NguoiNhan, string DiaChiLienLac, string DiaChiNguoiNhan,
                                    string Email, string SDT, string SDTDD, string DatMuaTapChi, int TongSoLuong,
                                    int TongThanhTien, int HinhThucChuyenTien, string DiaChiChuyenTien)
    {
        try
        {
            SqlParameter[] param = new SqlParameter[12];
            param[0] = new SqlParameter("@Oid", Oid);
            param[1] = new SqlParameter("@NguoiNhan", NguoiNhan);
            param[2] = new SqlParameter("@DiaChiLienLac", DiaChiLienLac);
            param[3] = new SqlParameter("@DiaChiNguoiNhan", DiaChiNguoiNhan);
            param[4] = new SqlParameter("@Email", Email);
            param[5] = new SqlParameter("@SDT", SDT);
            param[6] = new SqlParameter("@SDTDD", SDTDD);
            param[7] = new SqlParameter("@DatMuaTapChi", DatMuaTapChi);
            param[8] = new SqlParameter("@TongSoLuong", TongSoLuong);
            param[9] = new SqlParameter("@TongThanhTien", TongThanhTien);
            param[10] = new SqlParameter("@HinhThucChuyenTien", HinhThucChuyenTien);
            param[11] = new SqlParameter("@DiaChiChuyenTien", DiaChiChuyenTien);
            conect.ExeCuteNonQuery_Void("spd_Portal_TapChi_DatMuaTapChi", CommandType.StoredProcedure, param);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }


    }
    public void Create_ChiTietDatMuaTapChi(string Oid, string OidDatMua, int NamXuatBan, int SoLuong,
                                   int DonGiaTapChi, int ThanhTien, string OidSoBao)
    {
        SqlParameter[] param = new SqlParameter[7];
        param[0] = new SqlParameter("@Oid", Oid);
        param[1] = new SqlParameter("@OidDatMua", OidDatMua);
        param[2] = new SqlParameter("@NamXuatBan", NamXuatBan);
        param[3] = new SqlParameter("@SoLuong", SoLuong);
        param[4] = new SqlParameter("@DonGiaTapChi", DonGiaTapChi);
        param[5] = new SqlParameter("@ThanhTien", ThanhTien);
        param[6] = new SqlParameter("@OidSoBao", OidSoBao);
        conect.ExeCuteNonQuery_Void("spd_Portal_TapChi_ChiTietDatMuaTapChi", CommandType.StoredProcedure, param);
    }

    // viết gọn
    public DataTable LoadTapTinBaiBao(string baibao)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[1];
        param[0] = new SqlParameter("@Oid", baibao);
        dtb = conect.ExeCuteNonQuery_Table("spd_Portal_TapTinBaiBao", CommandType.StoredProcedure, param);
        return dtb;
    }
    public DataTable DS_BaiBaoMoiXuatBan()
    {
        DataTable dtb = new DataTable();
        dtb = conect.ExeCuteNonQuery_Table("spd_Portal_DanhSachBaiBaoMoiXuatBan", CommandType.StoredProcedure);

        return dtb;
    }
    #endregion

    // THÔNG KÊ
    public DataTable ThongKe(string mathukho, string loaiphie, string makho, string tungay, string dengay)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@MATHUKHO", mathukho);
        param[1] = new SqlParameter("@LOAIPHIEU", loaiphie);
        param[2] = new SqlParameter("@MAKHO", makho);
        param[3] = new SqlParameter("@TUNGAY", tungay);
        param[4] = new SqlParameter("@DENGAY", dengay);
        dtb = conect.ExeCuteNonQuery_Table("SPD_THONGKE_PHIEUHANGHOA", CommandType.StoredProcedure, param);
        return dtb;
    }

    public DataTable ThongKe_TongSoLuong(string mathukho, string loaiphie, string makho, string tungay, string dengay)
    {
        DataTable dtb = new DataTable();
        SqlParameter[] param = new SqlParameter[5];
        param[0] = new SqlParameter("@MATHUKHO", mathukho);
        param[1] = new SqlParameter("@LOAIPHIEU", loaiphie);
        param[2] = new SqlParameter("@MAKHO", makho);
        param[3] = new SqlParameter("@TUNGAY", tungay);
        param[4] = new SqlParameter("@DENGAY", dengay);
        dtb = conect.ExeCuteNonQuery_Table("SPD_THONGKE_PHIEUHANGHOA_TONGSOLUONG", CommandType.StoredProcedure, param);
        return dtb;
    }
}


