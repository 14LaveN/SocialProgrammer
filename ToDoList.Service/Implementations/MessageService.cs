using Amazon.Runtime.Internal.Util;
using Microsoft.Extensions.Logging;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.DAL.Repositories;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Enum;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Message;
using SocialProgrammer.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Implementations;

public class MessageService : IMessageService
{
    private readonly ILogger<MessageService> logger;
    private readonly IMessageRepository<MessageEntity> messageRepository;
    private readonly IMessageHistoryRepository<MessageHistoryEntity> messageHistoryRepository;

    public MessageService(ILogger<MessageService> logger,
        IMessageRepository<MessageEntity> messageRepository,
        IMessageHistoryRepository<MessageHistoryEntity> messageHistoryRepository)
    {
        this.logger = logger;
        this.messageRepository = messageRepository;
        this.messageHistoryRepository = messageHistoryRepository;
    }

    public async Task<IBaseResponse<MessageEntity>> CreateMessage(MessageViewModel messageViewModel)
    {
        try
        {
            logger.LogInformation($"Request for create a message - {messageViewModel.Message}");

            if (messageViewModel == null)
            {
                return new BaseResponse<MessageEntity>()
                {
                    Description = "Message not written",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            var message = new MessageEntity
            {
                Author = messageViewModel.Author,
                RecipientName = messageViewModel.RecipientId,
                Message = messageViewModel.Message,
                CreationDate = DateTime.Now
            };

            var messageHistory = new MessageHistoryEntity
            {
                Author = messageViewModel.Author,
                ReciepentId = messageViewModel.RecipientId
            };

            var messagesHistory = await messageHistoryRepository.GetAllAsync();
            var messagesHistoryWithDuplicates = messagesHistory.Where(x => x.Author == messageViewModel.Author
                && x.ReciepentId == messageViewModel.RecipientId
                || x.Author == messageViewModel.RecipientId 
                && x.ReciepentId == messageViewModel.Author).ToList();

            messageHistory.Messages++;

            if (messagesHistoryWithDuplicates.Count < 1)
                await messageHistoryRepository.CreateAsync(messageHistory);
            await messageRepository.CreateAsync(message);

            logger.LogInformation($"Message created: {message.Message}");

            return new BaseResponse<MessageEntity>()
            {
                Description = "Message created",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[MessageService.CreateMessage]: {exception.Message}");
            return new BaseResponse<MessageEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }

    public async Task<IBaseResponse<MessageEntity>> DeleteMessage(string messageId)
    {
        try
        {
            var message = await messageRepository.GetAsync(messageId);

            logger.LogInformation($"Request for remove a message - {message.Message}");

            if (message == null)
            {
                return new BaseResponse<MessageEntity>()
                {
                    Description = "Message with the same id not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            await messageRepository.RemoveAsync(messageId);

            logger.LogInformation($"Message deleted: {message.Message}");

            return new BaseResponse<MessageEntity>()
            {
                Description = "Message deleted",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[MessageService.DeleteMessage]: {exception.Message}");
            return new BaseResponse<MessageEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
    public async Task<IBaseResponse<MessageEntity>> UpdateMessage(string messageId, MessageViewModel messageViewModel)
    {
        try
        {
            var message = await messageRepository.GetAsync(messageId);

            logger.LogInformation($"Request for update a message - {message.Message}");

            if (message == null)
            {
                return new BaseResponse<MessageEntity>()
                {
                    Description = "Message with the same id not found",
                    StatusCode = StatusCode.TaskIsHasAlready
                };
            }

            message.Message = messageViewModel.Message;
            message.Author = messageViewModel.Author;
            message.CreationDate = DateTime.Now;
            message.RecipientName = messageViewModel.RecipientId;

            await messageRepository.UpdateAsync(messageId, message);

            logger.LogInformation($"Message updated: {message.Message}");

            return new BaseResponse<MessageEntity>()
            {
                Description = "Message updated",
                StatusCode = StatusCode.OK
            };

        }
        catch (Exception exception)
        {
            logger.LogError(exception, $"[MessageService.UpdateMessage]: {exception.Message}");
            return new BaseResponse<MessageEntity>()
            {
                Description = exception.Message,
                StatusCode = StatusCode.InternalServerError
            };
        }
    }
}
