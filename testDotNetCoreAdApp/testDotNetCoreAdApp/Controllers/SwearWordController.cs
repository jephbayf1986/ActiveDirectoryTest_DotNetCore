using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using testDotNetCoreAdApp.Data;
using testDotNetCoreAdApp.Models;

namespace testDotNetCoreAdApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SwearWordController : ControllerBase
    {
        private readonly ApplicationDbContext _db;

        public SwearWordController(ApplicationDbContext db)
        {
            _db = db;
        }

        public ApplicationDbContext Db { get; }

        // GET: api/SwearWord
        [HttpGet]
        public ICollection<SwearWord> Get()
        {
            ICollection<SwearWord> SwearWords = _db.SwearWord.ToList();
            
            return SwearWords;
        }

        // GET: api/SwearWord/5
        [HttpGet("{id}", Name = "Get")]
        public SwearWord Get(int id)
        {
            SwearWord RequestedWord = _db.SwearWord.FirstOrDefault(x => x.Id == id);

            return RequestedWord;
        }
    }
}