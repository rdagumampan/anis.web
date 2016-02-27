using System.Collections.Generic;
using Arnis.Web.ApiModels;
using MongoDB.Bson;

namespace Arnis.Web.Repositiories
{

    public interface IWorkspaceRepository
    {
        IEnumerable<Workspace> All();

        Workspace GetById(ObjectId id);

        void Add(Workspace workspace);

        void Update(Workspace workspace);

        bool Remove(ObjectId id);
    }
}
