using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;


namespace DotNet.Testing.TestData.ExampleTests
{
    public class ApiFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
    {
        private readonly Action<IServiceCollection>? _overrideServices;

        public ApiFactory(Action<IServiceCollection>? overrideServices = null)
        {
            _overrideServices = overrideServices;
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Testing");

            builder.ConfigureServices(services =>
            {
                _overrideServices?.Invoke(services);
            });
        }
    }
}