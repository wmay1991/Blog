namespace Blog.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeToPost : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.BlogComments", newName: "PostComments");
            RenameTable(name: "dbo.Blogs", newName: "Posts");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Posts", newName: "Blogs");
            RenameTable(name: "dbo.PostComments", newName: "BlogComments");
        }
    }
}
