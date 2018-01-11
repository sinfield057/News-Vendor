using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class UrlCheck
{
    public UrlCheck()
    {
        
    }

    public bool checkUrl( string url )
    {
        Uri uriResult;
        bool result = false;

        result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        return result;
    }
}