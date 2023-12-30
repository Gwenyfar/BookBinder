using BookBinder.Infrastructure.DataBaseConfiguration;
using FluentMigrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Migrations
{
    [Migration(202320120510)]
    public class IncludePassword : ForwardOnlyMigration
    {
        public override void Up()
        {
            Alter.Table("Users")
                .InSchema(Database.SCHEMA)
                .AddColumn("PasswordHash").AsString().NotNullable().WithDefaultValue("defaultPasswordhash")
                .AddColumn("Role").AsString().NotNullable().WithDefaultValue("User");

            Alter.Table("Authors")
                .InSchema(Database.SCHEMA)
                .AddColumn("UserName").AsString().Nullable()
                .AddColumn("PhoneNumber").AsString().Nullable()
                .AddColumn("PasswordHash").AsString().NotNullable().WithDefaultValue("defaultPasswordhash")
                .AddColumn("Role").AsString().NotNullable().WithDefaultValue("User");

            Alter.Table("Admins")
                .InSchema(Database.SCHEMA)
                .AddColumn("UserName").AsString().Nullable()
                .AddColumn("PhoneNumber").AsString().Nullable()
                .AddColumn("PasswordHash").AsString().NotNullable().WithDefaultValue("defaultPasswordhash")
                .AddColumn("Role").AsString().NotNullable().WithDefaultValue("User");

            Alter.Table("Publishers")
                .InSchema(Database.SCHEMA)
                .AddColumn("UserName").AsString().Nullable()
                .AddColumn("PhoneNumber").AsString().Nullable()
                .AddColumn("PasswordHash").AsString().NotNullable().WithDefaultValue("defaultPasswordhash")
                .AddColumn("Role").AsString().NotNullable().WithDefaultValue("User");
        }
    }
}
