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

        [DisplayName("Endereço")]
        public string Address { get; set; }

        [DisplayName("Bairro")]
        public string District { get; set; }

        [DisplayName("CEP")]
        public string ZipCode { get; set; }

        [DisplayName("Telefone")]
        public string PhoneNumber { get; set; }

        [DisplayName("Fax")]
        public string FaxPhoneNumber { get; set; }

        [DisplayName("Cidade")]
        public City CityNavigation { get; set; }
    }

    [ModelMetadataType(typeof(CommonCongregationMetadata))]
    public partial class CommonCongregation { }
}
