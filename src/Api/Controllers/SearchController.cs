using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SearchEngine;
using Shared.Services;
using System;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private SearchStrategyFactory factory;
        private ILogger logger;
        private ICachingService cachingService;
        private int numberResults;

        public SearchController(SearchStrategyFactory searchFactory, ILoggerFactory logFactory, ICachingService cachingService, IConfiguration config)
        {
            this.factory = searchFactory;
            this.logger = logFactory.CreateLogger("Search.Api");
            this.cachingService = cachingService;
            this.numberResults = int.Parse(config.GetSection("AppConfig:NumResults").Value);
        }

        // GET api/search/google?ulr=www.sympli.com.au&text=e-settlements
        [HttpGet("{engine}")]
        public async Task<ActionResult<string>> Get(string engine, string url, string text)
        {
            try
            {                
                var key = $"{engine}-{url}-{text}";
                var result = cachingService.Get(key);

                if(result == null)
                {
                    var responce = await factory.Get(engine).SearchAsync(url, text, numberResults);

                    result = responce.ContentStr;

                    if(string.IsNullOrWhiteSpace(result))
                    {
                        return NotFound();
                    }

                    cachingService.Set(key, result);
                }                

                return Ok(result);               
            }
            catch (Exception e)
            {
                logger.LogCritical(e, "Exception happend in SearchController.Get method.");

                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }        
    }
}
