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
var _1=1;
function _2(_3){
var _4=$.data(_3,"checkgroup");
var _5=_4.options;
$(_3).addClass("checkgroup").empty();
var c=$("<div></div>").appendTo(_3);
if(_5.dir=="h"){
c.addClass("f-row");
c.css("flex-wrap","wrap");
}else{
c.addClass("f-column");
}
var _6=_5.name||("checkname"+_1++);
for(var i=0;i<_5.data.length;i++){
var _7=$("<div class=\"checkgroup-item f-row f-vcenter f-noshrink\"></div>").appendTo(c);
if(_5.itemStyle){
_7.css(_5.itemStyle);
}
var ck=$("<input>").attr("name",_6).appendTo(_7);
ck.checkbox($.extend({},{labelWidth:_5.labelWidth,labelPosition:_5.labelPosition,labelAlign:_5.labelAlign},_5.data[i],{checked:$.inArray(_5.data[i].value,_5.value)>=0,item:_5.data[i],onChange:function(){
var vv=[];
c.find(".checkbox-f").each(function(){
var _8=$(this).checkbox("options");
if(_8.checked){
vv.push(_8.item.value);
}
});
_5.value=vv;
_5.onChange.call(_3,vv);
}}));
var _4=ck.data("checkbox");
if(_4.options.labelWidth=="auto"){
$(_4.label).css("width","auto");
}
}
};
function _9(_a,_b){
var _c=$.data(_a,"checkgroup");
var _d=_c.options;
var _e=_d.onChange;
_d.onChange=function(){
};
var _f=$.extend([],_d.value).sort().join(",");
$(_a).find(".checkbox-f").each(function(){
var _10=$(this).checkbox("options");
if($.inArray(_10.item.value,_b)>=0){
$(this).checkbox("check");
}else{
$(this).checkbox("uncheck");
}
});
_d.onChange=_e;
var _11=$.extend([],_d.value).sort().join(",");
if(_11!=_f){
_d.onChange.call(_a,_d.value);
}
};
$.fn.checkgroup=function(_12,_13){
if(typeof _12=="string"){
return $.fn.checkgroup.methods[_12](this,_13);
}
_12=_12||{};
return this.each(function(){
var _14=$.data(this,"checkgroup");
if(_14){
$.extend(_14.options,_12);
}else{
_14=$.data(this,"checkgroup",{options:$.extend({},$.fn.checkgroup.defaults,$.fn.checkgroup.parseOptions(this),_12)});
}
_2(this);
});
};
$.fn.checkgroup.methods={options:function(jq){
return jq.data("checkgroup").options;
},setValue:function(jq,_15){
return jq.each(function(){
_9(this,_15);
});
},getValue:function(jq){
return jq.checkgroup("options").value;
}};
$.fn.checkgroup.parseOptions=function(_16){
return $.extend({},$.parser.parseOptions(_16,["dir","name","value","labelPosition","labelAlign",{labelWidth:"number"}]));
};
$.fn.checkgroup.defaults={dir:"h",name:null,value:[],labelWidth:"",labelPosition:"after",labelAlign:"left",itemStyle:{height:30},onChange:function(_17){
}};
})(jQuery);

