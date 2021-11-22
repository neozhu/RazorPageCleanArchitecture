/**
 * pivotgrid - jQuery EasyUI
 * 
 * Licensed under the GPL:
 *   http://www.gnu.org/licenses/gpl.txt
 *
 * Copyright (c) 2014 www.jeasyui.com
 * 
 * Dependencies:
 *   treegrid
 *   menu
 *   dialog
 *   layout
 * 
 */
(function($){
	function create(target){
		var opts = $.data(target, 'pivotgrid').options;
		opts.pivot.filters = opts.pivot.filters || [];
		opts.pivot.filterRules = opts.pivot.filterRules || {};
		// var filterRules = {};
		var filterRules = opts.pivot.filterRules || {};
		$.map(opts.pivot.filters, function(field){
			filterRules[field] = opts.pivot.filterRules[field] || [];
		});
		opts.pivot.filterRules = filterRules;

		clearFilterBar(target);
		$(target).treegrid($.extend({}, opts, {
			onBeforeSortColumn:function(field){
				var f = function(data){return data};
				$(this).treegrid('options').loadFilter = f;
				$(this).datagrid('options').loadFilter = f;
			},
			onSortColumn: function(){
				$(this).treegrid('options').loadFilter = opts.loadFilter;
				$(this).datagrid('options').loadFilter = opts.loadFilter;
			},
			loadFilter: function(data, parentId){
				var state = $(this).data('pivotgrid');
				state.data = data;
				var opts = state.options;
				var originalData = opts.data;
				var originalUrl = opts.url;
				var filteredData = getFilteredData(target, data);
				opts.pivot.fields = getFields(data[0]);
				$(this).treegrid({
					data: null,
					url: null,
					frozenColumns: [[
						$.extend({}, opts.frozenColumns[0][0], {
							title: opts.forzenColumnTitle
						})
					]],
					columns: getColumns(this, filteredData)
				});
				buildFilterBar(this, data);
				setTimeout(function(){
					opts.data = originalData;
					opts.url = originalUrl;
				},0);

				var rows = getRows(this, filteredData);
				return {
					total: rows.length,
					rows: rows
				}

				function getFields(row){
					var fields = [];
					for(var field in row){
						fields.push(field);
					}
					subtract(opts.pivot.filters);
					subtract(opts.pivot.rows);
					subtract(opts.pivot.columns);
					subtract(opts.pivot.values);
					return fields;

					function subtract(aa){
						$.map(aa||[], function(a){
							var index = $.inArray(typeof a == 'string' ? a : a.field, fields);
							if (index >= 0){
								fields.splice(index, 1);
							}
						});
					}
				}
			}
		}));
	}

	function getFilteredData(target, data){
		var state = $.data(target, 'pivotgrid');
		var opts = state.options;
		var rows = [];
		$.map(data||[], function(row){
			if (isMatch(row)){
				rows.push(row);
			}
		});
		return rows;

		function isMatch(row){
			for(var field in opts.pivot.filterRules){
				var values = opts.pivot.filterRules[field] || [];
				if ($.isFunction(values)){
					if (!values.call(target, row[field])){
						return false;
					}
				} else if (values.length){
					if ($.inArray(String(row[field]), values) == -1){
						return false;
					}
				}
			}
			return true;
		}
	}

	function clearFilterBar(target){
		if ($(target).data('datagrid')){
			var panel = $(target).datagrid('getPanel');
			var fbar = panel.children('div.datagrid-toolbar');
			fbar.find('.combo-f').combo('destroy');
			fbar.find('.pg-fbar').remove();
		}
	}
	function buildFilterBar(target, rows){
		var opts = $.data(target, 'pivotgrid').options;
		if (!opts.pivot.filters.length){return}
		var panel = $(target).datagrid('getPanel');
		var tb = panel.children('div.datagrid-toolbar');
		if (tb.length){
			var bar = $('<div class="pg-fbar"></div>').appendTo(tb);
			bar.css('margin-top', '5px');
		} else {
			tb = $('<div class="datagrid-toolbar"></div>').prependTo(panel);
			var bar = $('<div class="pg-fbar"></div>').appendTo(tb);
		}

		$.map(opts.pivot.filters, function(field){
			$('<span class="pg-flabel"></span>').html(field).appendTo(bar);
			var f = $('<input>').attr('name',field).appendTo(bar);
			f.combobox({
				multiple: true,
				prompt: 'Selecting',
				data: getValues(field),
				icons:[{
					iconCls:'icon-ok',
					handler:function(e){
						var t = $(e.data.target);
						var field = t.attr('comboName');
						opts.pivot.filterRules[field] = t.combobox('getValues');
						t.combobox('hidePanel');
						$(target).pivotgrid();
					}
				}],
				
				onSelect: handler1,
				onUnselect: handler1,
				onShowPanel: handler1,
				onLoadSuccess: handler2,
				onHidePanel: handler2
			});
			function handler1(){
				$(this).combobox('setText', '');
			};
			function handler2(){
				var field = $(this).attr('comboName');
				var values = opts.pivot.filterRules[field] || [];
				if ($.isFunction(values)){
					vv = [];
					$.map($(this).combobox('getData'), function(r){
						if (values.call(target, r.value)){
							vv.push(r.value);
						}
					});
					values = vv;
				}
				$(this).combobox('setValues', values);
				$(this).combobox('setText', values.length ? (values.length == 1 ? values[0] : 'multiple items') : '');
			}
		});

		function getValues(field){
			var result = {};
			$.map(rows, function(row){
				result[row[field]] = 1;
			});
			var values = [];
			for(var v in result){
				values.push({value:v,text:v});
			}
			return values;
		}
	}
	
	function getRows(target, data){
		var opts = $.data(target, 'pivotgrid').options;

		var _idIndex = 1;
		var allRows = [];
		var topRows = [];
		$.map(opts.pivot.rows, function(field, index){
			var pfield = opts.pivot.rows[index-1];
			if (pfield){
				var tmpRows = [];
				while(topRows.length){
					var r1 = topRows.shift();
					var groups = getR1(field, r1._rows);
					$.map(groups, function(rows){
						var r = sumR1(rows);
						r._rows = rows;
						r[opts.treeField] = rows[0][field];
						r._parentId = r1[opts.idField];
						r[opts.idField] = _idIndex++;
						allRows.push(r);
						tmpRows.push(r);
					})
				}
				topRows = tmpRows;
			} else {
				var groups = getR1(field, data);
				$.map(groups, function(rows){
					var r = sumR1(rows);
					r._rows = rows;
					r[opts.treeField] = rows[0][field];
					r[opts.idField] = _idIndex++;
					topRows.push(r);
					allRows.push(r);
				});
			}
		});
		return allRows;

		function sumR1(rows){
			var r = {};
			var fields = $(target).datagrid('getColumnFields');
			$.map(fields, function(field){
				r[field] = _sum(field);
			});
			return r;

			function _sum(field){
				var col = $(target).datagrid('getColumnOption', field);
				var rr = $.map(rows, function(row){
					for(var i=0; i<opts.pivot.columns.length; i++){
						if (row[opts.pivot.columns[i]] != col.tt[i]){
							return undefined;
						}
					}
					return row;
				});
				return opts.operators[col.op||'sum'].call(target, rr, col.tt[col.tt.length-1]);
			}
		}
		
		function getR1(field, rows){
			var result = {};
			$.map(rows, function(row){
				var val = row[field];
				var rr = result[val];
				if (!rr){
					rr = [row];
				} else {
					rr.push(row);
				}
				result[val] = rr;
			});
			var groups = [];
			for(var val in result){
				groups.push(result[val]);
			}
			return groups;
		}
	}
	
	function getColumns(target, data){
		if (!data){return null;}
		var opts = $.data(target, 'pivotgrid').options;
		var columns = [];
		$.map(opts.pivot.columns, function(field, index){
			var pcolumns = columns[index-1];
			if (pcolumns){
				var cc = [];
				$.map(pcolumns, function(pcol){
					var subcol = getV1(field, pcol._field, pcol.title);
					$.map(subcol, function(v){
						cc.push({
							_field: field,
							title: v,
							tt: pcol.tt.concat(v),
							colspan: opts.pivot.values.length
						});
					});
					pcol.colspan += (subcol.length-1)*opts.pivot.values.length;
				});
				columns.push(cc);
			} else {
				var cc = [];
				$.map(getV1(field), function(v){
					cc.push({
						_field: field,
						title: v,
						tt: [v],
						colspan: opts.pivot.values.length
					});
				});
				columns.push(cc);
			}
		});
		
		var cc = [];
		$.map(columns[columns.length-1], function(col, index){
			$.map(opts.pivot.values, function(v){
				cc.push($.extend({}, v, {
					field: col.tt.join('_')+'_'+v.field,
					title: (v.title || v.field),
					tt: col.tt.concat(v.field),
					width: (v.width || opts.valueFieldWidth),
					align: (v.align || 'right'),
					styler: (v.styler || opts.valueStyler),
					formatter: (v.formatter || opts.valueFormatter),
					sortable: true,
					sorter: function(a,b){
						var v1 = parseFloat(a);
						var v2 = parseFloat(b);
						return v1==v2 ? 0 : (v1>v2 ? 1 : -1);
					}
				}))
			});
		});
		columns.push(cc);
		
		return columns;
		
		// function getV1(field, pfield, pvalue){
		// 	var tmp = {};
		// 	$.map(data, function(row){
		// 		var val = row[field];
		// 		if (pfield == undefined){
		// 			tmp[val] = 1;
		// 		} else if (row[pfield] == pvalue){
		// 			tmp[val] = 1;
		// 		}
		// 	});
		// 	var vv = [];
		// 	for(var p in tmp){
		// 		vv.push(p);
		// 	}
		// 	return vv;
		// }
		function getV1(field, pfield, pvalue){
			var vv = [];
			$.map(data, function(row){
				var val = String(row[field]);
				if (pfield == undefined || row[pfield] == pvalue){
					if ($.inArray(val, vv) == -1){
						vv.push(val);
					}
				}
			});
			return vv;
		}

	}
	
	function layout(target){
		var state = $.data(target, 'pivotgrid');
		var opts = state.options;
		
		if (!state.layoutDialog){
			var content = '<div class="easyui-layout" fit="true">' +
					'<div region="west" split="true" class="pg-fields" title="' + opts.i18n.fields + '" style="width:120px"></div>' +
					'<div region="center" border="false">' +
					'<div style="height:50%;">' +
					'<div class="easyui-panel pg-filters" title="' + opts.i18n.filters + '" data-options="style:{float:\'left\'}" style="width:50%;height:100%"></div>' +
					'<div class="easyui-panel pg-columns" title="' + opts.i18n.columns + '" data-options="style:{float:\'right\'}" style="width:50%;height:100%"></div>' +
					'</div>' +
					'<div style="height:50%;">' +
					'<div class="easyui-panel pg-rows" title="' + opts.i18n.rows + '" data-options="style:{float:\'left\'}" style="width:50%;height:100%"></div>' +
					'<div class="easyui-panel pg-values" title="' + opts.i18n.values + '" data-options="style:{float:\'right\'}" style="width:50%;height:100%"></div>' +
					'</div>' +
					'</div>' +
					'</div>';
			state.layoutDialog = $('<div style="border:0"></div>').appendTo('body');
			state.layoutDialog.dialog({
				noheader:true,
				width:400,
				height:300,
				resizable:true,
				content:content,
				buttons:[{
					text:opts.i18n.ok,
					width:80,
					handler:function(){
						opts.pivot.filters = getFields('filters');
						opts.pivot.rows = getFields('rows');
						opts.pivot.columns = getFields('columns');
						opts.pivot.values = getFields('values');
						state.layoutDialog.dialog('close');
						$(target).pivotgrid();
					}
				},{
					text:opts.i18n.cancel,
					width:80,
					handler:function(){
						state.layoutDialog.dialog('close');
					}
				}]
			});
			$.parser.parse(state.layoutDialog);
		}
		state.layoutDialog.dialog('open');
		
		fill(state.layoutDialog.find('div.pg-filters'), opts.pivot.filters);
		fill(state.layoutDialog.find('div.pg-fields'), opts.pivot.fields);
		fill(state.layoutDialog.find('div.pg-columns'), opts.pivot.columns);
		fill(state.layoutDialog.find('div.pg-rows'), opts.pivot.rows);
		fill(state.layoutDialog.find('div.pg-values'), opts.pivot.values);
		dnd();
		attachOperationMenu();
		
		function fill(p, d){
			p.empty();
			$.map(d, function(name){
				var opts = typeof name == 'object' ? name : {field:name};
				var text = typeof name == 'object' ? (name.field + '<span style="color:#aaa;margin:0 10px">'+(name.op||'sum')+'</span>') : name;
				var item = $('<a class="pivotgrid-item" href="javascript:void(0)"></a>').appendTo(p);
				item.linkbutton($.extend({}, opts, {
					text: text,
					plain: true,
					width: '100%'
				}));
			});
		}
		function dnd(){
			state.layoutDialog.find('.pivotgrid-item').draggable({
				revert:true,
				proxy:function(){
					var a = $(this).clone().appendTo('body');
					a.removeClass('l-btn-plain').css('zIndex','999999');
					return a;
				},
				onBeforeDrag:function(e){
					if (e.which != 1){return false;}
				}
			}).droppable({
				accept:'.pivotgrid-item',
				onDragEnter:function(e,source){
					$(this).addClass('pivotgrid-item-ins');
				},
				onDragLeave:function(e,source){
					$(this).removeClass('pivotgrid-item-ins');
				}
			});
			state.layoutDialog.find('.pg-fields,.pg-filters,.pg-columns,.pg-rows,.pg-values').droppable({
				accept: '.pivotgrid-item',
				onDrop:function(e,source){
					var btn = $(this).find('.pivotgrid-item-ins');
					if (btn.length){
						btn.removeClass('pivotgrid-item-ins');
						$(source).insertBefore(btn);
					} else {
						$(source).appendTo(this);
					}
					var opts = $(source).linkbutton('options');
					var text = opts.field;
					if ($(this).hasClass('pg-values')){
						text += '<span style="color:#aaa;margin:0 10px">'+(opts.op||'sum')+'</span>';
					}
					$(source).linkbutton({
						text: text
					});
				}
			});
		}
		function getFields(type){
			var fields = [];
			state.layoutDialog.find('.pg-'+type+' .l-btn').each(function(){
				var bopts = $(this).linkbutton('options');
				if (type == 'values'){
					fields.push($.extend({}, bopts, {
						width: opts.valueFieldWidth,
						align: 'right'
					}));
				} else {
					fields.push(bopts.field);					
				}
			});
			return fields;
		}
		function attachOperationMenu(){
			if (!state.attached){
				state.attached = true;
				state.layoutDialog.find('div.pg-values').bind('contextmenu', function(e){
					var btn = $(e.target).closest('a.l-btn');
					if (btn.length){
						e.preventDefault();
						var m = getMenu();
						m.menu('options').alignTo = btn;
						m.menu('show');
					}
				});
			}
			function getMenu(){
				if (!state.opMenu){
					state.opMenu = $('<div></div>').appendTo('body');
					state.opMenu.menu({
						onClick:function(item){
							var op = item.text;
							var opts = $(this).menu('options');
							var btn = opts.alignTo;
							var bopts = btn.linkbutton('options');
							var text = bopts.field + '<span style="color:#aaa;margin:0 10px">'+(op)+'</span>';
							btn.linkbutton({
								op:op,
								text:text
							});
						}
					});
					for(var op in opts.operators){
						state.opMenu.menu('appendItem',{
							text:op
						});
					}
				}
				return state.opMenu;
			}
		}
	}

	function initCss(){
		if (!$('#pivotgrid-style').length){
			$('head').append(
				'<style id="pivotgrid-style">' +
				'a.pivotgrid-item,a.pivotgrid-item:hover{text-align:left;-moz-border-radius:0;-webkit-border-radius:0;border-radius:0;}' +
				'a.pivotgrid-item-ins{border-top:1px solid red;}' +
				'.pg-fbar{padding:0;}' +
				'.pg-flabel{display:inline-block;height:22px;line-height:22px;vertical-align:middle;margin:0 5px;}' +
				'</style>'
			);
		}
	}
	
	$.fn.pivotgrid = function(options, param){
		if (typeof options == 'string'){
			var method = $.fn.pivotgrid.methods[options];
			if (method){
				return method(this, param);
			} else {
				return this.treegrid(options, param);
			}
		}
		
		options = options || {};
		return this.each(function(){
			var state = $.data(this, 'pivotgrid');
			if (state){
				$.extend(state.options, options);
			} else {
				$.data(this, 'pivotgrid', {
					options: $.extend({}, $.fn.pivotgrid.defaults, $.fn.pivotgrid.parseOptions(this), options)
				});
			}
			initCss();
			create(this);
		});
	};
	
	$.fn.pivotgrid.methods = {
		options: function(jq){
			return $.data(jq[0], 'pivotgrid').options;
		},
		getData: function(jq){
			return $.data(jq[0], 'pivotgrid').data;
		},
		layout: function(jq){
			return jq.each(function(){
				layout(this);
			});
		}
	}
	
	$.fn.pivotgrid.parseOptions = function(target){
		return $.extend({}, $.fn.treegrid.parseOptions(target), $.parser.parseOptions(target, [
		]));
	};
	
	$.fn.pivotgrid.defaults = $.extend({}, $.fn.treegrid.defaults, {
		idField: '_id_field',
		treeField: '_tree_field',
		frozenColumns: [[
			{field: '_tree_field', width:200, title:'', sortable:true}
		]],
		autoRowHeight:false,
		remoteSort:false,
		
		forzenColumnTitle:'',
		valueFieldWidth:80,
		valuePrecision:0,
		valueStyler:function(){},
		valueFormatter:function(value){return value},
		i18n:{
			fields:'Fields',
			filters:'Filters',
			rows:'Rows',
			columns:'Columns',
			values:'Values',
			ok:'Ok',
			cancel:'Cancel'
		},
		operators:{
			sum: function(rows, field){
				var opts = $(this).pivotgrid('options');
				var v = 0;
				$.map(rows,function(row){
					v += parseFloat(row[field])||0;
				});
				return v.toFixed(opts.valuePrecision);
			},
			count: function(rows, field){
				return rows.length;
			},
			max: function(rows, field){
				var max = 0;
				$.map(rows, function(row){
					var v = parseFloat(row[field])||0;
					if (max < v){max = v;}
				});
				return max;
			},
			min: function(rows, field){
				var min = parseFloat(rows[0][field]);
				$.map(rows, function(row){
					var v = parseFloat(row[field])||0;
					if (min > v){min = v;}
				});
				return min;
			}
		}
	});

	$.parser.plugins.push('pivotgrid');
})(jQuery);
