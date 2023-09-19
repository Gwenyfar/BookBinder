using BookBinder.Application.Services;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Application.Data.Migrations
{
    [Migration(202318091705)]
    public class InitialMigration : ForwardOnlyMigration
    {
        public override void Up()
        {
            Create.Table("Users")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable().Indexed("IX_Users_Email")
                .WithColumn("UserName").AsString().Nullable()
                .WithColumn("PhoneNumber").AsString().Nullable();

            Create.Table("Authors")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable().Indexed("IX_Authors_Email");

            Create.Table("Admins")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("FirstName").AsString().NotNullable()
                .WithColumn("LastName").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable();

            Create.Table("Publishers")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("Company").AsString().NotNullable()
                .WithColumn("Address").AsString().NotNullable()
                .WithColumn("Email").AsString().NotNullable().Indexed("IX_Users_Email");

            Create.Table("Books")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("Id").AsGuid().Unique().NotNullable().PrimaryKey()
                .WithColumn("Title").AsString().NotNullable()
                .WithColumn("ISBN").AsString().NotNullable()
                .WithColumn("Genre").AsString().NotNullable()
                .WithColumn("PublishedDate").AsDateTime().NotNullable()
                .WithColumn("Publisher_id").AsGuid().Nullable();

            Create.ForeignKey("Fk_Books_Publishers")
                .FromTable("Books")
                .InSchema(Bootstrapper.SCHEMA)
                .ForeignColumn("Publisher_id")
                .ToTable("Publishers")
                .InSchema(Bootstrapper.SCHEMA)
                .PrimaryColumn("Id");

            Create.Table("AuthorToBook")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("Author_id").AsGuid().NotNullable()
                .WithColumn("Book_id").AsGuid().NotNullable();

            Create.Table("UserToBook")
                .InSchema(Bootstrapper.SCHEMA)
                .WithColumn("User_id").AsGuid().NotNullable()
                .WithColumn("Book_id").AsGuid().NotNullable();

            Create.ForeignKey("Fk_AuthorToBook_Author_id")
                .FromTable("AuthorToBook")
                .InSchema(Bootstrapper.SCHEMA)
                .ForeignColumn("Author_id")
                .ToTable("Authors")
                .InSchema (Bootstrapper.SCHEMA)
                .PrimaryColumn ("Id");

            Create.ForeignKey("Fk_AuthorToBook_Book_id")
                .FromTable("AuthorToBook")
                .InSchema(Bootstrapper.SCHEMA)
                .ForeignColumn("Book_id")
                .ToTable("Books")
                .InSchema(Bootstrapper.SCHEMA)
                .PrimaryColumn("Id");

            Create.ForeignKey("Fk_UserToBook_User_id")
                .FromTable("UserToBook")
                .InSchema(Bootstrapper.SCHEMA)
                .ForeignColumn("User_id")
                .ToTable("Users")
                .InSchema(Bootstrapper.SCHEMA)
                .PrimaryColumn("Id");

            Create.ForeignKey("Fk_UserToBook_Book_id")
                .FromTable("UserToBook")
                .InSchema(Bootstrapper.SCHEMA)
                .ForeignColumn("Book_id")
                .ToTable("Books")
                .InSchema(Bootstrapper.SCHEMA)
                .PrimaryColumn("Id");

            Create.PrimaryKey("Pk_Author_Book_id")
                .OnTable("AuthorToBook")
                .WithSchema(Bootstrapper.SCHEMA)
                .Columns("Author_id", "Book_id");

            Create.PrimaryKey("Pk_User_Book_id")
                .OnTable("UserToBook")
                .WithSchema(Bootstrapper.SCHEMA)
                .Columns("User_id", "Book_id");
        }
    }
}
