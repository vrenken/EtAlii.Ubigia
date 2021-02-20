-- ==================================================================================================================
-- 01. Sets the value of an existing node.
-- Throws an error if multiple nodes are found.
Person = Person:Doe/John
{
    NickName = "Johnny"
}

-- ==================================================================================================================
-- 02. Clear a property on an existing node.
-- Throws an error if multiple nodes are found.
Person = Person:Doe/John
{
   FirstName = . <=
}

-- ==================================================================================================================
-- 03. Changes the name of an existing node.
-- Throws an error if multiple nodes are found.
Person = Person:Doe/Johhn
{
    FirstName = . <= "John"
}


-- ==================================================================================================================
-- 04. Add a new node and give it some properties.
 Person @node-add(Person:Doe, Jane)
 {
     NickName = . <= "Jahney"
 }

-- ==================================================================================================================
-- 05.a Add a child node and return everything.
 Person = Person:Doe/John
 {
     FirstName = @,
     LastName = \#FamilyName,
     NickName,
     Friend = @nodes-link(/Friends, Person:Stark/Tony, /Friends)
     {
        FirstName = @,
        LastName = \#FamilyName,
        NickName
     }
 }

-- ==================================================================================================================
-- 05.b Add a child node and return everything.
 Data
 {
     Person = Person:Doe/John
     {
         FirstName = @,
         LastName = \#FamilyName,
         NickName,
         Friend @nodes-link(/Friends, Person:Banner/Peter, /Friends)
         {
            FirstName = @,
            LastName = \#FamilyName,
            NickName
         }
     }
 }

-- ==================================================================================================================
-- 06 Link two nodes and return everything.
 Data
 {
     Person = Person:Doe/John
     {
         FirstName = @,
         LastName = \#FamilyName,
         NickName,
         Friend = @nodes-link(/Friends, Person:Banner/Peter, /Friends)
         {
            FirstName = @,
            LastName = \#FamilyName,
            NickName
         }
     },
     Person = Person:Banner/Peter
     {
         FirstName = @,
         LastName = \#FamilyName,
         NickName,
         Friend[] = /Friends
         {
            FirstName = @,
            LastName = node(\#FamilyName,
            NickName
         }
     }
 }

