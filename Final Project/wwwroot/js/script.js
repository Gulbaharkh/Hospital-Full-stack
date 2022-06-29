$('.accordion__header').click(function (e) {
	e.preventDefault();
	var currentIsActive = $(this).hasClass('is-active');
	$(this).parent('.accordion').find('> *').removeClass('is-active');
	if (currentIsActive != 1) {
		$(this).addClass('is-active');
		$(this).next('.accordion__body').addClass('is-active');
	}
});

function testFromScript() {
	alert("Hello from script")
}
function sendMail(params) {
	console.log("here we go");
  let fromName = document.getElementById("fromName").value;
  console.log(fromName);
  let toName = document.getElementById("toName").value;
	let msg = document.getElementById("msg").value;

	//let fname = document.getElementById("fname").value;
	//let lname = document.getElementById("lname").value;
	//let email = document.getElementById("email").value;
	//let subject = document.getElementById("subject").value;
	//let choose = document.getElementById("choose").value;
	//let appMessage = document.getElementById("message").value;

  emailjs.send("service_rqonmsj","template_vhpsjqa",{
	to_name: toName,
	from_name: fromName,
	  message: msg,

	  //fname: fname,
	  //lname: lname,
	  //email: email,
	  //subject: subject,
	  //choose: choose,
	  //app_message: message
}).then(function(res)
{
  console.log("success",res.status);
});
document.getElementById("fromName").value = "";
document.getElementById("toName").value = "";
document.getElementById("msg").value = "";

	//document.getElementById("fname").value = "";
	//document.getElementById("lname").value = "";
	//document.getElementById("email").value = "";
	//document.getElementById("subject").value = "";
	//document.getElementById("choose").value = "";
	//document.getElementById("message").value = "";

}

function makeAppointment(params) {
	console.log("here we go");


	let fname = document.getElementById("fname").value;
	let lname = document.getElementById("lname").value;
	let email = document.getElementById("email").value;
	let contact = document.getElementById("subject").value;
	let subject = document.getElementById("choose").value;
	let message = document.getElementById("message").value;

	emailjs.send("service_rqonmsj", "app_form", {
		
		name: fname,
		surname: lname,
		email: email,
		contact: contact,
		subject: subject,
		message: message
	}).then(function (res) {
		console.log("success", res.status);
	});
	

	document.getElementById("fname").value = "";
	document.getElementById("lname").value = "";
	document.getElementById("email").value = "";
	document.getElementById("subject").value = "";
	document.getElementById("choose").value = "";
	document.getElementById("message").value = "";

}
function makeTimer() {
	var date;
	$.ajax({
		url:'/Appointment/GetDate',
		//data: { 'id': selectedId, 'comments': comments },
		type: "get",
		cache: false,
		success: function (data) {
			console.log(data);
			date = data;
			console.log("here we go");
			var endTime = new Date(date);  //"29 April 2022 9:56:00 GMT+01:00"
			console.log(endTime);
			endTime = (Date.parse(endTime) / 1000);

			var now = new Date();
			now = (Date.parse(now) / 1000);

			var timeLeft = endTime - now;
			console.log(timeLeft);
			
			var days = Math.floor(timeLeft / 86400);
			var hours = Math.floor((timeLeft - (days * 86400)) / 3600);
			var minutes = Math.floor((timeLeft - (days * 86400) - (hours * 3600)) / 60);
			var seconds = Math.floor((timeLeft - (days * 86400) - (hours * 3600) - (minutes * 60)));

			if (hours < "10") { hours = "0" + hours; }
			if (minutes < "10") { minutes = "0" + minutes; }
			if (seconds < "10") { seconds = "0" + seconds; }

			if (timeLeft <= 0)
			{
				$(".btn-appointment").addClass("disabled");
				$(".btn-app").addClass("disabled");
				$(".btn-make").addClass("disabled");
			}

			console.log(days);
			console.log(hours);
			console.log(minutes);
			console.log(seconds);
			if (days >= 0 && hours >= 0 && minutes >= 0 && seconds >= 0) {
					$("#days").html(days + "<span>Days</span>");
					$("#hours").html(hours + "<span>Hours</span>");
					$("#minutes").html(minutes + "<span>Minutes</span>");
					$("#seconds").html(seconds + "<span>Seconds</span>");
			}
			else {
					$("#days").html("00" + "<span>Days</span>");
					$("#hours").html("00" + "<span>Hours</span>");
					$("#minutes").html("00" + "<span>Minutes</span>");
					$("#seconds").html("00" + "<span>Seconds</span>");
				}
			
			
			
			if (timeLeft == 0) clearInterval(makeTimer());
		},
		error: function (xhr, ajaxOptions, thrownError) {
			$('#lblCommentsNotification').text("Error encountered while saving the comments.");
		}

	});
	console.log(date);
	


}

setInterval(function () { makeTimer(); }, 1000);


	$('#menuToggle').click(function (e) {
		$('#menu').toggle();
		$('body');
	});

document.onreadystatechange = function () {
	var state = document.readyState
	if (state == 'complete') {
		setTimeout(function(){
			document.getElementById('interactive');
			   document.getElementById('preloader').style.visibility="hidden";
			},1000);
		}
}
document.addEventListener("DOMContentLoaded", function(){
	window.addEventListener('scroll', function() {
		if (window.scrollY > 150) {
		  document.getElementById('navbarScroll').classList.add('fixed-top');
		  navbar_height = document.querySelector('.menu').offsetHeight;
		  document.body.style.paddingTop = navbar_height + 'px';
		} else {
		  document.getElementById('navbarScroll').classList.remove('fixed-top');
		  document.body.style.paddingTop = '0';
		} 
	});
}); 


AOS.init({
	duration: 1200,
})



