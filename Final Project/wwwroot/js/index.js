﻿////var header = document.getElementById("click");
////var navs = header.getElementsByClassName("nav-item");
////for (var i = 0; i < navs.length; i++) {
////	navs[i].addEventListener("click", function () {
////		var current = document.getElementsByClassName("active");
////		current[0].className = current[0].className.replace(" active", "");
////		this.className += " active";
////	});
////}

//function makeTimer() {
//	$.ajax({
//		url: 'Consultation/GetDate',
//		//data: { 'id': selectedId, 'comments': comments },
//		type: "get",
//		cache: false,
//		success: function (data) {
//			var date = data;
//		},
//		error: function (xhr, ajaxOptions, thrownError) {
//			$('#lblCommentsNotification').text("Error encountered while saving the comments.");
//		}
//	});

//	var endTime = new Date(date);  //"29 April 2022 9:56:00 GMT+01:00"
//	endTime = (Date.parse(endTime) / 1000);

//	var now = new Date();
//	now = (Date.parse(now) / 1000);

//	var timeLeft = endTime - now;

//	var days = Math.floor(timeLeft / 86400);
//	var hours = Math.floor((timeLeft - (days * 86400)) / 3600);
//	var minutes = Math.floor((timeLeft - (days * 86400) - (hours * 3600)) / 60);
//	var seconds = Math.floor((timeLeft - (days * 86400) - (hours * 3600) - (minutes * 60)));

//	if (hours < "10") { hours = "0" + hours; }
//	if (minutes < "10") { minutes = "0" + minutes; }
//	if (seconds < "10") { seconds = "0" + seconds; }

//	$("#days").html(days + "<span>Days</span>");
//	$("#hours").html(hours + "<span>Hours</span>");
//	$("#minutes").html(minutes + "<span>Minutes</span>");
//	$("#seconds").html(seconds + "<span>Seconds</span>");

//}

//setInterval(function () { makeTimer(); }, 1000);
