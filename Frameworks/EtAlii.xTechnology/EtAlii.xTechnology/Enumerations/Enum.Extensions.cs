namespace EtAlii.xTechnology.Enumerations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class EnumExtensions
    {
        public static IEnumerable<T> GetIndividualValues<T>(this Enum enumeration) where T : struct
        {
            return enumeration.ToString()
                              .Split(new[] { ',' })
                              .Select(x => (T)Enum.Parse(typeof(T), x.Trim(), false))
                              .ToUniqueFlagEnumValues();
        }

        private static bool IsPowerOfTwo(this int value)
        {
            return (value & (value - 1)) == 0;
        }

        public static IEnumerable<T> ToUniqueFlagEnumValues<T>(this IEnumerable<T> flagsEnumValues) where T : struct
        {
            foreach (T item in flagsEnumValues)
            {
                int intValue = System.Convert.ToInt32(item);
                //if our int is a power of two, its a unique value of the flags enum
                if (intValue.IsPowerOfTwo())
                {
                    yield return item;
                }
                //otherwise its a combination of several unique values and we need to break it down further
                else
                {
                    //the enum value output as binary string representation
                    var fullBinaryString = System.Convert.ToString(intValue, 2)
                                                            .ToCharArray();
                    //an empty template with all 0's that is the length of our binary string
                    char[] individualBitTemplate = new string('0', fullBinaryString.Length).ToCharArray();

                    IEnumerable<T> individualFlagsEnumValues = fullBinaryString
                        .Select((character, index) =>
                        {
                            //project each individual bit into its own binary string with 0's in every position
                            //other than the index of the individual bit
                            //Example: binary string 1111
                            //produces 4 individual binary strings
                            //0001
                            //0010
                            //0100
                            //1000
                            char[] template = (char[])individualBitTemplate.Clone();
                            template[index] = character;
                            return new string(template);
                        })
                        .Where(individualBitBinaryString =>
                        {
                            //filter the results to exclude any binary strings that are all 0's
                            return !individualBitBinaryString.ToCharArray()
                                                             .All(character => character == '0');
                        })
                        .Select(individualBitBinaryString =>
                        {
                            //cast the individual binary strings back to their int value, and then into the enum value
                            int intValueOfIndividualBit = System.Convert.ToInt32(individualBitBinaryString, 2);
                            return (T)Enum.ToObject(typeof(T), intValueOfIndividualBit);
                        });

                    foreach (T value in individualFlagsEnumValues)
                    {
                        yield return value;
                    }
                }
            }
        }

        public static EnumDelta<T> GetDifference<T>(this Enum source, Enum compare) where T : struct
        {
            var sourceValues = source.GetIndividualValues<T>();
            var compareValues = compare.GetIndividualValues<T>();

            var added = compareValues.Where(value => !sourceValues.Contains(value));
            var removed = sourceValues.Where(value => !compareValues.Contains(value));
            return new EnumDelta<T>(added, removed);
        }
    }
}
