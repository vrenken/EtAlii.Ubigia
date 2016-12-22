namespace EtAlii.Servus.Infrastructure.WebApi.Portal.Admin
{
    using System.Collections.Generic;
    using System.Dynamic;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using EtAlii.Servus.Api.Transport;
    using Newtonsoft.Json;

    public class GoogleIdentityProvider : IGoogleIdentityProvider
    {
        private readonly HttpClient _httpClient;
        private readonly ISerializer _serializer;
        private readonly IGoogleNameConverter _googleNameConverter;
        private readonly IGoogleMailAddressConverter _googleMailAddressConverter;

        public GoogleIdentityProvider(
            ISerializer serializer, 
            IGoogleNameConverter googleNameConverter, 
            IGoogleMailAddressConverter googleMailAddressConverter)
        {
            _httpClient = new HttpClient();
            _serializer = serializer;
            _googleNameConverter = googleNameConverter;
            _googleMailAddressConverter = googleMailAddressConverter;
        }

        public GoogleIdentity GetGoogleIdentity(GoogleAuthenticationToken googleAuthenticationToken)
        {
            GoogleIdentity googleIdentity = null;

            var task = Task.Run(async () =>
            {
                var response = await _httpClient.GetAsync($"https://people.googleapis.com/v1/people/me?access_token={googleAuthenticationToken.access_token}");
                response.EnsureSuccessStatusCode();

                var result = await response.Content.ReadAsStringAsync();
                using (var textReader = new StringReader(result))
                {
                    using (var jsonReader = new JsonTextReader(textReader))
                    {
                        dynamic profile = _serializer.Deserialize<ExpandoObject>(jsonReader);
                        var id = profile.metadata.sources[0].id;
                        GoogleName[] googleNames = GetNames(profile);

                        var primaryName = googleNames.Single(n => n.IsPrimary);

                        GoogleMailAddress[] googleMailAddresses = GetMailAdresses(profile);
                        var primaryMailAddress = googleMailAddresses.Single(n => n.IsPrimary);

                        googleIdentity = new GoogleIdentity
                        {
                            Id = id,
                            Email = primaryMailAddress.Address,
                            DisplayName = primaryName.DisplayName,
                            DisplayNameLastFirst = primaryName.DisplayNameLastFirst,
                            FamilyName = primaryName.FamilyName,
                            GivenName = primaryName.GivenName,
                        };
                    }
                }
            });
            task.Wait();

            return googleIdentity;
        }

        private GoogleMailAddress[] GetMailAdresses(dynamic profile)
        {
            var googleAddresses = new List<GoogleMailAddress>();
            foreach (dynamic address in profile.emailAddresses)
            {
                var googleAddress = _googleMailAddressConverter.Convert(address);
                googleAddresses.Add(googleAddress);
            }
            return googleAddresses.ToArray();
        }

        private GoogleName[] GetNames(dynamic profile)
        {
            var googleNames = new List<GoogleName>();
            foreach (dynamic name in profile.names)
            {
                var googleName = _googleNameConverter.Convert(name);
                googleNames.Add(googleName);
            }
            return googleNames.ToArray();
        }
    }
}


//{
//  "resourceName": "people/106406075299309509716",
//  "etag": "4LXAsgqCGxc=",
//  "metadata": {
//    "sources": [
//      {
//        "type": "PROFILE",
//        "id": "106406075299309509716"
//      }
//    ],
//    "objectType": "PERSON"
//  },
//  "locales": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "ACCOUNT",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "en"
//    }
//  ],
//  "names": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "displayName": "Peter Vrenken",
//      "familyName": "Vrenken",
//      "givenName": "Peter",
//      "displayNameLastFirst": "Vrenken, Peter"
//    }
//  ],
//  "nicknames": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "Peter"
//    }
//  ],
//  "coverPhotos": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "url": "https://lh3.googleusercontent.com/-XviJW_7A56Q/T9kKKA9_iCI/AAAAAAAAAGI/DZCUNY97J0ceeAdiNdD0SDCDmtC_4GJ1g/s1000-fcrop64=1,00000000757f21c1/Green%2BGrass.jpg"
//    }
//  ],
//  "photos": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "url": "https://lh5.googleusercontent.com/-MSKMkUzvx0A/AAAAAAAAAAI/AAAAAAAASHw/Z_ygT5B9yek/photo.jpg"
//    }
//  ],
//  "genders": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "male",
//      "formattedValue": "Male"
//    }
//  ],
//  "residences": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "Enschede, Netherlands",
//      "current": true
//    }
//  ],
//  "emailAddresses": [
//    {
//      "metadata": {
//        "primary": true,
//        "verified": true,
//        "source": {
//          "type": "ACCOUNT",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "vrenken@gmail.com"
//    },
//    {
//      "metadata": {
//        "verified": true,
//        "source": {
//          "type": "ACCOUNT",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "gmail@petervrenken.nl"
//    }
//  ],
//  "urls": [
//    {
//      "metadata": {
//        "primary": true,
//        "verified": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "https://plus.google.com/+PeterVrenken",
//      "type": "profile",
//      "formattedType": "Profile"
//    },
//    {
//      "metadata": {
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "value": "http://petervrenken.wordpress.com",
//      "type": "My life...",
//      "formattedType": "My life..."
//    }
//  ],
//  "organizations": [
//    {
//      "metadata": {
//        "primary": true,
//        "source": {
//          "type": "PROFILE",
//          "id": "106406075299309509716"
//        }
//      },
//      "type": "work",
//      "formattedType": "Work",
//      "endDate": {
//        "year": 2011,
//        "month": 1,
//        "day": 1
//      },
//      "current": true,
//      "title": "Mobile Software Engineer"
//    }
//  ]
//}
