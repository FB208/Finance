using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameWork.MongoDB;
using FrameWork.MongoDB.MongoDbConfig;
using Models.FinanceDB;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Finance.Controllers
{
    public class JsonFormatController : Controller
    {
        // GET: JsonFormat
        public ActionResult Index()
        {
            FamilyGroup fg = new FamilyGroup
            {
                FamilyName = "MyFamily",
                Users = new List<User>
                {
                    new User
                    {
                        Id = 1,
                        UserName = "杨大壮"
                    },
                    new User {Id = 2, UserName = "汪小欠"}
                }
            };
            string jsonStr1 = JsonConvert.SerializeObject(fg);
            //
            FinanceRatio fr = new FinanceRatio
            {
                FamilyName = "MyFamily",
                ratio = new Dictionary<string, double>() { }
            };
            fr.ratio.Add("存储", 0.4);
            fr.ratio.Add("投资", 0.2);
            fr.ratio.Add("梦想基金", 0.1);
            fr.ratio.Add("零花-杨", 0.15);
            fr.ratio.Add("零花-汪", 0.15);
            fr.ratio.Add("生活基金", 3000);
            string jsonStr2 = JsonConvert.SerializeObject(fr);
            //入账
            MoneyStream ms = new MoneyStream
            {
                CreateTime = Convert.ToDateTime("2017-10-1"),
                Type=1,
                Money=7400+3000,
                UserId=1,
                UserName="杨大壮"
            };
            MoneyStream ms2 = new MoneyStream
            {
                CreateTime = Convert.ToDateTime("2017-10-1"),
                Type = 1,
                Money = 4500,
                UserId = 2,
                UserName="汪小欠"
            };
            string jsonStr3 = JsonConvert.SerializeObject(fr);

            new MongoDbService().Add<MoneyStream>("FinanceDB","MoneyStream",ms);
            new MongoDbService().Add<MoneyStream>("FinanceDB", "MoneyStream", ms2);
            return View();
        }
    }

    

    
    
    

   
}