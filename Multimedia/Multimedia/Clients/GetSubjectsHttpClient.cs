using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia.Clients
{
    class GetSubjectsHttpClient
    {
        private static Lazy<GetSubjectsHttpClient> _Lazy = new Lazy<GetSubjectsHttpClient>(() => new GetSubjectsHttpClient());
        public static GetSubjectsHttpClient Current { get => _Lazy.Value; }

        private GetSubjectsHttpClient()
        {
            _HttpClient = new HttpClient();
        }

        private readonly HttpClient _HttpClient;

        public async Task<List<SubjectResult>> GetSubjects(int year)
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/subjectsByYear/{year} "))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar o Plano de estudos!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar o Plano de estudos!");

                    RootSubjectList res = JsonConvert.DeserializeObject<RootSubjectList>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }

    public class SubjectResult
    {
        public string semester { get; set; }
        public string subject { get; set; }
        public string ects { get; set; }
    }

    public class RootSubjectList
    {
        public List<SubjectResult> data { get; set; }
    }
}
