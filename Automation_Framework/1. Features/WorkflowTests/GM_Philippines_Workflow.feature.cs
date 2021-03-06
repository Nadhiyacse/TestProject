// ------------------------------------------------------------------------------
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
    [NUnit.Framework.DescriptionAttribute("GM_Philippines_Workflow")]
    [NUnit.Framework.CategoryAttribute("WorkFlowTest")]
    public partial class GM_Philippines_WorkflowFeature
    {
        
        private TechTalk.SpecFlow.ITestRunner testRunner;
        
#line 1 "GM_Philippines_Workflow.feature"
#line hidden
        
        [NUnit.Framework.OneTimeSetUpAttribute()]
        public virtual void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("en-US"), "GM_Philippines_Workflow", null, ProgrammingLanguage.CSharp, new string[] {
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
        [NUnit.Framework.DescriptionAttribute("PH01 Creating the campaign")]
        public virtual void PH01CreatingTheCampaign()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH01 Creating the campaign", ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("PH02 Adding the cost items")]
        public virtual void PH02AddingTheCostItems()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH02 Adding the cost items", ((string[])(null)));
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
        [NUnit.Framework.DescriptionAttribute("PH03 Cost Item Version History")]
        public virtual void PH03CostItemVersionHistory()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH03 Cost Item Version History", ((string[])(null)));
#line 19
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 20
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 21
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 22
    testRunner.And("I open the version history of the first single placement", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 23
    testRunner.Then("the datetime of the 1st row should be based on the agency time zone", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH04 Version not increment for single placement edit")]
        public virtual void PH04VersionNotIncrementForSinglePlacementEdit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH04 Version not increment for single placement edit", ((string[])(null)));
#line 25
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 26
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 27
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 28
    testRunner.And("I edit the first single placement from test data without making any changes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 29
    testRunner.Then("The version for the first single placement from test data is not incremented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH05 Version not increment for performance package edit")]
        public virtual void PH05VersionNotIncrementForPerformancePackageEdit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH05 Version not increment for performance package edit", ((string[])(null)));
#line 31
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 32
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 33
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 34
    testRunner.And("I edit the first performance package from test data without making any changes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 35
    testRunner.Then("The version for the first performance package from test data is not incremented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH06 Version not increment for sponsorship package edit")]
        public virtual void PH06VersionNotIncrementForSponsorshipPackageEdit()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH06 Version not increment for sponsorship package edit", ((string[])(null)));
#line 37
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 38
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 39
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 40
    testRunner.And("I edit the first placement inside the first sponsorship package from test data wi" +
                    "thout making any changes", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 41
    testRunner.Then("The version for the first sponsorship package from test data is not incremented", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH09 Submit for approval")]
        public virtual void PH09SubmitForApproval()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH09 Submit for approval", ((string[])(null)));
#line 43
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 44
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 45
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 46
    testRunner.And("I confirm all items", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 47
    testRunner.And("I submit for approval", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 48
    testRunner.Then("The export is downloaded", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 49
    testRunner.And("The approval status is Pending Approval", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH10 Campaign approval")]
        public virtual void PH10CampaignApproval()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH10 Campaign approval", ((string[])(null)));
#line 51
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 52
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 53
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 54
    testRunner.And("I approve the items as Agency Approved on behalf of Client", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 55
    testRunner.Then("The approval status is Approved", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH11 Issuing insertion order")]
        public virtual void PH11IssuingInsertionOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH11 Issuing insertion order", ((string[])(null)));
#line 57
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 58
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 59
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 60
    testRunner.And("I create insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 61
    testRunner.And("I issue the insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 62
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 63
    testRunner.Then("The insertion order status should be \'Issued (V1)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH12 Recall RFP insertion order")]
        public virtual void PH12RecallRFPInsertionOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH12 Recall RFP insertion order", ((string[])(null)));
#line 65
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 66
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 67
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 68
    testRunner.And("I recall the insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 69
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 70
    testRunner.Then("The insertion order status should be \'Saved (V2)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH13 Subscribing publisher rejects IO")]
        public virtual void PH13SubscribingPublisherRejectsIO()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH13 Subscribing publisher rejects IO", ((string[])(null)));
#line 72
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 73
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 74
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 75
    testRunner.And("I click the Pending IO status link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 76
    testRunner.And("I issue the insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 78
    testRunner.Given("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 79
    testRunner.And("I have logged in as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 80
    testRunner.And("I select my campaign as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 81
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 82
    testRunner.When("I reject the insertion order as Publisher", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 83
    testRunner.And("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 84
    testRunner.And("I log in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 85
    testRunner.And("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 86
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 87
    testRunner.Then("The insertion order should no longer appear", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH14 Insertion order publisher sign off")]
        public virtual void PH14InsertionOrderPublisherSignOff()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH14 Insertion order publisher sign off", ((string[])(null)));
#line 89
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 90
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 91
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 92
    testRunner.And("I create insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 93
    testRunner.And("I issue the insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 95
    testRunner.Given("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 96
    testRunner.And("I have logged in as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 97
    testRunner.And("I select my campaign as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 98
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 99
    testRunner.When("I sign off the insertion order as Publisher", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 100
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 101
    testRunner.Then("The insertion order status should be \'Part Signed (V1)\' on manage multi IO page f" +
                    "or publisher", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH15 Agency rejects IO")]
        public virtual void PH15AgencyRejectsIO()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH15 Agency rejects IO", ((string[])(null)));
#line 103
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 104
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 105
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 106
    testRunner.And("I reject the insertion order as Agency", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 107
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 108
    testRunner.Then("The insertion order should no longer appear", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH16 Insertion order agency sign off")]
        public virtual void PH16InsertionOrderAgencySignOff()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH16 Insertion order agency sign off", ((string[])(null)));
#line 110
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 111
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 112
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 113
    testRunner.And("I create insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 114
    testRunner.And("I issue the insertion order", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 116
    testRunner.Given("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 117
    testRunner.And("I have logged in as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 118
    testRunner.And("I select my campaign as a publisher user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 119
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 120
    testRunner.When("I sign off the insertion order as Publisher", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 122
    testRunner.Given("I have logged out the current user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 123
    testRunner.And("I have logged in as an agency user", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 124
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 125
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 126
    testRunner.And("I sign off the insertion order as Agency", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 127
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 128
    testRunner.Then("The insertion order status should be \'DateSignedOff (V1)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH17 Publishers have correct data on Billing Landing Page")]
        public virtual void PH17PublishersHaveCorrectDataOnBillingLandingPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH17 Publishers have correct data on Billing Landing Page", ((string[])(null)));
#line 130
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 131
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 132
    testRunner.And("I navigate to the Billing page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 133
    testRunner.And("I select Buy (Multiple) from the currency dropdown on the Billing Landing page fo" +
                    "r the country Philippines", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 134
    testRunner.Then("The values per publisher should be based on test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 135
    testRunner.When("I select Base from the currency dropdown on the Billing Landing page for the coun" +
                    "try Philippines", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 136
    testRunner.Then("The values per publisher should be based on test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH18 Publisher has correct data on Custom Billing page")]
        public virtual void PH18PublisherHasCorrectDataOnCustomBillingPage()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH18 Publisher has correct data on Custom Billing page", ((string[])(null)));
#line 138
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 139
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 140
    testRunner.And("I navigate to the Billing page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 141
    testRunner.And("I open the Custom Billing page for the first publisher displayed on the UI", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 142
    testRunner.And("I select Buy (Multiple) from the currency dropdown on the Custom Billing page for" +
                    " the country Philippines", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 143
    testRunner.And("I customise my billing values based on test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 144
    testRunner.Then("The totals values per item are as per test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 145
    testRunner.When("I select Base from the currency dropdown on the Custom Billing page for the count" +
                    "ry Philippines", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 146
    testRunner.Then("The totals values per item are as per test data", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH19 Trafficking")]
        public virtual void PH19Trafficking()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH19 Trafficking", ((string[])(null)));
#line 148
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 149
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 150
    testRunner.And("I navigate to the Traffic page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 151
    testRunner.Then("I traffic all cost items for all AdServers", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH21 Media schedules exports")]
        public virtual void PH21MediaSchedulesExports()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH21 Media schedules exports", ((string[])(null)));
#line 161
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 162
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 163
    testRunner.And("I navigate to the Media Schedule page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 164
    testRunner.And("I export the media schedule export \'GroupM PH Cost Summary Export\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 165
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 166
    testRunner.When("I export the media schedule export \'GroupM PH Cost Estimate Export (Fee Basis)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 167
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line 168
    testRunner.When("I export the media schedule export \'GroupM PH Cost Estimate Export (Comm Basis)\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 169
    testRunner.Then("The media schedule export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH22 Export Insertion Order")]
        public virtual void PH22ExportInsertionOrder()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH22 Export Insertion Order", ((string[])(null)));
#line 171
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 172
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 173
    testRunner.And("I navigate to the Insertion Order page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 174
    testRunner.And("I click on last signed off link", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 175
    testRunner.And("I export the insertion order export \'GM Philippines Booking Order\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 176
    testRunner.Then("the insertion order export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH23 Export Billing")]
        public virtual void PH23ExportBilling()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH23 Export Billing", ((string[])(null)));
#line 178
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 179
    testRunner.When("I select my campaign", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "When ");
#line 180
    testRunner.And("I navigate to the Billing page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 181
    testRunner.And("I export the Billing export \'GroupM Philippines BMD Export V1\' with delivery meth" +
                    "od as \'Download\'", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 182
    testRunner.Then("the Billing export should be exported", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Then ");
#line hidden
            this.ScenarioCleanup();
        }
        
        [NUnit.Framework.TestAttribute()]
        [NUnit.Framework.DescriptionAttribute("PH24 Export Classification Filter")]
        public virtual void PH24ExportClassificationFilter()
        {
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("PH24 Export Classification Filter", ((string[])(null)));
#line 184
this.ScenarioSetup(scenarioInfo);
#line 4
this.FeatureBackground();
#line 185
    testRunner.Given("I navigate to the Administrator page", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Given ");
#line 186
    testRunner.And("I navigate to Classification Filter tab", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line 187
    testRunner.And("I export my classification filters", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "And ");
#line hidden
            this.ScenarioCleanup();
        }
    }
}
#pragma warning restore
#endregion

