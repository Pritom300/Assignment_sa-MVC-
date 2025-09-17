using Assignment_Sa.Data;
using Assignment_Sa.Dto;
using Assignment_Sa.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace Assignment_Sa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserRepository _userRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _userRepository.GetByUsernameAsync(model.Username) != null)
            {
                ModelState.AddModelError(nameof(model.Username), "Username already taken.");
                return View(model);
            }

            // First user = Admin, else Sales
            //string role = await _userRepository.AnyAsync() ? "Sales" : "Admin";

            string role = "Sales";

            var user = new Models.User
            {
                Username = model.Username,
                FullName = model.FullName,
                PasswordHash = ComputeSha256Hash(model.Password),
                Role = role
            };

            await _userRepository.AddAsync(user);

            // Auto-login after registration
            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto model)
        {
            if (!ModelState.IsValid) return View(model);

            string passwordHash = ComputeSha256Hash(model.Password);
            var user = await _userRepository.GetByUsernameAndPasswordAsync(model.Username, passwordHash);

            if (user == null)
            {
               
                ViewBag.Error = "Invalid username or password";
                return View(model);
            }

            HttpContext.Session.SetInt32("UserId", user.UserId);
            HttpContext.Session.SetString("Username", user.Username);
            HttpContext.Session.SetString("Role", user.Role);

            return RedirectToAction("Index", "Home");
        }

       
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // extract passport
        private string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                StringBuilder builder = new StringBuilder();
                foreach (var b in bytes)
                    builder.Append(b.ToString("x2"));
                return builder.ToString();
            }
        }

        public IActionResult AccessDenied()
        {

            return View();
        }

    }
}
