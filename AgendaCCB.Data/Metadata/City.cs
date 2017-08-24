using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data
{
    public class CityMetadata
    {
        [Display(Description = "Código")]
        public int Id { get; set; }

        [Display(Description = "Nome")]
        public string Name { get; set; }

        [Display(Description = "Código Estado")]
        public int IdState { get; set; }
    }

    [ModelMetadataTypeAttribute(typeof(CityMetadata))]
    public partial class City { }
}
