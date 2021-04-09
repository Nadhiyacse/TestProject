using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Marketplace
{
    public class MarketPlacePage : BasePage
    {
        private const string FRAME_AG_SINGLE_PLACEMENT = "/symphony-app/ag/single-placement-create/";
        private const string FRAME_AG_PERFORMANCE_PACKAGE = "/symphony-app/ag/performance-package-create/";

        private const string BUTTON_PACKAGE_ONLY_CSS = ".button-row>button";
        private const string BUTTONS_SINGLE_AND_PACKAGE_CSS = ".buttons>div>button";
        private const string BUTTON_SINGLE_PLACEMENT_XPATH = ".//button[contains(@id, '_single_placement')]";
        private const string BUTTON_SINGLE_TO_PACKAGE_XPATH = ".//button[contains(@id, '_performancepackage')]";

        private IEnumerable<IWebElement> _lstProduct => FindElementsByCssSelector(".product-list-grid>div").ToList();
        private IWebElement _txtSearch => FindElementByCssSelector(".aui--search-component-input");
        private IWebElement _btnSearch => FindElementByCssSelector(".aui--search-component-button");
        private IWebElement _deviceFilters => FindElementByXPath("//span[text()='Device']/following-sibling::ul");
        private IWebElement _buyTypeFilters => FindElementByXPath("//span[text()='Buy Type']/following-sibling::ul");

        public MarketPlacePage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SearchAgProduct<T>(string productName)
        {
            ClearInputAndTypeValue(_txtSearch, productName);
            ClickElement(_btnSearch);

            if (!_lstProduct.Any())
                throw new Exception("No placements for the searched text");

            foreach (var product in _lstProduct)
            {
                var name = product.FindElement(By.CssSelector(".card-component-content.detail-title>strong")).Text;
                if (name.Equals(productName))
                {
                    var action = new Actions(driver);
                    action.MoveToElement(product).Build().Perform();

                    string frame = null;
                    if (typeof(T) == typeof(ManagePerformancePackageModal))
                    {
                        if (DoesAgProductTileHaveCreateAsPackageAndPlacementButtons(product))
                        {
                            WaitForElementToBeClickable(product.FindElement(By.CssSelector(BUTTONS_SINGLE_AND_PACKAGE_CSS)));
                            product.FindElement(By.XPath(BUTTON_SINGLE_TO_PACKAGE_XPATH)).Click();
                        }
                        else
                        {
                            WaitForElementToBeClickable(product.FindElement(By.CssSelector(BUTTON_PACKAGE_ONLY_CSS)));
                            product.FindElement(By.CssSelector(BUTTON_PACKAGE_ONLY_CSS)).Click();
                        }

                        frame = FRAME_AG_PERFORMANCE_PACKAGE;
                    }
                    else
                    {
                        WaitForElementToBeClickable(product.FindElement(By.CssSelector(BUTTONS_SINGLE_AND_PACKAGE_CSS)));
                        product.FindElement(By.XPath(BUTTON_SINGLE_PLACEMENT_XPATH)).Click();
                        frame = FRAME_AG_SINGLE_PLACEMENT;
                    }

                    SwitchToFrame(frame);
                    return;
                }
            }

            throw new Exception("Searched placement is not available");
        }

        public bool CheckDeviceFilterShown(string device)
        {
            return _deviceFilters.Text.Contains(device);
        }

        public bool CheckBuyTypeFiltersShown(string buyType)
        {
            return _buyTypeFilters.Text.Contains(buyType);
        }

        private bool DoesAgProductTileHaveCreateAsPackageAndPlacementButtons(IWebElement product)
        {
            try
            {
                product.FindElement(By.CssSelector(BUTTONS_SINGLE_AND_PACKAGE_CSS));
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}