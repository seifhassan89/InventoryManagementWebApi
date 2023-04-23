

namespace Models
{
    public class FilterItemDTO
    {
        public string NameOrCodeOrMeasuringUnitName { get; set; }
        public List<int> MeasuringUnitId { get; set; }


        public FilterItemDTO()
        {
            MeasuringUnitId = new List<int>();
        }

    }
}
