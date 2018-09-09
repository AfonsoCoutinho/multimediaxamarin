using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia.Clients
{
    class GetEventsHttpClient
    {
        private static Lazy<GetEventsHttpClient> _Lazy = new Lazy<GetEventsHttpClient>(() => new GetEventsHttpClient());
        public static GetEventsHttpClient Current { get => _Lazy.Value; }

        private GetEventsHttpClient()
        {
            _HttpClient = new HttpClient();
        }

        private readonly HttpClient _HttpClient;

        public async Task<List<EventResult>> GetEvents()
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/events"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar os Eventos!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar os Eventos!");

                    RootEventsList res = JsonConvert.DeserializeObject<RootEventsList>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EventResult> GetEvent(int id)
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/events/{id}"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar o Evento!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar o Evento!");

                    RootEventsObject res = JsonConvert.DeserializeObject<RootEventsObject>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class EventResult
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string date { get; set; }
    }

    public class RootEventsObject
    {
        public EventResult data { get; set; }
    }

    public class RootEventsList
    {
        public List<EventResult> data { get; set; }
    }

}
