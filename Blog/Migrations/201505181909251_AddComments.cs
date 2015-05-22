namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogComments",
                c => new
                    {
                        CommentId = c.Guid(nullable: false),
                        CommentDate = c.DateTime(nullable: false),
                        CommentAuthor = c.String(),
                        CommentBody = c.String(),
                        PostId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                //.ForeignKey("dbo.Blogs", t => t.PostId, cascadeDelete: true
                .Index(t => t.PostId);
            AddForeignKey("dbo.BlogComments", "PostId", "dbo.Blogs", "PostId", true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BlogComments", "PostId", "dbo.Blogs");
            DropIndex("dbo.BlogComments", new[] { "PostId" });
            DropTable("dbo.BlogComments");
        }
    }
}
