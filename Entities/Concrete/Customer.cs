using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Customer : BaseEntity,IEntity
    {
        public int CustomerName { get; set; }

        public int CustomerCode { get; set; }

        public string Address { get; set; }

        public int PhoneNum { get; set; }

        public string Email { get; set; }
    }
}
