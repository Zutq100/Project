﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface IQrCodeService
    {
        byte[] GenerationCalendarEvent(string subject, string password, string location, DateTimeOffset start, DateTimeOffset end, bool allDay);
    }
}
