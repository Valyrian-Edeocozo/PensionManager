using System;

namespace PensionManager.PensionManager.Application.Helper;

public class NumberGenerator
{
    public static string GenerateUniqueReference()
    {
        // Get the current date and time
        DateTime now = DateTime.UtcNow;

        // Format the date and time as a string (e.g., "yyyyMMddHHmmssfff")
        string dateTimePart = now.ToString("yyyyMMddHHmmssfff");

        // Add a random component to ensure uniqueness
        Random random = new Random();
        string randomPart = random.Next(1000, 9999).ToString(); // 4-digit random number

        // Combine the parts to create the reference number
        string referenceNumber = $"REF-{dateTimePart}-{randomPart}";

        return referenceNumber;
    }
}
