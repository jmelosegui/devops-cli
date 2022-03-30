// Copyright (c) All contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Jmelosegui.DevOps.Client
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VariableGroupApiClient : IVariableGroupApiClient
    {
        private const string EndPoint = "_apis/distributedtask/variablegroups";

        public VariableGroupApiClient(IConnection connection)
        {
            this.Connection = connection;
        }

        /// <summary>
        /// Gets the underlying connection.
        /// </summary>
        public IConnection Connection { get; private set; }

        public async Task<IEnumerable<VariableGroup>> GetAllAsync(string projectName, VariableGroupListRequest request = null)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1-preview.1" },
            };

            if (request != null)
            {
                FluentDictionary.For(parameters)
                                .Add("groupName", request.SearchText, () => !string.IsNullOrEmpty(request.SearchText));
            }

            var endPointUrl = new Uri($"{projectName}/{EndPoint}", UriKind.Relative);
            var response = await this.Connection
                                     .Get<GenericCollectionResponse<VariableGroup>>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            return response.Body.Values;
        }

        public async Task<string> GetAsync(string projectName, int variableGroupId)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1-preview.1" },
            };

            var endPointUrl = new Uri($"{projectName}/{EndPoint}/{variableGroupId}", UriKind.Relative);

            var response = await this.Connection
                                     .Get<string>(endPointUrl, parameters, null)
                                     .ConfigureAwait(false);

            return response.Body;
        }

        public async Task<string> AddOrUpdateAsync(string projectName, int variableGroupId, string jsonBody)
        {
            Ensure.ArgumentNotNullOrEmptyString(projectName, nameof(projectName));

            var parameters = new Dictionary<string, object>
            {
                { "api-version", "4.1-preview.1" },
            };
            Uri endPointUrl;
            IApiResponse<string> response;

            if (variableGroupId > 0)
            {
                endPointUrl = new Uri($"{projectName}/{EndPoint}/{variableGroupId}/", UriKind.Relative);
                response = await this.Connection
                                     .Put<string>(endPointUrl, jsonBody, parameters, null)
                                     .ConfigureAwait(false);
            }
            else
            {
                endPointUrl = new Uri($"{projectName}/{EndPoint}/", UriKind.Relative);
                response = await this.Connection
                                     .Post<string>(endPointUrl, jsonBody, parameters, null)
                                     .ConfigureAwait(false);
            }

            return response.Body;
        }
    }
}