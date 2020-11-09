// ReSharper disable All

namespace HashLib.Crypto
{
    internal class SHA0 : BlockHash, ICryptoNotBuildIn
    {
        #region Consts
        protected const uint C1 = 0x5A827999;
        protected const uint C2 = 0x6ED9EBA1;
        protected const uint C3 = 0x8F1BBCDC;
        protected const uint C4 = 0xCA62C1D6;
        #endregion

        protected uint[] m_state = new uint[5];

        public SHA0()
            : base(20, 64)
        {
            Initialize();
        }

        public override void Initialize()
        {
            m_state[0] = 0x67452301;
            m_state[1] = 0xEFCDAB89;
            m_state[2] = 0x98BADCFE;
            m_state[3] = 0x10325476;
            m_state[4] = 0xC3D2E1F0;

            base.Initialize();
        }

        protected override void Finish()
        {
            ulong bits = m_processed_bytes * 8;
            int padindex = (m_buffer.Pos < 56) ? (56 - m_buffer.Pos) : (120 - m_buffer.Pos);

            byte[] pad = new byte[padindex + 8];

            pad[0] = 0x80;

            Converters.ConvertULongToBytesSwapOrder(bits, pad, padindex);
            padindex += 8;

            TransformBytes(pad, 0, padindex);
        }

        protected virtual void Expand(uint[] a_data)
        {
            for (int j = 0x10; j < 80; j++)
                a_data[j] = ((a_data[j - 3] ^ a_data[j - 8]) ^ a_data[j - 14]) ^ a_data[j - 0x10];
        }

        protected override void TransformBlock(byte[] a_data, int a_index)
        {
            uint[] data = new uint[80];
            Converters.ConvertBytesToUIntsSwapOrder(a_data, a_index, BlockSize, data, 0);

            Expand(data);

            uint A = m_state[0];
            uint B = m_state[1];
            uint C = m_state[2];
            uint D = m_state[3];
            uint E = m_state[4];

            int r = 0;

            uint T1 = data[r++] + SHA0.C1 + ((A << 5) | (A >> (32 - 5))) + ((B & C) | (~B & D)) + E;
            uint X7 = ((B << 30) | (B >> (32 - 30)));
            uint T2 = data[r++] + SHA0.C1 + ((T1 << 5) | (T1 >> (32 - 5))) + ((A & X7) | (~A & C)) + D;
            uint X8 = ((A << 30) | (A >> (32 - 30)));
            uint T3 = data[r++] + SHA0.C1 + ((T2 << 5) | (T2 >> (32 - 5))) + ((T1 & X8) | (~T1 & X7)) + C;
            uint X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            uint T4 = data[r++] + SHA0.C1 + ((T3 << 5) | (T3 >> (32 - 5))) + ((T2 & X1) | (~T2 & X8)) + X7;
            uint X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            uint T5 = data[r++] + SHA0.C1 + ((T4 << 5) | (T4 >> (32 - 5))) + ((T3 & X2) | (~T3 & X1)) + X8;
            uint X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            uint T6 = data[r++] + SHA0.C1 + ((T5 << 5) | (T5 >> (32 - 5))) + ((T4 & X3) | (~T4 & X2)) + X1;
            uint X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            uint T7 = data[r++] + SHA0.C1 + ((T6 << 5) | (T6 >> (32 - 5))) + ((T5 & X4) | (~T5 & X3)) + X2;
            uint X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            uint T8 = data[r++] + SHA0.C1 + ((T7 << 5) | (T7 >> (32 - 5))) + ((T6 & X5) | (~T6 & X4)) + X3;
            uint X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C1 + ((T8 << 5) | (T8 >> (32 - 5))) + ((T7 & X6) | (~T7 & X5)) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C1 + ((T1 << 5) | (T1 >> (32 - 5))) + ((T8 & X7) | (~T8 & X6)) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C1 + ((T2 << 5) | (T2 >> (32 - 5))) + ((T1 & X8) | (~T1 & X7)) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C1 + ((T3 << 5) | (T3 >> (32 - 5))) + ((T2 & X1) | (~T2 & X8)) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C1 + ((T4 << 5) | (T4 >> (32 - 5))) + ((T3 & X2) | (~T3 & X1)) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C1 + ((T5 << 5) | (T5 >> (32 - 5))) + ((T4 & X3) | (~T4 & X2)) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C1 + ((T6 << 5) | (T6 >> (32 - 5))) + ((T5 & X4) | (~T5 & X3)) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C1 + ((T7 << 5) | (T7 >> (32 - 5))) + ((T6 & X5) | (~T6 & X4)) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C1 + ((T8 << 5) | (T8 >> (32 - 5))) + ((T7 & X6) | (~T7 & X5)) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C1 + ((T1 << 5) | (T1 >> (32 - 5))) + ((T8 & X7) | (~T8 & X6)) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C1 + ((T2 << 5) | (T2 >> (32 - 5))) + ((T1 & X8) | (~T1 & X7)) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C1 + ((T3 << 5) | (T3 >> (32 - 5))) + ((T2 & X1) | (~T2 & X8)) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C2 + ((T4 << 5) | (T4 >> (32 - 5))) + (T3 ^ X2 ^ X1) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C2 + ((T5 << 5) | (T5 >> (32 - 5))) + (T4 ^ X3 ^ X2) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C2 + ((T6 << 5) | (T6 >> (32 - 5))) + (T5 ^ X4 ^ X3) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C2 + ((T7 << 5) | (T7 >> (32 - 5))) + (T6 ^ X5 ^ X4) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C2 + ((T8 << 5) | (T8 >> (32 - 5))) + (T7 ^ X6 ^ X5) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C2 + ((T1 << 5) | (T1 >> (32 - 5))) + (T8 ^ X7 ^ X6) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C2 + ((T2 << 5) | (T2 >> (32 - 5))) + (T1 ^ X8 ^ X7) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C2 + ((T3 << 5) | (T3 >> (32 - 5))) + (T2 ^ X1 ^ X8) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C2 + ((T4 << 5) | (T4 >> (32 - 5))) + (T3 ^ X2 ^ X1) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C2 + ((T5 << 5) | (T5 >> (32 - 5))) + (T4 ^ X3 ^ X2) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C2 + ((T6 << 5) | (T6 >> (32 - 5))) + (T5 ^ X4 ^ X3) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C2 + ((T7 << 5) | (T7 >> (32 - 5))) + (T6 ^ X5 ^ X4) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C2 + ((T8 << 5) | (T8 >> (32 - 5))) + (T7 ^ X6 ^ X5) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C2 + ((T1 << 5) | (T1 >> (32 - 5))) + (T8 ^ X7 ^ X6) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C2 + ((T2 << 5) | (T2 >> (32 - 5))) + (T1 ^ X8 ^ X7) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C2 + ((T3 << 5) | (T3 >> (32 - 5))) + (T2 ^ X1 ^ X8) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C2 + ((T4 << 5) | (T4 >> (32 - 5))) + (T3 ^ X2 ^ X1) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C2 + ((T5 << 5) | (T5 >> (32 - 5))) + (T4 ^ X3 ^ X2) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C2 + ((T6 << 5) | (T6 >> (32 - 5))) + (T5 ^ X4 ^ X3) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C2 + ((T7 << 5) | (T7 >> (32 - 5))) + (T6 ^ X5 ^ X4) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C3 + ((T8 << 5) | (T8 >> (32 - 5))) + ((T7 & X6) | (T7 & X5) | (X6 & X5)) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C3 + ((T1 << 5) | (T1 >> (32 - 5))) + ((T8 & X7) | (T8 & X6) | (X7 & X6)) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C3 + ((T2 << 5) | (T2 >> (32 - 5))) + ((T1 & X8) | (T1 & X7) | (X8 & X7)) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C3 + ((T3 << 5) | (T3 >> (32 - 5))) + ((T2 & X1) | (T2 & X8) | (X1 & X8)) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C3 + ((T4 << 5) | (T4 >> (32 - 5))) + ((T3 & X2) | (T3 & X1) | (X2 & X1)) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C3 + ((T5 << 5) | (T5 >> (32 - 5))) + ((T4 & X3) | (T4 & X2) | (X3 & X2)) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C3 + ((T6 << 5) | (T6 >> (32 - 5))) + ((T5 & X4) | (T5 & X3) | (X4 & X3)) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C3 + ((T7 << 5) | (T7 >> (32 - 5))) + ((T6 & X5) | (T6 & X4) | (X5 & X4)) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C3 + ((T8 << 5) | (T8 >> (32 - 5))) + ((T7 & X6) | (T7 & X5) | (X6 & X5)) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C3 + ((T1 << 5) | (T1 >> (32 - 5))) + ((T8 & X7) | (T8 & X6) | (X7 & X6)) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C3 + ((T2 << 5) | (T2 >> (32 - 5))) + ((T1 & X8) | (T1 & X7) | (X8 & X7)) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C3 + ((T3 << 5) | (T3 >> (32 - 5))) + ((T2 & X1) | (T2 & X8) | (X1 & X8)) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C3 + ((T4 << 5) | (T4 >> (32 - 5))) + ((T3 & X2) | (T3 & X1) | (X2 & X1)) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C3 + ((T5 << 5) | (T5 >> (32 - 5))) + ((T4 & X3) | (T4 & X2) | (X3 & X2)) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C3 + ((T6 << 5) | (T6 >> (32 - 5))) + ((T5 & X4) | (T5 & X3) | (X4 & X3)) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C3 + ((T7 << 5) | (T7 >> (32 - 5))) + ((T6 & X5) | (T6 & X4) | (X5 & X4)) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C3 + ((T8 << 5) | (T8 >> (32 - 5))) + ((T7 & X6) | (T7 & X5) | (X6 & X5)) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C3 + ((T1 << 5) | (T1 >> (32 - 5))) + ((T8 & X7) | (T8 & X6) | (X7 & X6)) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C3 + ((T2 << 5) | (T2 >> (32 - 5))) + ((T1 & X8) | (T1 & X7) | (X8 & X7)) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C3 + ((T3 << 5) | (T3 >> (32 - 5))) + ((T2 & X1) | (T2 & X8) | (X1 & X8)) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C4 + ((T4 << 5) | (T4 >> (32 - 5))) + (T3 ^ X2 ^ X1) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C4 + ((T5 << 5) | (T5 >> (32 - 5))) + (T4 ^ X3 ^ X2) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C4 + ((T6 << 5) | (T6 >> (32 - 5))) + (T5 ^ X4 ^ X3) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C4 + ((T7 << 5) | (T7 >> (32 - 5))) + (T6 ^ X5 ^ X4) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C4 + ((T8 << 5) | (T8 >> (32 - 5))) + (T7 ^ X6 ^ X5) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C4 + ((T1 << 5) | (T1 >> (32 - 5))) + (T8 ^ X7 ^ X6) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C4 + ((T2 << 5) | (T2 >> (32 - 5))) + (T1 ^ X8 ^ X7) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C4 + ((T3 << 5) | (T3 >> (32 - 5))) + (T2 ^ X1 ^ X8) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C4 + ((T4 << 5) | (T4 >> (32 - 5))) + (T3 ^ X2 ^ X1) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C4 + ((T5 << 5) | (T5 >> (32 - 5))) + (T4 ^ X3 ^ X2) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C4 + ((T6 << 5) | (T6 >> (32 - 5))) + (T5 ^ X4 ^ X3) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));
            T8 = data[r++] + SHA0.C4 + ((T7 << 5) | (T7 >> (32 - 5))) + (T6 ^ X5 ^ X4) + X3;
            X6 = ((T6 << 30) | (T6 >> (32 - 30)));
            T1 = data[r++] + SHA0.C4 + ((T8 << 5) | (T8 >> (32 - 5))) + (T7 ^ X6 ^ X5) + X4;
            X7 = ((T7 << 30) | (T7 >> (32 - 30)));
            T2 = data[r++] + SHA0.C4 + ((T1 << 5) | (T1 >> (32 - 5))) + (T8 ^ X7 ^ X6) + X5;
            X8 = ((T8 << 30) | (T8 >> (32 - 30)));
            T3 = data[r++] + SHA0.C4 + ((T2 << 5) | (T2 >> (32 - 5))) + (T1 ^ X8 ^ X7) + X6;
            X1 = ((T1 << 30) | (T1 >> (32 - 30)));
            T4 = data[r++] + SHA0.C4 + ((T3 << 5) | (T3 >> (32 - 5))) + (T2 ^ X1 ^ X8) + X7;
            X2 = ((T2 << 30) | (T2 >> (32 - 30)));
            T5 = data[r++] + SHA0.C4 + ((T4 << 5) | (T4 >> (32 - 5))) + (T3 ^ X2 ^ X1) + X8;
            X3 = ((T3 << 30) | (T3 >> (32 - 30)));
            T6 = data[r++] + SHA0.C4 + ((T5 << 5) | (T5 >> (32 - 5))) + (T4 ^ X3 ^ X2) + X1;
            X4 = ((T4 << 30) | (T4 >> (32 - 30)));
            T7 = data[r++] + SHA0.C4 + ((T6 << 5) | (T6 >> (32 - 5))) + (T5 ^ X4 ^ X3) + X2;
            X5 = ((T5 << 30) | (T5 >> (32 - 30)));

            m_state[0] += data[r++] + SHA0.C4 + ((T7 << 5) | (T7 >> (32 - 5))) + (T6 ^ X5 ^ X4) + X3;
            m_state[1] += T7;
            m_state[2] += ((T6 << 30) | (T6 >> (32 - 30)));
            m_state[3] += X5;
            m_state[4] += X4;
        }

        protected override byte[] GetResult()
        {
            return Converters.ConvertUIntsToBytesSwapOrder(m_state);
        }
    }
}