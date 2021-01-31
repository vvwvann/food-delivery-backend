using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Enums
{
  /// <summary>Статус обработки запроса</summary>
  public enum FondyResponseStatus
  {
    /// <summary>Если все прошло успешно и ошибок в валидации данных нет</summary>
    Success,
    /// <summary>Если возникла ошибка при валидации передаваемых параметров</summary>
    Failure,
    /// <summary>Если не удалось определить статус ответа</summary>
    Undefined
  }
}
