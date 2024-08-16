var dataTable;
$(document).ready(function () {
    dataTable = $('#DT_load').DataTable({
        "ajax": {
            "url": "/api/menuitem",
            "type": "GET",
            "datatype": "json"
        },
        "columns": [
            { "data": "name", "width": "20%" },
            { "data": "price", "width": "20%" },
            { "data": "category.name", "width": "20%" },
            { "data": "foodType.name", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="w-75 btn-group">
                        <a href="/Admin/MenuItems/Upsert?id=${data}" class='btn btn-success text-white mx-2' style='cursor:pointer; width:70px;'>
                           <i class="bi bi-pencil-square"></i>
                        </a>
                        &nbsp;
                        <a onClick=Delete('/api/MenuItem/${data}') class='btn btn-danger text-white mx-2' style='cursor:pointer; width:70px;'>
                            <i class="bi bi-trash-fill"></i>
                        </a>
                    </div>
                    `
                },
                "width": "40%"
            }
        ],
    });
});
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
                type: "DELETE",
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                }
            });
        }
    });
};