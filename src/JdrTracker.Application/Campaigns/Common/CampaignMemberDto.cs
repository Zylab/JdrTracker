using System;
using System.Collections.Generic;
using System.Text;

using JdrTracker.Domain.Enums;

namespace JdrTracker.Application.Campaigns.Common;

public record CampaignMemberDto(Guid Id, Guid UserId, string Nickname, CampaignRole Role, DateTime JoinedAt);
