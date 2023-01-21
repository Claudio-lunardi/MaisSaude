using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Cadastro
{
    public class MedicoController : Controller
    {
        // GET: CadastroMedicoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CadastroMedicoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroMedicoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroMedicoController/Create
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

        // GET: CadastroMedicoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroMedicoController/Edit/5
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

        // GET: CadastroMedicoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroMedicoController/Delete/5
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
