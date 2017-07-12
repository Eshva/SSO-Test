# SSO-Test
Application for test EVE-Online SSO authentication.

Them main thing to test is ability of authenticate a desktop Windows application through SSO without the client secret. By OAuth 2.0 specs the client secret should not be disclosed but desktop application, as well as SPA and mobile applications, can not keep anything in secret by itself. The current recommendations are allow applications to authenticate without client ID and secret using Authentication Code flow. The current docs and examples for EVE-Online SSO authentication don't show this as a possibility. Before open an issue for it we need to validate this inability.

The application uses registering of a custom URI-protocol on Windows to get redirections from a browser.
