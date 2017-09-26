﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AgendaCCB.Data.Models
{
    public class CollaboratorMetadata
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome Colaborador")]
        public string Name { get; set; }

        [DisplayName("Cargo/Ministério")]
        public int IdPositionMinistyNavigation { get; set; }

        [DisplayName("Código Telefone")]
        public int IdPhoneNumber { get; set; }

        [DisplayName("Código Comum Congregação")]
        public int IdCommonCongregationNavigation { get; set; }
    }

    [ModelMetadataType(typeof(CollaboratorMetadata))]
    public partial class Collaborator { }
}
