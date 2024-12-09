using InveonBootcamp.WebApi.Models;
using InveonBootcamp.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace InveonBootcamp.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        private readonly BookService _bookService;
        private readonly RedisService _cacheService;

        public BooksController(BookService bookService, RedisService cacheService)
        {
            _bookService = bookService;
            _cacheService = cacheService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            string cacheKey = "books_all";

            var cachedData = await _cacheService.GetAsync<List<BookItem>>(cacheKey);
            if (cachedData != null)
            {
                return Ok(cachedData);
            }

            var result = _bookService.GetAll();
            if (result.Status != 200)
                return StatusCode(result.Status, result.ProblemDetails);

            await _cacheService.SetAsync(cacheKey, result.Data, TimeSpan.FromMinutes(5));

            return Ok(result.Data);
        }

        [HttpGet("paginated")]
        public async Task<IActionResult> GetPaginated(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            string cacheKey = $"books_paginated_{pageNumber}_{pageSize}";

            var cachedData = await _cacheService.GetAsync<PaginatedResult<BookItem>>(cacheKey);
            if (cachedData != null)
            {
                return Ok(cachedData);
            }

            var result = _bookService.GetAllPaginated(pageNumber, pageSize);
            if (result.Status != 200)
                return StatusCode(result.Status, result.ProblemDetails);

            await _cacheService.SetAsync(cacheKey, result.Data, TimeSpan.FromMinutes(5));

            return Ok(result.Data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            string cacheKey = $"book_{id}";

            var cachedData = await _cacheService.GetAsync<BookItem>(cacheKey);
            if (cachedData != null)
            {
                return Ok(cachedData);
            }

            var result = _bookService.GetById(id);
            if (result.Status != 200)
                return StatusCode(result.Status, result.ProblemDetails);

            await _cacheService.SetAsync(cacheKey, result.Data, TimeSpan.FromMinutes(5));

            return Ok(result.Data);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookItem newBook, [FromQuery] string referenceCode)
        {
            var result = _bookService.Create(newBook, referenceCode);
            if (result.Status != 201)
                return StatusCode(result.Status, result.ProblemDetails);

            await _cacheService.RemoveAsync("books_all");

            return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] BookUpdateDto updatedBook)
        {
            updatedBook.Id = id;
            var result = _bookService.Update(updatedBook);

            if (result.Status != 200)
                return StatusCode(result.Status, result.ProblemDetails);

            await _cacheService.RemoveAsync("books_all");
            await _cacheService.RemoveAsync($"book_{id}");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = _bookService.Delete(id);
            if (result.Status != 200)
                return StatusCode(result.Status, result.ProblemDetails);

            await _cacheService.RemoveAsync("books_all");
            await _cacheService.RemoveAsync($"book_{id}");

            return NoContent();
        }
    }
}
