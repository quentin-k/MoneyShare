// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function myFetch(url, method, authenticated, data = {}) {
    var requestInfo = {
        method: method,
        headers: { 'Content-Type': 'application/json' },
        cache: 'no-cache',
        credentials: 'same-origin',
        redirect: 'manual'
    };
    if (authenticated) {
        var token = sessionStorage.getItem('jwt');
        requestInfo.headers['Authorization'] = 'bearer ' + token;
    }
    if (method == 'POST' || method == 'PUT') {
        requestInfo.body = JSON.stringify(data);
    }

    return fetch(url, requestInfo);
}

function enableLoginSpinner(on) {
    var loginSpinner = document.getElementById('loginSpinner');
    if (on)
        loginSpinner.classList.remove('d-none');
    else
        loginSpinner.classList.add('d-none');
}

async function isLoggedIn() {
    var jwt = sessionStorage.getItem('jwt');
    if (jwt != null && jwt !== '') {
        myFetch('/api/Internal', 'GET', true)
            .then(response => { setLoggedIn(response.ok); showHideBasedOnLogin() })
            .catch(exception => { setLoggedIn(false); showHideBasedOnLogin() });
    }
    else {
        setLoggedIn(false);
        showHideBasedOnLogin();
    }

}


function showHideBasedOnLogin() {
    if (loggedIn) {
        document.querySelectorAll(".hide-loggedin").forEach(box => { box.classList.add('d-none') });
        document.querySelectorAll(".show-loggedin").forEach(box => { box.classList.remove('d-none') });
    }
    else {
        document.querySelectorAll(".hide-loggedin").forEach(box => { box.classList.remove('d-none') });
        document.querySelectorAll(".show-loggedin").forEach(box => { box.classList.add('d-none') });
    }
}

var ready = (callback) => {
    if (document.readyState != "loading") callback();
    else document.addEventListener("DOMContentLoaded", callback);
}

function setLoggedIn(result) { loggedIn = result; }

var loggedIn = false;

ready(() => {
    isLoggedIn();
});






document.getElementById("signout").addEventListener("click", e => {
    sessionStorage.removeItem('jwt');
    document.location.href = '/';

});

document.getElementById("loginButton").addEventListener("click", e => {
    document.getElementById('loginStatus').innerText = "";
    enableLoginSpinner(true);
    var username = document.getElementById("loginUsername").value;
    var password = document.getElementById("loginPassword").value;
    data = { 'username': username, 'password': password };
    myFetch('/api/Login', 'POST', false, data)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
                //return response.json();
            }
        })
        .then(data => {
            //sessionStorage.setItem('jwt', data.token);
            //document.location.href = '/';
            //isLoggedIn();
            //$('#modalLogin').modal('hide');
            document.getElementById('loginButton').classList.add('d-none');
            document.getElementById('confirmTwoFactorButton').classList.remove('d-none');
            document.getElementById('loginUsernamePasswordContainer').classList.add('d-none');
            document.getElementById('loginTwoFactorFormGroup').classList.remove('d-none');
            enableLoginSpinner(false);
        })
        .catch(error => {
            console.log(error);
            document.getElementById('loginStatus').innerText = "Login Failed";
            enableLoginSpinner(false);
        });
});

document.getElementById("confirmTwoFactorButton").addEventListener("click", e => {
    document.getElementById('loginStatus').innerText = "";
    enableLoginSpinner(true);
    var username = document.getElementById("loginUsername").value;
    var password = document.getElementById("loginPassword").value;
    var code = document.getElementById('loginTwoFactorValue').value;
    data = { 'username': username, 'password': password, 'SecondFactorValue': code };
    myFetch('/api/SecondFactor', 'POST', false, data)
        .then(response => {
            if (response.ok) {

                return response.json();
            }
            else
                enableLoginSpinner(false);
            throw new Error(response.status);
        })
        .then(data => {
            sessionStorage.setItem('jwt', data.token);
            document.location.href = '/';
            isLoggedIn();
            //$('#modalLogin').modal('hide');
            //document.getElementById('loginButton').classList.remove('d-none');
            //document.getElementById('confirmTwoFactorButton').classList.add('d-none');
            //document.getElementById('loginUsernamePasswordContainer').classList.remove('d-none');
            //document.getElementById('loginTwoFactorFormGroup').classList.add('d-none');
            enableLoginSpinner(false);
        })
        .catch(error => {
            console.log(error);
            document.getElementById('loginStatus').innerText = "Login Failed";
            enableLoginSpinner(false);
        });
});
function enableRegisterSpinner(on) {
    var registerSpinner = document.getElementById('registerSpinner');
    if (on)
        registerSpinner.classList.remove('d-none');
    else
        registerSpinner.classList.add('d-none');
}

document.getElementById("forgotPasswordButton").addEventListener("click", e => {
    var registerStatus = document.getElementById('registerStatus');
    registerStatus.innerText = "";
    enableRegisterSpinner(true);
    var firstName = document.getElementById("firstName").value.trim();
    var lastName = document.getElementById("lastName").value.trim();
    var email = document.getElementById("email").value.trim();
    var username = document.getElementById("username").value.trim();

    data = { 'firstname': firstName, 'lastname': lastName, 'email': email, 'username': username };
    myFetch('/api/ForgotPassword', 'POST', false, data)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
            }
        })
        .then(data => {
            alert('Please check your email for a link to reset password.');
            document.location.href = "/";
            enableRegisterSpinner(false);
        })
        .catch(error => {
            alert('Please check your email for a link to reset password.');
            document.location.href = "/";
            enableRegisterSpinner(false);
        });
});