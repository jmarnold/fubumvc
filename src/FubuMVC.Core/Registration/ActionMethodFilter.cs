using System;
using System.Reflection;
using FubuCore.Reflection;
using FubuCore.Util;

namespace FubuMVC.Core.Registration
{
    public class ActionMethodFilter : CompositeFilter<MethodInfo>
    {
        public ActionMethodFilter()
        {
            Excludes += method => method.DeclaringType == typeof(object);
            Excludes += method => method.DeclaringType == typeof(MarshalByRefObject);
            Excludes += method => method.Name == ReflectionHelper.GetMethod<IDisposable>(x => x.Dispose()).Name;
            Excludes += method => method.ContainsGenericParameters;

            Excludes += method => method.IsSpecialName;
        }

        public void IgnoreMethodsDeclaredBy<T>()
        {
            Excludes += x => x.DeclaringType == typeof (T);
        }
    }
}