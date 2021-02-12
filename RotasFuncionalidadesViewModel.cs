using System.Collections.Generic;

namespace routing
{
    public class RotasFuncionalidadesViewModel
    {
        public int IdRota { get; set; }
        public string NomeRota { get; set; }
        public List<FuncionalidadeViewModel> Funcionalidades { get; set; }
    }
}