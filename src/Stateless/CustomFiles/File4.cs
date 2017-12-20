using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Stateless.NameSpace5
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
            AnotherClass anotherClass = new AnotherClass();
            anotherClass.PretendToDoStuff();
        }
    }

    /// <summary>
    /// Describes a fake class.
    /// </summary>
    public class AnotherClass
    {
        /// <summary>
        /// Describes a fake method.
        /// </summary>
        public int PretendToDoStuff()
        {
            int a = 1;
            int b = 3;
            return a + b;
        }
    }

}
