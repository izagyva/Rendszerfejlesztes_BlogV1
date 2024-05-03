async function login() {
    var username = document.getElementById('typeUsername').value;
    var password = document.getElementById('typePassword').value;
    if (isEmpty(username) || isEmpty(password)) {
        alert('Felhasználónév és jelszó megadása kötelező!');
    } else {
        var data = {
            username: username,
            password: password
        };
        await postData("auth/login", data, false)
            .then(async (data) => {
                if (await data.token) {
                    localStorage.setItem("data", JSON.stringify(data));
                    window.location.href = "index.html";
                } else {
                    alert(await data.Message);
                }
            });
    }
}

function isEmpty(str) {
    return (!str || 0 === str.length);
}
