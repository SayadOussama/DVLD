using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLDProject_DataAccessLayer
{
    public  class clsDataAccessInternationalLicenses
    {
        public static DataTable GetAllInternationalLicenses()
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //    string query = "SELECT * FROM People";
            string query = @" select  InternationalLicenses.InternationalLicenseID as 'Int.LicenseID', InternationalLicenses.ApplicationID ,InternationalLicenses.IssuedUsingLocalLicenseID as 'L.LicenesID', InternationalLicenses.IssueDate,
           InternationalLicenses.ExpirationDate , InternationalLicenses.IsActive  From InternationalLicenses";
            
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
 //       public static DataTable GetLicenseHistory(int PersonID)
 //       {

 //           DataTable dt = new DataTable();
 //           SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

 //           //    string query = "SELECT * FROM People";
 //           string query = @"select  Licenses.LicenseID as 'Li ID',  Licenses.ApplicationID as 'Ap ID',  LicenseClasses.ClassName , Licenses.IssueDate, 
 //Licenses.ExpirationDate , Licenses.IsActive
 //from Licenses 
 //inner join Drivers on  Licenses.DriverID = Drivers.DriverID
 //inner join LicenseClasses on  LicenseClasses.LicenseClassID = Licenses.LicenseClass

 //where (Drivers.PersonID = @PersonID)";

 //           SqlCommand command = new SqlCommand(query, connection);
 //           command.Parameters.AddWithValue("@PersonID", PersonID);
 //           try
 //           {
 //               connection.Open();

 //               SqlDataReader reader = command.ExecuteReader();

 //               if (reader.HasRows)

 //               {
 //                   dt.Load(reader);
 //               }

 //               reader.Close();


 //           }

 //           catch (Exception ex)
 //           {
 //               // Console.WriteLine("Error: " + ex.Message);
 //           }
 //           finally
 //           {
 //               connection.Close();
 //           }

 //           return dt;

 //       }
        public static bool GetInternationalLicenseByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate,  ref bool IsActive, ref int CreatedByUserID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    InternationalLicenseID = (int)reader["InternationalLicenseID"];
                    ApplicationID = (int)reader["ApplicationID"];
                    DriverID = (int)reader["DriverID"];
                    IssuedUsingLocalLicenseID = (int)reader["IssuedUsingLocalLicenseID"];
                    IssueDate = (DateTime)reader["IssueDate"];
                    ExpirationDate = (DateTime)reader["ExpirationDate"];
                    IsActive = (bool)reader["IsActive"];
                    CreatedByUserID = (int)reader["CreatedByUserID"];
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
        public static int AddNewInternationalLicense(  int ApplicationID,  int DriverID,  int IssuedUsingLocalLicenseID,  DateTime IssueDate,  DateTime ExpirationDate,  bool IsActive,  int CreatedByUserID)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            int InternationalLicenseID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"INSERT INTO InternationalLicenses (ApplicationID, DriverID,IssuedUsingLocalLicenseID,IssueDate,ExpirationDate ,IsActive,CreatedByUserID)
                             VALUES (@ApplicationID, @DriverID,@IssuedUsingLocalLicenseID,@IssueDate,@ExpirationDate,@IsActive,@CreatedByUserID)
                             SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    InternationalLicenseID = insertedID;
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


            return InternationalLicenseID;
        }
        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssueUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {

            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Update  InternationalLicenses  
                            set 
                                ApplicationID = @ApplicationID, 
                                DriverID = @DriverID, 
                                IssueUsingLocalLicenseID = @IssueUsingLocalLicenseID,
                                LicenseClass = @LicenseClass,
                                IssueDate = @IssueDate, 
                                ExpirationDate = @ExpirationDate, 
                                IsActive = @IsActive, 
                                CreatedByUserID = @CreatedByUserID 
                                where LicenseID = @LicenseID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssueUsingLocalLicenseID", IssueUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);


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
        public static bool PersonHaveAllReadyInternationalLicense(int ApplicationID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"select top 1  found =1 from InternationalLicenses
			  inner join Licenses on 
			    InternationalLicenses.IssuedUsingLocalLicenseID=Licenses.LicenseID

			  where (Licenses.ApplicationID =@ApplicationID)";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

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
        public static DataTable GetInternationalLicenseHistory(int PersonID)
        {

            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            //    string query = "SELECT * FROM People";
            string query = @"select  InternationalLicenses.InternationalLicenseID as 'Inter Li ID',  InternationalLicenses.ApplicationID as 'Ap ID', InternationalLicenses.IssueDate, 
            InternationalLicenses.ExpirationDate , InternationalLicenses.IsActive
             from InternationalLicenses 
             inner join Drivers on  InternationalLicenses.DriverID = Drivers.DriverID
 where (Drivers.PersonID = @PersonID)";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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


    }
}
