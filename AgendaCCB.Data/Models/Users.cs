using System;
using System.Collections.Generic;

namespace AgendaCCB.Data.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullUserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string QuestionPassword { get; set; }
        public string AnswerPassword { get; set; }
        public bool Blocked { get; set; }
        public int CountFailureResponseFailure { get; set; }
        public int CountFailurePasswordFailure { get; set; }
        public DateTime Created { get; set; }
        public DateTime? LastAccess { get; set; }
        public bool Active { get; set; }
    }
}
