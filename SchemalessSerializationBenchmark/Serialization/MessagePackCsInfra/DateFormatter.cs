using SchemalessSerializationBenchmark.Serialization.Contracts;

namespace SchemalessSerializationBenchmark.Serialization.MessagePackCsInfra
{
    //TODO: Candidate for further benchmarking. -LC
    //  Can it be smaller on the wire?
    //  Can it be faster to deserialize?

    /// <summary>
    /// Formats a date as an juman readbale integer i.e. 31 Jan 2000 is represented as the integer 20000131
    /// </summary>
    public sealed class DateFormatter : MessagePack.Formatters.IMessagePackFormatter<Date>
    {
        private const long YearMultiplier = 109951163L;
        private const int YearShift = 40;
        private const long MonthMultiplier = 1311L;
        private const int MonthShift = 17;
        //private 
        public int Serialize(ref byte[] bytes, int offset, Date input, MessagePack.IFormatterResolver formatterResolver)
        {
            //if (value == null)
            //{
            //    return MessagePack.MessagePackBinary.WriteNil(ref bytes, offset);
            //}
            int token = input.Year * 10000 + input.Month * 100 + input.Day;
            return MessagePack.MessagePackBinary.WriteInt32(ref bytes, offset, token);
        }

        public Date Deserialize(byte[] bytes, int offset, MessagePack.IFormatterResolver formatterResolver, out int readSize)
        {
            //if (MessagePack.MessagePackBinary.IsNil(bytes, offset))
            //{
            //    readSize = 1;
            //    return null;
            //}



            long token = MessagePack.MessagePackBinary.ReadInt32(bytes, offset, out readSize);
            //var year = token / 10000;
            var year = (token * YearMultiplier >> YearShift);
            token = token - (year * 10000);
            //var month = token / 100;
            var month = (token * MonthMultiplier >> MonthShift);
            token = token - (month * 100);
            var day = token;
            return new Date((int) year, (int) month, (int) day);
        }
    }
}
