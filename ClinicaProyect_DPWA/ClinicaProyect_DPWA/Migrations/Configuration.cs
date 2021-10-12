namespace ClinicaProyect_DPWA.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ClinicaProyect_DPWA.Context.ClinicaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            //Habilitamos la perdida de datos en caso de que una de mis migraciones lo solicite.
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(ClinicaProyect_DPWA.Context.ClinicaContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
