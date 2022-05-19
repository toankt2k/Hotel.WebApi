$(document).ready(function(){
    setDefault()
    handlesubmitRegisterForm()
    
})
function setDefault(){
    $('#confirmForm').hide()
}
var users = {}
function handlesubmitLoginForm(){
	var loginForm = new Validator("#loginform",errorHandle)
		console.log(loginForm)
	loginForm.onSubmit= function(dataForm){
        console.log(dataForm)
		// for(var key in dataForm){
		// 	userModel[key]= dataForm[key]
		// }
		// pushDataUser(userModel,
		// function(data){
		// 	alert("Thay đổi thông tin cá nhân thành công")
		// },
		// function(error){
		// 	alert("Thay đổi giữ liệu không thành công")
		// })
	}
	function errorHandle(){
		alert("Dữ liệu thêm chưa hợp lệ")
	}
}
function handlesubmitRegisterForm(){
	var registerForm = new Validator("#registerForm",errorHandle)
		console.log(registerForm)
	registerForm.onSubmit= function(dataForm){
        console.log(dataForm)
        if(dataForm.Password != dataForm.ConfirmPassword){
            alert("Mật khẩu chưa trùng khớp")
        }
        else{
            
            fetch('https://localhost:7268/api/v1/Users/register', {
                method: 'POST', // or 'PUT'
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${window.localStorage.getItem("token")}`
                },
                body: JSON.stringify(dataForm),
            })
            .then(response => response.json())
            .then(data => {
                if(data){
                    users = dataForm
                    users.Password = ''
                    users.ConfirmPassword = ''
                    console.log(users)
                    $('#confirmForm').show()
                    $('#registerForm').hide()
                }
                else{
                    console.log(data)
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });
            
        }
		// for(var key in dataForm){
		// 	userModel[key]= dataForm[key]
		// }
		// pushDataUser(userModel,
		// function(data){
		// 	alert("Thay đổi thông tin cá nhân thành công")
		// },
		// function(error){
		// 	alert("Thay đổi giữ liệu không thành công")
		// })
	}
	function errorHandle(){
		alert("Dữ liệu thêm chưa hợp lệ")
	}
}
function confirm(){
    var IdentifyCode = document.querySelector('input[name=IdentifyCode]').value
    if(IdentifyCode.trim() == ''){
        alert('Vui lòng nhập code')
    }
    else{
        users.IdentifyCode = IdentifyCode
        fetch('https://localhost:7268/api/v1/Users/identify', {
                method: 'POST', // or 'PUT'
                headers: {
                    'Content-Type': 'application/json',
                    'Authorization': `Bearer ${window.localStorage.getItem("token")}`
                },
                body: JSON.stringify(users),
            })
            .then(response => response.json())
            .then(data => {
                if(data){
                    console.log(data)
                }
                else{
                    console.log(data)
                }
            })
            .catch((error) => {
                console.error('Error:', error);
            });
            
    }
}