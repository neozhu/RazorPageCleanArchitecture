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
var _4=$.data(_3,"radiogroup");
var _5=_4.options;
$(_3).addClass("radiogroup").empty();
var c=$("<div></div>").appendTo(_3);
if(_5.dir=="h"){
c.addClass("f-row");
c.css("flex-wrap","wrap");
}else{
c.addClass("f-column");
}
var _6=_5.name||("radioname"+_1++);
for(var i=0;i<_5.data.length;i++){
var _7=$("<div class=\"radiogroup-item f-row f-vcenter f-noshrink\"></div>").appendTo(c);
if(_5.itemStyle){
_7.css(_5.itemStyle);
}
var rb=$("<input>").attr("name",_6).appendTo(_7);
rb.radiobutton($.extend({},{labelWidth:_5.labelWidth,labelPosition:_5.labelPosition,labelAlign:_5.labelAlign},_5.data[i],{checked:_5.data[i].value==_5.value,item:_5.data[i],onChange:function(){
c.find(".radiobutton-f").each(function(){
var _8=$(this).radiobutton("options");
if(_8.checked){
_5.value=_8.item.value;
_5.onChange.call(_3,_8.item.value);
}
});
}}));
var _4=rb.data("radiobutton");
if(_4.options.labelWidth=="auto"){
$(_4.label).css("width","auto");
}
}
};
function _9(_a,_b){
$(_a).find(".radiobutton-f").each(function(){
var _c=$(this).radiobutton("options");
if(_c.item.value==_b){
$(this).radiobutton("check");
}
});
};
$.fn.radiogroup=function(_d,_e){
if(typeof _d=="string"){
return $.fn.radiogroup.methods[_d](this,_e);
}
_d=_d||{};
return this.each(function(){
var _f=$.data(this,"radiogroup");
if(_f){
$.extend(_f.options,_d);
}else{
_f=$.data(this,"radiogroup",{options:$.extend({},$.fn.radiogroup.defaults,$.fn.radiogroup.parseOptions(this),_d)});
}
_2(this);
});
};
$.fn.radiogroup.methods={options:function(jq){
return jq.data("radiogroup").options;
},setValue:function(jq,_10){
return jq.each(function(){
_9(this,_10);
});
},getValue:function(jq){
return jq.radiogroup("options").value;
}};
$.fn.radiogroup.parseOptions=function(_11){
return $.extend({},$.parser.parseOptions(_11,["dir","name","value","labelPosition","labelAlign",{labelWidth:"number"}]));
};
$.fn.radiogroup.defaults={dir:"h",name:null,value:null,labelWidth:"",labelPosition:"after",labelAlign:"left",itemStyle:{height:30},onChange:function(_12){
}};
})(jQuery);

