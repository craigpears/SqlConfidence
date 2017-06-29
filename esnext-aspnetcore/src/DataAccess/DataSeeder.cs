using DataAccess.Models;
using DataAccess.Seed;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public static class DataSeeder
{
    // TODO: Move this code when seed data is implemented in EF 7

    /// <summary>
    /// This is a workaround for missing seed data functionality in EF 7.0-rc1
    /// More info: https://github.com/aspnet/EntityFramework/issues/629
    /// </summary>
    /// <param name="app">
    /// An instance that provides the mechanisms to get instance of the database context.
    /// </param>
    public static void SeedData(this IApplicationBuilder app)
    {
        var context = app.ApplicationServices.GetService<SqlConfidenceContext>();

        var dataSourceSeeder = new DataSourceSeeder();
        var usersSeeder = new UsersSeeder();
        var queryQuestionsSeeder = new QueryQuestionsSeeder();
        var multipleChoiceQuestionsSeeder = new MultipleChoiceQuestionsSeeder();
        var userActionTypesSeeder = new UserActionTypesSeeder();

        dataSourceSeeder.Seed(context);
        usersSeeder.Seed(context);
        queryQuestionsSeeder.Seed(context);
        multipleChoiceQuestionsSeeder.Seed(context);
        userActionTypesSeeder.Seed(context);
    }
}