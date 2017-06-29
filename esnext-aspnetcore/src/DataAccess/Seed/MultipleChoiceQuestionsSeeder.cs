using DataAccess.Models;
using System;
using System.Collections.Generic;
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
            if (context.MultipleChoiceExercises.Any()) return;

            
            var staffDataSourceId = context.DataSources.Single(x => x.Name == "Staff").Id;

            var section = new Section()
            {
                Name = "Multiple Choice Section",
                Description = "This section has the multiple choice exercises in it"
            };

            context.Sections.Add(section);
            context.SaveChanges();

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
                Section = section,
                Order = 0
            };

            context.MultipleChoiceExercises.Add(exercise);
            context.SaveChanges();

            var question = new MultipleChoiceQuestion
            {
                Description = "Salary Data Type",
                Instructions = "What kind of data type is the salary column?",
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
                            CorrectAnswerMessage = "Salaries are numbers with no decimal places, which makes them integers."
                        },
                        new MultipleChoiceOption
                        {
                            Description = "numeric",
                            IncorrectAnswerMessage = "The numeric data type contains decimal places, salaries have no decimals"
                        }
                    },
                DataQueries = new List<MultipleChoiceDataQuery>() { new MultipleChoiceDataQuery()
                    {
                        SqlQuery = "SELECT TOP 10 name, organisation_name, gender, salary, years_at_company, sick_days_taken, holiday_days_left, performance FROM ORGANISATIONS_AND_STAFF"
                    }
                },
                Exercise = exercise
            };

            context.MultipleChoiceQuestions.Add(question);
            context.SaveChanges();

            question.CorrectOption = question.Options.Single(x => !String.IsNullOrEmpty(x.CorrectAnswerMessage));
            context.SaveChanges();

            var questionTwo = new MultipleChoiceQuestion
            {
                Description = "Gender Data Type",
                Instructions = "What kind of data type is the gender column?",
                CreatedBy = "SeedScript",
                CreatedDate = DateTime.Now,
                Order = 1,
                Options = new List<MultipleChoiceOption>()
                    {
                        new MultipleChoiceOption
                        {
                            Description = "varchar",
                            CorrectAnswerMessage = "The gender column is a varchar column."
                        },
                        new MultipleChoiceOption
                        {
                            Description = "int",
                            IncorrectAnswerMessage = "Please try again."
                        },
                        new MultipleChoiceOption
                        {
                            Description = "numeric",
                            IncorrectAnswerMessage = "Please try again."
                        }
                    },
                DataQueries = new List<MultipleChoiceDataQuery>() { new MultipleChoiceDataQuery()
                    {
                        SqlQuery = "SELECT TOP 10 name, organisation_name, gender, salary, years_at_company, sick_days_taken, holiday_days_left, performance FROM ORGANISATIONS_AND_STAFF"
                    }
                },
                Exercise = exercise
            };

            context.MultipleChoiceQuestions.Add(questionTwo);
            context.SaveChanges();

            questionTwo.CorrectOption = questionTwo.Options.Single(x => !String.IsNullOrEmpty(x.CorrectAnswerMessage));
            context.SaveChanges();

            var questionThree = new MultipleChoiceQuestion
            {
                Description = "Salary Data Type",
                Instructions = "What kind of data type is the holiday_days_left column?",
                CreatedBy = "SeedScript",
                CreatedDate = DateTime.Now,
                Order = 2,
                Options = new List<MultipleChoiceOption>()
                    {
                        new MultipleChoiceOption
                        {
                            Description = "varchar",
                            IncorrectAnswerMessage = "Please try again."
                        },
                        new MultipleChoiceOption
                        {
                            Description = "int",
                            IncorrectAnswerMessage = "The int data type is a whole number, which is not right here."
                        },
                        new MultipleChoiceOption
                        {
                            Description = "numeric",
                            CorrectAnswerMessage = "Well done! The data has decimals, so it's numeric."
                        }
                    },
                DataQueries = new List<MultipleChoiceDataQuery>() { new MultipleChoiceDataQuery()
                    {
                        SqlQuery = "SELECT TOP 10 name, organisation_name, gender, salary, years_at_company, sick_days_taken, holiday_days_left, performance FROM ORGANISATIONS_AND_STAFF"
                    }
                },
                Exercise = exercise
            };

            context.MultipleChoiceQuestions.Add(questionThree);
            context.SaveChanges();

            questionThree.CorrectOption = questionThree.Options.Single(x => !String.IsNullOrEmpty(x.CorrectAnswerMessage));
            context.SaveChanges();
        }
    }
}
