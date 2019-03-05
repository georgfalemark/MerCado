using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace mercado.nu.Migrations
{
    public partial class migge1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contents",
                columns: table => new
                {
                    ContentId = table.Column<Guid>(nullable: false),
                    Headline = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Publish = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contents", x => x.ContentId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(nullable: false),
                    QuestionNumber = table.Column<int>(nullable: false),
                    ActualQuestion = table.Column<string>(nullable: true),
                    ChaptersId = table.Column<Guid>(nullable: false),
                    QuestionType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                });

            migrationBuilder.CreateTable(
                name: "QuestionOptions",
                columns: table => new
                {
                    QuestionOptionId = table.Column<Guid>(nullable: false),
                    QuestionOptionHeading = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    QuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionOptions", x => x.QuestionOptionId);
                    table.ForeignKey(
                        name: "FK_QuestionOptions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<Guid>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false),
                    MarketResearchId = table.Column<Guid>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Chapters",
                columns: table => new
                {
                    ChaptersId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    MarketResearchId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chapters", x => x.ChaptersId);
                });

            migrationBuilder.CreateTable(
                name: "GetQuestionToMarketResearches",
                columns: table => new
                {
                    QuestionId = table.Column<Guid>(nullable: false),
                    MarketResearchId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetQuestionToMarketResearches", x => new { x.MarketResearchId, x.QuestionId });
                    table.ForeignKey(
                        name: "FK_GetQuestionToMarketResearches_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responders",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    MarketResearchId = table.Column<Guid>(nullable: false),
                    MarketResearchCompleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responders", x => new { x.MarketResearchId, x.PersonId });
                });

            migrationBuilder.CreateTable(
                name: "MarketResearches",
                columns: table => new
                {
                    MarketResearchId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Purpose = table.Column<string>(nullable: true),
                    MinAge = table.Column<int>(nullable: false),
                    MaxAge = table.Column<int>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Area = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    NumberOfResponders = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<Guid>(nullable: true),
                    PersonId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketResearches", x => x.MarketResearchId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    Street = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    PostNumber = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    PersonId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    StreetName = table.Column<string>(nullable: true),
                    StreetNumber = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    ContactPersonId = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    PathLogoImage = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.OrganizationId);
                    table.ForeignKey(
                        name: "ForeignKey_ContacPersonOrganisation_ContactPerson",
                        column: x => x.ContactPersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_MarketResearchId",
                table: "Answers",
                column: "MarketResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_PersonId",
                table: "Answers",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PersonId",
                table: "AspNetUsers",
                column: "PersonId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Chapters_MarketResearchId",
                table: "Chapters",
                column: "MarketResearchId");

            migrationBuilder.CreateIndex(
                name: "IX_GetQuestionToMarketResearches_QuestionId",
                table: "GetQuestionToMarketResearches",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketResearches_OrganizationId",
                table: "MarketResearches",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketResearches_PersonId",
                table: "MarketResearches",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Organizations_ContactPersonId",
                table: "Organizations",
                column: "ContactPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Persons_OrganizationId",
                table: "Persons",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionOptions_QuestionId",
                table: "QuestionOptions",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ChaptersId",
                table: "Questions",
                column: "ChaptersId");

            migrationBuilder.CreateIndex(
                name: "IX_Responders_PersonId",
                table: "Responders",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                table: "AspNetUserTokens",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Chapters_ChaptersId",
                table: "Questions",
                column: "ChaptersId",
                principalTable: "Chapters",
                principalColumn: "ChaptersId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_MarketResearches_MarketResearchId",
                table: "Answers",
                column: "MarketResearchId",
                principalTable: "MarketResearches",
                principalColumn: "MarketResearchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Persons_PersonId",
                table: "Answers",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapters_MarketResearches_MarketResearchId",
                table: "Chapters",
                column: "MarketResearchId",
                principalTable: "MarketResearches",
                principalColumn: "MarketResearchId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GetQuestionToMarketResearches_MarketResearches_MarketResearchId",
                table: "GetQuestionToMarketResearches",
                column: "MarketResearchId",
                principalTable: "MarketResearches",
                principalColumn: "MarketResearchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responders_MarketResearches_MarketResearchId",
                table: "Responders",
                column: "MarketResearchId",
                principalTable: "MarketResearches",
                principalColumn: "MarketResearchId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Responders_Persons_PersonId",
                table: "Responders",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketResearches_Persons_PersonId",
                table: "MarketResearches",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "PersonId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MarketResearches_Organizations_OrganizationId",
                table: "MarketResearches",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Organizations_OrganizationId",
                table: "Persons",
                column: "OrganizationId",
                principalTable: "Organizations",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_ContacPersonOrganisation_ContactPerson",
                table: "Organizations");

            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contents");

            migrationBuilder.DropTable(
                name: "GetQuestionToMarketResearches");

            migrationBuilder.DropTable(
                name: "QuestionOptions");

            migrationBuilder.DropTable(
                name: "Responders");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Chapters");

            migrationBuilder.DropTable(
                name: "MarketResearches");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Organizations");
        }
    }
}
