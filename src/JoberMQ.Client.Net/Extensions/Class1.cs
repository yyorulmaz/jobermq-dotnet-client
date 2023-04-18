using System;
using System.Collections.Generic;
using System.Text;

namespace JoberMQ.Client.Net.Extensions
{
    internal class Class1
    {
        public event Action<string> ReceiveData;

        public void TriggerReceiveData(string data)
        {
            ReceiveData.InvokeWithParameter(data);
        }

    }

    public static class ActionExtensions
    {
        public static void InvokeWithParameter<T>(this Action<T> action, T parameter)
        {
            action?.Invoke(parameter);
        }
    }
}
