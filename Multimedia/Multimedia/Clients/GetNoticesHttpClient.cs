using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia.Clients
{
    class GetNoticesHttpClient
    {
        private static Lazy<GetNoticesHttpClient> _Lazy = new Lazy<GetNoticesHttpClient>(() => new GetNoticesHttpClient());
        public static GetNoticesHttpClient Current { get => _Lazy.Value; }

        private GetNoticesHttpClient()
        {
            _HttpClient = new HttpClient();
        }

        private readonly HttpClient _HttpClient;

        public async Task<List<NoticeResult>> GetNotices()
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/notices"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar os Avisos!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar os Avisos!");

                    RootNoticeList res = JsonConvert.DeserializeObject<RootNoticeList>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<NoticeResult> GetNotice(int id)
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/notices/{id}"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar o Aviso!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar o Aviso!");

                    RootNoticeObject res = JsonConvert.DeserializeObject<RootNoticeObject>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


    public class NoticeResult
    {
        public string id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string date { get; set; }
    }

    public class RootNoticeObject
    {
        public NoticeResult data { get; set; }
    }

    public class RootNoticeList
    {
        public List<NoticeResult> data { get; set; }
    }

}
