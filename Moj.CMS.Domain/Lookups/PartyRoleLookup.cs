using Moj.CMS.Domain.Shared.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Moj.CMS.Domain.Lookups
{
    public class PartyRoleLookup : BaseEnumeration
    {
        public bool IsComplaint { get; set; }

        public static PartyRoleLookup OriginalCreditor = new PartyRoleLookup((int)PartyRoleEnum.OriginalCreditor, nameof(PartyRoleEnum.OriginalCreditor), isComplaint: true);
        public static PartyRoleLookup OriginalDebtor = new PartyRoleLookup((int)PartyRoleEnum.OriginalDebtor, nameof(PartyRoleEnum.OriginalDebtor), isComplaint: false);
        public static PartyRoleLookup Applicant = new PartyRoleLookup((int)PartyRoleEnum.Applicant, nameof(PartyRoleEnum.Applicant), isComplaint: true);
        public static PartyRoleLookup Creditor = new PartyRoleLookup((int)PartyRoleEnum.Creditor, nameof(PartyRoleEnum.Creditor), isComplaint: true);
        public static PartyRoleLookup CreditorAgent = new PartyRoleLookup((int)PartyRoleEnum.CreditorAgent, nameof(PartyRoleEnum.CreditorAgent), isComplaint: true);
        public static PartyRoleLookup CreditorCounselor = new PartyRoleLookup((int)PartyRoleEnum.CreditorCounselor, nameof(PartyRoleEnum.CreditorCounselor), isComplaint: true);
        public static PartyRoleLookup CreditorCustodian = new PartyRoleLookup((int)PartyRoleEnum.CreditorCustodian, nameof(PartyRoleEnum.CreditorCustodian), isComplaint: true);
        public static PartyRoleLookup CreditorLawyer = new PartyRoleLookup((int)PartyRoleEnum.CreditorLawyer, nameof(PartyRoleEnum.CreditorLawyer), isComplaint: true);
        public static PartyRoleLookup CreditorRepresentative = new PartyRoleLookup((int)PartyRoleEnum.CreditorRepresentative, nameof(PartyRoleEnum.CreditorRepresentative), isComplaint: true);
        public static PartyRoleLookup Debtor = new PartyRoleLookup((int)PartyRoleEnum.Debtor, nameof(PartyRoleEnum.Debtor), isComplaint: false);
        public static PartyRoleLookup ServiceProvider = new PartyRoleLookup((int)PartyRoleEnum.ServiceProvider, nameof(PartyRoleEnum.ServiceProvider), isComplaint: true);

        public PartyRoleLookup(int id, string name, bool isComplaint) : base(id, name)
        {
            IsComplaint = isComplaint;
        }

        public static PartyRoleLookup Find(PartyRoleEnum partyRole)
        {
            return List().FirstOrDefault(r => r.Id == (int)partyRole);
        }

        public static IEnumerable<PartyRoleLookup> List()
        {
            yield return OriginalCreditor;
            yield return OriginalDebtor;
            yield return Applicant;
            yield return Creditor;
            yield return CreditorAgent;
            yield return CreditorCounselor;
            yield return CreditorCustodian;
            yield return CreditorLawyer;
            yield return CreditorRepresentative;
            yield return Debtor;
            yield return ServiceProvider;
        }
    }
}
