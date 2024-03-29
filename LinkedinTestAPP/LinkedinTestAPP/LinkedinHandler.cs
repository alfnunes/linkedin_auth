﻿using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;

namespace LinkedinTestAPP
{
    public class LinkedinHandler : OAuthHandler<LinkedinOptions>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedinHandler"/> class.
        /// </summary>
        /// <param name="options">The options.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="encoder">The encoder.</param>
        /// <param name="clock">The clock.</param>
        public LinkedinHandler(IOptionsMonitor<LinkedinOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            // Retrieve user info
            var request = new HttpRequestMessage(HttpMethod.Get, this.Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            request.Headers.Add("x-li-format", "json");

            var response = await this.Backchannel.SendAsync(request, this.Context.RequestAborted);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var user = JObject.Parse(content);

            OAuthCreatingTicketContext context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, this.Context, this.Scheme, this.Options, this.Backchannel, tokens, user);
            context.RunClaimActions();
            await this.Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, this.Scheme.Name);
        }
    }
}