// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace XamarinIdentity.Auth
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                //new ApiScope("dataEventRecords", "Scope for the dataEventRecords ApiResource"),
                //new ApiScope("securedFiles",  "Scope for the securedFiles ApiResource")
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                //new ApiResource("dataEventRecordsApi")
                //{
                //    ApiSecrets =
                //    {
                //        new Secret("dataEventRecordsSecret".Sha256())
                //    },
                //    Scopes = new List<string> { "dataEventRecords" }
                //},
                //new ApiResource("securedFilesApi")
                //{
                //    ApiSecrets =
                //    {
                //        new Secret("securedFilesSecret".Sha256())
                //    },
                //    Scopes = new List<string> { "securedFiles" }
                //}
            };
        }

        public static IEnumerable<Client> GetClients(IConfigurationSection stsConfig)
        {
            return new List<Client>
            {
                // mobile client
                new Client
                {
                    ClientName = "mobileclient",
                    ClientId = "gnabbermobileclient",
                    // AccessTokenType = AccessTokenType.Jwt,
                    // AccessTokenLifetime = 330,// 330 seconds, default 60 minutes
                    // IdentityTokenLifetime = 30,
                    AllowedGrantTypes = GrantTypes.Code,
                    // AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true, // allow refresh tokens
                    AbsoluteRefreshTokenLifetime = 2592000, // in seconds ~> 30 days
                    RequireClientSecret = false,
                    RedirectUris = new List<string>
                    {
                        "oidcxamarin://authenticated"

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                        "oidcxamarin://signout-callback-oidc",
                    },
                    // AllowedCorsOrigins = new List<string>
                    // {
                    //     "https://localhost:44311",
                    //     "http://localhost:44311"
                    // },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "role",
                        "profile",
                        "email"
                    }
                }
            };
        }
    }
}