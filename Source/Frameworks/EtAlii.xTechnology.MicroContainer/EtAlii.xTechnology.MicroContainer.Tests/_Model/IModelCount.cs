namespace EtAlii.xTechnology.MicroContainer.Tests
{
    public interface IModelCount
    {
        int FirstChildConstructorCount { get; set; }
        int FirstChildInitializeCount { get; set; }
        
        int SecondChildConstructorCount { get; set; }
        int SecondChildInitializeCount { get; set; }
        
        int ParentConstructorCount { get; set; }
        int ParentInitializeCount { get; set; }
    }
}