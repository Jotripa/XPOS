using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing.Text;
using System.Globalization;
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

        public async Task<VMUserCostumer> Register()
        {
            VMUserCostumer ListRole = new VMUserCostumer();

            return ListRole;
        }
    }
}
