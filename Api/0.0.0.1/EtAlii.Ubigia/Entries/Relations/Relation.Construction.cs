namespace EtAlii.Ubigia
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
        
        internal static Relation Create(Identifier id, UInt64 moment)
        {
            return new Relation
            {
                Id = id,
                Moment = moment,
            };
        }
        
        
    }
}
