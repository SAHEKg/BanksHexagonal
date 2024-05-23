using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace BanksHexagonal.Infrastructure.DataAccess;

[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    create type transaction_type as enum
    (
        'add',
        'take'
    );   

    create table accounts
    (
        account_number int not null primary key,
        pin int not null
    );
    
    create table account_balance
    (
        account_number int not null primary key references accounts(account_number),
        balance int not null
    );

    create table transaction_history
    (
        id int primary key generated always as identity,
        account_number int not null references accounts(account_number),
        transaction_type transaction_type not null,
        sum int not null
    )
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table accounts;
    drop table account_balance;
    drop table transaction_history;
    """;
}