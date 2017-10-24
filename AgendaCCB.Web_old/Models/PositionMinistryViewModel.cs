using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Web.Models
{
    public class PositionMinistryViewModel
    {
        public PositionMinistryViewModel()
        {

        }

        public PositionMinistryViewModel(PositionMinistryViewModel positionMinistryViewModel)
        {
            Id = positionMinistryViewModel.Id;
            Description = positionMinistryViewModel.Description;
            IsMinistry = positionMinistryViewModel.IsMinistry;
        }

        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Descrição Cargo/Ministério")]
        [Required(ErrorMessage = "É necessário informar uma Descrição")]
        public string Description { get; set; }

        [DisplayName("É ministério")]
        public bool IsMinistry { get; set; }
    }
}
