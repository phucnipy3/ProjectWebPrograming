// JavaScript Document
var isDown = false;
var originPositon;
var originMouseX;
var originMouseY;
var currentElement;
function rgbToHex(rgb) {
	"use strict";
  var a = rgb.split("(")[1].split(")")[0].split(",");
  return "#" + a.map(function(x) {
    x = parseInt(x).toString(16);
    return (x.length === 1) ? "0"+x : x;
  }).join("");
}
$(document).ready(function(){
	"use strict";
	$("#Delete").click(function(){
		$(currentElement).remove();
	});
	$("#Refresh").click(function(){
		$("#DesignArea").empty();
	});
	$("#AddText").click(function(){
		
		var txt = document.createElement("span");
		txt.innerHTML = "Hello";
		txt.style.fontSize = "30px";
		txt.style.fontFamily = "arial";
		txt.style.position = "absolute";
		txt.style.left = "75px";
		txt.style.top = "120px";
		txt.classList.add("UIDraggable");
		txt.style.border = "1px dashed #72C3AB";
		txt.style.color = "#000000";
		currentElement = $(txt);
		$(".UIDraggable").css("border","0px dashed #72C3AB");
		$("#DesignArea").append(txt);
	});
	$("#Size").keyup(function(){
		 if (this.value !== this.value.replace(/[^0-9]/g, '')) {
			 this.value = this.value.replace(/[^0-9]/g, '');
		 }
	});
	$(".number").keyup(function(){
		 if (this.value !== this.value.replace(/[^0-9]/g, '')) {
			 this.value = this.value.replace(/[^0-9]/g, '');
		 }
	});
	$("#Update").click(function(){
		currentElement.html(document.getElementById("EnterText").value);
		currentElement.css("font-size",document.getElementById("Size").value +"px");
		currentElement.css("color",document.getElementById("EnterColor").value);
		currentElement.css("font-family",document.getElementById("EnterFont").value);
	});
	$("#UpdateImg").click(function(){
		currentElement.css("width",$("#ChieuRong").val()+"px");
		currentElement.css("height",$("#ChieuCao").val()+"px");
	});
	$(document).on("mousedown",".UIDraggable",function(){
			isDown = true;
			originPositon = $(this).position();
			originMouseX = event.clientX;
			originMouseY = event.clientY;
			currentElement = $(this);
			$(".UIDraggable").css("border","0px dashed #72C3AB");
			currentElement.css("border","1px dashed #72C3AB");
			
		});
	$(document).on("mousedown","span.UIDraggable",function(){
		$("#SizeText").css("visibility","visible");
		$("#SizeImg").css("visibility","hidden");
		$("#EnterText").val(currentElement.html());
		$("#Size").val(currentElement.css("font-size").replace(/[^0-9]/g, ''));
		$("#EnterColor").val(rgbToHex($(currentElement).css("color")));
		$("#EnterFont").val(currentElement.css("font-family"));
	});
	$(document).on("mousedown","div.UIDraggable",function(){
		$("#SizeText").css("visibility","hidden");
		$("#SizeImg").css("visibility","visible");
		$("#ChieuRong").val($(currentElement).css("width").replace(/[^0-9]/g, ''));
		$("#ChieuCao").val($(currentElement).css("height").replace(/[^0-9]/g, ''));
	});
	$(document).on("mouseup",".UIDraggable",function(){
		isDown = false;
	});
	$(document).on("mousemove",".design-area",function(){
		if(isDown){
			$(currentElement).css("left",originPositon.left+(event.clientX-originMouseX)).css("top",originPositon.top+(event.clientY-originMouseY));
		}	
	});
	
	
});