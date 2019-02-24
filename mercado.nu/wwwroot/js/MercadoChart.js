let tables = document.getElementsByTagName("table");
for (var i = 0; i < tables.length; i++) {
    let number = i;
    let name = tables[i].id;
    getDataFromTable(name, number)
}

function getDataFromTable(name, id) {
    let firstTable = document.getElementById(`${name}`);
    let z = firstTable.rows.length;
    let x = firstTable.rows[0].cells.length;

    let column1 = [];
    let column2 = [];

    for (var i = 0; i < x; i++) {
        for (var j = 0; j < z; j++) {
            if (i === 0) {
                column1.push(firstTable.rows[j].cells[i].innerHTML);
            }
            if (i === 1) {
                column2.push(firstTable.rows[j].cells[i].innerHTML);
            }
        }
    }

    drawChart(column1, column2, id)

}

function drawChart(column1, column2, id) {
    let dataX = [];
    let dataY = [];


    for (var i = 1; i < column1.length; i++) {
        dataX.push(column1[i]);
    }

    for (var i = 1; i < column2.length; i++) {
        dataY.push(column2[i]);
    }
    var ctx = document.getElementById(`canvas${id}`).getContext('2d');
    var myChart = new Chart(ctx, {
        type: 'bar',
        data: {
            labels: dataX,
            datasets: [{
                label: '# of Votes',
                data: dataY,
                backgroundColor: [
                    'rgba(255, 99, 132, 0.2)',
                    'rgba(54, 162, 235, 0.2)',
                    'rgba(255, 206, 86, 0.2)',
                    'rgba(75, 192, 192, 0.2)',
                    'rgba(153, 102, 255, 0.2)',
                    'rgba(255, 159, 64, 0.2)'
                ],
                borderColor: [
                    'rgba(255,99,132,1)',
                    'rgba(54, 162, 235, 1)',
                    'rgba(255, 206, 86, 1)',
                    'rgba(75, 192, 192, 1)',
                    'rgba(153, 102, 255, 1)',
                    'rgba(255, 159, 64, 1)'
                ],
                borderWidth: 1
            }]
        },
        options: {
            scales: {
                yAxes: [{
                    ticks: {
                        beginAtZero: true
                    }
                }]
            }
        }
    });
}
