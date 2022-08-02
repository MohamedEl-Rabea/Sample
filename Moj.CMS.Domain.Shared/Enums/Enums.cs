namespace Moj.CMS.Domain.Shared.Enums
{
    public enum IbanPurposeEnum
    {
        SadadCollector = 1,
        Aggregator,
        Court,
        Party,
    }
    public enum FinancialTransactionTypeEnum
    {
        PaySadadInvoice = 1,
    }
    public enum VIbanReferenceTypeEnum
    {
        Case = 1,
        Party,
        Sadad
    }

    //TODO: Add in lookupSeeder & TestLookupSeeder
    public enum IntegrationRequestStatusEnum
    {
        Scheduled = 1,
        Processing,
        Successed,
        Failed
    }

    public enum CaseStatusEnum
    {
        Active = 1,
        InProgress,
        Closed
    }

    public enum CaseOperationEnum
    {
        CreateCase = 1,
        TerminateCase,
        CreateVIban,
        CreateSadadInvoice,
        AddPromissory,
        AddClaim,
        EditClaim,
        AddAccusedPartiesToClaim,
        AddParty,
        ChangeCourtDetails,
        CloseClaim,
        ActivateCase
    }

    public enum PromissoryTypeEnum
    {
        CourtOrder = 1,
        RulesExecutionOrder,
        ReconciliationReport,
        FinancialPaper,
        Contract,
        ForeignCourtOrder,
        Quasi_Judicial,// شبه قضائي
        StandAlonePaper,
    }

    public enum CaseTypeEnum
    {
        Financial = 1,
        Personal,
        Direct,
    }

    public enum PartyFinancialTypeEnum
    {
        Complaint_Credited = 1,
        Accused_Debited
    }

    public enum PartyClassificationEnum
    {
        Requester = 1,
        Respondent
    }

    public enum PartyRoleEnum
    {
        OriginalCreditor = 1,
        OriginalDebtor,
        CreditorLawyer,
        CreditorAgent,
        CreditorCounselor,
        CreditorRepresentative,
        CreditorCustodian,
        Applicant,
        Debtor,
        Creditor,
        ServiceProvider
    }

    public enum PartyLocationEnum
    {
        InsideSaudi = 1,
        InsideGulf,
        OutsideSaudi,
    }

    public enum PartyStatusEnum
    {
        Unable = 1,
        Deceased,
        Able,
    }

    public enum PartyIdentityTypeEnum
    {
        SaudiNationalId = 1,
        ResidencyId,
        TemporaryId,
        Passport,
        GulfNational,
        BorderNumber,
    }

    public enum PartyTypeEnum
    {
        Individual = 1,
        Company,
        Institution,
        GovernmentAgency,
        ClubLicense,
        ForeignCompany_Institution,
        CommerceChamber,
        CivilOrganization,
        CivilInstitution,
        ProfessionalCompany,
        LawOffice,
    }

    public enum RequestTerminationReasonsEnum
    {
        ReconciliationReport = 1,
        TotalWaiver,
        CancelAction
    }

    public enum Gender
    {
        Unkown = 0,
        Male,
        Female
    }

    public enum DebtTypeEnum
    {
        Preferential = 1,
        Alimony,
        Underadge,
        Expense
    }

    public enum ClaimFinancialStatusEnum
    {
        NotPaid = 1,
        PartiallyPaid,
        FullyPaid,
    }

    public enum ClaimStatusEnum
    {
        Active = 1,
        Closed,
        Finished
    }

    public enum FinancialEffectTypeEnum
    {
        WaiverRecord = 1,
        ServiceProvider,
        NewspaperAdvertisement
    }

    public enum ClaimTerminationReasonEnum
    {
    }

    public enum ClaimCloseReasonEnum
    {
    }
}
