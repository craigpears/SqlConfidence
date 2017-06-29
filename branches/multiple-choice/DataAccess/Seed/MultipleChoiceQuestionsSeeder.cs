using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Seed
{
    public class MultipleChoiceQuestionsSeeder
    {
        public void Seed(SqlConfidenceContext context)
        {
            // This seed script is for local use only, so only create for blank databases
            if (context.Exercises.Any()) return;

            var multipleChoiceSectionName = "Multiple Choice Exercises";
            var staffDataSourceId = context.DataSources.Single(x => x.Name == "Staff").DataSourceId;

            var staffDataQuery = new MultipleChoiceDataQuery()
            {
                SqlQuery = "SELECT name, organisation_name, gender, salary, years_at_company, sick_days_taken, holiday_days_left, performance FROM ORGANISATIONS_AND_STAFF"
            };

            var exercise = new MultipleChoiceExercise
            {
                DataSourceId = staffDataSourceId,
                Name = "Multiple Choice Example",
                Summary = "This exercise demonstrates the multiple choice exercise type",
                CreatedBy = "SeedScript",
                CreatedDate = DateTime.Now,
                Published = true,
                PublishedDate = DateTime.Now,
                PublishedBy = "SeedScript",
                Order = 0,
                SectionName = multipleChoiceSectionName,
                DataQueries = new List<MultipleChoiceDataQuery>()
                    {
                        staffDataQuery
                    }
            };


            context.MultipleChoiceExercises.AddOrUpdate(
                x => x.Name,
                exercise
            );

            context.SaveChanges();

            var question = new MultipleChoiceQuestion
            {
                Description = "Salary Data Type",
                InstructionsTemplate = "What kind of data type is the salary column?",
                CreatedBy = "SeedScript",
                CreatedDate = DateTime.Now,
                Order = 0,
                Options = new List<MultipleChoiceOption>()
                    {
                        new MultipleChoiceOption
                        {
                            Description = "varchar",
                            IncorrectAnswerMessage = "Salaries are number of some kind"
                        },
                        new MultipleChoiceOption
                        {
                            Description = "int",
                            CorrectAnswerMessage = "Correct!  Salaries are numbers with no decimal places, which makes them integers."
                        },
                        new MultipleChoiceOption
                        {
                            Description = "numeric",
                            IncorrectAnswerMessage = "The numeric data type contains decimal places, salaries have no decimals"
                        }
                    },
                DataQueries = new List<MultipleChoiceDataQuery>() { staffDataQuery },
                Exercise = exercise
            };

            context.MultipleChoiceQuestions.AddOrUpdate(
                x => x.Description,
                question
            );

            context.SaveChanges();

            question.CorrectOption = question.Options.Single(x => !String.IsNullOrEmpty(x.CorrectAnswerMessage));

            context.SaveChanges();
        }
    }
}
