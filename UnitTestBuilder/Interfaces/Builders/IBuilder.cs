using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTestBuilder.Domain.Entities;

namespace UnitTestBuilder.Interfaces.Builders
{
    public interface IBuilder
    {
        string BuildUsingDirectives();
        List<MethodParameter> BuildUoWParameters(MethodInfo methodInfo);
        string BuildReturnValues();
        string BuildConstructor();
        string BuildTestMethod();
    }
}
