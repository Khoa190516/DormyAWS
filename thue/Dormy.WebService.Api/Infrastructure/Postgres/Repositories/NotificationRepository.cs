﻿using Dormy.WebService.Api.Core.Entities;
using Dormy.WebService.Api.Infrastructure.Postgres.IRepositories;

namespace Dormy.WebService.Api.Infrastructure.Postgres.Repositories
{
    public class NotificationRepository : BaseRepository<NotificationEntity>, INotificationRepository
    {
        public NotificationRepository(ApplicationContext context) : base(context)
        {

        }
    }
}
