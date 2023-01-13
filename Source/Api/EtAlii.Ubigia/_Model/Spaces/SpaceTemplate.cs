// Copyright (c) Peter Vrenken. All rights reserved. See the license on https://github.com/vrenken/EtAlii.Ubigia

namespace EtAlii.Ubigia;

public sealed class SpaceTemplate
{
    /// <summary>
    /// A list of all space templates available.
    /// </summary>
    public static SpaceTemplate[] All { get; }

    /// <summary>
    /// Returns the space template with which a data space needs to be instantiated.
    /// </summary>
    public static SpaceTemplate Data { get; }

    /// <summary>
    /// Returns the space template with which a system space needs to be instantiated.
    /// </summary>
    public static SpaceTemplate System { get; }

    /// <summary>
    /// Returns the space template with which a configuration space needs to be instantiated.
    /// </summary>
    public static SpaceTemplate Configuration { get; }

    /// <summary>
    /// Returns the space template with which a metrics space needs to be instantiated.
    /// </summary>
    public static SpaceTemplate Metrics { get; }

    /// <summary>
    /// The name of the space template.
    /// </summary>
    public string Name { get; }

    public string[] RequiredRoles { get; }

    /// <summary>
    /// The roots to create in the space.
    /// </summary>
    public RootTemplate[] RootsToCreate { get; }

    /// <summary>
    /// The script to execute in order to create the space.
    /// </summary>
    public string[] SetupScript { get; }

    static SpaceTemplate()
    {
        Data = new SpaceTemplate
        (
            name: SpaceName.Data,
            requiredRoles: new [] { Role.User },
            rootsToCreate: new[]
            {
                new RootTemplate("Tail", RootType.Tail),
                new RootTemplate("Hierarchy", RootType.Text),
                new RootTemplate("Sequences", RootType.Text),
                new RootTemplate("Data", RootType.Text),
                new RootTemplate("Tag", RootType.Text),
                new RootTemplate("Time", RootType.Time),
                new RootTemplate("Communication", RootType.Text),
                new RootTemplate("Person", RootType.Person),
                new RootTemplate("Location", RootType.Location),
                new RootTemplate("Subscription", RootType.Text),
                new RootTemplate("Media", RootType.Media),
                new RootTemplate("Head", RootType.Head),
            },
            setupScript: new[] { "" }
        );
        System = new SpaceTemplate
        (
            name: SpaceName.System,
            requiredRoles: new [] { Role.System, },
            rootsToCreate: new[]
            {
                new RootTemplate("Tail", RootType.Tail),
                new RootTemplate("Providers", RootType.Provider),
                new RootTemplate("Users", RootType.Person),
                new RootTemplate("Spaces", RootType.Text),
                new RootTemplate("Diagnostics", RootType.Text),
                new RootTemplate("Time", RootType.Time),
                new RootTemplate("Head", RootType.Head),
            },
            setupScript: new [] { "" }
        );
        Configuration = new SpaceTemplate
        (
            name: SpaceName.Configuration,
            requiredRoles: new [] { Role.User },
            rootsToCreate: new[]
            {
                new RootTemplate("Tail", RootType.Tail),
                new RootTemplate("Providers", RootType.Provider),
                new RootTemplate("Time", RootType.Time),
                new RootTemplate("Data", RootType.Text),
                new RootTemplate("Head", RootType.Head),
            },
            setupScript: new [] { "" }
        );
        Metrics = new SpaceTemplate
        (
            name: SpaceName.Metrics,
            requiredRoles: new [] {Role.User },
            rootsToCreate: new[]
            {
                new RootTemplate("Tail", RootType.Tail),
                new RootTemplate("Providers", RootType.Provider),
                new RootTemplate("Users", RootType.Person),
                new RootTemplate("Spaces", RootType.Text),
                new RootTemplate("Time", RootType.Time),
                new RootTemplate("Head", RootType.Head),
            },
            setupScript: new[] { "" }
        );
        All = new[]
        {
            Data,
            System,
            Configuration,
            Metrics
        };
    }

    private SpaceTemplate(
        string name,
        string[] requiredRoles,
        RootTemplate[] rootsToCreate,
        string[] setupScript)
    {
        Name = name;
        RequiredRoles = requiredRoles;
        RootsToCreate = rootsToCreate;
        SetupScript = setupScript;
    }
}
