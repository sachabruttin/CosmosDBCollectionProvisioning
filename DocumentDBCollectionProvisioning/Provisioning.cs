using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CosmosDBCollectionProvisioning
{
    public class Provisioning
    {
        private readonly DocumentClient _client;
        private readonly ProvisioningArgument _args;

        public Provisioning(ProvisioningArgument args)
        {
            _args = args;
            _client = new DocumentClient(new Uri(args.EndpointUri), args.AccessKey);
        }

        public async Task CreateAsync()
        {
            var database = await GetOrCreateDatabaseAsync(_args.DatabaseId).ConfigureAwait(false);
            await CreateCollectionAsync(database, _args.CollectionName, _args.ThroughPut).ConfigureAwait(false);
        }

        private async Task CreateCollectionAsync(Database database, string collectionName, int throughPut)
        {
            var collection = await _client.CreateDocumentCollectionIfNotExistsAsync(database.SelfLink, new DocumentCollection { Id = collectionName }).ConfigureAwait(false);

            // Fetch the current offer to be updated
            var offer = _client.CreateOfferQuery()
                                .Where(o => o.ResourceLink == collection.Resource.SelfLink)
                                .AsEnumerable().Single();

            // Set the throughput to the new value
            offer = new OfferV2(offer, throughPut);

            // Now persist these changes to the database by replacing the original resource
            await _client.ReplaceOfferAsync(offer).ConfigureAwait(false);
            Console.WriteLine($"Collection {collectionName} created on database {database.Id} with a throughput of {throughPut}");
        }

        private async Task<Database> GetOrCreateDatabaseAsync(string id)
        {
            // Get the database by name, or create a new one if one with the name provided doesn't exist.
            // Create a query object for database, filter by name.
            IEnumerable<Database> query = from db in _client.CreateDatabaseQuery()
                                          where db.Id == id
                                          select db;

            // Run the query and get the database (there should be only one) or null if the query didn't return anything.
            var database = query.FirstOrDefault();
            if (database == null)
            {
                database = await _client.CreateDatabaseAsync(new Database { Id = id }).ConfigureAwait(false);
            }

            return database;
        }
    }
}
