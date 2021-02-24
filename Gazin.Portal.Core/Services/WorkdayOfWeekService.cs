using Gazin.Portal.Core.Helpers;
using Gazin.Portal.DataStorage.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Gazin.Portal.Core.Services
{
    public class WorkdayOfWeekService : IWorkdayOfWeekService
    {
        private readonly IHolidayRepository holidayRepository;
        private readonly IWorkdayOfWeekRepository workdayOfWeekRepository;
        private readonly IGenerateRangeDatesHelper generateRangeDatesHelper;
        private readonly ISanitizeRangeDatesHelper sanitizeRangeDatesHelper;

        public WorkdayOfWeekService(IHolidayRepository holidayRepository,
                                    IWorkdayOfWeekRepository workdayOfWeekRepository,
                                    IGenerateRangeDatesHelper generateRangeDatesHelper,
                                    ISanitizeRangeDatesHelper sanitizeRangeDatesHelper)
        {
            this.holidayRepository = holidayRepository;
            this.workdayOfWeekRepository = workdayOfWeekRepository;
            this.generateRangeDatesHelper = generateRangeDatesHelper;
            this.sanitizeRangeDatesHelper = sanitizeRangeDatesHelper;
        }

        public async Task<IList<DateTime>> GetAvailableDays(DateTime startDate, DateTime endDate)
        {
            var range = generateRangeDatesHelper.Create(startDate, endDate);
            var holidays = await holidayRepository.GetOnlyDates();
            var workdays = await workdayOfWeekRepository.GetActives();

            return sanitizeRangeDatesHelper.RemoveNotWorkdayAndCommemorativeDates(range, workdays, holidays);
        }
    }
}