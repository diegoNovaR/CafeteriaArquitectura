// IClienteRepositorio.cs
namespace Cafeteria.Models
{
    public interface IClienteRepositorio
    {
        Cliente? ObtenerClientePorCredenciales(string usuario, string contrasena);
        void AgregarCliente(Cliente nuevoCliente);
        bool ExisteUsuario(string usuario);
        //Cliente BuscarPorUsuario(string usuario);

    }
    public class ClienteRepositorio : IClienteRepositorio
    {

        //private static List<Cliente> _clientes = new List<Cliente>();//ultimo

        /*public Cliente BuscarPorUsuario(string usuario)//ultimo
        {
            return _clientes.FirstOrDefault(c => c.Usuario == usuario);
        }*/


        private static readonly List<Cliente> Clientes = new List<Cliente>
        {
        new Cliente { Nombre = "Ana", Apellido = "Ramirez", Usuario = "ana123", Contrasena = "clave123" },
        new Cliente { Nombre = "Luis", Apellido = "Fernandez", Usuario = "luis456", Contrasena = "pass456" }
        };

        public Cliente? ObtenerClientePorCredenciales(string usuario, string contrasena)
        {
            return Clientes.FirstOrDefault(c =>
                c.Usuario == usuario && c.Contrasena == contrasena);
        }

        public void AgregarCliente(Cliente nuevoCliente)
        {
            Clientes.Add(nuevoCliente);
        }

        public bool ExisteUsuario(string usuario)
        {
            return Clientes.Any(c => c.Usuario == usuario);
        }
    }
}