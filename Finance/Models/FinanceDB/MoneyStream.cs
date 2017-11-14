using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.MongoDB.MongoDbConfig;

namespace Models.FinanceDB
{
    [MongoAttribute("FinanceDB", "MoneyStream")]
    public class MoneyStream : MongoEntity
    {
        public DateTime CreateTime { get; set; }
        public int Type { get; set; }//0=出 1=入
        public double Money { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }

    }
}
