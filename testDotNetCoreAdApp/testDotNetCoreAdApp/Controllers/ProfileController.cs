using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using testDotNetCoreAdApp.Models;
using testDotNetCoreAdApp.Policies;

namespace testDotNetCoreAdApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IMicrosoftGraphService _graphService;

        public ProfileController(IMicrosoftGraphService graphService)
        {
            _graphService = graphService;
        }

        // GET: api/Profile
        [HttpGet]
        public async Task<UserProfile> Get()
        {
            User User = await _graphService.GetUserProfile();

            string Photo64Base;

            try
            {
                Photo64Base = await _graphService.GetUserPhoto();
            }
            catch (Exception CaughtException)
            {
                Photo64Base = null;
            }

            UserProfile Profile = new UserProfile()
            {
                User = User,
                Photo64Base = Photo64Base
            };

            return Profile;
        }
    }
}
