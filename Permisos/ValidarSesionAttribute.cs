using Microsoft.AspNetCore.Mvc.Filters;

namespace CRUD_CORE.Permisos
{
    public class ValidarSesionAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var sesion = filterContext.HttpContext.Session.GetString("usuario");
            if (string.IsNullOrEmpty(sesion))
            {
                filterContext.HttpContext.Response.Redirect("/Acceso/Login");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
