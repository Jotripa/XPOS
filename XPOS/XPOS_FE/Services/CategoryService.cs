using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using XPOS_API.Models;
namespace XPOS_FE.Services
{
    public class CategoryService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";

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

        private TblCategory View()
        {
            throw new NotImplementedException();
        }
    }
}
