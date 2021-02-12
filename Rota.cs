using System.Collections.Generic;

namespace routing
{
    public class Rota
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Componente { get; set; }
        
        //reverse navigation
        public List<RotaFuncionalidade> RotasFuncionalidades { get; set; }
    }
}