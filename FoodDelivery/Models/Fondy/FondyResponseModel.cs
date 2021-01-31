using FoodDelivery.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models.Fondy
{
  public class FondyResponseModel
  {
    [JsonProperty("merchant_id")]
    public int MerchantId { get; set; }

    /// <summary>Идентификатор заказа. Назначается мерчантом.</summary>
    [JsonProperty("order_id")]
    public string OrderId { get; set; }

    /// <summary>В общем случае не уникальны код авторизации, присвоенный банком-эмитентом</summary>
    [JsonProperty("approval_code")]
    public string ApprovalCode { get; set; }

    /// <summary>Платежная система, через которую был осуществлен платеж</summary>
    [JsonProperty("payment_system")]
    public string PaymentSystem { get; set; }

    /// <summary>Тип валюты заказа</summary>
    /// <example>
    /// UAH — украинская гривна
    /// RUB — российский рубль
    /// USD — доллар США
    /// GBP — фунт стерлингов
    /// EUR — евро
    /// </example>
    [JsonProperty("currency")]
    public string CurrencyType { get; set; }

    /// <summary>Фактическая валюта заказа</summary>
    [JsonProperty("actual_currency")]
    public string ActualCurrency { get; set; }

    /// <summary>Email плательщика</summary>
    [JsonProperty("sender_email")]
    public string SenderEmail { get; set; }

    /// <summary>Мобильный телефон плательщика</summary>
    [JsonProperty("sender_cell_phone")]
    public string SenderCellPhone { get; set; }

    /// <summary>Номер счета плательщика</summary>
    [JsonProperty("sender_account")]
    public string SenderAccount { get; set; }

    /// <summary>Идентификатор оплачиваемого товара/услуги</summary>
    [JsonProperty("product_id")]
    public string ProductId { get; set; }

    /// <summary>Любой произвольный набор данных, который мерчант передал в запросе</summary>
    //[JsonConverter(typeof(JsonToMerchantDataFieldConverter))]
    [JsonProperty("merchant_data")]
    public FondyMerchantDataField[] Fields { get; set; }

    /// <summary>Статус обработки заказа</summary>
   // [JsonConverter(typeof(OrderStatusConverter))]
    [JsonProperty("order_status")]
    public FondyProcessingStatus OrderStatus { get; set; }

    /// <summary>Статус обработки запроса</summary>
    //[JsonConverter(typeof(ResponseStatusConverter))]
    [JsonProperty("response_status")]
    public FondyResponseStatus ResponseStatus { get; set; }

    /// <summary>Сумма заказа в копейках/центах без разделителей</summary>
   // [JsonConverter(typeof(StringToDecimalConverter))]
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>Фактическая сумма заказа, после всех реверсов.</summary>
  //  [JsonConverter(typeof(StringToDecimalConverter))]
    [JsonProperty("actual_amount")]
    public decimal ActualAmount { get; set; }

    /// <summary>Сумма всех реверсов по данному заказу</summary>
   // [JsonConverter(typeof(StringToDecimalConverter))]
    [JsonProperty("reversal_amount")]
    public decimal ReversalAmount { get; set; }

    /// <summary>Ищет поле с указанным названием. Если такого поля нет, то вернет Null</summary>
    public FondyMerchantDataField FindField(string fieldName) => Fields.FirstOrDefault(field => field.Name == fieldName);
  }
}
