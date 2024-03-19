using Microsoft.AspNetCore.Mvc;
using PhoneBook.Services;

namespace PhoneBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuoteController : ControllerBase
    {
        private readonly IQuoteService _quoteService;

        public QuoteController(IQuoteService qouteService)
        {
            _quoteService = qouteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetRandomMotivationalQuote()
        {
            try
            {
                var qoute = await _quoteService.GetRandomQuote();

                return Ok(qoute);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}