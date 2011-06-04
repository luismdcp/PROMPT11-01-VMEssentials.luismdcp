using System;
using Handlers;
using NUnit.Framework;
using WebReflectorContracts;
using WebReflectorViews.Assembly;
using WebReflectorViews.Context;
using WebReflectorViews.TypeViews;

namespace RoutesManager.Tests
{
    class InvalidHandlerProvider : IHandlerProvider
    {

        public string ContextRootPath
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Domain
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Port
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public IResponseView ConstructorsHandler(string ctx, string ns, string shortName)
        {
            throw new NotImplementedException();
        }

        public IResponseView FieldHandler(string ctx, string ns, string shortName, string fieldName)
        {
            throw new NotImplementedException();
        }

        public IResponseView PropertyHandler(string ctx, string ns, string shortName, string propName)
        {
            throw new NotImplementedException();
        }

        public IResponseView EventHandler(string ctx, string ns, string shortName, string eventName)
        {
            throw new NotImplementedException();
        }

        public IResponseView MethodHandler(string ctx, string ns, string shortName, string methodName)
        {
            throw new NotImplementedException();
        }

        public IResponseView TypeHandler(string ctx, string ns, string shortName)
        {
            throw new NotImplementedException();
        }

        public IResponseView NamespaceHandler(string ctx, string namespacePrefix)
        {
            throw new NotImplementedException();
        }

        public IResponseView AssemblyHandler(string ctx, string assemblyName)
        {
            throw new NotImplementedException();
        }

        public IResponseView ContextNamespacesHandler(string ctx)
        {
            throw new NotImplementedException();
        }

        public IResponseView ContextAssembliesHandler(string ctx)
        {
            throw new NotImplementedException();
        }

        public IResponseView ContextHandler(string ctx)
        {
            throw new NotImplementedException();
        }

        public IResponseView ContextRootHandler()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    public class RoutesManagerTests
    {
        [Test]
        public void RoutesManager_registers_with_success_a_proper_HandlerProvider_Class_with_the_custom_attributes()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            Assert.IsNull(errorView);
            Assert.IsTrue(result);
        }

        [Test]
        public void RoutesManager_registers_doesnt_register_a_null_HandlerProvider_Class()
        {
            HandlerProvider provider = null;
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            Assert.IsNotNull(errorView);
            Assert.IsFalse(result);
        }

        [Test]
        public void RoutesManager_registers_doesnt_register_an_invalid_HandlerProvider_Class_without_the_custom_attributes()
        {
            IHandlerProvider provider = new InvalidHandlerProvider();
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            Assert.IsNotNull(errorView);
            Assert.IsFalse(result);
        }

        [Test]
        public void RoutesManager_executes_and_returns_ContextRootView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/");
            Assert.IsTrue(resultView.GetType() == typeof(ContextRootView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_ContextView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx");
            Assert.IsTrue(resultView.GetType() == typeof(ContextView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_ContextAssembliesView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/as");
            Assert.IsTrue(resultView.GetType() == typeof(ContextAssembliesView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_ContextNamespacesView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns");
            Assert.IsTrue(resultView.GetType() == typeof(ContextNamespacesView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_AssemblyView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/as/mscorlib");
            Assert.IsTrue(resultView.GetType() == typeof(AssemblyView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_NamespaceView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System");
            Assert.IsTrue(resultView.GetType() == typeof(NamespaceView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_TypeView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System/Int32");
            Assert.IsTrue(resultView.GetType() == typeof(TypeView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_ConstructorsView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System/Int32/c");
            Assert.IsTrue(resultView.GetType() == typeof(ConstructorsView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_EventView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System/Exception/e/SerializeObjectState");
            Assert.IsTrue(resultView.GetType() == typeof(EventView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_FieldView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System/Int32/f/MaxValue");
            Assert.IsTrue(resultView.GetType() == typeof(FieldView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_MethodView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System/Int32/m/ToString");
            Assert.IsTrue(resultView.GetType() == typeof(MethodView));
        }

        [Test]
        public void RoutesManager_executes_and_returns_PropertyView()
        {
            HandlerProvider provider = new HandlerProvider(@"C:\Contexts", "localhost", "8080");
            var router = new Router.RoutesManager(provider);
            IResponseView errorView = null;
            var result = router.RegisterHandlerProvider(out errorView);

            var resultView = router.Execute("/Ctx/ns/System/ActivationContext/p/Identity");
            Assert.IsTrue(resultView.GetType() == typeof(PropertyView));
        }
    }
}