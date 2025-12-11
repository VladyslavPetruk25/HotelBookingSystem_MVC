var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/roomtype/getall' },
        "columns": [
            { data: 'name', "width": "20%", className: "text-start" },
            { data: 'capacity', "width": "10%", className: "text-start" },
            { data: 'pricePerNight', "width": "10%", className: "text-start" },
            {
                data: 'description',
                "width": "35%",
                className: "text-start",
                "render": function (data) {
                    if (data == null) {
                        return "";
                    }

                    return data.length > 50 ? data.substring(0, 50) + '...' : data;
                }
            },
            {
                data: 'roomTypeId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/roomtype/edit?id=${data}" class="btn btn-primary mx-2"> 
                                    <i class="bi bi-pencil-square"></i> Edit
                                </a>
                                <a onClick=Delete('/admin/roomtype/delete/${data}') class="btn btn-danger mx-2"> 
                                    <i class="bi bi-trash-fill"></i> Delete
                                </a>
                            </div>`
                },
                "width": "25%",
                className: "text-center"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    });
}
