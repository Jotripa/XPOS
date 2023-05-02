using Newtonsoft.Json;
using System.Text;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_FE.Services
{
    public class ProductService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();

        public ProductService(IConfiguration _config)
        {
            //calling API port
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }
        public async Task<List<VMProduct>> AllProduct()
        {
            //calling api endpoint and data json
            List<VMProduct> dataProduct = new List<VMProduct>();
            string apiRespons = await client.GetStringAsync(RouteAPI + "APIProduct/GetAllProduct");

            //Change data string json to object
            dataProduct = JsonConvert.DeserializeObject<List<VMProduct>>(apiRespons);

            return dataProduct;
        }
        public async Task<VMProduct> GetById(int id)
        {
            VMProduct data = new VMProduct();
            string apiRespons = await client.GetStringAsync(RouteAPI + $"APIProduct/GetById/{id}");
            data = JsonConvert.DeserializeObject<VMProduct>(apiRespons);
            return data;
        }
        public async Task<VMRespons> PutProduct(VMProduct data)
        {
            //Change object to string
            string Datajson = JsonConvert.SerializeObject(data);

            //change string to json to API
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            //calling endpoint API and send data 
            string url = RouteAPI + "ApiProduct/PutProduct";
            var request = await client.PutAsync(url, content);

            //if condition
            if (request.IsSuccessStatusCode)
            {
                //read respons from API
                var apiRespons = await request.Content.ReadAsStringAsync();
                //comvert to object VMRespons
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons.Success = false;
                respons.Message = $"{request.StatusCode} : {request.ReasonPhrase}";
            }
            return respons;
        }
        public async Task<VMRespons> PostProduct(VMProduct data)
        {
            string DataJson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(DataJson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "ApiProduct/PostProduct";
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
        public async Task<VMRespons> DeleteProduct(int id)
        {
            string url = RouteAPI + $"APIProduct/DeleteProduct/{id}";
            var request = await client.DeleteAsync(url);

            if (request.IsSuccessStatusCode)
            {
                var apiRespons = await request.Content.ReadAsStringAsync();
                respons = JsonConvert.DeserializeObject<VMRespons>(apiRespons);
            }
            else
            {
                respons.Success = false;
                respons.Message = $"{request.StatusCode}: {request.ReasonPhrase}";
            }
            return respons;
        }

 
    }
}
