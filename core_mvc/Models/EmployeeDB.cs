using System.Data;
using System.Data.SqlClient;
namespace core_mvc.Models
{
    public class EmployeeDB
    {
        SqlConnection con = new SqlConnection(@"server=LAPTOP-USLUAF59\SQLEXPRESS;database=aspcore;integrated security=true");
        public string InsertDB(EmployeeInsert objcls)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("sp_insert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empna", objcls.emp_name);
                cmd.Parameters.AddWithValue("@empaddr", objcls.emp_address);
                cmd.Parameters.AddWithValue("@empsal", objcls.emp_salary);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return ("Inserted Successfully");
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }

        public string LoginDB(EmployeeInsert objcls)
        {
            try
            {
                int cid = 0;
                SqlCommand cmd = new SqlCommand("sp_login", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@eid", objcls.emp_id);
                cmd.Parameters.AddWithValue("@ena", objcls.emp_name);
                con.Open();
                cid = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                con.Close();
                return cid.ToString();

            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                return ex.Message.ToString();
            }
        }
        public EmployeeInsert selectprofiledb(int id)
        {
            var getdata = new EmployeeInsert();
            try
            {
                SqlCommand cmd = new SqlCommand("sp_profileview", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@empid", id);
                con.Open();
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    getdata = new EmployeeInsert
                    {
                        emp_id = Convert.ToInt32(sdr["emp_id"]), //set
                        emp_name = sdr["emp_name"].ToString(),
                        emp_address = sdr["emp_address"].ToString(),
                        emp_salary = sdr["emp_salary"].ToString(),
                    };
                }
                con.Close();
                return getdata;
            }
            catch(Exception ex)
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
                throw;
            }
        }
    }
}
