$(() => {
    loadChart();
})
function loadChart() {
    $.ajax({
        type: "GET",
        url: "/BodyWeight/GetAll",
        contextType: "application/json; charset=utf-8",
        dataType:"json",
        success: onSuccessResult,
        error: onerror
    });

    function onSuccessResult(data) {
        var _data = data;
        var _chartLabels = new Array();
        var _chartData = new Array();
        var numberOfPounds = 0;
        var numberOfKgs = 0;
        _data.data.forEach(function (obj) {
            if (obj.unit === "kgs") {              
                numberOfKgs+=1;
            } else {                       
                numberOfPounds+=1;
            }
            _chartLabels.push(obj.date);
        });
        _data.data.forEach(function (obj) {
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
        
           
        new Chart("bodyWeightChart", {
            type: 'line',
            options: {
                scales: {
                    x: {
                        type: 'time',
                        time: {
                            tooltipFormat: 'DD T'
                        }
                    },
                    y: {
                        grace: 15,
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
    }
}

