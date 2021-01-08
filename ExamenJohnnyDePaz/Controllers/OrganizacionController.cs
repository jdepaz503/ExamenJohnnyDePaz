using ExamenJohnnyDePaz.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamenJohnnyDePaz.Controllers
{
    public class OrganizacionController : Controller
    {
        private readonly ExamenContext _context;

        public OrganizacionController(ExamenContext context)
        {
            _context = context;
        }
        // GET: OrganizacionController
        public ActionResult Index()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            foreach (Empleado type in _context.Empleado)
            {
                nodes.Add(new TreeViewNode { id = type.IdEmpleado.ToString(), parent = "#", text = type.NombreCompleto });
            }

            foreach (EmpleadoHabilidad Subtype in _context.EmpleadoHabilidad)
            {
                nodes.Add(new TreeViewNode { id= Subtype.IdEmpleado.ToString() + "-" + Subtype.IdHabilidad.ToString(), parent=Subtype.IdEmpleado.ToString(), text=Subtype.NombreHabilidad});
            }

            ViewBag.Json = JsonConvert.SerializeObject(nodes);
            return View();
        }
        [HttpPost]
        public ActionResult nodes()
        {
            List<TreeViewNode> nodes = new List<TreeViewNode>();

            foreach (Empleado type in _context.Empleado)
            {
                nodes.Add(new TreeViewNode { id = type.IdEmpleado.ToString(), parent = "#", text = type.NombreCompleto });
            }

            foreach (EmpleadoHabilidad Subtype in _context.EmpleadoHabilidad)
            {
                nodes.Add(new TreeViewNode { id = Subtype.IdEmpleado.ToString() + "-" + Subtype.IdHabilidad.ToString(), parent = Subtype.IdEmpleado.ToString(), text = Subtype.NombreHabilidad });
            }

            return Json(nodes.ToList());
        }
        // GET: OrganizacionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: OrganizacionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrganizacionController/Create
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

        // GET: OrganizacionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: OrganizacionController/Edit/5
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

        // GET: OrganizacionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: OrganizacionController/Delete/5
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
