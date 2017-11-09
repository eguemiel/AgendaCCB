using System;
using System.Collections.Generic;

namespace AgendaCCB.Api.ModelsData
{
    public partial class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdState { get; set; }

        public State IdStateNavigation { get; set; }
        public CommonCongregation CommonCongregation { get; set; }
    }
}
