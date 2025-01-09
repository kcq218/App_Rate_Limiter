# App_Rate_Limiter
Rate Limitter for URL Hash Generator

This app was built using the Token Bucket Algorithm

Lessons learned:

Allow Anyone to access the application once Authentication is setup
This allows apps that has token to access

For client id and client secret authentication/authorization you need three things
Client id
Client secret
Scope (This is usually appended .default after application ID url in App registration)
This is also known as client credential flow
Need to register app first
Proceed to expose Api in the app registry for the resource app
Make sure you are the owner of the App registered

Create Client App Registry
Setup api permissions tying it to App
Use client id and secret of approved client to access app.
Use https://login.microsoftonline.com/organizations/oauth2/v2.0/token to get token

Setup Managed Identity for all apps
To access key vault use IAM at the key vault level for inheritance
User ENV variables to store config values. User kv secrets if necessary
Host.json is local config file

Pass parameters through method, if context is important
Use await all the way to the top level

Use appinsights transaction search feature to look at logs 

To do List:
Setup Rolling window rate limiter for other endpoints
