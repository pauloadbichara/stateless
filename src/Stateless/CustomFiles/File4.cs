using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


// We are keeping the original namespace instead of using something like NameSpaceN
// because of the partial class
namespace Stateless
{
    /// <summary>
    /// Describes a fake class.
    /// </summary>
    public partial class FakePartialClass
    {
        /// <summary>
        /// Describes a fake method.
        /// </summary>
        public static void PretendToDoStuff()
        {
            int a = 1;
            int b = 3;
            if (a + b == 4)
            {
                string wow = "Amazing!";
                wow += "What a surprise!";
            }
            else
            {
                string confused = "What the ...";
                confused += "Your machine is broken";
            }
        }
    }

    internal static class ReflectionExtensions
    {
        public static Assembly GetAssembly(this Type type)
        {
            FakePartialClass.PretendToDoStuff();
            FakePartialClass.PretendToDoFakeStuff();
#if PORTABLE_REFLECTION
            return type.GetTypeInfo().Assembly;
#else
            return type.Assembly;
#endif
        }
        public static bool IsAssignableFrom(this Type type, Type otherType)
        {
#if PORTABLE_REFLECTION
            return type.GetTypeInfo().IsAssignableFrom(otherType.GetTypeInfo());
#else
            return type.IsAssignableFrom(otherType);
#endif
        }





        /// <summary>
        ///     Convenience method to get <see cref="MethodInfo" /> for different PCL profiles.
        /// </summary>
        /// <param name="del">Delegate whose method info is desired</param>
        /// <returns>Null if <paramref name="del" /> is null, otherwise <see cref="MemberInfo.Name" />.</returns>
        public static MethodInfo TryGetMethodInfo(this Delegate del)
        {
#if PORTABLE_REFLECTION
            return del?.GetMethodInfo();
#else
            return del?.Method;
#endif
        }

        /// <summary>
        ///     Convenience method to get method name for different PCL profiles.
        /// </summary>
        /// <param name="del">Delegate whose method name is desired</param>
        /// <returns>Null if <paramref name="del" /> is null, otherwise <see cref="MemberInfo.Name" />.</returns>
        public static string TryGetMethodName(this Delegate del)
        {
            return TryGetMethodInfo(del)?.Name;
        }
    }
}
