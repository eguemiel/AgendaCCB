using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class PositionMinistryMetadata
    {
        [Display(Description="Código")]
        public int Id { get; set; }

        [Display(Description = "Descrição Cargo/Ministério")]
        public string Description { get; set; }

        [Display(Description = "É ministério")]
        public bool IsMinistry { get; set; }
    }

    [ModelMetadataType(typeof(PositionMinistryMetadata))]
    public partial class PositionMinistry { }
}
