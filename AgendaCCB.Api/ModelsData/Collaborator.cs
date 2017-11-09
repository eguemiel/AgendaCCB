using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
{
    public partial class Collaborator
    {
        public Collaborator()
        {
            PhoneNumber = new HashSet<PhoneNumber>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int IdPositionMinisty { get; set; }
        public int IdCommonCongregation { get; set; }

        public CommonCongregation IdCommonCongregationNavigation { get; set; }
        public PositionMinistry IdPositionMinistyNavigation { get; set; }
        public ICollection<PhoneNumber> PhoneNumber { get; set; }
    }
}
