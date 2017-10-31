using System.Collections.Generic;
using System.Threading.Tasks;
using AgendaCCB.App.Models;

namespace AgendaCCB.App.Services
{
    public interface IAgendaCCBApiService
    {
        Task<List<Collaborator>> GetAllCollaborators();
        Task<Collaborator> GetCollaboratorById(int id);
    }
}