using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Task1_CRUD_24_FEB_22_CORE_WEB_APP.Interface;
using Task1_CRUD_24_FEB_22_CORE_WEB_APP.Models;

namespace Task1_CRUD_24_FEB_22_CORE_WEB_APP.Services
{
    public class UserService : IUserService
    {
        private readonly String _connetionString;

        public UserService(IConfiguration _configuration)
        {
            _connetionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 192.168.2.9 )(PORT = 1521))(CONNECT_DATA =(SID = HDFCDEV)));User Id=HDFC_PIP_DEVUSER;Password=Synoverge123;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=600;Incr Pool Size=5; Decr Pool Size=2;validate connection=true;";
        }

        public IEnumerable<User> GetAllUser()       //GET ALL USERS DETAILS
        {
            List<User> UsersList = new List<User>();

            using (OracleConnection connection = new OracleConnection(_connetionString)) {

                using (OracleCommand command = connection.CreateCommand())
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                        Console.WriteLine("#####-----Connection Successfull-----#####");
                    }
                    //command.CommandText = "SELECT ID, FIRST_NAME, LAST_NAME, MOBILE_NO, PINCODE FROM TRAINEE_DEMO ORDER BY ID ASC";
                    command.CommandText = "SELECT ID, FIRST_NAME, LAST_NAME, MOBILE_NO, PINCODE, EMAIL FROM TRAINEE_DEMO_SEQ ORDER BY ID ASC";
                    OracleDataReader dataReader = command.ExecuteReader();

                    while (dataReader.Read())
                    {
                        User user = new User
                        {
                            Id = Convert.ToInt32(dataReader["id"]),
                            First_name = dataReader["first_name"].ToString(),
                            Last_name = dataReader["last_name"].ToString(),
                            Mobile_No = Convert.ToInt64(dataReader["Mobile_No"]),
                            Pincode = Convert.ToInt32(dataReader["Pincode"]),
                            Email = dataReader["email"].ToString()
                        };
                        UsersList.Add(user);
                    }
                }
            };
            return UsersList;
        }


        //Creates/Add New user in Database:
        public void AddUser(User user)
        {
            int id = user.Id;
            //var id = ' ';
            string email = " ";

            string fname = user.First_name;
            string lname = user.Last_name;
            long mobile_no = user.Mobile_No;
            int pincode = user.Pincode;
            email = user.Email;

            using(OracleConnection connection = new OracleConnection(_connetionString))
            {
                connection.Open();
                using (OracleCommand command = connection.CreateCommand())
                {
                    
                    //command.CommandText = "INSERT INTO TRAINEE_DEMO_SEQ_TEST VALUES( '" + id + "','" +  fname+ "', '" + lname + "', '" + mobile_no + "', '" + pincode + "', '" + email + "')" +"";
                    //Without 'ID'
                    command.CommandText = "INSERT INTO TRAINEE_DEMO_SEQ (FIRST_NAME, LAST_NAME, MOBILE_NO, PINCODE, EMAIL) VALUES( '" +  fname+ "', '" + lname + "', '" + mobile_no + "', '" + pincode + "', '" + email + "')" +"";
                    command.CommandType = CommandType.Text;
                    command.ExecuteNonQuery();
                    return;
                }
            }
            throw new NotImplementedException();
        }


        //Modifies the Existing user in Database:
        public void EditUser(User user)
        {
            using (OracleConnection connection = new OracleConnection(_connetionString))
            {
                connection.Open();
                using (OracleCommand command = connection.CreateCommand())
                { 
                    command.CommandText = "UPDATE TRAINEE_DEMO_SEQ SET FIRST_NAME='" + user.First_name+ "', LAST_NAME='" + user.Last_name+ "', MOBILE_NO='" + user.Mobile_No+ "', PINCODE='" + user.Pincode+ "',EMAIL='" + user.Email + "' where Id=" + user.Id;
                    command.CommandType = CommandType.Text;
                    //Console.WriteLine("#####-----Record Updated!! HAVING ID: " + user.Id);
                    command.ExecuteNonQuery();
                    return;
                }
            }
            throw new NotImplementedException();
        }


        //Get Individual User using ID:
        public User GetUserById(int id)
        {
            User IndividualUser = new User();
            using (OracleConnection connection = new OracleConnection(_connetionString))
            {
                using (OracleCommand command = connection.CreateCommand())
                { 
                    connection.Open();
                    Console.WriteLine("#####-----Connection Successfull-----#####");
                    
                    command.CommandText = "SELECT ID, FIRST_NAME, LAST_NAME, MOBILE_NO, PINCODE, EMAIL FROM TRAINEE_DEMO_SEQ WHERE ID= " + id;
                    OracleDataReader dataReader = command.ExecuteReader();
                    
                    while (dataReader.Read())
                    {
                        User user = new User
                        {
                            Id = Convert.ToInt32(dataReader["id"]),
                            First_name = dataReader["first_name"].ToString(),
                            Last_name = dataReader["last_name"].ToString(),
                            Mobile_No = Convert.ToInt64(dataReader["Mobile_No"]),
                            Pincode = Convert.ToInt32(dataReader["Pincode"]),
                            Email = dataReader["email"].ToString()
                        };
                        IndividualUser = user;
                    }
                    
                }
            };
            return IndividualUser;
        }
    }
}
