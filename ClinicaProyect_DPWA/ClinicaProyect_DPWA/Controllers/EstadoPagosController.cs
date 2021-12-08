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
    public class EstadoPagosController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: EstadoPagos
        public ActionResult Index()
        {
            if (TempData["Accion"] != null)
            {
                var accion = Convert.ToString(TempData["Accion"]);
                if (accion == "Insertado")
                {
                    ViewBag.Accion = "Insertado";
                }
                else if (accion == "Editado")
                {
                    ViewBag.Accion = "Editado";
                }
                else if (accion == "Eliminado")
                {
                    ViewBag.Accion = "Eliminado";
                }
            }
            return View(db.EstadoPagos.ToList());
        }

        // GET: EstadoPagos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // GET: EstadoPagos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EstadoPagos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_TipoDePago,EstadoDePago")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                db.EstadoPagos.Add(estadoPago);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            return View(estadoPago);
        }

        // GET: EstadoPagos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadoPagos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_TipoDePago,EstadoDePago")] EstadoPago estadoPago)
        {
            if (ModelState.IsValid)
            {
                db.Entry(estadoPago).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            return View(estadoPago);
        }

        // GET: EstadoPagos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            if (estadoPago == null)
            {
                return HttpNotFound();
            }
            return View(estadoPago);
        }

        // POST: EstadoPagos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EstadoPago estadoPago = db.EstadoPagos.Find(id);
            db.EstadoPagos.Remove(estadoPago);
            db.SaveChanges();
            TempData["Accion"] = "Eliminado";
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
