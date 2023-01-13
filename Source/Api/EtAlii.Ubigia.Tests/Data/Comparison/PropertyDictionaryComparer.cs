namespace EtAlii.Ubigia.Tests;

public class PropertyDictionaryComparer
{
    public bool AreEqual(IPropertyDictionary expected, IPropertyDictionary actual)
    {
        if (actual == null)
        {
            return false;
        }

        if (expected.Count != actual.Count)
        {
            return false;
        }
        foreach (var kvp in expected)
        {
            if (!actual.ContainsKey(kvp.Key))
            {
                return false;
            }
            if (!kvp.Value.Equals(actual[kvp.Key]))
            {
                return false;
            }
        }
        return true;
    }
}
