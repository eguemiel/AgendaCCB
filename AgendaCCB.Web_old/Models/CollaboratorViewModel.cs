using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Web.Models
{
    public class CollaboratorViewModel
    {
        public CollaboratorViewModel()
        {

        }

        public CollaboratorViewModel(CollaboratorViewModel collaboratorViewModel)
        {
            Id = collaboratorViewModel.Id;
            Name = collaboratorViewModel.Name;
            IdPositionMinisty = collaboratorViewModel.IdPositionMinisty;
            IdCommonCongregation = collaboratorViewModel.IdCommonCongregation;
        }

        [DisplayName("Código Colaborador")]
        public int Id { get; set; }

        [DisplayName("Nome Colaborador")]
        [Required(ErrorMessage = "É necessário informar um nome")]
        public string Name { get; set; }

        [DisplayName("Cargo")]
        [Required(ErrorMessage = "É necessário informar um cargo")]
        public int IdPositionMinisty { get; set; }

        [DisplayName("Telefone")]
        [Required(ErrorMessage = "É necessário informar ao menos um telefone")]
        public int IdPhoneNumber { get; set; }

        [DisplayName("Tipo")]
        public int TypePhone { get; set; }

        public List<PhoneNumberViewModel> PhoneNumberList { get; set; }

        public SelectList TypePhoneList { get; set; }

        [DisplayName("Comum Congregação")]
        [Required(ErrorMessage = "É necessário informar a comum Congregação")]
        public int IdCommonCongregation { get; set; }
    }
}
