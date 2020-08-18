using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using UI.Infrastructure;
using UI.Models;
using UI.ViewModels;

namespace UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<Api> apiAdd;

        public HomeController(ILogger<HomeController> logger,
                                IOptions<Api> ApiAdd)
        {
            _logger = logger;
            apiAdd = ApiAdd;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View("Campaign");
        }

        [HttpGet]
        [Route("Raport")]
        public async Task<IActionResult> Report()
        {
            var client = new HttpClient();

            var response = client.GetAsync(apiAdd.Value.ApiAdd + "/Campaign/Report");


            if (response.Result.StatusCode == HttpStatusCode.OK)
            {
                var z = JsonConvert.DeserializeObject<decimal>(await response.Result.Content.ReadAsStringAsync());
                ViewBag.Info = true;
                return View("Report", z);
            }
            ViewBag.Info = false;
            return View("Report");
        }

        [HttpGet]
        [Route("Edytuj")]
        public async Task<IActionResult> Edit(int id)
        {
            var client = new HttpClient();

            var response = client.GetAsync(string.Format(apiAdd.Value.ApiAdd + "/Campaign/Get/{0}", id));
            var z = JsonConvert.DeserializeObject<CampaignViewModel>(await response.Result.Content.ReadAsStringAsync());

            return View("Campaign", z);
        }

        [HttpGet]
        [Route("Usuń")]
        public async Task<IActionResult> Delete(int id)
        {
            var client = new HttpClient();

            var response = client.DeleteAsync(string.Format(apiAdd.Value.ApiAdd + "/Campaign/Delete/{0}", id)).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return await List();
            }
            ViewBag.DeleteError = true;
            return View("List");
        }

        [HttpGet]
        [Route("Wszystkie")]
        public async Task<IActionResult> List()
        {
            var client = new HttpClient();

            var response = client.GetAsync(apiAdd.Value.ApiAdd + "/Campaign/GetAll");
            var z = JsonConvert.DeserializeObject<IEnumerable<CampaignViewModel>>(await response.Result.Content.ReadAsStringAsync());

            return View("List",z);
        }

        [HttpGet]
        [Route("PDF")]
        public async Task<IActionResult> pdf()
        {
            var client = new HttpClient();

            var response = client.GetAsync(apiAdd.Value.ApiAdd + "/Campaign/pdf");
            var file = await response.Result.Content.ReadAsByteArrayAsync();
            return File(file, "application/pdf", "Raport.pdf");
        }

        [HttpPost]
        public async Task<IActionResult> Create(CampaignViewModel campaignViewModel)
        {

            var client = new HttpClient();
            HttpContent content = new StringContent(JsonConvert.SerializeObject(campaignViewModel), Encoding.UTF8, "application/json");
            var response = new HttpResponseMessage();

            if (campaignViewModel.Id == null)
            {
                response = await client.PostAsync(apiAdd.Value.ApiAdd + "/Campaign/Create", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ViewBag.Info = true;
                    return View("Campaign", campaignViewModel);
                }
                ViewBag.Info = false;
                return View("Campaign", campaignViewModel);
            }
            else 
            {
                response = await client.PutAsync(apiAdd.Value.ApiAdd + "/Campaign/Update", content);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    ViewBag.Update = true;
                    return View("Campaign", campaignViewModel);
                }
                ViewBag.Update = false;
                return View("Campaign", campaignViewModel);
            }
        }

    }
}
