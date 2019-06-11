using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;
using System.Dynamic;
using Microsoft.AspNetCore.Authentication.OAuth;
using System.Net.Http.Headers;

namespace LinkedinTestAPP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(option =>
            {
                option.LoginPath = "/auth";
                option.LogoutPath = "/auth/logout";
            })
            .AddOAuth("LinkedIn", option =>
            {
                option.ClientId = Configuration["linkedin:clientId"];
                option.ClientSecret = Configuration["linkedin:clientSecret"];
                option.CallbackPath = "/auth/linkedin";
                option.AuthorizationEndpoint = "https://www.linkedin.com/oauth/v2/authorization";
                option.TokenEndpoint = "https://www.linkedin.com/oauth/v2/accessToken";
                option.UserInformationEndpoint = "https://api.linkedin.com/v2/me?projection=(id,firstName,lastName,profilePicture(displayImage~:playableStreams))";
                option.Scope.Clear();
                option.Scope.Add("r_emailaddress");
                option.Scope.Add("r_liteprofile");
                option.Scope.Add("w_member_social");
                option.Events = new OAuthEvents
                {
                    OnCreatingTicket = async context =>
                    {
                        const string urlEmail = "https://api.linkedin.com/v2/emailAddress?q=members&projection=(elements*(handle~))";
                        var user = await GetDataFromEndPoint(context);
                        var email = await GetDataFromEndPoint(context, urlEmail);

                        dynamic userInfo = new ExpandoObject();
                        userInfo.name = user.SelectToken(@"firstName.localized").Values().FirstOrDefault().ToString();
                        userInfo.full_name = $"{userInfo.name} {user.SelectToken(@"lastName.localized").Values().FirstOrDefault().ToString()}";
                        userInfo.display_image = user.SelectToken(@"profilePicture.displayImage~.elements[0].identifiers[0].identifier").ToString();
                        userInfo.email = email.SelectToken("elements[0].handle~.emailAddress").ToString();

                        //Salvar no Banco de Dados

                        if (!string.IsNullOrWhiteSpace(userInfo.name))
                            context.Identity.AddClaim(new Claim(ClaimTypes.Name, userInfo.name, ClaimValueTypes.String, context.Options.ClaimsIssuer));
                        if (!string.IsNullOrWhiteSpace(userInfo.display_image))
                            context.Identity.AddClaim(new Claim("display_image", userInfo.display_image, ClaimValueTypes.String, context.Options.ClaimsIssuer));
                        if (!string.IsNullOrWhiteSpace(userInfo.email))
                            context.Identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userInfo.email, ClaimValueTypes.String, ClaimValueTypes.String, context.Options.ClaimsIssuer));
                    },
                    OnRemoteFailure = context =>
                    {
                        //Handling Cancel On Authentication
                        context.HandleResponse();
                        context.Response.Redirect("/Auth");
                        return Task.FromResult(0);
                    }
                };
            });
            services.AddMvc();
        }

        private static async Task<JObject> GetDataFromEndPoint(OAuthCreatingTicketContext context, string url = default(string))
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.IsNullOrWhiteSpace(url) ? context.Options.UserInformationEndpoint : url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", context.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await context.Backchannel.SendAsync(request, context.HttpContext.RequestAborted);
            response.EnsureSuccessStatusCode();

            return JObject.Parse(await response.Content.ReadAsStringAsync());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDirectoryBrowser();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }


    }
}
