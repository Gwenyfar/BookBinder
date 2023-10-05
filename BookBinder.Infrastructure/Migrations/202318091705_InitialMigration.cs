using BookBinder.Infrastructure.DataBaseConfiguration;
using BookBinder.Infrastructure.Utilities;
using FluentMigrator;

namespace BookBinder.Infrastructure.Migrations
{
    [Migration(202318091705)]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .InSchema(Database.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable().Indexed("IX_Users_Email")
                .WithColumn("UserName").AsString().Nullable()
                .WithColumn("PhoneNumber").AsString().Nullable();

            Create.Table("Authors")
                .InSchema(Database.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable().Indexed("IX_Authors_Email");

            Create.Table("Admins")
                .InSchema(Database.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable();

            Create.Table("Publishers")
                .InSchema(Database.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("Company").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable().Indexed("IX_Users_Email");

            Create.Table("Books")
                .InSchema(Database.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("ISBN").AsString().NotNullable()
                .WithColumn("Genre").AsString().NotNullable()
                .WithColumn("PublishedDate").AsDateTime().NotNullable()
                .WithColumn("Publisher_id").AsGuid().Nullable();

            Create.ForeignKey("Fk_Books_Publishers")
                .FromTable("Books")
                .InSchema(Database.SCHEMA)
                .ForeignColumn("Publisher_id")
                .ToTable("Publishers")
                .InSchema(Database.SCHEMA)
                .PrimaryColumn("Id");

            Create.Table("AuthorToBook")
                .InSchema(Database.SCHEMA)
                .WithColumn("Author_id").AsGuid().NotNullable()
                .WithColumn("Book_id").AsGuid().NotNullable();

            Create.Table("UserToBook")
                .InSchema(Database.SCHEMA)
                .WithColumn("User_id").AsGuid().NotNullable()
                .WithColumn("Book_id").AsGuid().NotNullable();

            Create.ForeignKey("Fk_AuthorToBook_Author_id")
                .FromTable("AuthorToBook")
                .InSchema(Database.SCHEMA)
                .ForeignColumn("Author_id")
                .ToTable("Authors")
                .InSchema (Database.SCHEMA)
                .PrimaryColumn ("Id");

            Create.ForeignKey("Fk_AuthorToBook_Book_id")
                .FromTable("AuthorToBook")
                .InSchema(Database.SCHEMA)
                .ForeignColumn("Book_id")
                .ToTable("Books")
                .InSchema(Database.SCHEMA)
                .PrimaryColumn("Id");

            Create.ForeignKey("Fk_UserToBook_User_id")
                .FromTable("UserToBook")
                .InSchema(Database.SCHEMA)
                .ForeignColumn("User_id")
                .ToTable("Users")
                .InSchema(Database.SCHEMA)
                .PrimaryColumn("Id");

            Create.ForeignKey("Fk_UserToBook_Book_id")
                .FromTable("UserToBook")
                .InSchema(Database.SCHEMA)
                .ForeignColumn("Book_id")
                .ToTable("Books")
                .InSchema(Database.SCHEMA)
                .PrimaryColumn("Id");

            Create.PrimaryKey("Pk_Author_Book_id")
                .OnTable("AuthorToBook")
                .WithSchema(Database.SCHEMA)
                .Columns("Author_id", "Book_id");

            Create.PrimaryKey("Pk_User_Book_id")
                .OnTable("UserToBook")
                .WithSchema(Database.SCHEMA)
                .Columns("User_id", "Book_id");
        }
    }
}
