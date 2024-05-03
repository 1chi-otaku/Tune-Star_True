using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tune_Star.DAL.Entities
{
    public class Users
    {
        public int Id { get; set; }

        public string? Login { get; set; }

        public string? Password { get; set; }

        public int Status { get; set; }

        public string? Salt { get; set; }
    }
}
