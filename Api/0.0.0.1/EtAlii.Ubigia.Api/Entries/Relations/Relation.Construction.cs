namespace EtAlii.Ubigia.Api
{
    using System;

    public partial struct Relation
    {
        public static Relation NewRelation(Identifier id)
        {
            return new Relation
            {
                Id = id,
                Moment = (UInt64)DateTime.UtcNow.Ticks,
            };
        }
    }
}
