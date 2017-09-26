using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AgendaCCB.Data.Models
{
    public class CommonCongregationMetadata
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome Comum")]
        public string Name { get; set; }

        [DisplayName("Código Cidade")]
        public int IdCity { get; set; }

        [DisplayName("Nome Cidade")]
        public int IdCityNavigation { get; set; }
    }

    [ModelMetadataType(typeof(CommonCongregationMetadata))]
    public partial class CommonCongregation { }
}
