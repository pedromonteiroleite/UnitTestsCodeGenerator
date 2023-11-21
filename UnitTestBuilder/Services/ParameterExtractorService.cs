using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Reflection;
using UnitTestBuilder.Domain.Entities;
using UnitTestBuilder.Interfaces.Services;

namespace UnitTestBuilder.Services
{
    public class ParameterExtractorService: IParameterExtractorService
    {

        public List<MethodParameter> ExtractInputParameters(MethodInfo methodInfo)
        {
            if(methodInfo == null) 
                throw new ArgumentNullException(nameof(methodInfo));

            var methodParameters = new List<MethodParameter>();

            // Use Roslyn to parse the method's source code and extract input parameters
            var methodSyntax = CSharpSyntaxTree.ParseText(methodInfo.ToString()).GetRoot().DescendantNodes().OfType<MethodDeclarationSyntax>().FirstOrDefault();

            if (methodSyntax != null)
            {
                foreach (var parameter in methodSyntax.ParameterList.Parameters)
                {
                    // Extract parameter information and add it to the list
                    var parameterInfo = new MethodParameter
                    {
                        Name = parameter.Identifier.ValueText,
                        Type = parameter.Type.ToString()
                    };

                    methodParameters.Add(parameterInfo);
                }
            }

            return methodParameters;
        }
    }
}
