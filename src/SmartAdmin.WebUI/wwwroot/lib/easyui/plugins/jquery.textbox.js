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
var _1=0;
function _2(_3){
$(_3).addClass("textbox-f").hide();
var _4=$("<span class=\"textbox\">"+"<input class=\"textbox-text\" autocomplete=\"off\">"+"<input type=\"hidden\" class=\"textbox-value\">"+"</span>").insertAfter(_3);
var _5=$(_3).attr("name");
if(_5){
_4.find("input.textbox-value").attr("name",_5);
$(_3).removeAttr("name").attr("textboxName",_5);
}
return _4;
};
function _6(_7){
var _8=$.data(_7,"textbox");
var _9=_8.options;
var tb=_8.textbox;
var _a="_easyui_textbox_input"+(++_1);
tb.addClass(_9.cls);
tb.find(".textbox-text").remove();
if(_9.multiline){
$("<textarea id=\""+_a+"\" class=\"textbox-text\" autocomplete=\"off\"></textarea>").prependTo(tb);
}else{
$("<input id=\""+_a+"\" type=\""+_9.type+"\" class=\"textbox-text\" autocomplete=\"off\">").prependTo(tb);
}
$("#"+_a).attr("tabindex",$(_7).attr("tabindex")||"").css("text-align",_7.style.textAlign||"");
tb.find(".textbox-addon").remove();
var bb=_9.icons?$.extend(true,[],_9.icons):[];
if(_9.iconCls){
bb.push({iconCls:_9.iconCls,disabled:true});
}
if(bb.length){
var bc=$("<span class=\"textbox-addon\"></span>").prependTo(tb);
bc.addClass("textbox-addon-"+_9.iconAlign);
for(var i=0;i<bb.length;i++){
bc.append("<a href=\"javascript:;\" class=\"textbox-icon "+bb[i].iconCls+"\" icon-index=\""+i+"\" tabindex=\"-1\"></a>");
}
}
tb.find(".textbox-button").remove();
if(_9.buttonText||_9.buttonIcon){
var _b=$("<a href=\"javascript:;\" class=\"textbox-button\"></a>").prependTo(tb);
_b.addClass("textbox-button-"+_9.buttonAlign).linkbutton({text:_9.buttonText,iconCls:_9.buttonIcon,onClick:function(){
var t=$(this).parent().prev();
t.textbox("options").onClickButton.call(t[0]);
}});
}
if(_9.label){
if(typeof _9.label=="object"){
_8.label=$(_9.label);
_8.label.attr("for",_a);
}else{
$(_8.label).remove();
_8.label=$("<label class=\"textbox-label\"></label>").html(_9.label);
_8.label.css("textAlign",_9.labelAlign).attr("for",_a);
if(_9.labelPosition=="after"){
_8.label.insertAfter(tb);
}else{
_8.label.insertBefore(_7);
}
_8.label.removeClass("textbox-label-left textbox-label-right textbox-label-top");
_8.label.addClass("textbox-label-"+_9.labelPosition);
}
}else{
$(_8.label).remove();
}
_c(_7);
_d(_7,_9.disabled);
_e(_7,_9.readonly);
};
function _f(_10){
var _11=$.data(_10,"textbox");
var tb=_11.textbox;
tb.find(".textbox-text").validatebox("destroy");
tb.remove();
$(_11.label).remove();
$(_10).remove();
};
function _12(_13,_14){
var _15=$.data(_13,"textbox");
var _16=_15.options;
var tb=_15.textbox;
var _17=tb.parent();
if(_14){
if(typeof _14=="object"){
$.extend(_16,_14);
}else{
_16.width=_14;
}
}
if(isNaN(parseInt(_16.width))){
var c=$(_13).clone();
c.css("visibility","hidden");
c.insertAfter(_13);
_16.width=c.outerWidth();
c.remove();
}
var _18=tb.is(":visible");
if(!_18){
tb.appendTo("body");
}
var _19=tb.find(".textbox-text");
var btn=tb.find(".textbox-button");
var _1a=tb.find(".textbox-addon");
var _1b=_1a.find(".textbox-icon");
if(_16.height=="auto"){
_19.css({margin:"",paddingTop:"",paddingBottom:"",height:"",lineHeight:""});
}
tb._size(_16,_17);
if(_16.label&&_16.labelPosition){
if(_16.labelPosition=="top"){
_15.label._size({width:_16.labelWidth=="auto"?tb.outerWidth():_16.labelWidth},tb);
if(_16.height!="auto"){
tb._size("height",tb.outerHeight()-_15.label.outerHeight());
}
}else{
_15.label._size({width:_16.labelWidth,height:tb.outerHeight()},tb);
if(!_16.multiline){
_15.label.css("lineHeight",_15.label.height()+"px");
}
tb._size("width",tb.outerWidth()-_15.label.outerWidth());
}
}
if(_16.buttonAlign=="left"||_16.buttonAlign=="right"){
btn.linkbutton("resize",{height:tb.height()});
}else{
btn.linkbutton("resize",{width:"100%"});
}
var _1c=tb.width()-_1b.length*_16.iconWidth-_1d("left")-_1d("right");
var _1e=_16.height=="auto"?_19.outerHeight():(tb.height()-_1d("top")-_1d("bottom"));
_1a.css(_16.iconAlign,_1d(_16.iconAlign)+"px");
_1a.css("top",_1d("top")+"px");
_1b.css({width:_16.iconWidth+"px",height:_1e+"px"});
_19.css({paddingLeft:(_13.style.paddingLeft||""),paddingRight:(_13.style.paddingRight||""),marginLeft:_1f("left"),marginRight:_1f("right"),marginTop:_1d("top"),marginBottom:_1d("bottom")});
if(_16.multiline){
_19.css({paddingTop:(_13.style.paddingTop||""),paddingBottom:(_13.style.paddingBottom||"")});
_19._outerHeight(_1e);
}else{
_19.css({paddingTop:0,paddingBottom:0,height:_1e+"px",lineHeight:_1e+"px"});
}
_19._outerWidth(_1c);
_16.onResizing.call(_13,_16.width,_16.height);
if(!_18){
tb.insertAfter(_13);
}
_16.onResize.call(_13,_16.width,_16.height);
function _1f(_20){
return (_16.iconAlign==_20?_1a._outerWidth():0)+_1d(_20);
};
function _1d(_21){
var w=0;
btn.filter(".textbox-button-"+_21).each(function(){
if(_21=="left"||_21=="right"){
w+=$(this).outerWidth();
}else{
w+=$(this).outerHeight();
}
});
return w;
};
};
function _c(_22){
var _23=$(_22).textbox("options");
var _24=$(_22).textbox("textbox");
_24.validatebox($.extend({},_23,{deltaX:function(_25){
return $(_22).textbox("getTipX",_25);
},deltaY:function(_26){
return $(_22).textbox("getTipY",_26);
},onBeforeValidate:function(){
_23.onBeforeValidate.call(_22);
var box=$(this);
if(!box.is(":focus")){
if(box.val()!==_23.value){
_23.oldInputValue=box.val();
box.val(_23.value);
}
}
},onValidate:function(_27){
var box=$(this);
if(_23.oldInputValue!=undefined){
box.val(_23.oldInputValue);
_23.oldInputValue=undefined;
}
var tb=box.parent();
if(_27){
tb.removeClass("textbox-invalid");
}else{
tb.addClass("textbox-invalid");
}
_23.onValidate.call(_22,_27);
}}));
};
function _28(_29){
var _2a=$.data(_29,"textbox");
var _2b=_2a.options;
var tb=_2a.textbox;
var _2c=tb.find(".textbox-text");
_2c.attr("placeholder",_2b.prompt);
_2c._unbind(".textbox");
$(_2a.label)._unbind(".textbox");
if(!_2b.disabled&&!_2b.readonly){
if(_2a.label){
$(_2a.label)._bind("click.textbox",function(e){
if(!_2b.hasFocusMe){
_2c.focus();
$(_29).textbox("setSelectionRange",{start:0,end:_2c.val().length});
}
});
}
_2c._bind("blur.textbox",function(e){
if(!tb.hasClass("textbox-focused")){
return;
}
_2b.value=$(this).val();
if(_2b.value==""){
$(this).val(_2b.prompt).addClass("textbox-prompt");
}else{
$(this).removeClass("textbox-prompt");
}
tb.removeClass("textbox-focused");
tb.closest(".form-field").removeClass("form-field-focused");
})._bind("focus.textbox",function(e){
_2b.hasFocusMe=true;
if(tb.hasClass("textbox-focused")){
return;
}
if($(this).val()!=_2b.value){
$(this).val(_2b.value);
}
$(this).removeClass("textbox-prompt");
tb.addClass("textbox-focused");
tb.closest(".form-field").addClass("form-field-focused");
});
for(var _2d in _2b.inputEvents){
_2c._bind(_2d+".textbox",{target:_29},_2b.inputEvents[_2d]);
}
}
var _2e=tb.find(".textbox-addon");
_2e._unbind()._bind("click",{target:_29},function(e){
var _2f=$(e.target).closest("a.textbox-icon:not(.textbox-icon-disabled)");
if(_2f.length){
var _30=parseInt(_2f.attr("icon-index"));
var _31=_2b.icons[_30];
if(_31&&_31.handler){
_31.handler.call(_2f[0],e);
}
_2b.onClickIcon.call(_29,_30);
}
});
_2e.find(".textbox-icon").each(function(_32){
var _33=_2b.icons[_32];
var _34=$(this);
if(!_33||_33.disabled||_2b.disabled||_2b.readonly){
_34.addClass("textbox-icon-disabled");
}else{
_34.removeClass("textbox-icon-disabled");
}
});
var btn=tb.find(".textbox-button");
btn.linkbutton((_2b.disabled||_2b.readonly)?"disable":"enable");
tb._unbind(".textbox")._bind("_resize.textbox",function(e,_35){
if($(this).hasClass("easyui-fluid")||_35){
_12(_29);
}
return false;
});
};
function _d(_36,_37){
var _38=$.data(_36,"textbox");
var _39=_38.options;
var tb=_38.textbox;
var _3a=tb.find(".textbox-text");
var ss=$(_36).add(tb.find(".textbox-value"));
_39.disabled=_37;
if(_39.disabled){
_3a.blur();
_3a.validatebox("disable");
tb.addClass("textbox-disabled");
ss._propAttr("disabled",true);
$(_38.label).addClass("textbox-label-disabled");
}else{
_3a.validatebox("enable");
tb.removeClass("textbox-disabled");
ss._propAttr("disabled",false);
$(_38.label).removeClass("textbox-label-disabled");
}
};
function _e(_3b,_3c){
var _3d=$.data(_3b,"textbox");
var _3e=_3d.options;
var tb=_3d.textbox;
var _3f=tb.find(".textbox-text");
_3e.readonly=_3c==undefined?true:_3c;
if(_3e.readonly){
_3f.triggerHandler("blur.textbox");
}
_3f.validatebox("readonly",_3e.readonly);
if(_3e.readonly){
tb.addClass("textbox-readonly");
$(_3d.label).addClass("textbox-label-readonly");
}else{
tb.removeClass("textbox-readonly");
$(_3d.label).removeClass("textbox-label-readonly");
}
};
function _40(_41,_42){
var _43=$.data(_41,"textbox");
var _44=_43.options;
var tb=_43.textbox;
var _45=tb.find(".textbox-text");
_44.editable=_42==undefined?true:_42;
_45.validatebox("setEditable",_44.editable);
_e(_41,_44.readonly);
};
$.fn.textbox=function(_46,_47){
if(typeof _46=="string"){
var _48=$.fn.textbox.methods[_46];
if(_48){
return _48(this,_47);
}else{
return this.each(function(){
var _49=$(this).textbox("textbox");
_49.validatebox(_46,_47);
});
}
}
_46=_46||{};
return this.each(function(){
var _4a=$.data(this,"textbox");
if(_4a){
$.extend(_4a.options,_46);
if(_46.value!=undefined){
_4a.options.originalValue=_46.value;
}
}else{
_4a=$.data(this,"textbox",{options:$.extend({},$.fn.textbox.defaults,$.fn.textbox.parseOptions(this),_46),textbox:_2(this)});
_4a.options.originalValue=_4a.options.value;
}
_6(this);
_28(this);
if(_4a.options.doSize){
_12(this);
}
var _4b=_4a.options.value;
_4a.options.value="";
$(this).textbox("initValue",_4b);
});
};
$.fn.textbox.methods={options:function(jq){
return $.data(jq[0],"textbox").options;
},cloneFrom:function(jq,_4c){
return jq.each(function(){
var t=$(this);
if(t.data("textbox")){
return;
}
if(!$(_4c).data("textbox")){
$(_4c).textbox();
}
var _4d=$.extend(true,{},$(_4c).textbox("options"));
var _4e=t.attr("name")||"";
t.addClass("textbox-f").hide();
t.removeAttr("name").attr("textboxName",_4e);
var _4f=$(_4c).next().clone().insertAfter(t);
var _50="_easyui_textbox_input"+(++_1);
_4f.find(".textbox-value").attr("name",_4e);
_4f.find(".textbox-text").attr("id",_50);
var _51=$($(_4c).textbox("label")).clone();
if(_51.length){
_51.attr("for",_50);
if(_4d.labelPosition=="after"){
_51.insertAfter(t.next());
}else{
_51.insertBefore(t);
}
}
$.data(this,"textbox",{options:_4d,textbox:_4f,label:(_51.length?_51:undefined)});
var _52=$(_4c).textbox("button");
if(_52.length){
t.textbox("button").linkbutton($.extend(true,{},_52.linkbutton("options")));
}
_28(this);
_c(this);
});
},textbox:function(jq){
return $.data(jq[0],"textbox").textbox.find(".textbox-text");
},button:function(jq){
return $.data(jq[0],"textbox").textbox.find(".textbox-button");
},label:function(jq){
return $.data(jq[0],"textbox").label;
},destroy:function(jq){
return jq.each(function(){
_f(this);
});
},resize:function(jq,_53){
return jq.each(function(){
_12(this,_53);
});
},disable:function(jq){
return jq.each(function(){
_d(this,true);
_28(this);
});
},enable:function(jq){
return jq.each(function(){
_d(this,false);
_28(this);
});
},readonly:function(jq,_54){
return jq.each(function(){
_e(this,_54);
_28(this);
});
},setEditable:function(jq,_55){
return jq.each(function(){
_40(this,_55);
_28(this);
});
},isValid:function(jq){
return jq.textbox("textbox").validatebox("isValid");
},clear:function(jq){
return jq.each(function(){
$(this).textbox("setValue","");
});
},setText:function(jq,_56){
return jq.each(function(){
var _57=$(this).textbox("options");
var _58=$(this).textbox("textbox");
_56=_56==undefined?"":String(_56);
if($(this).textbox("getText")!=_56){
_58.val(_56);
}
_57.value=_56;
if(!_58.is(":focus")){
if(_56){
_58.removeClass("textbox-prompt");
}else{
_58.val(_57.prompt).addClass("textbox-prompt");
}
}
if(_57.value){
$(this).closest(".form-field").removeClass("form-field-empty");
}else{
$(this).closest(".form-field").addClass("form-field-empty");
}
$(this).textbox("validate");
});
},initValue:function(jq,_59){
return jq.each(function(){
var _5a=$.data(this,"textbox");
$(this).textbox("setText",_59);
_5a.textbox.find(".textbox-value").val(_59);
$(this).val(_59);
});
},setValue:function(jq,_5b){
return jq.each(function(){
var _5c=$.data(this,"textbox").options;
var _5d=$(this).textbox("getValue");
$(this).textbox("initValue",_5b);
if(_5d!=_5b){
_5c.onChange.call(this,_5b,_5d);
$(this).closest("form").trigger("_change",[this]);
}
});
},getText:function(jq){
var _5e=jq.textbox("textbox");
if(_5e.is(":focus")){
return _5e.val();
}else{
return jq.textbox("options").value;
}
},getValue:function(jq){
return jq.data("textbox").textbox.find(".textbox-value").val();
},reset:function(jq){
return jq.each(function(){
var _5f=$(this).textbox("options");
$(this).textbox("textbox").val(_5f.originalValue);
$(this).textbox("setValue",_5f.originalValue);
});
},getIcon:function(jq,_60){
return jq.data("textbox").textbox.find(".textbox-icon:eq("+_60+")");
},getTipX:function(jq,_61){
var _62=jq.data("textbox");
var _63=_62.options;
var tb=_62.textbox;
var _64=tb.find(".textbox-text");
var _61=_61||_63.tipPosition;
var p1=tb.offset();
var p2=_64.offset();
var w1=tb.outerWidth();
var w2=_64.outerWidth();
if(_61=="right"){
return w1-w2-p2.left+p1.left;
}else{
if(_61=="left"){
return p1.left-p2.left;
}else{
return (w1-w2-p2.left+p1.left)/2-(p2.left-p1.left)/2;
}
}
},getTipY:function(jq,_65){
var _66=jq.data("textbox");
var _67=_66.options;
var tb=_66.textbox;
var _68=tb.find(".textbox-text");
var _65=_65||_67.tipPosition;
var p1=tb.offset();
var p2=_68.offset();
var h1=tb.outerHeight();
var h2=_68.outerHeight();
if(_65=="left"||_65=="right"){
return (h1-h2-p2.top+p1.top)/2-(p2.top-p1.top)/2;
}else{
if(_65=="bottom"){
return (h1-h2-p2.top+p1.top);
}else{
return (p1.top-p2.top);
}
}
},getSelectionStart:function(jq){
return jq.textbox("getSelectionRange").start;
},getSelectionRange:function(jq){
var _69=jq.textbox("textbox")[0];
var _6a=0;
var end=0;
if(typeof _69.selectionStart=="number"){
_6a=_69.selectionStart;
end=_69.selectionEnd;
}else{
if(_69.createTextRange){
var s=document.selection.createRange();
var _6b=_69.createTextRange();
_6b.setEndPoint("EndToStart",s);
_6a=_6b.text.length;
end=_6a+s.text.length;
}
}
return {start:_6a,end:end};
},setSelectionRange:function(jq,_6c){
return jq.each(function(){
var _6d=$(this).textbox("textbox")[0];
var _6e=_6c.start;
var end=_6c.end;
if(_6d.setSelectionRange){
_6d.setSelectionRange(_6e,end);
}else{
if(_6d.createTextRange){
var _6f=_6d.createTextRange();
_6f.collapse();
_6f.moveEnd("character",end);
_6f.moveStart("character",_6e);
_6f.select();
}
}
});
},show:function(jq){
return jq.each(function(){
$(this).next().show();
$($(this).textbox("label")).show();
});
},hide:function(jq){
return jq.each(function(){
$(this).next().hide();
$($(this).textbox("label")).hide();
});
}};
$.fn.textbox.parseOptions=function(_70){
var t=$(_70);
return $.extend({},$.fn.validatebox.parseOptions(_70),$.parser.parseOptions(_70,["prompt","iconCls","iconAlign","buttonText","buttonIcon","buttonAlign","label","labelPosition","labelAlign",{multiline:"boolean",iconWidth:"number",labelWidth:"number"}]),{value:(t.val()||undefined),type:(t.attr("type")?t.attr("type"):undefined)});
};
$.fn.textbox.defaults=$.extend({},$.fn.validatebox.defaults,{doSize:true,width:"auto",height:"auto",cls:null,prompt:"",value:"",type:"text",multiline:false,icons:[],iconCls:null,iconAlign:"right",iconWidth:26,buttonText:"",buttonIcon:null,buttonAlign:"right",label:null,labelWidth:"auto",labelPosition:"before",labelAlign:"left",inputEvents:{blur:function(e){
var t=$(e.data.target);
var _71=t.textbox("options");
if(t.textbox("getValue")!=_71.value){
t.textbox("setValue",_71.value);
}
},keydown:function(e){
if(e.keyCode==13){
var t=$(e.data.target);
t.textbox("setValue",t.textbox("getText"));
}
}},onChange:function(_72,_73){
},onResizing:function(_74,_75){
},onResize:function(_76,_77){
},onClickButton:function(){
},onClickIcon:function(_78){
}});
})(jQuery);

