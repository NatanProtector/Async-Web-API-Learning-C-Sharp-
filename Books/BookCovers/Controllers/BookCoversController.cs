using Microsoft.AspNetCore.Mvc;

namespace BookCovers.Controllers
{
    [ApiController]
    [Route("api/bookcovers")]
    public class BookCoversController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "Book Covers API is running.";
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBookCover(Guid id,
            bool returnFault = false)
        {

            if (returnFault)
            {
                await Task.Delay(1000); // Simulate async operation
                return StatusCode(500, "Simulated fault");
            }

            // Generetate "book cover" (byte array) between 5 and 10 megabytes
            var random = new Random();
            int kb = 1024;
            int mgb = kb * kb;
            int sizeInBytes = random.Next(5 * mgb, 10 * mgb);
            byte[] bookCover = new byte[sizeInBytes];
            random.NextBytes(bookCover);

            Console.WriteLine($"Generated book cover of size {sizeInBytes / (double)mgb:F2} MB for book with id {id}.");

            return Ok(new
            {
                Id = id,
                Cover = bookCover
            });
        }
    }
}
