using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyShoppingCart.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), "Electronics" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", "7Up 16oz Bottle" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.");

            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "Description", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("092fa47a-da69-4514-985e-7eed2739d817"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Big League Chew Bubble Gum", 100.00m },
                    { new Guid("10735d96-d843-4835-9312-4d19aa92bf5f"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Hungry Hungry Hippos", 16.00m },
                    { new Guid("11126500-2f81-4b90-88cb-84d8378e6d6b"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Mountain Dew 16oz Bottle", 100.00m },
                    { new Guid("13ae159d-4f77-4b31-9a99-1df06086a26d"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "G.I. Joe Action Figure", 100.00m },
                    { new Guid("1539ce54-3d94-4a0f-9580-da51123797cb"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Hubba Bubba Bubble Gum", 100.00m },
                    { new Guid("24181791-37f3-4af6-95e3-4a17409cc1be"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Sweet Tea 16oz Bottle", 100.00m },
                    { new Guid("24f97fd4-f17a-4bf3-998c-99c2ba557101"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Connect4", 18.00m },
                    { new Guid("31e90811-9d42-4459-903c-5092d21f992f"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Green Hoodie", 100.00m },
                    { new Guid("32aa1795-2bbf-4a94-bacf-334fbf72a49b"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Baseball Cap", 100.00m },
                    { new Guid("32fce956-7878-4eba-bc57-c850d58add33"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Boombox", 100.00m },
                    { new Guid("35afde2a-7889-43e8-925c-09b65f05c849"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "64gb iPod", 100.00m },
                    { new Guid("397387de-29aa-47cd-83b1-2e1e242bbfed"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "10 Pack of HotDogs", 100.00m },
                    { new Guid("3ab12432-5d93-496e-83c9-5929514f82b5"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A 4-Pack of T-Shirts", 100.00m },
                    { new Guid("41ab5b04-f9f1-4e49-8004-246fe79f6136"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Dungeons and Dragons Rulebook", 50.00m },
                    { new Guid("424f2a29-f09d-43e9-a28e-7fd1750f4102"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Baseball Glove", 100.00m },
                    { new Guid("430f8382-69c8-473f-a74f-a244a77133ae"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Adidas Tennis Shoes", 100.00m },
                    { new Guid("54e911e5-a9db-423a-b284-cadcd559b30b"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Lite Brite", 100.00m },
                    { new Guid("58a0114b-e802-40d4-9b0b-7187b77eb30c"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Pack of Pokemon Cards", 100.00m },
                    { new Guid("5b858f9e-1f64-47b0-841f-14604a9d4035"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Spearmint Gum", 100.00m },
                    { new Guid("64068786-c23f-4904-bc30-d297fe71eb6f"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Pair of Blue Jeans", 100.00m },
                    { new Guid("678c8fc3-8156-45f0-8a0a-04d747442cf9"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Long Underwear", 100.00m },
                    { new Guid("6ca7d4d7-2d22-4481-b39d-890c515a20fe"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Cowboy Boots", 100.00m },
                    { new Guid("6d761b94-26b6-4b15-a585-8c7dd15f8e4d"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A 20lb Bag of Dog Food", 100.00m },
                    { new Guid("70ab156e-d382-4d9f-b48a-8efa1e74c2b7"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Gallon of Sweet Tea", 8.00m },
                    { new Guid("77122595-d2ef-4b4b-8841-c6a5fb91acfe"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Tennis Racket", 100.00m },
                    { new Guid("8399bba9-b3eb-4e2b-9261-b6f38163f114"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Rubik's Cube", 10.00m },
                    { new Guid("88f90070-9bfb-4722-a802-a58e4459f625"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "32gb iPod", 100.00m },
                    { new Guid("916d84a6-727a-4aa5-b742-562815b28297"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "12 Pack of HotDog Buns", 100.00m },
                    { new Guid("99ebc170-e52d-4e52-b4ac-1abbbfb799a5"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Clock Radio", 100.00m },
                    { new Guid("9be0417a-8c93-4cde-9d6c-79a15996b5a3"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A 20lb Bag of Kitty Litter", 100.00m },
                    { new Guid("9fc97e4a-5b6c-4cbd-a4ce-35bfdb49bb05"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Pack of Baseball Cards", 100.00m },
                    { new Guid("a145dfbd-eefe-411c-8d6d-3feaf0747231"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Visio 46-Inch Television", 100.00m },
                    { new Guid("a6ebec19-67b5-4286-a634-a5994f332f4c"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Sony 42-Inch Television", 100.00m },
                    { new Guid("a922340c-81bc-4189-9060-56b796ff954e"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Dozen Donuts", 100.00m },
                    { new Guid("b10f29d8-addc-4fd2-81bb-b3084db31018"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "12 Pack of Beer", 15.00m },
                    { new Guid("b59eaa64-45f5-4b90-83fa-16235af1e5e3"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Sweat Pants", 100.00m },
                    { new Guid("bc4766e4-fbb4-4384-b3b8-bb5c3d7cb0b0"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "1lb of Bacon", 100.00m },
                    { new Guid("c042cee5-b114-4fa3-8ba6-64b180ff128e"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A 5-Foot Leash", 100.00m },
                    { new Guid("d71023e3-93b6-45a6-a5c0-1fdda2d4a1cf"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A Hockey Stick", 100.00m },
                    { new Guid("e53ce73a-3e50-4498-9c51-ef0df346a3c2"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Fruit Loops", 100.00m },
                    { new Guid("e6e7723f-0b6c-4c37-b259-18de64d486cb"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Barrel of Monkeys", 9.00m },
                    { new Guid("f7246fc8-8a97-4b0b-8fd6-21bb0287b6eb"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "Golf Clubs", 100.00m },
                    { new Guid("f85c2129-dc48-494e-a551-6fa684e77142"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis.", null, "A 20lb Bag of Cat Food", 100.00m }
                });

            migrationBuilder.InsertData(
                table: "ProductCategory",
                columns: new[] { "CategoryId", "ProductId" },
                values: new object[,]
                {
                    { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("092fa47a-da69-4514-985e-7eed2739d817") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("10735d96-d843-4835-9312-4d19aa92bf5f") },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("11126500-2f81-4b90-88cb-84d8378e6d6b") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("13ae159d-4f77-4b31-9a99-1df06086a26d") },
                    { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("1539ce54-3d94-4a0f-9580-da51123797cb") },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("24181791-37f3-4af6-95e3-4a17409cc1be") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("24f97fd4-f17a-4bf3-998c-99c2ba557101") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("31e90811-9d42-4459-903c-5092d21f992f") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("32aa1795-2bbf-4a94-bacf-334fbf72a49b") },
                    { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("32fce956-7878-4eba-bc57-c850d58add33") },
                    { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("35afde2a-7889-43e8-925c-09b65f05c849") },
                    { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("397387de-29aa-47cd-83b1-2e1e242bbfed") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("3ab12432-5d93-496e-83c9-5929514f82b5") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("41ab5b04-f9f1-4e49-8004-246fe79f6136") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("424f2a29-f09d-43e9-a28e-7fd1750f4102") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("430f8382-69c8-473f-a74f-a244a77133ae") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("54e911e5-a9db-423a-b284-cadcd559b30b") },
                    { new Guid("5330fda9-5934-4d84-936e-7e910ee66cd6"), new Guid("58a0114b-e802-40d4-9b0b-7187b77eb30c") },
                    { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("5b858f9e-1f64-47b0-841f-14604a9d4035") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("64068786-c23f-4904-bc30-d297fe71eb6f") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("678c8fc3-8156-45f0-8a0a-04d747442cf9") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("6ca7d4d7-2d22-4481-b39d-890c515a20fe") },
                    { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("6d761b94-26b6-4b15-a585-8c7dd15f8e4d") },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("70ab156e-d382-4d9f-b48a-8efa1e74c2b7") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("77122595-d2ef-4b4b-8841-c6a5fb91acfe") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("8399bba9-b3eb-4e2b-9261-b6f38163f114") },
                    { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("88f90070-9bfb-4722-a802-a58e4459f625") },
                    { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("916d84a6-727a-4aa5-b742-562815b28297") },
                    { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("99ebc170-e52d-4e52-b4ac-1abbbfb799a5") },
                    { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("9be0417a-8c93-4cde-9d6c-79a15996b5a3") },
                    { new Guid("5330fda9-5934-4d84-936e-7e910ee66cd6"), new Guid("9fc97e4a-5b6c-4cbd-a4ce-35bfdb49bb05") },
                    { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("a145dfbd-eefe-411c-8d6d-3feaf0747231") },
                    { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("a6ebec19-67b5-4286-a634-a5994f332f4c") },
                    { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("a922340c-81bc-4189-9060-56b796ff954e") },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("b10f29d8-addc-4fd2-81bb-b3084db31018") },
                    { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("b59eaa64-45f5-4b90-83fa-16235af1e5e3") },
                    { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("bc4766e4-fbb4-4384-b3b8-bb5c3d7cb0b0") },
                    { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("c042cee5-b114-4fa3-8ba6-64b180ff128e") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("d71023e3-93b6-45a6-a5c0-1fdda2d4a1cf") },
                    { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("e53ce73a-3e50-4498-9c51-ef0df346a3c2") },
                    { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("e6e7723f-0b6c-4c37-b259-18de64d486cb") },
                    { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("f7246fc8-8a97-4b0b-8fd6-21bb0287b6eb") },
                    { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("f85c2129-dc48-494e-a551-6fa684e77142") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("092fa47a-da69-4514-985e-7eed2739d817") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("10735d96-d843-4835-9312-4d19aa92bf5f") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("11126500-2f81-4b90-88cb-84d8378e6d6b") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("13ae159d-4f77-4b31-9a99-1df06086a26d") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("1539ce54-3d94-4a0f-9580-da51123797cb") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("24181791-37f3-4af6-95e3-4a17409cc1be") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("24f97fd4-f17a-4bf3-998c-99c2ba557101") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("31e90811-9d42-4459-903c-5092d21f992f") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("32aa1795-2bbf-4a94-bacf-334fbf72a49b") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("32fce956-7878-4eba-bc57-c850d58add33") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("35afde2a-7889-43e8-925c-09b65f05c849") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("397387de-29aa-47cd-83b1-2e1e242bbfed") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("3ab12432-5d93-496e-83c9-5929514f82b5") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("41ab5b04-f9f1-4e49-8004-246fe79f6136") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("424f2a29-f09d-43e9-a28e-7fd1750f4102") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("430f8382-69c8-473f-a74f-a244a77133ae") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("54e911e5-a9db-423a-b284-cadcd559b30b") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("5330fda9-5934-4d84-936e-7e910ee66cd6"), new Guid("58a0114b-e802-40d4-9b0b-7187b77eb30c") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("afa2903e-6595-4f62-9bc2-577f0399ce18"), new Guid("5b858f9e-1f64-47b0-841f-14604a9d4035") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("64068786-c23f-4904-bc30-d297fe71eb6f") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("678c8fc3-8156-45f0-8a0a-04d747442cf9") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("6ca7d4d7-2d22-4481-b39d-890c515a20fe") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("6d761b94-26b6-4b15-a585-8c7dd15f8e4d") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("70ab156e-d382-4d9f-b48a-8efa1e74c2b7") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("77122595-d2ef-4b4b-8841-c6a5fb91acfe") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("8399bba9-b3eb-4e2b-9261-b6f38163f114") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("88f90070-9bfb-4722-a802-a58e4459f625") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("916d84a6-727a-4aa5-b742-562815b28297") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("99ebc170-e52d-4e52-b4ac-1abbbfb799a5") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("9be0417a-8c93-4cde-9d6c-79a15996b5a3") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("5330fda9-5934-4d84-936e-7e910ee66cd6"), new Guid("9fc97e4a-5b6c-4cbd-a4ce-35bfdb49bb05") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("a145dfbd-eefe-411c-8d6d-3feaf0747231") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a303508a-6156-44b9-8dab-07f05039fa33"), new Guid("a6ebec19-67b5-4286-a634-a5994f332f4c") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("a922340c-81bc-4189-9060-56b796ff954e") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("b10f29d8-addc-4fd2-81bb-b3084db31018") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("c231f98e-f62e-4672-a12c-18c98b7e7669"), new Guid("b59eaa64-45f5-4b90-83fa-16235af1e5e3") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("31654a1c-14d6-47e5-bdf6-0c158aad9cc9"), new Guid("bc4766e4-fbb4-4384-b3b8-bb5c3d7cb0b0") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("c042cee5-b114-4fa3-8ba6-64b180ff128e") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("d71023e3-93b6-45a6-a5c0-1fdda2d4a1cf") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("592ca9e1-89ee-4807-8070-7e4fe75ce92f"), new Guid("e53ce73a-3e50-4498-9c51-ef0df346a3c2") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("663ac0e4-6265-417c-89e8-968b42077169"), new Guid("e6e7723f-0b6c-4c37-b259-18de64d486cb") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("2478b36b-05f0-4c89-97ab-1a2b8dd0158b"), new Guid("f7246fc8-8a97-4b0b-8fd6-21bb0287b6eb") });

            migrationBuilder.DeleteData(
                table: "ProductCategory",
                keyColumns: new[] { "CategoryId", "ProductId" },
                keyValues: new object[] { new Guid("a306808a-6156-44b9-8dab-07f05039fa33"), new Guid("f85c2129-dc48-494e-a551-6fa684e77142") });

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("a303508a-6156-44b9-8dab-07f05039fa33"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("092fa47a-da69-4514-985e-7eed2739d817"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("10735d96-d843-4835-9312-4d19aa92bf5f"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("11126500-2f81-4b90-88cb-84d8378e6d6b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("13ae159d-4f77-4b31-9a99-1df06086a26d"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1539ce54-3d94-4a0f-9580-da51123797cb"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24181791-37f3-4af6-95e3-4a17409cc1be"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24f97fd4-f17a-4bf3-998c-99c2ba557101"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("31e90811-9d42-4459-903c-5092d21f992f"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("32aa1795-2bbf-4a94-bacf-334fbf72a49b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("32fce956-7878-4eba-bc57-c850d58add33"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("35afde2a-7889-43e8-925c-09b65f05c849"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("397387de-29aa-47cd-83b1-2e1e242bbfed"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("3ab12432-5d93-496e-83c9-5929514f82b5"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("41ab5b04-f9f1-4e49-8004-246fe79f6136"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("424f2a29-f09d-43e9-a28e-7fd1750f4102"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("430f8382-69c8-473f-a74f-a244a77133ae"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("54e911e5-a9db-423a-b284-cadcd559b30b"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("58a0114b-e802-40d4-9b0b-7187b77eb30c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("5b858f9e-1f64-47b0-841f-14604a9d4035"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("64068786-c23f-4904-bc30-d297fe71eb6f"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("678c8fc3-8156-45f0-8a0a-04d747442cf9"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("6ca7d4d7-2d22-4481-b39d-890c515a20fe"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("6d761b94-26b6-4b15-a585-8c7dd15f8e4d"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("70ab156e-d382-4d9f-b48a-8efa1e74c2b7"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("77122595-d2ef-4b4b-8841-c6a5fb91acfe"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("8399bba9-b3eb-4e2b-9261-b6f38163f114"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("88f90070-9bfb-4722-a802-a58e4459f625"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("916d84a6-727a-4aa5-b742-562815b28297"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("99ebc170-e52d-4e52-b4ac-1abbbfb799a5"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9be0417a-8c93-4cde-9d6c-79a15996b5a3"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9fc97e4a-5b6c-4cbd-a4ce-35bfdb49bb05"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a145dfbd-eefe-411c-8d6d-3feaf0747231"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a6ebec19-67b5-4286-a634-a5994f332f4c"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a922340c-81bc-4189-9060-56b796ff954e"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("b10f29d8-addc-4fd2-81bb-b3084db31018"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("b59eaa64-45f5-4b90-83fa-16235af1e5e3"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("bc4766e4-fbb4-4384-b3b8-bb5c3d7cb0b0"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("c042cee5-b114-4fa3-8ba6-64b180ff128e"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("d71023e3-93b6-45a6-a5c0-1fdda2d4a1cf"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e53ce73a-3e50-4498-9c51-ef0df346a3c2"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e6e7723f-0b6c-4c37-b259-18de64d486cb"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("f7246fc8-8a97-4b0b-8fd6-21bb0287b6eb"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("f85c2129-dc48-494e-a551-6fa684e77142"));

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("0553ca62-284d-4379-afc5-c2d4903f7a4c"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1caa7fb0-8c2e-4304-a1ec-747a89623131"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("24ef70c3-0fc1-48d7-994f-380d4c533419"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2df1a80e-651a-417a-9028-b81d30a9a26e"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("7bc8ae1b-031a-4f3a-815c-2111288ff58c"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("9955f4d7-3e40-4111-a76d-23406f93334b"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("a9c15177-e1a4-4dc8-bcb0-5d78128fdeae"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("ad7d0cf7-ce00-477d-ae2a-5691f65eba0e"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e226d6b2-324f-4508-b5e5-0db77b345c69"),
                columns: new[] { "Description", "Name" },
                values: new object[] { "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.", "7Up" });

            migrationBuilder.UpdateData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("e3b2bcce-a8f4-4f7e-9c9e-6ac93e03554a"),
                column: "Description",
                value: "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Interdum varius sit amet mattis. Nec ullamcorper sit amet risus nullam eget felis eget nunc. Sapien eget mi proin sed libero enim. Nam at lectus urna duis. Volutpat maecenas volutpat blandit aliquam.");
        }
    }
}
