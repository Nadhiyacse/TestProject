using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.InfrastructureData;
using Automation_Framework.DataModels.WorkflowTestData;
using Automation_Framework.Helpers;
using Automation_Framework.Hooks.Enums;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using BoDi;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using TechTalk.SpecFlow;

namespace Automation_Framework.Hooks
{
    [Binding]
    public class TestFixture
    {
        private ExtentTest _scenario;
        private static ExtentReports _extentReports;
        private const string DOWNLOADS_FOLDER_NAME = "Downloads";
        private readonly IObjectContainer _objectConatiner;
        private IWebDriver _driver;
        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;
        private static Stopwatch _featureTimer = new Stopwatch();

        public TestFixture(IObjectContainer objectConatiner, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectConatiner = objectConatiner;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
        }

        // This block will be either deleted or updated once the threading is finalised
        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            var workingDirectory = FileHelper.GetWorkingDirectoryPath();
            var todaysdate = DateTime.Now.ToString("ddMMMyyyy");
            var reportsFolder = string.Format(Path.Combine(workingDirectory, @"Reports\{0}"), todaysdate);

            if (!Directory.Exists(reportsFolder))
            {
                Directory.CreateDirectory(reportsFolder);
            }

            var currentTimeStamp = DateTime.Now.ToString("ddMMMyyy_HH_mm_ss");
            var reportFilepath = string.Format(Path.Combine(reportsFolder, @"Report_{0}.html"), currentTimeStamp);
            var htmlReporter = new ExtentHtmlReporter(reportFilepath);
            htmlReporter.Configuration().Theme = Theme.Dark;

            _extentReports = new ExtentReports();
            _extentReports.AttachReporter(htmlReporter);
            var environmentUrl = ConfigurationManager.AppSettings["EnvironmentUrl"];
            _extentReports.AddSystemInfo("Application", "Symphony");
            _extentReports.AddSystemInfo("Environment", environmentUrl);
            _extentReports.AddSystemInfo("Machine", Environment.MachineName);
            _extentReports.AddSystemInfo("OS", Environment.OSVersion.VersionString);
            var environmentScope = (EnvironmentScope)Enum.Parse(typeof(EnvironmentScope), ConfigurationManager.AppSettings["EnvironmentScope"]);
            var genericSetupData = ReadTestDataFromJson<GenericInfrastructureData>($"Generic_Setup.json") as GenericInfrastructureData;
            genericSetupData.SymphonyAdminData.FeatureToggles = genericSetupData.SymphonyAdminData.FeatureToggles
                .Where(ft => ft.EnvironmentScopes.Contains(environmentScope)).ToList();

            var enabledFeatures = new StringBuilder();
            foreach (var ft in genericSetupData.SymphonyAdminData.FeatureToggles)
            {
                enabledFeatures.Append($"{ft.Feature.DisplayName()}<br>");
            }

            _extentReports.AddSystemInfo("Enabled Features", enabledFeatures.ToString());
        }

        // This block will be either deleted or updated once the threading is finalised
        [AfterTestRun]
        public static void AfterTestRun()
        {
            _extentReports.Flush();
        }

        [AfterFeature]
        public static void AfterFeature(FeatureContext featureContext)
        {
            {
                _featureTimer.Stop();
                TimeSpan ts = _featureTimer.Elapsed;
                var featureTitle = featureContext.FeatureInfo.Title;
                var executionTime = ts.ToString(@"dd\.hh\:mm\:ss");

                _extentReports.AddTestRunnerLogs($"Execution time for {featureTitle} {executionTime}<br>");
                _featureTimer.Reset();
            }
        }
        
        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            var featureTitle = featureContext.FeatureInfo.Title;
            var feature = _extentReports.CreateTest<Feature>(featureTitle);
            featureContext.Add(featureTitle, feature);

            if (featureContext.FeatureInfo.Tags.Contains("WorkFlowTest"))
            {
                var workflowTestData = ReadTestDataFromJson<WorkflowTestData>($"{featureTitle}.json");
                featureContext.Add(ContextStrings.WorkflowTestData, workflowTestData);

                featureTitle = featureTitle.Replace("Workflow", "Setup");
            }

            if (featureContext.FeatureInfo.Tags.Contains("Performance"))
            {
                var performanceTestData = ReadTestDataFromJson<WorkflowTestData>($"{featureTitle}.json");
                featureContext.Add(ContextStrings.PerformanceTestData, performanceTestData);
            }

            var agencySetupData = ReadTestDataFromJson<AgencyInfrastructureData>($"{featureTitle}.json");
            featureContext.Add(ContextStrings.AgencySetupData, agencySetupData);

            var environmentScope = (EnvironmentScope)Enum.Parse(typeof(EnvironmentScope), ConfigurationManager.AppSettings["EnvironmentScope"]);
            var genericSetupData = ReadTestDataFromJson<GenericInfrastructureData>($"Generic_Setup.json") as GenericInfrastructureData;
            genericSetupData.SymphonyAdminData.FeatureToggles = genericSetupData.SymphonyAdminData.FeatureToggles
                .Where(ft => ft.EnvironmentScopes.Contains(environmentScope)).ToList();

            featureContext.Add(ContextStrings.GenericSetupData, genericSetupData);
            _featureTimer.Start();
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            if (_featureContext.FeatureInfo.Tags.Contains("GermanCulture"))
            {
                Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("de-AT");
            }

            var browserType = ConfigurationManager.AppSettings["Browser"];
            var environmentUrl = ConfigurationManager.AppSettings["EnvironmentUrl"];
            var isHeadless = Convert.ToBoolean(ConfigurationManager.AppSettings["Headless"]);

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get("Browser")))
            {
                browserType = TestContext.Parameters.Get("Browser");
            }

            if (!string.IsNullOrWhiteSpace(TestContext.Parameters.Get("EnvironmentUrl")))
            {
                environmentUrl = TestContext.Parameters.Get("Environment");
            }

            var browser = (BrowserType)Enum.Parse(typeof(BrowserType), browserType);
            var isRemoteWebDriver = Convert.ToBoolean(ConfigurationManager.AppSettings["IsRemoteWebDriver"]);
            var hubHostProtocol = ConfigurationManager.AppSettings["HubHostProtocol"];
            var hubHost = ConfigurationManager.AppSettings["HubHost"];
            var hubPort = ConfigurationManager.AppSettings["HubPort"];

            switch (browser)
            {
                case BrowserType.Chrome:
                    var downloadsFolderPath = FileHelper.GetDownloadsFolderPath();
                    var options = new ChromeOptions();
                    options.AddArgument("start-maximized");
                    options.AddArguments("--disable-extensions");
                    options.AddArguments("--disable-infobars");

                    if (isHeadless)
                    {
                        options.AddArguments("--disable-gpu");
                        options.AddArguments("--headless");
                        options.AddArguments("--window-size=1366,768");
                    }
                    
                    options.AddUserProfilePreference("download.default_directory", downloadsFolderPath);
                    options.AddUserProfilePreference("download.prompt_for_download", false);
                    options.AddUserProfilePreference("download.directory_upgrade", true);
                    options.AddUserProfilePreference("safebrowsing.enabled", true);
                    options.AddUserProfilePreference("credentials_enable_service", false);

                    if (!isRemoteWebDriver)
                    {
                        _driver = new ChromeDriver(options);

                        if (isHeadless)
                        {
                            var settingForHeadlessDownloads = new Dictionary<string, object>();
                            settingForHeadlessDownloads["behavior"] = "allow";
                            settingForHeadlessDownloads["downloadPath"] = FileHelper.GetDownloadsFolderPath();
                            ((ChromeDriver)_driver).ExecuteChromeCommand("Page.setDownloadBehavior", settingForHeadlessDownloads);
                        }
                    }
                    else
                    {
                        _driver = new RemoteWebDriver(new Uri($"{hubHostProtocol}://{hubHost}:{hubPort}/wd/hub"), options.ToCapabilities());
                        var allowsDetection = _driver as IAllowsFileDetection;

                        if (allowsDetection != null)
                        {
                            allowsDetection.FileDetector = new LocalFileDetector();
                        }
                    }
                    break;
                case BrowserType.Firefox:
                    _driver = new FirefoxDriver();
                    break;
                case BrowserType.InternetExplorer:
                    _driver = new InternetExplorerDriver();
                    break;
                default:
                    throw new Exception("The browser type: " + browser + " is currently not supported.");
            }

            _objectConatiner.RegisterInstanceAs(_driver);

            var implicitWaitTimeout = double.Parse(ConfigurationManager.AppSettings["ImplicitWaitTimeout"]);
            var pageLoadTimeout = double.Parse(ConfigurationManager.AppSettings["PageLoadTimeout"]);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(implicitWaitTimeout);
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(pageLoadTimeout);
            _driver.Manage().Window.Maximize();
            _driver.Navigate().GoToUrl(environmentUrl);

            var feature = _featureContext[_featureContext.FeatureInfo.Title] as ExtentTest;
            _scenario = feature.CreateNode<Scenario>(_scenarioContext.ScenarioInfo.Title);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver.Close();
            _driver.Quit();
        }

        [AfterStep]
        public void AfterStep()
        {
            ExtentTest extentTestNode;
            switch (_scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString())
            {
                case "Given":
                    extentTestNode = _scenario.CreateNode<Given>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                case "When":
                    extentTestNode = _scenario.CreateNode<When>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                case "Then":
                    extentTestNode = _scenario.CreateNode<Then>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                case "And":
                    extentTestNode = _scenario.CreateNode<And>(_scenarioContext.StepContext.StepInfo.Text);
                    break;
                default:
                    throw new Exception("Failed to created a new extentTestNode");
            }

            if (_scenarioContext.TestError != null)
            {
                extentTestNode.Fail(_scenarioContext.TestError.Message);
                var screenshotPath = Capture(_driver, "fail " + DateTime.Now.ToString("yyyyMMddTHHmmss"));
                _scenario.Fail("Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build()).AddScreenCaptureFromPath(screenshotPath);
            }

            if (_scenarioContext.ScenarioExecutionStatus.ToString() == "StepDefinitionPending")
            {
                extentTestNode.Skip("Step Definition Pending");
            }
        }

        private static object ReadTestDataFromJson<T>(string testDataFileName)
        {
            var workingDirectory = FileHelper.GetWorkingDirectoryPath();
            var dataFilePath = string.Format(Path.Combine(workingDirectory, "DataFiles", testDataFileName));
            return JsonConvert.DeserializeObject(File.ReadAllText(dataFilePath), typeof(T));
        }

        private static string Capture(IWebDriver driver, string screenShotName)
        {
            var ts = (ITakesScreenshot)driver;
            var screenshot = ts.GetScreenshot();
            var workingDirectory = FileHelper.GetWorkingDirectoryPath();
            var todaysdate = DateTime.Now.ToString("ddMMMyyyy");
            var errorScreenshotsFolder = string.Format(Path.Combine(workingDirectory, @"Reports\{0}\ErrorScreenshots"), todaysdate);
            var screenshotFilepath = Path.Combine(errorScreenshotsFolder, $"{screenShotName}.png");
            var relativepath = $@".\ErrorScreenshots\{screenShotName}.png";

            if (!Directory.Exists(errorScreenshotsFolder))
            {
                Directory.CreateDirectory(errorScreenshotsFolder);
            }
            
            screenshot.SaveAsFile(screenshotFilepath, ScreenshotImageFormat.Png);

            return relativepath;
        }
    }
}