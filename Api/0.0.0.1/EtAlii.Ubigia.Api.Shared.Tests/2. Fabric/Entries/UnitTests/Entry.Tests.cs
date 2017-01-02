﻿namespace EtAlii.Ubigia.Api.Fabric.Tests
{
    using System;
    using EtAlii.Ubigia.Api.Tests;
    using Xunit;

    
    public class Entry_Tests
    {
        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_CreateRoot_With_Storage_Account_Space()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            Assert.Equal(storage, entry.Id.Storage);//, "The entry.Id.Storage property is incorrect");
            Assert.Equal(account, entry.Id.Account);//, "The entry.Id.Account property is incorrect");
            Assert.Equal(space, entry.Id.Space);//, "The entry.Id.Space property is incorrect");
            Assert.Equal(UInt64.MinValue, entry.Id.Period);//, "The entry.Id.Moment property is incorrect");
            Assert.Equal(UInt64.MinValue, entry.Id.Moment);//, "The entry.Id.Period property is incorrect");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Create_With_Previous()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var previousId = Identifier.NewIdentifier(storage, account, space);
            var previous = Relation.NewRelation(previousId);
            var identifier = Identifier.NewIdentifier(previousId, 0, 0, 1);
            var entry = Entry.NewEntry(identifier, previous);

            Assert.Equal(storage, entry.Id.Storage);//, "The entry.Id.Storage property is incorrect");
            Assert.Equal(account, entry.Id.Account);//, "The entry.Id.Account property is incorrect");
            Assert.Equal(space, entry.Id.Space);//, "The entry.Id.Space property is incorrect");
            Assert.Equal(UInt64.MinValue, entry.Id.Period);//, "The entry.Id.Moment property is incorrect");
            Assert.NotEqual(UInt64.MinValue, entry.Id.Moment);//, "The entry.Id.Period property is incorrect");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Based_On_Storage_Account_Space_Are_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space);
            var second = Entry.NewEntry(storage, account, space);

            Assert.Equal(first, second);//, "The two identical entries do not match");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space1);
            var second = Entry.NewEntry(storage, account, space2);

            // Act.
            var equals = first == second;

            // Assert.
            Assert.False(equals, "Two different entries do match");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator_With_Left_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            // Act.
            var equals = null == entry;

            // Assert.
            Assert.False(equals, "A entry matches with null");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator_With_Right_Null()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            // Act.
            var equals = entry == null;

            // Assert.
            Assert.False(equals, "A entry matches with null");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Compare_Using_Equals_Operator_With_Null()
        {
            // Arrange.
            var left = (Entry)null;
            var right = (Entry)null;

            // Act.
            var equals = left == right;

            // Assert.
            Assert.True(equals, "Two null entry should match");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Compare_With_Object()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space1);
            var second = Entry.NewEntry(storage, account, space2);

            // Act.
            var equals = first.Equals((object)second);

            // Assert.
            Assert.False(equals, "The two identical entries don't match");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Compare_With_Self()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space1 = Guid.NewGuid();
            var space2 = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space1);

            // Act.
            var equals = entry.Equals((object)entry);

            // Assert.
            Assert.True(equals, "The same entry should match");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Get_Hash()
        {
            // Arrange.
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var entry = Entry.NewEntry(storage, account, space);

            // Act.
            var hash = entry.GetHashCode();

            // Assert.
            Assert.NotNull(hash);//, "The entry has no hash");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Based_On_Storage_Account_Space_Compare_As_Equal()
        {
            var storage = Guid.NewGuid();
            var account = Guid.NewGuid();
            var space = Guid.NewGuid();
            var first = Entry.NewEntry(storage, account, space);
            var second = Entry.NewEntry(storage, account, space);

            Assert.True(first == second, "The two identical entries do not match");
        }

        [Fact, Trait("Category", TestAssembly.Category)]
        public void Entry_Based_On_Storage_Account_Space_Do_Not_Compare_As_Equal()
        {
            var firstStorage = Guid.NewGuid();
            var secondStorage = Guid.NewGuid();
            var firstAccount = Guid.NewGuid();
            var secondAccount = Guid.NewGuid();
            var firstSpace = Guid.NewGuid();
            var secondSpace = Guid.NewGuid();

            var first = Entry.NewEntry(firstStorage, firstAccount, firstSpace);
            var second = Entry.NewEntry(secondStorage, firstAccount, firstSpace);
            Assert.False(first == second, "The two identical entries do not match");
            Assert.True(first != second, "The two identical entries do not match");

            first = Entry.NewEntry(firstStorage, firstAccount, firstSpace);
            second = Entry.NewEntry(firstStorage, secondAccount, firstSpace);
            Assert.False(first == second, "The two identical entries do not match");
            Assert.True(first != second, "The two identical entries do not match");

            first = Entry.NewEntry(firstStorage, firstAccount, firstSpace);
            second = Entry.NewEntry(firstStorage, firstAccount, secondSpace);
            Assert.False(first == second, "The two identical entries do not match");
            Assert.True(first != second, "The two identical entries do not match");
        }
    }
}
