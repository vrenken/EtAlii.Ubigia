namespace EtAlii.Ubigia.Tests;

using Xunit;

public class AccountTemplateTests
{
    [Fact]
    public void AccountTemplate_Get_All()
    {
        // Arrange.

        // Act.
        var all = AccountTemplate.All;

        // Assert.
        Assert.NotEmpty(all);
    }
    [Fact]
    public void AccountTemplate_Administrator()
    {
        // Arrange.

        // Act.
        var administrator = AccountTemplate.Administrator;

        // Assert.
        Assert.Equal(AccountName.Administrator, administrator.Name);
        Assert.Contains(Role.Admin, administrator.RolesToAssign);
        Assert.Contains(Role.User, administrator.RolesToAssign);
        Assert.Contains(SpaceTemplate.Configuration, administrator.SpacesToCreate);
        Assert.Contains(SpaceTemplate.Data, administrator.SpacesToCreate);
    }

}
