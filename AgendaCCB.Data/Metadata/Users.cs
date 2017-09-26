using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace AgendaCCB.Data.Models
{
    public class UsersMetadata
    {
        [DisplayName("Código")]
        public int Id { get; set; }

        [DisplayName("Nome do usuário")]
        public string UserName { get; set; }

        [DisplayName("Senha")]
        public string Password { get; set; }

        [DisplayName("Confirmar Senha")]
        public string ConfirmPassword { get; set; }

        [DisplayName("E-mail")]
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

    [ModelMetadataType(typeof(UsersMetadata))]
    public partial class Users { }
}
