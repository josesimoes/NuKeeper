using McMaster.Extensions.CommandLineUtils;
using NuKeeper.Configuration;
using NuKeeper.Inspection.Logging;
using System.IO;

namespace NuKeeper.Commands
{
    internal abstract class LocalNuKeeperCommand : CommandBase
    {
        [Argument(0, Description = "The path to a .sln or .csproj file or .nfproj, or to a directory containing a .NET Core solution/project. " +
                                   "If none is specified, the current directory will be used.")]
        // ReSharper disable once UnassignedGetOnlyAutoProperty
        // ReSharper disable once MemberCanBePrivate.Global
        protected string Path { get; }

        protected LocalNuKeeperCommand(IConfigureLogger logger, IFileSettingsCache fileSettingsCache)
            : base(logger, fileSettingsCache)
        {
        }

        protected override ValidationResult PopulateSettings(SettingsContainer settings)
        {
            var baseResult = base.PopulateSettings(settings);
            if (!baseResult.IsSuccess)
            {
                return baseResult;
            }

            if (! string.IsNullOrWhiteSpace(Path) && ! Directory.Exists(Path))
            {
                return ValidationResult.Failure($"Path '{Path}' was not found");
            }

            settings.UserSettings.Directory = Path;
            return ValidationResult.Success;
        }
    }
}
