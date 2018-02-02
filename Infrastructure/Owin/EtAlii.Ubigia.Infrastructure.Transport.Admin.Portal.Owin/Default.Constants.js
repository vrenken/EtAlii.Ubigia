app.constant('Event',
{
    'Click': 'Click',
    'RouteChangeSuccess': '$routeChangeSuccess',
    'RouteChangeStart': '$routeChangeStart'
});
app.constant('relativeUrl',
{
    'authenticate': '/authenticate',

    'MicrosoftGraphSettingsAdmin': '/admin/settings/microsoft/graph',
    'GooglePeopleApiSettingsAdmin': '/admin/settings/google/peopleapi',

    'accountsAdministration': '/admin/accounts',
    'accountAdministration': '/admin/account/edit',

    'storagesAdministration': '/admin/storage',
    'spacesAdministration': '/admin/space',

    'login': '/login',

    'entry': '/data/entry',
    'relatedEntries': '/data/relatedentries',
    'entries': '/data/entries',

    'accounts': '/data/account',
    'roots': '/data/root',
    'content': '/data/content',
    'properties': '/data/properties',
    'contentDefinition': '/data/contentdefinition',


    // TODO: Could have a better name?
    '/data': '/datastream',
    '/admin': '/adminstream'

});
