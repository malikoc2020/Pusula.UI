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


            $("#startDate").datepicker({
                dateFormat: "dd/mm/yy"
            });
            $("#endDate").datepicker({
                dateFormat: "dd/mm/yy"
            });

        $('#mySelect2').select2({
            placeholder: 'Select a fruit',
            allowClear: true
        });


        getPermissions();
        getUsers();
        getGetAllPermissionTypes();


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

        function getUsers() {
            $.ajax({
                url: '/User/GetAllUsers', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("Users : ");
                        console.log(response.result);
                        var $select = $("#userId");
                        response.result.forEach(function (user) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(user.id) // Assuming 'id' is the property you want as the option value
                                .text(user.name + " " + user.surName); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                        });

                        $select.select2({
                            placeholder: "Select a user",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });


                    } else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data: ' + textStatus, errorThrown);
                }
            });
        }
        function getGetAllPermissionTypes() {
            $.ajax({
                url: '/Permission/GetAllPermissionTypes', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("PermissonTypes : ");
                        console.log(response.result);
                        var $select = $("#permissionTypeId");
                        response.result.forEach(function (user) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(user.id) // Assuming 'id' is the property you want as the option value
                                .text(user.name); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                        });

                        $select.select2({
                            placeholder: "Select a permission type",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });


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
                    { data: 'userName' },
                    { data: 'permissionTypeName' },
                    {
                        data: 'startDate',
                        render: function (data, type, row) {
                            if (type === 'display' && data) {
                                var date = new Date(data);
                                var day = ("0" + date.getDate()).slice(-2);
                                var month = ("0" + (date.getMonth() + 1)).slice(-2);
                                var year = date.getFullYear();
                                return day + '/' + month + '/' + year;
                            }
                            return data;
                        }
                    },
                    {
                        data: 'endDate',
                        render: function (data, type, row) {
                            if (type === 'display' && data) {
                                var date = new Date(data);
                                var day = ("0" + date.getDate()).slice(-2);
                                var month = ("0" + (date.getMonth() + 1)).slice(-2);
                                var year = date.getFullYear();
                                return day + '/' + month + '/' + year;
                            }
                            return data;
                        }
                    },
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
                userId: '',
                permissionTypeId: 0,
                startDate: '',
                endDate: ''
            }
            console.log(permission);
            Permissions.currentPermissionData = permission;
            setPermission(Permissions.currentPermissionData);
            $('#editPermissionModal').modal('show');
        });





        function getPermission(permissionId) {
            console.log('/Permission/GetPermissionById/' + permissionId);
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
                        setPermission(Permissions.currentPermissionData);
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
        function isNullOrEmpty(str) {
            return str === null || str === undefined || str.trim() === "";
        }
        function setPermission(permission) {
            $("#id").val(permission.id);
            $("#userId").val(permission.userId).trigger('change');
            $("#permissionTypeId").val(permission.permissionTypeId).trigger('change');
            var permissionStartDate = permission.startDate;
            if (!isNullOrEmpty(permissionStartDate)) {
                var dateStart = new Date(permissionStartDate);

                // Format the date as dd/mm/yyyy
                var formattedDate = ("0" + dateStart.getDate()).slice(-2) + "/"
                    + ("0" + (dateStart.getMonth() + 1)).slice(-2) + "/"
                    + dateStart.getFullYear();

                // Set the formatted date to the input field
                $("#startDate").val(formattedDate);
            } else {
                $("#startDate").val('');
            }



            var permissionEndDate = permission.endDate;
            if (!isNullOrEmpty(permissionEndDate)) {
                var dateEnd = new Date(permissionEndDate);

                // Format the date as dd/mm/yyyy
                var formattedDateEnd = ("0" + dateEnd.getDate()).slice(-2) + "/"
                    + ("0" + (dateEnd.getMonth() + 1)).slice(-2) + "/"
                    + dateEnd.getFullYear();


                $("#endDate").val(formattedDateEnd);
            } else {
                $("#endDate").val('');
            }
        }

        function formatToISO(dateStr) {
            var parts = dateStr.split('/');
            return parts[2] + '-' + parts[1] + '-' + parts[0];
        }

        $('#editPermissionModal').on('click', '#btnPermissionSave', function () {
            let id = $("#id").val();
            let userId = $("#userId").val();
            let permissionTypeId = $("#permissionTypeId").val();
            let startDate = formatToISO($("#startDate").val());
            let endDate = formatToISO($("#endDate").val());

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
                    console.log('Update successful:');
                    console.log(response.result);
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
            setPermission(Permissions.currentPermissionData);
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
