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

        public CommonCongregationViewModel(CommonCongregationViewModel commonCongregationViewModel)
        {
            Id = commonCongregationViewModel.Id;
            IdCity = commonCongregationViewModel.IdCity;
            Name = commonCongregationViewModel.Name;
        }

        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome Comum")]
        [Required(ErrorMessage = "É necessário informar um nome")]
        public string Name { get; set; }

        [DisplayName("Cidade")]
        [Required(ErrorMessage = "É necessário informar uma cidade")]
        public int IdCity { get; set; }

        public SelectList CityList { get; set; }
    }
}
