using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace GIB2019.Functions
{
    using GIB2019.Contract;
    using GIB2019.Methods;
    using Newtonsoft.Json;

    public static class CosmosDBFeed
    {
        [FunctionName("CosmosDBFeed")]
        public static void Run([CosmosDBTrigger(
            databaseName: "gib2019",
            collectionName: "contacts",
            ConnectionStringSetting = "CosmosDBConnection",
            LeaseCollectionName = "leases",CreateLeaseCollectionIfNotExists = true)]IReadOnlyList<Document> documents, TraceWriter log)
       {
            dynamic result;
            if (documents != null && documents.Count > 0)
            {
                foreach (var document in documents)
                {
                    dynamic dynamicdocument = document;
                    object obj1 = dynamicdocument;


                    switch (dynamicdocument.label)
                    {
                        case "contact":
                            var eventmsg = JsonConvert.SerializeObject(obj1);
                            string topic = "contact";

                            Contact Node = JsonConvert.DeserializeObject<Contact>(eventmsg);

                            result =  EventHandler.CosmosDBEventHander( Node, topic);
                            break;
                        default:
                             result = EventHandler.CosmosDBRelationEventHander(dynamicdocument, "Relation");
                            break;
                    }



                }
            }
        }
    }
}
