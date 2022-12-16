using MaisSaude.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Cadastro
{
    public class CadastroClienteController : Controller
    {
        // GET: CadastroClienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CadastroClienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroClienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroClienteController/Create
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

        // GET: CadastroClienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroClienteController/Edit/5
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

        // GET: CadastroClienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroClienteController/Delete/5
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
