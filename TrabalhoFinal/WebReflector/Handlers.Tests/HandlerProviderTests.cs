using NUnit.Framework;
using WebReflectorContracts;
using WebReflectorViews;
using WebReflectorViews.Assembly;
using WebReflectorViews.Context;
using WebReflectorViews.TypeViews;

namespace Handlers.Tests
{
    [TestFixture]
    public class HandlerProviderTests
    {
        #region "/"

        [Test]
        public void Context_root_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
                                           {
                                               ContextRootPath = @"c:\NonExistent",
                                               Domain = "localhost",
                                               Port = "8080"
                                           };

            IResponseView resultView = provider.ContextRootHandler();
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_root_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextRootHandler();
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_root_handler_with_a_non_empty_contexts_directory_returns_a_ContextRootView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextRootHandler();
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(ContextRootView));
        }

        #endregion

        #region "/{ctx}"

        [Test]
        public void Context_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_handler_with_a_non_empty_contexts_directory_returns_a_ContextView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(ContextView));
        }

        #endregion

        #region "/{ctx}/as"

        [Test]
        public void Context_assemblies_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextAssembliesHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_assemblies_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextAssembliesHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_assemblies_handler_with_a_non_empty_contexts_directory_returns_a_ContextAssembliesView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextAssembliesHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(ContextAssembliesView));
        }

        #endregion

        #region "/{ctx}/ns"

        [Test]
        public void Context_namespaces_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextNamespacesHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_namespaces_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextNamespacesHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Context_namespaces_handler_with_a_non_empty_contexts_directory_returns_a_ContextNamespacesView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ContextNamespacesHandler("Ctx");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(ContextNamespacesView));
        }

        #endregion

        #region "/{ctx}/as/{assemblyName}"

        [Test]
        public void Assembly_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.AssemblyHandler("Ctx", "mscorlib");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Assembly_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.AssemblyHandler("Ctx", "mscorlib");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Assembly_handler_with_a_non_empty_contexts_directory_returns_a_AssemblyView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.AssemblyHandler("Ctx", "mscorlib");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(AssemblyView));
        }

        [Test]
        public void Assembly_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_assembly_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.AssemblyHandler("Ctx", "NonExistenAssembly");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion

        #region "/{ctx}/ns/{namespacePrefix}")

        [Test]
        public void Namespace_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.NamespaceHandler("Ctx", "System");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Namespace_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.NamespaceHandler("Ctx", "System");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Namespace_handler_with_a_non_empty_contexts_directory_returns_a_NamespaceView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.NamespaceHandler("Ctx", "System");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NamespaceView));
        }

        [Test]
        public void Namespace_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.NamespaceHandler("Ctx", "NonExistentNamespace");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion

        #region "/{ctx}/ns/{namespace}/{shortName}"

        [Test]
        public void Type_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.TypeHandler("Ctx", "System", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Type_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.TypeHandler("Ctx", "System", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Type_handler_with_a_non_empty_contexts_directory_returns_a_TypeView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.TypeHandler("Ctx", "System", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(TypeView));
        }

        [Test]
        public void Type_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_type_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.TypeHandler("Ctx", "System", "NonExistentType");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Type_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.TypeHandler("Ctx", "NonExistentnamespace", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion

        #region "/{ctx}/ns/{namespace}/{shortName}/m/{methodName}"

        [Test]
        public void Method_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.MethodHandler("Ctx", "System", "Int32", "ToString");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Method_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.MethodHandler("Ctx", "System", "Int32", "ToString");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Method_handler_with_a_non_empty_contexts_directory_returns_a_MethodView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.MethodHandler("Ctx", "System", "Int32", "ToString");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(MethodView));
        }

        [Test]
        public void Method_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_method_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.MethodHandler("Ctx", "System", "Int32", "NonExistentMethod");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Method_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.MethodHandler("Ctx", "NonExistentnamespace", "Int32", "ToString");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Method_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_type_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.MethodHandler("Ctx", "System", "NonExistentType", "ToString");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion
         
        #region "/{ctx}/ns/{namespace}/{shortName}/e/{eventName}"

        [Test]
        public void Event_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.EventHandler("Ctx", "System", "Exception", "SerializeObjectState");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Event_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.EventHandler("Ctx", "System", "Exception", "SerializeObjectState");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Event_handler_with_a_non_empty_contexts_directory_returns_a_EventView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.EventHandler("Ctx", "System", "Exception", "SerializeObjectState");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(EventView));
        }

        [Test]
        public void Event_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_event_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.EventHandler("Ctx", "System", "Exception", "NonExistentEvent");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Event_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.EventHandler("Ctx", "NonExistentNamespace", "Exception", "SerializeObjectState");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Event_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_type_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.EventHandler("Ctx", "System", "NonExistentType", "SerializeObjectState");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion

        #region "/{ctx}/ns/{namespace}/{shortName}/f/{fieldName}"

        [Test]
        public void Field_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.FieldHandler("Ctx", "System", "Int32", "MaxValue");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Field_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.FieldHandler("Ctx", "System", "Int32", "MaxValue");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Field_handler_with_a_non_empty_contexts_directory_returns_a_FieldView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.FieldHandler("Ctx", "System", "Int32", "MaxValue");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(FieldView));
        }

        [Test]
        public void Field_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_event_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.FieldHandler("Ctx", "System", "Int32", "NonExistentField");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Field_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.FieldHandler("Ctx", "NonExistentNamespace", "Int32", "MaxValue");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Field_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_type_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.FieldHandler("Ctx", "System", "NonExistentType", "MaxValue");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion

        #region "/{ctx}/ns/{namespace}/{shortName}/p/{propName}"

        [Test]
        public void Prop_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.PropertyHandler("Ctx", "System", "ActivationContext", "Identity");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Prop_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.PropertyHandler("Ctx", "System", "ActivationContext", "Identity");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Prop_handler_with_a_non_empty_contexts_directory_returns_a_PropView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.PropertyHandler("Ctx", "System", "ActivationContext", "Identity");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(PropertyView));
        }

        [Test]
        public void Prop_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_property_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.PropertyHandler("Ctx", "System", "ActivationContext", "NonExistentProp");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Prop_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.PropertyHandler("Ctx", "NonExistentNamespace", "ActivationContext", "Identity");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Prop_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_type_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.PropertyHandler("Ctx", "System", "NonExistentType", "Identity");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion

        #region "/{ctx}/ns/{namespace}/{shortName}/c"

        [Test]
        public void Constructor_handler_with_a_nonexistent_context_path_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Nonexistent",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ConstructorsHandler("Ctx", "System", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Constructor_handler_with_an_empty_contexts_directory_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\EmptyContexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ConstructorsHandler("Ctx", "System", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Constructor_handler_with_a_non_empty_contexts_directory_returns_a_ConstructorView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ConstructorsHandler("Ctx", "System", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(ConstructorsView));
        }

        [Test]
        public void Constructor_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_type_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ConstructorsHandler("Ctx", "System", "NonExistentType");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        [Test]
        public void Constructor_handler_with_a_non_empty_contexts_directory_and_a_nonexistent_namespace_returns_a_NotFoundView()
        {
            HandlerProvider provider = new HandlerProvider
            {
                ContextRootPath = @"c:\Contexts",
                Domain = "localhost",
                Port = "8080"
            };

            IResponseView resultView = provider.ConstructorsHandler("Ctx", "NonExistentnamespace", "Int32");
            Assert.IsNotNull(resultView);
            Assert.IsTrue(resultView.GetType() == typeof(NotFoundView));
        }

        #endregion
    }
}