namespace Movies_Rental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        SignUpFee = c.Short(nullable: false),
                        DurationInMonths = c.Byte(nullable: false),
                        DiscountRate = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Byte(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "IsSubscribed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Customers", "MembershipTypeId", c => c.Int(nullable: false));
            AddColumn("dbo.Customers", "Birthdate", c => c.DateTime());
            AddColumn("dbo.Movies", "GenreId", c => c.Int(nullable: false));
            AddColumn("dbo.Movies", "ReleaseDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "DateAdded", c => c.DateTime(nullable: false));
            AddColumn("dbo.Movies", "NumberInStock", c => c.Short(nullable: false));
            AddColumn("dbo.Movies", "Genre_Id", c => c.Byte(nullable: false));
            AlterColumn("dbo.Customers", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Movies", "Name", c => c.String(nullable: false, maxLength: 50));
            CreateIndex("dbo.Customers", "MembershipTypeId");
            CreateIndex("dbo.Movies", "Genre_Id");
            AddForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Movies", "Genre_Id", "dbo.Genres", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Movies", "Genre_Id", "dbo.Genres");
            DropForeignKey("dbo.Customers", "MembershipTypeId", "dbo.MembershipTypes");
            DropIndex("dbo.Movies", new[] { "Genre_Id" });
            DropIndex("dbo.Customers", new[] { "MembershipTypeId" });
            AlterColumn("dbo.Movies", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Name", c => c.String());
            DropColumn("dbo.Movies", "Genre_Id");
            DropColumn("dbo.Movies", "NumberInStock");
            DropColumn("dbo.Movies", "DateAdded");
            DropColumn("dbo.Movies", "ReleaseDate");
            DropColumn("dbo.Movies", "GenreId");
            DropColumn("dbo.Customers", "Birthdate");
            DropColumn("dbo.Customers", "MembershipTypeId");
            DropColumn("dbo.Customers", "IsSubscribed");
            DropTable("dbo.Genres");
            DropTable("dbo.MembershipTypes");
        }
    }
}
