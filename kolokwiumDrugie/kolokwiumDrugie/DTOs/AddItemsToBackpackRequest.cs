namespace kolokwiumDrugie.DTOs;

    public class AddItemsToBackpackRequest
    {
        public List<int> ItemIds { get; set; } = new List<int>();
    }