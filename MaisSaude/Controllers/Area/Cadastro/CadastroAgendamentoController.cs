using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MaisSaude.Controllers.Area.Cadastro
{
    public class CadastroAgendamentoController : Controller
    {
        // GET: CadastroAgendamentoController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CadastroAgendamentoController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CadastroAgendamentoController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CadastroAgendamentoController/Create
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

        // GET: CadastroAgendamentoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CadastroAgendamentoController/Edit/5
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

        // GET: CadastroAgendamentoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CadastroAgendamentoController/Delete/5
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
