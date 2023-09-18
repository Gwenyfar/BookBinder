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

            Create.ForeignKey()


        }
    }
}
