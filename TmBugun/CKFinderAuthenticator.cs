using CKSource.CKFinder.Connector.Core;
using CKSource.CKFinder.Connector.Core.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace TmBugun
{
    public class CKFinderAuthenticator : IAuthenticator
    {
        public Task<IUser> AuthenticateAsync(ICommandRequest commandRequest, CancellationToken cancellationToken)
        {
            var user = new User(true, new List<String>());
            return Task.FromResult((IUser)user);
        }

    }
}