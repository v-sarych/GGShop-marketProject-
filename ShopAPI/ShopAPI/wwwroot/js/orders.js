window.displayUserOrders = function () {
    let user = JSON.parse(sessionStorage.getItem('user'));
    
    let tableBody = document.querySelector('.table__body_data--user_orders');
    
    user.orders.forEach(function(order) {
        var newRow = document.createElement('tr');
        var idCell = document.createElement('td');
        var costCell = document.createElement('td');
        var date = document.createElement('td');
        var statusCell = document.createElement('td');

        newRow.classList.add('table__body_row')
        idCell.classList.add('table__cell');
        costCell.classList.add('table__cell');
        date.classList.add('table__cell');
        statusCell.classList.add('table__cell');
        
        const today = new Date();

        const formattedDate = `${today.getDate()}.${today.getMonth() + 1}.${today.getFullYear()}`;

        idCell.textContent = order.id;
        costCell.textContent = order.cost;
        date.textContent = formattedDate;
        statusCell.textContent = order.status;
        
        newRow.appendChild(idCell);
        newRow.appendChild(costCell);
        newRow.appendChild(date);
        newRow.appendChild(statusCell);
        
        tableBody.appendChild(newRow);
    });
}
