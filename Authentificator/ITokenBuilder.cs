using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authentificator
{
    public interface ITokenBuilder
    {
        string BuildToken();
    }
}
