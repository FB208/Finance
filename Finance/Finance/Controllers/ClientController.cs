using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FrameWork.MongoDB;
using Models.FinanceDB;

namespace Finance.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client
        public ActionResult Index()
        {
            #region 取当前年月

            string year = System.DateTime.Now.Year.ToString();
            string month = System.DateTime.Now.Month.ToString();
            #endregion
            #region 取当前年月数据

            Models.FinanceDB.MonthMoney mmData =
                new MongoDbService().Get<MonthMoney>(m => m.Year == year && m.Month == month);
            
            //如果没有创建一条
            if (mmData==null)
            {
                
                //上月初
                DateTime prevMonth1 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(-1);
                //本月初
                DateTime prevMonth2 = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                //取上个月的入账
                List<MoneyStream> prevMonthStreams =
                    new MongoDbService().List<MoneyStream>(m =>
                        m.CreateTime >= prevMonth1 && m.CreateTime < prevMonth2&&m.Type==1);
                double totalMoney = prevMonthStreams.Sum(m => m.Money);
                //取资产分类
                FinanceRatio fr = new MongoDbService().Get<FinanceRatio>(m => m.FamilyName == "MyFamily");
                
                
                //减去固定用金额
                List<KeyValuePair<string,double>> fixedMoneys = fr.ratio.Where(m => m.Key.IndexOf("^", StringComparison.Ordinal) > -1).ToList();
                foreach (var fixedMoney in fixedMoneys)
                {
                    totalMoney -= fixedMoney.Value;
                    
                }
                List<KeyValuePair<string, double>> ratioMoneys = fr.ratio.Where(m => m.Key.IndexOf("^", StringComparison.Ordinal) == -1).ToList();
                //List<string> keys = fr.ratio.Select(m => m.Key).ToList();
                //结果集
                Dictionary<string, double> KindMoneys = new Dictionary<string, double>();
                //比例的
                foreach (var ratioMoney in ratioMoneys)
                {
                    KindMoneys.Add(ratioMoney.Key, ratioMoney.Value * totalMoney);
                }
                //固定的
                foreach (var fixedMoney in fixedMoneys)
                {
                    KindMoneys.Add(fixedMoney.Key, fixedMoney.Value);
                }
                //加上上个月剩的
                mmData = new MonthMoney()
                {
                    Year = year,
                    Month = month,
                    KindMoney= KindMoneys
                };
                //保存
                new MongoDbService().Add(mmData);
            }
            
            #endregion
            
            return View();
        }
    }
}