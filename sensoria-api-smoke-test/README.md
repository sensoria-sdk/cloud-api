# sensoria-api-smoke-test
This command line tool provides a sample of API usage, including the required handshaking of the authentication, for app to cloud or cloud to cloud operations.

**This code assumes you have obtained a "confidential" type app id/key from Sensoria through the Sensoria Developer Kit purchase / agreement.**

**Not all API methods are illustrated in this sample code.**
Please refer to: https://developer.sensoria.io/docs/services/56b0e0cc2bc1c50ebc6bd2d4/operations/56e085102bc1c5086021aa2f

## Build
Open the solution in VS 2017 or above, restore NuGet packages and compile
You can alter the code at CommandLineOptions.cs to include your clientid and clientsecret, or pass it on via command line each time

## Usage
Sensoria.Api.SmokeTest supports the following parameters:
* environment: point to production (prod==DEFAULT) or test environment (_note: test environment is by design an unstable environment with no guarantees of data integrity or availability)
* clientid: client id to identify the application calling - if not passed, the default will be used (which has to be set in code, see above)
* clientsecret: client secret to authenticate the app calling - if not passed, the default will be used (which has to be set in code, see above)
* username: username of the user associated to the API call - if not passed, the default smoketest@sensoriainc.com will be used
* password: password of the user associated to the API call - if not passed, TestPassword will be used
* testonly: tests only a part of the API calls: valid entries are access / sessions / shoecloset / achievements / shoedetails

Example:
Sensoria.SmokeTest.Api.exe -environment test -clientid 12345 -clientsecret ASdadkiladhas== -testonly Access

