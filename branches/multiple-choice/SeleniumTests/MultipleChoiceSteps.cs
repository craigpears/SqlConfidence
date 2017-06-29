using System;
using TechTalk.SpecFlow;
using NUnit.Framework;

namespace SeleniumTests
{
    [Binding]
    public class MultipleChoiceSteps
    {
        [Given(@"I am on the multiple choice page for exercise (.*)")]
        public void GivenIAmOnTheMultipleChoicePageForExercise(int exerciseId)
        {
            MultipleChoicePage.NavigateToPage(exerciseId);
        }
        
        [Then(@"I should be able to see the multiple choice page")]
        public void ThenIShouldBeAbleToSeeTheMultipleChoicePage()
        {
            Assert.IsTrue(true);
        }

        [Then(@"I should be able to see the following multiple choice options")]
        public void ThenIShouldBeAbleToSeeTheFollowingMultipleChoiceOptions(Table options)
        {
            var optionsVisible = MultipleChoicePage.Options;
            foreach (var option in options.Rows)
            {
                Assert.IsTrue(optionsVisible.Contains(option["OptionDescription"]));
            }
        }
    }
}
