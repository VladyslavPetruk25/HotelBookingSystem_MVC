var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/booking/getall' },
        "order": [[0, "desc"]],
        "columns": [
            { data: 'bookingId', "width": "5%" },
            {
                data: 'user',
                "render": function (data) {
                    return data.firstName + " " + data.lastName;
                },
                "width": "15%"
            },
            { data: 'room.roomNumber', "width": "10%" },
            {
                data: 'checkInDate',
                "render": function (data) { return new Date(data).toLocaleDateString(); },
                "width": "10%"
            },
            {
                data: 'checkOutDate',
                "render": function (data) { return new Date(data).toLocaleDateString(); },
                "width": "10%"
            },
            {
                data: 'status',
                "width": "10%",
                "render": function (data) {
                    if (data === "Approved") {
                        return `<span class="badge bg-success">${data}</span>`;
                    } else if (data === "Pending") {
                        return `<span class="badge bg-warning text-dark">${data}</span>`;
                    } else if (data === "Cancelled") {
                        return `<span class="badge bg-danger">${data}</span>`;
                    } else {
                        return `<span class="badge bg-secondary">${data}</span>`;
                    }
                }
            },
            {
                data: 'totalCost',
                "render": function (data) { return '$' + data; },
                "width": "10%"
            },
            {
                data: 'bookingId',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/booking/details?bookingId=${data}" class="btn btn-primary mx-2"> 
                                <i class="bi bi-pencil-square"></i> Details
                            </a>
                        </div>
                    `
                },
                "width": "15%"
            }
        ]
    });
}