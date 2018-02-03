//namespace EtAlii.Ubigia.Infrastructure.Hosting.Owin
//{
//    using EtAlii.xTechnology.Hosting;

//    class OpenUserPortalCommand : HostCommandBase, IOpenUserPortalCommand
//    {
//        public string Name => "User/User portal";

//        private readonly IWebsiteBrowser _websiteBrowser;

//        public OpenUserPortalCommand(IHost host, IWebsiteBrowser websiteBrowser)
//	        : base(host)
//		{
//            _websiteBrowser = websiteBrowser;
//        }
//        public void Execute()
//        {
//            _websiteBrowser.BrowseTo("/");
//        }

//        protected override void OnHostStateChanged(State state)
//        {
//            CanExecute = state == State.Running;
//        }

//    }
//}