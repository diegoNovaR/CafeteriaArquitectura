using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Cafeteria.Models;
using System.Diagnostics;
using System.Text.Json;

namespace Cafeteria.Controllers
{
    public class PedidoController : Controller
    {
        private readonly ICafeRepositorio ICafeRepositorio;
        private readonly IClienteRepositorio IClienteRepositorio; //ultimo
        public PedidoController(ICafeRepositorio ICafeRepositorio, IClienteRepositorio iClienteRepositorio)
        {
            this.ICafeRepositorio = ICafeRepositorio;
            IClienteRepositorio = iClienteRepositorio;
        }

        public IActionResult Pedidos()
        {
            var cafes = ICafeRepositorio.ObtenerCafe();
            return View(cafes);
        }
        
        [HttpPost]
        public IActionResult AgregarPedido(string nombreCafe)
        {
            var cafes = ICafeRepositorio.ObtenerCafe();
            var cafeSeleccionado = cafes.FirstOrDefault(c => c.Nombre == nombreCafe);

            if (cafeSeleccionado != null)
            {
                var clienteJson = HttpContext.Session.GetString("Cliente");
                if (!string.IsNullOrEmpty(clienteJson))//comprobamos si existe cliente
                {
                    var cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);//llamamos al cliente que se encuentra guardado en json
                                                                                   // deserializamos el json a un objeto
                    cliente?.Pedidos.Add(cafeSeleccionado);// agregamos el pedido al cliente
                    HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(cliente));// serializamos el cliente de nuevo a json y lo guardamos en la sesión
                }
            }
            return RedirectToAction("Pedidos");
        }


        /*
        [HttpPost]
        public IActionResult AgregarPedido(string nombreCafe)//ultimo
        {
            var cafes = ICafeRepositorio.ObtenerCafe();
            var cafeSeleccionado = cafes.FirstOrDefault(c => c.Nombre == nombreCafe);

            if (cafeSeleccionado != null)
            {
                var clienteJson = HttpContext.Session.GetString("Cliente");

                if (!string.IsNullOrEmpty(clienteJson))
                {
                    var clienteEnSesion = JsonSerializer.Deserialize<Cliente>(clienteJson);

                    if (clienteEnSesion != null)
                    {
                        // Buscar al cliente real en el repositorio usando el usuario
                        var clienteRegistrado = IClienteRepositorio.BuscarPorUsuario(clienteEnSesion.Usuario);

                        if (clienteRegistrado != null)
                        {
                            clienteRegistrado.Pedidos.Add(cafeSeleccionado);

                            // Actualizar la sesión con el cliente actualizado
                            HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(clienteRegistrado));
                        }
                    }
                }
            }

            return RedirectToAction("Pedidos");
        }*/

        [HttpPost]
        public IActionResult EliminarPedido(int index)
        {
            string? clienteJson = HttpContext.Session.GetString("Cliente");
            if (clienteJson != null)
            {
                var cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
                if (cliente != null && index >= 0 && index < cliente.Pedidos.Count)//tiene que tener pedidos y el indice en la lista que se quiere eliminar debe existir
                {
                    cliente.Pedidos.RemoveAt(index);//eliminar pedido
                    HttpContext.Session.SetString("Cliente", JsonSerializer.Serialize(cliente));//guardamos el cliente en sesión y lo transformamos
                                                                                                //a json
                }
            }

            return RedirectToAction("VerPedidos");
        }


        public IActionResult VerPedidos()
        {
            string? clienteJson = HttpContext.Session.GetString("Cliente");
            if (!string.IsNullOrEmpty(clienteJson))
            {
                var cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
                if (cliente != null)
                    return View(cliente);
            }

            return RedirectToAction("Crear_Usuario", "Cliente"); // Por si no hay sesión
        }

    }
}
