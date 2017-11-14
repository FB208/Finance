using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameWork.MongoDB;
using FrameWork.MongoDB.MongoDbConfig;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

namespace Finance.Controllers.Client
{
    public class MongoDBController : Controller
    {
        public class TestMongo 
        {

            [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
            public DateTime CreateDateTime { get; set; }

            public decimal Amount { get; set; }

            public string Name { get; set; }

        }
        // GET: MongoDB
        public ActionResult Index()
        {
            //var database = "FinanceDB";
            //        var collection = "Test1";
            //            var db = new MongoClient("mongodb://123.207.187.133:27017").GetDatabase(database);
            //             var coll = db.GetCollection<TestMongo>(collection);

            //             var entity = new TestMongo
            //             {
            //                     Name = "SkyChen",
            //                     Amount = 100,
            //                   CreateDateTime = DateTime.Now
            //                 };

            //            coll.InsertOneAsync(entity).ConfigureAwait(false);

            var ss =new MongoDbService().List<User>(m => true);


            return View();
        }
    }
    [MongoAttribute("FinanceDB", "Test1")]
    public class User:MongoEntity
    {
        public string S { get; set; }
    }
}