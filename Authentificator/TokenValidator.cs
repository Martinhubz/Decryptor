using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Authentificator
{
    public class TokenValidator : ITokenValidator
    {
        public bool ValidateToken(string appToken, string usrToken)
        {
            return true;
        }
    }
}