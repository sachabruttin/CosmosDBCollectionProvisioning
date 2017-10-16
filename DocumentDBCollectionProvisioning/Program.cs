using DocumentDBCollectionProvisioning.Extensions;
using System;
using System.Linq;

namespace DocumentDBCollectionProvisioning
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var arguments = new PrivisioningArgument();
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
