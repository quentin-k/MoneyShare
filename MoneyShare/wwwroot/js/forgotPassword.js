
document.getElementById("forgotPasswordButton").addEventListener("click", e => {
    var forgotPasswordFirstName = document.getElementById("forgotPasswordFirstName").value.trim();
    var forgotPasswordLastName = document.getElementById("forgotPasswordLastName").value.trim();
    var forgotPasswordEmail = document.getElementById("forgotPasswordEmail").value.trim();
    var forgotPasswordUsername = document.getElementById("forgotPasswordUsername").value.trim();

    data = { 'FirstName': forgotPasswordFirstName, 'LastName': forgotPasswordLastName, 'Email': forgotPasswordEmail, 'Username': forgotPasswordUsername };
    myFetch('/api/ForgotPassword', 'POST', false, data)
        .then(response => {
            if (!response.ok) {
                throw new Error(response.status);
            }
        })
        .then(data => {
            alert('Please check your email for a link to reset password.');
            document.location.href = "/";
        })
        .catch(error => {
            alert('Please check your email for a link to reset password.');
            document.location.href = "/";
        });
});