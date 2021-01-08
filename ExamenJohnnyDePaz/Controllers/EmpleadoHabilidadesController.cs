using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenJohnnyDePaz.Models;

namespace ExamenJohnnyDePaz.Controllers
{
    public class EmpleadoHabilidadesController : Controller
    {
        private readonly ExamenContext _context;

        public EmpleadoHabilidadesController(ExamenContext context)
        {
            _context = context;
        }

        // GET: EmpleadoHabilidades
        public async Task<IActionResult> Index()
        {
            var examenContext = _context.EmpleadoHabilidad.Include(e => e.IdEmpleadoNavigation);
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto");
            return View(await examenContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult json()
        {
            var examenContext = _context.EmpleadoHabilidad.Select(
                x => new 
                {
                    x.IdHabilidad,
                    x.NombreHabilidad,
                    empleado=x.IdEmpleadoNavigation.NombreCompleto
                }
                );
            return Json(examenContext.ToList());
        }


        public IActionResult HabilidadesEmpleado(int? id)
        {
            ViewBag.ID = id;
            return View();
        }


        [HttpPost]
        public IActionResult habXEmpleado(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var empleadoHabilidad = (from s in _context.EmpleadoHabilidad
                                           where s.IdEmpleado == id
                                           select s).Include(e=>e.IdEmpleadoNavigation).ToList();
                
            if (empleadoHabilidad == null)
            {
                return NotFound();
            }
            return Json(empleadoHabilidad);
        }

        // GET: EmpleadoHabilidades/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleadoHabilidad = await _context.EmpleadoHabilidad
                .Include(e => e.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdHabilidad == id);
            if (empleadoHabilidad == null)
            {
                return NotFound();
            }

            return View(empleadoHabilidad);
        }

        // GET: EmpleadoHabilidades/Create
        public IActionResult Create()
        {
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto");
            return View();
        }

        // POST: EmpleadoHabilidades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdHabilidad,IdEmpleado,NombreHabilidad")] EmpleadoHabilidad empleadoHabilidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleadoHabilidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto", empleadoHabilidad.IdEmpleado);
            return View(empleadoHabilidad);
        }

        // GET: EmpleadoHabilidades/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleadoHabilidad = await _context.EmpleadoHabilidad.FindAsync(id);
            if (empleadoHabilidad == null)
            {
                return NotFound();
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto", empleadoHabilidad.IdEmpleado);
            return View(empleadoHabilidad);
        }

        // POST: EmpleadoHabilidades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdHabilidad,IdEmpleado,NombreHabilidad")] EmpleadoHabilidad empleadoHabilidad)
        {
            if (id != empleadoHabilidad.IdHabilidad)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleadoHabilidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoHabilidadExists(empleadoHabilidad.IdHabilidad))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpleado"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto", empleadoHabilidad.IdEmpleado);
            return View(empleadoHabilidad);
        }

        // GET: EmpleadoHabilidades/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleadoHabilidad = await _context.EmpleadoHabilidad
                .Include(e => e.IdEmpleadoNavigation)
                .FirstOrDefaultAsync(m => m.IdHabilidad == id);
            if (empleadoHabilidad == null)
            {
                return NotFound();
            }

            return View(empleadoHabilidad);
        }

        // POST: EmpleadoHabilidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleadoHabilidad = await _context.EmpleadoHabilidad.FindAsync(id);
            _context.EmpleadoHabilidad.Remove(empleadoHabilidad);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoHabilidadExists(int id)
        {
            return _context.EmpleadoHabilidad.Any(e => e.IdHabilidad == id);
        }
    }
}
