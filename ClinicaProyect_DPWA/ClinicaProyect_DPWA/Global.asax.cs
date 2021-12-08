using ClinicaProyect_DPWA.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ClinicaProyect_DPWA
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //Cada que corra el proyecto verifica si el modelo cambia y se encarga
            //De correrlo si los hay.
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<Context.ClinicaContext,
                Migrations.Configuration>());
            //Roles
            ApplicationDbContext db = new ApplicationDbContext();
            CreateRoles(db);
            CreateSuperUser(db);
            AddPermisionToSuperUser(db);
            db.Dispose();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void AddPermisionToSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));

            //Encontramos el usuario para agregar superusuario
            var user = userManager.FindByName("AdminBelen@gmail.com");
            //Si no tiene el rol, se lo agrega
            if(!userManager.IsInRole(user.Id, "Administrador"))
            {
                userManager.AddToRole(user.Id, "Administrador");
            }

            if (!userManager.IsInRole(user.Id, "Cliente"))
            {
                userManager.AddToRole(user.Id, "Cliente");
            }

            if (!userManager.IsInRole(user.Id, "Empleado"))
            {
                userManager.AddToRole(user.Id, "Empleado");
            }
        }

        private void CreateSuperUser(ApplicationDbContext db)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindByName("AdminBelen@gmail.com");
            if (user == null)
            {
                user = new ApplicationUser
                {
                    UserName = "AdminBelen@gmail.com",
                    Email = "AdminBelen@gmail.com"
                };
                userManager.Create(user, "Admin123.");
            }
        }

        private void CreateRoles(ApplicationDbContext db)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            if (!roleManager.RoleExists("Administrador"))
            {
                roleManager.Create(new IdentityRole("Administrador"));
            }
            if (!roleManager.RoleExists("Cliente"))
            {
                roleManager.Create(new IdentityRole("Cliente"));
            }
            if (!roleManager.RoleExists("Empleado"))
            {
                roleManager.Create(new IdentityRole("Empleado"));
            }
        }
    }
}
