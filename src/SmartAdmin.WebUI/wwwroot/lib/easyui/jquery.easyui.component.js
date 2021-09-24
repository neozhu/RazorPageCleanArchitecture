//extend validatebox regex
$.extend($.fn.validatebox.defaults.rules, {
  regex: {//正则表达验证
    validator: function (value, param) {
      var re = new RegExp(param[0]);
      return re.test(value);
    },
    message: '{1}'
  },
  username: {// 验证用户名  
    validator: function (value) {
      return /^[\u4E00-\u9FA5a-zA-Z0-9_]{2,20}$/i.test(value);
    },
    message: '用户名不合法(字母开头，允许2-20位，允许字母数字下划线)'
  },
  carNo: {
    validator: function (value) {
      return /^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$/.test(value);
    },
    message: '车牌号码无效（例：苏E1235X）'
  },
  carengine: {
    validator: function (value) {
      return /^[a-zA-Z0-9]{16}$/.test(value);
    },
    message: '发动机型号无效(例：FG6H012345654584)'
  },
  ip: {// 验证IP地址  
    validator: function (value) {
      return /d+.d+.d+.d+/i.test(value);
    },
    message: 'IP地址格式不正确'
  },
  zip: {// 验证邮政编码  
    validator: function (value) {
      return /^[1-9]\d{5}$/i.test(value);
    },
    message: '邮政编码格式不正确'
  },
  chinese: {// 验证中文  
    validator: function (value) {
      return /^[\u0391-\uFFE5]+$/i.test(value);
    },
    message: '请输入中文'
  },
  english: {// 验证英语  
    validator: function (value) {
      return /^[A-Za-z]+$/i.test(value);
    },
    message: '请输入英文'
  },
  mobile: {// 验证手机号码  
    validator: function (value) {
      return /^(13|14|15|16|17|18|19)\d{9}$/i.test(value);
    },
    message: '手机号码格式不正确'
  },
  idcard: {// 验证身份证  
    validator: function (value) {
      var regex = /(^[1-9]\d{5}(18|19|([23]\d))\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}[0-9Xx]$)|(^[1-9]\d{5}\d{2}((0[1-9])|(10|11|12))(([0-2][1-9])|10|20|30|31)\d{3}$)/;
      return regex.test(value);
    },
    message: '身份证号码格式不正确'
  },
  qq: {// 验证QQ,从10000开始  
    validator: function (value) {
      return /^[1-9]\d{4,9}$/i.test(value);
    },
    message: 'QQ号码格式不正确'
  },
  cardno: {// 银行卡号  
    validator: function (value) {
      var regex = /^(998801|998802|622525|622526|435744|435745|483536|528020|526855|622156|622155|356869|531659|622157|627066|627067|627068|627069)\d{10}$/;
      return reg.test(value);
    },
    message: '银行卡号码格式不正确'
  },
  tel: {//验证电话号码
    validator: function (value, param) {
      this._invalidMessage = '电话号码输入规则:</br>手机号:13/15/18xxxxxxxxx</br>电话号:区号-电话号-分机号</br>(分机号/区号可选填)';
      return /(^(\d{3}-|\d{4}-)?(\d{8}|\d{7})?(-\d{1,6})?$)|(^(?:13\d|14\d|15\d|16\d|17\d|18\d|19\d)-?\d{5}(\d{3}|\*{3})$)/.test(value);
    },
    message: '电话号码输入规则:</br>手机号:13/15/18xxxxxxxxx</br>电话号:区号-电话号-分机号</br>(分机号/区号可选填)'
  },
  number: {//验证数字
    validator: function (value, param) {
      this._invalidMessage = '请输入数字';
      return /^[0-9]+(.[0-9]{1,10})?$/.test(value);
    },
    message: '请输入数字'
  },
  money: {//验证金额
    validator: function (value, param) {
      this._invalidMessage = '请输入正确的金额';
      return (/^(0|-?[1-9])+(.[0-9]{1,2})?$/).test(value);
    },
    message: '请输入正确的金额'

  },
  mone: {//只允许整数或小数
    validator: function (value, param) {
      this._invalidMessage = '请输入整数或小数';
      return (/^(([1-9]\d*)|\d)(\.\d{1,5})?$/).test(value);
    },
    message: '请输入整数或小数'

  },
  currency: {// 验证正的整数或正的任意位数的小数  
    validator: function (value) {
      return /^[0-9]+([.]{1}[0-9]+){0,1}$/i.test(numeral(value).value());
    },
    message: '输入格式不正确'
  },
  integer: {//不允许小数或0
    validator: function (value, param) {
      this._invalidMessage = '请输入最小为1的整数';
      return /^[+]?[1-9]\d*$/.test(numeral(value).value());
    },
    message: '请输入最小为1的整数'
  },
  decimal: {
    validator: function (value) {
      this._invalidMessage = '不能小于等于[0]';
      return /^\s*(?=.*[1-9])\d*(?:\.\d{1,5})?\s*$/.test(numeral(value).value());
    },
    message: '不能小于等于[0]'
  },
  minLength: {//最小字符数
    validator: function (value, param) {
      this._invalidMessage = '至少输入' + param[0] + '个字';
      return value.length >= param[0];
    },
    message: '至少输入{0}个字'
  },
  maxLength: {//最多字符数
    validator: function (value, param) {
      this._invalidMessage = '最多' + param[0] + '个字';
      return value.length <= param[0];
    },
    message: '最多{0}个字'
  },
  noZero: {//判断非零的正数
    validator: function (value) {
      this._invalidMessage = '输入值不能为[0]';
      return /(^[1-9]\d*\.\d*|0\.\d*[1-9]\d*$)|([1-9]\d*)/.test(value);
    },
    message: '输入值不能为[0]'
  },
  password: {
    validator: function (value) {
      this._invalidMessage = '只允许8/16/32位的字母数字组合';
      return /^[a-zA-Z0-9]{8}$|^[a-zA-Z0-9]{16}$|^[a-zA-Z0-9]{32}$/.test(value);
    },
    message: '只允许8/16/32位的字母数字组合！'
  },
  combocheck: {
    validator: function (value, param) {
      var obj = $(param[0]).combobox('getData');
      var val = $(param[0]).combobox('getValue');
      var textfield = $(param[0]).combobox('options').textField;
      var valuefield = $(param[0]).combobox('options').valueField;
      var arr = obj.filter(function (item) {
        return item[textfield] == value && item[valuefield] == val;
      });
      if (arr.length > 0) { return true; }
      else { return false; }
    },
    message: '{1}'
  },
  greaterthan: {
    validator: function (value, param) {
      var v1 = $(param[0]).datebox('getValue');
      var d1 = $.fn.datebox.defaults.parser(v1);
      var d2 = $.fn.datebox.defaults.parser(value);
      return d2 >= d1;
    },
    message: '必须大于{1}'
  }
});

//datebox datagrid editor
$.extend($.fn.datagrid.defaults.editors, {
  datebox: {
    init: function (container, options) {
      var input = $('<input>').appendTo(container);
      input.datebox(options);
      input.datebox('textbox').bind('keydown', function (e) {
        if (e.keyCode === 13) {
          $(e.target).emulateTab();
        }
      });
      return input;
    },
    destroy: function (target) {
      $(target).datebox('destroy');
    },
    getValue: function (target) {
      return $(target).datebox('getValue');
    },
    setValue: function (target, value) {
      if (moment(value).isValid()) {
        var notmin = !moment(value).isSame(moment('/Date(-62135596800000)/'));
        if (notmin) {
          var date = moment(value).format('YYYY-MM-DD');
          $(target).datebox('setValue', date);
        } else {
          $(target).datebox('setValue', null);
        }
       
      } else {
        $(target).datebox('setValue', null);
      }
    },
    resize: function (target, width) {
      $(target).datebox('resize', width);
    }
  }
});
//datetimebox datagrid editor
$.extend($.fn.datagrid.defaults.editors, {
  datetimebox: {
    init: function (container, options) {
      var input = $('<input>').appendTo(container);
      input.datetimebox(options);
      input.datetimebox('textbox').bind('keydown', function (e) {
        if (e.keyCode === 13) {
          $(e.target).emulateTab();
        }
      });
      return input;
    },
    destroy: function (target) {
      $(target).datetimebox('destroy');
    },
    getValue: function (target) {
      return $(target).datetimebox('getValue');
    },
    setValue: function (target, value) {
      if (moment(value).isValid()) {
        var notmin = !moment(value).isSame(moment('/Date(-62135596800000)/'));
        if (notmin) {
          var date = moment(value).format('YYYY-MM-DD HH:mm:ss');
          $(target).datebox('setValue', date);
        } else {
          $(target).datebox('setValue', null);
        }
      } else {
        $(target).datetimebox('setValue', null);
      }
    },
    resize: function (target, width) {
      $(target).datetimebox('resize', width);
    }
  }
});
//combobox filter
$.extend($.fn.combobox.defaults, {
  filter: function (q, row) {
    var opts = $(this).combobox('options');
    return row[opts.textField].toLowerCase().indexOf(q) === 0 || row[opts.valueField].toString().toLowerCase().indexOf(q) === 0;
  }
});
//filesize formatter
function filesizeformatter(value) {
  return filesize(value)
}

//number formmater
function numberformatter(value) {
  return numeral(value).format('0,0.00');
}
function parsernumber(str) {
  return numeral(str).value();
}
//int formatter
function intformatter(value) {
  return numeral(value).format('0,0');
}
function istrue(value) {
  if (typeof (value) === 'string') {
    value = value.trim().toLowerCase();
  }
  switch (true) {
    case true == value:
    case "true" == value:
    case value > 0:
    case "ok" == value:
    case "yes" == value:
    case "是" == value:
      return true;
    case "on" == value:
    case false == value:
    case "cancel" == value:
    case "null" == value:
    case "" == value:
    case "否" == value:
    case value <= 0:
      return false;
    default:
      return false;
  }
}
//booleanfilter
$.extend($.fn.datagrid.defaults.filters, {
  booleanfilter: {
    init: function (container, options) {
      var input = $('<select class="easyui-combobox" >').appendTo(container);
      var myoptions = {
        panelHeight: "auto",
        data: [{ value: '', text: 'All' }, { value: 'true', text: '是' }, { value: 'false', text: '否' }],
        onChange: function () {
          input.trigger('combobox.filter');
        }
      };
      $.extend(options, myoptions);
      input.combobox(options);
      return input;
    },
    destroy: function (target) {
      $(target).combobox('destroy');
    },
    getValue: function (target) {
      return $(target).combobox('getValue');
    },
    setValue: function (target, value) {
      $(target).combobox('setValue', value);
    },
    resize: function (target, width) {
      $(target).combobox('resize', width);
    }
  }
});
//CheckBox Editor
$.extend($.fn.datagrid.defaults.editors, {
  checkboxeditor: {
    init: function (container, options) {
      var checked = `<div class="datagrid-cell">
                      <div class="custom-control custom-checkbox">
                        <input type="checkbox"   class="custom-control-input" id="${options.id}">
                        <label class="custom-control-label" for="${options.id}"></label>
                      </div>
                    </div>`;
      var input = $(checked).appendTo(container);
      return input;
    },
    destroy: function (target) {

    },
    getValue: function (target) {
      return $(target[0]).find(':checkbox').prop('checked');
    },
    setValue: function (target, value) {
      $(target[0]).find(':checkbox').prop('checked', value);
    },
    resize: function (target, width) {

    }
  }
});
//checkbox editor
$.extend($.fn.datagrid.defaults.editors, {
  checkbox: {
    init: function (container, options) {
      console.log(options, container)
      var checked = `<div class="datagrid-cell"><div class="custom-control custom-checkbox">
                      <input type="checkbox"  class="custom-control-input" name="defaultUnchecked" id="${options.id}">
                      <label class="custom-control-label"  for="${options.id}"></label>
                     </div></div>`;
      var input = $(checked).appendTo(container);

      return input;
    },
    destroy: function (target) {

    },
    getValue: function (target) {
      //console.log('getValue', $(target[0]).find(':checkbox').prop('checked'));
      return $(target[0]).find(':checkbox').prop('checked');
    },
    setValue: function (target, value) {
      $(target[0]).find(':checkbox').prop('checked', value);



    },
    resize: function (target, width) {

    }
  }
});
//兼容性考虑等同于booleanformatter
function checkboxformatter(value, row, index) {
  if (istrue(value)) {

    const checked = `<div class="custom-control custom-checkbox">
                       <input type="checkbox" class="custom-control-input" name="defaultCheckedDisabled" checked="checked" disabled="">
                       <label class="custom-control-label" for="defaultCheckedDisabled"></label>
                   </div>`;
    return checked;
  } else {
    var unchecked = `<div class="custom-control custom-checkbox">
                       <input type="checkbox" class="custom-control-input" name="defaultCheckedDisabled"  disabled="">
                       <label class="custom-control-label" for="defaultCheckedDisabled"></label>
                   </div>`;

    return unchecked;
  }


}
//兼容性考虑等同于checkboxformatter
function booleanformatter(value, row, index) {
  if (istrue(value)) {

    const checked = `<div class="custom-control custom-checkbox">
                       <input type="checkbox" class="custom-control-input" name="defaultCheckedDisabled" checked="checked" disabled="">
                       <label class="custom-control-label" for="defaultCheckedDisabled"></label>
                   </div>`;
    return checked;
  } else {
    var unchecked = `<div class="custom-control custom-checkbox">
                       <input type="checkbox" class="custom-control-input" name="defaultCheckedDisabled" disabled="">
                       <label class="custom-control-label" for="defaultCheckedDisabled"></label>
                   </div>`;

    return unchecked;
  }


}
//switchbutton formatter for datagrid
function switchformatter(value, row, index) {
  if (istrue(value)) {
    return '<input class="easyui-switchbutton"  checked disabled />';
  } else {
    return '<input class="easyui-switchbutton"  disabled />';
  }
}
//switchbutton editor for datagrid
$.extend($.fn.datagrid.defaults.editors, {
  switchbutton: {
    init: function (container, options) {
      var switchbutton = '<input class="easyui-switchbutton" value="true">';
      var input = $(switchbutton).appendTo(container);
      var opts = {};
      input.switchbutton(opts);
      return input;
    },
    destroy: function (target) {

    },
    getValue: function (target) {
      //console.log('getvalue', $(target).switchbutton('options').checked);
      return $(target).switchbutton('options').checked;
    },
    setValue: function (target, value) {
      //console.log('set value', value);
      if (istrue(value))
        $(target).switchbutton('check');
      else
        $(target).switchbutton('uncheck');


    },
    resize: function (target, width) {

    }
  }
});

//date formatter
function dateformatter(value, row, index) {
  if (typeof value === "undefined") {
    return null;
  }
  else if (moment(value).isValid() && !moment(value).isSame(moment('/Date(-62135596800000)/'))) {
    return moment(value).format('YYYY-MM-DD');
  }
  else {
    return null;
  }

}
//datetime formatter
function datetimeformatter(value, row, index) {
  if (typeof value === "undefined") {
    return null;
  }
  else if (moment(value).isValid() && !moment(value).isSame(moment('/Date(-62135596800000)/'))) {
    return moment(value).format('YYYY-MM-DD HH:mm:ss');
  } else {
    return null;
  }
}


//easyui datebox default options
$.extend($.fn.datebox.defaults, {
  formatter: dateformatter ,
  parser: function (value) {
    if (typeof value !== "undefined"
      && moment(value).isValid()) {
      return moment(value).toDate();
    } else {
      return moment().toDate();
    }
  }
});
$.extend($.fn.datetimebox.defaults, {
  formatter: datetimeformatter,
  parser: function (value) {
    if (typeof value !== "undefined"
      && moment(value).isValid()) {
      return moment(value).toDate();
    } else {
      return moment().toDate();
    }
  }
});

//easyui form disable/enable/readonly
$.extend($.fn.form.methods, {
  enable: function (jq) {
    jq.form('disable', 'enable');
  },
  readonly: function (jq, omit) {
    var omit1 = '' || omit, mode = 'readonly';
    return jq.each(function () {
      var t = $(this);
      //t.find('input').not(omit)._propAttr('disabled','disabled');
      var plugins = ['textbox', 'combo', 'combobox', 'combotree', 'combogrid', 'datebox', 'datetimebox', 'spinner', 'timespinner', 'numberbox', 'numberspinner', 'slider', 'validatebox'];
      for (var i = 0; i < plugins.length; i++) {
        var plugin = plugins[i];
        var r = t.find('.' + plugin + '-f').not(omit1);
        if (r.length && r[plugin]) r[plugin](mode);
      }
      t.form('validate');
    });
  },
  disable: function (jq, omit) {
    var omit1 = '' || omit, mode = 'disable';
    if (omit1 === 'enable') mode = 'enable';
    return jq.each(function () {
      var t = $(this);
      //t.find('input').not(omit)._propAttr('disabled','disabled');
      var plugins = ['textbox', 'combo', 'combobox', 'combotree', 'combogrid', 'datebox', 'datetimebox', 'spinner', 'timespinner', 'numberbox', 'numberspinner', 'slider', 'validatebox'];
      for (var i = 0; i < plugins.length; i++) {
        var plugin = plugins[i];
        var r = t.find('.' + plugin + '-f').not(omit1);
        if (r.length && r[plugin]) r[plugin](mode);
      }
      t.form('validate');
    });
  }
});

//dateRange filter
$.extend($.fn.datagrid.defaults.filters, {
  dateRange: {
        init: function (container, options) {
      var cc = $('<span class="textbox combo datebox " style="padding:0px">\
                     <span class="textbox-addon textbox-addon-right" style="right: 0px; top: 0px;">\
                     <a href="javascript:" class="textbox-icon combo-arrow" icon-index="0" tabindex="-1" style="width: 26px; height: 29px;">\
                     </a>\
                     </span>\
                  </span>').appendTo(container);
      var input = $('<input type="text" value="" class="daterange" style="border:0px ;height:29px;padding:2px 8px">').appendTo(cc);
      var myoptions = {
        autoUpdateInput: false,
        applyClass: 'btn-sm btn-success',
        cancelClass: 'btn-sm btn-default',
        locale: {
          applyLabel: '确认',
          cancelLabel: '清空',
          fromLabel: '起始时间',
          toLabel: '结束时间',
          customRangeLabel: '自定义',
          firstDay: 1,
          daysOfWeek: ['日', '一', '二', '三', '四', '五', '六'],
          monthNames: ['一月', '二月', '三月', '四月', '五月', '六月',
            '七月', '八月', '九月', '十月', '十一月', '十二月'],
        },
        ranges: {
          //'最近1小时': [moment().subtract('hours',1), moment()],
          '今日': [moment(), moment()],
          '昨日': [moment().subtract(1, 'days').startOf('day'), moment().subtract(1, 'days').endOf('day')],
          '最近7日': [moment().subtract(6, 'days'), moment()],
          '最近30日': [moment().subtract(29, 'days'), moment()],
          '本月': [moment().startOf("month"), moment().endOf("month")],
          '上个月': [moment().subtract(1, "month").startOf("month"), moment().subtract(1, "month").endOf("month")]
        },
        opens: 'right',    // 日期选择框的弹出位置
        separator: '-',
        showWeekNumbers: false,     // 是否显示第几周
        format: 'YYYY/MM/DD'

      };
      input.on('blur', function () {
        $(this).parent().removeClass('textbox-focused');
      }).on('focus', function () {
        $(this).parent().addClass('textbox-focused');
      });
      input.daterangepicker(myoptions);

      container.find('.textbox-icon').on('click', function () {
        container.find('input').trigger('click.daterangepicker');
      });
      input.on('cancel.daterangepicker', function (ev, picker) {
        $(this).val('');
      });
      input.on('apply.daterangepicker', function (ev, picker) {
        console.log(picker.startDate.format('YYYY/MM/DD') + '-' + picker.endDate.format('YYYY/MM/DD'));
        $(this).val(picker.startDate.format('YYYY/MM/DD') + '-' + picker.endDate.format('YYYY/MM/DD'));
        //options.onChange(picker.startDate.format('YYYY/MM/DD') + '-' + picker.endDate.format('YYYY/MM/DD'));
      });


      //console.log($(target));
      return input;
    },
    destroy: function (target) {
      $(target).daterangepicker('destroy');
    },
    getValue: function (target) {
      return $(target).data('daterangepicker').getStartDate() + '-' + $(target).data('daterangepicker').getEndDate();
    },
    setValue: function (target, value) {
      //console.log($(target), value);
      var daterange = value.split('-');
      $(target).data('daterangepicker').setStartDate(daterange[0]);
      $(target).data('daterangepicker').setEndDate(daterange[1]);

    },
    resize: function (target, width) {
      $(target)._outerWidth(width - 2)._outerHeight(29);
      // $(target).daterangepicker('resize', width / 2);
    }
  }
});


