using IntuitSampleMVC.Business;
using IntuitSampleMVC.Entity;
using IntuitSampleMVC.Models;
using IntuitSampleMVC.Services;
using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace IntuitSampleMVC.Controllers
{
    public class IBSAccountController : Controller
    {
        public IWebSecurityService WebSecurityService { get; set; }
        public IMessengerService MessengerService { get; set; }

        protected override void Initialize(RequestContext requestContext)
        {
            if (WebSecurityService == null) { WebSecurityService = new WebSecurityService(); }
            if (MessengerService == null) { MessengerService = new MessengerService(); }

            base.Initialize(requestContext);
        }

     
        public ActionResult IBSHome()
        {           
            return View();
        }


        public ActionResult SignUp()
        {
            return View(new IBSSignUP());
        }

        public ActionResult SignUpviaQB(IBSSignUP signupFrmQB)
        {
            return View("../IBSAccount/SignUP", signupFrmQB);
        }

        public ActionResult SignUpFromQB()
        {
            IBSSignUP signupfrmQB = new IBSSignUP();
            signupfrmQB.Name = Session["FriendlyName"].ToString();
            signupfrmQB.Email = Session["FriendlyEmail"].ToString();

            IBSAccount ibsacc = new IBSAccount();
            ibsacc.GetUserByNameEmail(signupfrmQB);

            if (!string.IsNullOrEmpty(signupfrmQB.CompanyName))
            {
                PreLogin obj = new PreLogin();
                obj.Companyname = signupfrmQB.CompanyName;
                obj.UserName = signupfrmQB.Name;
                // Navigate back to the homepage and exit
                //WebSecurityService.Login(model.Name, model.Password);
                return Pre2LogOn(obj);
            }
            return SignUpviaQB(signupfrmQB);
        }


        /// <summary>
        /// Error view for the website.
        /// </summary>
        /// <returns>Action Result.</returns>
        public ActionResult Error()
        {
            ViewBag.Message = "There is an error on our website.";

            return View();
        }


        // **************************************
        // URL: /Account/LogOn
        // **************************************
         [HttpPost]
        public ActionResult Pre2LogOn(PreLogin model)
        {
            LogOnModel lm = new LogOnModel();
            lm.CompanyName=model.Companyname;
            lm.UserName = model.UserName;
            lm.Password = string.Empty;
            return View("LogOn",lm);
        }

        public ActionResult LogOnFromQB(LogOnModel model)
         {            
                 model = new LogOnModel();
                 model.UserName = Session["FriendlyEmail"].ToString();
                 model.Password = "nayudunz";
            
             if (ModelState.IsValid)
             {
                 if (WebSecurityService.Login(model.UserName, model.Password, model.RememberMe))
                 {
                       return RedirectToAction("IBSHome");
                    // return RedirectToAction("Index", "Home");                   
                 }
                 else
                 {
                     ModelState.AddModelError("", "The user name or password provided is incorrect.");
                 }
             }
             // If we got this far, something failed, redisplay form
             return RedirectToAction("IBSHome");
         }

        [HttpPost]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurityService.Login(model.UserName, model.Password, model.RememberMe))
                {
                    CheckForSingleSignON(model);
                    if (Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("IBSHome");
                       // return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "The user name or password provided is incorrect.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        private void CheckForSingleSignON(LogOnModel model)
        {
            IBSAccount srv = new IBSAccount();
            IBSSignUP temp = srv.GetUserByID(model.UserName);

          if (Session["FriendlyEmail"] == null && Session["FriendlyName"] == null)
          {
              Session["OpenIdResponse"] = "True";
              Session["FriendlyEmail"] = temp.Email;
              Session["FriendlyName"] = temp.Name;
          }
           //create session if  toke/secret/relm id exist and blue dot visible =true
            //else connect to qb enable
            //get the Oauth Access token for the user from OauthAccessTokenStorage.xml
            OauthAccessTokenStorageHelper.GetOauthAccessTokenForUser(Session["FriendlyEmail"].ToString(), this);
        }

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            WebSecurityService.Logout();

            return RedirectToAction("Index", "Home");
        }

        // **************************************
        // URL: /Account/Register
        // **************************************

        public ActionResult Register()
        {
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View();
        }

        [HttpPost]
        public ActionResult Register(IBSSignUP model)
        {
            if (ModelState.IsValid)
            {

                // Attempt to register the user
                var requireEmailConfirmation = false;
                var token = WebSecurityService.CreateUserAndAccount(model.Name, model.Password, requireConfirmationToken: requireEmailConfirmation);
                IBSQBService srv = new IBSQBService();
                srv.UpdateUserDetails(model);
                if (requireEmailConfirmation)
                {
                    // Send email to user with confirmation token
                    string hostUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                    string confirmationUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/Confirm?confirmationCode=" + HttpUtility.UrlEncode(token));

                    var fromAddress = "Your Email Address";
                    var toAddress = model.Email;
                    var subject = "Thanks for registering but first you need to confirm your registration...";
                    var body = string.Format("Your confirmation code is: {0}. Visit <a href=\"{1}\">{1}</a> to activate your account.", token, confirmationUrl);

                    // NOTE: This is just for sample purposes
                    // It's generally a best practice to not send emails (or do anything on that could take a long time and potentially fail)
                    // on the same thread as the main site
                    // You should probably hand this off to a background MessageSender service by queueing the email, etc.
                    MessengerService.Send(fromAddress, toAddress, subject, body, true);

                    // Thank the user for registering and let them know an email is on its way
                    return RedirectToAction("Thanks", "Account");
                }
                else
                {
                    PreLogin obj = new PreLogin();
                    obj.Companyname = model.CompanyName;
                    // Navigate back to the homepage and exit
                    //WebSecurityService.Login(model.Name, model.Password);
                    return Pre2LogOn(obj);
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View("SignUp",model);
        }

        public ActionResult Confirm()
        {
            string confirmationToken = Request.QueryString["confirmationCode"];
            WebSecurityService.Logout();

            if (!string.IsNullOrEmpty(confirmationToken))
            {
                if (WebSecurityService.ConfirmAccount(confirmationToken))
                {
                    ViewBag.Message = "Registration Confirmed! Click on the login link at the top right of the page to continue.";
                }
                else
                {
                    ViewBag.Message = "Could not confirm your registration info";
                }
            }

            return View();
        }

        // **************************************
        // URL: /Account/ChangePassword
        // **************************************

        [Authorize]
        public ActionResult ChangePassword()
        {
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurityService.ChangePassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return RedirectToAction("ChangePasswordSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The current password is incorrect or the new password is invalid.");
                }
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View(model);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordModel model)
        {
            var isValid = false;
            var resetToken = string.Empty;

            if (ModelState.IsValid)
            {
                if (WebSecurityService.GetUserId(model.UserName) > -1 && WebSecurityService.IsConfirmed(model.UserName))
                {
                    resetToken = WebSecurityService.GeneratePasswordResetToken(model.UserName);
                    isValid = true;
                }

                if (isValid)
                {
                    string hostUrl = Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);
                    string resetUrl = hostUrl + VirtualPathUtility.ToAbsolute("~/Account/PasswordReset?resetToken=" + HttpUtility.UrlEncode(resetToken));

                    var fromAddress = "Your Email Address";
                    var toAddress = model.Email;
                    var subject = "Password reset request";
                    var body = string.Format("Use this password reset token to reset your password. <br/>The token is: {0}<br/>Visit <a href='{1}'>{1}</a> to reset your password.<br/>", resetToken, resetUrl);

                    MessengerService.Send(fromAddress, toAddress, subject, body, true);
                }
                return RedirectToAction("ForgotPasswordMessage");
            }
            return View(model);
        }

        public ActionResult ForgotPasswordMessage()
        {
            return View();
        }

        public ActionResult PasswordReset()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PasswordReset(PasswordResetModel model)
        {
            if (ModelState.IsValid)
            {
                if (WebSecurityService.ResetPassword(model.ResetToken, model.NewPassword))
                {
                    return RedirectToAction("PasswordResetSuccess");
                }
                else
                {
                    ModelState.AddModelError("", "The password reset token is invalid.");
                }
            }

            return View(model);
        }

        public ActionResult PasswordResetSuccess()
        {
            return View();
        }

        // **************************************
        // URL: /Account/ChangePasswordSuccess
        // **************************************

        public ActionResult ChangePasswordSuccess()
        {
            return View();
        }

        public ActionResult Thanks()
        {
            return View();
        }

        public ActionResult PreLogin(string message)
        {
            PreLogin pl = new PreLogin();
            pl.Companyname = message;
            return View(pl);
        }
        public ActionResult UnderLaw(int? flag)
        {
            ViewBag.flag = flag;
            return View();
        }
    }
}
