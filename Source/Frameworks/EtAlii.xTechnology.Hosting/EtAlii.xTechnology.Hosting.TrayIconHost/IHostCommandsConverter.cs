namespace EtAlii.xTechnology.Hosting
{
    internal interface IHostCommandsConverter
    {
        MenuItemViewModel[] ToViewModels(ICommand[] commands);
    }
}