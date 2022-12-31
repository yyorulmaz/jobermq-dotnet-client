using JoberMQ.Client.Common.Models.Method;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace JoberMQ.Client.Common.Helpers
{
    internal class MethodHelper
    {
        public static async Task<MethodReturnDataModel<byte[]>> MethodRun(Expression<Action> methodCall) => await MethodRunner(MethodProperty(methodCall));
        public static async Task<MethodReturnDataModel<byte[]>> MethodRun(MethodPropertyModel methodProperty) => await MethodRunner(methodProperty);
        public static async Task<MethodReturnDataModel<byte[]>> MethodRun(string methodPropertyJson) => await MethodRunner(MethodPropertyDeserializeObject(methodPropertyJson));


        public static MethodPropertyModel MethodProperty(Expression<Action> methodCall)
        {
            var methodProperty = new MethodPropertyModel();
            var callExpression = methodCall.Body as MethodCallExpression;

            methodProperty.MethodAssemblyName = callExpression.Method.DeclaringType.Assembly.GetName().Name;


            // buna gerek olmayabilir sadece aşağıdaki kod yeterli olabilir.
            // methodProperty.MethodNamespaceName = callExpression.Method.DeclaringType.FullName;
            if (callExpression.Method.DeclaringType.FullName.IndexOf("+", 0, callExpression.Method.DeclaringType.FullName.Length - 1) == -1)
            {
                methodProperty.MethodNamespaceName = callExpression.Method.DeclaringType.Namespace;
                methodProperty.IsChildClass = false;
            }
            else
            {
                methodProperty.MethodNamespaceName = callExpression.Method.DeclaringType.FullName;
                methodProperty.IsChildClass = true;
            }
            // ----------------------------------------------------------------

            methodProperty.MethodClassName = callExpression.Method.DeclaringType.Name;
            methodProperty.MethodName = callExpression.Method.Name;
            methodProperty.ParemeterValues = ExpressionHelper.GetExpressionParameterValues2(methodCall);

            return methodProperty;
        }
        public static MethodPropertyModel MethodPropertyDeserializeObject(string methodPropertyJson)
        {
            return JsonConvert.DeserializeObject<MethodPropertyModel>(methodPropertyJson);
        }
        public static string MethodPropertySerialize(Expression<Action> methodCall)
        {
            return JsonConvert.SerializeObject(MethodProperty(methodCall));
        }
        public static string MethodPropertySerialize(MethodPropertyModel methodProperty)
        {
            return JsonConvert.SerializeObject(methodProperty);
        }


        private static async Task<MethodReturnDataModel<byte[]>> MethodRunner(MethodPropertyModel methodProperty)
        {
            var returnData = new MethodReturnDataModel<byte[]>();

            try
            {
                var assembly = Assembly.Load(new AssemblyName(methodProperty.MethodAssemblyName));

                Type type;
                if (methodProperty.IsChildClass == false)
                    type = assembly.GetType(methodProperty.MethodNamespaceName + "." + methodProperty.MethodClassName);
                else
                    type = assembly.GetType(methodProperty.MethodNamespaceName);


                //MethodInfo methodInfo = type.GetMethod(methodProperty.MethodName);
                MethodInfo methodInfo;
                try
                {
                    methodInfo = type.GetMethod(methodProperty.MethodName);
                }
                catch (Exception)
                {
                    methodInfo = type.GetMethod(methodProperty.MethodName, Type.EmptyTypes);

                }


                object instance = Activator.CreateInstance(type);

                object[] setParameter = new object[methodProperty.ParemeterValues.Count];
                for (int i = 0; i < methodProperty.ParemeterValues.Count; i++)
                {
                    var propertyAssembly = Assembly.Load(new AssemblyName(methodProperty.ParemeterValues[i].ParameterAssemblyName));
                    var propertyType = propertyAssembly.GetType(methodProperty.ParemeterValues[i].ParameterTypeFullName);
                    setParameter[i] = JsonConvert.DeserializeObject(methodProperty.ParemeterValues[i].ParameterValue, propertyType);
                }






                Type typeAsync = typeof(AsyncStateMachineAttribute);
                var isAsync = (AsyncStateMachineAttribute)methodInfo.GetCustomAttribute(typeAsync);

                if (isAsync == null)
                {
                    var rtrnDt = (object)methodInfo.Invoke(instance, setParameter);

                    if (rtrnDt != null)
                    {
                        var rtrnTyp = rtrnDt.GetType();
                        returnData.TypeFullName = rtrnTyp.FullName;
                        returnData.Data = ByteHelper.ObjectToByteArray(rtrnDt);
                        returnData.StatusCode = "0";
                    }
                    else
                    {
                        returnData.TypeFullName = null;
                        returnData.Data = null;
                        returnData.StatusCode = "0";
                    }
                }
                else
                {
                    var task = (Task)methodInfo.Invoke(instance, setParameter);
                    if (task != null)
                    {
                        await task.ConfigureAwait(false);
                        var resultProperty = task.GetType().GetProperty("Result");


                        var rtrnDt = resultProperty.GetValue(task);
                        var rtrnTyp = rtrnDt.GetType();

                        if (rtrnDt != null)
                        {
                            returnData.TypeFullName = rtrnTyp.FullName;

                            if (returnData.TypeFullName == "System.Threading.Tasks.VoidTaskResult")
                                returnData.Data = null;
                            else
                                returnData.Data = ByteHelper.ObjectToByteArray(rtrnDt);

                            returnData.StatusCode = "0";
                        }
                        else
                        {
                            returnData.TypeFullName = null;
                            returnData.Data = null;
                            returnData.StatusCode = "0";
                        }

                    }
                }


                #region ARSIVE
                //try
                //{
                //    var task = (Task)methodInfo.Invoke(instance, setParameter);
                //    if (task != null)
                //    {
                //        await task.ConfigureAwait(false);
                //        var resultProperty = task.GetType().GetProperty("Result");

                //        // bu sonucu bazı durumlarda dönmemiz gerekecek
                //        var rtrnDt = resultProperty.GetValue(task);
                //        var rtrnTyp = rtrnDt.GetType();

                //        if (rtrnDt != null)
                //        {
                //            returnData.Type = rtrnTyp.FullName;
                //            returnData.Data = ByteHelper.ObjectToByteArray(rtrnDt);
                //            returnData.StatusCode = "0";
                //        }
                //        else
                //        {
                //            returnData.Type = null;
                //            returnData.Data = null;
                //            returnData.StatusCode = "0";
                //        }

                //    }

                //}
                //catch (Exception)
                //{
                //    //methodInfo.Invoke(instance, setParameter);

                //    var rtrnDt = (object)methodInfo.Invoke(instance, setParameter);
                //    var rtrnTyp = rtrnDt.GetType();

                //    if (rtrnDt != null)
                //    {
                //        returnData.Type = rtrnTyp.FullName;
                //        returnData.Data = ByteHelper.ObjectToByteArray(rtrnDt);
                //        returnData.StatusCode = "0";
                //    }
                //    else
                //    {
                //        returnData.Type = null;
                //        returnData.Data = null;
                //        returnData.StatusCode = "0";
                //    }
                //}
                #endregion

            }
            catch (Exception ex)
            {
                returnData.IsOperationSuccess = false;
                returnData.Data = null;
                returnData.StatusCode = "1";
                returnData.Message = ex.Message;
            }

            return returnData;
        }
    }
}
