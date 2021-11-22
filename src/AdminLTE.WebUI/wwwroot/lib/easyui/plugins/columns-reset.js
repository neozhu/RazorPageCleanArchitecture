(function ($) {
  function buildMenu(target) {
    const state = $(target).data('datagrid');
    //冻结列不允许修改属性和位置
    //const fields = $(target).datagrid('getColumnFields',true).concat($(target).datagrid('getColumnFields', false));
    const fields = $(target).datagrid('getColumnFields');
    if (!state.columnMenu) {
      state.columnMenu = $('<div></div>').appendTo('body');
      state.columnMenu.menu({
        onClick: function (item) {
  
          if (item.iconCls === 'fal fa-check-square fa-lg') {
            $(target).datagrid('hideColumn', item.name);
            $(this).menu('setIcon', {
              target: item.target,
              iconCls: 'fal fa-square fa-lg'
            });
          } else if (item.iconCls === 'fal fa-square fa-lg') {
            $(target).datagrid('showColumn', item.name);
            $(this).menu('setIcon', {
              target: item.target,
              iconCls: 'fal fa-check-square fa-lg'
            });
          } else if (item.iconCls === 'fal fa-save fa-lg') {
            //保存配置
          }
          let opts = [];
          for (let i = 0; i < fields.length; i++) {
            const field = fields[i];
            const col = $(target).datagrid('getColumnOption', field);
            opts.push(col);
          }
          //将调整好的属性保存到localstorage中
          localStorage.setItem($(target).datagrid('options').id, JSON.stringify(opts));
        }
      });
      state.columnMenu.menu('appendItem', {
        text: '保存配置',
        name: 'saveconfigitem',
        iconCls: 'fal fa-save fa-lg'
      });
      for (let i = 0; i < fields.length; i++) {
        const field = fields[i];
        const col = $(target).datagrid('getColumnOption', field);
        if (col.title !== undefined)
          state.columnMenu.menu('appendItem', {
            text: col.title,
            name: field,
            iconCls: !col.hidden ? 'fal fa-check-square fa-lg' : 'fal fa-square fa-lg'
          });
      }
    }
    return state.columnMenu;
  }

  $.extend($.fn.datagrid.methods, {
    columnMenu: function (jq) {
      return buildMenu(jq[0]);

    },
    resetColumns: function (jq) {
      return jq.each(function () {
        const opts = $(this).datagrid('options');
        const local = JSON.parse(localStorage.getItem(opts.id));
        //冻结的列不参与设置
        //const fields = $(this).datagrid('getColumnFields', true).concat($(this).datagrid('getColumnFields', false));
        //const fields = $(this).datagrid('getColumnFields');
        if (local !== null) {
          //load  sort datagrid columns 
          let sortcolumns = [];
          for (let i = 0; i < local.length; i++) {
            const field = local[i].field;
            const localboxwidth = local[i].boxWidth;
            const localwidth = local[i].width;
            const localhidden = local[i].hidden || false;
            let col = $(this).datagrid('getColumnOption', field);
            //修改列的宽度和隐藏属性
            col.boxWidth = localboxwidth;
            col.width = localwidth;
            col.hidden = localhidden;
            sortcolumns.push(col);
          }
          $(this).datagrid({
            columns: [sortcolumns]
          }).datagrid('columnMoving');
        }

   
        
        
          
      });
    }
  });

})(jQuery);
