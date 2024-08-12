using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BaseEntity = Entities.Concrete.BaseEntity;

namespace Entities.Concrete
{
    public class Storage : BaseEntity, IEntity
    {
        public int ProductId { get; set; }

        public int piece { get; set; }

        public bool IsReady { get; set; }
    }
}
