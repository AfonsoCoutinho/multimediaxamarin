using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia.Clients
{
    class GetOffersHttpClient
    {
        private static Lazy<GetOffersHttpClient> _Lazy = new Lazy<GetOffersHttpClient>(() => new GetOffersHttpClient());
        public static GetOffersHttpClient Current { get => _Lazy.Value; }

        private GetOffersHttpClient()
        {
            _HttpClient = new HttpClient();
        }

        private readonly HttpClient _HttpClient;

        public async Task<List<OfferResult>> GetOffers()
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/offers"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar as Propostas!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar as Propostas!");

                    RootOfferList res = JsonConvert.DeserializeObject<RootOfferList>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OfferResult> GetOffer(int id)
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/offers/{id}"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar a Proposta!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar a Proposta!");

                    RootOfferObject res = JsonConvert.DeserializeObject<RootOfferObject>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class OfferResult
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string date { get; set; }
    }

    public class RootOfferObject
    {
        public OfferResult data { get; set; }
    }

    public class RootOfferList
    {
        public List<OfferResult> data { get; set; }
    }

}
