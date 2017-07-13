# SSO-Test
Application for test EVE-Online SSO authentication.

Them main thing to test is ability of authenticate a desktop Windows application through SSO without the client secret. By OAuth 2.0 specs the client secret should not be disclosed but desktop application, as well as SPA and mobile applications, can not keep anything in secret by itself. [The current recommendations](https://aaronparecki.com/oauth-2-simplified/#single-page-apps) are to allow applications to authenticate without client ID and secret using Authentication Code flow ([Redhat](https://www.ietf.org/mail-archive/web/oauth/current/msg16966.html), [Deutsche Telekom](https://www.ietf.org/mail-archive/web/oauth/current/msg16968.html), [Smart Health IT](https://www.ietf.org/mail-archive/web/oauth/current/msg16967.html)). The current docs and examples for EVE-Online SSO authentication don't show this as a possibility. Before open an issue for it we need to validate this inability.

The application uses registering of a custom URI-protocol on Windows to get redirections from a browser.

To run the test:
1. Compile solution.
2. Start the application.
3. Install custom URI-protocol by pressing the topmost button.
4. Press the second button to test the Authentication flow _WITH_ using of the application secret key.
5. Press the third button to test the Authentication flow _WITHOUT_ using of the application secret key.

A sample output:
```
HOW TO USE
For the first time press the topmost button to register a custom URI-scheme.
This will produce a .reg file in the temp-folder which will be imported
into your registry. It's safe. You can check the contents of this file afterwards.
If you want to check it beforehand look at the source code at:
https://github.com/Eshva/SSO-Test

After that you can check the next to buttons to authenticate WITH and WITHOUT
the client secret key by pressing the second and the third buttons.

The button #4 is to check API call of your character standings. (not implemented yet)

Installing a custom URI scheme eveauth-sso-test...
Custom URI scheme 'eveauth-sso-test' is installed.
Afterwards you can remove this URI scheme by deleting the registry key:
HKEY_CLASSES_ROOT\eveauth-sso-test

Try to get access and refresh tokens using the OAuth 2 Authentication flow
WITH sending the client secret key.
Expected to get them. But this way we DISCLOSE the secret key through source code
but we should not do it according to security reasons!

Open authentication page in default browser...
Authentication code from SSO received:
eveauth-sso-test://auth/?code=wm4AMg6hU2lTtw3ZMh719pPeiTLGxBJo3LUOJHIQf2pNrJ4B0TeW0N-zteuPwa1E0&state=test-state
Requesting access tokens...
Tokens received:
{"access_token":"wpb1BIMSU4vAICqBkpyhS_5I6XmCFQr0gN2sby1dVNonE2IafDzg-uxA7IfExvrKBmSWyW380B9AlP9rMkbH4A2","refresh_token":"rjx7IeDErgZeRJPt7XN-nb_GZVabU0CwILfhpp4LQFo1","token_type":"Bearer","expires_in":1199}

Try to get access and refresh tokens using the OAuth 2 Authentication flow
WITHOUT sending the client secret key.
Expecting to get them accourding to the new recomendation.
But CCP SSO don't authenticate us.

Open authentication page in default browser...
Authentication code from SSO received:
eveauth-sso-test://auth/?code=NekycTfY7wESH1N01biv2D8gkwFHvi5Tzse0YMpMXLnbgerABByXFEqXowFQjxbg0&state=test-state
Requesting access tokens...
System.Net.WebException: Удаленный сервер возвратил ошибку: (400) Недопустимый запрос.
   в System.Net.HttpWebRequest.GetResponse()
   в Eshva.SsoTest.UI.Services.SsoAuthenticationService.RequestTokens(String authorizationCode) в B:\Projects\GitHub\Eshva\SSO-Test\Eshva.SsoTest.UI\Services\SsoAuthenticationService.cs:строка 56
   в Eshva.SsoTest.UI.Services.SsoAuthenticationService.GetTokens(String uri) в B:\Projects\GitHub\Eshva\SSO-Test\Eshva.SsoTest.UI\Services\SsoAuthenticationService.cs:строка 36
   в Eshva.SsoTest.UI.ViewModels.ShellViewModel.HandleUri(String uri) в B:\Projects\GitHub\Eshva\SSO-Test\Eshva.SsoTest.UI\ViewModels\ShellViewModel.cs:строка 99
{"error":"invalid_client","error_description":"Unknown client"}
CCP PLEASE FIX this error. You should comply the current recomendations.
Otherwise you LIMIT us to server based web-applications or the implicit flow
which requires our applicaiton users to authenticate desktop, SPA and mobile
applications each 20 minutes. This is not acceptable!!!
```
