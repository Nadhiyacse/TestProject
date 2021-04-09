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
namespace Automation_Framework._1_Features.WorkflowTests
{
    using TechTalk.SpecFlow;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "2.3.2.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [NUnit.Framework.TestFixtureAttribute()]
    [NUnit.Framework.DescriptionAttribute("GM_Belgium_Workflow")]
    [NUnit.Framework.CategoryAttribute("WorkFlowTest")]
    public partial class GM_Belgium_WorkflowFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GM_Belgium_Workflow.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GM_Belgium_Workflow", null, ProgrammingLanguage.CSharp, new string[] {
                        "WorkFlowTest"});
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
        
        public virtual void FeatureBackground()
        {
#line 4
#line 5
    testRunner.Given("I have logged in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line hidden
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE01 Create the campaign")]
        public virtual void BE01CreateTheCampaign()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE01 Create the campaign", ((string[])(null)));
#line 7
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 8
    testRunner.When("I create a campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 9
    testRunner.Then("The campaign should be created successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE02 Adding the cost items")]
        public virtual void BE02AddingTheCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE02 Adding the cost items", ((string[])(null)));
#line 11
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 12
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 13
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 14
    testRunner.And("I create all single placements from test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 15
    testRunner.And("I create all performance packages from test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 16
    testRunner.And("I create all sponsorship packages from test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 17
    testRunner.Then("The cost items should be present in the media schedule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE03 Download import template")]
        public virtual void BE03DownloadImportTemplate()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE03 Download import template", ((string[])(null)));
#line 19
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 20
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
    testRunner.And("I download the import template", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
    testRunner.Then("the import template should be downloaded", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE04 Import cost items from file")]
        public virtual void BE04ImportCostItemsFromFile()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE04 Import cost items from file", ((string[])(null)));
#line 25
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 26
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
    testRunner.And("I import the media schedule items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
    testRunner.Then("The imported cost items should be present in the media schedule", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE05 Add Non Media Cost items")]
        public virtual void BE05AddNonMediaCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE05 Add Non Media Cost items", ((string[])(null)));
#line 31
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 32
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
    testRunner.And("I add non media cost items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
    testRunner.Then("the non media cost items are added successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE06 Confirm all cost items")]
        public virtual void BE06ConfirmAllCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE06 Confirm all cost items", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 38
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
    testRunner.And("I confirm all items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
    testRunner.Then("All the items should have status confirmed", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE07 Billing")]
        public virtual void BE07Billing()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE07 Billing", ((string[])(null)));
#line 43
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 44
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
    testRunner.And("I navigate to the Billing page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
    testRunner.And("I open the Custom Billing page for the first publisher displayed on the UI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
    testRunner.Then("The totals values per item are as per test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE08 Issuing insertion order")]
        public virtual void BE08IssuingInsertionOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE08 Issuing insertion order", ((string[])(null)));
#line 49
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 50
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 51
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 52
    testRunner.And("I create insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 53
    testRunner.And("I issue the insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
    testRunner.Then("The insertion order status should be \'Issued (V1)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE09 Insertion order publisher sign off")]
        public virtual void BE09InsertionOrderPublisherSignOff()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE09 Insertion order publisher sign off", ((string[])(null)));
#line 57
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 58
    testRunner.Given("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 59
    testRunner.And("I have logged in as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
    testRunner.And("I select my campaign as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
    testRunner.When("I sign off the insertion order as Publisher", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 63
    testRunner.And("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 64
    testRunner.And("I log in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 65
    testRunner.And("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 66
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 67
    testRunner.Then("The insertion order status should be \'Part Signed (V1)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE10 Insertion order agency sign off")]
        public virtual void BE10InsertionOrderAgencySignOff()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE10 Insertion order agency sign off", ((string[])(null)));
#line 69
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 70
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 71
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 72
    testRunner.And("I sign off the insertion order as Agency", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 73
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 74
    testRunner.Then("The insertion order status should be \'datesignedoff (V1)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE11 Trafficking")]
        public virtual void BE11Trafficking()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE11 Trafficking", ((string[])(null)));
#line 76
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 77
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 78
    testRunner.And("I navigate to the Traffic page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 79
    testRunner.Then("Excluded cost items are not visible", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 80
    testRunner.When("I traffic all cost items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 81
    testRunner.Then("All cost items should be trafficked successfully", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE12 Export cost items")]
        public virtual void BE12ExportCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE12 Export cost items", ((string[])(null)));
#line 83
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 84
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 85
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
    testRunner.And("I export the media schedule export \'GroupM (BE) Production Schedule Export\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 88
    testRunner.When("I export the media schedule export \'GroupM (BE) Media Plan Export\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 89
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 90
    testRunner.When("I export the media schedule export \'GroupM (BE) Trafficking Sheet\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 92
    testRunner.When("I export the media schedule export \'GroupM (BE) Campaign Report\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 93
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 94
    testRunner.When("I export the media schedule export \'GroupM (BE) Technical Specifications Export\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 95
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE13 Export Insertion Order")]
        public virtual void BE13ExportInsertionOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE13 Export Insertion Order", ((string[])(null)));
#line 97
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 98
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 99
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 100
    testRunner.And("I click on last signed off link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
    testRunner.And("I export the insertion order export \'IO PDF Export\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 102
    testRunner.Then("the insertion order export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 103
    testRunner.When("I export the insertion order export \'IO PDF Export (Prorata Monthly Allocations)\'" +
                    "", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 104
    testRunner.Then("the insertion order export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 105
    testRunner.When("I export the insertion order export \'IO PDF Export (Prorata Monthly Allocations w" +
                    "ith Site Breakdown)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 106
    testRunner.Then("the insertion order export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE14 Edit Non Media Cost items")]
        public virtual void BE14EditNonMediaCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE14 Edit Non Media Cost items", ((string[])(null)));
#line 108
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 109
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 110
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 111
    testRunner.And("I edit Non Media cost based on test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 112
    testRunner.Then("The version of Non Media cost is incremented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 113
    testRunner.When("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 114
    testRunner.And("I edit Non Media cost based without making any changes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 115
    testRunner.Then("The version of Non Media cost is not incremented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE15 Verify Cost tab of Non Media Cost items")]
        public virtual void BE15VerifyCostTabOfNonMediaCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE15 Verify Cost tab of Non Media Cost items", ((string[])(null)));
#line 117
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 118
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 119
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 120
    testRunner.And("I open the first non media cost item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 121
    testRunner.And("I switch to Costs tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 122
    testRunner.And("I set cost adjustment values", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 123
    testRunner.Then("the cost summaries in cost tab are as expected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 124
    testRunner.And("I save the cost item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 126
    testRunner.When("I open the first non media cost item", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 127
    testRunner.And("I switch to Costs tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 128
    testRunner.Then("the cost summaries in cost tab are as expected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE16 Cancel Non Media Cost items")]
        public virtual void BE16CancelNonMediaCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE16 Cancel Non Media Cost items", ((string[])(null)));
#line 130
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 131
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 132
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 133
    testRunner.And("I cancel Non Media costs based on test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 134
    testRunner.Then("Non Media costs were cancelled", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("BE17 VerifyForecastUnitcost")]
        public virtual void BE17VerifyForecastUnitcost()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("BE17 VerifyForecastUnitcost", ((string[])(null)));
#line 136
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 137
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 138
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 139
    testRunner.And("I open the first Single Placement", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 140
    testRunner.And("I switch to Forecast tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 141
    testRunner.Then("the values of estimate fields in forecast tab is as expected", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion
