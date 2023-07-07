using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Data.Entities
{
    public class Recipe : BaseEntity, IDeletedEntity
    {
        public string Name { get; set; }
        public bool Deleted { get; set; }
    }
}
