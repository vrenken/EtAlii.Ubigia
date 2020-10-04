namespace EtAlii.Ubigia
{
    using System;

    public partial struct Relation
    {
        public static Relation NewRelation(Identifier id)
        {
            return new Relation
            {
                _id = id,
                Moment = (ulong)DateTime.UtcNow.Ticks,
            };
        }
        
        internal static Relation Create(Identifier id, ulong moment)
        {
            return new Relation
            {
                _id = id,
                Moment = moment,
            };
        }
        
        
    }
}
