﻿-- ==================================================================================================================
-- 01. Query with nodes directive at entity level and normal property
Person = @node(person:Stark/Tony)
{
    nickname
}


-- ==================================================================================================================
-- 02. Query with nodes directive at entity path and relative property
Person = @node(person:Stark/Tony)
{
    lastname = @node(\\#FamilyName)
}