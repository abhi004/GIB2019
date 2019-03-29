

namespace GremlinAPI.Configuration
{
    using Gremlin.Net.Driver;
    using Gremlin.Net.Structure.IO.GraphSON;

    public  class Configuration
    {
        public string CosmosGraphHostName { get; set; }
        public string CosmosDatabaseName { get; set; }
        public string CosmosDatabaseCollectionName { get; set; }
        public string CosmosDatabasAuthkey { get; set; }
        public static string CosmosDatabasepartitionsName { get; set; }

        public GremlinServer Server;

        public Configuration()
        {

            CosmosGraphHostName = System.Environment.GetEnvironmentVariable("cosmosDbHostaddreess");
            CosmosDatabaseName = System.Environment.GetEnvironmentVariable("database");
            CosmosDatabaseCollectionName = System.Environment.GetEnvironmentVariable("collection");
            CosmosDatabasepartitionsName = System.Environment.GetEnvironmentVariable("partition");
            CosmosDatabasAuthkey = System.Environment.GetEnvironmentVariable("authKey");

            Server = new GremlinServer(CosmosGraphHostName, 443, enableSsl: true,
                                         username: "/dbs/" + CosmosDatabaseName + "/colls/" + CosmosDatabaseCollectionName,
                                         password: CosmosDatabasAuthkey);
        }

        public static GremlinClient InitializeGremlinClient()
        {
            Configuration conf = new Configuration();

            var _client = new GremlinClient(conf.Server, new GraphSON2Reader(), new GraphSON2Writer(), GremlinClient.GraphSON2MimeType, new ConnectionPoolSettings());
            return _client;
        }

    }
}
