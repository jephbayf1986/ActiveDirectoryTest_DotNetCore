using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace testDotNetCoreAdApp.Policies
{
    public class MicrosoftGraphService : IMicrosoftGraphService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfidentialClientApplication _clientApp;
        private readonly OnBehalfOfProvider _authProvider;
        private readonly IConfiguration _configuration;

        private readonly IEnumerable<string> _scopes;
        const string UserAssertionType = "urn:ietf:params:oauth:grant-type:jwt-bearer";

        public MicrosoftGraphService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _scopes = new List<string> { "User.Read", "User.Read.All", "Directory.Read.All" };

            _clientApp = ConfidentialClientApplicationBuilder
                .Create(_configuration["AzureAd:ClientId"])
                .WithTenantId(_configuration["AzureAd:TenantId"])
                .WithRedirectUri(_configuration["AzureAd:BaseUrl"] + "/signin")
                .WithClientSecret(_configuration["AzureAd:ClientSecret"])
                .Build();

            _authProvider = new OnBehalfOfProvider(_clientApp, _scopes);
        }

        public async Task<User> GetUserProfile()
        {
            GraphServiceClient client = new GraphServiceClient(_authProvider);

            User UserProfile = await client.Me.Request().WithUserAssertion(GetUserAssertion()).GetAsync();

            return UserProfile;
        }

        public async Task<string> GetUserPhoto()
        {
            GraphServiceClient client = new GraphServiceClient(_authProvider);

            Stream PhotoStream = await client.Me.Photo.Content.Request().GetAsync();

            return await GetBase64Picture(PhotoStream);
        }

        public async Task<ICollection<string>> GetUserGroupIds()
        {
            GraphServiceClient client = new GraphServiceClient(_authProvider);

            IUserMemberOfCollectionWithReferencesPage MemberReferences =  await client.Me.MemberOf.Request().WithUserAssertion(GetUserAssertion()).GetAsync();

            ICollection<string> GroupList = MemberReferences.CurrentPage.Where(g => g.ODataType == "#microsoft.graph.group")
                                                                                .Select(g => g.Id)
                                                                                    .ToList();

            return GroupList;
        }

        private UserAssertion GetUserAssertion()
        {
            HttpContext httpContext = _httpContextAccessor.HttpContext;

            string AuthHeader = httpContext.Request.Headers["Authorization"];
            string AuthToken = AuthHeader.Replace("Bearer ", "");

            return new UserAssertion(AuthToken, UserAssertionType);
        }

        private async Task<string> GetBase64Picture(Stream PictureStream)
        {
            MemoryStream MemStream = new MemoryStream();

            await PictureStream.CopyToAsync(MemStream);

            byte[] PictureBinary = MemStream.ToArray();

            var pictureBase64 = Convert.ToBase64String(PictureBinary);

            return "data:image/jpeg;base64," + pictureBase64;
        }
    }
}
