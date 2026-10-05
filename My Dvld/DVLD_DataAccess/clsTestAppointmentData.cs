using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_DataAccess
{
    public class clsTestAppointmentData
    {

     public static bool GetTestAppointmentByID(int TestAppointmentID,ref int TestTypeID,ref int LocalDrivingLicenseApplictionID,
     ref DateTime ApplicationDate,ref decimal PaidFees,ref int CreateByUserID,ref bool IsLocked ){


         bool IsFind=false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "select * from TestAppointments where TestAppointmentID = @TestAppointmentID";
            SqlCommand cmd = new SqlCommand(query, conn);

            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
            conn.Open();
            SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read()) {

                    TestTypeID = (int)dr["TestTypeID"];
                    LocalDrivingLicenseApplictionID = (int)dr["LocalDrivingLicenseApplication"];
                    ApplicationDate = (DateTime)dr["ApplicationDate"];
                    PaidFees = (decimal)dr["PaidFees"];
                    CreateByUserID = (int)dr["CreateByUserID"];
                    IsLocked = (bool)dr["IsLocked"];
                }


            }
            catch (Exception ex)
            {   //Console.WriteLine("Error: " + ex.Message);
                IsFind = false; }



            finally { conn.Close(); }

            return IsFind;

     }
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID,
              ref int TestTypeID, ref int LocalDrivingLicenseApplicationID,
              ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {

                    // The record was found
                    isFound = true;
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    IsLocked = (bool)reader["IsLocked"];

                    if (reader["RetakeTestApplicationID"] == DBNull.Value)
                        RetakeTestApplicationID = -1;
                    else
                        RetakeTestApplicationID = (int)reader["RetakeTestApplicationID"];

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

        public static DataTable GetAllTestAppointments()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select * from TestAppointments_View
                                  order by AppointmentDate Desc";


            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }

        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TestAppointmentID, AppointmentDate,PaidFees, IsLocked
                        FROM TestAppointments
                        WHERE  
                        (TestTypeID = @TestTypeID) 
                        AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
                        order by TestAppointmentID desc;";


            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);


            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)

                {
                    dt.Load(reader);
                }

                reader.Close();


            }

            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;

        }



        public static int AddNewTestAppointment(
             int TestTypeID, int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate, float PaidFees, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            Values (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,0,@RetakeTestApplicationID);
                
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestAppointmentID;

        }

        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID,
             DateTime AppointmentDate, float PaidFees,
             int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestAppointments  
                            set TestTypeID = @TestTypeID,
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID,
                                AppointmentDate = @AppointmentDate,
                                PaidFees = @PaidFees,
                                CreatedByUserID = @CreatedByUserID,
                                IsLocked=@IsLocked,
                                RetakeTestApplicationID=@RetakeTestApplicationID
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);





            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }

            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }



        public static bool CheckIfTheAppointmentIsLocked(int LocalDrivingLincenseID, int TestTypeID)
        {
            bool IsFind = false;

            SqlConnection connection = new SqlConnection(
                clsDataAccessSettings.ConnectionString);

            string query = @"SELECT TOP 1 *
                             FROM TestAppointments 
                            WHERE LocalDrivingLicenseApplicationID = @LDLID 
                            AND TestTypeID = @TestTypeID 
                            AND IsLocked = 0";

            SqlCommand cmd = new SqlCommand(query, connection);

            cmd.Parameters.AddWithValue("@LDLID", LocalDrivingLincenseID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    IsFind = true;
                }
            }
            catch (Exception ex)
            {
                IsFind = false;
            }
            finally
            {
                connection.Close();
            }

            return IsFind;
        }

        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select TestID from Tests where TestAppointmentID=@TestAppointmentID;";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestID = insertedID;
                }
            }

            catch (Exception ex)
            {
                //Console.WriteLine("Error: " + ex.Message);

            }

            finally
            {
                connection.Close();
            }


            return TestID;

        }

    }


}
