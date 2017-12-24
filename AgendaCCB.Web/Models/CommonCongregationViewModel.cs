using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Web.Models
{
    public class CommonCongregationViewModel
    {
        public CommonCongregationViewModel()
        {

        }

        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome Comum")]
        [Required(ErrorMessage = "É necessário informar um nome")]
        public string Name { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "É necessário informar uma cidade")]
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

        public SelectList CityList { get; set; }
    }
}
