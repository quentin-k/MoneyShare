function enableRegisterSpinner(on) {
    var registerSpinner = document.getElementById('registerSpinner');
    if (on)
        registerSpinner.classList.remove('d-none');
    else
        registerSpinner.classList.add('d-none');
}

document.getElementById("registerButton").addEventListener("click", e => {
    var registerStatus = document.getElementById('registerStatus');
    registerStatus.innerText = "";
    enableRegisterSpinner(true);
    var firstName = document.getElementById("firstName").value.trim();
    var lastName = document.getElementById("lastName").value.trim();
    var email = document.getElementById("email").value.trim();
    var username = document.getElementById("username").value.trim();
    var password = document.getElementById("password").value;
    var confirmPassword = document.getElementById("confirmPassword").value;
    if (password != confirmPassword) {
        enableRegisterSpinner(false);
        registerStatus.innerText = "Password and Confirm Password Did Not Match";
        return;
    }

    data = { 'firstname': firstName, 'lastname': lastName,'email':email,'username': username, 'password': password };
    myFetch('/api/v1/Register', 'PUT', false, data)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
            }
        })
        .then(data => {
            alert('Registration was successful! Please log in.');
            document.location.href = "/";
            enableRegisterSpinner(false);
        })
        .catch(error => {
            console.log(error);
            registerStatus.innerText = "Registration Failed";
            document.location.href = "#registerStatus";
            enableRegisterSpinner(false);
        });
});