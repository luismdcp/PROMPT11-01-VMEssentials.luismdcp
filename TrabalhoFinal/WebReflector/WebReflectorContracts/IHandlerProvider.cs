using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebReflectorContracts
{
    public interface IHandlerProvider
    {
        string ContextRootPath { get; set; }
        string Domain { get; set; }
        string Port { get; set; }

        IResponseView ConstructorsHandler(string ctx, string ns, string shortName);
        IResponseView FieldHandler(string ctx, string ns, string shortName, string fieldName);
        IResponseView PropertyHandler(string ctx, string ns, string shortName, string propName);
        IResponseView EventHandler(string ctx, string ns, string shortName, string eventName);
        IResponseView MethodHandler(string ctx, string ns, string shortName, string methodName);
        IResponseView TypeHandler(string ctx, string ns, string shortName);
        IResponseView NamespaceHandler(string ctx, string namespacePrefix);
        IResponseView AssemblyHandler(string ctx, string assemblyName);
        IResponseView ContextNamespacesHandler(string ctx);
        IResponseView ContextAssembliesHandler(string ctx);
        IResponseView ContextHandler(string ctx);
        IResponseView ContextRootHandler();
    }
}