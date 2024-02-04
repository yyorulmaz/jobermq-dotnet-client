using JoberMQ.Client.DotNet.Abs;
using JoberMQ.Client.DotNet.Constant;
using JoberMQ.Common.Dbos;
using JoberMQ.Common.Enums.Operation;
using JoberMQ.Common.Enums.Status;
using JoberMQ.Common.Models.Message;
using JoberMQ.Common.Models.Operation;
using JoberMQ.Common.Models.Producer;
using JoberMQ.Common.Models.Status;
using System;

public static class MessageMessageExtension
{
    public static MessageMessageBuilderModel Message(this MessageBuilderModel builder, IMessage message, IMessage resultMessage = null, bool isDbTextSave = ClientConst.IsDbTextSave)
    {
        var defaultMessageDbo = DefaultMessageDbo(builder.client);

        defaultMessageDbo.IsDbTextSave = isDbTextSave;

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
        defaultMessageDbo.Message = msg;


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

            defaultMessageDbo.IsResult = true;
            defaultMessageDbo.ResultMessage = resultMsg;

        }
        else
        {
            defaultMessageDbo.IsResult = false;
        }

        return new MessageMessageBuilderModel(builder.client, defaultMessageDbo);
    }

    public static MessageMessageResultBuilderModel ResultMessage(this MessageMessageBuilderModel builder, IMessage resultMessage)
    {
        builder.messageDbo.IsResult = true;

        var message = new MessageModel()
        {
            MessageType = resultMessage.MessageType,
            Message = resultMessage.Message,
            Routing = resultMessage.Routing,
            Info = resultMessage.Info,
            GeneralData = resultMessage.GeneralData,
            PriorityType = resultMessage.PriorityType
        };
        builder.messageDbo.ResultMessage = message;

        return new MessageMessageResultBuilderModel(builder.client, builder.messageDbo);
    }




    static MessageDbo DefaultMessageDbo(IClient client)
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