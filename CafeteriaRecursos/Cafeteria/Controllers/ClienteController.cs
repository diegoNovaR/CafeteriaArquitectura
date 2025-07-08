using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cafeteria.Models;
using System.Diagnostics;
using System.Text.Json; //PRUEBA


namespace Cafeteria.Controllers
{
    public class ClienteController : Controller
    {
        /*
         private readonly ICafeRepositorio ICafeRepositorio;
         public ClienteController(ICafeRepositorio ICafeRepositorio)
         {
             this.ICafeRepositorio = ICafeRepositorio;
         }
        */

        
        public IActionResult Crear_Usuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Crear_Usuario(Cliente nuevoCliente, [FromServices] IClienteRepositorio repo)
        {
            if (repo.ExisteUsuario(nuevoCliente.Usuario))//verificamos que exista
            {
                ViewBag.Error = "El nombre de usuario ya existe.";
                return View();
            }

            repo.AgregarCliente(nuevoCliente);
            HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(nuevoCliente));//guardamos en la sesión del naveg.
            return RedirectToAction("Index","Home");
        }

        /*
         [HttpPost]
         public IActionResult Crear_usuario(Cliente clienteSet)//prueba cliente cliente
         {

             var cliente = new Cliente()//prueba
             {
                 Nombre = clienteSet.Nombre,
                 Apellido = clienteSet.Apellido,
                 Usuario = clienteSet.Usuario,
                 Contrasena = clienteSet.Contrasena
             };
             var modelo = new HomeIndexViewModel() { MisClientes = cliente };//prueba

             HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(cliente));//PRUEBA
             return RedirectToAction("Index", "Home");
         }
        */
        
        public IActionResult UsuarioPerfil()
        {
            Cliente? cliente = null;
            string? clienteJson = HttpContext.Session.GetString("Cliente");
            if (!string.IsNullOrEmpty(clienteJson))
            {
                cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
            }

                return View(cliente);
        }

        /*
        public IActionResult Pedidos()
        {
            var cafes = ICafeRepositorio.ObtenerCafe();
            return View(cafes);
        }
        */


        /*
        [HttpPost]
        public IActionResult AgregarPedido(string nombreCafe)
        {
            var cafes = ICafeRepositorio.ObtenerCafe();
            var cafeSeleccionado = cafes.FirstOrDefault(c => c.Nombre == nombreCafe);
            
            if(cafeSeleccionado != null)
            {
                var clienteJson = HttpContext.Session.GetString("Cliente");
                if (!string.IsNullOrEmpty(clienteJson))
                {
                    var cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
                    cliente?.Pedidos.Add(cafeSeleccionado);
                    HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(cliente));
                }
            }
            return RedirectToAction("Pedidos");
        }
        */

        /*
        [HttpPost]
        public IActionResult EliminarPedido(int index)
        {
            string? clienteJson = HttpContext.Session.GetString("Cliente");
            if (clienteJson != null)
            {
                var cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
                if (cliente != null && index >= 0 && index < cliente.Pedidos.Count)
                {
                    cliente.Pedidos.RemoveAt(index);
                    HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(cliente));
                }
            }

            return RedirectToAction("VerPedidos");
        }
        */



        /*
          public IActionResult VerPedidos()
        {
            string? clienteJson = HttpContext.Session.GetString("Cliente");
            if (!string.IsNullOrEmpty(clienteJson))
            {
                var cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
                if (cliente != null)
                    return View(cliente);
            }

            return RedirectToAction("Iniciar", "Home"); // Por si no hay sesión
        }
        */

        [HttpPost]
        public IActionResult EliminarSesion()
        {
            HttpContext.Session.Clear(); // Elimina toda la sesión, incluyendo el cliente y pedidos
            return RedirectToAction("Index", "Home"); // Redirige a Inicio u otra vista
        }


        [HttpPost]
        public IActionResult CerrarSesion()
        {
            HttpContext.Session.Remove("Cliente"); // Solo borra el usuario
            return RedirectToAction("Index", "Home"); // Redirige al inicio o login
        }



        //Iniciar Sesión

        public IActionResult Iniciar()
        {
            return View(); // Vista para mostrar el formulario
        }

        [HttpPost]
        public IActionResult IniciarSesion(string usuario, string contrasena, [FromServices] IClienteRepositorio repo)
        {
            var cliente = repo.ObtenerClientePorCredenciales(usuario, contrasena);

            if (cliente != null)
            {
                HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(cliente));
                return RedirectToAction("UsuarioPerfil", "Cliente");
            }

            ViewBag.Error = "Usuario o contraseña incorrectos.";
            return View("Iniciar","Cliente");
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
