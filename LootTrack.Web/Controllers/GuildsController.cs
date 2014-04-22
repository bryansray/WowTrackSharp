using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LootTrack.Domain.Models;
using Repository.Pattern.Repositories;

namespace LootTrack.Web.Controllers
{
    public class GuildsController : Controller
    {
        private readonly IRepository<Guild> _guildRepository;

        public GuildsController(IRepository<Guild> guildRepository)
        {
            _guildRepository = guildRepository;
        }

        //
        // GET: /Guilds/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Guilds/Details/5
        public ActionResult Details(int id)
        {
            var guild = _guildRepository.Query(x => x.Id == id).Include(x => x.Characters);

            return View();
        }

        //
        // GET: /Guilds/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Guilds/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Guilds/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Guilds/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Guilds/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Guilds/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
