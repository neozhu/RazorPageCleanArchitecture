/*
    JQuery EasyUI 1.5.x of Insdep Theme 1.0.0
    演示地址：https://www.insdep.com/example/
    下载地址：https://www.insdep.com
    问答地址：https://bbs.insdep.com

    项目地址：http://git.oschina.net/weavors/JQuery-EasyUI-1.5.x-Of-Insdep-Theme

    QQ交流群：184075694 （优先发布更新主题及内测包）
*/

jQuery(function($){jQuery.ajaxSetup({cache:false});});/*重置ajax并取消缓存*/

/*初始化语言*/
if ($.fn.pagination){
	$.fn.pagination.defaults.beforePageText = '第';
	$.fn.pagination.defaults.afterPageText = '共 {pages} 页';
	$.fn.pagination.defaults.displayMsg = '显示 {from} 到 {to},共 {total} 记录';
}
if ($.fn.datagrid){
	$.fn.datagrid.defaults.loadMsg = '正在处理，请稍待。。。';
}
if ($.fn.treegrid && $.fn.datagrid){
	$.fn.treegrid.defaults.loadMsg = $.fn.datagrid.defaults.loadMsg;
}
if ($.messager){
	$.messager.defaults.ok = '确定';
	$.messager.defaults.cancel = '取消';
}
$.map(['validatebox','textbox','passwordbox','filebox','searchbox',
		'combo','combobox','combogrid','combotree',
		'datebox','datetimebox','numberbox',
		'spinner','numberspinner','timespinner','datetimespinner'], function(plugin){
	if ($.fn[plugin]){
		$.fn[plugin].defaults.missingMessage = '该输入项为必输项';
	}
});
if ($.fn.validatebox){
	$.fn.validatebox.defaults.rules.email.message = '请输入有效的电子邮件地址';
	$.fn.validatebox.defaults.rules.url.message = '请输入有效的URL地址';
	$.fn.validatebox.defaults.rules.length.message = '输入内容长度必须介于{0}和{1}之间';
	$.fn.validatebox.defaults.rules.remote.message = '请修正该字段';
}
if ($.fn.calendar){
	$.fn.calendar.defaults.weeks = ['日','一','二','三','四','五','六'];
	$.fn.calendar.defaults.months = ['一月','二月','三月','四月','五月','六月','七月','八月','九月','十月','十一月','十二月'];
}
if ($.fn.datebox){
	$.fn.datebox.defaults.currentText = '今天';
	$.fn.datebox.defaults.closeText = '关闭';
	$.fn.datebox.defaults.okText = '确定';
	$.fn.datebox.defaults.formatter = function(date){
		var y = date.getFullYear();
		var m = date.getMonth()+1;
		var d = date.getDate();
		return y+'-'+(m<10?('0'+m):m)+'-'+(d<10?('0'+d):d);
	};
	$.fn.datebox.defaults.parser = function(s){
		if (!s) return new Date();
		var ss = s.split('-');
		var y = parseInt(ss[0],10);
		var m = parseInt(ss[1],10);
		var d = parseInt(ss[2],10);
		if (!isNaN(y) && !isNaN(m) && !isNaN(d)){
			return new Date(y,m-1,d);
		} else {
			return new Date();
		}
	};
}
if ($.fn.datetimebox && $.fn.datebox){
	$.extend($.fn.datetimebox.defaults,{
		currentText: $.fn.datebox.defaults.currentText,
		closeText: $.fn.datebox.defaults.closeText,
		okText: $.fn.datebox.defaults.okText
	});
}
if ($.fn.datetimespinner){
	$.fn.datetimespinner.defaults.selections = [[0,4],[5,7],[8,10],[11,13],[14,16],[17,19]]
}


/*初始化*/
//待修复 组件设置label后，高度失效问题
$.extend($.fn.tabs.defaults,{tabHeight:36}); 
$.extend($.fn.textbox.defaults,{height:32});
$.extend($.fn.passwordbox.defaults,{height:32});
$.extend($.fn.combo.defaults,{height:32});
$.extend($.fn.combobox.defaults,{height:32});
$.extend($.fn.combogrid.defaults,{height:32});
$.extend($.fn.combotree.defaults,{height:32});
$.extend($.fn.combotreegrid.defaults,{height:32});

$.extend($.messager.defaults,{width:320}); 


$.extend($.fn.datebox.defaults,{
	height:32,
	panelWidth:220
	/*
	此处待修复，无法选择日期错误
	,
	formatter:function(date){
		var y = date.getFullYear();
		var m = date.getMonth()+1;
		var d = date.getDate();
		return y+"年"+m+"月"+d+"日";
	}*/
});
$.extend($.fn.datetimebox.defaults,{
	height:32,
	panelWidth:220
	/*
	此处待修复，无法选择日期错误
	,
	formatter:function(date){
		var y = date.getFullYear();
		var m = date.getMonth()+1;
		var d = date.getDate();
		var H = date.getHours();       //获取当前小时数(0-23)
		var i = date.getMinutes();     //获取当前分钟数(0-59)
		var s = date.getSeconds();     //获取当前秒数(0-59)
		return y+"年"+m+"月"+d+"日 "+H+":"+i+":"+s;
	}*/
});
$.extend($.fn.datetimespinner.defaults,{height:32});
$.extend($.fn.numberbox.defaults,{height:32});
$.extend($.fn.spinner.defaults,{height:32});


$.extend($.fn.numberspinner.defaults,{height:32});//上线数字不能使用
$.extend($.fn.searchbox.defaults,{height:32});
$.extend($.fn.filebox.defaults,{height:32});

$.extend($.fn.validatebox.defaults,{height:32});
$.extend($.fn.validatebox.defaults.tipOptions, {
	onShow: function() {
		$(this).tooltip("tip").css({
			color: "#fff",
			border: "none",
			backgroundColor: "#ff7e00"
		});
	}
}); //重置tipOptions的样式



;(function($){
	$.insdep={
		ajax:function(c){
			var d={
				url:"",
				async:false,//true异步请求,false同步请求,注：同步请求将锁住浏览器，用户其它操作必须等待请求完成才可以执行
				cache:false,
				type:"POST",
				dataType:"html",
				beforeSend:function(){
					$.messager.progress();
				},
				complete:function(){
					$.messager.progress('close');
				}
			};
			var n=$.extend(true,d,c);
			$.ajax(n);
		},
		error:function(result) {
			$('<div/>').window({
				id:"insdep-error-debug-window",
				cache:false,
			    width : 720,
			    height : 480,
			    modal : true,
				title:"错误",
				content:"<div style='padding:15px;overflow:hidden;height:auto;clear:both;'><b>Return Error</b><br/>"+result+"<br/></div>",
				border:false,
				collapsible:false,
				minimizable:false,
				maximizable:false,
				queryParams:"",
				onClose:function() {
					$(this).window('destroy');
				},
				buttons:[]
			});
		},
		window:function(c){
			var d={
               id:"temp-window",
               href:"",
               cache:false,
               method:"post",
               width : 680,
               height : 550,
               modal : true,
               title:"",
               border:false,
               collapsible:false,
               minimizable:false,
               maximizable:false,
               queryParams:"",
               onClose:function() {
                  $(this).window('destroy');
               },
               buttons:[]
			};
			var n=$.extend(true,d,c);
			$('<div/>').window(n);
		},
		control:function(url,queryParams){
			$('#control').panel({    
				href:url,
				cache:false,
				method:"post",
				queryParams:queryParams?queryParams:{},    
				onLoad:function(){    
					//alert('loaded successfully');    
				},
				onBeforeLoad:function(){
					$(this).panel('clear');
				}
			}); 
		}
	};
})(jQuery);
