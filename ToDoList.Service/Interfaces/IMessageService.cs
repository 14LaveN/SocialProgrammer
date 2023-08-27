using SocialProgrammer.Domain.Entity;
using SocialProgrammer.Domain.Response;
using SocialProgrammer.Domain.ViewModels.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialProgrammer.Service.Interfaces;

public interface IMessageService
{
    Task<IBaseResponse<MessageEntity>> CreateMessage(MessageViewModel messageViewModel);

    Task<IBaseResponse<MessageEntity>> DeleteMessage(string messageId);
}
