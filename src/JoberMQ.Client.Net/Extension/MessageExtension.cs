using JoberMQ.Client.Net.Abstraction.Client;
using JoberMQ.Client.Net.Abstraction.Message;
using JoberMQ.Client.Net.Constant;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Operation;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Message;
using JoberMQ.Common.Models.Operation;
using JoberMQ.Common.Models.Producer;
using JoberMQ.Common.Models.Response;
using JoberMQ.Common.Models.Status;
using System;
using System.Threading.Tasks;

public static class MessageExtension
{
    public static MessageExtensionModel<IClient> Message(this IClient client, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
    {
        var messageDbo = MessageDefault(client);

        messageDbo.IsDbTextSave = isDbTextSave;

        var msg = new MessageModel
        {
            MessageType = message.MessageType,
            Message = message.Message,
            Routing = message.Routing,
            Info = message.Info,
            GeneralData = message.GeneralData,
            PriorityType = message.PriorityType,
            MessageConsuming = message.MessageConsuming,
        };
        messageDbo.Message = msg;


        if (resultMessage != null)
        {
            var resultMsg = new MessageModel
            {
                MessageType = resultMessage.MessageType,
                Message = resultMessage.Message,
                Routing = resultMessage.Routing,
                Info = resultMessage.Info,
                GeneralData = resultMessage.GeneralData,
                PriorityType = resultMessage.PriorityType,
                MessageConsuming = resultMessage.MessageConsuming,
            };

            messageDbo.IsResult = true;
            messageDbo.ResultMessage = resultMsg;

        }
        else
        {
            messageDbo.IsResult = false;
        }

        return new MessageExtensionModel<IClient> { Client = client, Message = messageDbo };
    }
    public static MessageResultExtensionModel<IClient> ResultMessage(this MessageExtensionModel<IClient> messageExtensionModel, IMessage resultMessage)
    {
        messageExtensionModel.Message.IsResult = true;

        var message = new MessageModel()
        {
            MessageType = resultMessage.MessageType,
            Message = resultMessage.Message,
            Routing = resultMessage.Routing,
            Info = resultMessage.Info,
            GeneralData = resultMessage.GeneralData,
            PriorityType = resultMessage.PriorityType
        };
        messageExtensionModel.Message.ResultMessage = message;

        return new MessageResultExtensionModel<IClient> { Client = messageExtensionModel.Client, Message = messageExtensionModel.Message };
    }
    public static async Task<ResponseModel> SendAsync(this MessageExtensionModel<IClient> messageExtensionModel)
        => await messageExtensionModel.Client.Connect.InvokeAsync<ResponseModel>("Message", messageExtensionModel.Message);
    public static async Task<ResponseModel> SendAsync(this MessageResultExtensionModel<IClient> messageResultExtensionModel)
        => await messageResultExtensionModel.Client.Connect.InvokeAsync<ResponseModel>("Message", messageResultExtensionModel.Message);

    static MessageDbo MessageDefault(IClient client)
        => new MessageDbo
            {
                Id = Guid.NewGuid(),
                Operation = new OperationModel
                {
                    Version = 0,
                    OperationType = OperationTypeEnum.Message
                },
                Producer = new ProducerModel
                {
                    ClientKey = client.ClientInfo.ClientKey,
                },
                Message = null,
                IsResult = false,
                ResultMessage = null,
                TriggerGroupsId = null,
                CreatedJobId = null,
                CreatedJobDetailId = null,
                CreatedJobTransactionId = null,
                CreatedJobTransactionDetailId = null,
                EventGroupsId = null,
                Status = new StatusModel
                {
                    IsCompleted = false,
                    IsError = false,
                    StatusTypeMessage = StatusTypeMessageEnum.None,
                    TempAgainDate = null
                },
            };
}