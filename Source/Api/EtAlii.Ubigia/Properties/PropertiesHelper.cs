// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia
{
    public static class PropertiesHelper
    {
        public static void SetStored(IPropertyDictionary properties, bool stored)
        {
            ((PropertyDictionary)properties).Stored = stored;
        }
    }
}
