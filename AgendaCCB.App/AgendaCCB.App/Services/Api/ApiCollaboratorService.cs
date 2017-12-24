using AgendaCCB.App.Helpers;
using AgendaCCB.App.Models;
using AgendaCCB.App.Services.Commom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AgendaCCB.App.Services.Api
{
    public class ApiCollaboratorService : AgendaCCBApiService
    {
        public async Task<IList<Collaborator>> GetAllCollaborators()
        {
            try
            {
                IList<Collaborator> collaborators = new List<Collaborator>();
                ApiReturn apiReturn = await GetAsync(String.Format(UrlApi.GetCollaborators));

                if (apiReturn.Status == (int)HttpStatusCode.OK)
                {
                    collaborators = JsonConvert.DeserializeObject<IList<Collaborator>>(apiReturn.Object.ToString());
                }

                return collaborators;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Collaborator> GetCollaboratorById(int id)
        {
            try
            {
                Collaborator collaborators = new Collaborator();
                ApiReturn apiReturn = await GetAsync(String.Format(UrlApi.GetCollaborator,id));

                if (apiReturn.Status == (int)HttpStatusCode.OK)
                {
                    collaborators = JsonConvert.DeserializeObject<Collaborator>(apiReturn.Object.ToString());
                }

                return collaborators;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
