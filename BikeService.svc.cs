using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI.WebControls.WebParts;

namespace WcfService1
{
     public class BikeService : IBikeService
    {
        SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
        public Bike GetBike(Guid id)
        {
            String cmdText = "select * from bikes where id= '" + id + "'";

            conn.Open();

            SqlCommand command = new SqlCommand(cmdText, conn);
            SqlDataReader reader = command.ExecuteReader();



            Bike bike = new Bike();
            while (reader.Read())
            {
                bike.id = reader.GetGuid(0);
                bike.brand = reader.GetString(1);
                bike.type = reader.GetString(2);
                bike.size = reader.GetString(3);
                bike.color = reader.GetString(4);
                bike.price = reader.GetString(5);
                bike.description = reader.GetString(6);



            }

            conn.Close();

            return bike;



        }
        public Part GetPart(Guid id)
        {
           // SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
            String cmdText = "select * from parts where id= '" + id + "'";

            conn.Open();

            SqlCommand command = new SqlCommand(cmdText, conn);
            SqlDataReader reader = command.ExecuteReader();



            Part part = new Part();
            while (reader.Read())
            {
                part.id = reader.GetGuid(0);
                part.brand = reader.GetString(1);
                part.type = reader.GetString(2);
                part.size = reader.GetString(3);
                part.color = reader.GetString(4);
                part.price = reader.GetString(5);
                part.description = reader.GetString(6);



            }

            conn.Close();

            return part;



        }
        public IEnumerable<Bike>  GetBikes()
        {
          //  SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
            string cmdText;
            cmdText = "select * from bikes";

            conn.Open();

            SqlCommand command = new SqlCommand(cmdText, conn);
            SqlDataReader reader = command.ExecuteReader();
       

            List<Bike> bikes=new List<Bike>();
      
            while (reader.Read())
            {
                Bike bike = new Bike();
                bike.id = reader.GetGuid(0);
                bike.brand = reader.GetString(1);
                bike.type = reader.GetString(2);
                bike.size = reader.GetString(3);
                bike.color = reader.GetString(4);
                bike.price = reader.GetString(5);
                bike.description = reader.GetString(6);

                bikes.Add(bike);
         
            }

            conn.Close();

            return bikes;

        }

        public IEnumerable<Part> GetParts()
        {

           // SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
            string cmdText;
            cmdText = "select * from parts";

            conn.Open();

            SqlCommand command = new SqlCommand(cmdText, conn);
            SqlDataReader reader = command.ExecuteReader();


            List<Part> parts = new List<Part>();

            while (reader.Read())
            {
                Part part = new Part();
                part.id = reader.GetGuid(0);
                part.brand = reader.GetString(1);
                part.type = reader.GetString(2);
                part.size = reader.GetString(3);
                part.color = reader.GetString(4);
                part.price = reader.GetString(5);
                part.description = reader.GetString(6);

                parts.Add(part);
            }

            conn.Close();

            return parts;
        }


        public IEnumerable<ItemsReview> getReviews(Guid itemid)
        {
           
            string cmdText;
            cmdText = "select * from itemReviews where itemid=@a";
            conn.Open();
            SqlParameter par1 = new SqlParameter("@a",itemid );

            SqlCommand command= new SqlCommand(cmdText, conn);
            command.Parameters.Add(par1);
           SqlDataReader read= command.ExecuteReader();

           List<ItemsReview> list = new List<ItemsReview>();
            while (read.Read())
            {
                ItemsReview itemRev = new ItemsReview();
               
                itemRev.itemId=read.GetGuid(1);
                itemRev.userID = read.GetGuid(2);
                itemRev.dateofReview = read.GetDateTime(3);
                itemRev.rating = read.GetInt32(4);
                itemRev.review = read.GetString(5);
                
                    WebService1 e=new WebService1();
                    e.getUserName(itemRev.userID);
                    itemRev.userName = e.getUserName(itemRev.userID);

                
                list.Add(itemRev);
            }

            conn.Close();
            return list;
           
        }

        public void addReviews(ItemsReview rev)
        {
            string cmdText;
            cmdText = "insert into itemReviews  (itemId,userID, dateofReview ,rating,review) " +
                      "values (@p1,@p2,@p3,@p4,@p5)";
            conn.Open();
            DateTime time = DateTime.UtcNow;              // Use current time
     
            string format = "yyyy-MM-dd HH:mm:ss";    // modify the format depending upon input required in the column in database 

            SqlParameter par1 = new SqlParameter("@p1", rev.itemId);
            SqlParameter par2 = new SqlParameter("@p2", rev.userID);
            SqlParameter par3 = new SqlParameter("@p3", time);
            SqlParameter par4 = new SqlParameter("@p4", rev.rating);
            SqlParameter par5 = new SqlParameter("@p5", rev.review);

            SqlCommand command = new SqlCommand(cmdText, conn);
            command.Parameters.Add(par1);
            command.Parameters.Add(par2);
            command.Parameters.Add(par3);
            command.Parameters.Add(par4);
            command.Parameters.Add(par5);
            int res = command.ExecuteNonQuery();

        }

    }
}
