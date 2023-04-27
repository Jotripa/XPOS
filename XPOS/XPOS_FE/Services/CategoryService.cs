using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using XPOS_API.Models;
using XPOS_ViewModels;
using System.Text;

namespace XPOS_FE.Services
{
    public class CategoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();

        public CategoryService(IConfiguration _config)
        {
            //calling API port
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }
        public async Task<List<TblCategory>> AllCategory()
        {
            //calling api endpoint and data json
            List<TblCategory> dataCategory = new List<TblCategory>();
            string apiRespons = await client.GetStringAsync(RouteAPI + "APICategory/GetAllCategory");

            //Change data string json to object
            dataCategory = JsonConvert.DeserializeObject<List<TblCategory>>(apiRespons);

            return dataCategory;
        }
        public async Task<TblCategory> GetById(int id)
        {
            TblCategory data = new TblCategory();
            string apiRespons = await client.GetStringAsync(RouteAPI + $"APICategory/GetById/{id}");
            data = JsonConvert.DeserializeObject<TblCategory>(apiRespons);
            return data;
        }
        public async Task<VMRespons> PutCategory(TblCategory data)
        {
            //Change object to string
            string Datajson = JsonConvert.SerializeObject(data);

            //change string to json to API
            StringContent content = new StringContent(Datajson, UnicodeEncoding.UTF8, "application/json");

            //calling endpoint API and send data 
            string url = RouteAPI + "ApiCategory/PutCategory";
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
        public async Task<VMRespons> PostCategory(TblCategory data)
        {
            string DataJson = JsonConvert.SerializeObject(data);
            StringContent content = new StringContent(DataJson, UnicodeEncoding.UTF8, "application/json");

            string url = RouteAPI + "ApiCategory/PostCategory";
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
        public async Task<VMRespons> DeleteCategory(int id)
        {
            string url = RouteAPI + $"ApiCategory/DeleteCategory/{id}";
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
        private TblCategory View()
        {
            throw new NotImplementedException();
        }
    }
}
