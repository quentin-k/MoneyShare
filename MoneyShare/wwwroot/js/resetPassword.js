document.getElementById('ResetPasswordSubmit').addEventListener("click", e => {
    var userId = document.getElementById('userId').value;
    var token = document.getElementById('token').value;
    var password = document.getElementById('Password').value;
    var confirmPassword = document.getElementById('ConfirmPassword').value;
    if (password == confirmPassword) {
        var data = { 'userId': userId, 'token': token, 'password': password, 'confirmPassword': confirmPassword };
        var requestInfo = { 'method': 'POST', body: JSON.stringify(data), headers: { 'Content-Type': 'application/json' }, credentials: 'same-origin' };
        fetch('/api/ResetPassword', requestInfo)
            .then(response => {
                if (response.ok) {
                    document.getElementById('loginStatus').innerText = "Password Reset Suscessful";
                    document.location = '/#LoginModal';
                } else {
                    document.getElementById('ResetPasswordStatus').innerText = "Password Reset Failed";
                }
            });
    } else {
        document.getElementById('ResetPasswordStatus').innerText = "Passwords do not match";
    }
});