using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers;

public class CategoryController : Controller
{
    private readonly ApplicationDbContext _db;
    
    public CategoryController(ApplicationDbContext db)
    {
        _db = db;
    }
    public IActionResult Index()
    {
        //retrieving records from table db
        List<Category> objCategoryList = _db.Categories.ToList();
        
        return View(objCategoryList);
    }
    
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Category obj)
    {
        // if (obj.Name == obj.DisplayOrder.ToString())
        // {
        //     ModelState.AddModelError("name", "The Display Order cannot exactly match the name");
        // }
        
        // if (obj.Name != null && obj.Name.ToLower() == "test")
        // {
        //     ModelState.AddModelError("", "Test is an invalid value");
        // }

        if (ModelState.IsValid)
        {
            _db.Categories.Add(obj);
            _db.SaveChanges();
            TempData["success"] = "Category created succesfully";
            return RedirectToAction("Index");
        }

        return View();
    }
    
    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromBd = _db.Categories.Find(id);
        if (categoryFromBd == null)
        {
            return NotFound();
        }
        
        return View(categoryFromBd);
    }
    
    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        
        if (ModelState.IsValid)
        {
            _db.Categories.Update(obj);
            _db.SaveChanges();
            TempData["success"] = "Category updated succesfully";
            return RedirectToAction("Index");
        }

        return View();
    }
    
    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }

        Category? categoryFromBd = _db.Categories.Find(id);
        if (categoryFromBd == null)
        {
            return NotFound();
        }
        
        return View(categoryFromBd);
    }
    
    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePost(int? id)
    {
        Category? obj = _db.Categories.Find(id);
        if (obj == null)
        {
            return NotFound();
        }
        
        _db.Categories.Remove(obj);
        _db.SaveChanges();
        TempData["success"] = "Category deleted succesfully";
        return RedirectToAction("Index");
    }
}