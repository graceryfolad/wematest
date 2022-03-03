using System;
using System.Collections.Generic;

#nullable disable

namespace wematest.Data
{
    public partial class Stateofresidence
    {
        public Stateofresidence()
        {
            Customers = new HashSet<Customer>();
            Lgas = new HashSet<Lga>();
        }

        public string StaName { get; set; }
        public int StaId { get; set; }

        public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Lga> Lgas { get; set; }
    }
}
