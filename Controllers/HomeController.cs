using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using EduProject.Models;
using Microsoft.AspNetCore.Authorization;

namespace EduProject.Controllers;

public class HomeController : Controller
{
    private readonly DbContext _context;

    public HomeController(DbContext context)
    {
        _context = context;
    }
    
    public IActionResult Index()
    {
        List<Ilan> anailn = _context.Ilan
            .OrderBy(b => -b.IlanId)
            .ToList();
        if (anailn.Count()<3)
        {
            anailn = anailn.GetRange(0,anailn.Count());
        }
        ViewBag._ilanlar = anailn;
        List<ForumBaslik> forumlar = _context.ForumBaslik
            .OrderBy(b => -b.ForumBaslikId)
            .ToList();
        if (forumlar.Count()<3)
        {
            forumlar = forumlar.GetRange(0,forumlar.Count());
        }
        ViewBag.forumlar = forumlar;
        List<Duyuru> duyuruana = _context.Duyuru.ToList();
        ViewBag.duyurular = duyuruana;
        return View();
    }

    public IActionResult Search(string searchkey)
    {
        
        List<Ilan> sonucilan = _context.Ilan
            .Where(p=>p.Message.ToLower().Contains(searchkey.ToLower()))
            .ToList();
            sonucilan = sonucilan.Count()<=10?sonucilan:sonucilan.GetRange(0,sonucilan.Count());
        List<ForumBaslik> sonucforum = _context.ForumBaslik
            .Where(p=>p.Name.ToLower().Contains(searchkey.ToLower()) || p.Creator.ToLower().Contains(searchkey.ToLower()))
            .ToList();
            sonucforum = sonucforum.Count()<=10?sonucforum:sonucforum.GetRange(0,sonucforum.Count());
        List<Sınav> sonucsinav = _context.Sınav
            .Where(p=>p.Name.ToLower().Contains(searchkey.ToLower()) || p.DersAdı.ToLower().Contains(searchkey.ToLower()))
            .ToList();
            sonucsinav = sonucsinav.Count()<=10?sonucsinav:sonucsinav.GetRange(0,sonucsinav.Count());

        List<DersNotu> sonucdersnotu = _context.DersNotu
            .Where(p=>p.DersAdı.ToLower().Contains(searchkey.ToLower()) || p.Konu.ToLower().Contains(searchkey.ToLower()))
            .ToList();
            sonucdersnotu = sonucdersnotu.Count()<=10?sonucdersnotu:sonucdersnotu.GetRange(0,sonucdersnotu.Count());
        
        ViewBag.SonucForum = sonucforum;
        ViewBag.SonucSinav = sonucsinav;
        ViewBag.SonucIlan = sonucilan;
        ViewBag.SonucDers = sonucdersnotu;
        return View();
    }
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
