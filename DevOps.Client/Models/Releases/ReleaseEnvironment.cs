﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class ReleaseEnvironment
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public EnvironmentStatus Status { get; set; }
    }
}
