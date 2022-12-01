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
function _1(_2,_3){
var _4=$.data(_2,"window");
if(_3){
if(_3.left!=null){
_4.options.left=_3.left;
}
if(_3.top!=null){
_4.options.top=_3.top;
}
}
$(_2).panel("move",_4.options);
if(_4.shadow){
_4.shadow.css({left:_4.options.left,top:_4.options.top});
}
};
function _5(_6,_7){
var _8=$.data(_6,"window").options;
var pp=$(_6).window("panel");
var _9=pp._outerWidth();
if(_8.inline){
var _a=pp.parent();
_8.left=Math.ceil((_a.width()-_9)/2+_a.scrollLeft());
}else{
var _b=_8.fixed?0:$(document).scrollLeft();
_8.left=Math.ceil(($(window)._outerWidth()-_9)/2+_b);
}
if(_7){
_1(_6);
}
};
function _c(_d,_e){
var _f=$.data(_d,"window").options;
var pp=$(_d).window("panel");
var _10=pp._outerHeight();
if(_f.inline){
var _11=pp.parent();
_f.top=Math.ceil((_11.height()-_10)/2+_11.scrollTop());
}else{
var _12=_f.fixed?0:$(document).scrollTop();
_f.top=Math.ceil(($(window)._outerHeight()-_10)/2+_12);
}
if(_e){
_1(_d);
}
};
function _13(_14){
var _15=$.data(_14,"window");
var _16=_15.options;
var win=$(_14).panel($.extend({},_15.options,{border:false,hasResized:false,doSize:true,closed:true,cls:"window "+(!_16.border?"window-thinborder window-noborder ":(_16.border=="thin"?"window-thinborder ":""))+(_16.cls||""),headerCls:"window-header "+(_16.headerCls||""),bodyCls:"window-body "+(_16.noheader?"window-body-noheader ":" ")+(_16.bodyCls||""),onBeforeDestroy:function(){
if(_16.onBeforeDestroy.call(_14)==false){
return false;
}
if(_15.shadow){
_15.shadow.remove();
}
if(_15.mask){
_15.mask.remove();
}
},onClose:function(){
if(_15.shadow){
_15.shadow.hide();
}
if(_15.mask){
_15.mask.hide();
}
_16.onClose.call(_14);
},onOpen:function(){
if(_15.mask){
_15.mask.css($.extend({display:"block",zIndex:$.fn.window.defaults.zIndex++},$.fn.window.getMaskSize(_14)));
}
if(_15.shadow){
_15.shadow.css({display:"block",position:(_16.fixed?"fixed":"absolute"),zIndex:$.fn.window.defaults.zIndex++,left:_16.left,top:_16.top,width:_15.window._outerWidth(),height:_15.window._outerHeight()});
}
_15.window.css({position:(_16.fixed?"fixed":"absolute"),zIndex:$.fn.window.defaults.zIndex++});
_16.onOpen.call(_14);
},onResize:function(_17,_18){
var _19=$(this).panel("options");
$.extend(_16,{width:_19.width,height:_19.height,left:_19.left,top:_19.top});
if(_15.shadow){
_15.shadow.css({left:_16.left,top:_16.top,width:_15.window._outerWidth(),height:_15.window._outerHeight()});
}
_16.onResize.call(_14,_17,_18);
},onMinimize:function(){
if(_15.shadow){
_15.shadow.hide();
}
if(_15.mask){
_15.mask.hide();
}
_15.options.onMinimize.call(_14);
},onBeforeCollapse:function(){
if(_16.onBeforeCollapse.call(_14)==false){
return false;
}
if(_15.shadow){
_15.shadow.hide();
}
},onExpand:function(){
if(_15.shadow){
_15.shadow.show();
}
_16.onExpand.call(_14);
}}));
_15.window=win.panel("panel");
if(_15.mask){
_15.mask.remove();
}
if(_16.modal){
_15.mask=$("<div class=\"window-mask\" style=\"display:none\"></div>").insertAfter(_15.window);
}
if(_15.shadow){
_15.shadow.remove();
}
if(_16.shadow){
_15.shadow=$("<div class=\"window-shadow\" style=\"display:none\"></div>").insertAfter(_15.window);
}
var _1a=_16.closed;
if(_16.left==null){
_5(_14);
}
if(_16.top==null){
_c(_14);
}
_1(_14);
if(!_1a){
win.window("open");
}
};
function _1b(_1c,top,_1d,_1e){
var _1f=this;
var _20=$.data(_1f,"window");
var _21=_20.options;
if(!_21.constrain){
return {};
}
if($.isFunction(_21.constrain)){
return _21.constrain.call(_1f,_1c,top,_1d,_1e);
}
var win=$(_1f).window("window");
var _22=_21.inline?win.parent():$(window);
var _23=_21.fixed?0:_22.scrollTop();
if(_1c<0){
_1c=0;
}
if(top<_23){
top=_23;
}
if(_1c+_1d>_22.width()){
if(_1d==win.outerWidth()){
_1c=_22.width()-_1d;
}else{
_1d=_22.width()-_1c;
}
}
if(top-_23+_1e>_22.height()){
if(_1e==win.outerHeight()){
top=_22.height()-_1e+_23;
}else{
_1e=_22.height()-top+_23;
}
}
return {left:_1c,top:top,width:_1d,height:_1e};
};
function _24(_25){
var _26=$.data(_25,"window");
var _27=_26.options;
_26.window.draggable({handle:">.panel-header>.panel-title",disabled:_26.options.draggable==false,onBeforeDrag:function(e){
if(_26.mask){
_26.mask.css("z-index",$.fn.window.defaults.zIndex++);
}
if(_26.shadow){
_26.shadow.css("z-index",$.fn.window.defaults.zIndex++);
}
_26.window.css("z-index",$.fn.window.defaults.zIndex++);
},onStartDrag:function(e){
_28(e);
},onDrag:function(e){
_29(e);
return false;
},onStopDrag:function(e){
_2a(e,"move");
}});
_26.window.resizable({disabled:_26.options.resizable==false,onStartResize:function(e){
_28(e);
},onResize:function(e){
_29(e);
return false;
},onStopResize:function(e){
_2a(e,"resize");
}});
function _28(e){
_26.window.css("position",_27.fixed?"fixed":"absolute");
if(_26.shadow){
_26.shadow.css("position",_27.fixed?"fixed":"absolute");
}
if(_26.pmask){
_26.pmask.remove();
}
_26.pmask=$("<div class=\"window-proxy-mask\"></div>").insertAfter(_26.window);
_26.pmask.css({display:"none",position:(_27.fixed?"fixed":"absolute"),zIndex:$.fn.window.defaults.zIndex++,left:e.data.left,top:e.data.top,width:_26.window._outerWidth(),height:_26.window._outerHeight()});
if(_26.proxy){
_26.proxy.remove();
}
_26.proxy=$("<div class=\"window-proxy\"></div>").insertAfter(_26.window);
_26.proxy.css({display:"none",position:(_27.fixed?"fixed":"absolute"),zIndex:$.fn.window.defaults.zIndex++,left:e.data.left,top:e.data.top});
_26.proxy._outerWidth(e.data.width)._outerHeight(e.data.height);
_26.proxy.hide();
setTimeout(function(){
if(_26.pmask){
_26.pmask.show();
}
if(_26.proxy){
_26.proxy.show();
}
},500);
};
function _29(e){
$.extend(e.data,_1b.call(_25,e.data.left,e.data.top,e.data.width,e.data.height));
_26.pmask.show();
_26.proxy.css({display:"block",left:e.data.left,top:e.data.top});
_26.proxy._outerWidth(e.data.width);
_26.proxy._outerHeight(e.data.height);
};
function _2a(e,_2b){
_26.window.css("position",_27.fixed?"fixed":"absolute");
if(_26.shadow){
_26.shadow.css("position",_27.fixed?"fixed":"absolute");
}
$.extend(e.data,_1b.call(_25,e.data.left,e.data.top,e.data.width+0.1,e.data.height+0.1));
$(_25).window(_2b,e.data);
_26.pmask.remove();
_26.pmask=null;
_26.proxy.remove();
_26.proxy=null;
};
};
$(function(){
if(!$._positionFixed){
$(window).resize(function(){
$("body>.window-mask:visible").css({width:"",height:""});
setTimeout(function(){
$("body>.window-mask:visible").css($.fn.window.getMaskSize());
},50);
});
}
});
$.fn.window=function(_2c,_2d){
if(typeof _2c=="string"){
var _2e=$.fn.window.methods[_2c];
if(_2e){
return _2e(this,_2d);
}else{
return this.panel(_2c,_2d);
}
}
_2c=_2c||{};
return this.each(function(){
var _2f=$.data(this,"window");
if(_2f){
$.extend(_2f.options,_2c);
}else{
_2f=$.data(this,"window",{options:$.extend({},$.fn.window.defaults,$.fn.window.parseOptions(this),_2c)});
if(!_2f.options.inline){
document.body.appendChild(this);
}
}
_13(this);
_24(this);
});
};
$.fn.window.methods={options:function(jq){
var _30=jq.panel("options");
var _31=$.data(jq[0],"window").options;
return $.extend(_31,{closed:_30.closed,collapsed:_30.collapsed,minimized:_30.minimized,maximized:_30.maximized});
},window:function(jq){
return $.data(jq[0],"window").window;
},move:function(jq,_32){
return jq.each(function(){
_1(this,_32);
});
},hcenter:function(jq){
return jq.each(function(){
_5(this,true);
});
},vcenter:function(jq){
return jq.each(function(){
_c(this,true);
});
},center:function(jq){
return jq.each(function(){
_5(this);
_c(this);
_1(this);
});
}};
$.fn.window.getMaskSize=function(_33){
var _34=$(_33).data("window");
if(_34&&_34.options.inline){
return {};
}else{
if($._positionFixed){
return {position:"fixed"};
}else{
return {width:$(document).width(),height:$(document).height()};
}
}
};
$.fn.window.parseOptions=function(_35){
return $.extend({},$.fn.panel.parseOptions(_35),$.parser.parseOptions(_35,[{draggable:"boolean",resizable:"boolean",shadow:"boolean",modal:"boolean",inline:"boolean"}]));
};
$.fn.window.defaults=$.extend({},$.fn.panel.defaults,{zIndex:9000,draggable:true,resizable:true,shadow:true,modal:false,border:true,inline:false,title:"New Window",collapsible:true,minimizable:true,maximizable:true,closable:true,closed:false,fixed:false,constrain:false});
})(jQuery);

