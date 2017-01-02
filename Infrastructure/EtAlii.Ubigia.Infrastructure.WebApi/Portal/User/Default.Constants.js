app.constant('Event',
{
    'Click': 'Click',
    'RouteChangeSuccess': '$routeChangeSuccess',
    'RouteChangeStart': '$routeChangeStart'
});
app.constant('relativeUrl',
{
    'authenticate': '/authenticate',

    'microsoftGraphSettingsUser': '/settings/microsoft/graph',
    'googlePeopleApiSettingsUser': '/settings/google/peopleapi',

    'accountAdministration': '/account/edit',

    'spacesAdministration': '/space',

    'login': '/login',

    // TODO: Could have a better name?
    '/data': '/stream'

});
