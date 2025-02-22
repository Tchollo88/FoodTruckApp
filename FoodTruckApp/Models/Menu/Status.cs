namespace FoodTruckApp.Models.Menu
{
    public class Status
    {
        public int Id { get; set; }
        public bool IsComplete { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly EndTime { get; set; }
    }
}
