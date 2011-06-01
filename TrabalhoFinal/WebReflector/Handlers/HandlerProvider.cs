using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using WebReflectorContracts;
using WebReflectorContracts.Attributes;
using WebReflectorViews;

namespace Handlers
{
    [HandlerProvider]
    public class HandlerProvider : IHandlerProvider
    {
        public string ContextRootPath { get; set; }
        public string Domain { get; set; }
        public string Port { get; set; }

        private List<string> namespacesCache;

        public HandlerProvider()
        {
            this.namespacesCache = new List<string>();
        }

        [Handler("/{ctx}/ns/{namespace}/{shortName}/c")]
        public IResponseView ConstructorsHandler(string ctx, string ns, string shortName)
        {
            Type matchingType = this.CheckType(ctx, ns, shortName, this.ContextRootPath);

            if (matchingType != null)
            {
                var constructorsInfo = matchingType.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                return new ConstructorsView(constructorsInfo, ctx, ns, shortName, this.ContextRootPath, this.Domain, this.Port);
            }
            else
            {
                return new NotFoundView(String.Format("O tipo '{0}' não está definido no Namespace '{1} e no Contexto '{2}'.", shortName, ns, ctx));
            }
        }

        [Handler("/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}")]
        public IResponseView FieldHandler(string ctx, string ns, string shortName, string fieldName)
        {
            Type matchingType = this.CheckType(ctx, ns, shortName, this.ContextRootPath);

            if (matchingType != null)
            {
                var fieldInfo = matchingType.GetField(fieldName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                if (fieldInfo != null)
                {
                    return new FieldView(fieldInfo, ctx, ns, shortName, this.ContextRootPath, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O campo '{0}' não está definido no tipo '{1}' pertencente ao Namespace '{2}' e ao Contexto '{3}'.", fieldName, shortName, ns, ctx));
                }
            }
            else
            {
                return new NotFoundView(String.Format("O tipo '{0}' não está definido no Namespace '{1} e no Contexto '{2}'.", shortName, ns, ctx));
            }
        }

        [Handler("/{ctx}/ns/{namespace}/{shortName}/p/{propName}")]
        public IResponseView PropertyHandler(string ctx, string ns, string shortName, string propName)
        {
            Type matchingType = this.CheckType(ctx, ns, shortName, this.ContextRootPath);

            if (matchingType != null)
            {
                var propertyInfo = matchingType.GetProperty(propName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                if (propertyInfo != null)
                {
                    return new PropertyView(propertyInfo, ctx, ns, shortName, propName, this.ContextRootPath, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("A propriedade '{0}' não está definida no tipo '{1}' pertencente ao Namespace '{2}' e ao Contexto '{3}'.", propName, shortName, ns, ctx));
                }
            }
            else
            {
                return new NotFoundView(String.Format("O tipo '{0}' não está definido no Namespace '{1} e no Contexto '{2}'.", shortName, ns, ctx));
            }
        }

        [Handler("/{ctx}/ns/{namespace}/{shortName}/e/{eventName}")]
        public IResponseView EventHandler(string ctx, string ns, string shortName, string eventName)
        {
            Type matchingType = this.CheckType(ctx, ns, shortName, this.ContextRootPath);

            if (matchingType != null)
            {
                var eventInfo = matchingType.GetEvent(eventName, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);

                if (eventInfo != null)
                {
                    return new EventView(eventInfo, ctx, ns, shortName, eventInfo.Name, this.ContextRootPath, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O evento '{0}' não está definido no tipo '{1}' pertencente ao Namespace '{2}' e ao Contexto '{3}'.", eventName, shortName, ns, ctx));
                }
            }
            else
            {
                return new NotFoundView(String.Format("O tipo '{0}' não está definido no Namespace '{1} e no Contexto '{2}'.", shortName, ns, ctx));
            }
        }

        [Handler("/{ctx}/ns/{namespace}/{shortName}/m/{methodName}")]
        public IResponseView MethodHandler(string ctx, string ns, string shortName, string methodName)
        {
            Type matchingType = this.CheckType(ctx, ns, shortName, this.ContextRootPath);

            if (matchingType != null)
            {
                var methodsInfo = matchingType.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy);
                var methodsInfoQuery = methodsInfo.Where(mi => mi.Name == methodName);

                if (methodsInfoQuery.Count() > 0)
                {
                    return new MethodView(methodsInfoQuery, ctx, ns, shortName, methodName, this.ContextRootPath, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O método '{0}' não está definido no tipo '{1}' pertencente ao Namespace '{2}' e ao Contexto '{3}'.", methodName, shortName, ns, ctx));
                }
            }
            else
            {
                return new NotFoundView(String.Format("O tipo '{0}' não está definido no Namespace '{1} e no Contexto '{2}'.", shortName, ns, ctx));
            }
        }

        [Handler("/{ctx}/ns/{namespace}/{shortName}")]
        public IResponseView TypeHandler(string ctx, string ns, string shortName)
        {
            Type matchingType = this.CheckType(ctx, ns, shortName, this.ContextRootPath);

            if (matchingType != null)
            {
                return new TypeView(matchingType, ctx, ns, shortName, this.ContextRootPath, this.Domain, this.Port);
            }
            else
            {
                return new NotFoundView(String.Format("O tipo '{0}' não está definido no Namespace '{1} e no Contexto '{2}'.", shortName, ns, ctx));
            }
        }

        [Handler("/{ctx}/ns/{namespacePrefix}")]
        public IResponseView NamespaceHandler(string ctx, string namespacePrefix)
        {
            try
            {
                if (Directory.Exists(this.ContextRootPath + @"\" + ctx))
                {
                    var subNamespaces = this.GetAllSubNamespaces(this.ContextRootPath, ctx, namespacePrefix);
                    var shortNames = this.GetTypesDefinedInNamespace(this.ContextRootPath, ctx, namespacePrefix);

                    return new NamespaceView(subNamespaces, shortNames, ctx, namespacePrefix, this.ContextRootPath, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O directório '{0}' não existe.", this.ContextRootPath + @"\" + ctx));
                }
            }
            catch (Exception ex)
            {
                return new InternalErrorView("Erro ao processar os contextos.");
            }
        }

        [Handler("/{ctx}/as/{assemblyName}")]
        public IResponseView AssemblyHandler(string ctx, string assemblyName)
        {
            try
            {
                if (Directory.Exists(this.ContextRootPath + @"\" + ctx))
                {
                    var assembly = this.GetAssembly(ctx, assemblyName, this.ContextRootPath);
                    var publicKeyBytes = assembly.GetName().GetPublicKey();
                    var publicKeyBuffer = new StringBuilder();

                    foreach (var charByte in publicKeyBytes)
                    {
                        publicKeyBuffer.AppendFormat("{0:x}", charByte);
                    }

                    if (assembly != null)
                    {
                        var typesGroupedByNamespace = this.GetAssemblyTypes(assembly);
                        return new AssemblyView(assembly.GetName().Name,
                                                publicKeyBuffer.ToString(),
                                                typesGroupedByNamespace, ctx, assemblyName, this.ContextRootPath, this.Domain,
                                                this.Port);
                    }
                    else
                    {
                        return new NotFoundView(String.Format("A assembly '{0}' não está definida no contexto '{1}'.", assemblyName, ctx));
                    }
                }
                else
                {
                    return new NotFoundView(String.Format("O directório '{0}' não existe.", this.ContextRootPath + @"\" + ctx));
                }
            }
            catch (Exception ex)
            {
                return new InternalErrorView("Erro ao processar os contextos.");
            }
        }

        [Handler("/{ctx}/ns")]
        public IResponseView ContextNamespacesHandler(string ctx)
        {
            try
            {
                if (Directory.Exists(this.ContextRootPath + @"\" + ctx))
                {
                    var namespaces = this.GetAllNamespaces(this.ContextRootPath, ctx);
                    return new ContextNamespaces(namespaces, ctx, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O directório '{0}' não existe.", this.ContextRootPath + @"\" + ctx));
                }
            }
            catch (Exception ex)
            {
                return new InternalErrorView("Erro ao processar os contextos.");
            }
        }

        [Handler("/{ctx}/as")]
        public IResponseView ContextAssembliesHandler(string ctx)
        {
            try
            {
                if (Directory.Exists(this.ContextRootPath + @"\" + ctx))
                {
                    var assemblies = this.GetAllAssemblies(this.ContextRootPath, ctx);
                    return new ContextAssembliesView(assemblies, ctx, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O directório '{0}' não existe.", this.ContextRootPath + @"\" + ctx));
                }
            }
            catch (Exception ex)
            {
                return new InternalErrorView("Erro ao processar os contextos.");
            }
        }

        [Handler("/{ctx}")]
        public IResponseView ContextHandler(string ctx)
        {
            try
            {
                if (Directory.Exists(this.ContextRootPath + @"\" + ctx))
                {
                    var assemblies = this.GetAllAssemblies(this.ContextRootPath, ctx);
                    var namespaces = this.GetAllNamespaces(this.ContextRootPath, ctx);

                    return new ContextView(ctx, this.Domain, this.Port);
                }
                else
                {
                    return new NotFoundView(String.Format("O directório '{0}' não existe.", this.ContextRootPath + @"\" + ctx));
                }
            }
            catch (Exception ex)
            {
                return new InternalErrorView("Erro ao processar os contextos.");
            }
        }

        [Handler("/")]
        public IResponseView ContextRootHandler()
        {
            try
            {
                if (Directory.Exists(this.ContextRootPath))
                {
                    var contexts = this.GetAllDirectories(this.ContextRootPath);

                    if (contexts.Count() > 0)
                    {
                        return new ContextRootView(contexts, this.Domain, this.Port);
                    }
                    else
                    {
                        return new NotFoundView(String.Format("Não existem contextos definidos no directório '{0}'.", this.ContextRootPath));
                    }
                }
                else
                {
                    return new NotFoundView(String.Format("O directório '{0}' não existe.", this.ContextRootPath));
                }
            }
            catch (Exception)
            {
                return new InternalErrorView("Erro ao processar os contextos.");
            }
        }

        #region Helper Methods

        private IEnumerable<string> GetAllSubNamespaces(string contextRootPath, string ctx, string rootNamespace)
        {
            return this.GetAllNamespaces(contextRootPath, ctx).Where(ns => ns.StartsWith(rootNamespace) && ns != rootNamespace);
        }

        private IEnumerable<string> GetAllDirectories(string contextRootPath)
        {
            if (Directory.Exists(contextRootPath))
            {
                return Directory.GetDirectories(contextRootPath + @"\", "*", SearchOption.TopDirectoryOnly);
            }
            else
            {
                return new List<string>();
            }
        }

        private Assembly GetAssembly(string ctx, string assemblyName, string contextRootPath)
        {
            IEnumerable<Assembly> assemblies = this.LoadAssemblies(String.Format("{0}\\{1}", contextRootPath, ctx));
            Assembly asly = null;

            foreach (var assembly in assemblies)
            {
                if (assembly.GetName().Name == assemblyName)
                {
                    asly = assembly;
                }
            }

            return asly;
        }

        private IEnumerable<IGrouping<string, Type>> GetAssemblyTypes(Assembly asly)
        {
            return asly.GetExportedTypes().GroupBy(t => t.Namespace);
        }

        private IEnumerable<string> GetAllNamespaces(string contextRootPath, string ctx)
        {
            if (this.namespacesCache.Count == 0)
            {
                IEnumerable<Assembly> assemblies = this.LoadAssemblies(String.Format("{0}\\{1}", contextRootPath, ctx));

                foreach (var assembly in assemblies)
                {
                    this.namespacesCache.AddRange(assembly.GetExportedTypes().Select(t => t.Namespace).Distinct());
                }
            }

            return this.namespacesCache.AsReadOnly().Distinct();
        }

        private IEnumerable<string> GetAllAssemblies(string contextRootPath, string ctx)
        {
            List<string> buffer = new List<string>();
            IEnumerable<Assembly> assemblies = this.LoadAssemblies(String.Format("{0}\\{1}", contextRootPath, ctx));

            foreach (var assembly in assemblies)
            {
                buffer.Add(assembly.GetName().Name);
            }

            return buffer;
        }

        private IEnumerable<string> GetTypesDefinedInNamespace(string contextRootPath, string ctx, string rootNamespace)
        {
            List<string> buffer = new List<string>();
            IEnumerable<Assembly> assemblies = this.LoadAssemblies(String.Format("{0}\\{1}", contextRootPath, ctx));

            foreach (var assembly in assemblies)
            {
                buffer.AddRange(assembly.GetExportedTypes().Where(t => t.Namespace == rootNamespace).Select(t => t.Name));
            }

            return buffer.Distinct();
        }

        private IEnumerable<Assembly> LoadAssemblies(string contextPath)
        {
            DirectoryInfo directory = new DirectoryInfo(contextPath);
            FileInfo[] files = directory.GetFiles("*.dll", SearchOption.TopDirectoryOnly);

            foreach (FileInfo file in files)
            {
                AssemblyName assemblyName = AssemblyName.GetAssemblyName(file.FullName);
                Assembly assembly = AppDomain.CurrentDomain.Load(assemblyName);
                yield return assembly;
            }

            yield break;
        }

        private Type CheckType(string ctx, string ns, string shortName, string contextRootPath)
        {
            IEnumerable<Assembly> assemblies = this.LoadAssemblies(String.Format("{0}\\{1}", contextRootPath, ctx));
            Type matchingType = null;

            foreach (var assembly in assemblies)
            {
                var matchingTypeQuery = assembly.GetExportedTypes().Where(t => t.Namespace == ns && t.Name == shortName);

                if (matchingTypeQuery.Count() > 0)
                {
                    matchingType = matchingTypeQuery.First();
                    break;
                }
            }

            return matchingType;
        }

        #endregion
    }
}