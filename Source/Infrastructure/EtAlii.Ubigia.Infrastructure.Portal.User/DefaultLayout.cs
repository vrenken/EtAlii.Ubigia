namespace EtAlii.Ubigia.Infrastructure.Portal.User;

using Blazorise;

public static class DefaultLayout
{


    public static IFluentSpacingOnBreakpointWithSideAndSize Margin => new FluentMargin().Is1;


    public static IFluentColumnWithSize ItemColumnWidth => new FluentColumn().IsFull.OnMobile.IsHalf.OnTablet;

    public static IFluentColumnWithSize ItemDashboardColumnWidth => new FluentColumn().IsFull.OnMobile.IsHalf.OnTablet.Is5.OnDesktop;
    public static IFluentColumnWithSize ItemOneOffColumnWidth => new FluentColumn().IsFull.OnMobile.Is4.OnTablet.Is3.OnDesktop;
}
