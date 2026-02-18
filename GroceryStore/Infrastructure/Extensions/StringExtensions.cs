namespace GroceryStore.Database.Extensions;

using System.Runtime.CompilerServices;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var underscoreCount = CountUnderscores(input);
        if (underscoreCount == 0)
        {
            return input.ToLowerInvariant();
        }

        var newLength = input.Length + underscoreCount;

        return string.Create(newLength, input, (span, original) =>
        {
            var writeIndex = 0;
            var len = original.Length;

            span[writeIndex++] = char.ToLowerInvariant(original[0]);

            for (var i = 1; i < len; i++)
            {
                if (char.IsUpper(original[i]))
                {
                    var prevIsLower = !char.IsUpper(original[i - 1]);
                    var nextIsLower = i < len - 1 && !char.IsUpper(original[i + 1]);

                    if (prevIsLower || nextIsLower)
                    {
                        span[writeIndex++] = '_';
                    }
                }

                span[writeIndex++] = char.ToLowerInvariant(original[i]);
            }
        });
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int CountUnderscores(string input)
    {
        var count = 0;
        var len = input.Length;

        for (var i = 1; i < len; i++)
        {
            if (char.IsUpper(input[i]))
            {
                var prevIsLower = !char.IsUpper(input[i - 1]);
                var nextIsLower = i < len - 1 && !char.IsUpper(input[i + 1]);

                if (prevIsLower || nextIsLower)
                {
                    count++;
                }
            }
        }

        return count;
    }
}