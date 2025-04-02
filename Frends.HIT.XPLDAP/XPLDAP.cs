using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.DirectoryServices.Protocols;
using System.Net;
using System.Security.AccessControl;
using System.Threading;

try
{
    var conn = new LdapConnection("SERVER");
    conn.SessionOptions.ReferralChasing = ReferralChasingOptions.None;

    NetworkCredential credential =
        new NetworkCredential("USER", "PASS", "DOMAIN");

    conn.Credential = credential;

    conn.AuthType = AuthType.Basic;

    LdapSessionOptions options = conn.SessionOptions;

    options.ProtocolVersion = 3;

    try
    {
        conn.Bind();
        Console.WriteLine("Bind succeeded using basic " +
            "authentication and SSL.\n");

        Console.WriteLine("Complete another task over " + "this SSL connection");
    }
    catch (LdapException e)
    {
        Console.WriteLine(e.Message);
    }

    var request = new SearchRequest("BASE", "FILTER", SearchScope.Subtree);

    request.Controls.Add(new PageResultRequestControl(10));
    var response = conn.SendRequest(request);

    Console.WriteLine(response.MatchedDN);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

