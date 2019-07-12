using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    class SQLAT
    {
        static void Main()
        {
            using (var con = new SqlConnection(@"Data Source=DESKTOP-HDLDNCT\SQLEXPRESS; Initial Catalog =Testing; Integrated Security =true"))
            {
             
                var cmd = new SqlCommand();
                cmd.CommandText = "select * from dbo.Trans1";
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                //var param = new SqlParameter("@hi");
                //param.SqlDbType = SqlDbType.Structured;
                //param.TypeName = "dbo.hity";
                //cmd.Parameters.Add(param);

                con.Open();
                cmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                con.Close();
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    
                    Console.Write(dr["ID"].ToString() + " | " );
                    Console.Write(dr["Trans"].ToString() + " | ");
                    Console.WriteLine(dr["FK_Kelurahan"].ToString());
                }

                Console.Read();
            }
        }
    }
}
