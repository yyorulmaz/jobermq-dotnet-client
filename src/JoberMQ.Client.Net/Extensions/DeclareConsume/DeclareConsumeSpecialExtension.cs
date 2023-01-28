using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Enums.Declare;
using JoberMQ.Client.Net.Models.DeclareConsume;
using System.ComponentModel;
using System.Linq;

namespace JoberMQ.Client.Net.Extensions.DeclareConsume
{
    public static class DeclareConsumeSpecialExtension
    {
        public static DeclareConsumeBuilderSpecialExtensionModel SpecialAdd(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension)
            => Add(DeclareConsumeOperationTypeEnum.SpecialAdd, null);

        public static DeclareConsumeBuilderSpecialExtensionModel SpecialRemove(this DeclareConsumeBuilderExtensionModel declareConsumeBuilderExtension)
            => Add(DeclareConsumeOperationTypeEnum.SpecialRemove, null);

        private static DeclareConsumeBuilderSpecialExtensionModel Add(DeclareConsumeOperationTypeEnum declareConsumeOperationType, string declareKey)
        {
            var declareConsumeBuilderSpecial = new DeclareConsumeBuilderSpecialExtensionModel();
            declareConsumeBuilderSpecial.DeclareConsumeBuilder.DeclareConsumeOperationType = declareConsumeOperationType;
            declareConsumeBuilderSpecial.DeclareConsumeBuilder.DeclareKey = declareKey;
            return declareConsumeBuilderSpecial;
        }
        
    }
}
