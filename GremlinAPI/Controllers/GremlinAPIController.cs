using System;
using Microsoft.AspNetCore.Mvc;

namespace GremlinAPI.Controllers
{
    using Configuration;
    using Gremlin.Net.Driver;
    using GremlinAPI.Convertor;
    using GremlinAPI.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    [Route("api/Contact/[action]")]
    [ApiController]
    public class GremlinAPIController : Controller
    {
        public GremlinAPIController()
        {
            Configuration obj = new Configuration();

        }   
        private static Lazy<GremlinClient> lazyClient = new Lazy<GremlinClient>(Configuration.InitializeGremlinClient);
        private static GremlinClient _client => lazyClient.Value;

        [HttpPost]
        [ActionName("CreateContact")]
        public async Task<ActionResult> CreateContact([FromBody]Contact contact)
        {
            try
            {
                var result = (dynamic)null;

                Type type = typeof(Contact);
                PropertyInfo[] properties = type.GetProperties();
                string partition = Configuration.CosmosDatabasepartitionsName;
                string partitionquery = $".property('{partition}','{contact.Country}'";

                StringBuilder builder = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {
                    string query = $".property('{property.Name.ToLower()}','{property.GetValue(contact, null)}')";
                    builder.Append(query);
                }
                var queryString = string.Concat($"g.addV('contact')", builder, $".property('{partition}','users').property('name','{contact.FirstName}')");
                result = await _client.SubmitAsync<dynamic>(queryString);

                var responsemessage = JsonConvert.SerializeObject(result);
                var response = JsonConvert.DeserializeObject<List<Contact>>(responsemessage);

                return this.Content(JsonConvert.SerializeObject(response), "application/json");
            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpPatch]
        [ActionName("UpdateContact")]
        public async Task<ActionResult> UpdateContact([FromBody]Contact contact)
        {
            try
            {
                var result = (dynamic)null;

                Type type = typeof(Contact);
                PropertyInfo[] properties = type.GetProperties();
                string partition = Configuration.CosmosDatabasepartitionsName;
                string partitionquery = $".property('{partition}','{contact.Country}'";

                StringBuilder builder = new StringBuilder();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name != "Id")
                    {
                        string query = $".property('{property.Name.ToLower()}','{property.GetValue(contact, null)}')";
                        builder.Append(query);
                    }


                }
                var queryString = string.Concat($"g.V('{contact.Id}')", builder, $".property('name','{contact.FirstName}')");
                result = await _client.SubmitAsync<dynamic>(queryString);

                var responsemessage = JsonConvert.SerializeObject(result);
                var response = JsonConvert.DeserializeObject<List<Contact>>(responsemessage);

                return this.Content(JsonConvert.SerializeObject(response), "application/json");
            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpGet]
        [ActionName("GetContactWithNodeId")]
        public async Task<ActionResult> GetContactWithNodeId(string Nodeid)
        {
            try
            {
                var result = (dynamic)null;

                string partition = Configuration.CosmosDatabasepartitionsName;

                string query = $"g.V('{Nodeid}').has('{partition}','users')";

                result = await _client.SubmitAsync<dynamic>(query);
                var responsemessage = JsonConvert.SerializeObject(result);
                var response = JsonConvert.DeserializeObject<List<Contact>>(responsemessage);

                return this.Content(JsonConvert.SerializeObject(response), "application/json");
            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpDelete]
        [ActionName("DeleteContact")]
        public async Task<ActionResult> DeleteContact(string Nodeid)
        {
            try
            {
                var result = (dynamic)null;

                string query = $"g.V('{Nodeid}').Drop()";

                result = await _client.SubmitAsync<dynamic>(query);
                var responsemessage = JsonConvert.SerializeObject(result);
                var response = JsonConvert.DeserializeObject<List<Contact>>(responsemessage);

                return this.Content(JsonConvert.SerializeObject(response), "application/json");
            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }

        }

        [HttpPost]
        [ActionName("RealtionshipWithNodeId")]
        public async Task<ActionResult> RealtionshipWithNodeId([FromBody] List<GraphRealtionship<string>> NodeProperty)
        {
            try
            {
                var result = (dynamic)null;
                foreach (var item in NodeProperty)
                {
                    string querystring = $"g.V('{item.SourceNode}').addE('{item.EnumConstant}').to(g.V('{item.DestinationNode}'))";
                    result = await _client.SubmitAsync<dynamic>(querystring);

                }
                var responsemessage = JsonConvert.SerializeObject(result);
                var response = JsonConvert.DeserializeObject<List<Contact>>(responsemessage);

                return this.Content(JsonConvert.SerializeObject(response), "application/json");

            }

            catch (Exception ex)
            {
                return Content(ex.Message);
            }
        }
    }


}