namespace api.clients.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Telefono { get; set; }
        public int PaisId { get; set; }
        public Pais? Pais { get; set; }
    }
}