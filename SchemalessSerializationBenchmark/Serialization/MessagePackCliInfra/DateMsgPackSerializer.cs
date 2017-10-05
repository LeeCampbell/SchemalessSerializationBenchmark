using MsgPack;
using MsgPack.Serialization;
using SchemalessSerializationBenchmark.Serialization.Contracts;

namespace SchemalessSerializationBenchmark.Serialization.MessagePackCliInfra
{
    public sealed class DateMsgPackSerializer : MessagePackSerializer<Date>
    {
        public DateMsgPackSerializer(SerializationContext ownerContext)
            : base(ownerContext)
        {

        }

        protected override void PackToCore(Packer packer, Date input)
        {
            //var daysSinceEpoch = (int)(input.ToDateTime() - Epoc).TotalDays;
            //var token = int.Parse($"{input.Year}{input.Month:00}{input.Day:00}");
            int token = input.Year * 10000 + input.Month * 100 + input.Day;
            packer.Pack(token);
        }

        protected override Date UnpackFromCore(Unpacker unpacker)
        {
            //int daysSinceEpoch = unpacker.LastReadData.AsInt32();
            //var inner = Epoc.AddDays(daysSinceEpoch);

            int token = unpacker.LastReadData.AsInt32();
            var year = token / 10000;
            token = token - (year * 10000);
            var month = token / 100;
            token = token - (month * 100);
            var day = token;

            return new Date(year, month, day);
        }
    }
}
