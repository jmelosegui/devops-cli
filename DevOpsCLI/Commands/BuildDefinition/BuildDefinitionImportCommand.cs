﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands
{
    using System;
    using System.IO;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("import", Description = "Imports a BuildDefinition.")]
    public class BuildDefinitionImportCommand : ProjectCommandBase
    {
        public BuildDefinitionImportCommand(ApplicationConfiguration settings, ILogger<BuildDefinitionImportCommand> logger)
            : base(settings, logger)
        {
        }

        [Option(
            "-bdid|--build-definition-id",
            "Build definition id. if this value is not provided the import command will create a new build definition group otherwise it will attempt to update the build definition with the provided identifier.",
            CommandOptionType.SingleValue)]
        public int BuildDefinitionId { get; set; }

        [Option(
            "--input-file",
            "File containing the variable group details to add or update on the target project.",
            CommandOptionType.SingleValue)]
        public string InputFile { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            while (this.NonInteractive == false && string.IsNullOrEmpty(this.InputFile))
            {
                this.InputFile = Prompt.GetString("> InputFile:", null, ConsoleColor.DarkGray);
            }

            if (!File.Exists(this.InputFile))
            {
                throw new FileNotFoundException("Specified input file cannot be found", this.InputFile);
            }

            string jsonBody = File.ReadAllText(this.InputFile);

            string variableGroup = this.DevOpsClient.BuildDefinition.AddOrUpdateAsync(this.ProjectName, this.BuildDefinitionId, jsonBody).GetAwaiter().GetResult();

            Console.WriteLine(variableGroup);

            return ExitCodes.Ok;
        }
    }
}
