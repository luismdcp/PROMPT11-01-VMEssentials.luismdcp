using System.Collections.Generic;

namespace Sessao2
{
    interface IPropertiesResolver
    {
        IDictionary<string, object> GetPropertiesMap();
    }
}