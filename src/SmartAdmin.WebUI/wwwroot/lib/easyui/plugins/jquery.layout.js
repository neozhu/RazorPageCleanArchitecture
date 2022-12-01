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
var _1=false;
function _2(_3,_4){
var _5=$.data(_3,"layout");
var _6=_5.options;
var _7=_5.panels;
var cc=$(_3);
if(_4){
$.extend(_6,{width:_4.width,height:_4.height});
}
if(_3.tagName.toLowerCase()=="body"){
cc._size("fit");
}else{
cc._size(_6);
}
var _8={top:0,left:0,width:cc.width(),height:cc.height()};
_9(_a(_7.expandNorth)?_7.expandNorth:_7.north,"n");
_9(_a(_7.expandSouth)?_7.expandSouth:_7.south,"s");
_b(_a(_7.expandEast)?_7.expandEast:_7.east,"e");
_b(_a(_7.expandWest)?_7.expandWest:_7.west,"w");
_7.center.panel("resize",_8);
function _9(pp,_c){
if(!pp.length||!_a(pp)){
return;
}
var _d=pp.panel("options");
pp.panel("resize",{width:cc.width(),height:_d.height});
var _e=pp.panel("panel").outerHeight();
pp.panel("move",{left:0,top:(_c=="n"?0:cc.height()-_e)});
_8.height-=_e;
if(_c=="n"){
_8.top+=_e;
if(!_d.split&&_d.border){
_8.top--;
}
}
if(!_d.split&&_d.border){
_8.height++;
}
};
function _b(pp,_f){
if(!pp.length||!_a(pp)){
return;
}
var _10=pp.panel("options");
pp.panel("resize",{width:_10.width,height:_8.height});
var _11=pp.panel("panel").outerWidth();
pp.panel("move",{left:(_f=="e"?cc.width()-_11:0),top:_8.top});
_8.width-=_11;
if(_f=="w"){
_8.left+=_11;
if(!_10.split&&_10.border){
_8.left--;
}
}
if(!_10.split&&_10.border){
_8.width++;
}
};
};
function _12(_13){
var cc=$(_13);
cc.addClass("layout");
function _14(el){
var _15=$.fn.layout.parsePanelOptions(el);
if("north,south,east,west,center".indexOf(_15.region)>=0){
_19(_13,_15,el);
}
};
var _16=cc.layout("options");
var _17=_16.onAdd;
_16.onAdd=function(){
};
cc.find(">div,>form>div").each(function(){
_14(this);
});
_16.onAdd=_17;
cc.append("<div class=\"layout-split-proxy-h\"></div><div class=\"layout-split-proxy-v\"></div>");
cc._bind("_resize",function(e,_18){
if($(this).hasClass("easyui-fluid")||_18){
_2(_13);
}
return false;
});
};
function _19(_1a,_1b,el){
_1b.region=_1b.region||"center";
var _1c=$.data(_1a,"layout").panels;
var cc=$(_1a);
var dir=_1b.region;
if(_1c[dir].length){
return;
}
var pp=$(el);
if(!pp.length){
pp=$("<div></div>").appendTo(cc);
}
var _1d=$.extend({},$.fn.layout.paneldefaults,{width:(pp.length?parseInt(pp[0].style.width)||pp.outerWidth():"auto"),height:(pp.length?parseInt(pp[0].style.height)||pp.outerHeight():"auto"),doSize:false,collapsible:true,onOpen:function(){
var _1e=$(this).panel("header").children("div.panel-tool");
_1e.children("a.panel-tool-collapse").hide();
var _1f={north:"up",south:"down",east:"right",west:"left"};
if(!_1f[dir]){
return;
}
var _20="layout-button-"+_1f[dir];
var t=_1e.children("a."+_20);
if(!t.length){
t=$("<a href=\"javascript:;\"></a>").addClass(_20).appendTo(_1e);
t._bind("click",{dir:dir},function(e){
_39(_1a,e.data.dir);
return false;
});
}
$(this).panel("options").collapsible?t.show():t.hide();
}},_1b,{cls:((_1b.cls||"")+" layout-panel layout-panel-"+dir),bodyCls:((_1b.bodyCls||"")+" layout-body")});
pp.panel(_1d);
_1c[dir]=pp;
var _21={north:"s",south:"n",east:"w",west:"e"};
var _22=pp.panel("panel");
if(pp.panel("options").split){
_22.addClass("layout-split-"+dir);
}
_22.resizable($.extend({},{handles:(_21[dir]||""),disabled:(!pp.panel("options").split),onStartResize:function(e){
_1=true;
if(dir=="north"||dir=="south"){
var _23=$(">div.layout-split-proxy-v",_1a);
}else{
var _23=$(">div.layout-split-proxy-h",_1a);
}
var top=0,_24=0,_25=0,_26=0;
var pos={display:"block"};
if(dir=="north"){
pos.top=parseInt(_22.css("top"))+_22.outerHeight()-_23.height();
pos.left=parseInt(_22.css("left"));
pos.width=_22.outerWidth();
pos.height=_23.height();
}else{
if(dir=="south"){
pos.top=parseInt(_22.css("top"));
pos.left=parseInt(_22.css("left"));
pos.width=_22.outerWidth();
pos.height=_23.height();
}else{
if(dir=="east"){
pos.top=parseInt(_22.css("top"))||0;
pos.left=parseInt(_22.css("left"))||0;
pos.width=_23.width();
pos.height=_22.outerHeight();
}else{
if(dir=="west"){
pos.top=parseInt(_22.css("top"))||0;
pos.left=_22.outerWidth()-_23.width();
pos.width=_23.width();
pos.height=_22.outerHeight();
}
}
}
}
_23.css(pos);
$("<div class=\"layout-mask\"></div>").css({left:0,top:0,width:cc.width(),height:cc.height()}).appendTo(cc);
},onResize:function(e){
if(dir=="north"||dir=="south"){
var _27=_28(this);
$(this).resizable("options").maxHeight=_27;
var _29=$(">div.layout-split-proxy-v",_1a);
var top=dir=="north"?e.data.height-_29.height():$(_1a).height()-e.data.height;
_29.css("top",top);
}else{
var _2a=_28(this);
$(this).resizable("options").maxWidth=_2a;
var _29=$(">div.layout-split-proxy-h",_1a);
var _2b=dir=="west"?e.data.width-_29.width():$(_1a).width()-e.data.width;
_29.css("left",_2b);
}
return false;
},onStopResize:function(e){
cc.children("div.layout-split-proxy-v,div.layout-split-proxy-h").hide();
pp.panel("resize",e.data);
_2(_1a);
_1=false;
cc.find(">div.layout-mask").remove();
}},_1b));
cc.layout("options").onAdd.call(_1a,dir);
function _28(p){
var _2c="expand"+dir.substring(0,1).toUpperCase()+dir.substring(1);
var _2d=_1c["center"];
var _2e=(dir=="north"||dir=="south")?"minHeight":"minWidth";
var _2f=(dir=="north"||dir=="south")?"maxHeight":"maxWidth";
var _30=(dir=="north"||dir=="south")?"_outerHeight":"_outerWidth";
var _31=$.parser.parseValue(_2f,_1c[dir].panel("options")[_2f],$(_1a));
var _32=$.parser.parseValue(_2e,_2d.panel("options")[_2e],$(_1a));
var _33=_2d.panel("panel")[_30]()-_32;
if(_a(_1c[_2c])){
_33+=_1c[_2c][_30]()-1;
}else{
_33+=$(p)[_30]();
}
if(_33>_31){
_33=_31;
}
return _33;
};
};
function _34(_35,_36){
var _37=$.data(_35,"layout").panels;
if(_37[_36].length){
_37[_36].panel("destroy");
_37[_36]=$();
var _38="expand"+_36.substring(0,1).toUpperCase()+_36.substring(1);
if(_37[_38]){
_37[_38].panel("destroy");
_37[_38]=undefined;
}
$(_35).layout("options").onRemove.call(_35,_36);
}
};
function _39(_3a,_3b,_3c){
if(_3c==undefined){
_3c="normal";
}
var _3d=$.data(_3a,"layout");
var _3e=_3d.panels;
var p=_3e[_3b];
var _3f=p.panel("options");
if(_3f.onBeforeCollapse.call(p)==false){
return;
}
var _40="expand"+_3b.substring(0,1).toUpperCase()+_3b.substring(1);
if(!_3e[_40]){
_3e[_40]=_41(_3b);
var ep=_3e[_40].panel("panel");
if(!_3f.expandMode){
ep.css("cursor","default");
}else{
ep._bind("click",function(){
if(_3f.expandMode=="dock"){
_53(_3a,_3b);
}else{
p.panel("expand",false).panel("open");
var _42=_43();
p.panel("resize",_42.collapse);
p.panel("panel")._unbind(".layout")._bind("mouseleave.layout",{region:_3b},function(e){
var _44=this;
_3d.collapseTimer=setTimeout(function(){
$(_44).stop(true,true);
if(_1==true){
return;
}
if($("body>div.combo-p>div.combo-panel:visible").length){
return;
}
_39(_3a,e.data.region);
},_3d.options.collapseDelay);
});
p.panel("panel").animate(_42.expand,function(){
$(_3a).layout("options").onExpand.call(_3a,_3b);
});
}
return false;
});
}
}
var _45=_43();
if(!_a(_3e[_40])){
_3e.center.panel("resize",_45.resizeC);
}
p.panel("panel").animate(_45.collapse,_3c,function(){
p.panel("collapse",false).panel("close");
_3e[_40].panel("open").panel("resize",_45.expandP);
$(this)._unbind(".layout");
$(_3a).layout("options").onCollapse.call(_3a,_3b);
});
function _41(dir){
var _46={"east":"left","west":"right","north":"down","south":"up"};
var _47=(_3f.region=="north"||_3f.region=="south");
var _48="layout-button-"+_46[dir];
var p=$("<div></div>").appendTo(_3a);
p.panel($.extend({},$.fn.layout.paneldefaults,{cls:("layout-expand layout-expand-"+dir),title:"&nbsp;",titleDirection:_3f.titleDirection,iconCls:(_3f.hideCollapsedContent?null:_3f.iconCls),closed:true,minWidth:0,minHeight:0,doSize:false,region:_3f.region,collapsedSize:_3f.collapsedSize,noheader:(!_47&&_3f.hideExpandTool),tools:((_47&&_3f.hideExpandTool)?null:[{iconCls:_48,handler:function(){
_53(_3a,_3b);
return false;
}}]),onResize:function(){
var _49=$(this).children(".layout-expand-title");
if(_49.length){
var _4a=$(this).children(".panel-icon");
var _4b=_4a.length>0?(_4a._outerHeight()+2):0;
_49._outerWidth($(this).height()-_4b);
var _4c=($(this).width()-Math.min(_49._outerWidth(),_49._outerHeight()))/2;
var top=Math.max(_49._outerWidth(),_49._outerHeight());
if(_49.hasClass("layout-expand-title-down")){
_4c+=Math.min(_49._outerWidth(),_49._outerHeight());
top=0;
}
top+=_4b;
_49.css({left:(_4c+"px"),top:(top+"px")});
}
}}));
if(!_3f.hideCollapsedContent){
var _4d=typeof _3f.collapsedContent=="function"?_3f.collapsedContent.call(p[0],_3f.title):_3f.collapsedContent;
_47?p.panel("setTitle",_4d):p.html(_4d);
}
p.panel("panel").hover(function(){
$(this).addClass("layout-expand-over");
},function(){
$(this).removeClass("layout-expand-over");
});
return p;
};
function _43(){
var cc=$(_3a);
var _4e=_3e.center.panel("options");
var _4f=_3f.collapsedSize;
if(_3b=="east"){
var _50=p.panel("panel")._outerWidth();
var _51=_4e.width+_50-_4f;
if(_3f.split||!_3f.border){
_51++;
}
return {resizeC:{width:_51},expand:{left:cc.width()-_50},expandP:{top:_4e.top,left:cc.width()-_4f,width:_4f,height:_4e.height},collapse:{left:cc.width(),top:_4e.top,height:_4e.height}};
}else{
if(_3b=="west"){
var _50=p.panel("panel")._outerWidth();
var _51=_4e.width+_50-_4f;
if(_3f.split||!_3f.border){
_51++;
}
return {resizeC:{width:_51,left:_4f-1},expand:{left:0},expandP:{left:0,top:_4e.top,width:_4f,height:_4e.height},collapse:{left:-_50,top:_4e.top,height:_4e.height}};
}else{
if(_3b=="north"){
var _52=p.panel("panel")._outerHeight();
var hh=_4e.height;
if(!_a(_3e.expandNorth)){
hh+=_52-_4f+((_3f.split||!_3f.border)?1:0);
}
_3e.east.add(_3e.west).add(_3e.expandEast).add(_3e.expandWest).panel("resize",{top:_4f-1,height:hh});
return {resizeC:{top:_4f-1,height:hh},expand:{top:0},expandP:{top:0,left:0,width:cc.width(),height:_4f},collapse:{top:-_52,width:cc.width()}};
}else{
if(_3b=="south"){
var _52=p.panel("panel")._outerHeight();
var hh=_4e.height;
if(!_a(_3e.expandSouth)){
hh+=_52-_4f+((_3f.split||!_3f.border)?1:0);
}
_3e.east.add(_3e.west).add(_3e.expandEast).add(_3e.expandWest).panel("resize",{height:hh});
return {resizeC:{height:hh},expand:{top:cc.height()-_52},expandP:{top:cc.height()-_4f,left:0,width:cc.width(),height:_4f},collapse:{top:cc.height(),width:cc.width()}};
}
}
}
}
};
};
function _53(_54,_55){
var _56=$.data(_54,"layout").panels;
var p=_56[_55];
var _57=p.panel("options");
if(_57.onBeforeExpand.call(p)==false){
return;
}
var _58="expand"+_55.substring(0,1).toUpperCase()+_55.substring(1);
if(_56[_58]){
_56[_58].panel("close");
p.panel("panel").stop(true,true);
p.panel("expand",false).panel("open");
var _59=_5a();
p.panel("resize",_59.collapse);
p.panel("panel").animate(_59.expand,function(){
_2(_54);
$(_54).layout("options").onExpand.call(_54,_55);
});
}
function _5a(){
var cc=$(_54);
var _5b=_56.center.panel("options");
if(_55=="east"&&_56.expandEast){
return {collapse:{left:cc.width(),top:_5b.top,height:_5b.height},expand:{left:cc.width()-p.panel("panel")._outerWidth()}};
}else{
if(_55=="west"&&_56.expandWest){
return {collapse:{left:-p.panel("panel")._outerWidth(),top:_5b.top,height:_5b.height},expand:{left:0}};
}else{
if(_55=="north"&&_56.expandNorth){
return {collapse:{top:-p.panel("panel")._outerHeight(),width:cc.width()},expand:{top:0}};
}else{
if(_55=="south"&&_56.expandSouth){
return {collapse:{top:cc.height(),width:cc.width()},expand:{top:cc.height()-p.panel("panel")._outerHeight()}};
}
}
}
}
};
};
function _a(pp){
if(!pp){
return false;
}
if(pp.length){
return pp.panel("panel").is(":visible");
}else{
return false;
}
};
function _5c(_5d){
var _5e=$.data(_5d,"layout");
var _5f=_5e.options;
var _60=_5e.panels;
var _61=_5f.onCollapse;
_5f.onCollapse=function(){
};
_62("east");
_62("west");
_62("north");
_62("south");
_5f.onCollapse=_61;
function _62(_63){
var p=_60[_63];
if(p.length&&p.panel("options").collapsed){
_39(_5d,_63,0);
}
};
};
function _64(_65,_66,_67){
var p=$(_65).layout("panel",_66);
p.panel("options").split=_67;
var cls="layout-split-"+_66;
var _68=p.panel("panel").removeClass(cls);
if(_67){
_68.addClass(cls);
}
_68.resizable({disabled:(!_67)});
_2(_65);
};
$.fn.layout=function(_69,_6a){
if(typeof _69=="string"){
return $.fn.layout.methods[_69](this,_6a);
}
_69=_69||{};
return this.each(function(){
var _6b=$.data(this,"layout");
if(_6b){
$.extend(_6b.options,_69);
}else{
var _6c=$.extend({},$.fn.layout.defaults,$.fn.layout.parseOptions(this),_69);
$.data(this,"layout",{options:_6c,panels:{center:$(),north:$(),south:$(),east:$(),west:$()}});
_12(this);
}
_2(this);
_5c(this);
});
};
$.fn.layout.methods={options:function(jq){
return $.data(jq[0],"layout").options;
},resize:function(jq,_6d){
return jq.each(function(){
_2(this,_6d);
});
},panel:function(jq,_6e){
return $.data(jq[0],"layout").panels[_6e];
},collapse:function(jq,_6f){
return jq.each(function(){
_39(this,_6f);
});
},expand:function(jq,_70){
return jq.each(function(){
_53(this,_70);
});
},add:function(jq,_71){
return jq.each(function(){
_19(this,_71);
_2(this);
if($(this).layout("panel",_71.region).panel("options").collapsed){
_39(this,_71.region,0);
}
});
},remove:function(jq,_72){
return jq.each(function(){
_34(this,_72);
_2(this);
});
},split:function(jq,_73){
return jq.each(function(){
_64(this,_73,true);
});
},unsplit:function(jq,_74){
return jq.each(function(){
_64(this,_74,false);
});
},stopCollapsing:function(jq){
return jq.each(function(){
clearTimeout($(this).data("layout").collapseTimer);
});
}};
$.fn.layout.parseOptions=function(_75){
return $.extend({},$.parser.parseOptions(_75,[{fit:"boolean"}]));
};
$.fn.layout.defaults={fit:false,onExpand:function(_76){
},onCollapse:function(_77){
},onAdd:function(_78){
},onRemove:function(_79){
}};
$.fn.layout.parsePanelOptions=function(_7a){
var t=$(_7a);
return $.extend({},$.fn.panel.parseOptions(_7a),$.parser.parseOptions(_7a,["region",{split:"boolean",collpasedSize:"number",minWidth:"number",minHeight:"number",maxWidth:"number",maxHeight:"number"}]));
};
$.fn.layout.paneldefaults=$.extend({},$.fn.panel.defaults,{region:null,split:false,collapseDelay:100,collapsedSize:32,expandMode:"float",hideExpandTool:false,hideCollapsedContent:true,collapsedContent:function(_7b){
var p=$(this);
var _7c=p.panel("options");
if(_7c.region=="north"||_7c.region=="south"){
return _7b;
}
var cc=[];
if(_7c.iconCls){
cc.push("<div class=\"panel-icon "+_7c.iconCls+"\"></div>");
}
cc.push("<div class=\"panel-title layout-expand-title");
cc.push(" layout-expand-title-"+_7c.titleDirection);
cc.push(_7c.iconCls?" layout-expand-with-icon":"");
cc.push("\">");
cc.push(_7b);
cc.push("</div>");
return cc.join("");
},minWidth:10,minHeight:10,maxWidth:10000,maxHeight:10000});
})(jQuery);

