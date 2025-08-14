// auth.js

export function storeSession(key, value) {
    localStorage.setItem(key, value);
}

export function getSession(key) {
    return localStorage.getItem(key);
}

export function clearSession(key) {
    localStorage.removeItem(key);
}

// Attempt login
export async function loginSSO() {
    const existingAccount = msalInstance.getAllAccounts()[0];

    if (existingAccount) {
        console.log("Already signed in:", existingAccount.username);
        storeSession("user", existingAccount.username);
        return;
    }

    try {
        const result = await msalInstance.loginPopup({
            scopes: ["User.Read"]
        });

        console.log("Logged in:", result.account.username);
        storeSession("user", result.account.username);
    } catch (error) {
        console.error("SSO login failed:", error);
        clearSession("user");
    }
}

