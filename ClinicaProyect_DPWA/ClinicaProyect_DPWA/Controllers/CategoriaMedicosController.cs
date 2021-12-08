using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClinicaProyect_DPWA.Context;
using ClinicaProyect_DPWA.Models;

namespace ClinicaProyect_DPWA.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CategoriaMedicosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: CategoriaMedicos
        public ActionResult Index()
        {
            return View(db.CategoriaMedicos.ToList());
        }

        // GET: CategoriaMedicos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMedico categoriaMedico = db.CategoriaMedicos.Find(id);
            if (categoriaMedico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMedico);
        }

        // GET: CategoriaMedicos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriaMedicos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Categoria,NombreCategoria")] CategoriaMedico categoriaMedico)
        {
            if (ModelState.IsValid)
            {
                db.CategoriaMedicos.Add(categoriaMedico);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(categoriaMedico);
        }

        // GET: CategoriaMedicos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMedico categoriaMedico = db.CategoriaMedicos.Find(id);
            if (categoriaMedico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMedico);
        }

        // POST: CategoriaMedicos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Categoria,NombreCategoria")] CategoriaMedico categoriaMedico)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categoriaMedico).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(categoriaMedico);
        }

        // GET: CategoriaMedicos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaMedico categoriaMedico = db.CategoriaMedicos.Find(id);
            if (categoriaMedico == null)
            {
                return HttpNotFound();
            }
            return View(categoriaMedico);
        }

        // POST: CategoriaMedicos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CategoriaMedico categoriaMedico = db.CategoriaMedicos.Find(id);
            db.CategoriaMedicos.Remove(categoriaMedico);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
