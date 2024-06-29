$(() => {
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
})
function onSuccessResult(goal, bodyWeights) {
    let _progressData = bodyWeights[0].data;
    let _goalData = goal[0].data;
    let _chartLabels = new Array();
    let _progressChart = new Array();
    let _goalChart = new Array();
    let dominantUnit;
    if (_progressData.length > 0) {
        dominantUnit = _progressData[_progressData.length - 1].unit;
    }
    else {
        dominantUnit = "kgs";
    }

    let weight = 0;


    // changing the weight logs to dominant unit and populating _chartLabels and _progressChart with coordinate (x: date, y: weight)
    _progressData.forEach(function (bodyWeight) {
        _chartLabels.push(bodyWeight.date);
        if (dominantUnit == "lbs") {
            if (bodyWeight.unit == "kgs") {
                weight = bodyWeight.weight * 2.205
            } else {
                weight = bodyWeight.weight;
            }
        } else {
            if (bodyWeight.unit == "kgs") {
                weight = bodyWeight.weight;
            } else {
                weight = bodyWeight.weight / 2.205;
            }
        }
        _progressChart.push({ x: bodyWeight.date, y: weight });
    });


    // include initial bodyweight (in dominant unit) in _goalChart
    if (_progressData.length > 0) {
        if (dominantUnit == "lbs") {
            if (_progressData[0].unit == "kgs") {
                weight = _progressData[0].weight * 2.205;
            } else {
                weight = _progressData[0].weight;
            }
        } else {
            if (_progressData[0].unit == "kgs") {
                weight = _progressData[0].weight;
            } else {
                weight = _progressData[0].weight / 2.205;
            }
        }
        _goalChart.push({ x: _progressData[0].date, y: weight });
    }

    // adding goal weight logs to _goalChart and _chartLabels
    _goalData.forEach(function (goalWeight) {
        if (dominantUnit == "lbs") {
            if (goalWeight.unit == "kgs") {
                weight = goalWeight.targetWeight * 2.205;
            } else {
                weight = goalWeight.targetWeight;
            }
        } else {
            if (goalWeight.unit == "kgs") {
                weight = goalWeight.targetWeight;
            } else {
                weight = goalWeight.targetWeight / 2.205;
            }
        }
        _chartLabels.push(goalWeight.targetDate);
        _goalChart.push({ x: goalWeight.targetDate, y: weight })
    });
    const ctx = document.getElementById('homeGoalChart').getContext('2d');

    let homeGoalchart = new Chart(ctx, {
        type: 'line',
        options: {
            maintainAspectRatio: false,
            scales: {
                x: {
                    type: 'time',
                    time: {
                        tooltipFormat: 'DD T',
                        minUnit: "day"
                    }
                },
                y: {
                    title: {
                        display: true,
                        text: "Bodyweight (" + dominantUnit + ")"
                    },
                    grace: 15
                }
            },
            plugins: {
                legend: {
                    position: 'bottom'
                },
                title: {
                    display: true,
                    text: "Bodyweight Progress vs Goal",
                    font: {
                        size: 18,
                    },
                    padding: {
                        top: 5,
                        bottom: 5
                    }
                },
                subtitle: {
                    display: true,
                    text: "All Time"
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