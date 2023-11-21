using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks; // Add using for Task and Task<T>
using AutoFixture;

public class BaseTestClass
{
    protected IFixture Fixture { get; } = new Fixture();
}

public class TestClassGenerator : BaseTestClass
{
    public string GenerateTestClass(Type targetType, string methodName)
    {
        if (targetType == null)
        {
            throw new ArgumentNullException(nameof(targetType));
        }

        if (string.IsNullOrEmpty(methodName))
        {
            throw new ArgumentNullException(nameof(methodName));
        }

        // Get the MethodInfo for the specified method name
        MethodInfo methodInfo = targetType.GetMethod(methodName);

        if (methodInfo == null)
        {
            throw new ArgumentException($"Method '{methodName}' not found in type '{targetType.FullName}'.");
        }

        // Start building the test class code
        var testClassCode = $"using System;\nusing Xunit;\nusing AutoFixture;\n";
        testClassCode += $"using System.Threading.Tasks;\n"; // Add using statement for Task and Task<T>
        testClassCode += $"public class {targetType.Name}Tests\n";
        testClassCode += "{\n";

        // Create the method parameters region
        testClassCode += "\t#region Method Parameters\n";

        // Get the parameters of the method
        ParameterInfo[] parameters = methodInfo.GetParameters();

        foreach (var parameter in parameters)
        {
            // Generate a private member with _ prefix for each parameter
            var paramName = parameter.Name;
            var paramType = parameter.ParameterType.Name;
            testClassCode += $"\tprivate {paramType} _{char.ToLowerInvariant(paramName[0])}{paramName.Substring(1)};\n";
        }

        testClassCode += "\t#endregion\n\n";

        // Create the output value region if the method has output parameters
        if (parameters.Any(p => p.IsOut))
        {
            testClassCode += "\t#region Output Value\n";
            foreach (var parameter in parameters.Where(p => p.IsOut))
            {
                var paramName = parameter.Name;
                var paramType = parameter.ParameterType.Name;
                testClassCode += $"\tprivate {paramType} _{char.ToLowerInvariant(paramName[0])}{paramName.Substring(1)};\n";
            }
            testClassCode += "\t#endregion\n\n";
        }

        // Create the return value region if the method has a return value
        if (methodInfo.ReturnType != typeof(void))
        {
            var returnTypeName = methodInfo.ReturnType.Name;

            // Check if the return type is Task<T> and use the inner type
            if (methodInfo.ReturnType.IsGenericType && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                returnTypeName = methodInfo.ReturnType.GetGenericArguments()[0].Name;
            }

            testClassCode += $"\t#region Return Value\n";
            testClassCode += $"\tprivate {returnTypeName} _{char.ToLowerInvariant(returnTypeName[0])}{returnTypeName.Substring(1)};\n";
            testClassCode += $"\t#endregion\n\n";
        }

        // Create the constructor
        testClassCode += $"\tpublic {targetType.Name}Tests()\n";
        testClassCode += "\t{\n";

        // Initialize private members inside the constructor
        foreach (var parameter in parameters)
        {
            var paramName = parameter.Name;
            testClassCode += $"\t\t_{char.ToLowerInvariant(paramName[0])}{paramName.Substring(1)} = Fixture.Create<{parameter.ParameterType.Name}>();\n";
        }

        // Initialize return value
        if (methodInfo.ReturnType != typeof(void))
        {
            var returnTypeName = methodInfo.ReturnType.Name;

            if (methodInfo.ReturnType.IsGenericType && methodInfo.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
            {
                var innerType = methodInfo.ReturnType.GetGenericArguments()[0].Name;
                testClassCode += $"\t\t_{char.ToLowerInvariant(innerType[0])}{innerType.Substring(1)} = Fixture.Create<{innerType}>();\n";
            }
            else
            {
                testClassCode += $"\t\t_{char.ToLowerInvariant(returnTypeName[0])}{returnTypeName.Substring(1)} = Fixture.Create<{methodInfo.ReturnType.Name}>();\n";
            }
        }

        testClassCode += "\t}\n\n";

        // Add test methods (you can customize this part as needed)
        testClassCode += "\t// Add your test methods here\n";

        // Close the class
        testClassCode += "}\n";

        return testClassCode;
    }
}
