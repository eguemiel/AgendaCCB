using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaCCB.Web.Models
{
    public class UsersViewModel
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome de usuário é obrigatório.")]
        [DisplayName("Nome do usuário")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Nome de usuário é obrigatório.")]
        [DisplayName("Nome Completo do usuário")]
        [StringLength(250, ErrorMessage = "O Nome Completo do Usuário deve ter no mínimo 20 dígitos.", MinimumLength = 20)]
        public string FullUserName { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [DisplayName("Senha")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "A senha deve ter no mínimo 6 dígitos.", MinimumLength = 8)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirmar Senha é obrigatório.")]
        [DisplayName("Confirmar Senha")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "A senha deve ter no mínimo 6 dígitos.", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "A confirmação da senha não é igual a Senha digitada!")]
        public string ConfirmPassword { get; set; }

        [DisplayName("E-mail")]
        [Required(ErrorMessage = "E-mail é obrigatória.")]
        [EmailAddress(ErrorMessage ="E-mail Inválido.")]
        [MaxLength(150, ErrorMessage ="Número máximo de caracteres {0}")]
        public string Email { get; set; }

        [DisplayName("Pergunta Senha")]
        public string QuestionPassword { get; set; }

        [DisplayName("Resposta Senha")]
        public string AnswerPassword { get; set; }

        [DisplayName("Bloqueado")]
        public bool Blocked { get; set; }

        [DisplayName("Contagem Tentativa Resposta Falha")]
        public int CountFailureResponseFailure { get; set; }

        [DisplayName("Contagem Tentativa Senha Falha")]
        public int CountFailurePasswordFailure { get; set; }

        [DisplayName("Data Criação")]
        public DateTime Created { get; set; }

        [DisplayName("Último Acesso")]
        public DateTime? LastAccess { get; set; }

        [DisplayName("Ativo")]
        public bool Active { get; set; }
    }
}
