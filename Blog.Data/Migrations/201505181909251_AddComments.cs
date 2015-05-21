namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Blogs");
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                        CommentAuthor = c.String(),
                        CommentBody = c.String(),
                        BlogId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Blogs", t => t.BlogId, cascadeDelete: true)
                .Index(t => t.BlogId);
            
            AddColumn("dbo.Blogs", "BlogId", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Blogs", "BlogId");
            DropColumn("dbo.Blogs", "PostId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Blogs", "PostId", c => c.Guid(nullable: false));
            DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            DropIndex("dbo.BlogComments", new[] { "BlogId" });
            DropPrimaryKey("dbo.Blogs");
            DropColumn("dbo.Blogs", "BlogId");
            DropTable("dbo.BlogComments");
            AddPrimaryKey("dbo.Blogs", "PostId");
        }
    }
}
