using Gazin.Portal.Integrations.Jira.Configurations;
using Gazin.Portal.Integrations.Jira.Data.Dtos;
using Gazin.Portal.Integrations.Jira.Exceptions;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Gazin.Portal.Integrations.Jira
{
    public abstract class BaseJiraIntegration
    {
        private const string AUTHENTICATION_TYPE_BASIC = "Basic";
        private const string MEDIA_TYPE_JSON = "application/json";
        private const string HEAD_X_ACCOUNT_ID = "x-aaccountid";
        private const string HEAD_X_ATLASSIAN_TOKEN = "X-Atlassian-Token";
        private const string HEAD_X_ATLASSIAN_TOKEN_NO_CHECK = "no-check";
        private const string HEAD_X_FORCE_ACCEPT_LANGUAGE = "X-Force-Accept-Language";
        private const string HEAD_X_FORCE_ACCEPT_LANGUAGE_FALSE = "false";

        private static readonly JsonSerializerOptions JSON_OPTIONS = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = false
        };

        private readonly IJiraConfiguration jwtConfiguration;

        public BaseJiraIntegration(IJiraConfiguration jwtConfiguration)
        {
            this.jwtConfiguration = jwtConfiguration;
        }

        protected abstract string URL { get; set; }

        protected string GetRoute()
            => $"{jwtConfiguration.Hostname}{URL}";

        protected static HttpClient CreateInstance(string username, string password)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MEDIA_TYPE_JSON));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AUTHENTICATION_TYPE_BASIC, GetAuthenticationByConfig(username, password));
            client.DefaultRequestHeaders.Add(HEAD_X_ATLASSIAN_TOKEN, HEAD_X_ATLASSIAN_TOKEN_NO_CHECK);
            client.DefaultRequestHeaders.Add(HEAD_X_FORCE_ACCEPT_LANGUAGE, HEAD_X_FORCE_ACCEPT_LANGUAGE_FALSE);
            return client;
        }

        private static string GetAuthenticationByConfig(string username, string password)
            => Convert.ToBase64String(Encoding.ASCII.GetBytes($"{username}:{password}"));



        protected async Task<AuthenticationDto> AuthenticationAsync(string username,
                                                                    string password,
                                                                    CancellationToken cancellationToken)
        {
            var client = CreateInstance(username, password);
            var response = await client.GetAsync(GetRoute(), cancellationToken);
            return await GetAuthenticationResponse(response);
        }

        private static async Task<AuthenticationDto> GetAuthenticationResponse(HttpResponseMessage response)
        {
            await GetResponseError(response);

            var accountId = string.Empty;
            if (response.Headers.TryGetValues(HEAD_X_ACCOUNT_ID, out var values))
                accountId = values.First();

            return new AuthenticationDto
            {
                AccountId = accountId
            };
        }

        protected async Task<TResponse> GetAsync<TResponse>(string username,
                                                            string password,
                                                            CancellationToken cancellationToken)
        {
            var client = CreateInstance(username, password);
            var response = await client.GetAsync(GetRoute(), cancellationToken);
            return await GetResponse<TResponse>(response);
        }

        protected async Task<TResponse> PostAsync<TRequest, TResponse>(string username,
                                                                       string password,
                                                                       TRequest request,
                                                                       CancellationToken cancellationToken)
        {
            var client = CreateInstance(username, password);
            var content = MountContent(request);
            var response = await client.PostAsync(GetRoute(), content, cancellationToken);
            return await GetResponse<TResponse>(response);
        }

        protected async Task<TResponse> PutAsync<TRequest, TResponse>(string username,
                                                                      string password,
                                                                      TRequest request,
                                                                      CancellationToken cancellationToken)
        {
            var client = CreateInstance(username, password);
            var content = MountContent(request);
            var response = await client.PutAsync(GetRoute(), content, cancellationToken);
            return await GetResponse<TResponse>(response);
        }

        protected async Task<bool> DeleteAsyncIsOk(string username,
                                                   string password,
                                                   CancellationToken cancellationToken)
        {
            var client = CreateInstance(username, password);
            var response = await client.DeleteAsync(GetRoute(), cancellationToken);
            return response.StatusCode.Equals(HttpStatusCode.NoContent);
        }

        private static StringContent MountContent<T>(T data)
        {
            var json = JsonSerializer.Serialize<T>(data, JSON_OPTIONS);
            return new StringContent(json, Encoding.UTF8, MEDIA_TYPE_JSON);
        }

        private static async Task<TResponse> GetResponse<TResponse>(HttpResponseMessage response)
        {
            await GetResponseError(response);

            return await GetDeserializeResponse<TResponse>(response);
        }

        private static async Task GetResponseError(HttpResponseMessage response)
        {
            if (IsStatusOK(response)|| IsStatusCreated(response) || IsStatusNoContent(response))
                return;

            var erros = await GetDeserializeResponse<ErrorsDto>(response);

            throw new JiraException(string.Join("\n", erros));
        }

        private static async Task<TResponse> GetDeserializeResponse<TResponse>(HttpResponseMessage response)
            => JsonSerializer.Deserialize<TResponse>(await response.Content.ReadAsStringAsync(), JSON_OPTIONS);

        private static bool IsStatusOK(HttpResponseMessage response)
            => response.StatusCode.Equals(HttpStatusCode.OK);
        private static bool IsStatusCreated(HttpResponseMessage response)
            => response.StatusCode.Equals(HttpStatusCode.Created);

        private static bool IsStatusNoContent(HttpResponseMessage response)
            => response.StatusCode.Equals(HttpStatusCode.NoContent);
    }
}