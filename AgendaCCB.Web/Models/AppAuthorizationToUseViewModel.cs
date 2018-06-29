using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgendaCCB.Web.Models
{
    public class AppAuthorizationToUseViewModel
    {
        public AppAuthorizationToUseViewModel()
        {

        }

        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Número Telefone")]
        [Required(ErrorMessage = "É necessário informar um número de telefone")]
        public string PhoneNumber { get; set; }

        public string Token { get; set; }

        [DisplayName("Data Criação")]
        public DateTime Created { get; set; }

        [DisplayName("Usuário")]
        public string UserCreator { get; set; }

        [DisplayName("Usado?")]
        public bool Used { get; set; }
    }
}
