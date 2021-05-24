using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ContactsBook.DataAccess.MsSql.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(28)", maxLength: 28, nullable: false),
                    PhoneNumber = table.Column<long>(type: "bigint", maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(320)", maxLength: 320, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("33d5c018-e207-4931-ad11-cc99aca70fa5"), "Holly.Pacocha@gmail.com", "Holly Pacocha", 67468880795L },
                    { new Guid("efc6a94b-ea4b-42be-af29-28aaed83b745"), "Roberta.Green@yahoo.com", "Roberta Green", 46875330497L },
                    { new Guid("0e40fd64-f47f-482d-99b6-f67046dd05e7"), "Edna64@yahoo.com", "Edna Blick", 46707694193L },
                    { new Guid("1d6ee606-ae08-4e8a-beaf-3454cb451d2e"), "Amy_Veum51@yahoo.com", "Amy Veum", 75027030014L },
                    { new Guid("80a33566-acf0-43f9-b3a1-79c332792e68"), "Kim30@gmail.com", "Kim Dietrich", 36281547051L },
                    { new Guid("3527485b-f2ad-4c4d-b8a4-c18e69a7b238"), "Bernadette18@gmail.com", "Bernadette Stark", 94415393603L },
                    { new Guid("98b67a48-4dae-4e9c-b87f-a0b014862ecb"), "Marvin3@yahoo.com", "Marvin Deckow", 41556913313L },
                    { new Guid("b51edd27-9260-4c9f-9225-fb0d885f50a2"), "Eleanor4@gmail.com", "Eleanor Ward", 32085267842L },
                    { new Guid("83330f91-ce15-4eba-93e8-203a7ce664a7"), "Bernard.Torphy14@yahoo.com", "Bernard Torphy", 35292836989L },
                    { new Guid("65fd7234-7d16-4f5e-9303-1318d37c0126"), "Edna34@yahoo.com", "Edna Bartoletti", 38011369234L },
                    { new Guid("d03c4648-0014-4acb-9fe0-2cfc6cd22796"), "Nick96@yahoo.com", "Nick Oberbrunner", 93792188709L },
                    { new Guid("838ec341-6447-4cad-b91c-7f05c17aa2b3"), "Daisy_Streich@yahoo.com", "Daisy Streich", 12896673881L },
                    { new Guid("6c94de67-89d9-42a6-b21c-6eb284ecdc09"), "Jimmie.Gusikowski@hotmail.com", "Jimmie Gusikowski", 21648677602L },
                    { new Guid("e8978be1-0a5a-45e7-a3bb-c06c1864e894"), "Crystal42@hotmail.com", "Crystal Kertzmann", 69405921645L },
                    { new Guid("c77938b1-2795-4257-a7dc-f0b7aac7568e"), "Tara27@hotmail.com", "Tara Blick", 28060260419L },
                    { new Guid("abe3b179-f548-4aaa-8e6b-5ad0076c64b3"), "Darrel_Yundt42@gmail.com", "Darrel Yundt", 20749904984L },
                    { new Guid("1d041d82-d38a-4da1-875c-bfa037f952f8"), "Derrick_Hettinger28@yahoo.com", "Derrick Hettinger", 96072341466L },
                    { new Guid("8ccf4a6d-a32b-4595-aabd-666b955883cd"), "Russell.Abernathy92@gmail.com", "Russell Abernathy", 41040186584L },
                    { new Guid("3535102e-2283-4c99-9547-9309f826387d"), "Regina.Schamberger0@gmail.com", "Regina Schamberger", 79093908564L },
                    { new Guid("65b5627b-c8db-4667-9ca6-50912d2c43e3"), "Rick13@gmail.com", "Rick Gislason", 50231272950L },
                    { new Guid("32142c8f-9b0d-4767-a8a0-a6579975c1d6"), "Frederick_McClure@hotmail.com", "Frederick McClure", 21661263742L },
                    { new Guid("38def77d-e71d-42ee-a964-0695103a1531"), "Fannie.Littel75@yahoo.com", "Fannie Littel", 95142309504L },
                    { new Guid("86d30396-0c7c-43fd-9af2-2490481a213a"), "Brad.Heller26@gmail.com", "Brad Heller", 10471027545L },
                    { new Guid("b4a9dc25-a553-4f05-9206-3f6fa0f3097b"), "Seth.Ledner21@yahoo.com", "Seth Ledner", 61587560056L },
                    { new Guid("344a7f87-cf1a-4ccc-bba4-f3ba67065d44"), "Bruce_Zulauf92@gmail.com", "Bruce Zulauf", 69702255115L },
                    { new Guid("527831a0-ce2e-4238-ae8a-91bbe38346f0"), "Arthur_Franecki@yahoo.com", "Arthur Franecki", 70614671460L },
                    { new Guid("c768eaf8-c4b9-4db3-be08-16ba11a6789b"), "Dana_Kuhn24@yahoo.com", "Dana Kuhn", 28995719705L },
                    { new Guid("804655d0-bd80-4e77-9458-c583e5483567"), "Julius_Connelly@hotmail.com", "Julius Connelly", 61030624877L },
                    { new Guid("d5eafa70-da68-4c8e-91e5-974099792b78"), "Traci.Prohaska46@hotmail.com", "Traci Prohaska", 46806904662L },
                    { new Guid("f063b46b-4d8a-47b4-8370-5118fcfee2f3"), "Cecelia_Larkin55@yahoo.com", "Cecelia Larkin", 67935456041L },
                    { new Guid("a9c0914b-5f25-4064-92b3-a9f015a3cefe"), "Lee43@gmail.com", "Lee Keeling", 51107768104L },
                    { new Guid("1ee66d37-fe73-4a24-b212-037ff2be5f4c"), "Antoinette.OHara@hotmail.com", "Antoinette O'Hara", 42893055106L },
                    { new Guid("454e6139-95af-4fd9-b6e7-1484132e5928"), "Alfredo.Kling46@yahoo.com", "Alfredo Kling", 56597214963L },
                    { new Guid("69897d1f-8d7d-460a-8aea-1e5178c8d976"), "Yvette_Lubowitz@yahoo.com", "Yvette Lubowitz", 56899276458L },
                    { new Guid("fec27d9a-0669-40e4-8cc0-de2ff5a38d3e"), "Amelia68@gmail.com", "Amelia Armstrong", 94815621349L },
                    { new Guid("77ab99ce-7c15-4c66-8d80-b2f06c2a6bda"), "Darlene_Lockman@yahoo.com", "Darlene Lockman", 28766894084L },
                    { new Guid("b11195de-7785-4f76-9afd-066687a1716d"), "Ida_Wiegand@yahoo.com", "Ida Wiegand", 72718047156L },
                    { new Guid("8517f29a-ff6f-42a0-be03-3f600eb7ad70"), "Maryann_Gerlach@gmail.com", "Maryann Gerlach", 57392741310L },
                    { new Guid("b2df4f8c-ac10-40cb-a28a-de46d9d746d1"), "Rodney.Hoeger58@hotmail.com", "Rodney Hoeger", 37581367207L },
                    { new Guid("f93bfb2b-5151-47b4-a307-9bbabbfab7ad"), "Sammy.Jaskolski@gmail.com", "Sammy Jaskolski", 45712344490L },
                    { new Guid("d03cf164-c12d-42f8-9b8c-6f1c1b2d9fe7"), "Dan_Lowe51@yahoo.com", "Dan Lowe", 33640494101L },
                    { new Guid("96dee879-9a38-40e7-bb62-7bfb10ceacf5"), "Hazel.Schimmel@gmail.com", "Hazel Schimmel", 33697748200L }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("b0f06db5-ca26-4234-b354-68b376f1c02e"), "Paulette91@gmail.com", "Paulette Balistreri", 17082520680L },
                    { new Guid("0ac22cd4-e36f-42b9-b030-423cb5e61186"), "Sherry_Metz69@yahoo.com", "Sherry Metz", 38345682238L },
                    { new Guid("8d540623-de22-4749-b0d2-b4d3eb18e743"), "Patty.Satterfield83@gmail.com", "Patty Satterfield", 45188450103L },
                    { new Guid("c2e7496a-dddb-4b7f-a4b1-04e5985d9147"), "Sherman.Rice@yahoo.com", "Sherman Rice", 64373445293L },
                    { new Guid("808884b1-816c-43f3-bc3b-de89c6622ba0"), "Spencer66@yahoo.com", "Spencer Daugherty", 32715586541L },
                    { new Guid("e88a3118-e962-4916-9e5c-731174e539d0"), "Alfred.Barton29@gmail.com", "Alfred Barton", 85680523582L },
                    { new Guid("fde99049-021f-4331-a7df-6b80be67153c"), "Angel52@yahoo.com", "Angel O'Conner", 18533744117L },
                    { new Guid("00aaed29-5f0f-41e0-a25d-b204c534f6e5"), "Duane.Prosacco35@hotmail.com", "Duane Prosacco", 65939801254L },
                    { new Guid("9f654aec-1bf5-4fff-bb61-9537cabfb82a"), "Andrew76@hotmail.com", "Andrew Greenholt", 63906534395L },
                    { new Guid("d92d0388-a988-431c-a8f3-595e9b994be5"), "Pamela.Cremin61@yahoo.com", "Pamela Cremin", 65544495612L },
                    { new Guid("48ce22ed-cce6-4239-8a29-643e32b28f78"), "Shannon80@yahoo.com", "Shannon Batz", 82328311620L },
                    { new Guid("037c856b-6d15-4da7-95cf-4eea677ad97c"), "Timmy.King@gmail.com", "Timmy King", 77631584637L },
                    { new Guid("5af13175-1df1-48a0-9707-ddff84560de5"), "Rufus_Bergstrom@gmail.com", "Rufus Bergstrom", 96977695639L },
                    { new Guid("6adab844-a831-44e7-b6c5-34c7912d5f4a"), "Vernon_Kiehn@gmail.com", "Vernon Kiehn", 72223751573L },
                    { new Guid("60181006-47ee-49db-9f3f-0b175a0b15ff"), "Yvonne.Boyer13@hotmail.com", "Yvonne Boyer", 77808273857L },
                    { new Guid("5b54f2e4-7029-45fb-a8ef-2cc85bca95c0"), "Earl_Kshlerin20@yahoo.com", "Earl Kshlerin", 48699023541L },
                    { new Guid("b34875a1-8796-44d8-9f89-7f9cbb9bbbab"), "Wilbert_Goyette16@gmail.com", "Wilbert Goyette", 64393753681L },
                    { new Guid("41d4947a-7856-4553-afcf-e3ad705aba3b"), "Sherry_Wolf@gmail.com", "Sherry Wolf", 43577179044L },
                    { new Guid("820b415b-85c6-4f3d-b88c-c8ec77cdbd95"), "Timmy_Okuneva@hotmail.com", "Timmy Okuneva", 83102825876L },
                    { new Guid("9e55ce38-8297-4052-bfef-2080cbe9d20a"), "Cristina34@yahoo.com", "Cristina Smitham", 13749512664L },
                    { new Guid("941349ce-e9f2-4d09-936c-32d0768c7212"), "Edith_Jacobi@yahoo.com", "Edith Jacobi", 93203980481L },
                    { new Guid("f3a577a5-c9ea-43fe-ad40-270692fb5bdb"), "Derek.Donnelly@yahoo.com", "Derek Donnelly", 44342389262L },
                    { new Guid("4044eb06-5d6b-4a91-b139-e1baec13973c"), "Gail52@hotmail.com", "Gail Dach", 16016746352L },
                    { new Guid("727cca1f-5e4c-46f2-a873-0303a734233d"), "Daisy.Price47@hotmail.com", "Daisy Price", 42836871502L },
                    { new Guid("73438832-4e7b-4ccf-8ca8-2a5b5545a8ce"), "Alexandra_Heaney@hotmail.com", "Alexandra Heaney", 41036540037L },
                    { new Guid("6a4e2564-1cc4-42e5-9943-d69f6243d080"), "Jason.West@gmail.com", "Jason West", 69258162313L },
                    { new Guid("64eff27e-a1fd-4c81-9b8e-3284015e4d51"), "Matthew_Turcotte19@hotmail.com", "Matthew Turcotte", 73041681699L },
                    { new Guid("5fac7bc8-e75e-48e4-b7b6-e3db84811b13"), "Lynda38@hotmail.com", "Lynda Conn", 38164042499L },
                    { new Guid("38eb3d9f-4363-447c-9ee4-8d826663f609"), "Teresa.Von@yahoo.com", "Teresa Von", 72719569938L },
                    { new Guid("5b6d8694-fe3e-4c72-a477-c2c46a0db984"), "Terry.Blick@gmail.com", "Terry Blick", 80114973233L },
                    { new Guid("3cb774b7-5c35-429b-a018-726af0fd9a37"), "Rickey.Bechtelar@yahoo.com", "Rickey Bechtelar", 95804890945L },
                    { new Guid("adf3c74a-9b84-4ecb-9779-732c3614c759"), "Renee_Halvorson68@hotmail.com", "Renee Halvorson", 47185066378L },
                    { new Guid("266416b7-1722-40ab-8714-55803e8c94bb"), "Allan.Nader48@hotmail.com", "Allan Nader", 47906644934L },
                    { new Guid("494984f4-7d87-4ad5-8b44-b0aec2d9ba8f"), "Kathleen.Kuhic68@gmail.com", "Kathleen Kuhic", 61818545359L },
                    { new Guid("c0fe8c8d-9dc7-4ad4-9298-dcd152b007f4"), "June4@gmail.com", "June Wiza", 32715616046L },
                    { new Guid("532b75ac-838e-4ce8-a909-fea867b55dbe"), "Clarence55@yahoo.com", "Clarence Thompson", 94743814255L },
                    { new Guid("1a1d1531-ee95-429b-9c7d-2ed9cdf3b9b3"), "Doris53@yahoo.com", "Doris Auer", 76244148866L },
                    { new Guid("f2881559-b940-43aa-b0c9-d487babe1c1b"), "Joanna54@gmail.com", "Joanna Block", 90476852049L },
                    { new Guid("bfad2469-52b4-4048-81f4-cb4ad9737b6b"), "Pearl.Roberts@hotmail.com", "Pearl Roberts", 24003938541L },
                    { new Guid("d26bbd2b-7103-4cbb-ae6a-43f0c8db2d10"), "Margaret_Schulist57@gmail.com", "Margaret Schulist", 42393857786L },
                    { new Guid("3419563d-1ef1-4650-9256-6a351b7512c4"), "Curtis53@hotmail.com", "Curtis Buckridge", 65291823728L },
                    { new Guid("bcb749d1-3e20-4b28-bd03-373794e76b10"), "Alice99@yahoo.com", "Alice Vandervort", 99465892514L }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("2fdecbb8-9b4a-4c6f-bfd9-8b75e30be633"), "Vivian_Armstrong3@yahoo.com", "Vivian Armstrong", 47241681230L },
                    { new Guid("c42d2a78-a03f-41e7-957c-2cf0b4bbb925"), "Marshall84@gmail.com", "Marshall O'Hara", 11785620135L },
                    { new Guid("6d832461-3252-4078-9c5f-c91e7617cc55"), "Valerie3@yahoo.com", "Valerie D'Amore", 38757823058L },
                    { new Guid("c8d1bae9-fc94-4d83-a8d2-4cf154e1ff19"), "Nathaniel_Gerhold57@hotmail.com", "Nathaniel Gerhold", 44333592497L },
                    { new Guid("84caa8b7-9f26-46fc-b7eb-63a6fa7f0d71"), "Alexandra.Littel1@gmail.com", "Alexandra Littel", 84000275034L },
                    { new Guid("04eb1547-7850-4e43-85f5-1c2fd146e47b"), "Martha_Runolfsson66@hotmail.com", "Martha Runolfsson", 62671828005L },
                    { new Guid("62ac6dd7-2140-40ed-bb1c-de456b56009b"), "Thomas94@gmail.com", "Thomas Trantow", 26717778638L },
                    { new Guid("b48dd95e-219e-439c-a544-0980af173f81"), "Estelle.Reinger@yahoo.com", "Estelle Reinger", 95611155784L },
                    { new Guid("3755a4cb-55aa-40f4-bb74-41fc6808a118"), "Yolanda_Kozey@hotmail.com", "Yolanda Kozey", 96359637442L },
                    { new Guid("51456fd6-e435-4209-a26f-0f1cb099455b"), "Brandy.Funk@yahoo.com", "Brandy Funk", 97739787118L },
                    { new Guid("274318ab-7067-4d3a-823d-9a532c7cede9"), "Annie14@yahoo.com", "Annie Wolf", 37608332963L },
                    { new Guid("114cb444-d027-4851-84b2-ab95a6f0a78d"), "Norman87@yahoo.com", "Norman Schuppe", 55571766894L },
                    { new Guid("15bcab84-921e-49e6-ac8b-4efb9504bf7d"), "Earl.Osinski@yahoo.com", "Earl Osinski", 62095650700L },
                    { new Guid("d6cfc31f-a2b9-4dbb-aede-32aa1283e7ab"), "Ben_Bergstrom@hotmail.com", "Ben Bergstrom", 19370890930L },
                    { new Guid("4e2b9c0d-4399-425f-98a0-06bab9e1b374"), "Crystal27@gmail.com", "Crystal Heller", 90213429536L },
                    { new Guid("23f46120-7d04-4195-b805-cea968bb8d38"), "Alex5@hotmail.com", "Alex Bashirian", 47846396233L }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");
        }
    }
}
