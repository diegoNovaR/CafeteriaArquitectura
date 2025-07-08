namespace Cafeteria.Models
{
    public class Cliente
    {
        public string? Nombre { get; set; }
        public string? Apellido { get; set; }
        public string? Usuario { get; set; }
        public string? Contrasena { get; set; }

        public List<Cafe> Pedidos { get; set; } = new List<Cafe>();
        public float Total => Pedidos.Sum(c => c.Precio);

        //public Cliente ObtenerCliente()//prueba
       // {
        //    return new Cliente();
        //}
    }
}
