namespace FoodDelivery.Models.Restaurants
{
  public class DishRequestModel
  {
    public int Id { get; set; }

    public int Number { get; set; }

    public string Comment { get; set; }

    public class AdditionalItem
    {
      public int Id { get; set; }

      public int Number { get; set; }
    }
  }
}
