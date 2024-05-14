$(() => {
    $.ajax({
        type: "GET",
        url: "/BodyWeight/GetAll",
        contextType: "application/json; charset=utf-8",
        dataType: "json",
        success: onSuccessResult,
        error: onerror
    });
})



function onSuccessResult(data) {
    let _data = data.data;
    let _chartLabels = new Array();
    let _chartData = new Array();
    let numberOfPounds = 0;
    let numberOfKgs = 0;
    _data.forEach(function (obj) {
        if (obj.unit === "kgs") {              
            numberOfKgs+=1;
        } else {                       
            numberOfPounds+=1;
        }
        _chartLabels.push(obj.date);
    });

    let dominantUnit;
    if (numberOfPounds > numberOfKgs) {
        dominantUnit = "lb";
    } else {
        dominantUnit = "kg";
    }
    _data.forEach(function (obj) {
        if (numberOfPounds > numberOfKgs) {
            if (obj.unit == "kgs") {
                _chartData.push(obj.weight * 2.205);
            } else {
                _chartData.push(obj.weight);
            }
        } else {
            if (obj.unit == "kgs") {
                _chartData.push(obj.weight);
            } else {
                _chartData.push(obj.weight / 2.205);
            }
        }
    });
    let lastLogged = luxon.DateTime.fromISO(_data[_data.length - 1].date);

    let bodyweightchart = new Chart("bodyWeightChart", {
        type: 'line',
        options: {
            scales: {
                x: {
                    type: 'time',
                    time: {
                        tooltipFormat: 'DD T',
                        minUnit: "day",
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
                data: _chartData,
                file: false,
                borderColor: 'rgb(75,192,192)',
                tension: 0.1
            }]
        }
    });

    $("#bodyweight-weekly").on('click', () => {
        bodyweightchart.options.plugins.title.text = "This Week"
        bodyweightchart.options.scales.x.min = lastLogged.startOf("week").toISO();
        bodyweightchart.options.scales.x.time.displayFormats.day = "EEE, dd MMM";
        bodyweightchart.update();
    })
    $("#bodyweight-monthly").on('click', () => {
        bodyweightchart.options.plugins.title.text = "This Month"
        bodyweightchart.options.scales.x.min = lastLogged.startOf("month").toISO();
        delete bodyweightchart.options.scales.x.time.displayFormats.day;
        bodyweightchart.update();
    })
    $("#bodyweight-annual").on('click', () => {
        bodyweightchart.options.plugins.title.text = "This Year"
        bodyweightchart.options.scales.x.min = lastLogged.startOf("year").toISO();
        delete bodyweightchart.options.scales.x.time.displayFormats.day;
        bodyweightchart.update();
    })
    $("#bodyweight-all").on('click', () => {
        bodyweightchart.options.plugins.title.text = "All Time"
        delete bodyweightchart.options.scales.x.min;
        delete bodyweightchart.options.scales.x.time.displayFormats.day;
        bodyweightchart.update();
    })
}