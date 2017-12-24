using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        [DisplayName("Telefone")]
        public int IdPhoneNumber { get; set; }

        [DisplayName("Comum Congregação")]
        public CommonCongregation IdCommonCongregationNavigation { get; set; }

        [DisplayName("Telefone")]
        public ICollection<PhoneNumber> PhoneNumber { get; set; }

        [DisplayName("Cargo Ministério")]
        public ICollection<PositionMinistryCollaborator> PositionMinistryCollaborator { get; set; }
    }

    [ModelMetadataType(typeof(CollaboratorMetadata))]
    public partial class Collaborator { }
}
