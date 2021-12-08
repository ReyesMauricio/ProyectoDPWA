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
    public class ReservacionesController : Controller
    {
        private ClinicaContext db = new ClinicaContext();

        // GET: Reservaciones
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

            var reservaciones = db.Reservaciones.Include(r => r.EstadoPago).Include(r => r.EstadoReservacion).Include(r => r.Medico).Include(r => r.Paciente);
            return View(reservaciones.ToList());
        }

        // GET: Reservaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // GET: Reservaciones/Create
        public ActionResult Create()
        {
            //Modificamos la vista Create para evitar seleccionar un dato por defecto.
            var list = db.EstadoPagos.ToList();
            list.Add(new EstadoPago { Id_TipoDePago = 0, EstadoDePago = "[Seleccione estado de pago...]" });

            list = list.OrderBy(c => c.EstadoDePago).ToList();
            ViewBag.Id_TipoDePago = new SelectList(list, "Id_TipoDePago", "EstadoDePago");

            var list1 = db.EstadoReservaciones.ToList();
            list1.Add(new EstadoReservacion { Id_EstadoReservacion = 0, EstadoDeCita = "[Seleccione estado de cita...]" });

            list1 = list1.OrderBy(c => c.Id_EstadoReservacion).ToList();
            ViewBag.Id_EstadoReservacion = new SelectList(list1, "Id_EstadoReservacion", "EstadoDeCita");

            var list2 = db.Medicos.ToList();
            list2.Add(new Medico { Id_Medico = 0, Nombres = "[Seleccione nombre del doctor...]" });

            list2 = list2.OrderBy(c => c.Id_Medico).ToList();
            ViewBag.Id_Medico = new SelectList(list2, "Id_Medico", "Nombres");

            var list3 = db.Pacientes.ToList();
            list3.Add(new Paciente { Id_Paciente = 0, Nombres = "[Seleccione nombre del paciente...]" });

            list3 = list3.OrderBy(c => c.Id_Paciente).ToList();
            ViewBag.Id_Paciente = new SelectList(list3, "Id_Paciente", "Nombres");
            return View();
        }

        // POST: Reservaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_Reservacion,Titulo_reservacion,Nota_reservacion,Direccion,Fecha_creado,Fecha_cita,Hora_cita,Sintomas,Medicamentos,Precio,Id_Medico,Id_Paciente,Id_EstadoReservacion,Id_TipoDePago")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Reservaciones.Add(reservacion);
                db.SaveChanges();
                TempData["Accion"] = "Insertado";
                return RedirectToAction("Index");
            }

            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            return View(reservacion);
        }

        // GET: Reservaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            ViewBag.Fecha_cita = string.Format("{0:dd/MM/yyyy}", reservacion.Fecha_cita);
            ViewBag.Fecha_creado = string.Format("{0:dd/MM/yyyy}", reservacion.Fecha_creado);
            return View(reservacion);
        }

        // POST: Reservaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_Reservacion,Titulo_reservacion,Nota_reservacion,Direccion,Fecha_creado,Fecha_cita,Hora_cita,Sintomas,Medicamentos,Precio,Id_Medico,Id_Paciente,Id_EstadoReservacion,Id_TipoDePago")] Reservacion reservacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservacion).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Accion"] = "Editado";
                return RedirectToAction("Index");
            }
            ViewBag.Id_TipoDePago = new SelectList(db.EstadoPagos, "Id_TipoDePago", "EstadoDePago", reservacion.Id_TipoDePago);
            ViewBag.Id_EstadoReservacion = new SelectList(db.EstadoReservaciones, "Id_EstadoReservacion", "EstadoDeCita", reservacion.Id_EstadoReservacion);
            ViewBag.Id_Medico = new SelectList(db.Medicos, "Id_Medico", "Nombres", reservacion.Id_Medico);
            ViewBag.Id_Paciente = new SelectList(db.Pacientes, "Id_Paciente", "Nombres", reservacion.Id_Paciente);
            return View(reservacion);
        }

        // GET: Reservaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservacion reservacion = db.Reservaciones.Find(id);
            if (reservacion == null)
            {
                return HttpNotFound();
            }
            return View(reservacion);
        }

        // POST: Reservaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Reservacion reservacion = db.Reservaciones.Find(id);
            db.Reservaciones.Remove(reservacion);
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
