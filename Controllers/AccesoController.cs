using Microsoft.AspNetCore.Mvc;
using CRUD_CORE.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlClient;
using System.Text.Json;
using System.Data;
using Microsoft.AspNetCore.Http;


namespace CRUD_CORE.Controllers
{
    public class AccesoController : Controller
    {
        static string cadena = "Server=ANDERSONSOSA\\SQLEXPRESS; Database=APP-NET-CORE; Trusted_Connection=True; TrustServerCertificate=True;";

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Registrar()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Registrar(Usuario oUsuario)
        {
            bool registrado = false;
            string mensaje = "";

            if (oUsuario.clave == oUsuario.confirmar_clave) {
                oUsuario.clave = ConvertirSha256(oUsuario.clave);
            }
            else
            {
                ViewData["Mensaje"] = "Las claves no coinciden";
                return View();
            }

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_RegistrarUsuario", cn);
                cmd.Parameters.AddWithValue("nombre", oUsuario.nombre);
                cmd.Parameters.AddWithValue("usuario", oUsuario.usuario);
                cmd.Parameters.AddWithValue("clave", oUsuario.clave);
                cmd.Parameters.AddWithValue("correo", oUsuario.correo);
                cmd.Parameters.AddWithValue("id_rol", oUsuario.id_rol);
                cmd.Parameters.AddWithValue("url_foto_perfil", oUsuario.url_foto_perfil);
                cmd.Parameters.Add("Registrado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 100).Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                cmd.ExecuteNonQuery();

                registrado = Convert.ToBoolean(cmd.Parameters["Registrado"].Value);
                mensaje = cmd.Parameters["Mensaje"].Value.ToString();


            }

            ViewData["Mensaje"] = mensaje;

            if (registrado)
            {
                return RedirectToAction("Login", "Acceso");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Login(Usuario oUsuario)
        {
            oUsuario.clave = ConvertirSha256(oUsuario.clave);

            using (SqlConnection cn = new SqlConnection(cadena))
            {
                SqlCommand cmd = new SqlCommand("sp_ValidarUsuario", cn);
                cmd.Parameters.AddWithValue("usuario", oUsuario.usuario);
                cmd.Parameters.AddWithValue("clave", oUsuario.clave);
                cmd.CommandType = CommandType.StoredProcedure;

                cn.Open();

                oUsuario.id= Convert.ToInt32(cmd.ExecuteScalar());
            }

            if (oUsuario.id != 0)
            {
                string usuarioJson = JsonSerializer.Serialize(oUsuario);
                HttpContext.Session.SetString("usuario", usuarioJson);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["Mensaje"] = "Usuario no encontrado";
                return View();
            }
        }

        public static string ConvertirSha256(string texto)
        {
            
            StringBuilder sb = new StringBuilder();
            using (var hash = System.Security.Cryptography.SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(texto));

                foreach (byte b in result)
                    sb.Append(b.ToString("x2"));
            }
            return sb.ToString();
        }   


    }
}
