
document.getElementById("forgotPasswordButton").addEventListener("click", e => {
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
        })
        .catch(error => {
            alert('Please check your email for a link to reset password.');
            document.location.href = "/";
        });
});