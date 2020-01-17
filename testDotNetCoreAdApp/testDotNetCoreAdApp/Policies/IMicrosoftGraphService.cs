using Microsoft.Graph;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace testDotNetCoreAdApp.Policies
{
    public interface IMicrosoftGraphService
    {
        Task<User> GetUserProfile();

        Task<string> GetUserPhoto();

        Task<ICollection<string>> GetUserGroupIds();
    }
}