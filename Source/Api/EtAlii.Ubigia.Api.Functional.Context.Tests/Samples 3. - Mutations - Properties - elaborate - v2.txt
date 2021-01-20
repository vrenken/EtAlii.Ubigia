-- ==================================================================================================================
-- 01. Assign a constant to a graph node property and map it to a GCL property.
Person = @node(Person:Doe/John)
{
    NickName = @value("Johnny")
}

-- ==================================================================================================================
-- 02. Assign a variable to a graph node property and map it to a GCL property.
Person = @node(Person:Doe/John)
{
    NickName = @value($nickName)
}

-- ==================================================================================================================
-- 03. Assign a variable to a graph node property and map it to a GCL property.
-- The type of the GCL property will be used to verify and process the variable.
Person = @node(Person:Doe/John)
{
    string NickName = @value($nickName)
    datetime Birthday = .BirtDate = @value(@birthday)
}

