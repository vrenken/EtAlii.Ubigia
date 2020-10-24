﻿namespace EtAlii.Ubigia.Provisioning.Mail
{
    using System;
    using System.Linq;
    using System.Reactive.Linq;
    using System.Threading.Tasks;
    using ActiveUp.Net.Mail;
    using EtAlii.Ubigia.Api.Functional.Scripting;
    using Serilog;

    public class MailImporter : IMailImporter
    {
        private readonly ILogger _logger = Log.ForContext<IMailImporter>();
        
        private readonly IGraphSLScriptContext _scriptContext;

        private Imap4Client _imap;
        private readonly string username = "vrenken.test@gmail.com";
        private readonly string password = "@2drinkglas";
        private readonly string server = "imap.gmail.com";
        private int _lastMessageCount;
        private Mailbox _inbox;

        public MailImporter(IGraphSLScriptContext scriptContext)
        {
            _scriptContext = scriptContext;
        }

        public async Task Start()
        {
            try
            {
                _logger.Information("Starting mail provider");

                await Stop();
                Setup();

                _logger.Information("Started mail provider");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to start mail provider");
            }
        }

        public Task Stop()
        {
            try
            {
                _logger.Information("Stopping mail provider");

                Shutdown();

                _logger.Information("Stopped mail provider");
            }
            catch (Exception e)
            {
                _logger.Error(e, "Unable to stop mail provider");
            }
            return Task.CompletedTask;
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

            Task.Run(() => _imap.StartIdle());
        }
 
        public void NewMessageReceived(object source, NewMessageReceivedEventArgs e)
        {
            _logger.Information("Adding mail to space");

            try
            {
                //_imap.StopIdle()

                _inbox = _imap.ExamineMailbox("INBOX");

                var newMessageCount = _inbox.MessageCount;

                for(var i = _lastMessageCount; i < newMessageCount; i++)
                {
                    var message = _inbox.Fetch.MessageObject(i);

                    var task = Task.Run(async () =>
                    {
                        var safeTitle = message.Subject.Select(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c)).ToArray();
                        
                        var lastSequence = await _scriptContext.Process("/Communications += \"{0}\"", safeTitle);
                        await lastSequence.Output.ToArray();

                        var scope = new ScriptScope();
                        scope.Variables.Add("content", new ScopeVariable(message.BodyText.Text, "Mail"));
                        
                        lastSequence = await _scriptContext.Process("/Communications/\"{0}\" <= $content", safeTitle);
                        await lastSequence.Output.ToArray();

                    });
                    task.Wait();
                }

                //_imap.StartIdle()
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Unable to add mail to space");
            }
        }
    }
}