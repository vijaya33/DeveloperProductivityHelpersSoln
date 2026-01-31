using System;

namespace GuardExtensions
{

    public static class Guard
    {
        public static GuardClause<T> Against<T>(T value, string? paramName = null)
            => new(value, paramName);
    }

    public readonly record struct GuardClause<T>(T Value, string? ParamName)
    {
        public GuardClause<T> NotNull()
        {
            if (Value is null)
                throw new ArgumentNullException(ParamName ?? "value");
            return this;
        }

        public GuardClause<string> NotNullOrWhiteSpace()
        {
            if (Value is not string s)
                throw new ArgumentException("Value must be a string.", ParamName ?? "value");

            if (string.IsNullOrWhiteSpace(s))
                throw new ArgumentException("Value cannot be null/empty/whitespace.", ParamName ?? "value");

            return new GuardClause<string>(s, ParamName);
        }

        public GuardClause<int> Positive()
        {
            if (Value is not int i)
                throw new ArgumentException("Value must be an int.", ParamName ?? "value");

            if (i <= 0)
                throw new ArgumentOutOfRangeException(ParamName ?? "value", "Value must be > 0.");

            return new GuardClause<int>(i, ParamName);
        }

        public GuardClause<T> OneOf(params T[] allowed)
        {
            if (allowed is null || allowed.Length == 0)
                throw new ArgumentException("Allowed set cannot be empty.", nameof(allowed));

            if (!allowed.Contains(Value))
                throw new ArgumentOutOfRangeException(ParamName ?? "value", $"Value must be one of: {string.Join(", ", allowed)}");

            return this;
        }
    }
}