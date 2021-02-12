using System.Collections.Generic;

namespace routing
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }

        //reverse navigation
        public List<UsuarioRotaFuncionalidade> UsuarioRotaFuncionalidades { get; set; }
    }
}