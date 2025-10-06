using Microsoft.AspNetCore.Mvc;
using CRUD_CORE.Models;

namespace CRUD_CORE.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Server=HELIOS300\\SQLEXPRESS; Database=APP-NET-CORE; Trusted_Connection=True; TrustServerCertificate=True;";

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registro()
        {
            return View();
        }
    }
}
