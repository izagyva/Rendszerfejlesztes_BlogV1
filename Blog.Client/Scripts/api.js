var defaultUrl = "http://localhost:5276/api/";

async function postData(url = "", data = {}, needAuth = true) {
    const response = await fetch(defaultUrl + url, {
        method: "POST",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + (needAuth ? JSON.parse(localStorage.getItem("data")).token : null),
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
        body: testJSON(data) ? data : JSON.stringify(data),
    });
    if (response.status === 401 || response.status === 403) {
        logout();
    }
    try {
        return await response.json();
    } catch (error) {
        console.error("Error parsing JSON:", error);
        return {};
    }
}

async function putData(url = "", data = {}, needAuth = true) {
    const response = await fetch(defaultUrl + url, {
        method: "PUT",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + (needAuth ? JSON.parse(localStorage.getItem("data")).token : null),
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
        body: testJSON(data) ? data : JSON.stringify(data),
    });
    if (response.status === 401 || response.status === 403) {
        logout();
    }
    try {
        return await response.json();
    } catch (error) {
        console.error("Error parsing JSON:", error);
        return {};
    }
}

async function getData(url = "", needAuth = true) {
    const response = await fetch(defaultUrl + url, {
        method: "GET",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + (needAuth ? JSON.parse(localStorage.getItem("data")).token : null),
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
    });
    if (response.status === 401 || response.status === 403) {
        logout();
    }
    return response.json();
}

async function deleteData(url = "", needAuth = true) {
    const response = await fetch(defaultUrl + url, {
        method: "DELETE",
        mode: "cors",
        cache: "no-cache",
        credentials: "same-origin",
        headers: {
            "Content-Type": "application/json",
            Authorization: "Bearer " + (needAuth ? JSON.parse(localStorage.getItem("data")).token : null),
        },
        redirect: "follow",
        referrerPolicy: "no-referrer",
    });
    if (response.status === 401 || response.status === 403) {
        logout();
    }
    return response.json();
}

function testJSON(text) {
    if (typeof text !== "string") {
        return false;
    }
    try {
        JSON.parse(text);
        return true;
    } catch (error) {
        return false;
    }
}

function logout() {
    localStorage.clear();
    window.location.href = "login.html";
}
