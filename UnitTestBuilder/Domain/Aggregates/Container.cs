using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnitTestBuilder.Domain.Entities;

namespace UnitTestBuilder.Domain.Aggregates
{
    public class Container
    {

        public List<MethodParameter> MethodParameters { get; set; }
        public List<(MemberInfo, Container)> PrivateMethods { get; set; }

    }
}
