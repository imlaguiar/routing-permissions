namespace routing
{
    public class UsuarioRotaFuncionalidade
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
        public int IdRotaFuncionalidade { get; set; }
        public RotaFuncionalidade RotaFuncionalidade { get; set; }
    }
}