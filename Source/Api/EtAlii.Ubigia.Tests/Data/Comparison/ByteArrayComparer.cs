namespace EtAlii.Ubigia.Tests;

public class ByteArrayComparer
{
    public bool AreEqual(byte[] expected, byte[] actual)
    {
        if (expected.Length != actual.Length)
        {
            return false;
        }

        for (var i = 0; i < expected.Length; i++)
        {
            if (expected[i] != actual[i])
            {
                return false;
            }
        }
        return true;
    }


    public bool AreNotEqual(byte[] first, byte[] second)
    {
        var areEqual = true;

        try
        {
            areEqual &= first != second || (first == null && second == null);
            if (areEqual)
            {
                areEqual &= first.Length == second.Length;
            }
            if (areEqual)
            {
                for (var i = 0; i < first.Length; i++)
                {
                    areEqual &= first[i] == second[i];
                }
            }
        }
        catch
        {
            areEqual = false;
        }

        return !areEqual;
    }
}
