using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AnalizadorSoftware.Data;
using AnalizadorSoftware.Models;
using AnalizadorSoftware.Services;

namespace AnalizadorSoftware.Pages;

public class IndexModel : PageModel
{
    private readonly AppDbContext _context;
    private readonly AnalizadorService _analizador;

    public IndexModel(AppDbContext context, AnalizadorService analizador)
    {
        _context = context;
        _analizador = analizador;
    }

    [BindProperty]
    public string Especificacion { get; set; } = string.Empty;

    public List<Proceso> Procesos { get; set; } = new();
    public string? Mensaje { get; set; }
    public bool MostrarResultados { get; set; }

    public async Task OnGetAsync()
    {
        Procesos = await _context.Procesos
            .Include(p => p.Subprocesos)
            .ThenInclude(s => s.CasosUso)
            .OrderByDescending(p => p.IdProceso)
            .Take(10)
            .ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (string.IsNullOrWhiteSpace(Especificacion))
        {
            Mensaje = "Por favor ingresa una especificación.";
            return Page();
        }

        try
        {
            // Analizar con IA
            var procesosAnalizados = await _analizador.AnalizarEspecificacion(Especificacion);

            // Guardar en base de datos
            _context.Procesos.AddRange(procesosAnalizados);
            await _context.SaveChangesAsync();

            Mensaje = $"✓ Análisis completado. Se crearon {procesosAnalizados.Count} proceso(s).";
            MostrarResultados = true;
            Procesos = procesosAnalizados;
            Especificacion = string.Empty; // Limpiar el campo
        }
        catch (Exception ex)
        {
            Mensaje = $"Error: {ex.Message}";
            await OnGetAsync(); // Cargar procesos existentes
        }

        return Page();
    }
}
