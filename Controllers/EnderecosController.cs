using Microsoft.AspNetCore.Mvc;
using CaseAeC.Data;
using CaseAeC.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CaseAeC.Controllers;

public class EnderecosController : Controller
{
    private readonly AppDbContext _context;

    public EnderecosController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null) return RedirectToAction("Login", "Usuarios");

        var lista = await _context.Enderecos
            .Where(e => e.UsuarioId == userId.Value)
            .ToListAsync();
            
        return View(lista ?? new List<Endereco>());
    }

    public IActionResult Create()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null) return RedirectToAction("Login", "Usuarios");
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Endereco endereco)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null) return RedirectToAction("Login", "Usuarios");

        endereco.UsuarioId = userId.Value;

        _context.Add(endereco);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int? id)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null || id == null) return RedirectToAction("Login", "Usuarios");

        var endereco = await _context.Enderecos
            .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == userId.Value);

        if (endereco == null) return NotFound();

        return View(endereco);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Endereco endereco)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null || id != endereco.Id) return RedirectToAction("Login", "Usuarios");

        try
        {
            endereco.UsuarioId = userId.Value;
            
            _context.Update(endereco);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Enderecos.Any(e => e.Id == endereco.Id)) return NotFound();
            else throw;
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null) return RedirectToAction("Login", "Usuarios");

        var endereco = await _context.Enderecos
            .FirstOrDefaultAsync(e => e.Id == id && e.UsuarioId == userId.Value);

        if (endereco != null)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }
        
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> ExportarCsv()
    {
        var userId = HttpContext.Session.GetInt32("UsuarioLogadoId");
        if (userId == null) return RedirectToAction("Login", "Usuarios");

        var enderecos = await _context.Enderecos
            .Where(e => e.UsuarioId == userId.Value)
            .ToListAsync();

        var csv = new StringBuilder();
        csv.AppendLine("CEP;Logradouro;Numero;Complemento;Bairro;Cidade;UF");

        foreach (var e in enderecos)
        {
            csv.AppendLine($"{e.Cep ?? ""};{e.Logradouro ?? ""};{e.Numero ?? ""};{e.Complemento ?? ""};{e.Bairro ?? ""};{e.Cidade ?? ""};{e.Uf ?? ""}");
        }

        var bytes = Encoding.UTF8.GetBytes(csv.ToString());
        return File(bytes, "text/csv", "enderecos_exportados.csv");
    }
}