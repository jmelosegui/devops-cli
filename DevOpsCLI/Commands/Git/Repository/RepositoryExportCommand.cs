﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("export", Description = "Retrieve a git repository.")]
    public sealed class RepositoryExportCommand : ProjectCommandBase
    {
        public RepositoryExportCommand(ApplicationConfiguration settings, ILogger<RepositoryExportCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
        "--repositoryId",
        "The name or ID of the repository.",
        CommandOptionType.SingleValue)]
        public string RepositoryId { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.NonInteractive == false && !string.IsNullOrEmpty(this.RepositoryId))
            {
                this.RepositoryId = Prompt.GetString("> Pipeline Id", null, ConsoleColor.DarkGray);
            }

            var result = this.DevOpsClient.Git.RepositoryGetAsync(this.ProjectName, this.RepositoryId).GetAwaiter().GetResult();

            this.PrintOrExport(result);

            return ExitCodes.Ok;
        }
    }
}
