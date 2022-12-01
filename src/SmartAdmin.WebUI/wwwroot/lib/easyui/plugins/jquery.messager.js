/**
 * EasyUI for jQuery 1.10.8
 * 
 * Copyright (c) 2009-2022 www.jeasyui.com. All rights reserved.
 *
 * Licensed under the freeware license: http://www.jeasyui.com/license_freeware.php
 * To use it on other terms please contact us: info@jeasyui.com
 *
 */
(function($){
function _1(){
$(document)._unbind(".messager")._bind("keydown.messager",function(e){
if(e.keyCode==27){
$("body").children("div.messager-window").children("div.messager-body").each(function(){
$(this).dialog("close");
});
}else{
if(e.keyCode==9){
var _2=$("body").children("div.messager-window");
if(!_2.length){
return;
}
var _3=_2.find(".messager-input,.messager-button .l-btn");
for(var i=0;i<_3.length;i++){
if($(_3[i]).is(":focus")){
$(_3[i>=_3.length-1?0:i+1]).focus();
return false;
}
}
}else{
if(e.keyCode==13){
var _4=$(e.target).closest("input.messager-input");
if(_4.length){
var _5=_4.closest(".messager-body");
_6(_5,_4.val());
}
}
}
}
});
};
function _7(){
$(document)._unbind(".messager");
};
function _8(_9){
var _a=$.extend({},$.messager.defaults,{modal:false,shadow:false,draggable:false,resizable:false,closed:true,style:{left:"",top:"",right:0,zIndex:$.fn.window.defaults.zIndex++,bottom:-document.body.scrollTop-document.documentElement.scrollTop},title:"",width:300,height:150,minHeight:0,showType:"slide",showSpeed:600,content:_9.msg,timeout:4000},_9);
var _b=$("<div class=\"messager-body\"></div>").appendTo("body");
_b.dialog($.extend({},_a,{noheader:(_a.title?false:true),openAnimation:(_a.showType),closeAnimation:(_a.showType=="show"?"hide":_a.showType),openDuration:_a.showSpeed,closeDuration:_a.showSpeed,onOpen:function(){
_b.dialog("dialog").hover(function(){
if(_a.timer){
clearTimeout(_a.timer);
}
},function(){
_c();
});
_c();
function _c(){
if(_a.timeout>0){
_a.timer=setTimeout(function(){
if(_b.length&&_b.data("dialog")){
_b.dialog("close");
}
},_a.timeout);
}
};
if(_9.onOpen){
_9.onOpen.call(this);
}else{
_a.onOpen.call(this);
}
},onClose:function(){
if(_a.timer){
clearTimeout(_a.timer);
}
if(_9.onClose){
_9.onClose.call(this);
}else{
_a.onClose.call(this);
}
_b.dialog("destroy");
}}));
_b.dialog("dialog").css(_a.style);
_b.dialog("open");
return _b;
};
function _d(_e){
_1();
var _f=$("<div class=\"messager-body\"></div>").appendTo("body");
_f.dialog($.extend({},_e,{noheader:(_e.title?false:true),onClose:function(){
_7();
if(_e.onClose){
_e.onClose.call(this);
}
_f.dialog("destroy");
_10();
}}));
var win=_f.dialog("dialog").addClass("messager-window");
win.find(".dialog-button").addClass("messager-button").find("a:first").focus();
return _f;
};
function _6(dlg,_11){
var _12=dlg.dialog("options");
dlg.dialog("close");
_12.fn(_11);
};
function _10(){
var top=20+document.body.scrollTop+document.documentElement.scrollTop;
$("body>.messager-tip").each(function(){
$(this).animate({top:top},200);
top+=$(this)._outerHeight()+10;
});
};
$.messager={show:function(_13){
return _8(_13);
},tip:function(msg){
var _14=typeof msg=="object"?msg:{msg:msg};
if(_14.timeout==null){
_14.timeout=2000;
}
var top=0;
var _15=$("body>.messager-tip").last();
if(_15.length){
top=parseInt(_15.css("top"))+_15._outerHeight();
}
var cls=_14.icon?"messager-icon messager-"+_14.icon:"";
_14=$.extend({},$.messager.defaults,{content:"<div class=\""+cls+"\"></div>"+"<div style=\"white-space:nowrap\">"+_14.msg+"</div>"+"<div style=\"clear:both;\"></div>",border:false,noheader:true,modal:false,title:null,width:"auto",height:"auto",minHeight:null,shadow:false,top:top,cls:"messager-tip",bodyCls:"f-row f-vcenter f-full"},_14);
var dlg=_d(_14);
if(_14.timeout){
setTimeout(function(){
if($(dlg).closest("body").length){
$(dlg).dialog("close");
}
},_14.timeout);
}
setTimeout(function(){
_10();
},0);
return dlg;
},alert:function(_16,msg,_17,fn){
var _18=typeof _16=="object"?_16:{title:_16,msg:msg,icon:_17,fn:fn};
var cls=_18.icon?"messager-icon messager-"+_18.icon:"";
_18=$.extend({},$.messager.defaults,{content:"<div class=\""+cls+"\"></div>"+"<div>"+_18.msg+"</div>"+"<div style=\"clear:both;\"></div>"},_18);
if(!_18.buttons){
_18.buttons=[{text:_18.ok,onClick:function(){
_6(dlg);
}}];
}
var dlg=_d(_18);
return dlg;
},confirm:function(_19,msg,fn){
var _1a=typeof _19=="object"?_19:{title:_19,msg:msg,fn:fn};
_1a=$.extend({},$.messager.defaults,{content:"<div class=\"messager-icon messager-question\"></div>"+"<div>"+_1a.msg+"</div>"+"<div style=\"clear:both;\"></div>"},_1a);
if(!_1a.buttons){
_1a.buttons=[{text:_1a.ok,onClick:function(){
_6(dlg,true);
}},{text:_1a.cancel,onClick:function(){
_6(dlg,false);
}}];
}
var dlg=_d(_1a);
return dlg;
},prompt:function(_1b,msg,fn){
var _1c=typeof _1b=="object"?_1b:{title:_1b,msg:msg,fn:fn};
_1c=$.extend({},$.messager.defaults,{content:"<div class=\"messager-icon messager-question\"></div>"+"<div>"+_1c.msg+"</div>"+"<br>"+"<div style=\"clear:both;\"></div>"+"<div><input class=\"messager-input\" type=\"text\"></div>"},_1c);
if(!_1c.buttons){
_1c.buttons=[{text:_1c.ok,onClick:function(){
_6(dlg,dlg.find(".messager-input").val());
}},{text:_1c.cancel,onClick:function(){
_6(dlg);
}}];
}
var dlg=_d(_1c);
dlg.find(".messager-input").focus();
return dlg;
},progress:function(_1d){
var _1e={bar:function(){
return $("body>div.messager-window").find("div.messager-p-bar");
},close:function(){
var dlg=$("body>div.messager-window>div.messager-body:has(div.messager-progress)");
if(dlg.length){
dlg.dialog("close");
}
}};
if(typeof _1d=="string"){
var _1f=_1e[_1d];
return _1f();
}
_1d=_1d||{};
var _20=$.extend({},{title:"",minHeight:0,content:undefined,msg:"",text:undefined,interval:300},_1d);
var dlg=_d($.extend({},$.messager.defaults,{content:"<div class=\"messager-progress\"><div class=\"messager-p-msg\">"+_20.msg+"</div><div class=\"messager-p-bar\"></div></div>",closable:false,doSize:false},_20,{onClose:function(){
if(this.timer){
clearInterval(this.timer);
}
if(_1d.onClose){
_1d.onClose.call(this);
}else{
$.messager.defaults.onClose.call(this);
}
}}));
var bar=dlg.find("div.messager-p-bar");
bar.progressbar({text:_20.text});
dlg.dialog("resize");
if(_20.interval){
dlg[0].timer=setInterval(function(){
var v=bar.progressbar("getValue");
v+=10;
if(v>100){
v=0;
}
bar.progressbar("setValue",v);
},_20.interval);
}
return dlg;
}};
$.messager.defaults=$.extend({},$.fn.dialog.defaults,{ok:"Ok",cancel:"Cancel",width:300,height:"auto",minHeight:150,modal:true,collapsible:false,minimizable:false,maximizable:false,resizable:false,fn:function(){
}});
})(jQuery);

