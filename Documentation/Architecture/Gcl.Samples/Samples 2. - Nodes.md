﻿-- ==================================================================================================================
-- 01. Traverse to a single node and returns it as a named item.
-- Throws an error if multiple nodes are found.
Person = @node(Person:Doe/John)
{
    FirstName,
    LastName,
    NickName
}

-- ==================================================================================================================
-- 02. Traverse to multiple nodes and returns them as an array.
-- Does not throw an error when no node or only one node is found.
Person = @nodes(Person:Doe/)
{
    FirstName,
    LastName,
    NickName
}

-- ==================================================================================================================
-- 03. Returns both an item and an array in one single response.
Data
{
    Person = @node(Person:Doe/John)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    },
    Location = @nodes(location:DE/Berlin//)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
}

-- ==================================================================================================================
-- 04.a Returns two different arrays in one single response.
Data
{
    Person = @nodes(Person:Doe/*)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    },
    Location = @nodes(location:DE/Berlin//)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
}

-- ==================================================================================================================
-- 04.b Returns two different arrays in one single response.
Data
{
    Person = @nodes(Person:Doe/*)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
    Location = @nodes(location:DE/Berlin//)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName)
        }
    }
}

-- ==================================================================================================================
-- 06.a Nesting is always possible.
DataOuter
{
    DataInner
    {
        Person = @node(Person:Doe/John)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    }
}

-- ==================================================================================================================
-- 06.b Nesting is always possible.
Data
{
    Person = @node(Person:Doe/John)
    {
        Name
        {
            First = @node(),
            Last = @node(\LastName),
            NickName
        }
    }
}

-- ==================================================================================================================
-- 06.c Nest traversal queries to explore additional context.
Data
{
    Person = @node(Person:Doe/John)
    {
        FirstName = @node(),
        LastName = @node(\#FamilyName),
        NickName,
        Friends = @nodes(/Friends/)
        {
            FirstName = @node(),
            LastName = @node(\#FamilyName),
            NickName
        }
    }
}