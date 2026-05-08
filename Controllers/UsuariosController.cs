using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaseAeC.Data;
using CaseAeC.Models;

namespace CaseAeC.Controllers;

public class UsuariosController : Controller
{
    private readonly AppDbContext _context;

    public UsuariosController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        var usuario = await _context.Usuarios
            .FirstOrDefaultAsync(u => u.Login == model.Login && u.Senha == model.Senha);

        if (usuario != null)
        {
            HttpContext.Session.SetInt32("UsuarioLogadoId", usuario.Id);
            HttpContext.Session.SetString("UsuarioLogadoNome", usuario.Nome ?? "");
            
            return RedirectToAction("Index", "Enderecos"); 
        }

        ViewBag.Erro = "Usuário ou senha inválidos!";
        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Usuario usuario)
    {
        if (ModelState.IsValid)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction("Login");
        }
        return View(usuario);
    }
}