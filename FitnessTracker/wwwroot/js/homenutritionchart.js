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
        .done(onSuccess);
})

function onSuccess(goal, cumulativeMeals) {
    let _mealData = cumulativeMeals[0].data;
    let _goalData = goal[0].data;
    let _chartLabels = ["Calories", "Calories Remaining", "Carbs", "Carbs Remaining", "Protein", "Protein Remaining", "Fat", "Fat Remaining"];
    let inputdate = luxon.DateTime.now().startOf('day').toString().slice(0, 10);

    // finding relevant goal for the given date
    let relevantgoal = { dailyCalories: 2000, dailyCarbs: 275, dailyFats: 55, dailyProtein: 65 };
    for (let i = 0; i < _goalData.length; i++) {
        if (_goalData[i].targetDate > luxon.DateTime.fromISO(inputdate)) {
            relevantgoal = _goalData[i];
            break;
        }
        else {
            relevantgoal = _goalData[_goalData.length - 1];
        }
    }

    // finding relevant day meals for the given date
    let relevantmeal = {
        date: inputdate,
        totalCalories: 0,
        totalCarbs: 0,
        totalFats: 0,
        totalProtein: 0
    };
    for (let i = 0; i < _mealData.length; i++) {
        if (_mealData[i].date == inputdate) {
            relevantmeal = _mealData[i];
            break;
        }
    }


    const red = 'rgb(255, 99, 132)';
    const grey = 'rgb(206, 206, 206)';
    const orange = 'rgb(255, 159, 64)';
    const blue = 'rgb(54, 162, 235)';
    const yellow = 'rgb(255, 205, 86)';
    const lightred = 'rgb(253, 200, 200)';
    const lightturquoise = 'rgb(176, 249, 252)';
    const lightorange = 'rgb(253, 215, 157)';
    const lightblue = 'rgb(178, 232, 255)';
    const lightyellow = 'rgb(252, 238, 185)';
    const green = 'rgb(75, 192, 192)';


    let homeDailynutritionchart = new Chart("homeDailyNutritionChart", {
        type: "doughnut",
        options: {
            maintainAspectRatio: false,
            datasets: {
                doughnut: {
                    cutout: "20%"
                }
            },
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        generateLabels: function (chart) {
                            // Get the default label list
                            const original = Chart.overrides.pie.plugins.legend.labels.generateLabels;
                            const labelsOriginal = original.call(this, chart);
                            // Build an array of colors used in the datasets of the chart
                            let datasetColors = chart.data.datasets.map(function (e) {
                                return e.backgroundColor;
                            });
                            datasetColors = datasetColors.flat();

                            // Modify the color and hide state of each label
                            labelsOriginal.forEach(label => {
                                // There are twice as many labels as there are datasets. This converts the label index into the corresponding dataset index
                                label.datasetIndex = (label.index - label.index % 2) / 2;

                                // The hidden state must match the dataset's hidden state
                                label.hidden = !chart.isDatasetVisible(label.datasetIndex);

                                // Change the color to match the dataset
                                label.fillStyle = datasetColors[label.index];
                            });
                            return labelsOriginal;
                        },
                        filter: function (item, chart) {
                            return !item.text.includes('Calories Remaining') && !item.text.includes('Carbs Remaining') && !item.text.includes('Protein Remaining') && !item.text.includes('Fat Remaining');
                        }
                    },
                    onClick: function (mouseEvent, legendItem, legend) {
                        // toggle the visibility of the dataset from what it currently is
                        legend.chart.getDatasetMeta(
                            legendItem.datasetIndex
                        ).hidden = legend.chart.isDatasetVisible(legendItem.datasetIndex);
                        legend.chart.update();
                    }
                },
                tooltip: {
                    callbacks: {
                        label: function (context) {
                            const labelIndex = (context.datasetIndex * 2) + context.dataIndex;
                            return context.chart.data.labels[labelIndex] + ': ' + context.formattedValue;
                        },
                        title: function (context) {
                            return "";
                        }
                    }
                },
                title: {
                    display: true,
                    text: "Today's Nutrition Progress",
                    font: {
                        size: 18
                    },
                    padding: 0
                }
            }
        },
        data: {
            labels: _chartLabels,
            datasets: [{
                data: (relevantgoal.dailyCalories >= relevantmeal.totalCalories) ? [relevantmeal.totalCalories, relevantgoal.dailyCalories - relevantmeal.totalCalories] : [relevantmeal.totalCalories, 0],
                backgroundColor: [red, lightred]
            }, {
                data: (relevantgoal.dailyCarbs >= relevantmeal.totalCarbs) ? [relevantmeal.totalCarbs, relevantgoal.dailyCarbs - relevantmeal.totalCarbs] : [relevantmeal.totalCarbs, 0],
                backgroundColor: [orange, lightorange]
            }, {
                data: (relevantgoal.dailyProtein >= relevantmeal.totalProtein) ? [relevantmeal.totalProtein, relevantgoal.dailyProtein - relevantmeal.totalProtein] : [relevantmeal.totalProtein, 0],
                backgroundColor: [blue, lightblue]
            }, {
                data: (relevantgoal.dailyFats >= relevantmeal.totalFats) ? [relevantmeal.totalFats, relevantgoal.dailyFats - relevantmeal.totalFats] : [relevantmeal.totalFats, 0],
                backgroundColor: [green, lightturquoise]
            }
            ]
        }
    });
}