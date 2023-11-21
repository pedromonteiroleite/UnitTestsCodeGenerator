using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestBuilder.Reflection
{
    public class PathGenerator
    {
        public string GeneratePathForMethod(Type targetType, string methodName)
        {
            if (targetType == null)
            {
                throw new ArgumentNullException(nameof(targetType));
            }

            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException(nameof(methodName));
            }

            // Get the method's declaring type (class) and namespace
            string methodNamespace = targetType.Namespace;

            if (string.IsNullOrEmpty(methodNamespace))
            {
                throw new InvalidOperationException("The target type does not belong to any namespace.");
            }

            // Combine the namespace, class name, and method name to create the path
            string path = $"{methodNamespace.Replace(".", "\\")}\\{targetType.Name}\\{methodName}";

            return path;
        }
    }
}
