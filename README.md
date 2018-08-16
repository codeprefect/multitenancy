# MultiTenant Architecture in .NET Core (*Database per Tenant*)

This is a proof of concept for multi-tenant application architrecture in ASP.NET Core.
The following features has been completed

* Database instance per tenant
* Entity framework code-first migration (must set-up default tenant on development)
* Automatic migration on start-up
* Custom themes
* Missing Tenant Handler (redirects to default tenant)

## How to install

```[bash]
git clone git@github.com:codeprefect/multitenancy.git multitenancy
cd multitenancy

# cd to src/multitenant or use VS Code tasks to restore, build and watch
```

With &#x1F493; from [@codeprefect](https://gihub.com/codeprefect)
