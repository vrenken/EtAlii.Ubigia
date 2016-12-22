namespace EtAlii
{
    using System;

    public static class RandomExtensions
    {
        public static T Next<T>(this System.Random rnd, ref T[] objects)
        {
            int index = rnd.Next(objects.Length);
            return objects[index];
        }

        public static T Next<T>(this System.Random rnd, T[] objects)
        {
            int index = rnd.Next(objects.Length);
            return objects[index];
        }
        public static bool NextBool(this System.Random rnd)
        {
            int i = rnd.Next(0, 2);
            switch (i)
            {
                case 0: return false;
                case 1: return true;
            }
            throw new InvalidOperationException();
        }

        public static float NextSingle(this System.Random rnd)
        {
            return (float)rnd.NextDouble();
        }

        public static bool NextBool(this System.Random rnd, double probabilityForTrue)
        {
            double i = rnd.NextDouble();
            i = i * ((double)0.5f / probabilityForTrue);
            return i < (double)0.5f;
        }

        public static T NextEnum<T>(this System.Random rnd)
            where T: struct
        {
            Type enumType = typeof(T);

            //if (!enumType.GetTypeInfo().IsEnum)
            //{
            //    throw new InvalidOperationException("Specified generic parameter must be an enumeration.");
            //}

            var enumValues = (object[])Enum.GetValues(enumType);
            var enumIndexToChoose = rnd.Next(0, enumValues.Length);
            return (T)enumValues[enumIndexToChoose];
        }

        public static T Next<T>(this System.Random rnd, Chance<T>[] chances)
        {
            if (chances == null) { throw new ArgumentNullException(@"chances"); }

            double sum = 0;
            foreach(Chance<T> chance in chances)
            {
                sum += chance.Probability;
            }

            //if (sum != 1.0f)
            //{
            //    throw new InvalidOperationException("The chance sum of the specified chances does not equal one.");
            //}

            double min = 0.0f;
            double value = (float)rnd.NextDouble();

            T valueToReturn = default(T);
            foreach (Chance<T> chance in chances)
            {
                double max = min + chance.Probability;

                if (min <= value && value < max)
                {
                    valueToReturn = chance.Value;
                    break;
                }
                min = max;
            }
            return valueToReturn;
        }
    }
}
