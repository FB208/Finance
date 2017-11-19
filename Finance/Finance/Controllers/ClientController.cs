using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameWork.MongoDB;
using Models.FinanceDB;

namespace Finance.Controllers
{
    public class ClientController : BaseController
    {
        // GET: Client
        public ActionResult Index()
        {
            

            return View();
        }

        public JsonResult GetMMData()
        {
            #region 取当前年月

            string year = System.DateTime.Now.Year.ToString();
            string month = System.DateTime.Now.Month.ToString();
            #endregion
            #region 取当前年月数据

            Models.FinanceDB.MonthMoney mmData1 =
                new MongoDbService().Get<MonthMoney>(m => m.Year == year && m.Month == month&&m.Type==1);
            Models.FinanceDB.MonthMoney mmData0 =
                new MongoDbService().Get<MonthMoney>(m => m.Year == year && m.Month == month && m.Type == 0);

            //如果没有创建一条
            if (mmData1 == null)
            {
                //上月初
                DateTime prevMonth1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                //本月初
                DateTime prevMonth2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //取上个月的入账
                List<MoneyStream> prevMonthStreams =
                    new MongoDbService().List<MoneyStream>(m =>
                        m.CreateTime >= prevMonth1 && m.CreateTime < prevMonth2 && m.Type == 1);
                double totalMoney = prevMonthStreams.Sum(m => m.Money);
                //取资产分类
                FinanceRatio fr = new MongoDbService().Get<FinanceRatio>(m => m.FamilyName == "MyFamily");
                //减去固定用金额
                List<KeyValueClass<double>> fixedMoneys = fr.ratio.Where(m => m.Key.IndexOf("^", StringComparison.Ordinal) > -1).ToList();
                foreach (var fixedMoney in fixedMoneys)
                {
                    totalMoney -= fixedMoney.Value;

                }
                List<KeyValueClass<double>> ratioMoneys = fr.ratio.Where(m => m.Key.IndexOf("^", StringComparison.Ordinal) == -1).ToList();
                //List<string> keys = fr.ratio.Select(m => m.Key).ToList();
                //结果集
                List<KeyValueClass<double>> KindMoneys = new List<KeyValueClass<double>>();
                //比例的
                foreach (var ratioMoney in ratioMoneys)
                {
                    KindMoneys.Add(new KeyValueClass<double>
                    {
                        Key= ratioMoney.Key,
                        Value= ratioMoney.Value * totalMoney
                    });
                }
                //固定的
                foreach (var fixedMoney in fixedMoneys)
                {
                    KindMoneys.Add(new KeyValueClass<double>
                    {
                        Key = fixedMoney.Key,
                        Value = fixedMoney.Value
                    });
                }
                //加上上个月剩的
                MonthMoney prevMonthMoney = new MongoDbService().Get<MonthMoney>(m =>
                    m.Year == prevMonth1.Year.ToString() && m.Month == prevMonth1.Month.ToString());
                if (prevMonthMoney != null)
                {
                    foreach (var item in prevMonthMoney.KindMoney)
                    {
                        //存在
                        if (KindMoneys.Exists(m=>m.Key==item.Key))
                        {
                            KindMoneys.First(m=>m.Key==item.Key).Value += item.Value;
                        }
                        else//算在存款里
                        {
                            KindMoneys.First(m => m.Key == "存储").Value += item.Value;
                        }

                    }
                }

                //本月数据-初始
                mmData0 = new MonthMoney()
                {
                    Year = year,
                    Month = month,
                    Type=0,
                    KindMoney = KindMoneys
                };
                //保存
                new MongoDbService().Add(mmData0);
                //本月数据-剩余
                mmData1 = new MonthMoney()
                {
                    Year = year,
                    Month = month,
                    Type = 1,
                    KindMoney = KindMoneys
                };
                //保存
                new MongoDbService().Add(mmData1);
            }

            #endregion

            Result r = new Result();
            r.status = 1;
            r.Data = mmData1;
            r.BaseData = mmData0;
            return Json(r,JsonRequestBehavior.AllowGet);

            
            
            
        }

        
    }
}