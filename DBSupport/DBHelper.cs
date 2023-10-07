using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APIAutomation.Configuration;

namespace APIAutomation.Support
{
    public class DBHelper
    {
        private ReadConfig _readConfig;

        public DBHelper(ReadConfig readConfig)
        {
            _readConfig = readConfig;
        }

        public string GetConnectionString()
        {

            return _readConfig.GetConfigData()["DatabaseConfiguration:"+ (_readConfig.GetConfigData()["Environment"]) + "DB"];

        }

        public DataTable GetDataFromDB(string Query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using(SqlCommand command = new SqlCommand(Query,connection))
                    {
                        connection.Open();
                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataSet dt = new DataSet();
                        da.Fill(dt);
                        if(dt.Tables.Count>0)
                        {
                            return dt.Tables[0];
                        }
                        else
                        {
                            return new DataTable();
                        }


                    }

                }



            }
            catch (Exception ex)
            {

                throw new Exception("Found exception in GetDataFromDB method as " + ex.Message);
            }



        }

        public void ClearDB(string Query)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(GetConnectionString()))
                {
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }


            catch (Exception ex)
            {

                throw new Exception("Found exception in ClearDB method as " + ex.Message);

            }


        }


    }
}
