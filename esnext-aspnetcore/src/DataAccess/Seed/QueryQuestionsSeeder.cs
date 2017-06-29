using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public class QueryQuestionsSeeder
    {
        public void Seed(SqlConfidenceContext context)
        {
            // This seed script is for local use only, so only create for blank databases
            if (context.QueryExercises.Any()) return;
          
            var staffDataSourceId = context.DataSources.Single(x => x.Name == "Staff").Id;

            var section = new Section()
            {
                Name = "Query Questions Section",
                Description = "This section containts the query exercises"
            };

            context.Sections.Add(section);
            context.SaveChanges();

            var exercise = new QueryExercise
            {
                DataSourceId = staffDataSourceId,
                Name = "Query Exercise Example",
                Summary = "This exercise demonstrates the query exercise type",
                CreatedBy = "SeedScript",
                CreatedDate = DateTime.Now,
                PublishedBy = "SeedScript",
                Order = 1,
                Section = section
            };

            context.QueryExercises.Add(exercise);
            context.SaveChanges();

            var question = new QueryQuestion
            {
                Description = "Select everything from the table ORGANISATIONS_AND_STAFF",                
                CreatedBy = "SeedScript",
                CreatedDate = DateTime.Now,
                Order = 0,
                Exercise = exercise,
                CorrectAnswerQuery = "SELECT * FROM ORGANISATIONS_AND_STAFF"
            };

            context.QueryQuestions.Add(question);
            context.SaveChanges();
        }
    }
}
