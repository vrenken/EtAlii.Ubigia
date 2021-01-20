-- ==================================================================================================================
-- 01. Assign a constant to a graph node property and map it to a GCL property.
Person = Person:Doe/John
{
    NickName = "Johnny"
}

-- ==================================================================================================================
-- 02. Assign a variable to a graph node property and map it to a GCL property.
Person = Person:Doe/John
{
    NickName = $nickName
}

-- ==================================================================================================================
-- 03. Assign a variable to a graph node property and map it to a GCL property.
-- The type of the GCL property will be used to verify and process the variable.
Person = Person:Doe/John
{
    string NickName = $nickName
    datetime Birthday = .BirtDate = @birthday
}

