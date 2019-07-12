using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using ExcelDataReader;

namespace ConsoleApp2
{
    class Program2
    {
        static void Second()
        {
            using (var con = new SqlConnection(@"Data Source=DESKTOP-HDLDNCT\SQLEXPRESS; Initial Catalog =Hoho; Integrated Security =true"))
            {
                DataSet hasil;
                string filepath = @"D:\Superindo\Task\TESt.xlsx";
                using (var stream = File.Open(filepath, FileMode.Open, FileAccess.Read))
                {
                    IExcelDataReader reader;
                    if (filepath.EndsWith(".xls"))
                    {
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    }
                    else if (filepath.EndsWith(".xlsx"))
                    {
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        reader = null;
                    }


                    hasil = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                            FilterRow = rowReader => rowReader.Depth > 4,
                            FilterColumn = (rowReader, colIndex) => colIndex < 2
                        }
                    });
                    reader.Close();
                }
                DataTable dt = hasil.Tables[0];

                //DataTable dt = new DataTable();
                //dt.Clear();
                //dt.Columns.Add("f1");
                //dt.Columns.Add("f2");
                //DataRow _ravi = dt.NewRow();
                //_ravi["f1"] = "3";
                //_ravi["f2"] = "5";
                //dt.Rows.Add(_ravi);



                //var bulk = new SqlBulkCopy(con);



                var cmd = new SqlCommand();
                cmd.CommandText = "sphi";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = con;

                var param = new SqlParameter("@hi", dt);
                param.SqlDbType = SqlDbType.Structured;
                param.TypeName = "dbo.hity";
                cmd.Parameters.Add(param);

                con.Open();
                cmd.ExecuteNonQuery();
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                con.Close();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Console.WriteLine(dr["f1"].ToString());
                    Console.WriteLine(dr["f2"].ToString());
                }

                Console.Read();
            }
        }
    }
}
