using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authentificator
{
    public interface ITokenValidator
    {
        bool ValidateToken(string appToken, string usrToken);
    }
}
