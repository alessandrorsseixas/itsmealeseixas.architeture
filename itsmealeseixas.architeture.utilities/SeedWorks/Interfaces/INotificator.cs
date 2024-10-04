using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using itsmealeseixas.architeture.utilities.Seedworks;

namespace itsmealeseixas.architeture.utilities.SeedWorks.Interfaces
{
    public interface INotificator
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
