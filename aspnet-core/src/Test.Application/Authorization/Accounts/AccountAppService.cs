using System.Security.Policy;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Zero.Configuration;
using Test.Authorization.Accounts.Dto;
using Test.Authorization.Users;
using System.Net;
using System.Net.Mail;
using System;
using System.Web;
using Abp.Domain.Repositories;
using System.Linq;

namespace Test.Authorization.Accounts
{
    public class AccountAppService : TestAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly UserManager _userManager;
        private readonly IRepository<User, long> _repository;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager, UserManager userManager
            , IRepository<User, long> repository)
        {
            _userRegistrationManager = userRegistrationManager;
            _userManager = userManager;
            _repository = repository;
        }

        public async Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input)
        {
            var tenant = await TenantManager.FindByTenancyNameAsync(input.TenancyName);
            if (tenant == null)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.NotFound);
            }

            if (!tenant.IsActive)
            {
                return new IsTenantAvailableOutput(TenantAvailabilityState.InActive);
            }

            return new IsTenantAvailableOutput(TenantAvailabilityState.Available, tenant.Id);
        }

        public void SendEmailConfirmation(string token,int userId,string mail)
        {

            var fromAddress = new MailAddress("omarsawy45@gmail.com", "From DSBA");
            var toAddress = new MailAddress(mail, "To Parent");
            const string fromPassword = "omarsawy1998";

            var user = _repository.FirstOrDefault(u => u.Id == userId);

            var uriBuilder = new UriBuilder("http://localhost:4200/account/loginExternal");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["tokenId"] = token;
            parameters["userId"] = userId.ToString();
            uriBuilder.Query = parameters.ToString();

            Uri finalUrl = uriBuilder.Uri;


            string body = finalUrl.AbsoluteUri;

            string EmailSubject = "Deutsche Schule der Borromäerinnen Alexandria – Activate your account?";

            #region Email Body

            string EmailBody = "";
            EmailBody = "<table align='center' border='0' cellpadding='0' cellspacing='0' bgcolor='#fafafa' style='background-color:#fafafa;border-top:1px solid #e1e1e1;border-left:1px solid #e1e1e1;border-right:1px solid #e1e1e1;border-bottom:1px solid #e1e1e1;' width='90%'>";

            EmailBody = EmailBody + "<tbody><tr><td width='24'>&nbsp;</td><td style='padding-top:16px;'>";
           
            EmailBody = EmailBody + "<td align='right' style='padding-top:10px;'>&nbsp;</td><td width='24'>&nbsp;</td></tr><tr><td width='24'>&nbsp;</td>";
            EmailBody = EmailBody + "<td colspan='2' style='color:#333333;font-family:Arial, Helvetica, sans-serif;font-size:24px;line-height:26px;padding-top:18px;'></td>";
            EmailBody = EmailBody + "<td width='24'>&nbsp;</td></tr><tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#00000;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;'>";
            EmailBody = EmailBody + "Dear &nbsp;" + user.Name + ", </td>";
            EmailBody = EmailBody + "</tr><tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#00000;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;'>  Thank you for registering on the Deutsche Schule der Borromäerinnen Alexandria website  </td></tr>";
            EmailBody = EmailBody + "<td width='24'>&nbsp;</td></tr><tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#00000;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;'>";

            EmailBody = EmailBody + "Your username is: &nbsp; " + user.EmailAddress + "</td>";

            EmailBody = EmailBody + "<tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#00000;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;'> You can now login to the website using your username and password in order to start the registration process.</td></tr>";


            EmailBody = EmailBody + "<tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#00000;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;'>To activate your account please click here:</td></tr>";



            EmailBody = EmailBody + "<td width='24'>&nbsp;</td></tr><tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#858585;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:5px;'>";
            var VarificationUrl = finalUrl;
            EmailBody = EmailBody + "<a href='" + VarificationUrl + "' style='color:#156a9b;text-decoration:none;font-weight:bold;' target='_blank'>Click here to activate your account</a></td>";
            //-----------------------------last row have Varification Url-------------//		

            EmailBody = EmailBody + "<td width='24'>&nbsp;</td></tr><tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#858585;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;'>";
            EmailBody = EmailBody + "If the link above is not working, please copy the following address into your web browser:</td>";

            EmailBody = EmailBody + "<tr><td width='24'>&nbsp;</td><td colspan='2' style='color:#00000;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:5px;'><a href='" + VarificationUrl + "' style='color:#156a9b;text-decoration:none;font-weight:bold;' target='_blank'>" + VarificationUrl + "</a></td></tr>";

            //-----------------------------last row have app url---------------//	
            EmailBody = EmailBody + "<td width='24'>&nbsp;</td></tr><tr><td style='padding-top:18px;padding-bottom:32px;border-bottom:1px solid #e1e1e1;' width='24'>&nbsp;</td>";
            EmailBody = EmailBody + "<td colspan='2' style='color:#858585;font-family:Arial, Helvetica, sans-serif;font-size:14px;line-height:20px;padding-top:18px;padding-bottom:32px;border-bottom:1px solid #e1e1e1;'>We're glad you're here,<br>";
            EmailBody = EmailBody + "Deutsche Schule der Borromäerinnen Alexandria</td><td style='padding-top:18px;padding-bottom:32px;border-bottom:1px solid #e1e1e1;' width='24'>&nbsp;</td></tr></tbody></table>";

            #endregion



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = EmailSubject,
                Body = EmailBody,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }


        }

        public bool CheckUserEmailConfirmation(string userMailOrName)
        {

            var user = _repository.FirstOrDefault(u => u.EmailAddress == userMailOrName
            || u.UserName == userMailOrName);


            if (user.IsEmailConfirmed) return true;

            return false;

        }

        public EmailConfirmationResult ConfirmEmail(string token,int userId)
        {

            var user = _userManager.GetUserById(userId);

            if (user.EmailConfirmationCode == token)
            {
                user.IsEmailConfirmed = true;
                return new EmailConfirmationResult
                {

                    CanLogin = true,
                    userInfo = user,

                };
            }

            return new EmailConfirmationResult
            {
                CanLogin = false,
                userInfo = user,
            };

        }

        public async Task<RegisterOutput> Register(RegisterInput input)
        {
            var user = await _userRegistrationManager.RegisterAsync(
                input.Name,
                input.Surname,
                input.EmailAddress,
                input.UserName,
                input.Password,
                false // Assumed email address is always confirmed. Change this if you want to implement email confirmation.
            );

            user.EmailConfirmationCode = await UserManager.GenerateEmailConfirmationTokenAsync(user);


            SendEmailConfirmation(user.EmailConfirmationCode, (int)user.Id,input.EmailAddress);

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                //CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
                CanLogin = user.IsActive && user.IsEmailConfirmed
            };
        }
    }
}
