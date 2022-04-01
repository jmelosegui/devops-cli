﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOpsCLI.Commands.Graph.Groups
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Jmelosegui.DevOps;
    using Jmelosegui.DevOps.Client;
    using McMaster.Extensions.CommandLineUtils;
    using Microsoft.Extensions.Logging;

    [Command("list", Description = "Gets a list of all groups in the current scope (usually organization or account).")]
    public sealed class GraphGroupListCommand : CommandBase
    {
        public GraphGroupListCommand(ILogger<GraphGroupListCommand> logger)
            : base(logger)
        {
        }

        [Option(
          "--scope-descriptor",
          "Specify a non-default scope (collection, project) to search for groups.",
          CommandOptionType.SingleValue)]
        public string ScopeDescriptor { get; set; }

        protected override int OnExecute(CommandLineApplication app)
        {
            base.OnExecute(app);

            GraphGroupListRequest request = null;
            if (!string.IsNullOrEmpty(this.ScopeDescriptor))
            {
                request = new GraphGroupListRequest
                {
                    ScopeDescriptor = this.ScopeDescriptor,
                };
            }

            IEnumerable<GraphGroup> list = this.DevOpsClient.Graph.GroupGetAllAsync(request).GetAwaiter().GetResult();

            Console.WriteLine();

            string json = JsonSerializer.Serialize(list);

            Console.WriteLine(json);

            return ExitCodes.Ok;
        }
    }
}
