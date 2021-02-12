using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace routing.Controllers
{
    [ApiController]
    [Route("api/route")]
    public class RouteController : ControllerBase
    {
        private readonly ILogger<RouteController> _logger;
        private readonly DbConetxt _db;

        public RouteController(ILogger<RouteController> logger, DbConetxt db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create()
        {
            var rotaDashboard = new Rota { Descricao = "dashboard", Componente = "dashboard" };
            var rotaCampanhas = new Rota { Descricao = "campanhas", Componente = "campanhas" }; 
            var rotaCadastros = new Rota { Descricao = "cadastros", Componente = "cadastros" };

            var rotas = new List<Rota> {
                rotaDashboard,
                rotaCampanhas,
                rotaCadastros
            };
            await _db.Rotas.AddRangeAsync(rotas);

            //funcionalidades rota dashboard
            var graficoDoacoes = new Funcionalidade { Descricao = "DoacoesDashboard", ActionName = "TesteTemAcesso" };
            var graficoCadastros = new Funcionalidade { Descricao = "CadastrosDashboard" };

            //funcionalidades rota campanhas
            var criarCampanha = new Funcionalidade { Descricao = "CriacaoCampanha" };
            var listarCampanhas = new Funcionalidade { Descricao = "ListarCampanhas" };

            //funcionalidades rota cadastros
            var modificarCadastro = new Funcionalidade { Descricao = "ModificarCadastro", ActionName = "TesteNaoTemAcesso" };
            var apagarCadastro = new Funcionalidade { Descricao = "ApagarCadastro" };

            await _db.Funcionalidades.AddRangeAsync(new List<Funcionalidade>{
                graficoDoacoes,
                graficoCadastros,
                criarCampanha,
                listarCampanhas,
                modificarCadastro,
                apagarCadastro
            });

            var rotasFuncionalidades = new List<RotaFuncionalidade>{
                new RotaFuncionalidade {
                    Rota = rotaDashboard,
                    Funcionalidade = graficoDoacoes
                },
                new RotaFuncionalidade {
                    Rota = rotaDashboard,
                    Funcionalidade = graficoCadastros
                },
                new RotaFuncionalidade {
                    Rota = rotaCampanhas,
                    Funcionalidade = criarCampanha
                },
                new RotaFuncionalidade {
                    Rota = rotaCampanhas,
                    Funcionalidade = listarCampanhas
                },
                new RotaFuncionalidade {
                    Rota = rotaCadastros,
                    Funcionalidade = modificarCadastro
                },
                new RotaFuncionalidade {
                    Rota = rotaCadastros,
                    Funcionalidade = apagarCadastro
                }
            };
            await _db.RotasFuncionalidades.AddRangeAsync(rotasFuncionalidades);

            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("user")]
        public async Task<ActionResult> CreateUser()
        {
            var user = new Usuario { Nome = "Usuario de drogas pesadas (java)" };
            await _db.Usuarios.AddAsync(user);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("functionalities")]
        public async Task<List<RotasFuncionalidadesViewModel>> Functionalites()
        {
            var resultado = new List<RotasFuncionalidadesViewModel>();
            var rotas = _db.Rotas.ToList();
            foreach (var rota in rotas)
            {
                resultado.Add(new RotasFuncionalidadesViewModel
                {
                    IdRota = rota.Id,
                    Funcionalidades = (
                        from rf in _db.RotasFuncionalidades
                        join f in _db.Funcionalidades on rf.IdFuncionalidade equals f.Id
                        where rf.IdRota == rota.Id
                        select new FuncionalidadeViewModel { Idfuncionalidade = f.Id, NomeFuncionalidade = f.Descricao }
                    ).ToList()
                });
            }
            return resultado;
        }

        [HttpGet("functionalities/user")]
        public async Task<List<RotasFuncionalidadesViewModel>> FunctionalitesUser(/*int IdUsuario*/)
        {
            var resultado = new List<RotasFuncionalidadesViewModel>();
            var rotas = _db.Rotas.ToList();
            foreach (var rota in rotas)
            {
                resultado.Add(new RotasFuncionalidadesViewModel
                {
                    IdRota = rota.Id,
                    Funcionalidades = (
                        from rf in _db.RotasFuncionalidades
                        join f in _db.Funcionalidades on rf.IdFuncionalidade equals f.Id
                        join urf in _db.UsuariosRotasFuncionalidades on rf.Id equals urf.IdRotaFuncionalidade into joinedUrf
                        from jurf in joinedUrf.DefaultIfEmpty() 
                        where rf.IdRota == rota.Id
                        select new FuncionalidadeViewModel { Idfuncionalidade = f.Id, NomeFuncionalidade = f.Descricao, Assigned = jurf != null }
                    ).ToList()
                });
            }
            return resultado;
        }

        [HttpGet("functionalities/allowed")]
        public async Task<List<RotasFuncionalidadesViewModel>> AllowedFunctionalities(/*int IdUsuario*/)
        {
            var resultado = new List<RotasFuncionalidadesViewModel>();
            var rotas = _db.Rotas.ToList();
            foreach (var rota in rotas)
            {
                resultado.Add(new RotasFuncionalidadesViewModel
                {
                    IdRota = rota.Id,
                    Funcionalidades = (
                        from rf in _db.RotasFuncionalidades
                        join f in _db.Funcionalidades on rf.IdFuncionalidade equals f.Id
                        join urf in _db.UsuariosRotasFuncionalidades on rf.Id equals urf.IdRotaFuncionalidade into joinedUrf
                        from jurf in joinedUrf.DefaultIfEmpty() 
                        where rf.IdRota == rota.Id
                        && jurf != null
                        && jurf.IdUsuario == 1
                        select new FuncionalidadeViewModel { Idfuncionalidade = f.Id, NomeFuncionalidade = f.Descricao, Assigned = (jurf != null) }
                    ).ToList()
                });
            }
            return resultado;
        }

        [HttpPost("assign")]
        public async Task<ActionResult> Assign(/*AtribuicaoFuncionalidadeUsuarioRequestModel requestModel*/)
        {
            var requestModel = new AtribuicaoFuncionalidadeUsuarioRequestModel {
                IdUsuario = 1,
                IdsFuncionalidades = new List<int> {
                    1, 4, 6
                }
            };

            foreach (var funcionalidade in requestModel.IdsFuncionalidades)
            {
                _db.UsuariosRotasFuncionalidades.Add(new UsuarioRotaFuncionalidade{
                    IdUsuario = requestModel.IdUsuario,
                    IdRotaFuncionalidade = _db.RotasFuncionalidades.Where(rf => rf.IdFuncionalidade == funcionalidade).Select(rf => rf.Id).FirstOrDefault()
                });
            }
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("TesteTemAcesso")]
        public async Task<string> TesteTemAcesso()
        {
            return "tem acesso";
        }

        [HttpGet("TesteNaoTemAcesso")]
        public async Task<string> TesteNaoTemAcesso()
        {
            return "Não tem acesso";
        }
    }
}
