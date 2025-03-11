﻿using BookCart.Data;
using BookCart.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BookCart.Controllers
{
    public class UserController : Controller
    {
        readonly BookCartDbContext _ctx;

        public UserController(BookCartDbContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<IActionResult> Index()
        {
            var users = await _ctx.Users.ToListAsync();
            return View(users);
        }
        async Task PopulateRoles()
        {
            var roles = await _ctx.Roles.ToListAsync();
            var roleList = new SelectList(roles, "Id", "Name");
            ViewBag.Roles = roles;
        }
        public async Task<IActionResult> Create()
        {
            await PopulateRoles();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            if (ModelState.IsValid)
            {
                await _ctx.Users.AddAsync(user);
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            await PopulateRoles();

            return View(user);
        }
        [HttpPost]

        //[Route("(id)")]
        public async Task<IActionResult> Edit(int id, User user)
        {
            if (ModelState.IsValid) {
                _ctx.Attach(user).State=EntityState.Modified;
                await _ctx.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            await PopulateRoles();
            return View(user);
        }
        [HttpPost]

        //[Route("(id)")]
        public async Task<IActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                User? user = await _ctx.Users
                    .SingleOrDefaultAsync(u => u.Id == id);
                if (user == null)
                {
                    _ctx.Users.Remove(user);
                    await _ctx.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index");
        }
    }
}
