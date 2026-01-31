using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;


namespace ConfigGuard
{

    public static class ConfigGuardExtensions
    {
        /// <summary>
        /// Binds configuration section to options, validates DataAnnotations,
        /// and optionally performs custom validation. Fails fast on startup.
        /// </summary>
        public static OptionsBuilder<TOptions> AddValidatedOptions<TOptions>(
            this IServiceCollection services,
            IConfiguration configuration,
            string sectionName,
            Action<TOptions>? customValidate = null,
            bool validateOnStart = true)
            where TOptions : class, new()
        {
            if (string.IsNullOrWhiteSpace(sectionName))
                throw new ArgumentException("Section name must be provided.", nameof(sectionName));

            var section = configuration.GetSection(sectionName);
            if (!section.Exists())
                throw new InvalidOperationException($"Required configuration section '{sectionName}' was not found.");

            var builder = services
                .AddOptions<TOptions>()
               // .Bind(section, options => options.BindNonPublicProperties = true)
                .Bind(section)
                .ValidateDataAnnotations();

            if (customValidate is not null)
            {
                builder.Validate(options =>
                {
                    try { customValidate(options); return true; }
                    catch { return false; }
                }, $"Custom validation failed for '{typeof(TOptions).Name}'.");
            }

            if (validateOnStart)
                builder.ValidateOnStart();

            return builder;
        }

        /// <summary>
        /// Validates DataAnnotations immediately and throws detailed errors.
        /// Helpful for manual access to validated options.
        /// </summary>
        public static TOptions GetValidated<TOptions>(this IConfiguration configuration, string sectionName)
            where TOptions : class, new()
        {
            var section = configuration.GetSection(sectionName);
            if (!section.Exists())
                throw new InvalidOperationException($"Required configuration section '{sectionName}' was not found.");

            var options = new TOptions();
            section.Bind(options);

            var context = new ValidationContext(options);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(options, context, results, validateAllProperties: true);

            if (!isValid)
                throw new OptionsValidationException(typeof(TOptions).Name, typeof(TOptions), (IEnumerable<string>?) results);

            return options;
        }

        /// <summary>
        /// Ensures required configuration paths exist at startup (sections or keys).
        /// </summary>
        public static IServiceCollection AddConfigPresenceChecks(
            this IServiceCollection services,
            IConfiguration configuration,
            params string[] requiredPaths)
        {
            services.AddSingleton<IHostedService>(_ => new ConfigPresenceHostedService(configuration, requiredPaths));
            return services;
        }

       
    private sealed class ConfigPresenceHostedService : IHostedService
        {
            private readonly IConfiguration _config;
            private readonly string[] _requiredPaths;

            public ConfigPresenceHostedService(IConfiguration config, string[] requiredPaths)
            {
                _config = config;
                _requiredPaths = requiredPaths ?? Array.Empty<string>();
            }

            public Task StartAsync(CancellationToken cancellationToken)
            {
                var missing = new List<string>();

                foreach (var path in _requiredPaths)
                {
                    if (string.IsNullOrWhiteSpace(path)) continue;

                    var section = _config.GetSection(path);
                    var value = _config[path];

                    if (!section.Exists() && value is null)
                        missing.Add(path);
                }

                if (missing.Count > 0)
                {
                    throw new InvalidOperationException(
                        "Missing required configuration paths:" + Environment.NewLine +
                        string.Join(Environment.NewLine, missing.Select(m => $"- {m}")));
                }

                return Task.CompletedTask;
            }

            public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
        }
    }
}