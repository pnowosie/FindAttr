# FindAttr
List .NET assembly attributes and its values 

## this is really silly project
Once upon a time I thought it is cool idea to create a lot of custom attributes and decorate assembly with them.

Then I realized that's hard to get value of some attribute out of given assembly file. Maybe I haven't tried hard 
enogh but even [dotPeek](https://www.jetbrains.com/decompiler/) can't do this.

A then I wrote this tool

## Usage

Listing attributes
```
λ .\FindAttr.exe nunit.framework.dll

Assembly attributes for 'nunit.framework'...
System.Reflection.AssemblyTrademarkAttribute
System.Reflection.AssemblyInformationalVersionAttribute
System.Reflection.AssemblyConfigurationAttribute
System.Security.AllowPartiallyTrustedCallersAttribute
System.Runtime.CompilerServices.CompilationRelaxationsAttribute
System.Runtime.CompilerServices.RuntimeCompatibilityAttribute
System.Reflection.AssemblyKeyFileAttribute
System.Reflection.AssemblyKeyNameAttribute
System.Reflection.AssemblyCopyrightAttribute
System.Reflection.AssemblyDelaySignAttribute
System.CLSCompliantAttribute
System.Reflection.AssemblyCompanyAttribute
System.Reflection.AssemblyProductAttribute
```

Get value of attribute
```
λ .\FindAttr.exe nunit.framework.dll trade

Assembly attributes for 'nunit.framework'...
Listing properties of System.Reflection.AssemblyTrademarkAttribute
      Trademark : NUnit is a trademark of NUnit.org
         TypeId : System.Reflection.AssemblyTrademarkAttribute
```

Get properties of assembly

```
λ .\FindAttr.exe nunit.framework.dll /info

Assembly attributes for 'nunit.framework'...
Listing properties of nunit.framework, Version=2.6.2.12296, Culture=neutral, PublicKeyToken=96d09a1eb7f44a77
       CodeBase : file:///C:/Projects/Roche/UIA/Roche.UIA/packages/NUnit.2.6.2/lib/nunit.framework.dll
       FullName : nunit.framework, Version=2.6.2.12296, Culture=neutral, PublicKeyToken=96d09a1eb7f44a77
     EntryPoint :
   DefinedTypes : System.RuntimeType[]
       Evidence : System.Security.Policy.Evidence
  PermissionSet : <PermissionSet class="System.Security.PermissionSet"
version="1"
Unrestricted="true"/>
 ...
```

Thanks for reading!