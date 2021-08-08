(function($){
	function buildRibbon(target){
		var opts = $.data(target, 'ribbon').options;
		
		$(target).addClass('ribbon').tabs(opts);
		var tabs = $(target).tabs('tabs');
		for(var i=0; i<tabs.length; i++){
			tabs[i].addClass('ribbon-tab');
		}
	}
	
	function bindEvents(target){
		var opts = $.data(target, 'ribbon').options;
		
		$(target).find('.l-btn').unbind('.ribbon').bind('click.ribbon', function(e){
			var bopts = $(this).linkbutton('options');
			opts.onClick.call(target, bopts.name, this);
		});
		$(target).find('.m-btn').each(function(){
			var m = $($(this).menubutton('options').menu);
			if (m.length){
				var mopts = m.menu('options');
				var onClick = mopts.onClick;
				mopts.onClick = function(item){
					onClick.call(this, item);
					if (mopts.timer){
						clearTimeout(mopts.timer);
					}
					mopts.timer = setTimeout(function(){
						opts.onClick.call(target, item.name, m[0]);
					},0);
				}
			}
		});
	}
	
	function loadData(target, data){
		var opts = $.data(target, 'ribbon').options;
		var r = $(target);
		for(var i=r.ribbon('tabs').length-1; i>=0; i--){
			r.ribbon('close', i);
		}
		if (data){
			opts.data = data;
		}
		opts.data.tabs = opts.data.tabs || [];
		for(var i=0; i<opts.data.tabs.length; i++){
			var tab = opts.data.tabs[i];
			r.ribbon('add', $.extend({}, tab, {
				bodyCls:'ribbon-tab'
			}));
			
			var p = r.ribbon('getTab', i);
			buildGroups(tab.groups, p);
		}
		if (opts.data.selected == undefined){
			opts.data.selected = 0;
		}
		r.ribbon('select', opts.data.selected);
		
		bindEvents(target);
		
		function buildGroups(groups, p){
			groups = groups || [];
			for(var i=0; i<groups.length; i++){
				var group = groups[i];
				group.dir = group.dir || 'h';
				var g = $('<div class="ribbon-group"></div>').appendTo(p);
				$('<div class="ribbon-group-sep"></div>').appendTo(p);
				$('<div class="ribbon-group-title"></div>').html(group.title||'').appendTo(g);
				group.tools = group.tools || [];
				$.map(group.tools, function(tool){
					var type = tool.type || 'linkbutton';
					if (type == 'toolbar'){
						var toolbar = $('<div class="ribbon-toolbar"></div>').appendTo(g);
						toolbar.css(tool.style||{});
						if (group.dir == 'v'){
							toolbar.css('clear', 'both');
						}
						var dir = tool.dir || 'h';
						$.map(tool.tools, function(tool){
							buildTool(tool, toolbar, dir);
						});
						toolbar.append('<div style="clear:both"></div>');
					} else {
						buildTool(tool, g, group.dir);
					}
				});
				g.find('.ribbon-group-title')._outerWidth(g.outerWidth()-2);
			}
			
			function buildTool(options, p, dir){
				var type = options.type || 'linkbutton';
				options.plain = options.plain || true;
				if (options.menuItems){
					var m = $('<div></div>').appendTo('body');
					m.menu();
					$.map(options.menuItems, function(item){
						m.menu('appendItem', item);
					});
					options.menu = m;
				}
				var btn = $('<a href="javascript:void(0)"></a>').appendTo(p);
				btn[type](options);
				if (dir == 'v'){
					btn.css('clear','both');
				}
			}
		}
	}
	
	$.fn.ribbon = function(options, param){
		if (typeof options == 'string'){
			var method = $.fn.ribbon.methods[options];
			if (method){
				return method(this, param);
			} else {
				return this.tabs(options, param);
			}
		}
		
		options = options || {};
		return this.each(function(){
			var state = $.data(this, 'ribbon');
			if (state){
				$.extend(state.options, options);
			} else {
				state = $.data(this, 'ribbon', {
					options: $.extend({}, $.fn.ribbon.defaults, $.fn.ribbon.parseOptions(this), options)
				});
			}
			buildRibbon(this);
			bindEvents(this);
			if (state.options.data){
				loadData(this, state.options.data);
			}
		});
	};
	
	$.fn.ribbon.methods = {
		options: function(jq){
			return $.data(jq[0], 'ribbon').options;
		},
		loadData: function(jq, data){
			return jq.each(function(){
				loadData(this, data);
			});
		}
	};
	
	$.fn.ribbon.parseOptions = function(target){
		return $.extend({}, $.fn.tabs.parseOptions(target), {
			
		});
	};
	
	$.fn.ribbon.defaults = $.extend({}, $.fn.tabs.defaults, {
		onClick:function(name, target){}
	});
	
	////////////////////////////////
	$.parser.plugins.push('ribbon');
})(jQuery);
