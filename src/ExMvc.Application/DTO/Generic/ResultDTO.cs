using ExMvc.Application.Enums;

namespace ExMvc.Application.DTO.Generic
{
    public class ResultDTO
    {
        public NotificationEnum Notification { get; private set; }
        public string Message { get; private set; }

        public ResultDTO(NotificationEnum notification, string message)
        {
            Notification = notification;
            Message = message;
        }
    }
}
