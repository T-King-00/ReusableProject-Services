using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace WcfService1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");


        [WebMethod]
        public Guid Login(string userName,string password)
        {

            //SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
            conn.Open();

            string cmdText;
            cmdText = "select * from Users where @x=userName and @y=password ;";

            SqlParameter parameter1 = new SqlParameter("@x", userName);
            SqlParameter parameter2 = new SqlParameter("@y", password);
            SqlCommand command = new SqlCommand(cmdText, conn);

            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            SqlDataReader reader = command.ExecuteReader();

            Guid id=Guid.Empty;
            if (reader.Read())
            {
               id = reader.GetGuid(0);
                return id;
            }
            conn.Close();


            return id;
        }

        [WebMethod]
        public Boolean Register(User newUser)
        {
           // SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");

            string cmdText;
            cmdText = "insert into users (firstName ,lastName ,userName ,email," +
                      "password,gender ,address ,city) " +
                      "values (@fname,@lname,@userName,@email,@password,@gender,@address,@city);";

            conn.Open();
         
            SqlParameter parameter1 = new SqlParameter("@fname", newUser.firstName);
            SqlParameter parameter2 = new SqlParameter("@lname", newUser.lastName);
            SqlParameter parameter3 = new SqlParameter("@userName", newUser.UserName);
            SqlParameter parameter4 = new SqlParameter("@address", newUser.address);
            SqlParameter parameter5 = new SqlParameter("@gender", newUser.gender);
            SqlParameter parameter6 = new SqlParameter("@city", newUser.city);
            SqlParameter parameter7 = new SqlParameter("@email", newUser.email);
            SqlParameter parameter8 = new SqlParameter("@password", newUser.password);
            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter3);
            command.Parameters.Add(parameter4);
            command.Parameters.Add(parameter5);
            command.Parameters.Add(parameter6);
            command.Parameters.Add(parameter7);
            command.Parameters.Add(parameter8);
            try
            {
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
                
                
            }
            

        }


        [WebMethod]
        public Boolean UpdateUserDetails(User userToEdit)
        {

          //  SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");

            string cmdText;
            cmdText = "update  users " +
                      "set firstName=@fname , lastName=@lname , address=@address ,gender=@gender ,city=@city ,email=@email " +
                      "where userName= '"+userToEdit.UserName+"'";
                   

            conn.Open();

            SqlParameter parameter1 = new SqlParameter("@fname", userToEdit.firstName);
            SqlParameter parameter2 = new SqlParameter("@lname", userToEdit.lastName);
            SqlParameter parameter4 = new SqlParameter("@address", userToEdit.address);
            SqlParameter parameter5 = new SqlParameter("@gender", userToEdit.gender);
            SqlParameter parameter6 = new SqlParameter("@city", userToEdit.city);
            SqlParameter parameter7 = new SqlParameter("@email", userToEdit.email);
            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);
            command.Parameters.Add(parameter4);
            command.Parameters.Add(parameter5);
            command.Parameters.Add(parameter6);
            command.Parameters.Add(parameter7);
            try
            {
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;


            }




           
        }

        [WebMethod]
        public User ViewUserDetails(string userName)
        {
            //SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");

            conn.Open();
            string cmdText;
            cmdText = "select * from Users where @x=userName ;";
            SqlParameter parameter1 = new SqlParameter("@x", userName);
            SqlCommand command = new SqlCommand(cmdText,conn);
            command.Parameters.Add(parameter1);

            SqlDataReader reader = command.ExecuteReader();
            User userObjToView = new User();
            userObjToView.UserName = userName;
            while (reader.Read())
            {

                userObjToView.Id= reader.GetGuid(0);
                userObjToView.firstName = reader.GetString(1);
                userObjToView.lastName = reader.GetString(2);
                userObjToView.UserName = reader.GetString(3);
                userObjToView.email = reader.GetString(4);
                userObjToView.gender = reader.GetString(6);
                userObjToView.address = reader.GetString(7);
                userObjToView.city = reader.GetString(8);



            }
            conn.Close();

            return userObjToView;
        }

        //used to get user name for viewing user names in reviews section
        [WebMethod]
        public string getUserName(Guid userid)
        {

            string cmdText;
            cmdText = "select * from Users where @x=id ;";
            SqlParameter parameter1 = new SqlParameter("@x", userid);
            conn.Open();

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.Add(parameter1);

            SqlDataReader reader = command.ExecuteReader();
            string name;
            if (reader.Read())
            {
                name = reader.GetString(3);
            }
            else
                name = "";
            conn.Close();
            return name;

        }

        [WebMethod]
        public Boolean paywithcredit(String authorized)
        {

            //verification 

            return true;
        }

    }
}
