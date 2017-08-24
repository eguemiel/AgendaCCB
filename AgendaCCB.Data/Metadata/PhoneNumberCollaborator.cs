using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public partial class PhoneNumberCollaboratorMetadata
    {
        [Display(Description = "Código")]
        public int Id { get; set; }

        [Display(Description = "Código Telefone")]
        public int IdPhoneNumber { get; set; }

        [Display(Description = "Código Colaborador")]
        public int IdCollaborator { get; set; }
    }

    [ModelMetadataType(typeof(PhoneNumberCollaboratorMetadata))]
    public partial class PhoneNumberCollaborator { }
}
