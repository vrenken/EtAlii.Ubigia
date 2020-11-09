// ReSharper disable all

namespace HashLib.Crypto
{
    internal class MD4 : MDBase
    {
        public MD4() 
            : base(4, 16)
        {
        }

        protected override void TransformBlock(byte[] a_data, int a_index)
        {
            var data0 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 0);
            var data1 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 1);
            var data2 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 2);
            var data3 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 3);
            var data4 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 4);
            var data5 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 5);
            var data6 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 6);
            var data7 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 7);
            var data8 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 8);
            var data9 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 9);
            var data10 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 10);
            var data11 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 11);
            var data12 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 12);
            var data13 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 13);
            var data14 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 14);
            var data15 = Converters.ConvertBytesToUInt(a_data, a_index + 4 * 15);

            var a = m_state[0];
            var b = m_state[1];
            var c = m_state[2];
            var d = m_state[3];

            a += data0 + ((b & c) | ((~b) & d));
            a = a << 3 | a >> (32 - 3);
            d += data1 + ((a & b) | ((~a) & c));
            d = d << 7 | d >> (32 - 7);
            c += data2 + ((d & a) | ((~d) & b));
            c = c << 11 | c >> (32 - 11);
            b += data3 + ((c & d) | ((~c) & a));
            b = b << 19 | b >> (32 - 19);
            a += data4 + ((b & c) | ((~b) & d));
            a = a << 3 | a >> (32 - 3);
            d += data5 + ((a & b) | ((~a) & c));
            d = d << 7 | d >> (32 - 7);
            c += data6 + ((d & a) | ((~d) & b));
            c = c << 11 | c >> (32 - 11);
            b += data7 + ((c & d) | ((~c) & a));
            b = b << 19 | b >> (32 - 19);
            a += data8 + ((b & c) | ((~b) & d));
            a = a << 3 | a >> (32 - 3);
            d += data9 + ((a & b) | ((~a) & c));
            d = d << 7 | d >> (32 - 7);
            c += data10 + ((d & a) | ((~d) & b));
            c = c << 11 | c >> (32 - 11);
            b += data11 + ((c & d) | ((~c) & a));
            b = b << 19 | b >> (32 - 19);
            a += data12 + ((b & c) | ((~b) & d));
            a = a << 3 | a >> (32 - 3);
            d += data13 + ((a & b) | ((~a) & c));
            d = d << 7 | d >> (32 - 7);
            c += data14 + ((d & a) | ((~d) & b));
            c = c << 11 | c >> (32 - 11);
            b += data15 + ((c & d) | ((~c) & a));
            b = b << 19 | b >> (32 - 19);

            a += data0 + C2 + ((b & (c | d)) | (c & d));
            a = a << 3 | a >> (32 - 3);
            d += data4 + C2 + ((a & (b | c)) | (b & c));
            d = d << 5 | d >> (32 - 5);
            c += data8 + C2 + ((d & (a | b)) | (a & b));
            c = c << 9 | c >> (32 - 9);
            b += data12 + C2 + ((c & (d | a)) | (d & a));
            b = b << 13 | b >> (32 - 13);
            a += data1 + C2 + ((b & (c | d)) | (c & d));
            a = a << 3 | a >> (32 - 3);
            d += data5 + C2 + ((a & (b | c)) | (b & c));
            d = d << 5 | d >> (32 - 5);
            c += data9 + C2 + ((d & (a | b)) | (a & b));
            c = c << 9 | c >> (32 - 9);
            b += data13 + C2 + ((c & (d | a)) | (d & a));
            b = b << 13 | b >> (32 - 13);
            a += data2 + C2 + ((b & (c | d)) | (c & d));
            a = a << 3 | a >> (32 - 3);
            d += data6 + C2 + ((a & (b | c)) | (b & c));
            d = d << 5 | d >> (32 - 5);
            c += data10 + C2 + ((d & (a | b)) | (a & b));
            c = c << 9 | c >> (32 - 9);
            b += data14 + C2 + ((c & (d | a)) | (d & a));
            b = b << 13 | b >> (32 - 13);
            a += data3 + C2 + ((b & (c | d)) | (c & d));
            a = a << 3 | a >> (32 - 3);
            d += data7 + C2 + ((a & (b | c)) | (b & c));
            d = d << 5 | d >> (32 - 5);
            c += data11 + C2 + ((d & (a | b)) | (a & b));
            c = c << 9 | c >> (32 - 9);
            b += data15 + C2 + ((c & (d | a)) | (d & a));
            b = b << 13 | b >> (32 - 13);

            a += data0 + C4 + (b ^ c ^ d);
            a = a << 3 | a >> (32 - 3);
            d += data8 + C4 + (a ^ b ^ c);
            d = d << 9 | d >> (32 - 9);
            c += data4 + C4 + (d ^ a ^ b);
            c = c << 11 | c >> (32 - 11);
            b += data12 + C4 + (c ^ d ^ a);
            b = b << 15 | b >> (32 - 15);
            a += data2 + C4 + (b ^ c ^ d);
            a = a << 3 | a >> (32 - 3);
            d += data10 + C4 + (a ^ b ^ c);
            d = d << 9 | d >> (32 - 9);
            c += data6 + C4 + (d ^ a ^ b);
            c = c << 11 | c >> (32 - 11);
            b += data14 + C4 + (c ^ d ^ a);
            b = b << 15 | b >> (32 - 15);
            a += data1 + C4 + (b ^ c ^ d);
            a = a << 3 | a >> (32 - 3);
            d += data9 + C4 + (a ^ b ^ c);
            d = d << 9 | d >> (32 - 9);
            c += data5 + C4 + (d ^ a ^ b);
            c = c << 11 | c >> (32 - 11);
            b += data13 + C4 + (c ^ d ^ a);
            b = b << 15 | b >> (32 - 15);
            a += data3 + C4 + (b ^ c ^ d);
            a = a << 3 | a >> (32 - 3);
            d += data11 + C4 + (a ^ b ^ c);
            d = d << 9 | d >> (32 - 9);
            c += data7 + C4 + (d ^ a ^ b);
            c = c << 11 | c >> (32 - 11);
            b += data15 + C4 + (c ^ d ^ a);
            b = b << 15 | b >> (32 - 15);

            m_state[0] += a;
            m_state[1] += b;
            m_state[2] += c;
            m_state[3] += d;
        }
    }
}
