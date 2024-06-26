﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client.Models
{
    public sealed class PipelineCreateRequest
    {
        public string Name { get; set; }

        public string Folder { get; set; }

        public PipelineConfiguration Configuration { get; set; }
    }
}
