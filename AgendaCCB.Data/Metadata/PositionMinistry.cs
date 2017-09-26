using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AgendaCCB.Data.Models
{
    public class PositionMinistryMetadata
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Descrição Cargo/Ministério")]
        public string Description { get; set; }

        [DisplayName("É ministério")]
        public bool IsMinistry { get; set; }
    }

    [ModelMetadataType(typeof(PositionMinistryMetadata))]
    public partial class PositionMinistry { }
}
