using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.MongoDB.MongoDbConfig;

namespace Models.FinanceDB
{
    [MongoAttribute("FinanceDB", "FinanceRatio")]
    public class FinanceRatio: MongoEntity
    {
        public string FamilyName { get; set; }
        public List<KeyValueClass<double>> ratio { get; set; }
    }
}
