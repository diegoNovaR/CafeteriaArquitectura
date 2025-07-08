namespace Cafeteria.Models
{
    public interface ICafeRepositorio
    {
        List<Cafe> ObtenerCafe();
    }
    public class CafeRepositorio: ICafeRepositorio
    {
         public List<Cafe> ObtenerCafe()
        {
            return new List<Cafe>() { new Cafe{

                Nombre = "Cafesito sin cafe",
                Descripcion = "este cafe no tiene cafe pero su nombre dice que si, perfecto para incomprendidos",
                Precio = 4.4f
          
            }, new Cafe{

                Nombre = "Cafe Americano",
                Descripcion = "este cafe Tiene mucho cafe y viene de america",
                Precio = 7.4f

            }, new Cafe{

                Nombre = "Cafe con Te",
                Descripcion = "este cafe tiene tesito combinado con cafe, para los gustos exóticos",
                Precio = 8.4f

            }, new Cafe{

                Nombre = "Cafe con pan",
                Descripcion = "este cafe solo es café, pero con pan",
                Precio = 8.4f

            }, new Cafe{

                Nombre = "Cafe solo cafe",
                Descripcion = "este cafe solo es café, sin nada especial.",
                Precio = 5.4f

            }
            };
        }
    }
}
