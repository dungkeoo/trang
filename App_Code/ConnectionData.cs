using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ClosedXML.Excel;
/// <summary>
/// Summary description for ConnectionData
/// </summary>
public class ConnectionData
{

    SqlConnection cnn;
    SqlDataAdapter da;
    DataSet ds;
    SqlCommand cmd;
    public static SqlConnection GetStringConnection(string datasource, string database, string username, string password)

    {
        //
        // Data Source=TRAN-VMWARE\SQLEXPRESS;Initial Catalog=simplehr;Persist Security Info=True;User ID=sa;Password=12345
        //
        //string connString = @"Data Source=" + datasource +
        //    ";Initial Catalog=" + database +
        //    ";Persist Security Info=True;User ID=" + username +
        //    ";Password=" + password +
        //    "; Connection Timeout = 0; Max Pool Size = 100; Pooling = True; ";
        string connString = @"Data Source=DESKTOP-55QGD34\SQLEXPRESS;Initial Catalog=QuanLyKhoHang;Integrated Security=SSPI;";

        //string connString = "Server=DESKTOP-55QGD34\SQLEXPRESS,Authentication=Windows Authentication, Database=QuanLyKhoHang;";
       
        SqlConnection conn = new SqlConnection(connString);

        return conn;
    }

    public SqlConnection DBConnection()
    {
        //string datasource = @".";
        //string database = "QuanLyKhoHang";
        //string username = "sa";
        //string password = "123456";


        string datasource = @"DESKTOP-55QGD34\SQLEXPRESS";
        string database = "QuanLyKhoHang";
        string username = "";
        string password = "";
        return GetStringConnection(datasource, database, username, password);
    }

    public void connect()
    {
        if (cnn == null)
            cnn = DBConnection();
        if (cnn.State == ConnectionState.Closed)
            cnn.Open();
    }
    public void disconnect()
    {
        if (cnn == null)
            cnn = DBConnection();
        if (cnn.State == ConnectionState.Open)
            cnn.Close();
    }
    public void ExeCuteNonQuery(string sql)
    {
        cnn = DBConnection();
        cmd = new SqlCommand(sql, cnn);
        cmd.CommandType = CommandType.Text;
        connect();
        cmd.ExecuteNonQuery();
        disconnect();
    }

    public SqlDataReader ExecuteReaderForSQL(SqlCommand sCmd)
    {
        try
        {
            if (sCmd.Connection.State != ConnectionState.Closed) sCmd.Connection.Close();
            if (sCmd.Connection.State != ConnectionState.Open)
            {
                try
                {
                    sCmd.Connection.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            System.Data.SqlClient.SqlDataReader rv = sCmd.ExecuteReader(CommandBehavior.CloseConnection);
            sCmd.Parameters.Clear();
            return rv;
        }
        catch (Exception ex)
        {
            sCmd.Connection.Close();
            throw new Exception(ex.Message);
        }
    }

    public SqlParameter CreateInputParameterForSQL(SqlCommand sqlDbCmd, string prmName, SqlDbType sqlDbType, object value)
    {
        SqlParameter sPrm = sqlDbCmd.CreateParameter();
        sPrm.ParameterName = prmName;
        sPrm.SqlDbType = sqlDbType;
        sPrm.Direction = ParameterDirection.Input;
        sPrm.Value = value;
        return sPrm;
    }

    public DataTable ExeCuteNonQuery_Table(string sql, CommandType type)
    {
        cnn = DBConnection();
        cmd = new SqlCommand(sql, cnn);
        cmd.CommandType = type;
        connect();
        cmd.ExecuteNonQuery();
        da = new SqlDataAdapter();
        da.SelectCommand = cmd;
        DataTable dtb = new DataTable();
        da.Fill(dtb);
        disconnect();
        return dtb;
    }

    public DataTable ExeCuteNonQuery_Table(string sql, CommandType type, SqlParameter[] param)
    {
        cnn = DBConnection();
        using (cmd = new SqlCommand(sql, cnn))
        {
            cmd.CommandType = type;
            foreach (var item in param)
            {
                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            connect();
            cmd.ExecuteNonQuery();
            da = new SqlDataAdapter();
            da.SelectCommand = cmd;
            DataTable dtb = new DataTable();
            da.Fill(dtb);
            disconnect();
            return dtb;
        }
    }

   
    public void ExeCuteNonQuery_Void(string sql, CommandType type, SqlParameter[] param)
    {
        cnn = DBConnection();
        using (cmd = new SqlCommand(sql, cnn))
        {
            cmd.CommandType = type;
            foreach (var item in param)
            {
                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            connect();
            cmd.ExecuteNonQuery();
            disconnect();

        }
    }

    public bool ExeCuteNonQuery_bool(string sql, CommandType type, SqlParameter[] param)
    {
        cnn = DBConnection();
        using (cmd = new SqlCommand(sql, cnn))
        {
            cmd.CommandType = type;
            foreach (var item in param)
            {
                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            connect();
            try
            {
                cmd.ExecuteNonQuery();
                disconnect();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public bool ExeCuteNonQuery_bool(string sql, CommandType type)
    {
        cnn = DBConnection();
        using (cmd = new SqlCommand(sql, cnn))
        {
            cmd.CommandType = type;
            connect();
            try
            {
                cmd.ExecuteNonQuery();
                disconnect();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
    public DataSet GetDataSet(string sql)
    {
        DBConnection();
        da = new SqlDataAdapter(sql, cnn);
        ds = new DataSet();
        da.Fill(ds);
        disconnect();
        return ds;
    }

    public DataSet GetDataSet(string sql, CommandType type)
    {
        DBConnection();
        da = new SqlDataAdapter(sql, cnn);
        ds = new DataSet();
        da.Fill(ds);

        disconnect();
        return ds;
    }

    public DataSet GetDataSet(string sql, CommandType type, SqlParameter[] param)
    {
        DBConnection();
        using (cmd = new SqlCommand(sql, cnn))
        {
            cmd.CommandType = type;
            foreach (var item in param)
            {
                cmd.Parameters.AddWithValue(item.ParameterName, item.Value);
            }
            connect();
            cmd.ExecuteReader();
            da = new SqlDataAdapter();
            ds = new DataSet();

            da.Fill(ds);
            disconnect();
            return ds;
        }
    }

    //Tải file về gồm Đường dẫn file và tên file, oid file
    //public void ProcessRequest()
    //{
    //    string oidbaibao = Request.QueryString["oid"].ToSafetyString();

    //    //
    //    SqlParameter[] param = new SqlParameter[1];
    //    param[0] = new SqlParameter("@Oid", oidbaibao);
    //    using (DataTable dt = connect.ExeCuteNonQuery_Table("spd_Portal_TapTinBaiBao", CommandType.StoredProcedure, param))
    //    {
    //        if (dt != null && dt.Rows.Count > 0)
    //        {
    //            string filepath = dt.Rows[0]["DuongDanTapTin"].ToString();
    //            string filename = dt.Rows[0]["TenTapTin"].ToString();
    //            //
    //            FileStream MyFileStream = new FileStream(filepath, FileMode.Open);
    //            long FileSize = MyFileStream.Length;
    //            byte[] Buffer = new byte[(int)FileSize];
    //            MyFileStream.Read(Buffer, 0, (int)MyFileStream.Length);
    //            MyFileStream.Close();
    //            Context.Response.ContentType = "application/octet-stream";
    //            Context.Response.AddHeader("content-disposition", "attachment; filename=" + filename + "");
    //            Context.Response.BinaryWrite(Buffer);
    //            Context.Response.Flush();
    //            Context.Response.End();
    //        }
    //    }
    //}
}