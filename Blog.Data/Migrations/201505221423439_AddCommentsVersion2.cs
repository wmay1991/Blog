namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCommentsVersion2 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs");
            //RenameColumn(table: "dbo.BlogComments", name: "BlogId", newName: "PostId");
            //RenameIndex(table: "dbo.BlogComments", name: "IX_BlogId", newName: "IX_PostId");
            //DropPrimaryKey("dbo.Blogs");
            //AddColumn("dbo.Blogs", "PostId", c => c.Guid(nullable: false));
            //AddPrimaryKey("dbo.Blogs", "PostId");
            //AddForeignKey("dbo.BlogComments", "PostId", "dbo.Blogs", "PostId", cascadeDelete: true);
            //DropColumn("dbo.Blogs", "BlogId");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Blogs", "BlogId", c => c.Guid(nullable: false));
            //DropForeignKey("dbo.BlogComments", "PostId", "dbo.Blogs");
            //DropPrimaryKey("dbo.Blogs");
            //DropColumn("dbo.Blogs", "PostId");
            //AddPrimaryKey("dbo.Blogs", "BlogId");
            //RenameIndex(table: "dbo.BlogComments", name: "IX_PostId", newName: "IX_BlogId");
            //RenameColumn(table: "dbo.BlogComments", name: "PostId", newName: "BlogId");
            //AddForeignKey("dbo.BlogComments", "BlogId", "dbo.Blogs", "BlogId", cascadeDelete: true);
        }
    }
}
