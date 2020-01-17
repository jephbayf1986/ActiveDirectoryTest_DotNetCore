using Microsoft.AspNetCore.Authorization;

namespace testDotNetCoreAdApp.Policies
{
    public static class TestAdminPolicy
    {
        public static string Name => "TestAdmin";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.RequireClaim("groups", "1cfb343c-3b6b-4a06-aab0-5b883f277bc7");
    }
}