﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Proyecto.ML.Models;

    

namespace ProyectoPA_G5.Controllers
{
    //public class CuentaController: Controller
    //{

    //    private readonly UserManager<IdentityUser> _userManager;
    //    private readonly SignInManager<IdentityUser> _signInManager;

    //    public CuentaController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    //    {
    //        _userManager = userManager;
    //        _signInManager = signInManager;
    //    }

    //    public IActionResult Login()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Login(Login model)
    //    {
    //        if (ModelState.IsValid)
    //        {
    //            var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
    //            if (result.Succeeded)
    //            {
    //                return RedirectToAction("Index", "Home");
    //            }
    //            ModelState.AddModelError("", "Login invalido");
    //        }
    //        return View(model);
    //    }


    //    public IActionResult Register()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Register(Register model)
    //    {
    //        if (!ModelState.IsValid) return View(model);

    //        var user = new IdentityUser { UserName = model.Email, Email = model.Email };
    //        var result = await _userManager.CreateAsync(user, model.Password);

    //        if (result.Succeeded)
    //        {
    //            await _signInManager.SignInAsync(user, isPersistent: false);
    //            return RedirectToAction("Index", "Home");
    //        }

    //        foreach (var error in result.Errors)
    //        {
    //            ModelState.AddModelError("", error.Description);
    //        }

    //        return View(model);
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Logout()
    //    {
    //        await _signInManager.SignOutAsync();
    //        return RedirectToAction("Login", "Account");
    //    }


    //}
}
