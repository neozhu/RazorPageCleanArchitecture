(function($){
	function saveRange(target){
		var opts = $.data(target, 'texteditor').options;
		opts.selectedRange = null;
		if (window.getSelection){
			var sel = window.getSelection();
			if (sel.getRangeAt && sel.rangeCount){
				var range = sel.getRangeAt(0);
				var pnode = range.commonAncestorContainer.parentNode;
				if ($(pnode).closest('.texteditor')[0] == $(target).parent()[0]){
					opts.selectedRange = range;
				}
			}
		} else if (document.selection && document.selection.createRange){
			var range = document.selection.createRange();
			var pnode = range.parentElement();
			if ($(pnode).closest('.texteditor')[0] == $(target).parent()[0]){
				opts.selectedRange = range;
			}
		}
	}

	function restoreRange(target){
		var opts = $.data(target, 'texteditor').options;
		var range = opts.selectedRange;
		if (window.getSelection){
			var sel = window.getSelection();
			sel.removeAllRanges();
			if (range){
				sel.addRange(range);
			}
		} else if (document.selection){
			document.selection.empty();
			if (range && range.select){
				range.select();
			}
		}
	}

	function insertContent(target, html){
		var opts = $.data(target, 'texteditor').options;
		if (opts.selectedRange){
			if (window.getSelection){
				opts.selectedRange.collapse(false);
				opts.selectedRange.insertNode($(html)[0]);
			} else if (document.selection && opts.selectedRange.select){
				opts.selectedRange.collapse(false);
				opts.selectedRange.pasteHTML($(html)[0]);
			}
		}
	}

	function updateToolbar(target){
		var opts = $.data(target, 'texteditor').options;
		opts.dlgToolbar.find('.l-btn').each(function(){
			var cmd = $(this).attr('cmd');
			if (document.queryCommandState(cmd)){
				$(this).linkbutton('select');
			} else {
				$(this).linkbutton('unselect');
			}
		});
		opts.dlgToolbar.find('.combobox-f').each(function(){
			var cmd = $(this).attr('cmd');
			var value = String(document.queryCommandValue(cmd)||'');
			value = value.replace(/['"]/g,'').toLowerCase();
			var copts = $(this).combo('options');
			var onChange = copts.onChange;
			copts.onChange = function(){};
			var data = $(this).combobox('getData');
			if ($.easyui.indexOfArray(data, copts.valueField, value) >= 0){
				$(this).combobox('setValue', value);
			} else {
				$(this).combobox('clear');
			}
			copts.onChange = onChange;
		});
	}

	function buildEditor(target){
		var opts = $.data(target, 'texteditor').options;
		$(opts.dlgToolbar).panel('clear').remove();
		opts.dlgToolbar = $('<div></div>');
		for(var i=0; i<opts.toolbar.length; i++){
			var tool = opts.toolbar[i];
			if (tool == '-'){
				$('<span class="dialog-tool-separator"></span>').appendTo(opts.dlgToolbar);
			} else {
				var cmd = opts.commands[tool];
				if (cmd){
					cmd.type = cmd.type || 'linkbutton';
					cmd.plain = cmd.plain || true;
					var btn = $('<a href="javascript:;"></a>').appendTo(opts.dlgToolbar);
					btn.attr('cmd', tool);
					btn[cmd.type](cmd);
					if (cmd.onInit){
						cmd.onInit.call(btn[0]);
					}
				}
			}
		}
		$(target).dialog($.extend({}, opts, {
			toolbar: opts.dlgToolbar
		}));
		$(target).attr('contenteditable', true);
		var input = $(target).dialog('dialog').children('.texteditor-value');
		if (!input.length){
			input = $('<textarea class="texteditor-value" style="display:none"></textarea>').insertAfter(target);
		}
		input.attr('name', opts.name || $(target).attr('name'));
		$(target).unbind('.texteditor').bind('mouseup.texteditor keyup.texteditor',function(){
			saveRange(target);
			updateToolbar(target);
		}).bind('blur.texteditor', function(e){
			input.val($(target).html());
		});
		input.val($(target).html());
	}

	function buildColorPanel(mb){
		var opts = $(mb).menubutton('options');
		if (!opts.menu){
			opts.menu = $('<div class="menu-content texteditor-color"></div>');
		}
		opts.menu.menu({
			onShow: function(){
				if ($(this).is(':empty')){
					var colors = [
						"0,0,0","68,68,68","102,102,102","153,153,153","204,204,204","238,238,238","243,243,243","255,255,255",
						"244,204,204","252,229,205","255,242,204","217,234,211","208,224,227","207,226,243","217,210,233","234,209,220",
						"234,153,153","249,203,156","255,229,153","182,215,168","162,196,201","159,197,232","180,167,214","213,166,189",
						"224,102,102","246,178,107","255,217,102","147,196,125","118,165,175","111,168,220","142,124,195","194,123,160",
						"204,0,0","230,145,56","241,194,50","106,168,79","69,129,142","61,133,198","103,78,167","166,77,121",
						"153,0,0","180,95,6","191,144,0","56,118,29","19,79,92","11,83,148","53,28,117","116,27,71",
						"102,0,0","120,63,4","127,96,0","39,78,19","12,52,61","7,55,99","32,18,77","76,17,48"
					];
					for(var i=0; i<colors.length; i++){
						var a = $('<a class="texteditor-colorcell"></a>').appendTo(this);
						a.css('backgroundColor', 'rgb('+colors[i]+')');
					}
				}
			}
		});
		opts.menu.bind('click', function(e){
			if ($(e.target).hasClass('texteditor-colorcell')){
				var color = $(e.target).css('backgroundColor');
				opts.menu.menu('hide');
				opts.onClick.call(mb, color);
			}
		});
		$(mb).menubutton({menu:opts.menu});	// attach the menu
	}

	$.fn.texteditor = function(options, param){
		if (typeof options == 'string'){
			var method = $.fn.texteditor.methods[options];
			if (method){
				return method(this, param);
			} else {
				return this.dialog(options, param);
			}
		}
		options = options || {};
		return this.each(function(){
			var state = $.data(this, 'texteditor');
			if (state){
				$.extend(state.options, options);
			} else {
				state = $.data(this, 'texteditor', {
					options: $.extend({}, $.fn.texteditor.defaults, $.fn.texteditor.parseOptions(this), options)
				});
			}
			buildEditor(this);
		});
	};

	$.fn.texteditor.methods = {
		options: function(jq){
			return jq.data('texteditor').options;
		},
		execCommand: function(jq, cmd){
			return jq.each(function(){
				var a = cmd.split(' ');
				var c = a.shift();
				restoreRange(this);
				document.execCommand(c, false, a.join(' ')||null);
				updateToolbar(this);
				saveRange(this);
			});
		},
		getEditor: function(jq){
			return jq.closest('.texteditor').children('.texteditor-body');
		},
		insertContent: function(jq, html){
			return jq.each(function(){
				insertContent(this, html);
				$(this).dialog('dialog').children('.texteditor-value').val($(this).html());
			});
		},
		destroy: function(jq){
			return jq.each(function(){
				var opts = $(this).texteditor('options');
				$(opts.dlgToolbar).panel('clear');
				$(this).dialog('destroy');
			});
		},
		getValue: function(jq){
			return jq.dialog('dialog').children('.texteditor-value').val();
		},
		setValue: function(jq, html){
			return jq.each(function(){
				$(this).html(html);
				$(this).dialog('dialog').children('.texteditor-value').val($(this).html());
			});
		}
	};

	$.fn.texteditor.parseOptions = function(target){
		return $.extend({}, $.fn.dialog.parseOptions(target), {

		});
	};

	$.fn.texteditor.defaults = $.extend({}, $.fn.dialog.defaults, {
		title: null,
		cls: 'texteditor',
		bodyCls: 'texteditor-body',
		draggable: false,
		shadow: false,
		closable: false,
		inline: true,
		border: 'thin',
		name: '',
		toolbar: ['bold','italic','strikethrough','underline','-','justifyleft','justifycenter','justifyright','justifyfull','-','insertorderedlist','insertunorderedlist','outdent','indent','-','forecolor','backcolor','-','formatblock','fontname','fontsize'],
		commands: {
			'bold': {
				type: 'linkbutton',
				iconCls: 'icon-bold',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','bold');
				}
			},
			'italic': {
				type: 'linkbutton',
				iconCls: 'icon-italic',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','italic');
				}
			},
			'strikethrough': {
				type: 'linkbutton',
				iconCls: 'icon-strikethrough',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','strikethrough');
				}
			},
			'underline': {
				type: 'linkbutton',
				iconCls: 'icon-underline',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','underline');
				}
			},
			'justifyleft': {
				type: 'linkbutton',
				iconCls: 'icon-justifyleft',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','justifyleft');
				}
			},
			'justifycenter': {
				type: 'linkbutton',
				iconCls: 'icon-justifycenter',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','justifycenter');
				}
			},
			'justifyright': {
				type: 'linkbutton',
				iconCls: 'icon-justifyright',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','justifyright');
				}
			},
			'justifyfull': {
				type: 'linkbutton',
				iconCls: 'icon-justifyfull',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','justifyfull');
				}
			},
			'insertorderedlist': {
				type: 'linkbutton',
				iconCls: 'icon-insertorderedlist',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','insertorderedlist');
				}
			},
			'insertunorderedlist': {
				type: 'linkbutton',
				iconCls: 'icon-insertunorderedlist',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','insertunorderedlist');
				}
			},
			'outdent': {
				type: 'linkbutton',
				iconCls: 'icon-outdent',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','outdent');
				}
			},
			'indent': {
				type: 'linkbutton',
				iconCls: 'icon-indent',
				onClick: function(){
					$(this).texteditor('getEditor').texteditor('execCommand','indent');
				}
			},
			'forecolor': {
				type: 'menubutton',
				iconCls: 'icon-forecolor',
				width: 26,
				onInit: function(){
					buildColorPanel(this);
				},
				onClick: function(color){
					$(this).texteditor('getEditor').texteditor('execCommand','forecolor '+color);
				}
			},
			'backcolor': {
				type: 'menubutton',
				iconCls: 'icon-backcolor',
				width: 26,
				onInit: function(){
					buildColorPanel(this);
				},
				onClick: function(color){
					$(this).texteditor('getEditor').texteditor('execCommand','backcolor '+color);
				}
			},
			'formatblock': {
				type: 'combobox',
				width: 100,
				prompt: 'Font Format',
				editable: false,
				panelHeight: 'auto',
				data: [
					{value: 'p',text:'p'},
					{value: 'pre',text:'pre'},
					{value: 'h6',text:'h6'},
					{value: 'h5',text:'h5'},
					{value: 'h4',text:'h4'},
					{value: 'h3',text:'h3'},
					{value: 'h2',text:'h2'},
					{value: 'h1',text:'h1'}
				],
				formatter: function(row){
					return '<'+row.value+' style="margin:0;padding:0">'+row.text+'</'+row.value+'>';
				},
				onChange: function(value){
					$(this).texteditor('getEditor').texteditor('execCommand','formatblock '+value);
				}
			},
			'fontname': {
				type: 'combobox',
				width: 100,
				prompt: 'Font Family',
				editable: false,
				panelHeight: 'auto',
				data: [
					{value:'arial',text:'Arial'},
					{value:'comic sans ms',text:'Comic Sans'},
					{value:'courier new',text:'Courier New'},
					{value:'georgia',text:'Georgia'},
					{value:'helvetica',text:'Helvetica'},
					{value:'impact',text:'Impact'},
					{value:'times new roman',text:'Times'},
					{value:'trebuchet ms',text:'Trebuchet'},
					{value:'verdana',text:'Verdana'}
				],
				formatter: function(row){
					return '<font face="'+row.value+'">'+row.text+'</font>';
				},
				onChange: function(value){
					$(this).texteditor('getEditor').texteditor('execCommand','fontname '+value);
				}
			},
			'fontsize': {
				type: 'combobox',
				width: 100,
				prompt: 'Font Size',
				editable: false,
				panelHeight: 'auto',
				data: [
					{value:1,text:'Size 1'},
					{value:2,text:'Size 2'},
					{value:3,text:'Size 3'},
					{value:4,text:'Size 4'},
					{value:5,text:'Size 5'},
					{value:6,text:'Size 6'}
				],
				formatter: function(row){
					return '<font size="'+row.value+'">'+row.text+'</font>';
				},
				onChange: function(value){
					$(this).texteditor('getEditor').texteditor('execCommand','fontsize '+value);
				}
			}
		}

	});

	$.parser.plugins.push('texteditor');
})(jQuery);
