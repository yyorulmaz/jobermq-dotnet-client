using System;
using System.Collections.Generic;
using System.Linq;

namespace JoberMQ.Client.Common.RoundRobin
{
    public class RoundRobin<T> : IRoundRobin<T>
    {
        private List<RoundRobinModel<T>> Datas = new List<RoundRobinModel<T>>();
        private int counter = 1;
        private int counterWeight = 1;
        private Guid? endRoundRobinId;
        public RoundRobinModel<T> Get(Guid id)
        {
            return Datas.FirstOrDefault(x => x.Id == id);
        }
        public List<RoundRobinModel<T>> GetAll() => Datas;
        public RoundRobinModel<T> GetEndRoundRobin()
        {
            return Datas.FirstOrDefault(x => x.Id == endRoundRobinId);
        }
        public T GetEndRoundRobinData()
        {
            return Datas.FirstOrDefault(x => x.Id == endRoundRobinId).Data;
        }
        public RoundRobinModel<T> Add(T value, int weight)
        {
            var data = new RoundRobinModel<T>();
            data.Id = Guid.NewGuid();
            data.Weight = weight;
            data.Data = value;
            Datas.Add(data);

            if (endRoundRobinId == null)
                endRoundRobinId = data.Id;

            return data;
        }
        public RoundRobinModel<T> Remove(Guid id)
        {
            var data = Datas.FirstOrDefault(x => x.Id == id);
            Datas.Remove(data);

            counter--;
            counterWeight = 1;

            return data;
        }

        public T Next => OperationValue();



        private T OperationValue()
        {
            var data = Datas[counter - 1];

            if (Datas.Count == counter && data.Weight == counterWeight)
            {
                counter = 1;
                counterWeight = 1;
            }
            else if (Datas.Count != counter && data.Weight == counterWeight)
            {
                counter++;
                counterWeight = 1;
            }
            else
            {
                counterWeight++;
            }

            endRoundRobinId = data.Id;
            return data.Data;
        }

        
    }
}
