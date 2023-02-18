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
function _1(_2){
var _3=$.data(_2,"combogrid");
var _4=_3.options;
var _5=_3.grid;
$(_2).addClass("combogrid-f").combo($.extend({},_4,{onShowPanel:function(){
_22(this,$(this).combogrid("getValues"),true);
var p=$(this).combogrid("panel");
var _6=p.outerHeight()-p.height();
var _7=p._size("minHeight");
var _8=p._size("maxHeight");
var dg=$(this).combogrid("grid");
dg.datagrid("resize",{width:"100%",height:(isNaN(parseInt(_4.panelHeight))?"auto":"100%"),minHeight:(_7?_7-_6:""),maxHeight:(_8?_8-_6:"")});
var _9=dg.datagrid("getSelected");
if(_9){
dg.datagrid("scrollTo",dg.datagrid("getRowIndex",_9));
}
_4.onShowPanel.call(this);
}}));
var _a=$(_2).combo("panel");
if(!_5){
_5=$("<table></table>").appendTo(_a);
_3.grid=_5;
}
_5.datagrid($.extend({},_4,{border:false,singleSelect:(!_4.multiple),onLoadSuccess:_b,onClickRow:_c,onSelect:_d("onSelect"),onUnselect:_d("onUnselect"),onSelectAll:_d("onSelectAll"),onUnselectAll:_d("onUnselectAll")}));
function _e(dg){
return $(dg).closest(".combo-panel").panel("options").comboTarget||_2;
};
function _b(_f){
var _10=_e(this);
var _11=$(_10).data("combogrid");
var _12=_11.options;
var _13=$(_10).combo("getValues");
_22(_10,_13,_11.remainText);
_12.onLoadSuccess.call(this,_f);
};
function _c(_14,row){
var _15=_e(this);
var _16=$(_15).data("combogrid");
var _17=_16.options;
_16.remainText=false;
_18.call(this);
if(!_17.multiple){
$(_15).combo("hidePanel");
}
_17.onClickRow.call(this,_14,row);
};
function _d(_19){
return function(_1a,row){
var _1b=_e(this);
var _1c=$(_1b).combogrid("options");
if(_19=="onUnselectAll"){
if(_1c.multiple){
_18.call(this);
}
}else{
_18.call(this);
}
_1c[_19].call(this,_1a,row);
};
};
function _18(){
var dg=$(this);
var _1d=_e(dg);
var _1e=$(_1d).data("combogrid");
var _1f=_1e.options;
var vv=$.map(dg.datagrid("getSelections"),function(row){
return row[_1f.idField];
});
vv=vv.concat(_1f.unselectedValues);
var _20=dg.data("datagrid").dc.body2;
var _21=_20.scrollTop();
_22(_1d,vv,_1e.remainText);
_20.scrollTop(_21);
};
};
function nav(_23,dir){
var _24=$.data(_23,"combogrid");
var _25=_24.options;
var _26=_24.grid;
var _27=_26.datagrid("getRows").length;
if(!_27){
return;
}
var tr=_25.finder.getTr(_26[0],null,"highlight");
if(!tr.length){
tr=_25.finder.getTr(_26[0],null,"selected");
}
var _28;
if(!tr.length){
_28=(dir=="next"?0:_27-1);
}else{
var _28=parseInt(tr.attr("datagrid-row-index"));
_28+=(dir=="next"?1:-1);
if(_28<0){
_28=_27-1;
}
if(_28>=_27){
_28=0;
}
}
_26.datagrid("highlightRow",_28);
if(_25.selectOnNavigation){
_24.remainText=false;
_26.datagrid("selectRow",_28);
}
};
function _22(_29,_2a,_2b){
var _2c=$.data(_29,"combogrid");
var _2d=_2c.options;
var _2e=_2c.grid;
var _2f=$(_29).combo("getValues");
var _30=$(_29).combo("options");
var _31=_30.onChange;
_30.onChange=function(){
};
var _32=_2e.datagrid("options");
var _33=_32.onSelect;
var _34=_32.onUnselect;
var _35=_32.onUnselectAll;
_32.onSelect=_32.onUnselect=_32.onUnselectAll=function(){
};
if(!$.isArray(_2a)){
_2a=_2a.split(_2d.separator);
}
if(!_2d.multiple){
_2a=_2a.length?[_2a[0]]:[""];
}
var vv=$.map(_2a,function(_36){
return String(_36);
});
vv=$.grep(vv,function(v,_37){
return _37===$.inArray(v,vv);
});
var _38=$.grep(_2e.datagrid("getSelections"),function(row,_39){
return $.inArray(String(row[_2d.idField]),vv)>=0;
});
_2e.datagrid("clearSelections");
_2e.data("datagrid").selectedRows=_38;
var ss=[];
_2d.unselectedValues=[];
$.map(vv,function(v){
var _3a=_2e.datagrid("getRowIndex",v);
if(_3a>=0){
_2e.datagrid("selectRow",_3a);
}else{
if($.easyui.indexOfArray(_38,_2d.idField,v)==-1){
_2d.unselectedValues.push(v);
}
}
ss.push(_3b(v,_2e.datagrid("getRows"))||_3b(v,_38)||_3b(v,_2d.mappingRows)||v);
});
$(_29).combo("setValues",_2f);
_30.onChange=_31;
_32.onSelect=_33;
_32.onUnselect=_34;
_32.onUnselectAll=_35;
if(!_2b){
var s=ss.join(_2d.separator);
if($(_29).combo("getText")!=s){
$(_29).combo("setText",s);
}
}
$(_29).combo("setValues",_2a);
function _3b(_3c,a){
var _3d=$.easyui.getArrayItem(a,_2d.idField,_3c);
return _3d?_3d[_2d.textField]:undefined;
};
};
function _3e(_3f,q){
var _40=$.data(_3f,"combogrid");
var _41=_40.options;
var _42=_40.grid;
_40.remainText=true;
var qq=_41.multiple?q.split(_41.separator):[q];
qq=$.grep(qq,function(q){
return $.trim(q)!="";
});
if(_41.mode=="remote"){
_43(qq);
_42.datagrid("load",$.extend({},_41.queryParams,{q:q}));
}else{
_42.datagrid("highlightRow",-1);
var _44=_42.datagrid("getRows");
var vv=[];
$.map(qq,function(q){
q=$.trim(q);
var _45=q;
_46(_41.mappingRows,q);
_46(_42.datagrid("getSelections"),q);
var _47=_46(_44,q);
if(_47>=0){
if(_41.reversed){
_42.datagrid("highlightRow",_47);
}
}else{
$.map(_44,function(row,i){
if(_41.filter.call(_3f,q,row)){
_42.datagrid("highlightRow",i);
}
});
}
});
_43(vv);
}
function _46(_48,q){
for(var i=0;i<_48.length;i++){
var row=_48[i];
if((row[_41.textField]||"").toLowerCase()==q.toLowerCase()){
vv.push(row[_41.idField]);
return i;
}
}
return -1;
};
function _43(vv){
if(!_41.reversed){
_22(_3f,vv,true);
}
};
};
function _49(_4a){
var _4b=$.data(_4a,"combogrid");
var _4c=_4b.options;
var _4d=_4b.grid;
var tr=_4c.finder.getTr(_4d[0],null,"highlight");
_4b.remainText=false;
if(tr.length){
var _4e=parseInt(tr.attr("datagrid-row-index"));
if(_4c.multiple){
if(tr.hasClass("datagrid-row-selected")){
_4d.datagrid("unselectRow",_4e);
}else{
_4d.datagrid("selectRow",_4e);
}
}else{
_4d.datagrid("selectRow",_4e);
}
}
var vv=[];
$.map(_4d.datagrid("getSelections"),function(row){
vv.push(row[_4c.idField]);
});
$.map(_4c.unselectedValues,function(v){
if($.easyui.indexOfArray(_4c.mappingRows,_4c.idField,v)>=0){
$.easyui.addArrayItem(vv,v);
}
});
$(_4a).combogrid("setValues",vv);
if(!_4c.multiple){
$(_4a).combogrid("hidePanel");
}
};
$.fn.combogrid=function(_4f,_50){
if(typeof _4f=="string"){
var _51=$.fn.combogrid.methods[_4f];
if(_51){
return _51(this,_50);
}else{
return this.combo(_4f,_50);
}
}
_4f=_4f||{};
return this.each(function(){
var _52=$.data(this,"combogrid");
if(_52){
$.extend(_52.options,_4f);
}else{
_52=$.data(this,"combogrid",{options:$.extend({},$.fn.combogrid.defaults,$.fn.combogrid.parseOptions(this),_4f)});
}
_1(this);
});
};
$.fn.combogrid.methods={options:function(jq){
var _53=jq.combo("options");
return $.extend($.data(jq[0],"combogrid").options,{width:_53.width,height:_53.height,originalValue:_53.originalValue,disabled:_53.disabled,readonly:_53.readonly,editable:_53.editable});
},cloneFrom:function(jq,_54){
return jq.each(function(){
$(this).combo("cloneFrom",_54);
$.data(this,"combogrid",{options:$.extend(true,{cloned:true},$(_54).combogrid("options")),combo:$(this).next(),panel:$(_54).combo("panel"),grid:$(_54).combogrid("grid")});
});
},grid:function(jq){
return $.data(jq[0],"combogrid").grid;
},setValues:function(jq,_55){
return jq.each(function(){
var _56=$(this).combogrid("options");
if($.isArray(_55)){
_55=$.map(_55,function(_57){
if(_57&&typeof _57=="object"){
$.easyui.addArrayItem(_56.mappingRows,_56.idField,_57);
return _57[_56.idField];
}else{
return _57;
}
});
}
_22(this,_55);
});
},setValue:function(jq,_58){
return jq.each(function(){
$(this).combogrid("setValues",$.isArray(_58)?_58:[_58]);
});
},clear:function(jq){
return jq.each(function(){
$(this).combogrid("setValues",[]);
});
},reset:function(jq){
return jq.each(function(){
var _59=$(this).combogrid("options");
if(_59.multiple){
$(this).combogrid("setValues",_59.originalValue);
}else{
$(this).combogrid("setValue",_59.originalValue);
}
});
}};
$.fn.combogrid.parseOptions=function(_5a){
var t=$(_5a);
return $.extend({},$.fn.combo.parseOptions(_5a),$.fn.datagrid.parseOptions(_5a),$.parser.parseOptions(_5a,["idField","textField","mode"]));
};
$.fn.combogrid.defaults=$.extend({},$.fn.combo.defaults,$.fn.datagrid.defaults,{loadMsg:null,idField:null,textField:null,unselectedValues:[],mappingRows:[],mode:"local",keyHandler:{up:function(e){
nav(this,"prev");
e.preventDefault();
},down:function(e){
nav(this,"next");
e.preventDefault();
},left:function(e){
},right:function(e){
},enter:function(e){
_49(this);
},query:function(q,e){
_3e(this,q);
}},inputEvents:$.extend({},$.fn.combo.defaults.inputEvents,{blur:function(e){
$.fn.combo.defaults.inputEvents.blur(e);
var _5b=e.data.target;
var _5c=$(_5b).combogrid("options");
if(_5c.reversed){
$(_5b).combogrid("setValues",$(_5b).combogrid("getValues"));
}
}}),panelEvents:{mousedown:function(e){
}},filter:function(q,row){
var _5d=$(this).combogrid("options");
return (row[_5d.textField]||"").toLowerCase().indexOf(q.toLowerCase())>=0;
}});
})(jQuery);

