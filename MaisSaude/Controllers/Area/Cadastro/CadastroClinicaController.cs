using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Cadastro
{
    public class CadastroClinicaController : Controller
    {
        // GET: CadastroClinicaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CadastroClinicaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroClinicaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroClinicaController/Create
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

        // GET: CadastroClinicaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroClinicaController/Edit/5
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

        // GET: CadastroClinicaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroClinicaController/Delete/5
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
