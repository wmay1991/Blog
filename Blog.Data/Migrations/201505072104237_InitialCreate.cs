namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        PostId = c.Guid(nullable: false),
                        PostTitle = c.String(),
                        PostAuthor = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        PostTease = c.String(),
                        PostBody = c.String(),
                    })
                .PrimaryKey(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Blogs");
        }
    }
}
