using Microsoft.AspNetCore.Authorization;

namespace testDotNetCoreAdApp.Policies
{
    public class AuthorizeTestAdmin : AuthorizeAttribute
    {
        public AuthorizeTestAdmin() : base(TestAdminPolicy.Name)
        {
        }
    }
}