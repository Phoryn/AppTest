using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Api.ModelDTO;
using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampaignController : ControllerBase
    {
        private readonly ILogger<CampaignController> _logger;
        private readonly ICampaignCRUDService campaignCRUDService;
        private readonly IPDFService pDFService;

        public CampaignController(ILogger<CampaignController> logger,
                                  ICampaignCRUDService campaignCRUDService,
                                  IPDFService pDFService)
        {
            _logger = logger;
            this.campaignCRUDService = campaignCRUDService;
            this.pDFService = pDFService;
        }

        //[HttpGet]
        //public IEnumerable<WeatherForecast> Get()
        //{
        //    var rng = new Random();
        //    return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //    {
        //        Date = DateTime.Now.AddDays(index),
        //        TemperatureC = rng.Next(-20, 55),
        //        Summary = Summaries[rng.Next(Summaries.Length)]
        //    })
        //    .ToArray();
        //}

        [HttpPost]
        [Route("Create")]
        public string Create(JsonElement campaign)
        {
            var json = campaign.GetRawText();
            var campaignTo = System.Text.Json.JsonSerializer.Deserialize<CampaignDTO>(json);

            var response = campaignCRUDService.Create(campaignTo);
            if (response != null)
            {
                return JsonConvert.SerializeObject(response);
            }
            return null;
        }


        [HttpPut]
        [Route("Update")]
        public IActionResult Update(JsonElement campaign)
        {
            var json = campaign.GetRawText();
            var campaignTo = System.Text.Json.JsonSerializer.Deserialize<CampaignDTO>(json);

            var response = campaignCRUDService.Update(campaignTo);
            if (response == true)
            {
                return Ok();
            }
            return NotFound();
        }


        [HttpGet]
        [Route("GetAll")]
        public string GetAll()
        {
            var response = campaignCRUDService.GetAll();
            if (response != null)
            {
                return JsonConvert.SerializeObject(response);
            }
            return null;
        }

        [HttpGet]
        [Route("Get/{id}")]
        public string Get(string id)
        {
            var idTo = Int32.Parse(id);
            if (idTo == null)
            {
                return null;
            }
            var response = campaignCRUDService.Get(idTo);
            if (response != null)
            {
                return JsonConvert.SerializeObject(response);
            }
            return null;
        }

        [HttpGet]
        [Route("Report")]
        public IActionResult Report()
        {
            var response = campaignCRUDService.Report();
            if (response > 0)
            {
                return Ok(response);
            }
            return NotFound();

        }

        [HttpDelete]
        [Route("Delete/{id}")]
        public IActionResult Delete(string id)
        {
            var idTo = Int32.Parse(id);
            if (idTo == null)
            {
                return null;
            }
            var response = campaignCRUDService.Delete(idTo);
            if (response == true)
            {
                return Ok();
            }
            return NotFound();

        }
        [HttpGet]
        [Route("pdf")]
        public async Task<IActionResult> pdf()
        {
            var collection = campaignCRUDService.GetAll();
            if (collection != null)
            {
                return File(await pDFService.Create(collection), "application/pdf", "Raport.pdf");
            }
            return null;
        }


        //try
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Students.Add(student);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //}
        //catch (DataException /* dex */)
        //{
        //    //Log the error (uncomment dex variable name and add a line here to write a log.
        //    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //}
        //return View(student);
    }
}
