using CommandLine;

namespace CosmosDBCollectionProvisioning
{
    public class ProvisioningArgument
    {
        [Option('u', "uri", Required = true, HelpText = "DocumentDB URI")]
        public string EndpointUri { get; set; }

        [Option('k', "key", Required = true, HelpText = "Key to access to DocumentDB")]
        public string AccessKey { get; set; }

        [Option('d', "databaseId", Required = true, HelpText = "DocumentDB database id (name)")]
        public string DatabaseId { get; set; }

        [Option('c', "collection", Required = true, HelpText = "Collection name to create")]
        public string CollectionName { get; set; }

        [Option('t', "throughput", Required = false, HelpText = "Collection throughput", DefaultValue = 400)]
        public int ThroughPut { get; set; }
    }
}
