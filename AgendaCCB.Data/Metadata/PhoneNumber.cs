using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Data.Models
{
    public class PhoneNumberMetadata
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Número Telefone")]
        public string Number { get; set; }

        [DisplayName("Tipo Telefone")]
        public string Type { get; set; }
    }

    [ModelMetadataType(typeof(PhoneNumberMetadata))]
    public partial class PhoneNumber { }
}
