namespace EtAlii.Ubigia
{
    using System;

    public partial struct Relation
    {
        public static Relation NewRelation(in Identifier id)
        {
            return new Relation
            {
                _id = id,
                _moment = (ulong)DateTime.UtcNow.Ticks,
            };
        }
        
        internal static Relation Create(in Identifier id, ulong moment)
        {
            return new Relation
            {
                _id = id,
                _moment = moment,
            };
        }
        
        
    }
}
