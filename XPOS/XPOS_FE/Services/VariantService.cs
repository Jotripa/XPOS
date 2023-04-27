using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using XPOS_API.Models;
using XPOS_ViewModels;
using System.Text;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace XPOS_FE.Services
{
    public class VariantService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();

        public VariantService(IConfiguration _config)
        {
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }
        //Get All Data
        public async Task<List<VMVariant>> AllVariant()
        {
            List<VMVariant> dataVariant = new List<VMVariant>();
            string apiRespons = await client.GetStringAsync(RouteAPI + "APIVariant/GetAllVariant");

            dataVariant = JsonConvert.DeserializeObject<List<VMVariant>>(apiRespons);

            return dataVariant;
        }
        //Get data by id
        public async Task<VMVariant> GetById(int id)
        {
            VMVariant data = new VMVariant();
            string apiRespons = await client.GetStringAsync(RouteAPI + $"APIVariant/GetById/{id}");
            data = JsonConvert.DeserializeObject<VMVariant>(apiRespons);
            return data;
        }
        public async Task<VMRespons> PutVariant(VMVariant data)
        {
            string Datajson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "ApiVariant/PutVariant";
            var request = await client.PutAsync(url, content);

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
        public async Task<VMRespons> PostVariant(VMVariant data)
        {
            string Datajson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "ApiVariant/PostVariant";
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
        public async Task<VMRespons> DeleteVariant(int id)
        {
            string url = RouteAPI + $"ApiVariant/DeleteVariant/{id}";
            var request = await client.DeleteAsync(url);
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
        
    }
}
