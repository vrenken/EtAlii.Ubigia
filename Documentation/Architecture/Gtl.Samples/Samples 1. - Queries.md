# GTL Queries

Free form path querying.

```gtl
/Documents/'Readme.txt'
/Documents/Files/
/Documents/Tests/'Readme.txt'
```

Rooted querying.
```gtl
Documents:'Readme.txt'
Documents:Files/
```

Path components
```gtl
/Documents/invitation                   -- Symbol
/Documents/$i1                          -- Variable
/Documents/*                            -- Wildcard
/Persons/*/John                         -- Wildcard
/&4BE52B49-60EF-41CB-9314-69CC1E1F05B8.9824BEB0-5144-440F-8545-E32E2046AA51.ECDD1E96-6A4B-43AC-A7EE-6B452661C031.93212.54534.9423   -- Identifier
```

Hierarchical queries
```gtl
/Documents/Files/'Image001.jpg'/	    -- Children
/Documents/Files/'Image001.jpg'\	    -- Parent(s) (Should at least return 'Files')
```

List based queries
```gtl
/Documents/Files/'Image001.jpg'<	    -- Previous
/Documents/Files/'Image001.jpg'>	    -- Next

-- (NOT YET SUPPORTED)
-- /Documents/Files/'Image001.jpg'<10	-- 10 previous
-- /Documents/Files/'Image001.jpg'>10	-- 10 next

/Documents/Files/'Image001.jpg'<<	    -- To the first previous (Start)
/Documents/Files/'Image001.jpg'>>	    -- To the last next (End)
```

Temporal queries
```gtl
/Documents/Files/'Image001.jpg'}	    -- Update
/Documents/Files/'Image001.jpg'{	    -- Downdate

/Documents/Files/'Image001.jpg'}*	    -- All updates
/Documents/Files/'Image001.jpg'{*	    -- All downdates

-- (NOT YET SUPPORTED)
-- /Documents/Files/'Image001.jpg'}10	-- 10 updates
-- /Documents/Files/'Image001.jpg'{10	-- 10 downdates

/Documents/Files/'Image001.jpg'{{	    -- To the oldest downdate (Original)
/Documents/Files/'mage001.jpg'}}	    -- To the newest update (Latest)
```

Experimental 2018-08-20 (for possible GraphQL merger)

```gtl
/Documents/Files/'Image001.jpg'/	    -- Children
/Documents/Files/'Image001.jpg'\	    -- Parent(s) (Should at least return 'Files')
/Documents/Files/'Image001.jpg'// 	    -- All children
/Documents/Files/'Image001.jpg'\\	    -- All parent(s) (Should at least return 'Files')
-- (NOT YET SUPPORTED)
-- /Documents/Files/'Image001.jpg'/3 	-- 3 levels of children
-- /Documents/Files/'Image001.jpg'\3 	-- 3 levels of parent(s) (Should at least return 'Files')
```

List based queries
```gtl
/Documents/Files/'Image001.jpg'<	    -- Previous
/Documents/Files/'Image001.jpg'>	    -- Next
/Documents/Files/'Image001.jpg'<<	    -- All previous
/Documents/Files/'Image001.jpg'>>	    -- All next
-- (NOT YET SUPPORTED)
-- /Documents/Files/'Image001.jpg'<<<	-- To the first previous (Start)
-- /Documents/Files/'Image001.jpg'>>>	-- To the last next (End)
-- /Documents/Files/'Image001.jpg'<10	-- 10 previous
-- /Documents/Files/'Image001.jpg'>10	-- 10 next
```

Temporal queries
```gtl
/Documents/Files/'Image001.jpg'}	    -- Update
/Documents/Files/'Image001.jpg'{	    -- Downdate
/Documents/Files/'Image001.jpg'}}	    -- All updates
/Documents/Files/'Image001.jpg'{{	    -- All downdates
-- (NOT YET SUPPORTED)
-- /Documents/Files/'Image001.jpg'}10	-- 10 updates
-- /Documents/Files/'Image001.jpg'{10	-- 10 downdates
-- /Documents/Files/'Image001.jpg'{{{	-- To the oldest downdate (Original)
-- /Documents/Files/'Image001.jpg'}}}	-- To the newest update (Latest)
```

