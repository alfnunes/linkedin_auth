using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LinkedinTestAPP
{
    public static class OAuthExtension
    {
        public static AuthenticationBuilder AddLinkedin(this AuthenticationBuilder builder, Action<LinkedinOptions> linkedinOptions)
        {
            return builder.AddOAuth<LinkedinOptions, LinkedinHandler>("LinkedIn", linkedinOptions);
        }
    }
}
