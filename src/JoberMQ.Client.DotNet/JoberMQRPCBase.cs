using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Common.Helpers;
using JoberMQ.Common.Method.Models;
using JoberMQ.Common.Models.General;
using Newtonsoft.Json;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace JoberMQ.Client.DotNet
{
    public abstract class JoberMQRPCBase<T> : IDisposable
    {
        IClient joberMQ;
        protected T baseClass;

        public virtual async Task<bool> Setup(JoberMQParameterModel joberMQParameterModel, T baseClass)
        {
            this.baseClass = baseClass;

            #region JoberMQ
            var joberMQResult = await JoberMQClient.CreateClientAndConnectAsync(joberMQParameterModel);
            if (joberMQResult.connect)
            {
                joberMQ = joberMQResult.client;
                joberMQ.Connect.ReceiveMessageRpcFunction += Connect_ReceiveMessageRpcFunction;
            }
            else
                return false;
            #endregion

            return true;
        }

        private void Connect_ReceiveMessageRpcFunction(Guid arg1, string arg2)
        {
            var returnData = new MethodReturnModel<byte[]>();

            try
            {
                var methodProperty = JsonConvert.DeserializeObject<MethodPropertyModel>(arg2);

                MethodInfo methodInfo;
                try
                {
                    methodInfo = typeof(T).GetMethod(methodProperty.MethodName);
                }
                catch (Exception)
                {
                    methodInfo = typeof(T).GetMethod(methodProperty.MethodName, Type.EmptyTypes);
                }

                object[] setParameter = new object[methodProperty.ParemeterValues.Count];
                for (int i = 0; i < methodProperty.ParemeterValues.Count; i++)
                {
                    var propertyAssembly = Assembly.Load(new AssemblyName(methodProperty.ParemeterValues[i].ParameterAssemblyName));
                    var propertyType = propertyAssembly.GetType(methodProperty.ParemeterValues[i].ParameterTypeFullName);
                    setParameter[i] = JsonConvert.DeserializeObject(methodProperty.ParemeterValues[i].ParameterValue, propertyType);
                }

                //Type typeAsync = typeof(AsyncStateMachineAttribute);
                //var isAsync = (AsyncStateMachineAttribute)methodInfo.GetCustomAttribute(typeAsync);

                var isAsync = IsMethodAsync(methodInfo);


                if (isAsync == false)
                {
                    var rtrnDt = methodInfo.Invoke(baseClass, setParameter);

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
                    var task = (Task)methodInfo.Invoke(baseClass, setParameter);
                    if (task != null)
                    {
                        //await task.ConfigureAwait(false);
                        task.ConfigureAwait(false).GetAwaiter();
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


            }
            catch (Exception ex)
            {
                returnData.IsOperationSuccess = false;
                returnData.Data = null;
                returnData.StatusCode = "1";
                returnData.Message = ex.Message;
            }

            _ = joberMQ.SendAsync("MessageRpcResponse", arg1, returnData.Data, returnData.IsOperationSuccess, returnData.Message);
        }

        public static bool IsMethodAsync(MethodInfo methodInfo)
        {
            if (methodInfo == null)
                throw new ArgumentNullException(nameof(methodInfo));

            if (methodInfo.GetCustomAttribute(typeof(AsyncStateMachineAttribute)) != null)
                return true;

            // Check if the method returns a Task or Task<T>
            return typeof(Task).IsAssignableFrom(methodInfo.ReturnType);
        }

        #region Dispose
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue=true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~JoberMQRPCBase()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }

}
