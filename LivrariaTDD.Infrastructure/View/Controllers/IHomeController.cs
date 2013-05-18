using System.Web.Mvc;

namespace LivrariaTDD.Infrastructure.View.Controllers
{
    public interface IHomeController : IController
    {
        RedirectToRouteResult Index();
    }
}