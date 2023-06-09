﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using personapi_dotnet.Models.Entities;

public class EstudiosController : Controller
{
    private readonly PersonaDbContext _context;

    public EstudiosController(PersonaDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Estudio>), 200)]
    public async Task<IActionResult> Index()
    {
        Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Estudio, Profesion> personaDbContext = _context.Estudios.Include(e => e.CcPerNavigation).Include(e => e.IdProfNavigation);
        return Ok(await _context.Estudios.ToListAsync());
    }


    // GET: Estudios/Details/5
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Estudio), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Estudios == null)
        {
            return NotFound();
        }

        var estudio = await _context.Estudios
            .Include(e => e.CcPerNavigation)
            .Include(e => e.IdProfNavigation)
            .FirstOrDefaultAsync(m => m.IdProf == id);
        if (estudio == null)
        {
            return NotFound();
        }

        return View(estudio);
    }

    // GET: Estudios/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc");
        ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id");
        return View();
    }

    // POST: Estudios/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
    {
        if (ModelState.IsValid)
        {
            _context.Add(estudio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
        ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
        return View(estudio);
    }

    // GET: Estudios/Edit/5
    [HttpGet("Edit/{id}")]
    [ProducesResponseType(typeof(Estudio), 200)]
    [ProducesResponseType(typeof(void), 404)]
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Estudios == null)
        {
            return NotFound();
        }

        var estudio = await _context.Estudios.FindAsync(id);
        if (estudio == null)
        {
            return NotFound();
        }
        ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
        ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
        return View(estudio);
    }

    // POST: Estudios/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPut("{id}")]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Edit(int id, [Bind("IdProf,CcPer,Fecha,Univer")] Estudio estudio)
    {
        if (id != estudio.IdProf)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(estudio);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstudioExists(estudio.IdProf))
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
        ViewData["CcPer"] = new SelectList(_context.Personas, "Cc", "Cc", estudio.CcPer);
        ViewData["IdProf"] = new SelectList(_context.Profesions, "Id", "Id", estudio.IdProf);
        return View(estudio);
    }

    // GET: Estudios/Delete/5
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Estudios == null)
        {
            return NotFound();
        }

        var estudio = await _context.Estudios
            .Include(e => e.CcPerNavigation)
            .Include(e => e.IdProfNavigation)
            .FirstOrDefaultAsync(m => m.IdProf == id);
        if (estudio == null)
        {
            return NotFound();
        }

        return View(estudio);
    }

    // POST: Estudios/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Estudios == null)
        {
            return Problem("Entity set 'PersonaDbContext.Estudios'  is null.");
        }
        var estudio = await _context.Estudios.FindAsync(id);
        if (estudio != null)
        {
            _context.Estudios.Remove(estudio);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool EstudioExists(int id)
    {
        return (_context.Estudios?.Any(e => e.IdProf == id)).GetValueOrDefault();
    }
}

