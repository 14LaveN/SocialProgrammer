using Microsoft.AspNetCore.Mvc;
using SocialProgrammer.DAL.Interfaces;
using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.ViewModels.Comment;
using SocialProgrammer.Domain.ViewModels.Message;
using SocialProgrammer.Service.Implementations;
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

    public async Task<IActionResult> MessageForm(string recipientName)
    {
        var messagesByHistory = await messageRepository.GetAllAsync();
        var messagesByMessagesCurrentHistory = messagesByHistory
            .Where(x => x.RecipientName == recipientName)
            .Where(x => x.Author == User.Identity.Name);

        return View(messagesByMessagesCurrentHistory);
    }

    public async Task<IActionResult> MessagesForm()
    {
        var messages = await messageHistoryRepository.GetAllAsync();
        return View(messages.Where(x => x.Author == User.Identity.Name).ToList());
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
}