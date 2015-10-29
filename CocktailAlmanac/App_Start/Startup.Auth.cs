using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using CocktailAlmanac.Models;
using System.Configuration;
using System.Security.Claims;

namespace CocktailAlmanac
{
    public partial class Startup
    {
        // Weitere Informationen zum Konfigurieren der Authentifizierung finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301864".
        public void ConfigureAuth(IAppBuilder app)
        {
            // Konfigurieren des db-Kontexts, des Benutzer-Managers und des Anmelde-Managers für die Verwendung einer einzelnen Instanz pro Anforderung.
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Anwendung für die Verwendung eines Cookies zum Speichern von Informationen für den angemeldeten Benutzer aktivieren
            // und ein Cookie zum vorübergehenden Speichern von Informationen zu einem Benutzer zu verwenden, der sich mit dem Anmeldeanbieter eines Drittanbieters anmeldet.
            // Konfigurieren des Anmeldecookies.
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Aktiviert die Anwendung für die Überprüfung des Sicherheitsstempels, wenn sich der Benutzer anmeldet.
                    // Dies ist eine Sicherheitsfunktion, die verwendet wird, wenn Sie ein Kennwort ändern oder Ihrem Konto eine externe Anmeldung hinzufügen.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Aktiviert die Anwendung für das vorübergehende Speichern von Benutzerinformationen beim Überprüfen der zweiten Stufe im zweistufigen Authentifizierungsvorgang.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Aktiviert die Anwendung für das Speichern der zweiten Anmeldeüberprüfungsstufe (z. B. Telefon oder E-Mail).
            // Wenn Sie diese Option aktivieren, wird Ihr zweiter Überprüfungsschritt während des Anmeldevorgangs auf dem Gerät gespeichert, von dem aus Sie sich angemeldet haben.
            // Dies ähnelt der RememberMe-Option bei der Anmeldung.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Auskommentierung der folgenden Zeilen aufheben, um die Anmeldung mit Anmeldeanbietern von Drittanbietern zu ermöglichen
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            app.UseFacebookAuthentication(
               appId: "1632504923704609",
               appSecret: "027e62ad420a8cc467314186365c9b8a");

            //var googleOptions = new GoogleOAuth2AuthenticationOptions
            //{
            //    ClientId = "30442445639-o8mv4a65gqvdkdn1q3s02nl1hrqdp93p.apps.googleusercontent.com",
            //    ClientSecret = "dHYY0SO3r8E0CG8SXFgB_c4d"
            //};
            //app.UseGoogleAuthentication(googleOptions);

            #region Google
            var googleAuthenticationOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = ConfigurationManager.AppSettings["Google_AppID"],
                ClientSecret = ConfigurationManager.AppSettings["Google_AppSecret"]
            };
            googleAuthenticationOptions.Scope.Add("profile");
            googleAuthenticationOptions.Scope.Add("email");
            googleAuthenticationOptions.Scope.Add("https://www.googleapis.com/auth/plus.login");
            googleAuthenticationOptions.Provider = new GoogleOAuth2AuthenticationProvider()
            {
                OnAuthenticated = async context =>
                {
                    string claimType;
                    bool bAddClaim = false;
                    foreach (var claim in context.User)
                    {

                        claimType = string.Empty;
                        bAddClaim = false;
                        switch (claim.Key)
                        {
                            case "given_name":
                                claimType = "FirstName";
                                bAddClaim = true;
                                break;
                            case "family_name":
                                claimType = "LastName";
                                bAddClaim = true;
                                break;
                            case "gender":
                                claimType = "gender";
                                bAddClaim = true;
                                break;
                            case "image":
                                claimType = "image";
                                bAddClaim = true;
                                break;
                            case "language":
                                claimType = "language";
                                bAddClaim = true;
                                break;
                        }

                        if (bAddClaim)
                        {
                            string claimValue = claim.Value.ToString();
                            if (!context.Identity.HasClaim(claimType, claimValue))
                                context.Identity.AddClaim(new Claim(claimType, claimValue, "XmlSchemaString", "Google"));
                        }
                    }
                }
            };
            app.UseGoogleAuthentication(googleAuthenticationOptions);
            #endregion            
        }
    }
}