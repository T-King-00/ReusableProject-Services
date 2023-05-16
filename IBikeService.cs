using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfService1
{
    [ServiceContract]
    public interface IBikeService
    {

        [OperationContract]
        IEnumerable<Bike> GetBikes();

        [OperationContract]
        Bike GetBike(Guid id);

        [OperationContract]
        IEnumerable<Part> GetParts();

        [OperationContract]
        Part GetPart(Guid id);

        [OperationContract]
        IEnumerable<ItemsReview> getReviews(Guid itemid);

        [OperationContract(IsOneWay = true)]
        void addReviews(ItemsReview rev);
    }


    [DataContract]
    public class Bike
    {

        [DataMember]
        public Guid id { get; set; }
        [DataMember]
        public string brand { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string size { get; set; }
        [DataMember]
        public string color { get; set; }
        [DataMember]

        public string price { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string picurl { get; set; }



    }


    [DataContract]
    public class Part
    {

        [DataMember]
        public Guid id { get; set; }
        [DataMember]
        public string brand { get; set; }
        [DataMember]
        public string type { get; set; }
        [DataMember]
        public string size { get; set; }
        [DataMember]
        public string color { get; set; }
        [DataMember]

        public string price { get; set; }
        [DataMember]
        public string description { get; set; }
        [DataMember]
        public string picurl { get; set; }



    }


    [DataContract]
    public class ItemsReview
    {

        [DataMember]
        public Guid itemId { get; set; }
        [DataMember]
        public Guid userID { get; set; }
        [DataMember]
        public DateTime dateofReview { get; set; }
        [DataMember]
        public int rating { get; set; }
        [DataMember]
        public string review { get; set; }
        [DataMember]
        public string userName { get; set; }






    }
}
