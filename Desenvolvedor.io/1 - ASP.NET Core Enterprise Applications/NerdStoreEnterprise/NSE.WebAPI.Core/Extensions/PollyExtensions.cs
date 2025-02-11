﻿using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NSE.WebAPI.Core.Extensions
{
    public static class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .WaitAndRetryAsync(new[]
              {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(3),
              }, (outcome, timespan, retryCount, context) =>
              {
                  Console.ForegroundColor = ConsoleColor.Blue;
                  Console.WriteLine($"Tentando pela {retryCount} vez!");
                  Console.ForegroundColor = ConsoleColor.White;
              });
        }
    }
}