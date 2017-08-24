using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class CollaboratorMetadata
    {
        [Display(Description="Código")]
        public int Id { get; set; }

        [Display(Description = "Nome Colaborador")]
        public string Name { get; set; }

        [Display(Description = "Cargo/Ministério")]
        public int IdPositionMinisty { get; set; }

        [Display(Description = "Código Telefone")]
        public int IdPhoneNumber { get; set; }

        [Display(Description = "Código Comum Congregação")]
        public int IdCommonCongregation { get; set; }
    }

    [ModelMetadataTypeAttribute(typeof(CollaboratorMetadata))]
    public partial class Collaborator { }
}
