## External projects

This folder contains various utility projects that will eventually be converted over to NuGet packages when they stabilise (it is harder to work with external NuGet dependencies while they are unstable as we need to manage versioning, backward-compatibility, etc. Once they stabilise and they don't change very often, it makes sense to turn these projects into NuGet packages so that they can be shared between different microservices, residing in different git repositories.)

For we have:
- Common.Versioning