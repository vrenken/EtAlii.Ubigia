namespace EtAlii.Ubigia.Tests;

using System;
using System.IO;
using Xunit;

public class AccountSerializationTests
{
    [Fact]
    public void Account_Serialize_Deserialize()
    {
        // Arrange.
        var account = new Account
        {
            Id = Guid.NewGuid(),
            Created = new DateTime(2022, 10, 8, 13, 56, 2),
            Updated = new DateTime(2022, 10, 8, 14, 56, 2),
            Name = "John Doe",
            Password = "123",
            Roles = new[] { Role.User }
        };

        using var stream = new MemoryStream();
        using var writer = new BinaryWriter(stream);
        using var reader = new BinaryReader(stream);

        // Act.
        writer.Write(account);
        stream.Position = 0;
        var account2 = reader.Read<Account>();

        // Assert.
        Assert.NotNull(account2);
        Assert.Equal(account.Id, account2.Id);
        Assert.Equal(account.Created, account2.Created);
        Assert.Equal(account.Updated, account2.Updated);
        Assert.Equal(account.Name, account2.Name);
        Assert.Equal(account.Password, account2.Password);
        Assert.Equal(account.Roles[0], account2.Roles[0]);
    }
}
