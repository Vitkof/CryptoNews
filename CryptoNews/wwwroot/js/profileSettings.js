﻿let loginReq = new XMLHttpRequest();
loginReq.open('GET', '/Account/Settings', true);

loginReq.onload = function () {
    if (loginReq.status >= 200 && loginReq.status < 400) {
        let resp = loginReq.responseText;
        document.getElementById('profile-settings').innerHTML += resp;
    }
}

loginReq.send();
