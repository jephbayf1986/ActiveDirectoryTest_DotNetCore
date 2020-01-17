using Microsoft.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace testDotNetCoreAdApp.Models
{
    public class UserProfile
    {
        public User User { get; set; }

        public string Photo64Base { get; set; }
    }
}
