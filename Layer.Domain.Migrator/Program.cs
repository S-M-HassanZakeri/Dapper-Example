using FluentMigrator.Runner;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace Layer.Domain.Migrator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var options = GetSettings(args, Directory.GetCurrentDirectory());
            var connectionString = options.ConnectionString;

            CreateDatabase(connectionString);
            CreateDatabaseUser(connectionString);

            var runner = CreateRunner(connectionString, options);
            runner.MigrateUp();

            Console.WriteLine("Press Any Key for End.");
            Console.ReadKey();
        }

        private static void CreateDatabaseUser(string connectionString)
        {
            var databaseName = GetDatabaseName(connectionString);
            string masterConnectionString = ChangeDatabaseName(connectionString, "master");
            var commandScript =
                $@"USE [{databaseName}]
                   IF (Not Exists (SELECT sl.name  
                                   FROM sys.sysusers su
                   	               join sys.syslogins sl on sl.sid = su.sid
                                   WHERE sl.name  = 'WHT_Admin_EVERST'))
                   Begin
                   	use [master]
                   	
                   	BEGIN TRY  
                   		CREATE LOGIN [WHT_Admin_EVERST] WITH PASSWORD=N'$ep!D_P$w', DEFAULT_DATABASE=[master], CHECK_EXPIRATION=OFF, CHECK_POLICY=OFF
                   		ALTER SERVER ROLE [sysadmin] ADD MEMBER [WHT_Admin_EVERST]
                   	END TRY  
                   	BEGIN CATCH  
                   		ALTER SERVER ROLE [sysadmin] ADD MEMBER [WHT_Admin_EVERST]
                   	END CATCH 
                   
                   	ALTER SERVER ROLE [sysadmin] ADD MEMBER [WHT_Admin_EVERST]
                   	use [master];
                   	USE [{databaseName}]
                   	CREATE USER [WHT_Admin_EVERST] FOR LOGIN [WHT_Admin_EVERST]
                   	USE [{databaseName}]
                   	ALTER ROLE [db_owner] ADD MEMBER [WHT_Admin_EVERST]
                   	Print N'Created User WHT_Admin_EVERST'
                   End
                   use [master]";

            using var connection = new SqlConnection(masterConnectionString);
            using var command = new SqlCommand(commandScript, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static void CreateDatabase(string connectionString)
        {
            var databaseName = GetDatabaseName(connectionString);
            string masterConnectionString = ChangeDatabaseName(connectionString, "master");
            var commandScript = $"if db_id(N'{databaseName}') is null create database [{databaseName}] COLLATE Persian_100_CI_AI";

            using var connection = new SqlConnection(masterConnectionString);
            using var command = new SqlCommand(commandScript, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }

        private static string ChangeDatabaseName(string connectionString, string databaseName)
        {
            var csb = new SqlConnectionStringBuilder(connectionString)
            {
                InitialCatalog = databaseName
            };
            return csb.ConnectionString;
        }

        private static string GetDatabaseName(string connectionString)
        {
            return new SqlConnectionStringBuilder(connectionString).InitialCatalog;
        }

        private static IMigrationRunner CreateRunner(string connectionString, MigrationSettings options)
        {
            var container = new ServiceCollection()
                .AddFluentMigratorCore()
                .ConfigureRunner(_ => _
                    .AddSqlServer()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(Program).Assembly).For.All())
                .AddSingleton<MigrationSettings>(options)
                .AddLogging(_ => _.AddFluentMigratorConsole())
                .BuildServiceProvider();
            return container.GetRequiredService<IMigrationRunner>();
        }

        private static MigrationSettings GetSettings(string[] args, string baseDir)
        {
            var configurations = new ConfigurationBuilder()
                .SetBasePath(baseDir)
                .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            var settings = new MigrationSettings();
            settings.ConnectionString = configurations.GetValue<string>("ConnectionString");
            return settings;
        }

        public class MigrationSettings
        {
            public string ConnectionString { get; set; }
        }
    }
}