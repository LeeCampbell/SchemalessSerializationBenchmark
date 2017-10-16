using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.Serialization;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public struct Date : IComparable<Date>, IEquatable<Date>, System.Runtime.Serialization.ISerializable, IFormattable, IConvertible
    {
        private const string SerializationFormat = "yyyy-MM-dd";
        private readonly DateTimeOffset _inner;

        // Tech Debt: This is to ease the transition from DateTime.Today, it might be better to require consumer to resolve the day from configurable injected provider (test support / time travel)

        /// <summary>
        /// Gets the current date.
        /// </summary>
        public static Date Today => DateTimeOffset.Now.ToDate();

        /// <summary>
        /// Represents the earliest possible <see cref="Date"/> value. This field is read-only.
        /// </summary>
        public static Date MinValue => DateTimeOffset.MinValue.ToDate();

        /// <summary>
        /// Represents the greatest possible <see cref="Date"/> value. This field is read-only.
        /// </summary>
        public static Date MaxValue => DateTimeOffset.MaxValue.ToDate();

        /// <summary>
        /// Returns the less of two <see cref="Date"/> instances.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>The instance of <see cref="Date"/> that is the earliest of the two values provided.</returns>
        public static Date Min(Date a, Date b)
        {
            return (a > b) ? b : a;
        }

        /// <summary>
        /// Returns the greatest of two <see cref="Date"/> instances.
        /// </summary>
        /// <param name="a">The first object to compare.</param>
        /// <param name="b">The second object to compare.</param>
        /// <returns>The instance of <see cref="Date"/> that is the latest of the two values provided.</returns>
        public static Date Max(Date a, Date b)
        {
            return (a > b) ? a : b;
        }

        /// <summary>
        /// Converts the specified string representation of a date to its <see cref="Date"/> equivalent using the specified format. 
        /// The format of the string representation must match the specified format exactly.
        /// </summary>
        /// <param name="input">A string that contains a date and time to convert.</param>
        /// <param name="format">A format specifier that defines the required format of <paramref name="input"/>.</param>
        /// <returns>An object that is equivalent to the date contained in <paramref name="input"/>, as specified by <paramref name="format"/>.</returns>
        public static Date ParseExact(string input, string format)
        {
            return DateTimeOffset.ParseExact(input, format, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal).ToDate();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Date"/> structure using the specified year, month and day.
        /// </summary>
        /// <param name="year">The year.</param>
        /// <param name="month">The month (1 through 12).</param>
        /// <param name="day">The day (1 through the number of days in month).</param>
        [DebuggerStepThrough]
        public Date(int year, int month, int day)
        {
            _inner = new DateTimeOffset(year, month, day, 0, 0, 0, TimeSpan.Zero);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Date"/> class with serialized data.
        /// </summary>
        /// <param name="info">The object that holds the serialized object data.</param>
        /// <param name="context">The contextual information about the source or destination.</param>
        public Date(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            _inner = ParseExact((string)info.GetValue("Date", typeof(string)), SerializationFormat).ToDateTimeOffset(TimeSpan.Zero, TimeSpan.Zero);
        }

        private long Ticks => _inner.Ticks;

        /// <summary>
        /// Gets the day of the month represented by the current <see cref="Date"/> object.
        /// </summary>
        public int Day => _inner.Day;

        /// <summary>
        /// Gets the month component of the date represented by the current <see cref="Date"/> object.
        /// </summary>
        public int Month => _inner.Month;

        /// <summary>
        /// Gets the year component of the date represented by the current <see cref="Date"/> object.
        /// </summary>
        public int Year => _inner.Year;

        /// <summary>
        /// Gets the day of the week represented by the current <see cref="Date"/> object.
        /// </summary>
        public DayOfWeek DayOfWeek => _inner.DayOfWeek;

        /// <summary>
        /// Returns the number of days in the year of this instance.
        /// </summary>
        /// <returns>Either 365 or 366</returns>
        public int DaysInYear => DateTime.IsLeapYear(Year) ? 366 : 365;

        /// <summary>
        /// Returns a new <see cref="Date"/> object that adds a specified number of whole days to the value of this instance.
        /// </summary>
        /// <param name="days">A number of whole days. The number can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by the current <see cref="Date"/> object and the number of days represented by days.</returns>
        public Date AddDays(int days)
        {
            return _inner.AddDays(days).ToDate();
        }

        /// <summary>
        /// Returns a new <see cref="Date"/> object that adds a specified number of months to the value of this instance.
        /// </summary>
        /// <param name="months">A number of whole months. The number can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by the current <see cref="Date"/> object and the number of months represented by months.</returns>
        public Date AddMonths(int months)
        {
            return _inner.AddMonths(months).ToDate();
        }

        /// <summary>
        /// Returns a new <see cref="Date"/> object that adds a specified number of years to the value of this instance.
        /// </summary>
        /// <param name="years">A number of years. The number can be negative or positive.</param>
        /// <returns>An object whose value is the sum of the date and time represented by the current <see cref="Date"/> object and the number of years represented by years.</returns>
        public Date AddYears(int years)
        {
            return _inner.AddYears(years).ToDate();
        }



        /// <summary>
        /// Gets a <see cref="DateTime"/> representation of this instance.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> instance.</returns>
        public DateTime ToDateTime()
        {
            return _inner.Date;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent DateTime using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">Ignored.</param>
        /// <returns>A <see cref="DateTime"/> instance equivalent to the value of this instance.</returns>
        public DateTime ToDateTime(IFormatProvider provider)
        {
            return ToDateTime();
        }

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> representation of this instance with the specified time and timezone components.
        /// </summary>
        /// <param name="timeOfDay">The time of day.</param>
        /// <param name="timeZoneOffset">The time zone.</param>
        /// <returns>A <see cref="DateTimeOffset"/> instance.</returns>
        public DateTimeOffset ToDateTimeOffset(TimeSpan timeOfDay, TimeSpan timeZoneOffset)
        {
            return new DateTimeOffset(_inner.Date + (timeOfDay), timeZoneOffset);
        }

        /// <summary>
        /// Gets a <see cref="DateTimeOffset"/> representation of this instance with the specified time and timezone components.
        /// </summary>
        /// <param name="timeOfDay">The time of day.</param>
        /// <param name="timeZoneInfo">The time zone.</param>
        /// <returns>A <see cref="DateTimeOffset"/> instance.</returns>
        public DateTimeOffset ToDateTimeOffset(TimeSpan timeOfDay, TimeZoneInfo timeZoneInfo)
        {
            return new DateTimeOffset(_inner.Date + (timeOfDay), timeZoneInfo.GetUtcOffset((DateTimeOffset) _inner));
        }

        /// <summary>
        /// Compares the current <see cref="Date"/> object to a specified <see cref="Date"/> object and indicates whether the current object is earlier than, the same as, or later than the second <see cref="Date"/> object.
        /// </summary>
        /// <param name="date">An object to compare with the current <see cref="Date"/> object.</param>
        /// <returns><c>-1</c> if the current <see cref="Date"/> is less than <paramref name="date"/>, <c>0</c> if the current <see cref="Date"/> is equal to <paramref name="date"/>, or, <c>1</c> if the current <see cref="Date"/> is greater than <paramref name="date"/>.</returns>
        public int CompareTo(Date date)
        {
            return Ticks.CompareTo(date.Ticks);
        }

        /// <summary>
        /// Determines whether a <see cref="Date"/> object represents the same point in time as a specified object.
        /// </summary>
        /// <param name="obj">The object to compare to the current <see cref="Date"/> object.</param>
        /// <returns><c>true</c> if the <paramref name="obj"/> parameter is a <see cref="Date"/> object and represents the same point in time as the current <see cref="Date"/> object; otherwise, <c>false</c>.</returns>
        public override bool Equals(object obj)
        {
            if (!(obj is Date))
                return false;

            return Equals((Date)obj);
        }

        /// <summary>
        /// Determines whether the current <see cref="Date"/> object represents the same point in time as a specified <see cref="Date"/> object.
        /// </summary>
        /// <param name="date">An object to compare to the current <see cref="Date"/> object.</param>
        /// <returns><c>true</c> if both <see cref="Date"/> objects have the same value value; otherwise, <c>false</c>.</returns>
        public bool Equals(Date date)
        {
            return Ticks == date.Ticks;
        }

        /// <summary>
        /// Returns the hash code for the current <see cref="Date"/> object.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return _inner.GetHashCode();
        }


        /// <summary>
        /// Converts the value of the current <see cref="Date"/> object to its equivalent string representation using the formatting conventions of the current culture.
        /// </summary>
        /// <returns>A string representation of the value of the current <see cref="Date"/> object.</returns>
        public override string ToString()
        {
            return _inner.ToString(CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern);
        }

        /// <summary>
        /// Converts the value of the current <see cref="Date"/> object to its equivalent string representation using the specified format.
        /// </summary>
        /// <param name="format">A standard or custom date and time format string.</param>
        /// <returns>A string representation of value of the current <see cref="Date"/> object as specified by <paramref name="format"/>.</returns>
        public string ToString(string format)
        {
            return _inner.ToString(format);
        }

        /// <summary>
        /// Converts the value of the current <see cref="Date"/> object to its equivalent string representation using the specified format and culture-specific format information.
        /// </summary>
        /// <param name="format">A standard or custom date and time format string.</param>
        /// <param name="formatProvider">An object that supplies culture-specific formatting information.</param>
        /// <returns>A string representation of value of the current <see cref="Date"/> object as specified by <paramref name="format"/> and <paramref name="formatProvider"/>.</returns>
        public string ToString(string format, IFormatProvider formatProvider)
        {
            return _inner.ToString(format, formatProvider);
        }

        #region Operator overloads
        /// <summary>
        /// Determines whether two specified <see cref="Date"/> objects represent the same point in time.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if both <see cref="Date"/> objects have the same value; otherwise, <c>false</c>.</returns>
        public static bool operator ==(Date left, Date right)
        {
            return left.Ticks == right.Ticks;
        }

        /// <summary>
        /// Determines whether two specified <see cref="Date"/> objects refer to different points in time.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if  <paramref name="left"/> and <paramref name="right"/> do not have the same value; otherwise, <c>false</c>.</returns>
        public static bool operator !=(Date left, Date right)
        {
            return left.Ticks != right.Ticks;
        }

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> object is less than a second specified <see cref="Date"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is earlier than the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator <(Date left, Date right)
        {
            return left.Ticks < right.Ticks;
        }

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> object is less than or equal to a second specified <see cref="Date"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is earlier or equal to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator <=(Date left, Date right)
        {
            return left.Ticks <= right.Ticks;
        }

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> object is greater than a second specified <see cref="Date"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is later than the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator >(Date left, Date right)
        {
            return left.Ticks > right.Ticks;
        }

        /// <summary>
        /// Determines whether one specified <see cref="Date"/> object is greater than or equal to a second specified <see cref="Date"/> object.
        /// </summary>
        /// <param name="left">The first object to compare.</param>
        /// <param name="right">The second object to compare.</param>
        /// <returns><c>true</c> if the value of <paramref name="left"/> is later or equal to the value of <paramref name="right"/>; otherwise, <c>false</c>.</returns>
        public static bool operator >=(Date left, Date right)
        {
            return left.Ticks >= right.Ticks;
        }

        /// <summary>
        /// Subtracts a specified <see cref="Date"/> object from another <see cref="Date"/> object.
        /// </summary>
        /// <param name="left">The minuend.</param>
        /// <param name="right">The subtrahend.</param>
        /// <returns>An object that represents the difference between left and right.</returns>
        public static TimeSpan operator -(Date left, Date right)
        {
            return new DateTime(left.Ticks) - new DateTime(right.Ticks);
        }

        #endregion

        #region Explicit interface implementations
        /// <summary>
        /// This API supports the product infrastructure and is not intended to be used directly from your code. 
        /// Populates a <see cref="SerializationInfo"/> object with the data required to serialize the current <see cref="Date"/> object.
        /// </summary>
        /// <param name="info">The object to populate with data.</param>
        /// <param name="context">The destination for this serialization (see <see cref="StreamingContext"/>).</param>
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue((string) "Date", (object) ToString(SerializationFormat));
        }

        /// <summary>
        /// Returns the <see cref="TypeCode"/> for this instance.
        /// </summary>
        /// <returns>The enumerated constant that is the <see cref="TypeCode"/> of the class or value type that implements this interface.</returns>
        TypeCode IConvertible.GetTypeCode()
        {
            return TypeCode.Object;
        }



        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="String"/> using the specified culture-specific formatting information.
        /// </summary>
        /// <param name="provider">Ignored</param>
        /// <returns>A <see cref="String"/> instance equivalent to the value of this instance.</returns>
        string IConvertible.ToString(IFormatProvider provider)
        {
            return ToString();
        }


        #region IConvertible interface members that are not implemented 
        bool IConvertible.ToBoolean(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Boolean");
        }

        char IConvertible.ToChar(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Char");
        }

        sbyte IConvertible.ToSByte(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Signed Byte");
        }

        byte IConvertible.ToByte(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Byte");
        }

        short IConvertible.ToInt16(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Short");
        }

        ushort IConvertible.ToUInt16(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Unsigned Short");
        }

        int IConvertible.ToInt32(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Int");
        }

        uint IConvertible.ToUInt32(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Unsigned Int");
        }

        long IConvertible.ToInt64(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Long");
        }

        ulong IConvertible.ToUInt64(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Unsigned Long");
        }

        float IConvertible.ToSingle(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Float");
        }

        double IConvertible.ToDouble(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Double");
        }

        decimal IConvertible.ToDecimal(IFormatProvider provider)
        {
            throw new InvalidCastException("Invalid cast from Date to Decimal");
        }

        object IConvertible.ToType(Type conversionType, IFormatProvider provider)
        {
            throw new InvalidCastException($"Invalid cast from Date to {conversionType.FullName}");
        }
        #endregion

        #endregion
    }

    /// <summary>
    /// Extension methods to <see cref="DateTimeOffset"/> and 
    /// </summary>
    public static class DateTimeOffsetExtensions
    {
        /// <summary>
        /// Returns the <see cref="Date"/> component of the provided <see cref="DateTimeOffset"/> in the timezone of the provided DateTimeOffset
        /// </summary>
        [DebuggerStepThrough]
        public static Date ToDate(this DateTimeOffset date)
        {
            var cache = date.Date;  //Avoid 2 extra allocations from in DateTimeOffset. -LC
            return new Date(cache.Year, cache.Month, cache.Day);
        }

        /// <summary>
        /// Returns the <see cref="Date"/> component of the provided <see cref="DateTimeOffset"/> in the provided conversion timezone
        /// </summary>
        public static Date ToDate(this DateTimeOffset date, TimeZoneInfo conversionTimezone)
        {
            return ToDate(TimeZoneInfo.ConvertTime(date, conversionTimezone));
        }

        /// <summary>
        /// Returns the <see cref="Date"/> component of the provided <see cref="DateTimeOffset"/> in the Back Office timezone
        /// </summary>
        public static Date ToBackOfficeDate(this DateTimeOffset date)
        {
            return ToDate(date, DateTimeOffsetDateHelper.BackOfficeTimeZoneInfo);
        }

        /// <summary>
        /// Returns the specified <see cref="DateTimeOffset"/> in the Back Office timezone
        /// </summary>
        public static DateTimeOffset ToBackOffice(this DateTimeOffset timestamp)
        {
            return TimeZoneInfo.ConvertTime(timestamp, DateTimeOffsetDateHelper.BackOfficeTimeZoneInfo);
        }
    }
    /// <summary>
    /// Extension methods to <see cref="DateTimeOffset"/> and 
    /// </summary>
    public static class DateTimeOffsetDateHelper
    {
        /// <summary>
        /// Time zone for the backoffice.
        /// </summary>
        public static readonly TimeZoneInfo BackOfficeTimeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("E. Australia Standard Time");
    }
}