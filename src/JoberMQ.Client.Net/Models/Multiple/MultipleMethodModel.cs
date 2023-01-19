using System;
using System.Linq.Expressions;

namespace JoberMQ.Client.Net.Models.Multiple
{
    public class MultipleMethodModel : MultipleMethodModelBase
    {
        public Expression<Action> MethodCall { get; set; }
    }
}
