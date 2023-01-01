using JoberMQ.Common.Models.Method;
using Newtonsoft.Json;
using System.Linq.Expressions;

namespace JoberMQ.Client.Net.Helpers
{
    internal class ExpressionHelper
    {
        public static object[] GetExpressionParameterTypes(LambdaExpression methodCall)
        {
            var callExpression = methodCall.Body as MethodCallExpression;
            var arguments = callExpression.Arguments;

            object[] objts = new object[arguments.Count];

            for (int i = 0; i < arguments.Count; i++)
                objts[i] = arguments[i].Type;

            return objts;
        }
        public static Type[] GetExpressionParameterTypes2(LambdaExpression methodCall)
        {
            var callExpression = methodCall.Body as MethodCallExpression;
            var arguments = callExpression.Arguments;

            Type[] typs = new Type[arguments.Count];

            for (int i = 0; i < arguments.Count; i++)
                typs[i] = arguments[i].Type;

            return typs;
        }



        public static object[] GetExpressionParameterValues(LambdaExpression methodCall)
        {
            var callExpression = methodCall.Body as MethodCallExpression;
            var arguments = callExpression.Arguments;

            object[] objts = new object[arguments.Count];

            for (int i = 0; i < arguments.Count; i++)
                objts[i] = Expression.Lambda(arguments[i]).Compile().DynamicInvoke();

            return objts;
        }

        public static List<MethodParameterModel> GetExpressionParameterValues2(LambdaExpression methodCall)
        {
            var methodParameters = new List<MethodParameterModel>();
            var callExpression = methodCall.Body as MethodCallExpression;
            var arguments = callExpression.Arguments;

            foreach (var item in arguments)
            {
                var methodParameter = new MethodParameterModel();
                methodParameter.ParameterAssemblyName = item.Type.Assembly.FullName;
                methodParameter.ParameterTypeFullName = item.Type.FullName;
                methodParameter.ParameterValue = JsonConvert.SerializeObject(Expression.Lambda(item).Compile().DynamicInvoke());

                methodParameters.Add(methodParameter);
            }

            return methodParameters;
        }
    }
}
