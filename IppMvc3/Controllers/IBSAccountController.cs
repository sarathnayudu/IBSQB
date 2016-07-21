using IntuitSampleMVC.Business;
using IntuitSampleMVC.Entity;
using IntuitSampleMVC.Models;
using IntuitSampleMVC.Services;
using IntuitSampleMVC.utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace IntuitSampleMVC.Controllers
{
    public class IBSAccountController : BaseController
    {  
        public ActionResult IBSHome()
        {
            FillQBDataIfAny(string.Empty);
            return View();
        }


        public ActionResult SignUp()
        {
            return View(new IBSSignUP());
        }

        public ActionResult SignUpOrLogin()
        {
            SignUPOrLogin model = new SignUPOrLogin();
            model.IBSSignUP = new IBSSignUP();
            model.LogOnModel = new LogOnModel();
            model.IBSSignUP.QBParamObj = new QBParam();

            QBUser qbusr = QBUser;
            model.IBSSignUP.CompanyName = qbusr.CompanyName;
            model.IBSSignUP.Email = qbusr.QBEmail;
            model.IBSSignUP.Name = qbusr.Name;

            model.LogOnModel.UserName = qbusr.QBEmail;

            model.IBSSignUP.isLayout = true;
            model.LogOnModel.isLayout = true;

            return View(model);
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

        public ActionResult LogmeIn()
        {
            return View("Logon", new LogOnModel());
        }

        [HttpPost]
        public ActionResult LogOnFrmQB(IBSSignUP model)
        {
            if (!WebSecurityService.Login(model.Email, model.Password, false))
            {
                ModelState.AddModelError("", "The user name or password provided is incorrect.");
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
                    FillQBDataIfAny(model.UserName);
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

      

     

        // **************************************
        // URL: /Account/LogOff
        // **************************************

        public ActionResult LogOff()
        {
            WebSecurityService.Logout();

            return RedirectToAction("Index","Logout");
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
                var token = WebSecurityService.CreateUserAndAccount(model.Email, model.Password,
                    propertyValues: new
                                                {
                                                    UserName = model.Name,
                                                    CompanyName = model.CompanyName,
                                                    Country = model.Country,
                                                    CreatedDate = DateTime.Now,
                                                    UpdatedDate = DateTime.Now,
                                                    PhoneNumber = model.PhoneNumber
                                                },
            requireConfirmationToken: requireEmailConfirmation);

                if (!Roles.IsUserInRole(model.Email, "Admin"))
                    Roles.AddUserToRole(model.Email, "Admin");


                LogOnModel lgmodel = new LogOnModel();
                lgmodel.UserName = model.Email;
                lgmodel.Password = model.Password;

                return LogOn(lgmodel, string.Empty);
            }

            // If we got this far, something failed, redisplay form
            ViewBag.PasswordLength = WebSecurityService.MinPasswordLength;
            return View("SignUp", model);
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

        // **************************************
        // URL: /Account/LogOn
        // **************************************
        [HttpPost]
        public ActionResult Pre2LogOn(PreLogin model)
        {
            LogOnModel lm = new LogOnModel();
            lm.CompanyName = model.Companyname;
            lm.UserName = model.UserName;
            lm.Password = string.Empty;
            return View("LogOn", lm);
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
