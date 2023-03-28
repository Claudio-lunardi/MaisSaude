using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Home
{
    public class HomeSiteController : Controller
    {
        // GET: HomeSiteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: HomeSiteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: HomeSiteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HomeSiteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeSiteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: HomeSiteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeSiteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: HomeSiteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
