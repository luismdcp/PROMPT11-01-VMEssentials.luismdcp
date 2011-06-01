using System.Collections.Generic;
using System.IO;

namespace Sessao2
{
    class DirectoryInfoResolver : IPropertiesResolver
    {
        private DirectoryInfo di;

        public DirectoryInfoResolver(DirectoryInfo di)
        {
            this.di = di;
        }

        public IDictionary<string, object> GetPropertiesMap()
        {
            Dictionary<string, object> propertiesBuffer = new Dictionary<string, object>
                                                              {
                                                                  {"FullName", di.FullName},
                                                                  {"Name", di.Name},
                                                                  {"CreationTime", di.CreationTime},
                                                                  {"Subdirectories", di.GetDirectories()}
                                                              };
            return propertiesBuffer;
        }
    }
}