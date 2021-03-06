namespace Nancy.Demo.Authentication.Forms
{
    using System;
    using Nancy;
    using Nancy.Authentication.Forms;

    public class MainModule : NancyModule
    {
        public MainModule()
        {
            Get["/"] = x => {
                return View["index"];
            };

            Get["/login"] = x => {
                return View["login"];
            };

            Post["/login"] = x => {
                var userGuid = UserDatabase.ValidateUser((string)this.Request.Form.Username, (string)this.Request.Form.Password);

                if (userGuid == null)
                {
                    return Response.AsRedirect("/login?error=true");
                }

                DateTime? expiry = null;
                if (this.Request.Form.RememberMe.HasValue)
                {
                    expiry = DateTime.Now.AddDays(7);
                }

                return this.LoginAndRedirect(userGuid.Value, expiry);
            };

            Get["/logout"] = x => {
                return this.LogoutAndRedirect("/");
            };
        }
    }
}