﻿// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial interface IGraphApiClient
    {
        Task<GraphDescriptor> DescriptorGetAsync(string storageKey);

        Task<GraphGroup> GroupGetAsync(string groupDescriptor);

        Task<IEnumerable<GraphGroup>> GroupGetAllAsync(GraphGroupListRequest request);

        Task<GraphUser> UserGetAsync(string userDescriptor);

        Task<IEnumerable<GraphUser>> UserGetAllAsync(GraphUserListRequest request);

        Task<IEnumerable<GraphMembership>> MembershipGetAllAsync(GraphMembershipListRequest request);

        Task<GraphServicePrincipal> ServicePrincipalGetAsync(string servicePrincipalDescriptor);

        Task<IEnumerable<GraphServicePrincipal>> ServicePrincipalGetAllAsync(GraphServicePrincipalListRequest request);
    }
}
