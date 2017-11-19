using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.FinanceDB
{
    public class KeyValueClass<T>
    {
        public string Key { get; set; }
        public T Value { get; set; }
    }
}
