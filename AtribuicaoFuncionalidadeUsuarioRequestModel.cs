using System.Collections.Generic;

namespace routing
{
    public class AtribuicaoFuncionalidadeUsuarioRequestModel
    {
        public int IdUsuario { get; set; }
        public List<int> IdsFuncionalidades { get; set; }
    }
}