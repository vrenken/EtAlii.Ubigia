namespace EtAlii.xTechnology.Hosting
{
    internal interface IHostCommandsConverter
    {
        MenuItem[] ToMenuItems(ICommand[] commands);
    }
}