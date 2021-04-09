using Automation_Framework.Helpers;

namespace Automation_Framework._3._Pages.Symphony.Common.Enums
{
    // Copied from Symphony.sln Src\CrossCutting\External\FeatureToggling\Feature.cs
    public enum FeatureToggle
    {
        [FeatureDisplayName(Value = "GM-4182 Check Agency Mapping when Trafficking to AdMonitor")]
        CheckAgencyMappingWhenTraffickingToAdMonitor,
        [FeatureDisplayName(Value = "GMREQ-321 - CN - IO: IO Format")]
        CNIOPDFExportApprovedV2,
        [FeatureDisplayName(Value = "GMREQ-845 - Google Ads Integration with Symphony")]
        GoogleAdwordsSunset,
        [FeatureDisplayName(Value = "GMREQ-908: Prevent brand change after IO creation")]
        PreventBrandChangeAfterIOCreation,
        [FeatureDisplayName(Value = "SYM-14821 - IO: Grid Landing Page")]
        InsertionOrderGridLandingPage,
        [FeatureDisplayName(Value = "GMREQ-993: RSA Cost Model - Editable Client side Discounts")]
        GmRsaV2CostModel,
        [FeatureDisplayName(Value = "GMREQ-998: OC BE - Custom Data for BE Media Plan Export")]
        FixedNonMediaCostItemBEMediaPlanExportData,
        [FeatureDisplayName(Value = "SYM-15844 - Shared Agency Fee Agreement")]
        SharedAgencyFeeAgreement,
        [FeatureDisplayName(Value = "NMC: Linked - CPU P1a - Add/Manage Linked Costs - CPM")]
        LinkedNonMediaPhase1CostPerUnit,
        [FeatureDisplayName(Value = "GMREQ - 1017: Forecast Unit Cost")]
        ForecastUnitCost,
        [FeatureDisplayName(Value = "GMREQ-985 - NZ Adding CTC column and monthly breakdown")]
        NzExportUpdate,
        [FeatureDisplayName(Value = "SYM-16280: Insertion Order External Rejection")]
        InsertionOrderExternalRejection,
        [FeatureDisplayName(Value = "SYM-16329: Grid: Bulk Duplicate")]
        GridBulkDuplicate,
        [FeatureDisplayName(Value = "GMREQ-1007: GroupM Belgium - Production Schedule Changes")]
        BEProdScheduleChanges,
        [FeatureDisplayName(Value = "GMREQ-1003: AT PNC")]
        ATPNCSymphonyAdmin,
    }
}
