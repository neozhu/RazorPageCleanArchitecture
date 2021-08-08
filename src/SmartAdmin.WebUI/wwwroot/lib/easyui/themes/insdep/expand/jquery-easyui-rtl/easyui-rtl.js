(function($){
	$.map(['validatebox','textbox','filebox','passwordbox','searchbox',
			'combo','combobox','combogrid','combotree','combotreegrid',
			'datebox','datetimebox','numberbox',
			'spinner','numberspinner','timespinner','datetimespinner'], function(plugin){
		if ($.fn[plugin]){
			$.extend($.fn[plugin].defaults, {
				iconAlign: 'left',
				buttonAlign: 'left',
				tipPosition: 'left',
				panelAlign: 'right',
				labelAlign: 'right',
				reversed: true,	// defined for spinner plugins
				deltaX: -19
			});
		}
	});
	$.fn.validatebox.defaults.deltaX = 0;
	$.map(['linkbutton','menubutton','splitbutton'], function(plugin){
		if ($.fn[plugin]){
			$.extend($.fn[plugin].defaults, {
				iconAlign: 'right',
				menuAlign: 'right'
			});
		}
	});
	$.map(['datagrid','propertygrid','treegrid','combogrid'], function(plugin){
		if ($.fn[plugin]){
			$.extend($.fn[plugin].defaults, {
				resizeHandle: 'left'
			});
		}
	});
	$.fn.searchbox.defaults.buttonAlign = 'right';
	$.fn.menu.defaults.align = 'right';
	$.fn.tabs.defaults.toolPosition = 'left';
	$.fn.pagination.defaults.nav.first.iconCls = 'pagination-last';
	$.fn.pagination.defaults.nav.prev.iconCls = 'pagination-next';
	$.fn.pagination.defaults.nav.next.iconCls = 'pagination-prev';
	$.fn.pagination.defaults.nav.last.iconCls = 'pagination-first';
	$.fn.slider.defaults.reversed = true;
	
	var datagrid_freezeRow = $.fn.datagrid.methods.freezeRow;
	$.fn.datagrid.methods.freezeRow = function(jq, index){
		return jq.each(function(){
			datagrid_freezeRow.call(this, jq, index);
			var state = $.data(this, 'datagrid');
			if (!state.rtlscroll){
				state.rtlscroll = true;
				var dc = state.dc;
				dc.body2.bind('scroll', function(){
					var ftable = $(this).children('table.datagrid-btable-frozen');
					ftable.css('left',  $(this)._outerWidth() + $(this)._scrollLeft() - ftable._outerWidth());
				});
			}
		});
	}
	
	var tabs_update = $.fn.tabs.methods.update;
	$.fn.tabs.methods.update = function(jq, options){
		return jq.each(function(){
			tabs_update.call(this, jq, options);
			
			var wrap = $(this).find('>div.tabs-header>div.tabs.wrap');
			var opts = options.tab.panel('options');
			var tab = opts.tab;
			var tool = tab.find('.tabs-p-tool');
			if (tool.length){
				var pos = tool.css('right');
				tool.css({
					left: pos,
					right: 'auto'
				});
				
				var title = tab.find('.tabs-title');
				var pos = title.css('padding-right');
				title.css({
					paddingLeft: pos,
					paddingRight: ''
				});
			}
		});
	}
	
	function checkScrollLeftType(){
		var tmp = $('<div dir="rtl" style="font-size: 14px; width: 1px; height: 1px; position: absolute; top: -1000px; overflow: scroll">test</div>').appendTo('body');
		if (tmp.scrollLeft() > 0){
			$._rtlScrollType = 'default';	// chrome
		} else {
			tmp.scrollLeft(1);
			if (tmp.scrollLeft() == 0){
				$._rtlScrollType = 'negative';	// firefox
			} else {
				$._rtlScrollType = 'reverse';	// ie
			}
		}
		tmp.remove();
	}
	
	$.fn._scrollLeft = function(left){
		if ($._rtlScrollType == undefined){checkScrollLeftType();}
		if (left == undefined){
			if ($._rtlScrollType == 'reverse'){
				return this.scrollLeft();
			} else if ($._rtlScrollType == 'negative'){
				return -this.scrollLeft();
			} else {
				return this[0].scrollWidth - this[0].clientWidth - this.scrollLeft();
			}
		}
		return this.each(function(){
			if ($._rtlScrollType == 'reverse'){
				$(this).scrollLeft(left);
			} else if ($._rtlScrollType == 'negative'){
				$(this).scrollLeft(-left);
			} else {
				$(this).scrollLeft(this.scrollWidth - this.clientWidth - left);
			}
		});
	}
	$.fn.tabs.methods.scrollBy = function(jq, deltaX){
		if ($._rtlScrollType == undefined){checkScrollLeftType();}
		return jq.each(function(){
			var opts = $(this).tabs('options');
			var wrap = $(this).find('>div.tabs-header>div.tabs-wrap');
			var pos = Math.min(wrap._scrollLeft() - deltaX, getMaxScrollWidth());
//			if ($.browser.msie){
//				wrap.animate({scrollLeft: pos}, opts.scrollDuration);
//			} else if ($.browser.mozilla){
//				wrap.animate({scrollLeft: -pos}, opts.scrollDuration);
//			} else {
//				wrap.animate({scrollLeft: wrap[0].scrollWidth - wrap[0].clientWidth - pos}, opts.scrollDuration);
//			}
			if ($._rtlScrollType == 'reverse'){
				wrap.animate({scrollLeft: pos}, opts.scrollDuration);
			} else if ($._rtlScrollType == 'negative'){
				wrap.animate({scrollLeft: -pos}, opts.scrollDuration);
			} else {
				wrap.animate({scrollLeft: wrap[0].scrollWidth - wrap[0].clientWidth - pos}, opts.scrollDuration);
			}
			
			function getMaxScrollWidth(){
				var w = 0;
				var ul = wrap.children('ul');
				ul.children('li').each(function(){
					w += $(this).outerWidth(true);
				});
				return w - wrap.width() + (ul.outerWidth() - ul.width());
			}
		});
	}
	
	$.fn.combo.methods.showPanel = function(jq){
		return jq.each(function(){
			var target = this;
			var state = $.data(target, 'combo');
			var combo = state.combo;
			var panel = state.panel;
			var opts = $(target).combo('options');
			var palOpts = panel.panel('options');
			
			palOpts.comboTarget = target;	// store the target combo element
			if (palOpts.closed){
				panel.panel('panel').show().css({
					zIndex: ($.fn.menu ? $.fn.menu.defaults.zIndex++ : $.fn.window.defaults.zIndex++),
					left: -999999
				});
				panel.panel('resize', {
					width: (opts.panelWidth ? opts.panelWidth : combo._outerWidth()),
					height: opts.panelHeight
				});
				panel.panel('panel').hide();
				panel.panel('open');
			}
			
			(function(){
				if (panel.is(':visible')){
					panel.panel('move', {
						left:getLeft(),
						top:getTop()
					});
					setTimeout(arguments.callee, 200);
				}
			})();
			
			function getLeft(){
				var left = combo.offset().left;
				if (opts.panelAlign == 'right'){
					left += combo._outerWidth() - panel._outerWidth();
				}
				return left;
			}
			function getTop(){
				var top = combo.offset().top + combo._outerHeight();
				if (top + panel._outerHeight() > $(window)._outerHeight() + $(document).scrollTop()){
					top = combo.offset().top - panel._outerHeight();
				}
				if (top < $(document).scrollTop()){
					top = combo.offset().top + combo._outerHeight();
				}
				return top;
			}
		});
	}
	
	$.fn.menu.methods.show = function(jq, param){
		return jq.each(function(){
			var target = this;
			var left,top;
			param = param || {};
			var menu = $(param.menu || target);
			if (menu.hasClass('menu-top')){
				var opts = $.data(target, 'menu').options;
				$.extend(opts, param);
				left = opts.left - menu.outerWidth();
				top = opts.top;
				if (opts.alignTo){
					var at = $(opts.alignTo);
					left = at.offset().left;
					top = at.offset().top + at._outerHeight();
					if (opts.align == 'right'){
						left += at.outerWidth() - menu.outerWidth();
					}
				}
				top = _fixTop(top, opts.alignTo);
			} else {
				var parent = param.parent;	// the parent menu item
				left = parent.offset().left - menu.outerWidth() + 2;
				top = _fixTop(parent.offset().top - 3);
			}
			
			function _fixTop(top, alignTo){
				if (top + menu.outerHeight() > $(window)._outerHeight() + $(document).scrollTop()){
					if (alignTo){
						var at = $(alignTo);
						top = at.offset().top - menu._outerHeight();
						if (top < $(document).scrollTop()){
							top = at.offset().top + at._outerHeight();
						}
					} else {
						top = $(window)._outerHeight() + $(document).scrollTop() - menu.outerHeight() - 5;
					}
				}
				if (top < 0){top = 0;}
				return top;
			}
			
			menu.css({left:left,top:top});
			menu.show(0, function(){
				if (!menu[0].shadow){
					menu[0].shadow = $('<div class="menu-shadow"></div>').insertAfter(menu);
				}
				menu[0].shadow.css({
					display:'block',
					zIndex:$.fn.menu.defaults.zIndex++,
					left:menu.css('left'),
					top:menu.css('top'),
					width:menu.outerWidth(),
					height:menu.outerHeight()
				});
				menu.css('z-index', $.fn.menu.defaults.zIndex++);
				if (menu.hasClass('menu-top')){
					$.data(menu[0], 'menu').options.onShow.call(menu[0]);
				}
			});
		});
	}
	
})(jQuery);
