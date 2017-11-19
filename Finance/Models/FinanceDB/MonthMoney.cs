using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.MongoDB.MongoDbConfig;

namespace Models.FinanceDB
{
    [MongoAttribute("FinanceDB", "MonthMoney")]
    public class MonthMoney:MongoEntity
    {
        public string Year { get; set; }
        public string Month { get; set; }
        /// <summary>
        /// 0=初始值 1=剩余值
        /// </summary>
        public int Type { get; set; }
        public List<KeyValueClass<double>> KindMoney { get; set; }
    }
}
