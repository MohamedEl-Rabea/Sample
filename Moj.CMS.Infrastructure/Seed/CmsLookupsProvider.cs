using Moj.CMS.Domain.Shared.Enums;
using Moj.CMS.Domain.Shared.LookupModels;
using System.Collections.Generic;

namespace Moj.CMS.Infrastructure.Seed
{
    public class CmsLookupsProvider
    {
        public static List<IbanPurpose> IbanPurposeList = new()
        {
            new IbanPurpose{Id=((int) IbanPurposeEnum.SadadCollector), Name="سداد التحصيلى"},
            new IbanPurpose{Id=((int) IbanPurposeEnum.Party), Name="طرف تنفيذى"},
            new IbanPurpose{Id=((int) IbanPurposeEnum.Court), Name="محكمة"},
            new IbanPurpose{Id=((int) IbanPurposeEnum.Aggregator), Name="التجميعى"},
        };
        public static List<VIbanReferenceType> VIbanReferenceTypeList = new List<VIbanReferenceType>
        {
            new VIbanReferenceType {Id = ((int) VIbanReferenceTypeEnum.Case), Name = "طلب تنفيذ"},
            new VIbanReferenceType {Id = ((int) VIbanReferenceTypeEnum.Party), Name = "طالب تنفيذ"},
            new VIbanReferenceType {Id = ((int) VIbanReferenceTypeEnum.Sadad), Name = "فاتورة سداد"}
        };

        public static List<IntegrationRequestStatus> IntegrationRequestStatusList = new List<IntegrationRequestStatus>
        {
            new IntegrationRequestStatus {Id = ((int) IntegrationRequestStatusEnum.Scheduled), Name = "مجدول"},
            new IntegrationRequestStatus {Id = ((int) IntegrationRequestStatusEnum.Processing), Name = "قيد التنفيذ"},
            new IntegrationRequestStatus {Id = ((int) IntegrationRequestStatusEnum.Successed), Name = "تم بنجاح"},
            new IntegrationRequestStatus {Id = ((int) IntegrationRequestStatusEnum.Failed), Name = "فشل"}
        };

        public static List<CaseStatus> CaseStatusList = new List<CaseStatus>
        {
           new CaseStatus{Id=((int)CaseStatusEnum.Active), Name = "نشطة"},
           new CaseStatus{Id=((int)CaseStatusEnum.InProgress), Name = "قيد التنفيذ"},
           new CaseStatus{Id=((int)CaseStatusEnum.Closed), Name = "منتهية"},

        };

        public static List<PromissoryType> PromissoryTypeList = new List<PromissoryType>
        {
            new PromissoryType{ Id=((int)PromissoryTypeEnum.CourtOrder), Name="قرار", Description = "الأحكام، والقرارات، والأوامر الصادرة من المحاكم" },
             new PromissoryType{ Id=((int)PromissoryTypeEnum.RulesExecutionOrder), Name="احكام",  Description = "احكام المحكمين مذيله بامر التنفيذ" },
             new PromissoryType{ Id=((int)PromissoryTypeEnum.ReconciliationReport), Name="محاضر",  Description = "محاضر الصلح التي تصدرها الجهات المخوله بذلك" },
             new PromissoryType{ Id=((int)PromissoryTypeEnum.FinancialPaper), Name="أوراق نافذة",  Description = "الأوراق التجارية النافذة - شيك، سند لأمر" },
             new PromissoryType{ Id=((int)PromissoryTypeEnum.Contract), Name="عقد ايجار",  Description = "العقود والمحررات الموثقة - عقد ايجار موحد"},
             new PromissoryType{ Id=((int)PromissoryTypeEnum.ForeignCourtOrder), Name="احكام اجنبية",  Description = "لأحكام، والأوامر القضائية، وأحكام المحكمين، والمحررات الموثقة الصادرة في بلد أجنبى"},
             new PromissoryType{ Id=((int)PromissoryTypeEnum.Quasi_Judicial), Name="أوراق موقعة",  Description = "الأوراق العادية التي يقر باستحقاق محتواها كلياً، أو جزئياً - أوراق عادية موقعة او مختومة و عليها بصمة، عقود و أوراق أخرى، الاحكام و القرارات الصادرة من اللجان شبه القضائية"},
             new PromissoryType{ Id=((int)PromissoryTypeEnum.StandAlonePaper), Name="أوراق نافذة بحالات خاصة",  Description = "العقود والأوراق الأخرى التي لها قوة سند التنفيذ بموجب نظام - تنفيذ شيك (بحالاتها الخاصة)، تنفيذ كمبيالة(بحالاتها الخاصة)، تنفيذ سند لأمر(بحالاتها الخاصة )" },
        };

        public static List<CaseType> CaseTypeList = new List<CaseType>
        {
            new CaseType{Id=((int)CaseTypeEnum.Financial), Name = "مالي"},
            new CaseType{Id=((int)CaseTypeEnum.Personal), Name= "احوال شخصيه"},
            new CaseType{Id=((int)CaseTypeEnum.Direct), Name = "مباشر"}
        };

        public static List<CaseOperation> CaseOperationList = new List<CaseOperation>
        {
            new CaseOperation{Id=((int)CaseOperationEnum.CreateCase), Name = "إضافة قضية"},
            new CaseOperation{Id=((int)CaseOperationEnum.AddPromissory), Name = "إضافة سند تنفيذى"},
            new CaseOperation{Id=((int)CaseOperationEnum.AddParty), Name = "إضافة مستفيد"},
            new CaseOperation{Id=((int)CaseOperationEnum.AddClaim), Name = "إضافة مطالبة مالية"},
            new CaseOperation{Id=((int)CaseOperationEnum.EditClaim), Name = "تعديل مطالبة مالية"},
            new CaseOperation{Id=((int)CaseOperationEnum.AddAccusedPartiesToClaim), Name = "إضافة مدينين للمطالبة المالية"},
            new CaseOperation{Id=((int)CaseOperationEnum.ActivateCase), Name = "تفعيل القضيه"}
        };

        public static List<PartyLocation> PartyLocationList = new List<PartyLocation>
        {
            new PartyLocation{Id=((int)PartyLocationEnum.InsideSaudi),Name="داخل السعوديه"},
            new PartyLocation{Id=((int)PartyLocationEnum.InsideGulf),Name="داخل دول مجلس التعاون"},
            new PartyLocation{Id=((int)PartyLocationEnum.OutsideSaudi),Name="خارج السعوديه"},
        };

        public static List<PartyIdentityType> PartyIdentityTypeList = new List<PartyIdentityType>
        {
            new PartyIdentityType{Id=((int)PartyIdentityTypeEnum.SaudiNationalId), Name="هوية وطنية سعودية" },
            new PartyIdentityType{Id=((int)PartyIdentityTypeEnum.ResidencyId), Name="هوية مقيم" },
            new PartyIdentityType{Id=((int)PartyIdentityTypeEnum.TemporaryId), Name="هوية مؤقته" },
            new PartyIdentityType{Id=((int)PartyIdentityTypeEnum.Passport), Name="جواز سفر" },
            new PartyIdentityType{Id=((int)PartyIdentityTypeEnum.GulfNational), Name="هوية وطنية خليجية" },
            new PartyIdentityType{Id=((int)PartyIdentityTypeEnum.BorderNumber), Name="رقم حدود" },
        };

        public static List<PartyType> PartyTypeList = new List<PartyType>
        {
            new PartyType{Id=((int)PartyTypeEnum.Individual),Name="فرد" },
            new PartyType{Id=((int)PartyTypeEnum.Company),Name="شركة" },
            new PartyType{Id=((int)PartyTypeEnum.Institution),Name="مؤسسة" },
            new PartyType{Id=((int)PartyTypeEnum.GovernmentAgency),Name="جهة حكومية" },
            new PartyType{Id=((int)PartyTypeEnum.ClubLicense),Name="ترخيص نادي" },
            new PartyType{Id=((int)PartyTypeEnum.ForeignCompany_Institution),Name="شركه او مؤسسة اجنبية" },
            new PartyType{Id=((int)PartyTypeEnum.CommerceChamber),Name="غرفة تجارة" },
            new PartyType{Id=((int)PartyTypeEnum.CivilOrganization),Name="جمعية أهلية" },
            new PartyType{Id=((int)PartyTypeEnum.CivilInstitution),Name="مؤسسة أهلية" },
            new PartyType{Id=((int)PartyTypeEnum.ProfessionalCompany),Name="شركة مهنية" },
            new PartyType{Id=((int)PartyTypeEnum.LawOffice),Name="مكاتب محاماة" },
        };

        public static List<PartyStatus> PartyStatusList = new List<PartyStatus>
        {
            new PartyStatus{Id=(int)PartyStatusEnum.Unable,Name="متعسر" },
            new PartyStatus{Id=(int)PartyStatusEnum.Deceased,Name="متوفى" },
            new PartyStatus{Id=(int)PartyStatusEnum.Able,Name="نشط" },

        };

        public static List<PartyClassification> PartyClassificationList = new List<PartyClassification>
        {
            new PartyClassification {Id = (int) PartyClassificationEnum.Requester, Name = "طالب تنفيذ"},
            new PartyClassification {Id = (int) PartyClassificationEnum.Respondent, Name = "منفذ ضده"}
        };

        public static List<RequestTerminationReasons> RequestTerminationReasonsList = new List<RequestTerminationReasons>
        {
            new RequestTerminationReasons{Id=((int)RequestTerminationReasonsEnum.ReconciliationReport), Name="محضر صلح"},
            new RequestTerminationReasons{Id=((int)RequestTerminationReasonsEnum.TotalWaiver), Name="تنازل كلي"},
            new RequestTerminationReasons{Id=((int)RequestTerminationReasonsEnum.CancelAction), Name="رفع الاجراء"},
        };

        public static List<PartyFinancialType> PartyFinancialTypeList = new List<PartyFinancialType>
        {
            new PartyFinancialType{Id=((int)PartyFinancialTypeEnum.Complaint_Credited), Name="دائن" },
            new PartyFinancialType{Id=((int)PartyFinancialTypeEnum.Accused_Debited) , Name="مدين" },
        };

        public static List<PartyRole> PartyRoleList = new List<PartyRole>
        {
            new PartyRole{Id=((int)PartyRoleEnum.OriginalCreditor), Name="محكوم له - دائن - طالب التنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.OriginalDebtor), Name= "المحكوم ضده - المدين" },
            new PartyRole{Id=((int)PartyRoleEnum.CreditorLawyer), Name= "محامي طالب تنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.CreditorAgent), Name= "وكيل طالب تنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.CreditorCounselor), Name= "مستشار طالب تنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.CreditorRepresentative), Name= "ممثل طالب تنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.CreditorCustodian), Name= "ولي طالب تنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.Applicant), Name= "مقدم طلب تنفيذ"},
            new PartyRole{Id=((int)PartyRoleEnum.Debtor), Name= "مدين"},
            new PartyRole{Id=((int)PartyRoleEnum.Creditor), Name= "دائن"},
            new PartyRole{Id=((int)PartyRoleEnum.ServiceProvider), Name= "مقدم خدمات"},
        };

        public static List<DebtType> DebtTypeList = new List<DebtType>
        {
           new DebtType{Id=(int)DebtTypeEnum.Preferential, Name = "دين ممتاز"},
           new DebtType{Id=((int)DebtTypeEnum.Alimony), Name = "دين نفقة"},
           new DebtType{Id=(int)DebtTypeEnum.Underadge, Name = "دين قاصر"},
           new DebtType{Id=((int)DebtTypeEnum.Expense), Name = "دين مصروفات"},
        };

        public static List<ClaimFinancialStatus> ClaimFinancialStatusList = new List<ClaimFinancialStatus>
        {
           new ClaimFinancialStatus{Id=((int)ClaimFinancialStatusEnum.NotPaid), Name = "غير مدفوعه"},
           new ClaimFinancialStatus{Id=((int)ClaimFinancialStatusEnum.PartiallyPaid), Name = "مدفوعه جزئياً"},
           new ClaimFinancialStatus{Id=((int)ClaimFinancialStatusEnum.FullyPaid), Name = "مدفوعه بالكامل" },
        };

        public static List<ClaimStatus> ClaimStatusList = new List<ClaimStatus>
        {
            new ClaimStatus{Id=((int)ClaimStatusEnum.Active), Name = "نشطة"},
            new ClaimStatus{Id=((int)ClaimStatusEnum.Closed), Name = "مغلقة"},
            new ClaimStatus{Id=((int)ClaimStatusEnum.Finished), Name = "منتهية"}
        };

        public static List<FinancialEffectType> FinancialEffectTypeList = new List<FinancialEffectType>
        {
           new FinancialEffectType{Id=(int)FinancialEffectTypeEnum.WaiverRecord, Name = "محضر تنازل", IsIncrementOnClaim = false},
           new FinancialEffectType{Id=(int)FinancialEffectTypeEnum.ServiceProvider, Name = "مقدم خدمة", IsIncrementOnClaim = true},
           new FinancialEffectType{Id=(int)FinancialEffectTypeEnum.NewspaperAdvertisement, Name = "اعلان صحف", IsIncrementOnClaim = true},
        };

        public static List<ClaimTerminationReason> ClaimTerminationReasonEnum = new List<ClaimTerminationReason>
        {
        };
    }
}