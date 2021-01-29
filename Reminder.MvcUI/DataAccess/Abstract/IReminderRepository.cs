using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Reminder.MvcUI.DataAccess.Abstract
{
    public interface IReminderRepository
    {
        List<Entities.Reminder> GetNotTimeYets();
        List<Entities.Reminder> GetTimeOuts();
        List<Entities.Reminder> GetNowIsTime();
        Entities.Reminder GetById(Guid id);

        void Add(Entities.Reminder reminder);
        void Remove(Guid id);
        void Retitle(string title, Entities.Reminder reminder);
        void TimeOut(Guid id);
        void NowIsTime(Guid id);

    }
}
