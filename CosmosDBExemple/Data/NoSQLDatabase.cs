﻿using CosmosDBExample.Config;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;

//  Está no emulador.

namespace CosmosDBExemple.Data
{
    public class NoSQLDatabase<T>
    {
        private static readonly string EndpointUri = AppSettings.CosmosDdEndpointUri;
        private static readonly string PrimaryKey = AppSettings.CosmosDdPrimaryKey;
        private readonly string databaseId = "testandocosmosdb";

        public async Task<IEnumerable<T>> GetAllItens(string containerId)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            var sqlQueryText = "SELECT * FROM c";

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            List<T> lista = new();

            QueryDefinition queryDefinition = new(sqlQueryText);

            var iterator = container.GetItemQueryIterator<T>(queryDefinition);

            while (iterator.HasMoreResults)
            {
                FeedResponse<T> result = await iterator.ReadNextAsync();
                foreach (var item in result)
                {
                    lista.Add(item);
                }
            }

            return lista;
        }

        public async Task<IEnumerable<T>> GetByPredicate(string containerId, Expression<Func<T, bool>> predicate)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            var query = container.GetItemLinqQueryable<T>();
            var iterator = query.Where(predicate).ToFeedIterator();
            var resultado = await iterator.ReadNextAsync();
            return resultado.ToList();
        }

        public async Task Add(string containerId, T data, string id)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            await container.CreateItemAsync<T>(data, new PartitionKey(id));
        }

        public async Task UpdatePessoa(string containerId, string id, T data)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            await container.UpsertItemAsync<T>(data, new PartitionKey(id));
        }

        public async Task DeletePessoa(string containerId, string id)
        {
            CosmosClient cosmosClient = new(EndpointUri, PrimaryKey);

            Database database = cosmosClient.GetDatabase(databaseId);
            Container container = database.GetContainer(containerId);

            await container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }
    }
}