using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Api.Hub
{
    public class SemiAnon : AuthorizeAttribute, IAuthorizationFilter
    {
        public SemiAnon() : base(JwtBearerDefaults.AuthenticationScheme)
        {
            
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // this will read `token` parameter from your URL
            Console.WriteLine("myFilter");
        }
    }
}
