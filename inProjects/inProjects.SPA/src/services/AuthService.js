import { select } from "async";

const host = process.env.VUE_APP_BACKEND;

class AuthService {
    constructor() {
        this.allowedOrigins = [];
        this.providers = {};
        this.logoutEndpoint = null;
        this.appRedirect = () => null;
        this.authenticatedCallbacks = [];
        this.mailSentCallbacks = [];
        this.signedOutCallbacks = [];
        this.identity = null;
        this.userProvider = null;

        window.addEventListener("message", (e) => this.onMessage(e), false);
    }

    get isConnected() {
        return this.identity != null;
    }

    get accessToken() {
        var identity = this.identity;
        return identity ? identity.bearer.accessToken : null;
    }

    get email() {
        var identity = this.identity;

        return identity ? identity.email : null;
    }

    get boundProviders() {
        var identity = this.identity;

        return identity ? identity.boundProviders : [];
    }

    isBoundToProvider(expectedProviders) {
        var isBound = false;

        for (var p of expectedProviders) {
            if (this.boundProviders.indexOf(p) > -1) isBound = true;
        }

        return isBound;
    }

    onMessage(e) {
        if (!e.origin || this.allowedOrigins.indexOf(e.origin) < 0) return;

        var data = e.data;

        if (data.type == 'authenticated') this.onAuthenticated(data.payload);
        else if (data.type == 'signedOut') this.onSignedOut();
        else if (data.type == 'linkAccount') this.onMailSent(data.payload);
    }

    onMailSent(i) {
        this.identity = i;
        for (var cb of this.mailSentCallbacks) {
            cb();
        }
    }

    registerMailSentCallback(cb) {
        this.mailSentCallbacks.push(cb);
    }

    removeMailSentCallback(cb) {
        this.mailSentCallbacks.splice(this.mailSentCallbacks.indexOf(cb), 1);
    }

    login(selectedProvider) {
        var provider = this.providers[selectedProvider];
        this.userProvider = selectedProvider;
        var popup = window.open(provider.endpoint, "Connexion à Pick'N'Trip", "menubar=no, status=no, scrollbars=no, menubar=no, width=700, height=700");
    }

    registerAuthenticatedCallback(cb) {
        this.authenticatedCallbacks.push(cb);
    }

    removeAuthenticatedCallback(cb) {
        this.authenticatedCallbacks.splice(this.authenticatedCallbacks.indexOf(cb), 1);
    }

    onAuthenticated(i) {
        this.identity = i;

        for (var cb of this.authenticatedCallbacks) {
            cb();
        }
    }

    logout() {
        var popup = window.open(this.logoutEndpoint, "Déconnexion de Pick'N'Trip", "menubar=no, status=no, scrollbars=no, menubar=no, width=700, height=600");
    }

    registerSignedOutCallback(cb) {
        this.signedOutCallbacks.push(cb);
    }

    removeSignedOutCallback(cb) {
        this.signedOutCallbacks.splice(this.signedOutCallbacks.indexOf(cb), 1);
    }

    onSignedOut() {
        this.identity = null;

        for (var cb of this.signedOutCallbacks) {
            cb();
        }
    }

    getUserProvider() {
        return this.userProvider;
    }
    
    async getToken() {
        let result = await fetch(host + '/api/token', {
            credentials: 'include',
            method: 'GET',
            mode: 'cors',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (result.ok) {
            let token = await result.json();
            window.console.log(token);
            if (token.success) return token;
        }

        return null;
    }

    async refreshToken() {
        this.identity = await this.getToken();
    }

    async init() {
        let token = await this.getToken();
        if (token !== null) this.onAuthenticated(token);
    }
}

export default new AuthService();