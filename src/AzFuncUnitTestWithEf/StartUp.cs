using AzFuncUnitTestWithEf;
using AzFuncUnitTestWithEf.DataContext;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: FunctionsStartup(typeof(StartUp))]

namespace AzFuncUnitTestWithEf
{
    public class StartUp : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var services = builder.Services;
            var configuration = builder.GetContext().Configuration;

            services.AddDbContext<BookDataContext>(options => options.UseSqlServer(configuration["DbConnection"]));
        }
    }
}
