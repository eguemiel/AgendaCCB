using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class StateMetadata
    {
        [Display(Description = "Código")]
        public int Id { get; set; }

        [Display(Description = "Nome")]
        public string Name { get; set; }

        [Display(Description = "Código País")]
        public int IdCountry { get; set; }
    }
}
