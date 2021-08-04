var chart = c3.generate({
	data: {
		columns: [
			['data1', 30, 200, 100, 400, 150, 250, 50, 100, 250]
		],
		type: 'spline',
		selection: {
			enabled: true
		}
	},
	color: {
        pattern: [color.primary._500, color.info._500, color.success._500, color.danger._500, color.warning._500]
    },
	grid: {
		x: {
			show: false
		},
		y: {
			show: true
		}
	}

});

var defaultMessage = $('#message').html(),
	currentIndex = 0,
	timer, duration = 1500,
	demos = [
		function () {
			chart.load({
				columns: [
					['data2', 100, 30, 200, 320, 50, 150, 230, 80, 150]
				],
				type: 'spline'
			})
			setMessage('Load data2');
			setProgressBar('4')
		},
		function () {
			chart.load({
				columns: [
					['data3', 70, 90, 170, 220, 100, 110, 130, 40, 50]
				]
			})
			setMessage('Load data3');
			setProgressBar('8')
		},
		function () {
			chart.select(['data1'], [2]);
			setMessage('Select point for index 2 of data1');
			setProgressBar('12')
		},
		function () {
			chart.select(['data1'], [4, 6]);
			setMessage('Select point for index 4,6 of data1');
			setProgressBar('16')
		},
		function () {
			chart.unselect();
			setMessage('Unselect points');
			setProgressBar('20')
		},
		function () {
			chart.focus('data2');
			setMessage('Focus on data2');
			setProgressBar('24')
		},
		function () {
			chart.focus('data3');
			setMessage('Focus on data3');
			setProgressBar('28')
		},
		function () {
			chart.revert();
			setMessage('Defocus');
			setProgressBar('32')
		},
		function () {
			chart.load({
				columns: [
					['data1', 300, 230, 400, 520, 230, 250, 330, 280, 250]
				]
			})
			setMessage('Update data1');
			setProgressBar('36')
		},
		function () {
			chart.load({
				columns: [
					['data2', 30, 50, 90, 120, 40, 50, 80, 70, 50]
				]
			})
			setMessage('Update data2');
			setProgressBar('40')
		},
		function () {
			chart.regions([{
				start: 1,
				end: 3
			}]);
			setMessage('Add region from 1 to 3');
			setProgressBar('44')
		},
		function () {
			chart.regions.add([{
				start: 6
			}]);
			setMessage('Add region from 6 to end');
			setProgressBar('48')
		},
		function () {
			chart.regions([]);
			setMessage('Clear regions');
			setProgressBar('52')
		},
		function () {
			chart.xgrids([{
				value: 1,
				text: 'Label 1'
			}, {
				value: 4,
				text: 'Label 4'
			}]);
			setMessage('Add x grid lines for 1, 4');
			setProgressBar('56')
		},
		function () {
			chart.ygrids.add([{
				value: 450,
				text: 'Label 450'
			}]);
			setMessage('Add y grid lines for 450');
			setProgressBar('60')
		},
		function () {
			chart.xgrids.remove({
				value: 1
			});
			chart.xgrids.remove({
				value: 4
			});
			setMessage('Remove grid lines for 1, 4');
			setProgressBar('64')
		},
		function () {
			chart.ygrids.remove({
				value: 450
			});
			setMessage('Remove grid line for 450');
			setProgressBar('68')
		},
		function () {
			chart.transform('bar');
			setMessage('Show as bar chart');
			setProgressBar('72')
		},
		function () {
			chart.groups([
				['data2', 'data3']
			]);
			setMessage('Grouping data2 and data3');
			setProgressBar('76')
		},
		function () {
			chart.groups([
				['data1', 'data2', 'data3']
			]);
			setMessage('Grouping data1, data2 and data3');
		},
		function () {
			chart.groups([
				['data2', 'data3']
			]);
			chart.transform('spline', 'data1');
			setMessage('Show data1 as spline');
			setProgressBar('80')
		},
		function () {
			chart.unload({
				ids: 'data3'
			});
			setMessage('Unload data3');
			setProgressBar('84')
		},
		function () {
			chart.unload({
				ids: 'data2'
			});
			setMessage('Unload data2');
			setProgressBar('88')
		},
		function () {
			chart.flow({
				columns: [
					['data1', 390, 400, 200, 500]
				],
				duration: 1000,
			});
			setMessage('Flow 4 data');
			setProgressBar('92')
		},
		function () {
			// wait for end of transition for flow
		},
		function () {
			chart.flow({
				columns: [
					['data1', 190, 230]
				],
			});
			setMessage('Flow 2 data');
			setProgressBar('96')
		},
		function () {
			// wait for end of transition for flow
		},
		function () {
			chart.transform('spline', ['data1', 'data2', 'data3']);
			chart.groups([
				['data1'],
				['data2'],
				['data3']
			]);
			chart.load({
				columns: [
					['data1', 30, 200, 100, 400, 150, 250, 50, 100, 250]
				]
			})
			setMessage('Finishing demo..');
			setProgressBar('100')
			stopDemo()
		}
	];

function setMessage(message) {
	document.getElementById('message').innerHTML = '<div id="demoMessage" class="shadow-lg fs-xl p-3 rounded fadeinup">' + message + '</div>';
}

function setProgressBar(percentage) {
	$('#demo-progress').css("width", percentage + "%");
}

function startDemo() {
	setMessage('Starting Demo...');
	timer = setInterval(function () {
		if (currentIndex == demos.length) currentIndex = 0;
		demos[currentIndex++]();
	}, duration);
	$('#playDemo').hide();
	$('#pauseDemo').show();
}

function stopDemo() {
	clearInterval(timer);
	document.getElementById('message').innerHTML = '<div id="demoMessage" class="shadow-lg fs-xl p-3 rounded fadeinup bg-success-500 text-center">Thanks for watching! <br> <button class="btn btn-xs btn-dark mt-2" onclick="startDemo();">Play again</button> </div>';
	$('#playDemo').hide();
	$('#pauseDemo').hide();
};

function pauseDemo() {
	clearInterval(timer);
	document.getElementById('message').innerHTML = '<div id="demoMessage" class="shadow-lg fs-xl p-3 rounded highlight"> Demo Paused </div>';
	$('#playDemo').show();
	$('#pauseDemo').hide();
};