﻿using System;

namespace FoodDelivery.Exceptions
{
  public class ApiException : Exception
  {
    public int StatusCode { get; set; }

    public ApiException(string message,
                        int statusCode = 500) :
        base(message)
    {
      StatusCode = statusCode;
    }
    public ApiException(Exception ex, int statusCode = 500) : base(ex.Message)
    {
      StatusCode = statusCode;
    }
  }

  public class ApiError
  {
    public string Message { get; set; }
    public bool IsError { get; set; } = true;
    public string Detail { get; set; }
  }
}