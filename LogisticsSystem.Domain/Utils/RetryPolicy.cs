using System;
using System.Threading;

namespace LogisticsSystem.Domain.Utils;

public static class RetryPolicy
{
    public static T Execute<T>(Func<T> operation, int maxRetries = 3)
    {
        int attempt = 0;
        while (true)
        {
            try
            {
                return operation();
            }
            catch (Exception ex)
            {
                attempt++;
                if (attempt > maxRetries) throw;

                int delayMs = (int)Math.Pow(2, attempt) * 100;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"[Retry Policy] Помилка: '{ex.Message}'. Спроба {attempt}/{maxRetries}. Очікування {delayMs}мс...");
                Console.ResetColor();

                Thread.Sleep(delayMs);
            }
        }
    }
}