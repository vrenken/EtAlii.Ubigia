﻿$nickname <= { NickName: "Pete" }
/Person/Vrenken/Peter <= $nickname
/Person/Vrenken/*

<= /Person/*
<= $v2 <= /Person/*

<= Count() <= /Person/*/*/Emails/.IsPrimary=true

#$var1 <= /Time/2015/
#$var2 <= /Time/2015/01/
$var3 <= "Test"
$var4 <= /Person/Vr*/P*

#<= $var1
#<= $var2
#<= $var3
#<= /Person/*/.Weight<76.0
