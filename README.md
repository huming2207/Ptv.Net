.NET Portable Class Library for Public Transport Victoria API (Ptv.dll)
=======================================================================

Status
-----

**Now I'm going to add version 2.2 support. It may takes one or two weeks to finish.** 
**Partially added v2.2 support, still have some left.**

Mono (Travis CI): [![Build Status](https://travis-ci.org/huming2207/Ptv.Net.svg?branch=master)](https://travis-ci.org/huming2207/Ptv.Net)

Microsoft offical toolchain (AppVeyor): [![Build status](https://ci.appveyor.com/api/projects/status/hee4cw8oxdjtgpql/branch/master?svg=true)](https://ci.appveyor.com/project/huming2207/ptv-net/branch/master)

Introduction
-----

Ptv.Net is a Portable Class Library which provides .NET-based wrapper around the Public Transport Victoria APIs that have been published at http://data.vic.gov.au.

**Originally written by former Readify staff. I've forked from them and add some supports for newest API v2.1.0 and v2.2.0.**

Usage
-----

Firstly, open **Ptv.sln** (NOT THE ONE CALLED **Ptv-CITest.sln**) to compile it, 
or it can also be downloaded though NuGet: https://www.nuget.org/packages/Ptv.Net

Then, in your code add the following:

```C#
var developerID = "12345";
var securityKey = "xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx";

var client = new TimetableClient(
    developerID,
    securityKey,
    (input, key) =>
    {
		// Unfortunately the APIs exposed to .NET PCLs does not include an implementation
		// of the HMACSHA1 algorithm which the PTV API requires to generate signatures, so
		// rather than take a dependency on another library, for now the API defines a
		// delegate (TimetableClientHasher) which takes the key, and a sequence of bytes
		// to be hashed which can then be passed into the underlying platforms APIs.
        var provider = new HMACSHA1(key);
        var hash = provider.ComputeHash(input);
        return hash;
    });

var results = await client.SearchAsync("South Melbourne");
```
