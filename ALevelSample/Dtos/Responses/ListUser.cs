using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelSample.Dtos.Responses
{
    public class ListUser<T>
        where T : class
    {
        public List<T> Data { get; set; }
        public SupportDto Support { get; set; }
    }
}
