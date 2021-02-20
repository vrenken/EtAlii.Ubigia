﻿-- ==================================================================================================================
-- 01.b Add an named Track entry to a Person given a specific time and location.
Person = @nodes(Person:Doe/John)
{
    Track = @node-add(/Track, "Love Parade")
    {
        Time = @node-link(/Time, Time:"2006-04-03 11:23", /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @node()
        }
    }
}

-- ==================================================================================================================
-- 01.c Add an named Track entry to a Person given a specific time and location.
Person = @nodes(Person:Doe/John)
{
    Track = @node-add(/Track, "Love Parade")
    {
        Time = @node-link(/Time, Time:"2006-04-03 11:23", /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @node()
        }
    }
}

-- ==================================================================================================================
-- 01.d Add multiple named Track entry to a Person given specific times and locations.
Person = @nodes(Person:Doe/John)
{
    Track = @node-add(/Track, "Love Parade 2006")
    {
        Time = @node-link(/Time, Time:"2006-04-03 11:23", /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @node()
        }
    }
    Track = @node-add(/Track, "Love Parade 2000")
    {
        Time = @node-link(/Time, Time:'2000-05-02 23:07', /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @node()
        }
    }
    Track = @node-add(/Track, "Love Parade 2007")
    {
        Time = @node-link(/Time, Time:"2007-06-13 09:12", /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Essen, /Visit)
        {
            Location = @node()
        }
    }
}

-- ==================================================================================================================
-- 01.e Assign Values.
Person = @nodes(Person:Doe/John)
{
    Lastname = @node-set(\\#FamilyName, 'Doesies')
}

-- ==================================================================================================================
-- 01.f Clear Values.
Person = @nodes(Person:Doe/John)
{
    Lastname = @node-clear(/Nickname)
}