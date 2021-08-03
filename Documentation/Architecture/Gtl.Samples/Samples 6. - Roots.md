# GTL Roots

Proposal for root registrations & more.
These are already partially implemented in unit tests but definitely deserves further refinement.

Assign
```gtl
root:time <= EtAlii.Ubigia.Roots.Time
root:time <= Time
root:specialtime <= EtAlii.Ubigia.Roots.Time
root:specialtime <= Time
root:projects1 <= EtAlii.Ubigia.Roots.Object
root:projects2 <= Object
root:projects3 <= EtAlii.Ubigia.Roots.Object
root:projects4 <= Object
```

Unassign
```gtl
root:time <=
root:specialtime <=
root:projects <=
```

Proposal for root registration.

``root:[ROOT] <= [ROOT_TYPE]``

```gtl
root:time <= EtAlii.Api.Roots.Time
root:text <= EtAlii.Api.Roots.Text
root:location <= EtAlii.Api.Roots.Location
root:orders <= EtAlii.Api.Roots.Text
root:emailtext <= EtAlii.Api.Roots.Text
```

Proposal for root unregistration.
```gtl
root:time <=
```

Proposal for root relation (indexing) registration.

``root:[ROOT] += /[PATH].[PROPERTY]``

```
(NOT YET SUPPORTED)
root:text += /orders/Order.Id
root:time += /orders/Order.Created
root:time += /orders/Order.Added
root:location += /orders/Order.Location
root:time += /*.*
root:time += /Orders/Order.*
root:emailtext += /Documents/Email.Text
root:projects3 += /[WORD]/[NUMBER]
root:projects4 += /[WORD]/[NUMBER]
root:time += /[yyyy]/[MM]/[DD]/[HH]/[mm]/[ss]
root:time += [yyyy][MM][DD][HH][mm][ss]
root:time += [yyyy][MM][DD][HH][mm]
root:time += [yyyy][MM][DD][HH]
root:time += ["HH:mm DD-MM-yyyy"]
root:time += "now"
root:time += "NOW"
root:time += now
root:time += NOW
root:person += ["FIRSTNAME LASTNAME"]
root:person += /[LASTNAME]/[FIRSTNAME]  --Returns multiple records so translates to /LAST/FIRST/NUMBER.
root:person += /[LASTNAME]/[FIRSTNAME]
root:person += /[LASTNAME]/[FIRSTNAME]/
```

Root usage
```gtl
time:"23:54 12-08-2016"
time:201608122354
time:now

person:"Peter Banner"
person:Banner/Peter/1
person:Banner/Peter
```

Proposal for root relation (indexing) registration removal
```
(NOT YET SUPPORTED)
root:text-=/orders/Order.Id
```

Find orders using a root as an index.
```
(NOT YET SUPPORTED)
orders:*.Id=text:ABC164235
orders:*.Id=text:ABV*
orders:*.Id=orders:ABV*
orders:*.Created=time:2014
orders:*.Added=time:20140826
orders:*.Added=time:2014-08-26
orders:*.Added=time:2014/08/26
orders:*.Added=time:2014-08-26T11:12:23
orders:*.Added=time:2014/08/26/11/12/23
orders:*.Version=version:3.2.123.234
orders:*.Version=version:3/2/123/234
orders:*.Version=version:3.2.123.*
orders:*.Version=version:3/2/123/*

orders:*.Id=text:ABC164235&Added=2014-08-26/Location
orders:*.Added=2014-08-26|Removed>2014-08-26/Location
orders:*.Location <= $location
```

Find orders __without__ using a root as an index. Remark: this method will be slow.
```
(NOT YET SUPPORTED)
orders:*.Id=ABC164235
orders:*.Id="ABC164235"
orders:*.Id=ABC*
orders:*.Id="ABC*"
orders:*.Customer=NL/Enschede
orders:*.Created=2014
orders:*.Added=20140826
orders:*.Added=2014-08-26
orders:*.Added=2014/08/26
orders:*.Added=2014-08-26T11:12:23
orders:*.Added=2014/08/26/11/12/23
orders:*.Version=3.2.123.234
orders:*.Version=3/2/123/234
orders:*.Version=3.2.123.*
orders:*.Version=3/2/123/*
```

use a root for discovery
```gtl
time:2014/08/12/15/15
time:"2014-08-12T15:15"
time:2014/08/12/15/15/34
time:201408121515
time:20140812151534
time:20140812/151534
time:2014/08/12
time:20140812
location:Netherlands/Overijssel/Enschede
location:Germany/Berlin/Center/"Zoologischen Garten"
location:DE/Berlin/Center/"Zoologischen Garten"
location:NL/Enschede
location:NL/Enschede/Lavenhorsthoek/23
location:52.2167/6.9000
orders:ABC164235
```

Find orders based on the time root as an index.
```
(NOT YET SUPPORTED)
orders:*=time:2014-08-26T11:12:23
```

Possible roots
- root
- time
- location
- text
- version
- person
- device
