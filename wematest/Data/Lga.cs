using System;
using System.Collections.Generic;

#nullable disable

namespace wematest.Data
{
    public partial class Lga
    {
        public int LgaId { get; set; }
        public string LgaName { get; set; }
        public int? LgaState { get; set; }

        public virtual Stateofresidence LgaStateNavigation { get; set; }
    }
}
