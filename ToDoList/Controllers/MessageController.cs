using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.Message;
using SocialProgrammer.Service.Interfaces;

namespace SocialProgrammer.Controllers;

public class MessageController : Controller
{
    private readonly IMessageService messageService;
    private readonly IMessageHistoryRepository<MessageHistoryEntity> messageHistoryRepository;
    private readonly IMessageRepository<MessageEntity> messageRepository;

    public MessageController(IMessageService messageService,
        IMessageHistoryRepository<MessageHistoryEntity> messageHistoryRepository,
        IMessageRepository<MessageEntity> messageRepository)
    {
        this.messageService = messageService;
        this.messageHistoryRepository = messageHistoryRepository;
        this.messageRepository = messageRepository;
    }

	[HttpGet]
	public async Task<IActionResult> MessageForm(string recipientName)
    {
        var messagesByHistory = await messageRepository.GetAllAsync();
        var messagesByMessagesCurrentHistory = messagesByHistory
            .Where(x => x.RecipientName == recipientName || x.Author == User.Identity?.Name || x.RecipientName == User.Identity?.Name);
        return View(messagesByMessagesCurrentHistory);
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMessageForm(string messageId)
    {
        var messagesByHistory = await messageRepository.GetAllAsync();
        var messageByMessagesCurrentHistory = messagesByHistory
            .Find(x => x.Id == messageId);

        return View(messageByMessagesCurrentHistory);
    }

    [HttpGet]
    public async Task<IActionResult> MessagesForm()
    {
        var messages = await messageHistoryRepository.GetAllAsync();
        var messagesHistoryByName = messages.Where(x => x.Author == User.Identity.Name || x.ReciepentId == User.Identity.Name).ToList();
        return View(messagesHistoryByName);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(MessageViewModel messageViewModel)
    {
        var response = await messageService.CreateMessage(messageViewModel);
        if (response.StatusCode == Domain.Enum.StatusCode.OK)
        {
            return Ok(new { description = response.Description });
        }
        return BadRequest(new { description = response.Description });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteMessage(string messageId)
    {
        var message = await messageRepository.GetAsync(messageId);
        if (message.Author == User.Identity.Name)
        {
            var response = await messageService.DeleteMessage(messageId);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
        return RedirectToAction("MessageForm");
    }
    [HttpPost]
    public async Task<IActionResult> UpdateMessage(string messageId, MessageViewModel messageViewModel)
    {
        var message = await messageRepository.GetAsync(messageId);
        if (message.Author == User.Identity.Name)
        {
            var response = await messageService.UpdateMessage(messageId, messageViewModel);
            if (response.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
        return RedirectToAction("MessageForm");
    }
}