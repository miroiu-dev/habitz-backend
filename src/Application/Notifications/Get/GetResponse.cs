using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Notifications.Get;
public sealed record GetResponse(List<NotificationResponse> Data);
