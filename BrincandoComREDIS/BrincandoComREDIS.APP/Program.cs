using Google.Apis.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrincandoComREDIS.APP
{
    class Program
    {
        static void Main(string[] args)
        {
            string apiKey = "AIzaSyCVGlnfsGaaJ3HYTHVu6OH_jBaIFJQYrH0";
            string cx = "013638634553566051485:xwnfxqp2pso";
            string query = "3 + 2?";
            var svc = new Google.Apis.Customsearch.v1.CustomsearchService(
            new BaseClientService.Initializer { ApiKey = apiKey });
            var listRequest = svc.Cse.List(query);
            listRequest.Cx = cx;
            var search = listRequest.Execute(); //listRequest.Fetch(); This method has been removed
            foreach (var result in search.Items)
            {
                Console.WriteLine("Title: {0}", result.Title.FirstOrDefault());
                Console.WriteLine("Link: {0}\n", result.Link.FirstOrDefault());
            }
            Console.ReadKey();
        }
    }
}
