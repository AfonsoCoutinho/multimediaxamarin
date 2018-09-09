using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Multimedia.Clients
{
    class GetCourseHttpClient
    {
        private static Lazy<GetCourseHttpClient> _Lazy = new Lazy<GetCourseHttpClient>(() => new GetCourseHttpClient());
        public static GetCourseHttpClient Current { get => _Lazy.Value; }

        private GetCourseHttpClient()
        {
            _HttpClient = new HttpClient();
        }

        private readonly HttpClient _HttpClient;

        public async Task<CourseResult> GetCourse()
        {
            try
            {

                using (var response = await _HttpClient.GetAsync($"http://multimedia.gq/api/course"))
                {
                    if (!response.IsSuccessStatusCode)
                        throw new InvalidOperationException("Não foi possivel consultar a informação do curso!");

                    var result = await response.Content.ReadAsStringAsync();

                    if (string.IsNullOrWhiteSpace(result))
                        throw new InvalidOperationException("Não foi possivel consultar a informação do curso!");

                    RootCourseObject res = JsonConvert.DeserializeObject<RootCourseObject>(result);

                    return res.data;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public class CourseResult
        {
            public string name { get; set; }
            public string description { get; set; }
            public string objectives_description { get; set; }
            public List<ObjectiveResult> objectives { get; set; }
            public string careers_description { get; set; }
            public List<CareerResult> careers { get; set; }
            public List<ContactResult> contacts { get; set; }
        }

        public class ObjectiveResult
        {
            public string description { get; set; }
        }
        public class CareerResult
        {
            public string description { get; set; }
        }
        public class ContactResult
        {
            public string type { get; set; }
            public string name { get; set; }
            public string email { get; set; }
        }

        public class RootCourseObject
        {
            public CourseResult data { get; set; }
        }
    }
}
