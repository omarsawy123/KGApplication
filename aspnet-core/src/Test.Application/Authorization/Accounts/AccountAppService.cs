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

namespace Test.Authorization.Accounts
{
    public class AccountAppService : TestAppServiceBase, IAccountAppService
    {
        // from: http://regexlib.com/REDetails.aspx?regexp_id=1923
        public const string PasswordRegex = "(?=^.{8,}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?!.*\\s)[0-9a-zA-Z!@#$%^&*()]*$";

        private readonly UserRegistrationManager _userRegistrationManager;
        private readonly UserManager _userManager;

        public AccountAppService(
            UserRegistrationManager userRegistrationManager, UserManager userManager)
        {
            _userRegistrationManager = userRegistrationManager;
            _userManager = userManager;
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

        public void SendEmailConfirmation(string token,int userId)
        {

            var fromAddress = new MailAddress("omarsawy51@gmail.com", "From Name");
            var toAddress = new MailAddress("omarsawy51@gmail.com", "To Name");
            const string fromPassword = "console.write12";
            const string subject = "DSBA Email Confirmation ";

            //string foo = ... coming from user input
            //string baz = ... coming from user input
            //var uri = Microsoft.AspNetCore.Http.HttpContext.Current.Request.Url.AbsoluteUri
            var uriBuilder = new UriBuilder("http://localhost:4200/account/loginExternal");
            var parameters = HttpUtility.ParseQueryString(string.Empty);
            parameters["tokenId"] = token;
            parameters["userId"] = userId.ToString();
            uriBuilder.Query = parameters.ToString();

            Uri finalUrl = uriBuilder.Uri;
            //var request = WebRequest.Create(finalUrl);


            string body = finalUrl.AbsoluteUri;


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
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }


        }

        public bool CheckUserEmailConfirmation(string userMail)
        {
            

            //if (user.IsEmailConfirmed) return true;

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


            SendEmailConfirmation(user.EmailConfirmationCode, (int)user.Id);

            var isEmailConfirmationRequiredForLogin = await SettingManager.GetSettingValueAsync<bool>(AbpZeroSettingNames.UserManagement.IsEmailConfirmationRequiredForLogin);

            return new RegisterOutput
            {
                //CanLogin = user.IsActive && (user.IsEmailConfirmed || !isEmailConfirmationRequiredForLogin)
                CanLogin = user.IsActive && user.IsEmailConfirmed
            };
        }
    }
}
