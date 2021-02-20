﻿-- ==================================================================================================================
-- 01.a Add an unnamed Track entry to a Person given a specific time and location.
Person = Person:Doe/John
{
    Track = @node-add(/Track, UNNAMED)
    {
        Time = @node-link(/Time, Time:'2006-04-03 11:23', /Event)
        {
            Time = @
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @
        }
    }
}

-- ==================================================================================================================
-- 01.b Add an named Track entry to a Person given a specific time and location.
Person = Person:Doe/John
{
    Track = @node-add(/Track, "Love Parade")
    {
        Time = @node-link(/Time, Time:'2006-04-03 11:23', /Event)
        {
            Time = @
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @
        }
    }
}

-- ==================================================================================================================
-- 01.b Add an named Track entry to a Person given a specific time and location.
Person = Person:Doe/John
{
    Track = @node-add(/Track, "Love Parade")
    {
        Time = @node-link(/Time, Time:'2006-04-03 11:23', /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @
        }
    }
}
-- ==================================================================================================================
-- 01.b Add an named Track entry to a Person given a specific time and location.
Person = Person:Doe/John
{
    Track = @node-add(/Track, "Love Parade")
    {
        Time = @node-link(/Time, Time:'2006-04-03 11:23', /Event)
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
-- 01.c Add multiple named Track entry to a Person given specific times and locations.
Person = @nodes(Person:Doe/John)
{
    Track = @node-add(/Track, "Love Parade 2006")
    {
        Time = @node-link(/Time, Time:'2006-04-03 11:23', /Event)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @node()
        }
    },
    Track = @node-add(/Track, "Love Parade 2000")
    {
        Time = @node-link(/Time, Time:'2000-05-02 11:23', /Visit)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Berlin, /Visit)
        {
            Location = @node()
        }
    },
    Track = @node-add(/Track, "Love Parade 2007")
    {
        Time = @node-link(/Time, Time:'2007-06-13 09:12', /Visit)
        {
            Time = @node()
        }
        Location = @node-link(/Location, Location:Europe/Germany/Essen, /Visit)
        {
            Location = @node()
        }
    }
}