var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/room/getall'},
        "columns": [
            { data: 'roomNumber', "width": "15%", className: "text-start" },
            { data: 'roomType.name', "width": "10%", className: "text-start" },
            { data: 'flour', "width": "15%", className: "text-start" },
            { data: 'status', "width": "15%", className: "text-start" },
            {
                data: 'roomId',
                "render": function (data) {
                    return `
                        <div class="w-75 btn-group" role="group">
                            <a href="/admin/room/edit?id=${data}" class="btn btn-primary mx-2"> 
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>
                            <a onClick=Delete('/admin/room/delete/${data}') class="btn btn-danger mx-2"> 
                                <i class="bi bi-trash-fill"></i> Delete
                            </a>
                        </div>
                    `
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
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
