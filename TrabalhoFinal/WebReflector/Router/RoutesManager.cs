using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using WebReflectorContracts;
using WebReflectorContracts.Attributes;
using WebReflectorViews;

namespace Router
{
    public class RoutesManager
    {
        public IDictionary<string, MethodInfo> MethodRegistry { get; private set; }
        public IDictionary<Regex, string> RegexRegistry { get; private set; }
        public IHandlerProvider HandlerProvider { get; private set; }

        public RoutesManager(IHandlerProvider handlerProvider)
        {
            this.MethodRegistry = new Dictionary<string, MethodInfo>();
            this.RegexRegistry = new Dictionary<Regex, string>();
            this.HandlerProvider = handlerProvider;
        }

        public bool RegisterHandlerProvider(out IResponseView errorView)
        {
            if (this.HandlerProvider == null)
            {
                errorView = new InternalErrorView("É necessário obter um HanlderProvider não nulo.");
                return false;
            }

            var attributes = this.HandlerProvider.GetType().GetCustomAttributes(typeof(HandlerProviderAttribute), true);

            if (attributes.Length > 0)
            {
                foreach (var tuple in GetMethodInfosAndCustomAttribute(this.HandlerProvider.GetType()))
                {
                    var pattern = ((HandlerAttribute) tuple.Item2).Pattern;

                    this.MethodRegistry.Add(pattern, tuple.Item1);
                    Regex regex = BuildRegexFromPattern(pattern);
                    this.RegexRegistry.Add(regex, pattern);
                }
            }
            else
            {
                errorView = new InternalErrorView(String.Format("A classe '{0}' não é um válido HandlerProvider.", this.HandlerProvider.GetType().Name));
                return false;
            }

            errorView = null;
            return true;
        }

        public IResponseView Execute(string patternContent)
        {
            List<string> groupMatchBuffer = new List<string>();
            Regex matchedRegex = null;

            foreach (var regex in this.RegexRegistry.Keys)
            {
                if (regex.IsMatch(patternContent))
                {
                    matchedRegex = regex;
                    Match match = regex.Match(patternContent);

                    for (int i = 1; i < match.Groups.Count; i++)
                    {
                        groupMatchBuffer.Add(match.Groups[i].Value);
                    }

                    break;
                }
            }

            if (matchedRegex == null)
            {
                return new InternalErrorView(String.Format("Não existe um método do Handler registado para processar o URI '{0}'.", patternContent));
            }

            MethodInfo matchedMethodInfo = this.MethodRegistry[this.RegexRegistry[matchedRegex]];
            var parameterCount = matchedMethodInfo.GetParameters().Length;

            if (parameterCount != groupMatchBuffer.Count)
            {
                return new InternalErrorView(String.Format("O método '{0}' registado não é um handler válido para o URI '{1}'.", matchedMethodInfo.Name, patternContent));
            }

            return (IResponseView) matchedMethodInfo.Invoke(this.HandlerProvider, groupMatchBuffer.ToArray());
        }

        #region Helper methods

        private static IEnumerable<Tuple<MethodInfo, Attribute>> GetMethodInfosAndCustomAttribute(Type type)
        {
            foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy))
            {
                if (methodInfo.GetCustomAttributes(true).Count(at => at is HandlerAttribute) > 0)
                {
                    var customAttribute = methodInfo.GetCustomAttributes(typeof(HandlerAttribute), true);
                    yield return new Tuple<MethodInfo, Attribute>(methodInfo, (HandlerAttribute) customAttribute[0]);
                }
            }

            yield break;
        }

        private static Regex BuildRegexFromPattern(string pattern)
        {
            List<string> literalsBuffer = new List<string> { "as", "ns", "p", "e", "f", "c", "m", "/" };
            StringBuilder regexPattern = new StringBuilder();
            StringBuilder buffer = new StringBuilder();

            for (int i = 0; i < pattern.Length; i++)
            {
                if (literalsBuffer.Contains(pattern[i].ToString()))
                {
                    regexPattern.Append(pattern[i].ToString());
                }
                else
                {
                    if (pattern[i] == '{')
                    {
                        while (pattern[i] != '}')
                        {
                            buffer.Append(pattern[i]);
                            i++;
                        }

                        buffer.Append('}');

                        string group = String.Format(@"(?<{0}>[A-Z-a-z0-9_.]+)", buffer.ToString().Substring(1, buffer.Length - 2));
                        buffer.Clear();
                        regexPattern.Append(group);
                    }
                    else
                    {
                        string literal = String.Format("{0}{1}", pattern[i], pattern[i + 1]);

                        if (i <= pattern.Length - 2 && literalsBuffer.Contains(literal))
                        {
                            regexPattern.Append(literal);
                            i = i + 1;
                        }
                    }
                }
            }

            return new Regex(regexPattern.ToString());
        }

        #endregion
    }
}