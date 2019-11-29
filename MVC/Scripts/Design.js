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

function CloseDialog(str)
{
    document.getElementById(str).style.visibility = "hidden";
return true;
}
function ShowDialog(str)
{
    document.getElementById(str).style.visibility = "visible";
return true;
}
function ResetSizeText()
{
    document.getElementById("EnterText").value = "Hello";
    document.getElementById("EnterColor").value = "#000000";
    document.getElementById("Size").value = "30";
    document.getElementById("EnterFont").value = "arial";
}
function SetFont(st)
{
    document.getElementById("EnterFont").value = st;
    return true;
}
function ChangeColor(str)
{
    document.getElementById("CaseColor").style.backgroundColor = str;
    document.getElementById("CaseColor").style.backgroundImage = "";
    return true;
}
function ChangeHeight()
{
    document.getElementById("CaseColor").style.height = document.getElementById("Img").height + "px";
    return true;
}
function ChangeCase(str)
{
    $("#Img").attr("src", "/Resource/OpTrong/" + str);
return true;
}
function ThemHinh(str)
{
    return AddImg("/Resource/HinhMau/"+str);
}
function AddImg(str)
{
	var img = document.createElement("div");
    img.style.position = "absolute";
    img.style.left = "100px";
    img.style.top = "120px";
    img.classList.add("UIDraggable");
    img.style.border = "1px dashed #72C3AB";
    img.style.width = "200px";
    img.style.height = "200px";
    img.style.backgroundSize = "100% 100%";
    img.style.backgroundRepeat = "no-repeat";
    currentElement = $(img);
    currentElement.css("background-image","url("+str+")");
    $(".UIDraggable").css("border","0px dashed #72C3AB");
    $("#DesignArea").append(img);
    CloseDialog('SizeText');
    ShowDialog('SizeImg');
    ResetSizeImg();
    return true;
}
function ThemHinhTuLink()
{
	var str = document.getElementById("LinkAnh").value.trim();
    document.getElementById("LinkAnh").value = "";
    return AddImg(str);
}
function ResetSizeImg()
{
    document.getElementById("ChieuCao").value = "200";
    document.getElementById("ChieuRong").value = "200";
    return true;
}
function MoChiTiet()
{
    window.open('trangbanhang/thongtin.html');
}
function ChangeBackground(str)
{
    document.getElementById("CaseColor").style.backgroundImage = "url('/Resource/WaterColor/" + str + "')";
    document.getElementById("CaseColor").style.backgroundColor = "#FFFFFF";
    return true;
}
function loginClick()
{
    CloseDialog("login");
inforlogin();
}
function signinClick()
{
    CloseDialog("signin");
    alert("Bạn đã đăng ký thành công!");
return true;
}
function inforlogin()
{
    alert("Bạn đã đăng nhập thành công!");
}
function GioHangClick()
{
    alert("Giỏ hàng của bạn đang trống");
}
