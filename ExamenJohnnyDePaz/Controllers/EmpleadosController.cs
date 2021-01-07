using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamenJohnnyDePaz.Models;

namespace ExamenJohnnyDePaz.web.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly ExamenContext _context;

        public EmpleadosController(ExamenContext context)
        {
            _context = context;
        }

        // GET: Empleados
        public async Task<IActionResult> Index()
        {
            var examenContext = _context.Empleado.Include(e => e.IdAreaNavigation).Include(e => e.IdJefeNavigation);
            return View(await examenContext.ToListAsync());
        }

        [HttpPost]
        public IActionResult json()
        {
            var examenContext = _context.Empleado.Select(
                x=>new 
                { 
                    x.IdEmpleado,
                    x.NombreCompleto,
                    x.Cedula,x.Correo,
                    x.FechaNacimiento,
                    x.FechaIngreso,
                    jefe=x.IdJefeNavigation.NombreCompleto,
                    area=x.IdAreaNavigation.Nombre,
                    x.Foto
                });
            //var examenContext = _context.Empleado.Include(e => e.IdAreaNavigation).Include(e => e.IdJefeNavigation); 
            return Json(examenContext.ToList());
        }

        // GET: Empleados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdJefeNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // GET: Empleados/Create
        public IActionResult Create()
        {
            ViewData["IdArea"] = new SelectList(_context.Area, "IdArea", "Nombre");
            ViewData["IdJefe"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto");
            return View();
        }

        // POST: Empleados/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdEmpleado,NombreCompleto,Cedula,Correo,FechaNacimiento,FechaIngreso,IdJefe,IdArea,Foto")] Empleado empleado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(empleado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdArea"] = new SelectList(_context.Area, "IdArea", "Nombre", empleado.IdArea);
            ViewData["IdJefe"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto", empleado.IdJefe);
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado.FindAsync(id);
            if (empleado == null)
            {
                return NotFound();
            }
            ViewData["IdArea"] = new SelectList(_context.Area, "IdArea", "Nombre", empleado.IdArea);
            ViewData["IdJefe"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto", empleado.IdJefe);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdEmpleado,NombreCompleto,Cedula,Correo,FechaNacimiento,FechaIngreso,IdJefe,IdArea,Foto")] Empleado empleado)
        {
            if (id != empleado.IdEmpleado)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(empleado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmpleadoExists(empleado.IdEmpleado))
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
            ViewData["IdArea"] = new SelectList(_context.Area, "IdArea", "Nombre", empleado.IdArea);
            ViewData["IdJefe"] = new SelectList(_context.Empleado, "IdEmpleado", "NombreCompleto", empleado.IdJefe);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var empleado = await _context.Empleado
                .Include(e => e.IdAreaNavigation)
                .Include(e => e.IdJefeNavigation)
                .FirstOrDefaultAsync(m => m.IdEmpleado == id);
            if (empleado == null)
            {
                return NotFound();
            }

            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var empleado = await _context.Empleado.FindAsync(id);
            _context.Empleado.Remove(empleado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmpleadoExists(int id)
        {
            return _context.Empleado.Any(e => e.IdEmpleado == id);
        }
    }
}
