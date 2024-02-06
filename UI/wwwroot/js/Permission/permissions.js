// specificPage.js

var Permissions = {
    init: function () {
        console.log("Specific Page initialized");
        // Other initialization code
        this.HandlePermissionCompanents();
    },
    currentPermissionData: null,
    // You can add other methods as needed
    HandlePermissionCompanents: function () {
        getPermissions();




        function getPermissions() {
            $.ajax({
                url: '/Permission/GetAllPermissions', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        setTable(response.result);
                    } else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data: ' + textStatus, errorThrown);
                }
            });
        }

        function setTable(data) {

            // Check if the DataTable instance exists and destroy it
            if ($.fn.DataTable.isDataTable("#datatable-buttons")) {
                $("#datatable-buttons").DataTable().destroy();
            }

            $("#datatable-buttons").DataTable({
                dom: "Blfrtip",
                buttons: [
                    {
                        extend: "copy",
                        className: "btn-sm"
                    },
                    {
                        extend: "csv",
                        className: "btn-sm"
                    },
                    {
                        extend: "excel",
                        className: "btn-sm"
                    },
                    {
                        extend: "pdfHtml5",
                        className: "btn-sm"
                    },
                    {
                        extend: "print",
                        className: "btn-sm"
                    },
                ],
                responsive: true,
                data: data,
                columns: [
                    { data: 'id' },
                    { data: 'userId' },
                    { data: 'permissionTypeId' },
                    { data: 'startDate' },
                    { data: 'endDate' },
                    {
                        data: null,
                        render: function (data, type, row) {
                            return '<button type="button" class="btn btn-success btn-sm edit" data-id="' + row.id + '" data-row="' + row.row + '"> Edit </button>';
                        },
                        orderable: false
                    }
                    // Define more columns if needed
                ]

            });
        }

        $('#datatable-buttons').on('click', '.edit', function () {
            var permissionId = $(this).data('id');
            console.log("Edit button clicked for permission ID:", permissionId);
            getPermission(permissionId);
        });
        $(document).on('click', '.insert', function () {

            let permission = {
                id: 0,
                userId: 'e3ce9c8c-c9a8-4a69-a784-c6dcc6ab7de5',
                permissionTypeId: 1,
                startDate: '2022-10-11',
                endDate: '2022-10-15'
            }

            setPermission(permission);
            $('#editPermissionModal').modal('show');
        });





        function getPermission(permissionId) {
            $.ajax({
                url: '/Permission/GetPermissionById/' + permissionId, // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("permission data : ");
                        console.log(response);
                        Permissions.currentPermissionData = response.result; // Assign the response to the global variable
                        // Open the modal
                        setPermission(Permissions.currentPermissionData.permission);
                        $('#editPermissionModal').modal('show');
                    } else {
                        console.log(response);
                        // Show error toast here
                        toastr.error('Error occurred: ' + response.errorMessage);

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data: ' + textStatus, errorThrown);
                    // Show error toast instead of logging to console
                    toastr.error('Error fetching data: ' + textStatus + ', ' + errorThrown);
                }
            });
        }

        function setPermission(permission) {
            $("#id").val(permission.id);
            $("#userId").val(permission.userId);
            $("#permissionTypeId").val(permission.permissionTypeId);
            $("#startDate").val(permission.startDate);
            $("#endDate").val(permission.endDate);
            //setRoleArea(permissionResponse.allRoles, permission.userRoles);
        }

        $('#editPermissionModal').on('click', '#btnPermissionSave', function () {
            let id = $("#id").val();
            let userId = $("#userId").val();
            let permissionTypeId = $("#permissionTypeId").val();
            let startDate = $("#startDate").val();
            let endDate = $("#endDate").val();

            var request = {
                Id: id,
                UserId: userId,
                PermissionTypeId: permissionTypeId,
                StartDate: startDate,
                EndDate: endDate
            }
            console.log("Edit Request : ");
            console.log(request);
            let URL = '/Permission/UpdatePermission';
            if (request.Id == 0) {
                URL = '/Permission/InsertPermission';
            }
            $.ajax({
                url: URL, // Update with the correct endpoint URL
                method: 'POST',
                contentType: 'application/json', // Specify the content type
                data: JSON.stringify(request), // Convert the JavaScript object to a JSON string
                success: function (response) {
                    // Handle success
                    console.log('Update successful', response);

                    if (response.isSuccess) {
                        getPermissions();
                        $('#editPermissionModal').modal('hide');
                    } else {
                        console.log(response);
                        // Show error toast here
                        toastr.error('Error occurred: ' + response.errorMessage);

                    }

                    // You might want to close the modal or refresh the page here
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle error
                    console.error('Error updating permission: ' + textStatus, errorThrown);
                    toastr.error('Error fetching data: ' + textStatus + ', ' + errorThrown);
                }
            });

        });
        $('#editPermissionModal').on('click', '#btnPermissionCancel', function () {
            setPermission(Permissions.currentPermissionData.permission);
        });

        $('#editPermissionModal').on('hidden.bs.modal', function (e) {
            // If you need to reset the global variable or perform other cleanup tasks, do it here
            Permissions.currentPermissionData = null;
        });
    },
};

$(document).ready(function () {
    Permissions.init();
});
