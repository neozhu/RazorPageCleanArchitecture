$(function() {
	// 初始化主题
	var initTheme = function(themeName) {
		if(themeName == null) {
			themeName = $('#themeCss').attr('href').split('/').pop().split('.css')[0];
			// 添加勾选状态
			$(".themeItem ul li").removeClass('themeActive');
			$('.themeItem ul li .' + themeName).parent().addClass('themeActive');
			return;
		}
		var themeUrl = $('#themeCss').attr('href').split('/');
		themeUrl.pop();
		$('#themeCss').after('<link rel="stylesheet" href="' + themeUrl.join('/') + '/' + themeName + '.css" id="themeCss">');
		$('#themeCss').remove();

		// 添加勾选状态
		$(".themeItem ul li").removeClass('themeActive');
		$('.themeItem ul li .' + themeName).parent().addClass('themeActive');
	}

	initTheme(localStorage.getItem('superTheme'));

	// 左侧导航分类选中样式
	$(".panel-body .accordion-body>ul").on('click', 'li', function() {
		$(this).siblings().removeClass('super-accordion-selected');
		$(this).addClass('super-accordion-selected');

		//新增一个选项卡
		var tabUrl = $(this).data('url');
		var tabTitle = $(this).text();
		//tab是否存在
		if($("#tt").tabs('exists', tabTitle)) {
			$("#tt").tabs('select', tabTitle);
		} else {
			var content = '<iframe scrolling="auto" frameborder="0"  src="' + tabUrl + '" style="width:100%;height:99%;"></iframe>';
			$('#tt').tabs('add', {
				title: tabTitle,
				//content: content,
				href: tabUrl,
				closable: true
			});
		}
	});

	// 设置按钮的下拉菜单
	$('.super-setting-icon').on('click', function() {
		$('#mm').menu('show', {
			top: 50,
			left: document.body.scrollWidth - 160
		});
	});

	// 修改主题
	$('#themeSetting').on('click', function() {
		var themeWin = $('#win').dialog({
			width: 460,
			height: 260,
			modal: true,
			title: '主题设置',
			buttons: [{
				text: '保存',
				id: 'btn-sure',
				handler: function() {
					themeWin.panel('close');
					// css
					var themeName = $(".themeItem ul li.themeActive>div").attr('class');
					initTheme(themeName);
					localStorage.setItem('superTheme', themeName);
				}
			}, {
				text: '关闭',
				handler: function() {
					themeWin.panel('close');
				}
			}],
			onOpen: function() {
				$(".themeItem").show();
			}
		});
	});

	// 勾选主题
	$(".themeItem ul li").on('click', function() {
		$(".themeItem ul li").removeClass('themeActive');
		$(this).addClass('themeActive');
	});

	// 退出系统
	$("#logout").on('click', function() {
		$.messager.confirm('提示', '确定退出系统？', function(r) {
			if(r) {
				console.log('确定退出')
			}
		});
	});
});