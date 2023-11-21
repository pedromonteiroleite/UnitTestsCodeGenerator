using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using UnitTestBuilder.Domain.Entities;
using UnitTestBuilder.Interfaces.Builders;
using UnitTestBuilder.Interfaces.Services;

namespace UnitTestBuilder.Builders
{
    public class Builder : IBuilder
    {

        private IParameterExtractorService _parameterExtractorService;

        public Builder(IParameterExtractorService parameterExtractorService)
        {
            _parameterExtractorService = parameterExtractorService;
        }

        public string BuildConstructor()
        {
            throw new NotImplementedException();
        }

        public List<MethodParameter> BuildUoWParameters(MethodInfo methodInfo)
        {
            return _parameterExtractorService.ExtractInputParameters(methodInfo);
        }

        public string BuildReturnValues()
        {
            throw new NotImplementedException();
        }

        public string BuildTestMethod()
        {
            throw new NotImplementedException();
        }

        public string BuildUsingDirectives()
        {
            throw new NotImplementedException();
        }
    }
}
