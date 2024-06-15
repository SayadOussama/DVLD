using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_DataAccessLayer
{
    public  class clsDataAccessTestAppointements
    {
        public static bool FindTestAppointmentByID(int TestAppointmentID, ref int TestTypeID,  ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate,ref decimal PaidFees,ref int CreateByUserID,ref bool IsLocked, ref int ReTakeTestAppID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "Select * From TestAppointments where TestAppointmentID = @TestAppointmentID";

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
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreateByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                    {
                        ReTakeTestAppID = (int)reader["RetakeTestApplicationID"];
                    }
                    else
                    {
                        ReTakeTestAppID = -1;
                    }


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
        public static bool FindTestAppointmentByLocalDrivingLicenseApplication(ref int TestAppointmentID, ref int TestTypeID,  int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref decimal PaidFees, ref int CreateByUserID, ref bool IsLocked,ref int ReTakeTestAppID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                                                                                                                                             //Vision Test Allways TestType ID =1
            string query = "Select * From TestAppointments where LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID and TestTypeID =1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestTypeID = (int)reader["TestTypeID"];
                    AppointmentDate = (DateTime)reader["AppointmentDate"];
                    PaidFees = (decimal)reader["PaidFees"];
                    CreateByUserID = (int)reader["CreatedByUserID"];
                    IsLocked = (bool)reader["IsLocked"];
                    if (reader["RetakeTestApplicationID"] != DBNull.Value)
                    {
                        ReTakeTestAppID = (int)reader["RetakeTestApplicationID"];
                    }
                    else
                    {
                        ReTakeTestAppID = -1;
                    }


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
        public static bool IsISAllReadyHaveAppointmentSearchingByLDLAID(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
                                                                                                                              //Vision Test Allways TestType ID =1                         
            string query = @"select top 1 found =1
                      from LocalDrivingLicenseApplications INNER JOIN 
                      TestAppointments on LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID =TestAppointments.LocalDrivingLicenseApplicationID
                      where 
                      (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID )and
                      (TestAppointments.TestTypeID = @TestTypeID)and TestAppointments.IsLocked= 0 
                      order BY TestAppointments.TestAppointmentID  DESC";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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
        public static bool IsISAllReadyHaveAppointmentAndFile(int LocalDrivingLicenseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            //Vision Test Allways TestType ID =1                         
            string query = @"SELECT Found=1 
               FROM TestAppointments INNER JOIN
               Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
			   where (Tests.TestResult =0 and TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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
        


        public static int AddNewTestAppointment( int TestTypeID, int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  decimal PaidFees, int CreateByUserID,  bool IsLocked, int ReTakeTestAppID)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO TestAppointments ( TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate,PaidFees,CreatedByUserID,IsLocked,ReTakeTestApplicationID)
                             VALUES (@TestTypeID, @LocalDrivingLicenseApplicationID, @AppointmentDate, @PaidFees,@CreatedByUserID,@IsLocked,@ReTakeTestApplicationID);
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreateByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (ReTakeTestAppID != -1 && ReTakeTestAppID != null)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", ReTakeTestAppID);

            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);




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
        public static bool UpdateTestAppointment( int TestAppointmentID,  int TestTypeID, int LocalDrivingLicenseApplicationID,  DateTime AppointmentDate,  decimal PaidFees,  int CreatedByUserID,  bool  IsLocked,int ReTakeTestAppID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  TestAppointments  
                            set 
                               
                                TestTypeID = @TestTypeID, 
                                LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, 
                                AppointmentDate = @AppointmentDate, 
                                PaidFees = @PaidFees,
                                CreatedByUserID =@CreatedByUserID,
                                IsLocked =@IsLocked,
                                RetakeTestApplicationID = @RetakeTestApplicationID
                                where TestAppointmentID = @TestAppointmentID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsLocked", IsLocked);
            if (ReTakeTestAppID != -1 && ReTakeTestAppID != null)
                command.Parameters.AddWithValue("@RetakeTestApplicationID", ReTakeTestAppID);

            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", System.DBNull.Value);

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
        public static bool DeleteTestApointment(int TestApointment)
        {

            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Delete TestApointments 
                                where TestApointment = @TestApointment";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestApointment", TestApointment);

            try
            {
                connection.Open();

                rowsAffected = command.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {

                connection.Close();

            }

            return (rowsAffected > 0);

        }
        public static DataTable GetTestApointmentByLDLAID(int LocalDrivingLicenseApplicationID,int TestTypeID )
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //    string query = "SELECT * FROM People";
            string query = @"select TestAppointmentID, AppointmentDate , PaidFees, IsLocked
           from TestAppointments where (TestTypeID = @TestTypeID)
           and (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
           order by TestAppointmentID desc";
            
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
        // Get How Many Trail Time 
        public static int HowManyTrail(int LocalDrivingLicenseApplicationID, int TestTypeID)

        {
            //** You Must declare var string To Loaded With First Name You Looking For

            int Trails = -1;

            //SqlConnection they there Objective Doing the Connectivity with Data Base


            //the Necessary information To Get Access To Database 
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);


            //Write Your Query                                          //the parametrize (will Looking For)

            string Query = @"sELECT COUNT(*) AS HowTrail
             FROM TestAppointments INNER JOIN
             Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
            where (TestAppointments.TestTypeID =@TestTypeID and TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)
			and(Tests.TestResult =0 )";


            //Prepare To Execute Comment 

            SqlCommand command = new SqlCommand(Query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                //Open the Gate to Get Access to Database 
                connection.Open();

                //**
                //**We Use Here Execute Scalar
                //**

                object Result = command.ExecuteScalar();

                if (Result != null)//IF Find 
                {
                    Trails =int.Parse(Result.ToString());
                }
                else//If Not Find
                {
                    Trails = -1;
                }

                connection.Close();

            }
            //Must Apply catch Because if the Data base Get ERROR Will Display it on the Screen 
            catch (Exception ex)
            {
                Console.WriteLine("Error " + ex.Message);
            }
            //IMPORTANT:
            //Return First name Must Be At The End of function 
            return Trails;

        }

        public static bool IsHaveLockedAppointment(int LocalDrivingLicenseApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"SELECT PassedTestCount = count(TestTypeID)
                         FROM Tests INNER JOIN
                         TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						 where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=0 and TestAppointments.IsLocked =1";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

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
    }
}
