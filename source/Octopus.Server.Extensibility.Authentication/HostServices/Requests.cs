﻿
namespace Octopus.Server.Extensibility.Authentication.HostServices
{
    public static class Requests
    {
        public static bool IsLocalUrl(string absoluteVirtualDirectoryPath, string url, string corsWhitelist)
        {
            // Credit to Microsoft - Preventing Open Redirection Attacks (C#)
            // http://www.asp.net/mvc/overview/security/preventing-open-redirection-attacks

            var isLocalUrl = !string.IsNullOrEmpty(url) &&

                             // Allows full paths that start with the absoluteVirtualDirectoryPath
                             (url.StartsWith(absoluteVirtualDirectoryPath) ||

                             // Allows paths on the corsWhitelist, if one is defined
                             (!string.IsNullOrWhiteSpace(corsWhitelist) && (corsWhitelist == "*" || url.ToLower().StartsWith(corsWhitelist.ToLower()))) ||

                             // Allows "/" or "/foo" but not "//" or "/\".
                             (url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) ||

                             // Allows "~/" or "~/foo".
                             (url.Length > 1 && url[0] == '~' && url[1] == '/'));

            return isLocalUrl;
        }
    }
}