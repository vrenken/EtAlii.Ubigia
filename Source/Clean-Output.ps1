<# 
 https://sachabarbs.wordpress.com/2014/10/24/powershell-to-clean-visual-studio-binobj-folders/
#>
Get-ChildItem -inc bin,obj,TestResults -rec | Remove-Item -rec -force