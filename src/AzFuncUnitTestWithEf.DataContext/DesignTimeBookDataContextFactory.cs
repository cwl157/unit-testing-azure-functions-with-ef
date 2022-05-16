using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AzFuncUnitTestWithEf.DataContext
{
    public sealed class DesignTimeBookDataContextFactory : IDesignTimeDbContextFactory<BookDataContext>
    {
        BookDataContext IDesignTimeDbContextFactory<BookDataContext>.CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<BookDataContext>();
            var dbContext = new BookDataContext(builder.Options);

            return dbContext;
        }
    }
}
