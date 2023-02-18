/**
 * EasyUI for jQuery 1.10.15
 * 
 * Copyright (c) 2009-2023 www.jeasyui.com. All rights reserved.
 *
 * Licensed under the freeware license: http://www.jeasyui.com/license_freeware.php
 * To use it on other terms please contact us: info@jeasyui.com
 *
 */
(function($){
$(function(){
$(document)._unbind(".menu")._bind("mousedown.menu",function(e){
var m=$(e.target).closest("div.menu,div.combo-p");
if(m.length){
return;
}
$("body>div.menu-top:visible").not(".menu-inline").menu("hide");
_1($("body>div.menu:visible").not(".menu-inline"));
});
});
function _2(_3){
var _4=$.data(_3,"menu").options;
$(_3).addClass("menu-top");
_4.inline?$(_3).addClass("menu-inline"):$(_3).appendTo("body");
$(_3)._bind("_resize",function(e,_5){
if($(this).hasClass("easyui-fluid")||_5){
$(_3).menu("resize",_3);
}
return false;
});
var _6=_7($(_3));
for(var i=0;i<_6.length;i++){
_b(_3,_6[i]);
}
function _7(_8){
var _9=[];
_8.addClass("menu");
_9.push(_8);
if(!_8.hasClass("menu-content")){
_8.children("div").each(function(){
var _a=$(this).children("div");
if(_a.length){
_a.appendTo("body");
this.submenu=_a;
var mm=_7(_a);
_9=_9.concat(mm);
}
});
}
return _9;
};
};
function _b(_c,_d){
var _e=$(_d).addClass("menu");
if(!_e.data("menu")){
_e.data("menu",{options:$.parser.parseOptions(_e[0],["width","height"])});
}
if(!_e.hasClass("menu-content")){
_e.children("div").each(function(){
_f(_c,this);
});
$("<div class=\"menu-line\"></div>").prependTo(_e);
}
_10(_c,_e);
if(!_e.hasClass("menu-inline")){
_e.hide();
}
_11(_c,_e);
};
function _f(_12,div,_13){
var _14=$(div);
var _15=$.extend({},$.parser.parseOptions(_14[0],["id","name","iconCls","href",{separator:"boolean"}]),{disabled:(_14.attr("disabled")?true:undefined),text:$.trim(_14.html()),onclick:_14[0].onclick},_13||{});
_15.onclick=_15.onclick||_15.handler||null;
_14.data("menuitem",{options:_15});
if(_15.separator){
_14.addClass("menu-sep");
}
if(!_14.hasClass("menu-sep")){
_14.addClass("menu-item");
_14.empty().append($("<div class=\"menu-text\"></div>").html(_15.text));
if(_15.iconCls){
$("<div class=\"menu-icon\"></div>").addClass(_15.iconCls).appendTo(_14);
}
if(_15.id){
_14.attr("id",_15.id);
}
if(_15.onclick){
if(typeof _15.onclick=="string"){
_14.attr("onclick",_15.onclick);
}else{
_14[0].onclick=eval(_15.onclick);
}
}
if(_15.disabled){
_16(_12,_14[0],true);
}
if(_14[0].submenu){
$("<div class=\"menu-rightarrow\"></div>").appendTo(_14);
}
}
};
function _10(_17,_18){
var _19=$.data(_17,"menu").options;
var _1a=_18.attr("style")||"";
var _1b=_18.is(":visible");
_18.css({display:"block",left:-10000,height:"auto",overflow:"hidden"});
_18.find(".menu-item").each(function(){
$(this)._outerHeight(_19.itemHeight);
$(this).find(".menu-text").css({height:(_19.itemHeight-2)+"px",lineHeight:(_19.itemHeight-2)+"px"});
});
_18.removeClass("menu-noline").addClass(_19.noline?"menu-noline":"");
var _1c=_18.data("menu").options;
var _1d=_1c.width;
var _1e=_1c.height;
if(isNaN(parseInt(_1d))){
_1d=0;
_18.find("div.menu-text").each(function(){
if(_1d<$(this).outerWidth()){
_1d=$(this).outerWidth();
}
});
_1d=_1d?_1d+40:"";
}
var _1f=_18.outerHeight();
if(isNaN(parseInt(_1e))){
_1e=_1f;
if(_18.hasClass("menu-top")&&_19.alignTo){
var at=$(_19.alignTo);
var h1=at.offset().top-$(document).scrollTop();
var h2=$(window)._outerHeight()+$(document).scrollTop()-at.offset().top-at._outerHeight();
_1e=Math.min(_1e,Math.max(h1,h2));
}else{
if(_1e>$(window)._outerHeight()){
_1e=$(window).height();
}
}
}
_18.attr("style",_1a);
_18.show();
_18._size($.extend({},_1c,{width:_1d,height:_1e,minWidth:_1c.minWidth||_19.minWidth,maxWidth:_1c.maxWidth||_19.maxWidth}));
_18.find(".easyui-fluid").triggerHandler("_resize",[true]);
_18.css("overflow",_18.outerHeight()<_1f?"auto":"hidden");
_18.children("div.menu-line")._outerHeight(_1f-2);
if(!_1b){
_18.hide();
}
};
function _11(_20,_21){
var _22=$.data(_20,"menu");
var _23=_22.options;
_21._unbind(".menu");
for(var _24 in _23.events){
_21._bind(_24+".menu",{target:_20},_23.events[_24]);
}
};
function _25(e){
var _26=e.data.target;
var _27=$.data(_26,"menu");
if(_27.timer){
clearTimeout(_27.timer);
_27.timer=null;
}
};
function _28(e){
var _29=e.data.target;
var _2a=$.data(_29,"menu");
if(_2a.options.hideOnUnhover){
_2a.timer=setTimeout(function(){
_2b(_29,$(_29).hasClass("menu-inline"));
},_2a.options.duration);
}
};
function _2c(e){
var _2d=e.data.target;
var _2e=$(e.target).closest(".menu-item");
if(_2e.length){
_2e.siblings().each(function(){
if(this.submenu){
_1(this.submenu);
}
$(this).removeClass("menu-active");
});
_2e.addClass("menu-active");
if(_2e.hasClass("menu-item-disabled")){
_2e.addClass("menu-active-disabled");
return;
}
var _2f=_2e[0].submenu;
if(_2f){
$(_2d).menu("show",{menu:_2f,parent:_2e});
}
}
};
function _30(e){
var _31=$(e.target).closest(".menu-item");
if(_31.length){
_31.removeClass("menu-active menu-active-disabled");
var _32=_31[0].submenu;
if(_32){
if(e.pageX>=parseInt(_32.css("left"))){
_31.addClass("menu-active");
}else{
_1(_32);
}
}else{
_31.removeClass("menu-active");
}
}
};
function _33(e){
var _34=e.data.target;
var _35=$(e.target).closest(".menu-item");
if(_35.length){
var _36=$(_34).data("menu").options;
var _37=_35.data("menuitem").options;
if(_37.disabled){
return;
}
if(!_35[0].submenu){
_2b(_34,_36.inline);
if(_37.href){
location.href=_37.href;
}
}
_35.trigger("mouseenter");
_36.onClick.call(_34,$(_34).menu("getItem",_35[0]));
}
};
function _2b(_38,_39){
var _3a=$.data(_38,"menu");
if(_3a){
if($(_38).is(":visible")){
_1($(_38));
if(_39){
$(_38).show();
}else{
_3a.options.onHide.call(_38);
}
}
}
return false;
};
function _3b(_3c,_3d){
_3d=_3d||{};
var _3e,top;
var _3f=$.data(_3c,"menu").options;
var _40=$(_3d.menu||_3c);
$(_3c).menu("resize",_40[0]);
if(_40.hasClass("menu-top")){
$.extend(_3f,_3d);
_3e=_3f.left;
top=_3f.top;
if(_3f.alignTo){
var at=$(_3f.alignTo);
_3e=at.offset().left;
top=at.offset().top+at._outerHeight();
if(_3f.align=="right"){
_3e+=at.outerWidth()-_40.outerWidth();
}
}
if(_3e+_40.outerWidth()>$(window)._outerWidth()+$(document)._scrollLeft()){
_3e=$(window)._outerWidth()+$(document).scrollLeft()-_40.outerWidth()-5;
}
if(_3e<0){
_3e=0;
}
top=_41(top,_3f.alignTo);
}else{
var _42=_3d.parent;
_3e=_42.offset().left+_42.outerWidth()-2;
if(_3e+_40.outerWidth()+5>$(window)._outerWidth()+$(document).scrollLeft()){
_3e=_42.offset().left-_40.outerWidth()+2;
}
top=_41(_42.offset().top-3);
}
function _41(top,_43){
if(top+_40.outerHeight()>$(window)._outerHeight()+$(document).scrollTop()){
if(_43){
top=$(_43).offset().top-_40._outerHeight();
}else{
top=$(window)._outerHeight()+$(document).scrollTop()-_40.outerHeight();
}
}
if(top<0){
top=0;
}
return top;
};
_40.css(_3f.position.call(_3c,_40[0],_3e,top));
_40.show(0,function(){
if(!_40[0].shadow){
_40[0].shadow=$("<div class=\"menu-shadow\"></div>").insertAfter(_40);
}
_40[0].shadow.css({display:(_40.hasClass("menu-inline")?"none":"block"),zIndex:$.fn.menu.defaults.zIndex++,left:_40.css("left"),top:_40.css("top"),width:_40.outerWidth(),height:_40.outerHeight()});
_40.css("z-index",$.fn.menu.defaults.zIndex++);
if(_40.hasClass("menu-top")){
_3f.onShow.call(_3c);
}
});
};
function _1(_44){
if(_44&&_44.length){
_45(_44);
_44.find("div.menu-item").each(function(){
if(this.submenu){
_1(this.submenu);
}
$(this).removeClass("menu-active");
});
}
function _45(m){
m.stop(true,true);
if(m[0].shadow){
m[0].shadow.hide();
}
m.hide();
};
};
function _46(_47,_48){
var fn=$.isFunction(_48)?_48:function(_49){
for(var p in _48){
if(_49[p]!=_48[p]){
return false;
}
}
return true;
};
var _4a=[];
_4c(_47,function(_4b){
if(fn.call(_47,_4b)==true){
_4a.push(_4b);
}
});
return _4a;
};
function _4c(_4d,cb){
var _4e=false;
function nav(_4f){
_4f.children("div.menu-item").each(function(){
if(_4e){
return;
}
var _50=$(_4d).menu("getItem",this);
if(cb.call(_4d,_50)==false){
_4e=true;
}
if(this.submenu&&!_4e){
nav(this.submenu);
}
});
};
nav($(_4d));
};
function _16(_51,_52,_53){
var t=$(_52);
if(t.hasClass("menu-item")){
var _54=t.data("menuitem").options;
_54.disabled=_53;
if(_53){
t.addClass("menu-item-disabled");
t[0].onclick=null;
}else{
t.removeClass("menu-item-disabled");
t[0].onclick=_54.onclick;
}
}
};
function _55(_56,_57){
var _58=$.data(_56,"menu").options;
var _59=$(_56);
if(_57.parent){
if(!_57.parent.submenu){
var _5a=$("<div></div>").appendTo("body");
_57.parent.submenu=_5a;
$("<div class=\"menu-rightarrow\"></div>").appendTo(_57.parent);
_b(_56,_5a);
}
_59=_57.parent.submenu;
}
var div=$("<div></div>").appendTo(_59);
_f(_56,div,_57);
};
function _5b(_5c,_5d){
function _5e(el){
if(el.submenu){
el.submenu.children("div.menu-item").each(function(){
_5e(this);
});
var _5f=el.submenu[0].shadow;
if(_5f){
_5f.remove();
}
el.submenu.remove();
}
$(el).remove();
};
_5e(_5d);
};
function _60(_61,_62,_63){
var _64=$(_62).parent();
if(_63){
$(_62).show();
}else{
$(_62).hide();
}
_10(_61,_64);
};
function _65(_66){
$(_66).children("div.menu-item").each(function(){
_5b(_66,this);
});
if(_66.shadow){
_66.shadow.remove();
}
$(_66).remove();
};
$.fn.menu=function(_67,_68){
if(typeof _67=="string"){
return $.fn.menu.methods[_67](this,_68);
}
_67=_67||{};
return this.each(function(){
var _69=$.data(this,"menu");
if(_69){
$.extend(_69.options,_67);
}else{
_69=$.data(this,"menu",{options:$.extend({},$.fn.menu.defaults,$.fn.menu.parseOptions(this),_67)});
_2(this);
}
$(this).css({left:_69.options.left,top:_69.options.top});
});
};
$.fn.menu.methods={options:function(jq){
return $.data(jq[0],"menu").options;
},show:function(jq,pos){
return jq.each(function(){
_3b(this,pos);
});
},hide:function(jq){
return jq.each(function(){
_2b(this);
});
},destroy:function(jq){
return jq.each(function(){
_65(this);
});
},setText:function(jq,_6a){
return jq.each(function(){
var _6b=$(_6a.target).data("menuitem").options;
_6b.text=_6a.text;
$(_6a.target).children("div.menu-text").html(_6a.text);
});
},setIcon:function(jq,_6c){
return jq.each(function(){
var _6d=$(_6c.target).data("menuitem").options;
_6d.iconCls=_6c.iconCls;
$(_6c.target).children("div.menu-icon").remove();
if(_6c.iconCls){
$("<div class=\"menu-icon\"></div>").addClass(_6c.iconCls).appendTo(_6c.target);
}
});
},getItem:function(jq,_6e){
var _6f=$(_6e).data("menuitem").options;
return $.extend({},_6f,{target:$(_6e)[0]});
},findItem:function(jq,_70){
var _71=jq.menu("findItems",_70);
return _71.length?_71[0]:null;
},findItems:function(jq,_72){
if(typeof _72=="string"){
return _46(jq[0],function(_73){
return $("<div>"+_73.text+"</div>").text()==_72;
});
}else{
return _46(jq[0],_72);
}
},navItems:function(jq,cb){
return jq.each(function(){
_4c(this,cb);
});
},appendItem:function(jq,_74){
return jq.each(function(){
_55(this,_74);
});
},removeItem:function(jq,_75){
return jq.each(function(){
_5b(this,_75);
});
},enableItem:function(jq,_76){
return jq.each(function(){
_16(this,_76,false);
});
},disableItem:function(jq,_77){
return jq.each(function(){
_16(this,_77,true);
});
},showItem:function(jq,_78){
return jq.each(function(){
_60(this,_78,true);
});
},hideItem:function(jq,_79){
return jq.each(function(){
_60(this,_79,false);
});
},resize:function(jq,_7a){
return jq.each(function(){
_10(this,_7a?$(_7a):$(this));
});
}};
$.fn.menu.parseOptions=function(_7b){
return $.extend({},$.parser.parseOptions(_7b,[{minWidth:"number",itemHeight:"number",duration:"number",hideOnUnhover:"boolean"},{fit:"boolean",inline:"boolean",noline:"boolean"}]));
};
$.fn.menu.defaults={zIndex:110000,left:0,top:0,alignTo:null,align:"left",minWidth:150,itemHeight:32,duration:100,hideOnUnhover:true,inline:false,fit:false,noline:false,events:{mouseenter:_25,mouseleave:_28,mouseover:_2c,mouseout:_30,click:_33},position:function(_7c,_7d,top){
return {left:_7d,top:top};
},onShow:function(){
},onHide:function(){
},onClick:function(_7e){
}};
})(jQuery);

