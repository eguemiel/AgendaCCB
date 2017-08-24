using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class CommonCongregationMetadata
    {
        [Display(Description="Código")]
        public int Id { get; set; }

        [Display(Description = "Nome Comum")]
        public string Name { get; set; }

        [Display(Description = "Código Cidade")]
        public int IdCity { get; set; }
    }

    [ModelMetadataType(typeof(CommonCongregationMetadata))]
    public partial class CommonCongregation { }
}
