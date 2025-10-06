namespace CRUD_CORE.Models

{
    public class Usuario
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string usuario { get; set; }
        public string clave { get; set; }
        public string correo { get; set; }
        public int id_rol { get; set; }
        public string url_foto_perfil { get; set; }

        public string confirmar_clave { get; set; }

    }
}
