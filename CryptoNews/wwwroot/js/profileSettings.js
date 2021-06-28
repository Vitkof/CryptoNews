let loginReq = new XMLHttpRequest();
loginReq.open('GET', '/Account/Settings', false);

loginReq.onload = function () {
    if (loginReq.status >= 200 && loginReq.status < 400) {
        let resp = loginReq.responseText;
        document.getElementById('navbar-nav').innerHTML += resp;
    }
}

loginReq.send();
