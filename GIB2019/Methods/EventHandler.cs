using GIB2019.Contract;
using Microsoft.Azure.EventGrid;
using Microsoft.Azure.EventGrid.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GIB2019.Methods
{
   public class EventHandler 
    {

        private static string eventgridkey = System.Environment.GetEnvironmentVariable("eventgridkey");
        private static string eventgridendpoint = System.Environment.GetEnvironmentVariable("eventgridendpoint");
       
        private static string topicHostname = new Uri(eventgridendpoint).Host;

        public static object CosmosDBEventHander(Contact Event, string topicName) 
        {
            List<EventGridEvent> eventlist = new List<EventGridEvent>();
            var events = JsonConvert.SerializeObject(Event);

            dynamic obj = JsonConvert.DeserializeObject(events);

            eventlist.Add(new EventGridEvent()
            {
                Topic = topicName,
                Id = Event.Id,
                EventType = "integration.graph.notification",
                EventTime = DateTime.Now,
                Subject = "IntegrationEvent",
                DataVersion = "1.0",
                Data = Event
            });

            TopicCredentials topicCredentials = new TopicCredentials(eventgridkey);
            EventGridClient client = new EventGridClient(topicCredentials);
            client.PublishEventsAsync(topicHostname, eventlist).GetAwaiter().GetResult();
            return obj;

        }

        public static object CosmosDBRelationEventHander(dynamic Event, string topicName)
        {
            List<EventGridEvent> eventlist = new List<EventGridEvent>();
            var events = JsonConvert.SerializeObject(Event);

            dynamic obj = JsonConvert.DeserializeObject(events);

            eventlist.Add(new EventGridEvent()
            {
                Topic = topicName,
                Id = Event.Id,
                EventType = "integration.graph.notification",
                EventTime = DateTime.Now,
                Subject = "IntegrationEvent",
                DataVersion = "1.0",
                Data = Event
            });

            TopicCredentials topicCredentials = new TopicCredentials(eventgridkey);
            EventGridClient client = new EventGridClient(topicCredentials);
            client.PublishEventsAsync(topicHostname, eventlist).GetAwaiter().GetResult();
            return obj;

        }
    }
}
