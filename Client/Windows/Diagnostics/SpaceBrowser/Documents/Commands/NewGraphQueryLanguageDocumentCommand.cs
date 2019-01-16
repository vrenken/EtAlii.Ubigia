﻿namespace EtAlii.Ubigia.Windows.Diagnostics.SpaceBrowser
{
    using EtAlii.Ubigia.Api.Fabric;
    using EtAlii.Ubigia.Api.Functional;
    using EtAlii.Ubigia.Api.Logical;
    using EtAlii.Ubigia.Api.Transport;
    using EtAlii.xTechnology.Diagnostics;
    using EtAlii.xTechnology.Logging;

    public class NewGraphQueryLanguageDocumentCommand : NewDocumentCommandBase, INewGraphQueryLanguageDocumentCommand
    {
        public NewGraphQueryLanguageDocumentCommand(IDocumentContext documentContext, IGraphQueryLanguageDocumentFactory factory) 
            : base(documentContext)
        {
            DocumentFactory = factory;
            Header = "GraphQL";
            Icon = @"pack://siteoforigin:,,,/Images/File-Format-GraphQuery.png";
            TitleFormat = "GraphQL view {0}";
            InfoLine = "Create a document to invoke scripts on a space";
            InfoTip1 = "Allows execution queries that adhere to the GraphQL definition";
            InfoTip2 = "Useful for information discovery";
        }
    }
}
