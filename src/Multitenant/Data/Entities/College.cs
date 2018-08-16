using System.Collections.Generic;

namespace Multitenant.Data.Entities
{
    public class College : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
