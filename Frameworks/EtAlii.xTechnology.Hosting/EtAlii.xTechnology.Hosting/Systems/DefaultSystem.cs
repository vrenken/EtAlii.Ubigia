namespace EtAlii.xTechnology.Hosting
{
    public class DefaultSystem : SystemBase
    {
        private static int _defaultSystemCounter; 
        
        #pragma warning disable S2696 // Pretty sure this counter won't cause any weird threading issues.
        protected override Status CreateInitialStatus() => new Status($"System {++_defaultSystemCounter}");
        #pragma warning restore S2696
    }
}