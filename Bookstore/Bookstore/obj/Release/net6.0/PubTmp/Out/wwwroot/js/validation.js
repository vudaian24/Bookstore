//Kiểm tra email
function isEmail(email) {
    var emailRegex = /^[^\s@]+@gmail\.com$/i;

    var isGmail = emailRegex.test(email);

    return isGmail;
}

//Kiểm tra số điện thoại
function validatePhoneNumber(str) {
    const phoneNumber = str.trim();
    // Kiểm tra xem đầu vào có đúng 10 ký tự không
    if (phoneNumber.length !== 10) {
        return false;
    }
    // Kiểm tra xem đầu vào có chứa các ký tự không phải số không
    if (!/^\d+$/.test(phoneNumber)) {
        return false;
    }
    return true;
}

//Kiểm tra độ dài mật khẩu
function validatePassword(input) {
    const password = input.trim();
    // Kiểm tra xem độ dài của mật khẩu có lớn hơn 8 ký tự không
    if (password.length >= 8) {
        return false;
    }
    // Kiểm tra xem mật khẩu có chứa ít nhất một chữ in hoa không
    if (!/[A-Z]/.test(password)) {
        return false;
    }
    return true;
}
