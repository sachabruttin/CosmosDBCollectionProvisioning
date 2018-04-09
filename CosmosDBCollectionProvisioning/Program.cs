using CosmosDBCollectionProvisioning.Extensions;
using System;

namespace CosmosDBCollectionProvisioning
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new ProvisioningArgument();
            var isValid = CommandLine.Parser.Default.ParseArgumentsStrict(args, arguments);

            if (isValid)
            {
                try
                {
                    var provisioning = new Provisioning(arguments);
                    provisioning.CreateAsync().Wait();
                }
                catch (AggregateException ex)
                {
                    Console.WriteLine($"Error during creation of the Collection {arguments.CollectionName} on database {arguments.DatabaseId}:");
                    Console.WriteLine(ex.GetaAllMessages());
                }
            }
        }
    }
}
