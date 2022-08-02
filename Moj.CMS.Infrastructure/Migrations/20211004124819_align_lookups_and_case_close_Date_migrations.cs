using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlazorHero.CleanArchitecture.Infrastructure.Migrations
{
    public partial class align_lookups_and_case_close_Date_migrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtBankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestTerminationReasons",
                schema: "CMS",
                table: "RequestTerminationReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromissoryTypes",
                schema: "CMS",
                table: "PromissoryTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyTypes",
                schema: "CMS",
                table: "PartyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyStatuses",
                schema: "CMS",
                table: "PartyStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyRoles",
                schema: "CMS",
                table: "PartyRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyLocations",
                schema: "CMS",
                table: "PartyLocations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyIdentityTypes",
                schema: "CMS",
                table: "PartyIdentityTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyFinancialTypes",
                schema: "CMS",
                table: "PartyFinancialTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nationality",
                schema: "CMS",
                table: "Nationality");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Judges",
                schema: "CMS",
                table: "Judges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialClaimStatuses",
                schema: "CMS",
                table: "FinancialClaimStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAdjustmentReasons",
                schema: "CMS",
                table: "FinancialAdjustmentReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Divisions",
                schema: "CMS",
                table: "Divisions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebtTypes",
                schema: "CMS",
                table: "DebtTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courts",
                schema: "CMS",
                table: "Courts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseTypes",
                schema: "CMS",
                table: "CaseTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseStatuses",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseOperation",
                schema: "CMS",
                table: "CaseOperation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Areas",
                schema: "CMS",
                table: "Areas");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "CMS",
                table: "CaseStatuses");

            migrationBuilder.RenameTable(
                name: "RequestTerminationReasons",
                schema: "CMS",
                newName: "RequestTerminationReasonsLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PromissoryTypes",
                schema: "CMS",
                newName: "PromissoryTypesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyTypes",
                schema: "CMS",
                newName: "PartyTypesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyStatuses",
                schema: "CMS",
                newName: "PartyStatusesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyRoles",
                schema: "CMS",
                newName: "PartyRolesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyLocations",
                schema: "CMS",
                newName: "PartyLocationsLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyIdentityTypes",
                schema: "CMS",
                newName: "PartyIdentityTypesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyFinancialTypes",
                schema: "CMS",
                newName: "PartyFinancialTypesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "Nationality",
                schema: "CMS",
                newName: "NationalityLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "Judges",
                schema: "CMS",
                newName: "JudgesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "FinancialClaimStatuses",
                schema: "CMS",
                newName: "FinancialClaimStatusesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "FinancialAdjustmentReasons",
                schema: "CMS",
                newName: "FinancialAdjustmentReasonsLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "Divisions",
                schema: "CMS",
                newName: "DivisionsLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "DebtTypes",
                schema: "CMS",
                newName: "DebtTypesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "Courts",
                schema: "CMS",
                newName: "CourtsLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CaseTypes",
                schema: "CMS",
                newName: "CaseTypesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CaseStatuses",
                schema: "CMS",
                newName: "CaseStatusesLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CaseOperation",
                schema: "CMS",
                newName: "CaseOperationsLookup",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "Areas",
                schema: "CMS",
                newName: "AreasLookup",
                newSchema: "CMS");

            migrationBuilder.AddColumn<DateTime>(
                name: "CloseDate",
                schema: "CMS",
                table: "Case",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestTerminationReasonsLookup",
                schema: "CMS",
                table: "RequestTerminationReasonsLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromissoryTypesLookup",
                schema: "CMS",
                table: "PromissoryTypesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyTypesLookup",
                schema: "CMS",
                table: "PartyTypesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyStatusesLookup",
                schema: "CMS",
                table: "PartyStatusesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyRolesLookup",
                schema: "CMS",
                table: "PartyRolesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyLocationsLookup",
                schema: "CMS",
                table: "PartyLocationsLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyIdentityTypesLookup",
                schema: "CMS",
                table: "PartyIdentityTypesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyFinancialTypesLookup",
                schema: "CMS",
                table: "PartyFinancialTypesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NationalityLookup",
                schema: "CMS",
                table: "NationalityLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JudgesLookup",
                schema: "CMS",
                table: "JudgesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialClaimStatusesLookup",
                schema: "CMS",
                table: "FinancialClaimStatusesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAdjustmentReasonsLookup",
                schema: "CMS",
                table: "FinancialAdjustmentReasonsLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DivisionsLookup",
                schema: "CMS",
                table: "DivisionsLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebtTypesLookup",
                schema: "CMS",
                table: "DebtTypesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CourtsLookup",
                schema: "CMS",
                table: "CourtsLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseTypesLookup",
                schema: "CMS",
                table: "CaseTypesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseStatusesLookup",
                schema: "CMS",
                table: "CaseStatusesLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseOperationsLookup",
                schema: "CMS",
                table: "CaseOperationsLookup",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AreasLookup",
                schema: "CMS",
                table: "AreasLookup",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtBankAccounts_CourtsLookup_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts",
                column: "CourtId",
                principalSchema: "CMS",
                principalTable: "CourtsLookup",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CourtBankAccounts_CourtsLookup_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RequestTerminationReasonsLookup",
                schema: "CMS",
                table: "RequestTerminationReasonsLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PromissoryTypesLookup",
                schema: "CMS",
                table: "PromissoryTypesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyTypesLookup",
                schema: "CMS",
                table: "PartyTypesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyStatusesLookup",
                schema: "CMS",
                table: "PartyStatusesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyRolesLookup",
                schema: "CMS",
                table: "PartyRolesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyLocationsLookup",
                schema: "CMS",
                table: "PartyLocationsLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyIdentityTypesLookup",
                schema: "CMS",
                table: "PartyIdentityTypesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PartyFinancialTypesLookup",
                schema: "CMS",
                table: "PartyFinancialTypesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NationalityLookup",
                schema: "CMS",
                table: "NationalityLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JudgesLookup",
                schema: "CMS",
                table: "JudgesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialClaimStatusesLookup",
                schema: "CMS",
                table: "FinancialClaimStatusesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FinancialAdjustmentReasonsLookup",
                schema: "CMS",
                table: "FinancialAdjustmentReasonsLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DivisionsLookup",
                schema: "CMS",
                table: "DivisionsLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DebtTypesLookup",
                schema: "CMS",
                table: "DebtTypesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CourtsLookup",
                schema: "CMS",
                table: "CourtsLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseTypesLookup",
                schema: "CMS",
                table: "CaseTypesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseStatusesLookup",
                schema: "CMS",
                table: "CaseStatusesLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CaseOperationsLookup",
                schema: "CMS",
                table: "CaseOperationsLookup");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AreasLookup",
                schema: "CMS",
                table: "AreasLookup");

            migrationBuilder.DropColumn(
                name: "CloseDate",
                schema: "CMS",
                table: "Case");

            migrationBuilder.RenameTable(
                name: "RequestTerminationReasonsLookup",
                schema: "CMS",
                newName: "RequestTerminationReasons",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PromissoryTypesLookup",
                schema: "CMS",
                newName: "PromissoryTypes",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyTypesLookup",
                schema: "CMS",
                newName: "PartyTypes",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyStatusesLookup",
                schema: "CMS",
                newName: "PartyStatuses",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyRolesLookup",
                schema: "CMS",
                newName: "PartyRoles",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyLocationsLookup",
                schema: "CMS",
                newName: "PartyLocations",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyIdentityTypesLookup",
                schema: "CMS",
                newName: "PartyIdentityTypes",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "PartyFinancialTypesLookup",
                schema: "CMS",
                newName: "PartyFinancialTypes",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "NationalityLookup",
                schema: "CMS",
                newName: "Nationality",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "JudgesLookup",
                schema: "CMS",
                newName: "Judges",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "FinancialClaimStatusesLookup",
                schema: "CMS",
                newName: "FinancialClaimStatuses",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "FinancialAdjustmentReasonsLookup",
                schema: "CMS",
                newName: "FinancialAdjustmentReasons",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "DivisionsLookup",
                schema: "CMS",
                newName: "Divisions",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "DebtTypesLookup",
                schema: "CMS",
                newName: "DebtTypes",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CourtsLookup",
                schema: "CMS",
                newName: "Courts",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CaseTypesLookup",
                schema: "CMS",
                newName: "CaseTypes",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CaseStatusesLookup",
                schema: "CMS",
                newName: "CaseStatuses",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "CaseOperationsLookup",
                schema: "CMS",
                newName: "CaseOperation",
                newSchema: "CMS");

            migrationBuilder.RenameTable(
                name: "AreasLookup",
                schema: "CMS",
                newName: "Areas",
                newSchema: "CMS");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "CMS",
                table: "CaseStatuses",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RequestTerminationReasons",
                schema: "CMS",
                table: "RequestTerminationReasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PromissoryTypes",
                schema: "CMS",
                table: "PromissoryTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyTypes",
                schema: "CMS",
                table: "PartyTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyStatuses",
                schema: "CMS",
                table: "PartyStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyRoles",
                schema: "CMS",
                table: "PartyRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyLocations",
                schema: "CMS",
                table: "PartyLocations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyIdentityTypes",
                schema: "CMS",
                table: "PartyIdentityTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PartyFinancialTypes",
                schema: "CMS",
                table: "PartyFinancialTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nationality",
                schema: "CMS",
                table: "Nationality",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Judges",
                schema: "CMS",
                table: "Judges",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialClaimStatuses",
                schema: "CMS",
                table: "FinancialClaimStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FinancialAdjustmentReasons",
                schema: "CMS",
                table: "FinancialAdjustmentReasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Divisions",
                schema: "CMS",
                table: "Divisions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DebtTypes",
                schema: "CMS",
                table: "DebtTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courts",
                schema: "CMS",
                table: "Courts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseTypes",
                schema: "CMS",
                table: "CaseTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseStatuses",
                schema: "CMS",
                table: "CaseStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CaseOperation",
                schema: "CMS",
                table: "CaseOperation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Areas",
                schema: "CMS",
                table: "Areas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CourtBankAccounts_Courts_CourtId",
                schema: "CMS",
                table: "CourtBankAccounts",
                column: "CourtId",
                principalSchema: "CMS",
                principalTable: "Courts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
