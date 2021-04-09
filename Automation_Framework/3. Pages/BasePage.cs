using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using Automation_Framework.DataModels.InfrastructureData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.FeatureToggles;
using Automation_Framework.Helpers;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages
{
    public class BasePage
    {
        public WebDriverWait Wait;

        protected FeatureContext FeatureContext;
        protected IWebDriver driver;

        protected readonly List<FeatureTogglesData> FeatureToggles;

        private IWebElement _toastContainer => FindElementByCssSelector(".Toastify");

        private const string DATE_TIME_FORMAT_EXPORT = "yyyy-MM-dd";
        private const string DATE_TIME_FORMAT_PH = "MMMddyyyy";
        private const string DATE_TIME_FORMAT_IDO = "yyMM";

        public BasePage(IWebDriver webDriver, FeatureContext featureContext)
        {
            driver = webDriver;
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));

            var genericSetupData = featureContext[ContextStrings.GenericSetupData] as GenericInfrastructureData;
            FeatureToggles = genericSetupData.SymphonyAdminData.FeatureToggles;
            FeatureContext = featureContext;
        }

        public void SwitchToFrame(string iframe)
        {
            try
            {
                Wait.Until(d => IsFrameVisible(iframe));
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException($"Frame {iframe} was not visible", e);
            }
            var frame = driver.FindElement(By.XPath($"//iframe[contains(@src, '{iframe}')]"));
            driver.SwitchTo().Frame(frame);
        }

        public bool IsFrameVisible(string iframe)
        {
            return IsElementPresent(By.XPath($"//iframe[contains(@src, '{iframe}')]"));
        }

        public void SwitchToDefaultContent()
        {
            driver.SwitchTo().DefaultContent();
        }

        public void SwitchToNewWindow(string windowTitle)
        {
            Wait.Until(d => d.WindowHandles.Count != 1);
            driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count - 1]);
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.TitleContains(windowTitle));

            var actualWindowTitle = driver.Title;
            if (!actualWindowTitle.Contains(windowTitle))
            {
                var errorMessage = $"Expected string in title: {windowTitle}\nActual title: {actualWindowTitle}";
                throw new Exception(errorMessage);
            }
        }

        public void SwitchToMainWindow()
        {
            driver.SwitchTo().Window(driver.WindowHandles[0]);
        }

        public void ClearInputAndTypeValue(IWebElement element, string text)
        {
            try
            {
                Wait.Until(driver => element.Displayed);
                if (element.GetAttribute("value") != null)
                {
                    element.SendKeys(Keys.Control + "a");
                    element.SendKeys(Keys.Delete);
                }
                element.SendKeys(text);
                element.SendKeys(Keys.Tab);
            }
            catch (StaleElementReferenceException)
            {
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                Wait.Until(driver => element.Displayed);
                if (element.GetAttribute("value") != null)
                {
                    element.Clear();
                }
                element.SendKeys(text);
                element.SendKeys(Keys.Tab);
            }
            catch (NoSuchElementException)
            {
                var errorMessage = $"{element}' not found";
                throw new Exception(errorMessage);
            }
            catch (InvalidElementStateException)
            {
                WaitForElementToBeClickable(element);
                if (element.GetAttribute("value") != null)
                {
                    element.Clear();
                }
                element.SendKeys(text);
                element.SendKeys(Keys.Tab);
            }
        }

        public void WaitForLoaderSpinnerToDisappear()
        {
            WaitForElementToBeInvisible(By.XPath("//div[@class='loader']"));
        }

        public void ScrollAndClickElement(IWebElement element)
        {
            ScrollToTopOfView(element);
            if (!IsElementClickable(element))
            {
                ScrollToBottomOfView(element);
                if (!IsElementClickable(element))
                    throw new Exception("ERROR: Cannot scroll and click to element");
            }
        }

        public void ScrollToTopOfView(IWebElement element)
        {
            try
            {
                var js = driver as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception e)
            {
                Console.WriteLine("ScrollToTopOfView method failed with exception" + e.Message);
            }
        }

        public void ScrollToBottomOfView(IWebElement element)
        {
            try
            {
                var js = driver as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].scrollIntoView(false);", element);
            }
            catch (Exception e)
            {
                Console.WriteLine("ScrollToBottomOfView method failed with exception" + e.Message);
            }
        }

        public void GridInlineEditElement(IWebElement element, string text)
        {
            new Actions(driver).MoveToElement(element)
                        .Click()
                        .KeyDown(Keys.Control)
                        .SendKeys("a")
                        .KeyUp(Keys.Control)
                        .SendKeys(Keys.Backspace)
                        .SendKeys(text)
                        .Build().Perform();
        }

        public void ScrollRightUntilElementIsDisplayed(IWebElement scrollbar, IWebElement scrollableArea, string elementXpath)
        {
            const int INCREMENT = 100;

            var scrollBarMaxOffset = scrollableArea.Size.Width - scrollbar.Size.Width;
            var offset = 0;

            while (!driver.FindElement(By.XPath(elementXpath)).Displayed && offset < scrollBarMaxOffset)
            {
                offset += INCREMENT;
                ScrollRight(scrollbar, INCREMENT);
            }
        }

        private void ScrollRight(IWebElement scrollbar, int offsetX)
        {
            Actions action = new Actions(driver);
            action.ClickAndHold(scrollbar).MoveByOffset(offsetX, 0).Release().Perform();
        }

        public void SetElementText(IWebElement element, string text)
        {
            try
            {
                var js = driver as IJavaScriptExecutor;
                js.ExecuteScript("arguments[0].value=arguments[1]", element, text);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void SelectWebformDropdownValueByIndex(IWebElement element, int index)
        {
            new SelectElement(element).SelectByIndex(index);
        }

        public void SelectWebformDropdownValueByText(IWebElement element, string text)
        {
            try
            {
                new SelectElement(element).SelectByText(text);
            }
            catch (StaleElementReferenceException)
            {
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                new SelectElement(element).SelectByText(text);
            }
        }

        public void WaitUntilFileIsDownloaded(string exportType, CampaignData campaignData)
        {
            var fileName = string.Empty;

            switch (exportType)
            {
                case "Standard Media Schedule Grid Data Export":
                    fileName = $"{DateTime.Now.ToString(DATE_TIME_FORMAT_EXPORT)}-symphonydataexport_{campaignData.DetailsData.CampaignName.Replace(" ", "_").Replace(":", string.Empty).Replace("/", string.Empty).ToLower()}.xlsx";
                    break;
                case "GroupM PH Cost Estimate Export (Fee Basis)":
                    fileName = $"costestimate_aft_agency_ph_test_client_{DateTime.Now.ToString(DATE_TIME_FORMAT_PH).ToLower()}.xlsx";
                    break;
                case "GroupM Indonesia Mediacom Media Schedule Export":
                    fileName = $"aft_agency_ido_test_client_generic_mediaschedule_{DateTime.Now.ToString(DATE_TIME_FORMAT_IDO).ToLower()}.xlsx";
                    break;
                case "AG IO PDF Export (Prorata Monthly Allocations with Site Breakdown)":
                    fileName = $"{DateTime.Now.ToString(DATE_TIME_FORMAT_EXPORT)}-_ag_io_pdf_export_{string.Join("_",campaignData.DetailsData.CampaignName.ToLower().Trim().Split(' ')).ToLower()}.pdf";
                    break;
                default:
                    Console.WriteLine($"WARN: The {exportType} file is not supported yet");
                    return;
            }

            try
            {
                Wait.Until(driver => FileHelper.FileExists(FileHelper.GetDownloadsFolderPath(), fileName));
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"File '{fileName}' was not found");
            }
        }

        public void WaitForPageLoadCompleteAfterClickElement(IWebElement element)
        {
            ClickElement(element);
            WaitForPageLoadComplete();
        }

        public void ClickElement(IWebElement element)
        {
            try
            {
                Wait.Until(driver => element.Displayed);
                WaitForElementToBeClickable(element);
                element.Click();
            }
            catch (StaleElementReferenceException)
            {
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(element));
                Wait.Until(driver => element.Displayed);
                WaitForElementToBeClickable(element);
                element.Click();
            }
            catch (NoSuchElementException e)
            {
                throw e;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new WebDriverException(e.InnerException.Message);
            }
        }

        public bool IsElementClickable(IWebElement element)
        {
            try
            {
                WaitForElementToBeVisible(element);
                WaitForElementToBeClickable(element);
                element.Click();
                return true;
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine($"INFO: Element became stale. Retrying click...");
                element = FindElementByXPath(GetAbsoluteXPath(element));
                WaitForElementToBeVisible(element);
                WaitForElementToBeClickable(element);
                element.Click();
                return true;
            }
            catch (NoSuchElementException e)
            {
                Console.WriteLine($"WARN: {e.Message}");
                return false;
            }
            catch (WebDriverTimeoutException e)
            {
                Console.WriteLine($"WARN: {e.Message}");
                Console.WriteLine($"WARN: {e.InnerException.Message}");
                return false;
            }
            catch (ElementClickInterceptedException e)
            {
                Console.WriteLine($"WARN: {e.Message}");
                return false;
            }
        }

        public void WaitForElementToBeClickable(IWebElement element)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public void WaitForElementToBeClickable(By by)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(by));
        }

        public void WaitForElementToBeVisible(By by)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
        }

        public void WaitForElementToBeVisible(IWebElement element)
        {
            Wait.Until(driver => element.Displayed);
        }

        public void WaitForElementToBeInvisible(By by)
        {
            Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(by));
        }

        public void WaitForPageLoadComplete(int timeOutInSeconds = 60)
        {
            var js = driver as IJavaScriptExecutor;
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds));

            try
            {
                Wait.Until(d => js.ExecuteScript("return document.readyState").ToString().Equals("complete"));
                Wait.Until(d => FindElementByXPath("//button[.='Switch Accounts']"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException.Message);
            }
        }

        public void RemoveAllItemsFromMultiSelect(IWebElement element)
        {
            var items = element.FindElements(By.CssSelector(".select-component__multi-value__remove"));

            foreach (var item in items)
            {
                item.Click();
            }
        }

        public void SelectMultipleValuesFromReactDropdownByText(IWebElement element, List<string> values, bool contains = false)
        {
            foreach (var value in values)
            {
                SelectReactDropdownByText(element, value, contains, true);
            }
        }

        public void SelectSingleValueFromReactDropdownByText(IWebElement element, string value, bool contains = false)
        {
            SelectReactDropdownByText(element, value, contains);
        }

        public void SelectSingleValueFromReactDropdownByText(string elementXpath, string value, bool contains = false)
        {
            SelectReactDropdownByText(elementXpath, value, contains);
        }

        private void SelectReactDropdownByText(IWebElement element, string value, bool contains = false, bool isMultiSelect = false)
        {
            string path = $"//*[contains(@class, 'select-component__menu-list')]//div[(text()='{value}')]";

            if (contains)
            {
                path = path.Replace($"text()='{value}'", $"contains(text(),'{value}')");
            }

            try
            {
                ValidateSelectedItem(element, path, value, isMultiSelect: isMultiSelect);
            }
            catch (StaleElementReferenceException)
            {
                Console.WriteLine($"INFO: Element became stale. Retrying select...");
                ValidateSelectedItem(element, path, value, isMultiSelect: isMultiSelect);
            }
            catch (NoSuchElementException e)
            {
                var errorMessage = $"The dropdown or the option '{value}' was not found\n.{e.Message}\n{e.StackTrace}";
                throw new Exception(errorMessage);
            }
            catch (WebDriverTimeoutException e)
            {
                var errorMessage = $"A wait method timed because: {e.InnerException.Message}\n{e.StackTrace}";
                throw new Exception(errorMessage);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void SelectReactDropdownByText(string elementXpath, string value, bool contains = false, bool isMultiSelect = false)
        {
            string path = $"//*[contains(@class, 'select-component__menu-list')]//div[(text()='{value}')]";

            if (contains)
            {
                path = path.Replace($"text()='{value}'", $"contains(text(),'{value}')");
            }

            try
            {
                ValidateSelectedItem(elementXpath, path, value, isMultiSelect: isMultiSelect);
            }
            catch (StaleElementReferenceException)
            {
                Wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.StalenessOf(FindElementByXPath(elementXpath)));
                ValidateSelectedItem(elementXpath, path, value, isMultiSelect: isMultiSelect);
            }
            catch (NoSuchElementException)
            {
                var errorMessage = $"'{value}' not found";
                throw new Exception(errorMessage);
            }
            catch (Exception e)
            {
                throw new Exception(e.InnerException.Message);
            }
        }

        public object ExecuteJavaScript(string script)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(script);
        }

        public void Highlight(IWebElement element)
        {
            try
            {
                var javaScriptDriver = (IJavaScriptExecutor)driver;
                var highlightJavascript = @"arguments[0].style.cssText = ""border-width: 3px; border-style: solid; border-color: green"";";
                javaScriptDriver.ExecuteScript(highlightJavascript, element);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void Ajaxwait()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
                wait.Until((d) => (bool)(driver as IJavaScriptExecutor).ExecuteScript("return jQuery.active == 0"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void AcceptAlert(int timeout = 60)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeout));

            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                Console.WriteLine("INFO: Alert is present");
                var alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("WARN: Alert was not present");
            }
        }

        public void SetWaitTimeout(int timeoutInSeconds)
        {
            Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public bool IsElementPresent(By by)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
            finally
            {
                var implicitWaitTimeout = double.Parse(ConfigurationManager.AppSettings["ImplicitWaitTimeout"]);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTimeout);
            }
        }

        public bool HaveClassName(IWebElement element, string className)
        {
            var classAttributes = element.GetAttribute("class").Split(' ');

            return classAttributes.Contains(className);
        }

        public string GetTodaysDate(string format = "dd MMM yyyy")
        {
            return DateTime.Today.ToString(format);
        }

        public string GetFutureDate(string startDate, int addValue, string addType = "Months", string format = "dd MMM yyyy")
        {
            var date = DateTime.Parse(startDate);

            switch (addType)
            {
                case "Months":
                    date = date.AddMonths(addValue);
                    break;
                case "Days":
                    date = date.AddDays(addValue);
                    break;
            }

            return date.ToString(format);
        }

        public FlightDates GetActualFlightDates(FlightDates flightDates)
        {
            if (string.IsNullOrWhiteSpace(flightDates.StartDate))
            {
                flightDates.StartDate = GetTodaysDate();
            }

            if (string.IsNullOrWhiteSpace(flightDates.EndDate))
            {
                flightDates.EndDate = GetFutureDate(flightDates.StartDate, 3);
            }
            else if (flightDates.EndDate.Contains("+"))
            {
                var relative = flightDates.EndDate.TrimStart('+').Split(' ');
                flightDates.EndDate = GetFutureDate(flightDates.StartDate, int.Parse(relative[0]), relative[1]);
            }

            return flightDates;
        }

        public void OpenAndSwitchToNewWindow()
        {
            var js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("window.open();");
            SwitchToNewWindow(string.Empty);
        }

        public bool ElementContainsText(IWebElement element, string text)
        {
            return element.Text.Contains(text);
        }

        public IWebElement GetFirstElementWithText(IList<IWebElement> elementList, string text)
        {
            return elementList.FirstOrDefault(element => element.Text.Contains(text));
        }

        public void SelectElement(IWebElement element)
        {
            if (!element.Selected)
            {
                ScrollAndClickElement(element);
            }
        }

        public bool CheckNumberOfElementsThatExistContainingText(IWebElement element, string text, int numberOfElements = 1)
        {
            var elementCount = element.FindElements(By.XPath($"//*[contains(text(), '{text}')]/../..")).Count;
            return elementCount == numberOfElements;
        }

        public bool CheckNumberOfElementsThatExistByExactText(IWebElement element, string text, int numberOfElements = 1)
        {
            var elementCount = element.FindElements(By.XPath($"//*[text()='{text}']/../..")).Count;
            return elementCount == numberOfElements;
        }

        private void ValidateSelectedItem(IWebElement element, string path, string value, int tries = 6, bool isMultiSelect = false)
        {
            var attempts = 0;
            string selected = null;
            var xpath = GetAbsoluteXPath(element);

            while (!IsElementPresent(By.XPath(path)) && attempts < tries)
            {
                Wait.Until(d => FindElementByXPath(xpath));
                element = FindElementByXPath(xpath);
                ScrollAndClickElement(element);
                WaitForElementToBeVisible(By.XPath(path));
                attempts++;
            }

            for (int i = 0; i < tries; i++)
            {
                if (!isMultiSelect && !IsElementPresent(By.XPath(path)))
                {
                    ScrollAndClickElement(element);
                    WaitForElementToBeVisible(By.XPath(path));
                }

                var dropdownElement = FindElementByXPath(path);
                WaitForElementToBeClickable(dropdownElement);
                ScrollAndClickElement(dropdownElement);

                if (isMultiSelect)
                    return;

                element = FindElementByXPath(xpath);
                selected = GetSelectedValueFromDropDown(element, true);
                if (selected == value)
                    return;
            }

            if (selected != value)
                throw new Exception($"Failed to select value '{value}' from the dropdown list");
        }

        private void ValidateSelectedItem(string elementXpath, string path, string value, int maxRetries = 6, bool isMultiSelect = false)
        {
            var attempts = 0;
            string selected = null;
            var element = FindElementByXPath(elementXpath);

            while (!IsElementPresent(By.XPath(path)) && attempts < maxRetries)
            {
                ClickElement(element);
                WaitForElementToBeVisible(By.XPath(path));
                attempts++;
            }

            for (int i = 0; i < maxRetries; i++)
            {
                if (!isMultiSelect && !IsElementPresent(By.XPath(path)))
                {
                    ClickElement(element);
                    WaitForElementToBeVisible(By.XPath(path));
                }

                var dropdownElement = FindElementByXPath(path);
                WaitForElementToBeClickable(dropdownElement);
                ScrollAndClickElement(dropdownElement);

                if (isMultiSelect)
                    return;

                element = FindElementByXPath(elementXpath);
                selected = GetSelectedValueFromDropDown(element, true);
                if (selected == value)
                return;
            }

            if (selected != value)
                throw new Exception($"Failed to select value '{value}' from the dropdown list");
        }

        public string GetSelectedValueFromDropDown(IWebElement ddl, bool react = false)
        {
            return react ? ddl.FindElement(By.XPath(".//div[contains(@class,'select-component__value-container')]")).Text : ddl.FindElements(By.XPath("option")).Where(e => e.Selected == true).First().Text;
        }

        protected virtual IWebElement FindElementById(string id)
        {
            return driver.FindElement(By.Id(id));
        }

        protected virtual IWebElement FindElementByCssSelector(string cssSelector)
        {
            return driver.FindElement(By.CssSelector(cssSelector));
        }

        protected virtual IEnumerable<IWebElement> FindElementsByCssSelector(string cssSelector)
        {
            return driver.FindElements(By.CssSelector(cssSelector));
        }

        protected virtual IWebElement FindElementByXPath(string xpath)
        {
            return driver.FindElement(By.XPath(xpath));
        }

        protected virtual IEnumerable<IWebElement> FindElementsByXPath(string xpath)
        {
            return driver.FindElements(By.XPath(xpath));
        }

        protected virtual IWebElement FindElementByLinkText(string linkText)
        {
            return driver.FindElement(By.LinkText(linkText));
        }

        protected virtual IWebElement FindElementByName(string name)
        {
            return driver.FindElement(By.Name(name));
        }

        protected virtual IWebElement FindElementByClassName(string className)
        {
            return driver.FindElement(By.ClassName(className));
        }

        public bool IsReactCheckboxSelected(IWebElement we)
        {
            var isSelected = we.FindElement(By.XPath("../..")).GetAttribute("class").Contains("checked") ? true : false;
            return isSelected;
        }

        public void EnterDate(IWebElement element, string date)
        {
            element.SendKeys(Keys.Control + "a");
            element.SendKeys(Keys.Delete);
            element.SendKeys(date);
            element.SendKeys(Keys.Tab);
        }

        public void TriggerOnBlurForCurrentlySelectedInput()
        {
            driver.FindElement(By.TagName("body")).Click();
        }

        public void WaitForDataToBePopulated(double second = 1)
        {
            Thread.Sleep(TimeSpan.FromSeconds(second));
        }

        protected void SelectWebformDropdownValueIfRequired(IWebElement dropdownElement, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                SelectWebformDropdownValueByText(dropdownElement, value);
            }
        }

        protected void ClearInputAndTypeValueIfRequired(IWebElement textboxElement, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                ClearInputAndTypeValue(textboxElement, value);
            }
        }

        protected void TypeValueIfRequired(IWebElement element, string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                element.SendKeys(value);
            }
        }

        protected void SetReactCheckBoxState(IWebElement checkboxElement, bool state)
        {
            if ((state && !IsReactCheckboxSelected(checkboxElement)) || (!state && IsReactCheckboxSelected(checkboxElement)))
            {
                ScrollAndClickElement(checkboxElement);
            }
        }

        protected void SetWebformCheckBoxState(IWebElement checkboxElement, bool state)
        {
            if ((state && !checkboxElement.Selected) || (!state && checkboxElement.Selected))
            {
                ScrollAndClickElement(checkboxElement);
            }
        }

        protected IWebElement GetWebElementUsingSettingLabel(string xpath, string label)
        {
            var webElement = FindElementByXPath(string.Format(xpath, label));

            return webElement;
        }

        protected void SetCustomFieldElement(IWebElement customFieldElement, CustomFieldType type, List<string> values)
        {
            switch (type)
            {
                case CustomFieldType.MultiSelect:
                    customFieldElement = customFieldElement.FindElement(By.XPath("..//div[contains(@class,'select-component__control')]"));
                    SelectMultipleValuesFromReactDropdownByText(customFieldElement, values);
                    break;

                case CustomFieldType.Text:
                    customFieldElement = customFieldElement.FindElement(By.XPath("..//input"));
                    ClearInputAndTypeValue(customFieldElement, values[0]);
                    break;

                case CustomFieldType.MultiLine:
                    customFieldElement = customFieldElement.FindElement(By.XPath("..//textarea"));
                    ClearInputAndTypeValue(customFieldElement, values[0]);
                    break;

                case CustomFieldType.Image:
                    var removeImageElementXPath = "//button[contains(@class, 'fileupload-component-btn-remove-file')]";
                    if (IsElementPresent(By.XPath(removeImageElementXPath)))
                    {
                        var removeImageElement = customFieldElement.FindElement(By.XPath($"..{removeImageElementXPath}"));
                        ClickElement(removeImageElement);
                    }

                    customFieldElement = customFieldElement.FindElement(By.XPath("..//input[@name='file']"));
                    var filePath = FileHelper.GetImageFilePath(values[0]);
                    customFieldElement.SendKeys(filePath);
                break;

                case CustomFieldType.DatePicker:
                    customFieldElement = customFieldElement.FindElement(By.XPath("..//input"));
                    if (!values.Any())
                    {
                        values.Add(GetTodaysDate());
                    }
                    ClearInputAndTypeValue(customFieldElement, values[0]);
                    break;

                default:
                    throw new Exception($"The custom field type: '{type}' is currently not supported.");
            }
        }

        protected void ClearSelectedValueFromReactDropdown(IWebElement dropdownElement)
        {
            var elementList = dropdownElement.FindElements(By.XPath("..//div[contains(@class,'select-component__clear-indicator')]"));
            if (elementList.Count > 0)
            {
                var clearElement = elementList.First();
                ScrollAndClickElement(clearElement);
            }                
        }

        protected bool IsFeatureToggleEnabled(FeatureToggle featureToggle)
        {
            return FeatureToggles.Any(ft => ft.Feature.Equals(featureToggle));
        }

        public void RefreshPage()
        {
            driver.Navigate().Refresh();
            WaitForPageLoadComplete();
        }

        public bool IsOctopusVariable(string variable)
        {
            const string octopusVariablePrefix = "#{";
            return variable.StartsWith(octopusVariablePrefix); 
        }

        protected string FormatDate(string date, string format)
        {
            var dateTime = Convert.ToDateTime(date);
            return dateTime.ToString(format);
        }

        protected PurchaseType ConvertToPurchaseTypeEnum(string purchaseType)
        {
            if (!Enum.TryParse(purchaseType, out PurchaseType parsedPurchaseType))
            {
                return new PurchaseType();
            }

            return parsedPurchaseType;
        }

        protected void CloseTabAndSwitchToNewWindow(string windowTitle)
        {
            driver.Close();

            SwitchToNewWindow(windowTitle);
        }

        public void DismissAllToastNotifications()
        {
            var toastCssSelector = ".Toastify__close-button";

            try
            {
                WaitForElementToBeVisible(By.CssSelector(toastCssSelector));    
            }
            catch
            {
                Console.WriteLine("WARN: There was no toast notification to dismiss");
            }

            var toastDismissButtons = _toastContainer.FindElements(By.CssSelector(toastCssSelector));
            Console.WriteLine($"INFO: Toast count = {toastDismissButtons.Count}");

            foreach (var toastDismissButton in toastDismissButtons)
            {
                ClickElement(toastDismissButton);
            }
        }

        public bool IsSingleSuccessToastNotificationShown()
        {
            WaitForElementToBeVisible(By.ClassName("toast-type-success"));
            var successToasts = _toastContainer.FindElements(By.ClassName("toast-type-success"));
            return successToasts.Count == 1;
        }

        public bool IsSuccessNotificationShownWithMessage(string message)
        {
            WaitForElementToBeVisible(By.ClassName("toast-type-success"));
            var successToasts = _toastContainer.FindElements(By.XPath("//span[@class='toast-type-success']/../.."));
            return successToasts.Any(toast => toast.Text.Contains(message));
        }

        private string GetAbsoluteXPath(IWebElement element)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(
                    "function absoluteXPath(element) {" +
                            "var comp, comps = [];" +
                            "var parent = null;" +
                            "var xpath = '';" +
                            "var getPos = function(element) {" +
                            "var position = 1, curNode;" +
                            "if (element.nodeType == Node.ATTRIBUTE_NODE) {" +
                            "return null;" +
                            "}" +
                            "for (curNode = element.previousSibling; curNode; curNode = curNode.previousSibling) {" +
                            "if (curNode.nodeName == element.nodeName) {" +
                            "++position;" +
                            "}" +
                            "}" +
                            "return position;" +
                            "};" +

                            "if (element instanceof Document) {" +
                            "return '/';" +
                            "}" +

                            "for (; element && !(element instanceof Document); element = element.nodeType == Node.ATTRIBUTE_NODE ? element.ownerElement : element.parentNode) {" +
                            "comp = comps[comps.length] = {};" +
                            "switch (element.nodeType) {" +
                            "case Node.TEXT_NODE:" +
                            "comp.name = 'text()';" +
                            "break;" +
                            "case Node.ATTRIBUTE_NODE:" +
                            "comp.name = '@' + element.nodeName;" +
                            "break;" +
                            "case Node.PROCESSING_INSTRUCTION_NODE:" +
                            "comp.name = 'processing-instruction()';" +
                            "break;" +
                            "case Node.COMMENT_NODE:" +
                            "comp.name = 'comment()';" +
                            "break;" +
                            "case Node.ELEMENT_NODE:" +
                            "comp.name = element.nodeName;" +
                            "break;" +
                            "}" +
                            "comp.position = getPos(element);" +
                            "}" +

                            "for (var i = comps.length - 1; i >= 0; i--) {" +
                            "comp = comps[i];" +
                            "xpath += '/' + comp.name.toLowerCase();" +
                            "if (comp.position !== null) {" +
                            "xpath += '[' + comp.position + ']';" +
                            "}" +
                            "}" +

                            "return xpath;" +

                            "} return absoluteXPath(arguments[0]);", element).ToString();
        }
    }
}