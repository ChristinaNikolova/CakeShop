namespace CakeShop.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : BaseController
    {
        public async Task<IActionResult> GetAll()
        {
            return this.View();
        }
    }
}
