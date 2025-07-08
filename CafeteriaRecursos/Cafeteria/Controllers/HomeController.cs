using System.Diagnostics;
using System.Text.Json;
using Cafeteria.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cafeteria.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;//logger es util para saber que sucede en el sistema 
        //Se usa una interfaz para el principio de inversión de dependencias
        private readonly ICafeRepositorio cafeRepositorio;//colocamos el caferepositorio como variable de la clase

        // colocamos la clase cafe repositorio en el constructor     
        public HomeController(ILogger<HomeController> logger, ICafeRepositorio cafeRepositorio)
        {
            _logger = logger;
            this.cafeRepositorio = cafeRepositorio;//
        }

        public IActionResult Index()
        {
            //PRUEBA
            Cliente? cliente = null;
            string? clienteJson = HttpContext.Session.GetString("Cliente");
            if (!string.IsNullOrEmpty(clienteJson))
            {
                cliente = JsonSerializer.Deserialize<Cliente>(clienteJson);
            }//PRUEBA

            //principio de responsabilidad unica e inyección de dependencias

            var cafes = cafeRepositorio.ObtenerCafe().Take(3).ToList();
            var modelo = new HomeIndexViewModel() { MisCafes = cafes, MisClientes = cliente };
            return View(modelo);
        }

       


        //public IActionResult Iniciar()
       // {
        //    return View();
        //}

        public IActionResult Pedidos()
        {
            return View();
        }
        public IActionResult Productos()
        {
            var cafes = cafeRepositorio.ObtenerCafe();
            
            return View(cafes);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
