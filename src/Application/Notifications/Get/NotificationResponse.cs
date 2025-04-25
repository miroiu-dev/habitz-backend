using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Get;
public record NotificationResponse(int Id, string Icon, string Title, string Description, DateTime CreatedAt);

