using System.Security.Authentication;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System;

namespace routing
{
    public class RouteAuthorizationFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            JsonResult jsonResult;
            base.OnActionExecuting(context);
            // if(context.HttpContext.User.Identity.IsAuthenticated)
            var isAuthenticated = true;
            if(isAuthenticated)
            {
                var _db = context.HttpContext.RequestServices.GetService(typeof(DbConetxt)) as DbConetxt;
                var actionName = context.RouteData.Values["action"] as string;
                
                var usuarioAssociadoAFuncionalidade =( 
                    from rf in _db.RotasFuncionalidades
                    join f in _db.Funcionalidades on rf.IdFuncionalidade equals f.Id
                    join urf in _db.UsuariosRotasFuncionalidades on rf.Id equals urf.IdRotaFuncionalidade into joinedUrf
                    from jurf in joinedUrf.DefaultIfEmpty() 
                    where 
                    f.ActionName == actionName
                    && jurf != null
                    && jurf.IdUsuario == 1
                    select f
                ).FirstOrDefault();

                var autorizado = usuarioAssociadoAFuncionalidade != null;
                if(!autorizado)
                {
                    // jsonResult = new JsonResult(new { mensagem = "Usuário não tem acesso a funcionalidade" });
                    jsonResult = new JsonResult(new UnauthorizedAccessException("Usuário não tem acesso a funcionalidade"));
                    context.Result = jsonResult;
                }
            }
            else
            {
                jsonResult = new JsonResult(new AuthenticationException("Usuário não autenticado"));
                // jsonResult = new JsonResult(new { mensagem = "Usuário não autenticado" });
                context.Result = jsonResult;
            }
        }
    }
}