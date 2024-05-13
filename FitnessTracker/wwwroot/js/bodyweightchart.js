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
                    grace: 15
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
                data: _chartData,
                file: false,
                borderColor: 'rgb(75,192,192)',
                tension: 0.1
            }]
        }
    });

    $("#Weekly").on('click', () => {
        bodyweightchart.options.scales.x.min = lastLogged.startOf("week").toISO();
        bodyweightchart.options.scales.x.time.displayFormats.day = "EEE, dd MMM";
        bodyweightchart.update();
    })
    $("#Monthly").on('click', () => {
        bodyweightchart.options.scales.x.min = lastLogged.startOf("month").toISO();
        delete bodyweightchart.options.scales.x.time.displayFormats.day;
        bodyweightchart.update();
    })
    $("#Annual").on('click', () => {
        bodyweightchart.options.scales.x.min = lastLogged.startOf("year").toISO();
        delete bodyweightchart.options.scales.x.time.displayFormats.day;
        bodyweightchart.update();
    })
    $("#AllTime").on('click', () => {
        delete bodyweightchart.options.scales.x.min;
        delete bodyweightchart.options.scales.x.time.displayFormats.day;
        bodyweightchart.update();
    })
}