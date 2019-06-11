using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace LinkedinTestAPP
{
    public class LinkedinOptions : OAuthOptions
    {
        public LinkedinOptions()
        {
            CallbackPath = new PathString("/signin-linkedin");
            AuthorizationEndpoint = LinkedInDefaults.AuthorizationEndpoint;
            TokenEndpoint = LinkedInDefaults.TokenEndpoint;

            UserInformationEndpoint = LinkedInDefaults.UserInformationEndpoint;
            Scope.Add("r_basicprofile");
            Scope.Add("r_emailaddress");

            ClaimActions.MapJsonKey(ClaimTypes.NameIdentifier, "id", ClaimValueTypes.String);
            ClaimActions.MapJsonKey(ClaimTypes.Name, "formattedName", ClaimValueTypes.String);
            ClaimActions.MapJsonKey(ClaimTypes.Email, "emailAddress", ClaimValueTypes.Email);
            ClaimActions.MapJsonKey("picture", "pictureUrl", ClaimValueTypes.String);
        }
    }
}
