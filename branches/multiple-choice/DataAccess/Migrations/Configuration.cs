namespace DataAccess.Migrations
{
    using Models;
    using Seed;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<SqlConfidenceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(SqlConfidenceContext context)
        {
            var dataSourceSeeder = new DataSourceSeeder();
            var usersSeeder = new UsersSeeder();
            var multipleChoiceQuestionsSeeder = new MultipleChoiceQuestionsSeeder();
            var userActionTypesSeeder = new UserActionTypesSeeder();

            dataSourceSeeder.Seed(context);
            usersSeeder.Seed(context);
            multipleChoiceQuestionsSeeder.Seed(context);
            userActionTypesSeeder.Seed(context);
        }
    }
}
