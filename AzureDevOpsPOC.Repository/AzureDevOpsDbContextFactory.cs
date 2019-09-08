using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace AzureDevOpsPOC.Repository
{
    public class AzureDevOpsDbContextFactory : IDesignTimeDbContextFactory<AzureDevOpsDbContext>
    {
        private static string _connectionString = @"Server=.\SQLEXPRESS;Database=POC_AzureDevOps;Trusted_Connection=True;MultipleActiveResultSets=true";

        public AzureDevOpsDbContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public AzureDevOpsDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AzureDevOpsDbContext>();
            builder.UseSqlServer(_connectionString);

            return new AzureDevOpsDbContext(builder.Options);
        }
    }
}
