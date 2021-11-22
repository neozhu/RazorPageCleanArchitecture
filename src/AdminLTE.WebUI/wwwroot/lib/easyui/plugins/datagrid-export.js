(function($){
    function getRows(target){
        var state = $(target).data('datagrid');
        if (state.filterSource){
            return state.filterSource.rows;
        } else {
            return state.data.rows;
        }
    }
  function toString(val) {
    var regex1 = /^\/Date\((d|-|.*)\)[\/|\\]$/;
    var regex2 = /^[0-9]{4}-(0[1-9]|1[0-2])-(0[1-9]|[1-2][0-9]|3[0-1]) (2[0-3]|[01][0-9]):[0-5][0-9]:[0-5][0-9]$/;
    if (regex1.exec(val)) {
      return moment(val).format('YYYY-MM-DD HH:mm').replace(' 00:00', '');
    } else if (regex2.exec(val)) {
      console.log(val);
      return moment(val).format('YYYY-MM-DD HH:mm').replace(' 00:00', '');
    }
    else if (typeof val == "undefined" || val == null) {
      return '';
    } else if (typeof val == 'number') {
      if (Number.isInteger(val)) {
        return numeral(val).format('0,0');
      } else {
        return numeral(val).format('0,0.00');
      }
    }
    return val;
  }
  function toHtml(target, rows) {
        rows = rows || getRows(target);
        var dg = $(target); 
    var data = ['<table  class="table table-bordered" rules="all" >'];
    var exclude = ['ck', '_operate1', '_operate2', '_operate3', '_cmd', '_buttons','Id','RId'];
    var fields = dg.datagrid('getColumnFields', true).concat(dg.datagrid('getColumnFields', false))
      .filter(function (el) {
        return exclude.indexOf(el) < 0;
      });
        var trStyle = 'height:32px';
        var tdStyle0 = 'vertical-align:middle;padding:0 4px';
    data.push('<thead><tr>');
        for(var i=0; i<fields.length; i++){
            var col = dg.datagrid('getColumnOption', fields[i]);
            var tdStyle = tdStyle0 + ';width:'+col.boxWidth+'px;';
            data.push('<th>'+col.title+'</th>');
        }
    data.push('</tr></thead>');
        $.map(rows, function(row){
            data.push('<tr style="'+trStyle+'">');
            for(var i=0; i<fields.length; i++){
                var field = fields[i];
                data.push(
                    '<td style="' + tdStyle0 + '">' +toString(row[field])+'</td>'
                );
            }
            data.push('</tr>');
        });
        data.push('</table>');
        return data.join('');
    }

    function toArray(target, rows){
        rows = rows || getRows(target);
      var dg = $(target);
      var exclude = ['ck', '_operate1', '_operate2', '_operate3', '_cmd', '_buttons', 'Id', 'RId'];
      var fields = dg.datagrid('getColumnFields', true).concat(dg.datagrid('getColumnFields', false))
        .filter(function (el) {
          return exclude.indexOf(el) < 0;
        });
        var data = [];
        var r = [];
        for(var i=0; i<fields.length; i++){
            var col = dg.datagrid('getColumnOption', fields[i]);
            r.push(col.title);
        }
        data.push(r);
        $.map(rows, function(row){
            var r = [];
            for(var i=0; i<fields.length; i++){
                r.push(row[fields[i]]);
            }
            data.push(r);
        });
        return data;
    }

  function print(target, param) {
        var title = null;
        var rows = null;
        if (typeof param == 'string'){
            title = param;
        } else {
            title = param['title'];
            rows = param['rows'];
        }
        var newWindow = window.open('', '', 'width=800, height=500');
        var document = newWindow.document.open();
        var content = 
            '<!doctype html>' +
            '<html>' +
          '<head>' +
          '<link href="/Content/css/bootstrap.css" rel="stylesheet" />' +
            '<meta charset="utf-8">' +
            '<title>'+title+'</title>' +
            '</head>' +
            '<body>' + toHtml(target, rows) + '</body>' +
            '</html>';
        document.write(content);
        document.close();
        newWindow.print();
    }

    function b64toBlob(data){
        var sliceSize = 512;
        var chars = atob(data);
        var byteArrays = [];
        for(var offset=0; offset<chars.length; offset+=sliceSize){
            var slice = chars.slice(offset, offset+sliceSize);
            var byteNumbers = new Array(slice.length);
            for(var i=0; i<slice.length; i++){
                byteNumbers[i] = slice.charCodeAt(i);
            }
            var byteArray = new Uint8Array(byteNumbers);
            byteArrays.push(byteArray);
        }
        return new Blob(byteArrays, {
            type: ''
        });
    }

    function toExcel(target, param){
        var filename = null;
        var rows = null;
        var worksheet = 'Worksheet';
        if (typeof param == 'string'){
            filename = param;
        } else {
            filename = param['filename'];
            rows = param['rows'];
            worksheet = param['worksheet'] || 'Worksheet';
        }
        var dg = $(target);
        var uri = 'data:application/vnd.ms-excel;base64,'
        , template = '<html xmlns:o="urn:schemas-microsoft-com:office:office" xmlns:x="urn:schemas-microsoft-com:office:excel" xmlns="http://www.w3.org/TR/REC-html40"><meta http-equiv="content-type" content="application/vnd.ms-excel; charset=UTF-8"><head><!--[if gte mso 9]><xml><x:ExcelWorkbook><x:ExcelWorksheets><x:ExcelWorksheet><x:Name>{worksheet}</x:Name><x:WorksheetOptions><x:DisplayGridlines/></x:WorksheetOptions></x:ExcelWorksheet></x:ExcelWorksheets></x:ExcelWorkbook></xml><![endif]--></head><body>{table}</body></html>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

        var table = toHtml(target, rows);
        var ctx = { worksheet: worksheet, table: table };
        var data = base64(format(template, ctx));
        if (window.navigator.msSaveBlob){
            var blob = b64toBlob(data);
            window.navigator.msSaveBlob(blob, filename);
        } else {
            var alink = $('<a style="display:none"></a>').appendTo('body');
            alink[0].href = uri + data;
            alink[0].download = filename;
            alink[0].click();
            alink.remove();
        }
    }

    $.extend($.fn.datagrid.methods, {
        toHtml: function(jq, rows){
            return toHtml(jq[0], rows);
        },
        toArray: function(jq, rows){
            return toArray(jq[0], rows);
        },
        toExcel: function(jq, param){
            return jq.each(function(){
                toExcel(this, param);
            });
        },
        print: function(jq, param){
            return jq.each(function(){
                print(this, param);
            });
        }
    });
})(jQuery);
