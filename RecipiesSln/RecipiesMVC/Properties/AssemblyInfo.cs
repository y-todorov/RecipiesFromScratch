﻿using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("RecipiesMVC")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("RecipiesMVC")]
[assembly: AssemblyCopyright("Copyright ©  2014")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("43fb96fa-764b-4a30-82b2-e582b7c9bc25")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers
// by using the '*' as shown below:
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]

// apply the aspect on every type that is the namespace RecipiesMVC.*
//[assembly: RecipiesPlatform.PostSharp.StopWatchPostSharp(
//                       AttributeTargetTypes = "RecipiesMVC.*")] // THIS IS FUCKING SLOWLY, at least 100 times slower !!!

//[assembly: RecipiesPlatform.PostSharp.StopWatchPostSharp(
//                       AttributeTargetTypes = "RecipiesMVC.Controllers.*")]

