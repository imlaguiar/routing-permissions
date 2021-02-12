using System.Collections.Generic;

namespace routing
{
    public class Funcionalidade
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string ActionName { get; set; }

        //reverse navigation
        public List<RotaFuncionalidade> RotasFuncionalidades { get; set; }
    }
}