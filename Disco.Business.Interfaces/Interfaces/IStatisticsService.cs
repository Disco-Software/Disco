﻿using Disco.Business.Interfaces.Dtos.Analytic.Admin.GetAnalytics;
using Disco.Business.Interfaces.Enums;
using Disco.Domain.Models;
using Disco.Domain.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Disco.Business.Interfaces.Interfaces
{
    public interface IAnalyticService
    {
        Task<AnalyticResponseDto> GetAnalyticAsync(DateTime from, DateTime to, AnalyticFor statistics);
    }
}
