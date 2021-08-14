﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Export release environment.")]
    public class ReleaseEnvironmentExportCommand : CommandBase
    {
        public ReleaseEnvironmentExportCommand(ILogger<CommandBase> logger)
            : base(logger)
        {
        }

        [Option("-rid|--release-id", "Release id", CommandOptionType.SingleValue)]
        public int ReleaseId { get; set; }

        [Option("-eid|--environment-id", "Environment Id", CommandOptionType.SingleValue)]
        public int EnvironmentId { get; set; }

        [Option("--output-file", "File to export the release details. If this value is not provided the output will be the console.", CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.ReleaseId <= 0)
            {
                int.TryParse(Prompt.GetString("> ReleaseDefinitionId:", null, ConsoleColor.DarkGray), out int releaseDefinitionId);
                this.ReleaseId = releaseDefinitionId;
            }

            while (this.EnvironmentId <= 0)
            {
                int.TryParse(Prompt.GetString("> EnvironmentId:", null, ConsoleColor.DarkGray), out int environmentId);
                this.EnvironmentId = environmentId;
            }

            var result = this.DevOpsClient.Release.GetEnvironmentAsync(this.ProjectName, this.ReleaseId, this.EnvironmentId).GetAwaiter().GetResult();

            Console.WriteLine(result);

            return ExitCodes.Ok;
        }
    }
}
