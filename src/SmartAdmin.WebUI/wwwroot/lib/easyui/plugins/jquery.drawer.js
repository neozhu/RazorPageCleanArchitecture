/**
 * EasyUI for jQuery 1.10.0
 * 
 * Copyright (c) 2009-2021 www.jeasyui.com. All rights reserved.
 *
 * Licensed under the freeware license: http://www.jeasyui.com/license_freeware.php
 * To use it on other terms please contact us: info@jeasyui.com
 *
 */
(function($){
function _1(_2){
var _3=$.data(_2,"drawer").options;
$(_2).dialog($.extend({},_3,{cls:"drawer f-column window-shadow layout-panel layout-collapsed layout-panel-"+_3.region,bodyCls:"f-full",collapsed:false,top:0,left:"auto",right:"auto"}));
$(_2).dialog("header").find(".panel-tool-collapse").addClass("layout-button-"+(_3.region=="east"?"right":"left"))._unbind()._bind("click",function(){
_6(_2);
});
var _4=$(_2).dialog("dialog").width();
$(_2).dialog("dialog").css({display:"",left:_3.region=="east"?"auto":-_4,right:_3.region=="east"?-_4:"auto"});
var _5=$(_2).data("window").mask;
$(_5).addClass("drawer-mask").hide()._unbind()._bind("click",function(){
_6(_2);
});
};
function _7(_8){
var _9=$.data(_8,"drawer").options;
var _a=$(_8).dialog("dialog").width();
var _b=$(_8).data("window").mask;
$(_b).show();
$(_8).show().css({display:""}).dialog("dialog").animate({left:_9.region=="east"?"auto":0,right:_9.region=="east"?0:"auto"},function(){
$(this).removeClass("layout-collapsed");
_9.collapsed=false;
_9.onExpand.call(_8);
});
};
function _6(_c){
var _d=$.data(_c,"drawer").options;
var _e=$(_c).dialog("dialog").width();
$(_c).show().css({display:""}).dialog("dialog").animate({left:_d.region=="east"?"auto":-_e,right:_d.region=="east"?-_e:"auto"},function(){
$(this).addClass("layout-collapsed");
var _f=$(_c).data("window").mask;
$(_f).hide();
_d.collapsed=true;
_d.onCollapse.call(this);
});
};
$.fn.drawer=function(_10,_11){
if(typeof _10=="string"){
var _12=$.fn.drawer.methods[_10];
if(_12){
return _12(this,_11);
}else{
return this.dialog(_10,_11);
}
}
_10=_10||{};
this.each(function(){
var _13=$.data(this,"drawer");
if(_13){
$.extend(_13.options,_10);
}else{
var _14=$.extend({},$.fn.drawer.defaults,$.fn.drawer.parseOptions(this),_10);
$.data(this,"drawer",{options:_14});
}
_1(this);
});
};
$.fn.drawer.methods={options:function(jq){
var _15=$.data(jq[0],"drawer").options;
return $.extend(jq.dialog("options"),{region:_15.region,collapsed:_15.collapsed});
},expand:function(jq){
return jq.each(function(){
_7(this);
});
},collapse:function(jq){
return jq.each(function(){
_6(this);
});
}};
$.fn.drawer.parseOptions=function(_16){
return $.extend({},$.fn.dialog.parseOptions(_16),$.parser.parseOptions(_16,["region"]));
};
$.fn.drawer.defaults=$.extend({},$.fn.dialog.defaults,{border:false,region:"east",title:null,shadow:false,fixed:true,collapsed:true,closable:false,modal:true,draggable:false});
})(jQuery);

