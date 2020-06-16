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
    myFetch('/api/ForgotPassword', 'PUT', false, data)
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