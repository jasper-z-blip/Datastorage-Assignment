namespace WebApi.Helpers;

public static class CalculateOnlyBusinessDays
{
    public static int GetBusinessDays(DateOnly startDate, DateOnly endDate)
    {
        int businessDays = 0;

        for (var date = startDate.ToDateTime(TimeOnly.MinValue); date <= endDate.ToDateTime(TimeOnly.MinValue); date = date.AddDays(1))
        {
            if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
            {
                businessDays++;
            }
        }

        return businessDays;
    }
}

