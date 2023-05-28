const uri = 'api/Orders';
let orders = [];
function getOrders() {
    fetch(uri)
        .then(response =>  response.json())
.then(data => _displayOrders(data))
.catch (error => console.error('Unable to get orders.', error));
}
function addOrder() {
    const addNameTextbox = document.getElementById('add - name');
    const addInfoTextbox = document.getElementById('add - statusOfOrderId');
    const order = {
        name: addNameTextbox.value.trim(),
        info: addInfoTextbox.value.trim(),
    };
    fetch(uri, {
        method: 'POST',
    headers: {

'Accept':'application / json',
'Content - Type ': 'application / json'
    },
    body: JSON.stringify(order)
})
.then(response => response.json())
.then(() => {
    getOrders();
    addNameTextbox.value = '';
    addInfoTextbox.value = '';
})
.catch (error => console.error('Unable to add orders.', error));
}
function deleteOrder(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
})
.then(() =>getOrders())
.catch (error => console.error('Unable to delete orders.', error));
}
function displayEditForm(id) {
    const order = orders.find(order =& gt; order.id === id);
    document.getElementById('edit - id').value = order.id;
    document.getElementById('edit - name').value = order.name;
    document.getElementById('edit - statusOfOrderId').value = order.statusOfOrderId;
    document.getElementById('editForm').style.display = 'block';
}
function updateOrder() {
    const orderId = document.getElementById('edit - id').value;
    const order = {
        id: parseInt(orderId, 10),
        name: document.getElementById('edit- name ').value.trim(),
        info: document.getElementById('edit - statusOfOrderId ').value.trim()
};
fetch(`${uri}/${orderyId}`, {
    method: 'PUT&',
headers: {
    'Accept ': ' application / json ';,
    ' Content - Type ': ' application / json '
},
body: JSON.stringify(order)
})
.then(() => getOrders())
    .catch(error => console.error('Unable to update order.', error));
closeInput();
return false;
}
function closeInput() {
    document.getElementById(' editForm ').style.display = ' none ';
}
function _displayOrders(data) {
    const tBody = document.getElementById('orders ');

    tBody.innerHTML = '';
    const button = document.createElement('button ');
    data.forEach(order => {
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit ';
        editButton.setAttribute(' onclick ', `displayEditForm(${order.id})`);
        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = ' Delete ';
        deleteButton.setAttribute('onclick ', `deleteOrder(${order.id})`);
        let tr = tBody.insertRow();
        let td1 = tr.insertCell(0);
        let textNode = document.createTextNode(order.name);
        td1.appendChild(textNode);
        let td2 = tr.insertCell(1);
        let textNodeInfo = document.createTextNode(order.statusOfOrderId);
        td2.appendChild(textNodeInfo);
        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);
        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });
    orders = data;
}