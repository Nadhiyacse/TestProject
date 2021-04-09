﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (http://www.specflow.org/).
//      SpecFlow Version:2.3.2.0
//      SpecFlow Generator Version:2.3.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Automation_Framework._1_Features.InfrastructureTests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("GM_Turkey_Setup")]
    public partial class GM_Turkey_SetupFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GM_Turkey_Setup.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GM_Turkey_Setup", null, ProgrammingLanguage.CSharp, ((string[])(null)));
            testRunner.OnFeatureStart(featureInfo);
        }
        
        [NUnit.Framework.OneTimeTearDownAttribute()]
        public virtual void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        [NUnit.Framework.SetUpAttribute()]
        public virtual void TestInitialize()
        {
        }
        
        [NUnit.Framework.TearDownAttribute()]
        public virtual void ScenarioTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public virtual void ScenarioSetup(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioStart(scenarioInfo);
        }
        
        public virtual void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("Create agency in symphony admin")]
        [NUnit.Framework.CategoryAttribute("ConfigureUserAndAgency")]
        [NUnit.Framework.CategoryAttribute("GenericDataSetup")]
        public virtual void CreateAgencyInSymphonyAdmin()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Create agency in symphony admin", new string[] {
                        "ConfigureUserAndAgency",
                        "GenericDataSetup"});
#line 4
this.ScenarioSetup(scenarioInfo);
#line 5
    testRunner.Given("I have logged in as a symphony admin user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 6
    testRunner.And("I navigate to the Symphony Admin page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 7
    testRunner.And("I navigate to the Symphony Admin Agencies page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 8
    testRunner.And("I configure my agencies", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 9
    testRunner.And("I configure my agencies access control", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 10
    testRunner.And("I configure my agencies users", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 11
    testRunner.And("I configure my agencies feature control", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 12
    testRunner.And("I configure my agencies custom labels", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 13
    testRunner.And("I configure my agencies custom fields", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
    testRunner.And("I configure my agencies classifications", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("01 Setup agency administration")]
        [NUnit.Framework.CategoryAttribute("ConfigureAdministrator")]
        [NUnit.Framework.CategoryAttribute("AgencyAdminDataSetup")]
        public virtual void _01SetupAgencyAdministration()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("01 Setup agency administration", new string[] {
                        "ConfigureAdministrator",
                        "AgencyAdminDataSetup"});
#line 17
this.ScenarioSetup(scenarioInfo);
#line 18
    testRunner.Given("I have logged in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 19
    testRunner.And("I navigate to the Administrator page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 20
    testRunner.And("I configure my default agency fees", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 21
    testRunner.And("I configure my agency default cost adjustments", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
    testRunner.And("I navigate to the Administrator Clients page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
    testRunner.And("I configure my clients", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 24
    testRunner.And("I navigate to the Administrator Access page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 25
    testRunner.And("I configure my agencies access control as Administrator", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("02 Setup Integration Data Mapping")]
        [NUnit.Framework.CategoryAttribute("DataMapping")]
        [NUnit.Framework.CategoryAttribute("AgencyAdminDataSetup")]
        public virtual void _02SetupIntegrationDataMapping()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("02 Setup Integration Data Mapping", new string[] {
                        "DataMapping",
                        "AgencyAdminDataSetup"});
#line 28
this.ScenarioSetup(scenarioInfo);
#line 29
    testRunner.Given("I have logged in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 30
    testRunner.And("I navigate to the Integration page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 31
    testRunner.And("I navigate to the Data Mapping page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 32
    testRunner.And("I configure my external applications data mapping", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("03 Setup Integration Ext Accounts")]
        [NUnit.Framework.CategoryAttribute("ExtAccounts")]
        [NUnit.Framework.CategoryAttribute("AgencyAdminDataSetup")]
        public virtual void _03SetupIntegrationExtAccounts()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("03 Setup Integration Ext Accounts", new string[] {
                        "ExtAccounts",
                        "AgencyAdminDataSetup"});
#line 35
this.ScenarioSetup(scenarioInfo);
#line 36
    testRunner.Given("I have logged in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 37
    testRunner.And("I navigate to the Integration page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 38
    testRunner.And("I navigate to the Ext Accounts page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 39
    testRunner.And("I configure my agencies external credentials", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

