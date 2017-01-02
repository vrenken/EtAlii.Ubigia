namespace EtAlii.Servus.Model.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public partial struct Identifier
    {
        public static Identifier Create(UInt64 root, string account)
        {
            return new Identifier
            {
                _root = root,
                _account = account,
                _period = UInt64.MinValue,
                _moment = UInt64.MinValue,
            };
        }

        public static Identifier Create(UInt64 root, string account, UInt64 period, UInt64 moment)
        {
            return new Identifier
            {
                _root = root,
                _account = account,
                _period = period,
                _moment = moment,
            };
        }
    }
}
