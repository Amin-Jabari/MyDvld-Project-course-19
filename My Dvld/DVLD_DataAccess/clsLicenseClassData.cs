using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsLicenseClassData
    {


        public static bool GetLicenseClassByClassID(int LicenseClassID,ref string ClassName,ref string ClassDescription,ref byte MinimumAllowedAge, ref byte ValidityLength,ref float ClassFees){

          bool isfind=false;

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from LicenseClasses where LicenseClassID = @LicenseClassID";
            SqlCommand cmd = new SqlCommand(query,conn);


            cmd.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);


            try
            {

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {

                    ClassName = (string)reader["ClassName"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    ValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                    isfind = true;
                }

            }


            catch (Exception ex)
            {
               Console.WriteLine("Error", ex.Message);
                isfind = false;
            }
            finally
            {
                conn.Close();
            }
             return isfind;   
        }
        public static bool GetLicenseClassInfoByClassName(string ClassName, ref int LicenseClassID,
           ref string ClassDescription, ref byte MinimumAllowedAge,
          ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ClassName", ClassName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;
                    LicenseClassID = (int)reader["LicenseClassID"];
                    ClassDescription = (string)reader["ClassDescription"];
                    MinimumAllowedAge = (byte)reader["MinimumAllowedAge"];
                    DefaultValidityLength = (byte)reader["DefaultValidityLength"];
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);

                }
                else
                {
                    // The record was not found
                    isFound = false;
                }

                reader.Close();


            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static DataTable GetAllLicenseClassTable()
        {

            DataTable dt = new DataTable();

            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from LicenseClasses order by ClassName ";
            SqlCommand cmd = new SqlCommand(query, conn);




            try
            {

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {

                  dt.Load(reader);
                }

            }


            catch (Exception ex)
            {
               // Console.WriteLine("Error", ex.Message);
            
            }
            finally
            {
                conn.Close();
            }
            return dt;
        }


    }


}
