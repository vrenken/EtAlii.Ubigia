namespace EtAlii.Ubigia.Provisioning
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class PersonModelBuilder
    {
        private readonly List<LastName> _lastNames;
        private readonly List<Email> _emails;
        private readonly List<Phone> _phones;
        private readonly List<Photo> _photos;

        public PersonModelBuilder()
        {
            _lastNames = new List<LastName>();
            _emails = new List<Email>();
            _phones = new List<Phone>();
            _photos = new List<Photo>();
        }

        public void Clear()
        {
            _lastNames.Clear();
            _emails.Clear();
            _phones.Clear();
            _photos.Clear();
        }

        public void Add(string lastName, string firstName, string email, string phone)
        {
            lastName = CleanupLastName(lastName);
            firstName = CleanupFirstName(firstName);
            phone = CleanupPhone(phone);
            email = CleanupEmail(email);

            // #1 Find existing person (using lastname, firstname). 
            var namePersons = Find(lastName, firstName);

            // #2 Find existing person (using email). 
            var emailInstance =  _emails.SingleOrDefault(e => e.Address == email);

            // #3 Find existing person (using phone). 
            var phoneInstance = _phones.SingleOrDefault(p => p.Number == phone);

            // if #1 == 1: add
            if (namePersons.Count == 1)
            {
                var namePerson = namePersons.Single();
                AddPhoneIfMatch(emailInstance, phoneInstance, namePerson, phone);
                AddEmailIfMatch(emailInstance, phoneInstance, namePerson, email);
            }
            else if (namePersons.Count > 1)
            {
                if (phoneInstance != null)
                {
                    var matchByPhone = namePersons.SingleOrDefault(n => n.Phones.Contains(phoneInstance));
                    if (matchByPhone != null)
                    {
                        AddEmailIfMatch(emailInstance, phoneInstance, matchByPhone, email);
                    }
                }
                if (emailInstance != null)
                {
                    var matchByEmail = namePersons.SingleOrDefault(n => n.Emails.Contains(emailInstance));
                    if (matchByEmail != null)
                    {
                        AddPhoneIfMatch(emailInstance, phoneInstance, matchByEmail, phone);
                    }
                }
            }
            else
            {
                throw new NotSupportedException("We always expect at least one person");
            }

        }

        private void AddEmailIfMatch(Email email, Phone phone, Person person, string address)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }
            
            if(email != null)
            {
                if (person == phone?.Person && person.Emails.All(e => e.Address != email.Address))
                {
                    person.Emails.Add(email);
                }
            }
            else
            {
                email = new Email(address, person);
                _emails.Add(email);
                person.Emails.Add(email);
            }
        }

        private void AddPhoneIfMatch(Email email, Phone pone, Person person, string number)
        {
            if (person == null)
            {
                throw new ArgumentNullException(nameof(person));
            }

            if (pone != null)
            {
                if (person == email?.Person && person.Phones.All(p => p.Number != pone.Number))
                {
                    person.Phones.Add(pone);
                }
            }
            else
            {
                pone = new Phone(number, person);
                _phones.Add(pone);
                person.Phones.Add(pone);
            }
        }

        public void ToModel(out LastName[] lastNames, out Email[] emails, out Phone[] phones, out Photo[] photos)
        {
            lastNames = _lastNames.ToArray();
            emails = _emails.ToArray();
            phones = _phones.ToArray();
            photos = _photos.ToArray();

        }

        private List<Person> Find(string lastName, string firstName)
        {
            var ln = _lastNames.SingleOrDefault(_ => _.Name == lastName);
            if (ln == null)
            {
                ln = new LastName(lastName);
                _lastNames.Add(ln);
            }

            var fn = ln.FirstNames.SingleOrDefault(_ => _.Name == firstName);
            if (fn == null)
            {
                fn = new FirstName(firstName);
                ln.FirstNames.Add(fn);

                var p = new Person(0);
                fn.Persons.Add(p);
            }
            return fn.Persons;

            //return _lastNames
            //    .SingleOrDefault(ln => ln.Name == lastName)?
            //    .FirstNames.SingleOrDefault(fn => fn.Name == firstName)
            //    ?.Persons
        }

        private string CleanupPhone(string phone)
        {
            phone = phone
                .Trim()
                .Replace(" ", "");

            if (phone[0] == '+')
            {
                phone = "00" + phone.Replace("+", "");
            }
            return new string(phone.Where(char.IsDigit).ToArray());
        }

        private string CleanupEmail(string mail)
        {
            return mail.Trim();
        }

        private string CleanupFirstName(string firstName)
        {
            return new string(firstName.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray()).Trim();
        }
        private string CleanupLastName(string lastName)
        {
            return new string(lastName.Where(c => char.IsLetterOrDigit(c) || c == ' ').ToArray()).Trim();
        }
    }
}