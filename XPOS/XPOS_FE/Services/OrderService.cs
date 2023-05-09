using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Text;
using XPOS_ViewModels;

namespace XPOS_FE.Services
{
    public class OrderService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();
        public OrderService(IConfiguration _config)
        {
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }

        public  async Task<VMRespons> SubmitPayment(VMOrderHeader dataHeader)
        {
            string DataJson = JsonConvert.SerializeObject(dataHeader);
            var content = new StringContent(DataJson, UnicodeEncoding.UTF8, "application/json");
            string url = RouteAPI + "ApiOrder/SubmitPayment";
            var request = await client.PostAsync(url, content);
            if (request.IsSuccessStatusCode)
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons.Success = false;
                respons.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respons;
        }
        public async Task<List<VMOrderHeader>> GetAllOrder()
        {
            List<VMOrderHeader> ListOrder = new List<VMOrderHeader>();
            string apiRespons = await client.GetStringAsync(RouteAPI + "ApiOrder/GetAllOrder");

            ListOrder = JsonConvert.DeserializeObject<List<VMOrderHeader>>(apiRespons);
            return ListOrder;
        }
    }
}
