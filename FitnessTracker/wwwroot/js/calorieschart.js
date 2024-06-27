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
            url: '/Meal/GetAll',
            contextType: "application/json; charset=utf-8",
            dataType: "json"
        })
    )
    .done(generateCaloriesBarGraph);
})

function generateCaloriesBarGraph(goal, cumulativeMeals) {
    let _progressData = cumulativeMeals[0].data;
    let _goalData = goal[0].data;
    let _chartLabels = new Array();
    let _progressChart = new Array();
    let _goalChart = new Array();

    // populating _chartLabels and _progressChart with coordinate (x: date, y: total calories on that day)
    _progressData.forEach(function (todayMeals) {
        _chartLabels.push(todayMeals.date);
        _progressChart.push({ x: todayMeals.date, y: todayMeals.totalCalories });
    });


    // include starting point for _goalChart
    if (_progressData.length > 0) {
        _goalChart.push({ x: _progressData[0].date, y: _goalData[0].dailyCalories });
    }

    // adding goal weight logs to _goalChart and _chartLabels
    _goalData.forEach(function (goal) {
        _chartLabels.push(goal.targetDate);
        _goalChart.push({ x: goal.targetDate, y: goal.dailyCalories })
    });

    let lastLogged = luxon.DateTime.Now;
    if (_progressData.length > 0) {
        lastLogged = luxon.DateTime.fromISO(_progressData[_progressData.length - 1].date);
    }

    let goalchart = new Chart("caloriesChart", {
        options: {
            maintainAspectRatio: false,
            scales: {
                x: {
                    type: 'time',
                    time: {
                        tooltipFormat: 'DD T',
                        minUnit: "day",
                        displayFormats: {
                            day: "EEE, dd MMM"
                        }
                    },
                    min: lastLogged.startOf("week").toISO(),
                    max: lastLogged.endOf("week").toISO()
                },
                y: {
                    title: {
                        display: true,
                        text: "Calories (cal)"
                    },
                    grace: 100
                }
            },
            plugins: {
                legend: {
                    position: 'bottom'
                },
                title: {
                    display: true,
                    text: "This Week"
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
                type: 'bar',
                label: 'Calories',
                data: _progressChart,
                backgroundColor: 'rgb(75,192,192)',
                tension: 0.1
            }, {
                type: 'line',
                label: 'Calories Required',
                data: _goalChart,
                borderColor: 'rgb(255, 205, 86)',
                borderDash: [5, 5]
            }]
        }
    });


    $("#Calories-Weekly").on('click', () => {
        goalchart.options.plugins.title.text = "This Week"
        goalchart.options.scales.x.min = lastLogged.startOf("week").toISO();
        goalchart.options.scales.x.max = lastLogged.endOf("week").toISO();
        goalchart.options.scales.x.time.displayFormats.day = "EEE, dd MMM";
        goalchart.options.scales.x.time.minUnit = "day";
        goalchart.update();
    })
    $("#Calories-Monthly").on('click', () => {
        goalchart.options.plugins.title.text = "This Month"
        goalchart.options.scales.x.min = lastLogged.startOf("month").toISO();
        goalchart.options.scales.x.max = lastLogged.endOf("month").toISO();
        goalchart.options.scales.x.time.minUnit = "day";
        delete goalchart.options.scales.x.time.displayFormats.day;
        goalchart.update();
    })
    $("#Calories-Annual").on('click', () => {
        goalchart.options.plugins.title.text = "This Year"
        goalchart.options.scales.x.min = lastLogged.startOf("year").toISO();
        goalchart.options.scales.x.max = lastLogged.endOf("year").toISO();
        goalchart.options.scales.x.time.minUnit = "month";
        delete goalchart.options.scales.x.time.displayFormats.day;
        goalchart.update();
    })
    $("#Calories-AllTime").on('click', () => {
        goalchart.options.plugins.title.text = "All Time"
        delete goalchart.options.scales.x.min;
        delete goalchart.options.scales.x.max;
        delete goalchart.options.scales.x.time.displayFormats.day;
        goalchart.update();
    })
}
