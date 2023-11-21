using Microsoft.CodeAnalysis.Emit;
using System.Reflection;
using UnitTestBuilder.Domain.Aggregates;
using UnitTestBuilder.Interfaces.Builders;

namespace UnitTestBuilder.Builders
{
    public class Director
    {
        
        private IBuilder _builder;

        public Director(IBuilder builder)
        {
            _builder = builder;
        }

        public void ChangeBuilder(IBuilder builder)
        {
            _builder = builder;
        }

        public string Make(Type type, string uoWName)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            if (string.IsNullOrEmpty(uoWName))
                throw new ArgumentNullException(nameof(uoWName));

            var memberInfo = type.GetMember(uoWName);

            if (memberInfo[0].MemberType == MemberTypes.Method)
            {
                var methodInfo = type.GetMethod(uoWName);

                if (methodInfo == null)
                    throw new ArgumentException($"Method '{uoWName}' not found in type '{type.FullName}'.");

                var basket = new Container()
                {
                    MethodParameters = _builder.BuildUoWParameters(methodInfo)
                };
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(uoWName));
            }

            return string.Empty;
        }

    }
}
