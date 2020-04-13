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

# cd to src/multitenant or use VS Code tasks to restore and build
```

## Dependencies

* ### Default Tenant

Create a `tenancy.json` file in `src/Multitenant` based off `sample-tenancy.json`.
The default tenant must be provided both in `src/Multitenant/appsettings.json` and `src/Multitenant/Config/tenancy.json`, In the `appsettings.json` case

```[json]
"DefaultDatabase": {
      "ConnectionString": "DataSource=default.db",
      "Provider": "SQLITE"
  },
"DefaultTenantUrl": "https://default.tenancy.localhost",
```

`ConnectionStrings.DefaultConnection` must match a valid tenant `ConnectionString` and the `DefaultTenantUrl` must match the `Host` address of an existing tenant.

* ### Test Tenants

Within the directory `src/Multitenant/Config`, you will find the sample `tenancy.json` file with a list of tenants with each sample defined like:

```[json]
{
    "Id": 1,
    "Name": "Default",
    "Host": "default.tenancy.localhost",
    "ConnectionString": "db\\default.db",
    "Theme": "blue"
}
```

**NOTE**: The tenant `Host` url must resolve to a valid IP address, in my case `localhost`, For more information on the setup, see [here](https://7labs.io/tips-tricks/how-to-set-up-your-own-custom-domain-on-localhost.html).

With &#x1F493; from [@codeprefect](https://gihub.com/codeprefect)
