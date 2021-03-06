﻿using System.Linq;
using esme.Admin.Shared.ViewModels;
using esme.Infrastructure.Data;

namespace esme.Admin.Infrastructure.Services
{
    public class InvitationsGridService : GridService<Invitation, InvitationViewModel>
    {
        public InvitationsGridService(ApplicationDbContext db) : base(db)
        {
        }

        protected override IQueryable<InvitationViewModel> FetchViewModels()
        {
            return Entities.Select(i => new InvitationViewModel
            {
                Id = i.Id,
                To = i.To,
                SentBy = i.SentBy.Email,
                SentAt = i.SentAt,
                Error = i.Error,
                AcceptedAt = i.AcceptedAt,
            });
        }
    }
}
