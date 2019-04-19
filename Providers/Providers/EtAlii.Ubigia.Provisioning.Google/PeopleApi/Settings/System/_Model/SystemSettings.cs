namespace EtAlii.Ubigia.Provisioning.Google.PeopleApi
{
    public class SystemSettings
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string AuthenticationUrl { get; set; }
        public string TokenUrl { get; set; }
        public string AuthenticationProviderx509CertificateUrl { get; set; }
        public string ClientSecret { get; set; }
        public string RedirectUrl { get; set; }
    }
}


//[
//  "web": {
//    "client_id": "654017958519-l27t9bn8ec1p3o7kc4k97dnm9ik3iibn.apps.googleusercontent.com",
//    "project_id": "etalii-ubigia",
//    "auth_uri": "https://accounts.google.com/o/oauth2/auth",
//    "token_uri": "https://accounts.google.com/o/oauth2/token",
//    "auth_provider_x509_cert_url": "https://www.googleapis.com/oauth2/v1/certs",
//    "client_secret": "jgvSuDubPB6ulAi2bSCqEkat",
//    "redirect_uris": [ "http://localhost:64000/settings/google/" ],
//    "javascript_origins": [ "http://localhost:64000" ]
//  }
//}