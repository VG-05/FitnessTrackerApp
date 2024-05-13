$(() => {
    loadChart();
})
function loadChart() {
    $.when(
        $.ajax({
            type: "GET",
            url: "/Goal/GetAll",
            contextType: "application/json; charset=utf-8",
            dataType: "json"
        }),
        $.ajax({
            type: "GET",
            url: '/BodyWeight/GetAll',
            contextType: "application/json; charset=utf-8",
            dataType: "json"
        })
    )
        .done(onSuccessResult);

    function onSuccessResult(goal, bodyWeights) {
        let _chartLabels = new Array();
        let _goalChartData = new Array();
        let _progressChartData = new Array();
        let numberOfPounds = 0;
        let numberOfKgs = 0;

        // calculating how many weight logs are in kgs or lbs and populating _chartLabels
        bodyWeights[0].data.forEach(function (bodyWeight) {
            if (bodyWeight.unit === "kgs") {
                numberOfKgs += 1;
            } else {
                numberOfPounds += 1;
            }
            _chartLabels.push(bodyWeight.date);
        });

        // changing the weight logs to dominant unit and populating _progressChartData
        bodyWeights[0].data.forEach(function (bodyWeight) {
            if (numberOfPounds > numberOfKgs) {
                if (bodyWeight.unit == "kgs") {
                    _progressChartData.push(bodyWeight.weight * 2.205);
                } else {
                    _progressChartData.push(bodyWeight.weight);
                }
            } else {
                if (bodyWeight.unit == "kgs") {
                    _progressChartData.push(bodyWeight.weight);
                } else {
                    _progressChartData.push(bodyWeight.weight / 2.205);
                }
            }
        });

        // include initial bodyweight (in dominant unit) in _goalChartData
        if (numberOfPounds > numberOfKgs) {
            if (bodyWeights[0].data[0].unit == "kgs") {
                _goalChartData.push(_progressChartData[0] * 2.205);
            } else {
                _goalChartData.push(_progressChartData[0]);
            }
        } else {
            if (bodyWeights[0].data[0].unit == "kgs") {
                _goalChartData.push(_progressChartData[0]);
            } else {
                _goalChartData.push(_progressChartData[0] / 2.205);
            }
        }
        // adding goal weight log to _goalChartData and _chartLabels
        goal[0].data.forEach(function (goalWeight) {
            if (numberOfPounds > numberOfKgs) {
                if (goalWeight.unit == "kgs") {
                    _goalChartData.push(goalWeight.targetWeight * 2.205);
                } else {
                    _goalChartData.push(goalWeight.targetWeight);
                }
            } else {
                if (goalWeight.unit == "kgs") {
                    _goalChartData.push(goalWeight.targetWeight);
                } else {
                    _goalChartData.push(goalWeight.targetWeight / 2.205);
                }
            }
            _chartLabels.push(goalWeight.targetDate);
        });

        let _progressChart = new Array();
        for (let i = 0; i < _progressChartData.length; i++) {
            _progressChart.push({
                x: _chartLabels[i],
                y: _progressChartData[i]
            });
        }

        let _goalChart = new Array();
        _goalChart.push({ x: _chartLabels[0], y: _goalChartData[0] });
        _goalChart.push({ x: _chartLabels[_chartLabels.length - 1], y: _goalChartData[1] });



        new Chart("goalChart", {
            type: 'line',
            options: {
                scales: {
                    x: {
                        type: 'time',
                        time: {
                            tooltipFormat: 'DD T'
                        }
                    }
                },
                plugins: {
                    legend: {
                        position: 'bottom'
                    },
                    title: {
                        display: true,
                        text: "Body Weight Progress",
                        font: {
                            size: 14
                        },
                        padding: 14
                    }
                },
                elements: {
                    point: {
                        pointBackgroundColor: 'rgb(75,192,192)'
                    }
                }
            },
            data: {
                labels: _chartLabels,
                datasets: [{
                    label: 'Body Weight',
                    data: _progressChart,
                    borderColor: 'rgb(75,192,192)',
                    tension: 0.1
                }, {
                    label: 'Target Weight',
                    data: _goalChart,
                    borderColor: 'rgb(255, 205, 86)',
                    borderDash: [5, 5]
                }]
            }
        });
    }
}