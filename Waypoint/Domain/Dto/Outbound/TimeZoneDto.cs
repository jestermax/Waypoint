using Domain.Models;

namespace Domain.Dto.Outbound
{
    public class TimeZoneDto
    {
        public TimeZoneDto(TimeZone timeZone)
        {
            id = timeZone.Id;
            name = timeZone.Name;
            offset = timeZone.Offset;
        }

        public string id { get; set; }

        public string name { get; set; }

        public int offset { get; set; }
    }
}
