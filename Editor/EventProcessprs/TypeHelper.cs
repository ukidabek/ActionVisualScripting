using System;
using System.Linq;

namespace ActionVisualScripting
{
    public class TypeHelper
    {
        public static Type[] GetDerivedTypes(Type baseType, bool isAbstract = false)
        {
            Type[] types = baseType.Assembly.GetTypes();
            return types.Where(type => (type.IsSubclassOf(baseType) && type.IsAbstract == isAbstract)).ToArray();
        }
    }
}