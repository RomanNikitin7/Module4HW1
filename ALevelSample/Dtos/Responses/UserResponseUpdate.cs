using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALevelSample.Dtos.Responses
{
    public class UserResponseUpdate
    {
        public string Name { get; set; }
        public string Job { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
