#region Copyright (c) Pixeval/Pixeval.CoreApi
// GPL v3 License
// 
// Pixeval/Pixeval.CoreApi
// Copyright (c) 2023 Pixeval.CoreApi/PixivApiHttpMessageHandler.cs
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.
#endregion

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Pixeval.CoreApi.Net;

internal class PixivApiHttpMessageHandler(MakoClient makoClient) : MakoClientSupportedHttpMessageHandler
{
    public sealed override MakoClient MakoClient { get; set; } = makoClient;

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var headers = request.Headers;
        var host = request.RequestUri!.Host; // the 'RequestUri' is guaranteed to be nonnull here, because the 'HttpClient' will set it to 'BaseAddress' if it's null

        var bypass = MakoHttpOptions.BypassRequiredHost.IsMatch(host) && MakoClient.Configuration.Bypass || host == MakoHttpOptions.OAuthHost;

        if (bypass)
        {
            MakoHttpOptions.UseHttpScheme(request);
        }

        _ = request.Headers.TryAddWithoutValidation("User-Agent", MakoClient.Configuration.UserAgent);
        _ = headers.TryAddWithoutValidation("Accept-Language", MakoClient.Configuration.CultureInfo.Name);

        var session = MakoClient.Session;

        switch (host)
        {
            case MakoHttpOptions.WebApiHost:
                _ = headers.TryAddWithoutValidation("Cookie", session.Cookie);
                break;
            case MakoHttpOptions.AppApiHost:
                headers.Authorization = new AuthenticationHeaderValue("Bearer", session.AccessToken);
                break;
        }

        return (MakoClient.Configuration.Bypass
                ? MakoClient.GetHttpMessageInvoker<PixivApiNameResolver>()
                : MakoClient.GetHttpMessageInvoker<LocalMachineNameResolver>())
            .SendAsync(request, cancellationToken);
    }
}
