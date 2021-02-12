using System.Collections.Generic;

namespace routing
{
    public class RotaFuncionalidade
    {
        public int Id { get; set; }
        public int IdFuncionalidade { get; set; }
        public Funcionalidade Funcionalidade { get; set; }
        public int IdRota { get; set; }
        public Rota Rota { get; set; }

        //reverse navigation
        public List<UsuarioRotaFuncionalidade> UsuariosRotasFuncionalidades { get; set; }
    }
}