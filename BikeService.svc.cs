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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BikeService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BikeService.svc or BikeService.svc.cs at the Solution Explorer and start debugging.
    public class BikeService : IBikeService
    {
        public Bike GetBike(Guid id)
        {
            SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
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
            SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
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
            SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
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

            SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
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
            SqlConnection conn = new SqlConnection("Data Source=TONYRIAD;Initial Catalog=BikeShopDb;Integrated Security=True");
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

        }

    }
}
