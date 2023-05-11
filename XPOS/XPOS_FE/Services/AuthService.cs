using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Globalization;
using System.Net.WebSockets;
using System.Text;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_FE.Services
{
    public class AuthService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();

        public AuthService(IConfiguration _config)
        {
            //calling API port
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }

        public async Task<List<TblRole>> GetAllRole()
        {
            //calling api endpoint and data json
            List<TblRole> ListRole = new List<TblRole>();
            string apiRespons = await client.GetStringAsync(RouteAPI + "APIRegister/GetRegister");

            //Change data string json to object
            ListRole = JsonConvert.DeserializeObject<List<TblRole>>(apiRespons);

            return ListRole;
        }
        public async Task<VMRespons> Register(VMUserCostumer data)
        {
            string DataJson = JsonConvert.SerializeObject(data);
            var content = new StringContent(DataJson, UnicodeEncoding.UTF8, "application/json");
            string url = RouteAPI + "APIRegister/PostRegister";
            var request = await client.PostAsync(url, content);

            if(request.IsSuccessStatusCode)
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
        
        public async Task<bool> CheckEmailDuplicate(string email)
        {
            bool IsDuplicate = false;
            string apiRespons = await client.GetStringAsync(RouteAPI + $"APIRegister/CheckEmailDuplicate/{email}");
            IsDuplicate = JsonConvert.DeserializeObject<bool>(apiRespons);
            
            return IsDuplicate;

        }
        public async Task<VMUserCostumer> CheckLogin(VMUserCostumer data)
        {
            VMUserCostumer dataUser = new VMUserCostumer();

            string url = RouteAPI + $"APIRegister/CheckLogin/{data.Email}/{data.Password}";
            var apiRespons = await client.GetStringAsync(url);

            dataUser = JsonConvert.DeserializeObject<VMUserCostumer>(apiRespons);

            return dataUser;

        }


    }
}
