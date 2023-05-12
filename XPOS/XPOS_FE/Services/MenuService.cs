using Newtonsoft.Json;
using XPOS_API.Models;
using XPOS_ViewModels;

namespace XPOS_FE.Services
{
    public class MenuService
    {
        private static readonly HttpClient client = new HttpClient();
        private IConfiguration config;
        private string RouteAPI = "";
        private VMRespons respons = new VMRespons();

        public MenuService(IConfiguration _config)
        {
            //calling API port
            this.config = _config;
            this.RouteAPI = this.config["RouteAPI"];
        }
        public async Task<List<VMMenu>> Menu(int IdRole)
        {            
            List<VMMenu> ListMenu = new List<VMMenu>();
            string apiRespons = await client.GetStringAsync(RouteAPI + $"APIMenu/GetMenu/{IdRole}");

            ListMenu = JsonConvert.DeserializeObject<List<VMMenu>>(apiRespons);

            return ListMenu;
        }
    }
}
