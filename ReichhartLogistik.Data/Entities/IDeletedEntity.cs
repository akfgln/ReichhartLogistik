using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReichhartLogistik.Data.Entities
{
    public partial interface IDeletedEntity
    {
        bool Deleted { get; set; }
    }
}
