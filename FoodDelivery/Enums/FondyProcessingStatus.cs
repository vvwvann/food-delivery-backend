namespace FoodDelivery.Enums
{
  public enum FondyProcessingStatus
  {
    /// <summary>Заказ был создан, но клиент еще не ввел платежные реквизиты</summary>
    Created,
    /// <summary>Заказ все еще находится в процессе обработки платежным шлюзом</summary>
    Processing,
    /// <summary>Заказ отклонен платежным шлюзом FONDY, внешней платежной системой или банком-эквайером</summary>
    Declined,
    /// <summary>Заказ успешно совершен, средства заблокированы на счету плательщика и вскоре будут зачислены мерчанту;
    /// мерчант может оказывать услугу или “отгружать” товар</summary>
    Approved,
    /// <summary>Время жизни заказа, указанное в параметре lifetime, истекло</summary>
    Expired,
    /// <summary>Ранее успешная транзакция была полностью или частично отменена.
    /// В таком случае параметр <see cref="FondyResponseModel.ReversalAmount"/> имеет не нулевое значение</summary>
    Reversed,
    /// <summary>В случае неожиданного статуса заказа, выдает это значение.</summary>
    Undefined
  }
}
