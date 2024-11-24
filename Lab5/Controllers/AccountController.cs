using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Auth0.ManagementApi;
using Auth0.ManagementApi.Models;
using Microsoft.AspNetCore.Mvc;
using Lab5.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Lab5.Controllers
{
    public class AccountController : Controller
    {
        // ��������� ������� API ���������� Auth0
        private readonly IManagementApiClient _managementApiClient;

        public AccountController(IManagementApiClient managementApiClient)
        {
            _managementApiClient = managementApiClient;
        }

        // ����� ��� ����� � ������� � �������������� Auth0
        public async Task Login(string returnUrl = "/")
        {
            // ��������� ���������� �������������� ��� �������� ����� ��������� �����
            var authenticationProperties = new LoginAuthenticationPropertiesBuilder()
                .WithRedirectUri(returnUrl)
                .Build();

            await HttpContext.ChallengeAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
        }

        // ����� ��� ������ �� �������
        [Authorize]
        public async Task Logout()
        {
            // ��������� ���������� �������������� ��� ������
            var authenticationProperties = new LogoutAuthenticationPropertiesBuilder()
                .WithRedirectUri(Url.Action("Index", "Home"))
                .Build();

            await HttpContext.SignOutAsync(Auth0Constants.AuthenticationScheme, authenticationProperties);
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        // ����������� ����� �����������
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(AccountModel model)
        {
            // ��������� ������ ����������� ������������
            if (ModelState.IsValid)
            {
                try
                {
                    var userCreateRequest = new UserCreateRequest
                    {
                        Email = model.Email,
                        Password = model.Password,
                        FullName = model.FullName,
                        PhoneNumber = model.PhoneNumber,
                        Connection = "Username-Password-Authentication",
                        EmailVerified = true, // ������������� email ��� ��������������
                        PhoneVerified = true  // ������������� ����� �������� ��� ��������������
                    };

                    // �������� ������������ ����� API ���������� Auth0
                    var user = await _managementApiClient.Users.CreateAsync(userCreateRequest);

                    // ��������������� �� �������� ����� ��� ����������� ��������� �� �������� �����������
                    return RedirectToAction("Login");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "��������� ������ ��� �����������: " + ex.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        // ����������� ������� ������������
        [Authorize]
        public IActionResult Profile()
        {
            // ����� ���� ������ � ������������ �� ��� ����������� (claims)
            foreach (var claim in User.Claims)
            {
                Console.WriteLine($"{claim.Type}: {claim.Value}");
            }
            return View(new
            {
                Name = User.Identity.Name,
                PhoneNumber = User.Claims.FirstOrDefault(c => c.Type == "phone_number")?.Value,
                EmailAddress = User.Claims.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value,
                ProfileImage = User.Claims.FirstOrDefault(c => c.Type == "picture")?.Value
            });
        }
    }
}