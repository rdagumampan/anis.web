using System;
using System.Collections.Generic;
using Arnis.Web.Models;
using Microsoft.Extensions.OptionsModel;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;

namespace Arnis.Web.Repositiories
{
    class WorkspaceRepository : IWorkspaceRepository
    {
        private readonly IOptions<Settings> _settings;
        private IMongoDatabase _database;

        public WorkspaceRepository(IOptions<Settings> settings)
        {
            _settings = settings;
            _database = Connect();
        }

        public IEnumerable<Workspace> All()
        {
            var workspaces = _database.GetCollection<Workspace>("Workspaces").Find(new BsonDocument()).ToListAsync();
            return workspaces.Result;
        }

        public void Add(Workspace workspace)
        {
            _database.GetCollection<Workspace>("Workspaces").InsertOneAsync(workspace);
        }

        public Workspace GetById(ObjectId id)
        {
            var query = Builders<Workspace>.Filter.Eq(e => e.Id, id);
            var workspaces = _database.GetCollection<Workspace>("Workspaces").Find(query).ToListAsync();

            return workspaces.Result.FirstOrDefault();
        }

        public bool Remove(ObjectId id)
        {
            var query = Builders<Workspace>.Filter.Eq(e => e.Id, id);
            var result = _database.GetCollection<Workspace>("Workspaces").DeleteOneAsync(query);

            return GetById(id) == null;
        }

        public void Update(Workspace workspace)
        {
            var query = Builders<Workspace>.Filter.Eq(e => e.Id, workspace.Id);
            var update = _database.GetCollection<Workspace>("Workspaces").ReplaceOneAsync(query, workspace);
        }

        private IMongoDatabase Connect()
        {
            var client = new MongoClient(_settings.Value.ConnectionString);
            var database = client.GetDatabase(_settings.Value.Database);

            return database;
        }
    }
}