namespace EtAlii.Ubigia.Provisioning.Mail
{
    using ActiveUp.Net.Mail;
    using EtAlii.xTechnology.Logging;
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using EtAlii.Ubigia.Api.Functional;

    public class MailImporter : IMailImporter
    {
        private readonly ILogger _logger;
        private readonly IDataContext _context;

        private Imap4Client _imap;
        private readonly string username = "vrenken.test@gmail.com";
        private readonly string password = "@2drinkglas";
        private readonly string server = "imap.gmail.com";
        private int _lastMessageCount;
        private Mailbox _inbox;
        private Task _task;

        public MailImporter(
            ILogger logger,
            IDataContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void Start()
        {
            try
            {
                _logger.Info("Starting mail provider");

                Stop();
                Setup();

                _logger.Info("Started mail provider");
            }
            catch (Exception e)
            {
                _logger.Critical("Unable to start mail provider", e);
            }
        }

        public void Stop()
        {
            try
            {
                _logger.Info("Stopping mail provider");

                Shutdown();

                _logger.Info("Stopped mail provider");
            }
            catch (Exception e)
            {
                _logger.Critical("Unable to stop mail provider", e);
            }
        }

        private void Shutdown()
        {
            if (_imap != null && _imap.IsConnected)
            {
                _inbox.Unsubscribe();
                _imap.NewMessageReceived -= NewMessageReceived;
                _imap.StopIdle();
                _imap.Disconnect();
                _imap = null;
            }
        }

        private void Setup()
        {
            _imap = new Imap4Client();
            _imap.ConnectSsl(server, 993);
            _imap.Login(username, password);
            _imap.NewMessageReceived += NewMessageReceived;

            _inbox = _imap.SelectMailbox("INBOX");
            _lastMessageCount = _inbox.MessageCount;
            _inbox.Subscribe();

            _task = Task.Run(() => _imap.StartIdle());
        }
 
        public void NewMessageReceived(object source, NewMessageReceivedEventArgs e)
        {
            _logger.Info("Adding mail to space");

            try
            {
                //_imap.StopIdle();

                _inbox = _imap.ExamineMailbox("INBOX");

                var newMessageCount = _inbox.MessageCount;

                for(int i = _lastMessageCount; i < newMessageCount; i++)
                {
                    Message message = _inbox.Fetch.MessageObject(i);

                    var task = Task.Run(async () =>
                    {
                        var safeTitle = message.Subject.Select(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray();
                        
                        var lastSequence = await _context.Scripts.Process("/Communications += \"{0}\"", safeTitle);
                        var result = await lastSequence.Output.ToArray();

                        var scope = new ScriptScope();
                        scope.Variables.Add("content", new ScopeVariable(message.BodyText.Text, "Mail"));
                        
                        lastSequence = await _context.Scripts.Process("/Communications/\"{0}\" <= $content", safeTitle);
                        result = await lastSequence.Output.ToArray();

                    });
                    task.Wait();
                }

                //_imap.StartIdle();
            }
            catch (Exception exception)
            {
                _logger.Critical("Unable to add mail to space", exception);
            }
        }
    }
}
