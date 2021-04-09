using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyClassificationsPage : BasePage
    {
        private const string TAG_NAME_TH = "th";
        private const string CLASSIFICATIONS_TABLE_XPATH = "//*[@class='symphony-admin-agency-classification-page']//table";

        private IWebElement _btnAdd => FindElementByXPath("//div[text() = 'Add']/ancestor::button");
        private IWebElement _btnEdit => FindElementByXPath("//div[text() = 'Edit']/ancestor::button");
        private IWebElement _tableHeaderRow => FindElementByXPath($"{CLASSIFICATIONS_TABLE_XPATH}/thead");

        // Elements in Add Edit Classification modal
        private IWebElement _txtName => FindElementByXPath($"//label[text() = 'Name']/ancestor::div[@class = 'field-row row']//input");
        private IWebElement _txtTechnicalName => FindElementByXPath($"//label[text() = 'Technical Name']/ancestor::div[@class = 'field-row row']//input");
        private IWebElement _chkMandatory => FindElementByXPath($"//input[@id = 'mandatoryClassification']/../div[@class = 'checkbox-component-icon']");
        private IWebElement _chkApplyToFutureClients => FindElementByXPath($"//input[@id = 'enableForFutureClients']/../div[@class = 'checkbox-component-icon']");
        private IWebElement _chkApplyToCurrentClients => FindElementByXPath($"//input[@id = 'enableForExistingClients']/../div[@class = 'checkbox-component-icon']");
        private IWebElement _rdGroupOrCascadingGroup(string displayStyle) => FindElementByXPath($"//div[text() = '{displayStyle}']/../div[@class = 'radio-component-input-container']");
        private IWebElement _txtSubcategoryLabel => FindElementByXPath($"//label[text() = 'Subcategory Label']/ancestor::div[@class = 'field-row row']//input");
        private IWebElement _ddlGroupName => FindElementByXPath($"//label[text() = 'Group Name']/ancestor::div[@class = 'field-row row']//div[contains(@class,'select-component')]");
        private IWebElement _ddlEnterItems => FindElementByXPath($"//label[text() = 'Enter Items']/ancestor::div[@class = 'field-row row']//div[contains(@class,'select-component')]");
        private IWebElement _btnAddItems => FindElementByXPath($"//label[text() = 'Enter Items']/ancestor::div[@class = 'field-row row']//button");
        private IWebElement _btnSave => FindElementByXPath($"//*[text() = 'Save']/ancestor::button");
        private IWebElement _btnCancel => FindElementByXPath($"//*[text() = 'Cancel']/ancestor::button");
        private IWebElement _lnkContinueAndSave => FindElementByXPath($"//*[text() = 'Continue and Save']/ancestor::button");

        public AgencyClassificationsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public bool IsTechnicalNameExist(ClassificationData data)
        {
            var isTechnicalNameExist = false;

            if (!IsElementPresent(By.XPath(CLASSIFICATIONS_TABLE_XPATH)))
            {
                isTechnicalNameExist = false;
            }
            else
            {
                var technicalNameColumnIndex = _tableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList().FindIndex(x => x.Text == "Technical Name") + 1;

                isTechnicalNameExist = IsElementPresent(By.XPath($"{CLASSIFICATIONS_TABLE_XPATH}/tbody//tr//td[{technicalNameColumnIndex}][text() = '{data.TechnicalName}']"));
            }

            return isTechnicalNameExist;
        }

        public void AddClassification(ClassificationData data)
        {
            EnsureMandatoryValuesAreProvided(data);

            ScrollAndClickElement(_btnAdd);
            ClearInputAndTypeValue(_txtName, data.Name);
            ClearInputAndTypeValue(_txtTechnicalName, data.TechnicalName);
            SetReactCheckBoxState(_chkMandatory, data.IsMandatory);
            SetReactCheckBoxState(_chkApplyToFutureClients, data.IsAppliedtoFutureClients);
            SetReactCheckBoxState(_chkApplyToCurrentClients, data.IsAppliedToCurrentClients);
            ScrollAndClickElement(_rdGroupOrCascadingGroup(data.DisplayStyle));

            if (data.DisplayStyle.Equals("Cascading Group"))
            {
                ClearInputAndTypeValue(_txtSubcategoryLabel, data.SubcategoryLabel);
            }

            foreach (var group in data.Groups)
            {
                EnterValueForDropDownList(_ddlGroupName, group.GroupName);
                foreach (var item in group.Items)
                {
                    EnterValueForDropDownList(_ddlEnterItems, item);
                }
                ScrollAndClickElement(_btnAddItems);
                Assert.IsTrue(AreAllItemsExist(group.Items));
            }
            ScrollAndClickElement(_btnSave);
            WaitForElementToBeInvisible(By.XPath("//div[@class='modal-sm modal-dialog']"));
        }

        private bool AreAllItemsExist(List<string> items)
        {
            var areAllItemsExist = true;

            foreach (var item in items)
            {
                if (!IsElementPresent(By.XPath($"//input[@name = 'itemName'][@value = '{item}']")))
                {
                    areAllItemsExist = false;
                    break;
                }
            }

            return areAllItemsExist;
        }

        private void EnsureMandatoryValuesAreProvided(ClassificationData data)
        {
            var dataErrorFound = false;
            var classificationDataErrors = new StringBuilder();
            classificationDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(data.Name.ToString()))
            {
                dataErrorFound = true;
                classificationDataErrors.Append("\n- Name was not set");
            }

            if (string.IsNullOrWhiteSpace(data.TechnicalName))
            {
                dataErrorFound = true;
                classificationDataErrors.Append("\n- Technical Name was not set");
            }

            if (string.IsNullOrWhiteSpace(data.DisplayStyle))
            {
                dataErrorFound = true;
                classificationDataErrors.Append("\n- Display Style was not set");
            }

            if (data.Groups == null || !data.Groups.Any())
            {
                dataErrorFound = true;
                classificationDataErrors.Append("\n- Group Items was not set");
            }
            else
            {
                foreach (var group in data.Groups)
                {
                    if (group.Items == null || !group.Items.Any())
                    {
                        dataErrorFound = true;
                        classificationDataErrors.Append("\n- Items was not set");
                    }
                }
            }

            if (dataErrorFound)
                throw new ArgumentException(classificationDataErrors.ToString());
        }

        private void EnterValueForDropDownList(IWebElement we, string value)
        {
            Actions actions = new Actions(driver);
            if (!string.IsNullOrEmpty(value))
            {
                ScrollAndClickElement(we);
                actions.SendKeys(value).SendKeys(Keys.Tab).Build().Perform();
            }
        }
    }
}
