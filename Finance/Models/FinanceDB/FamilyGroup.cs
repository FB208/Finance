using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameWork.MongoDB.MongoDbConfig;

namespace Models.FinanceDB
{
    [MongoAttribute("FinanceDB", "FamilyGroup")]
    public class FamilyGroup: MongoEntity
    {
        public string FamilyName { get; set; }
        public List<User> Users { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
