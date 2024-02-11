// specificPage.js

var Users = {
    init: function () {
        console.log("Specific Page initialized");
        // Other initialization code
        this.HandleUserCompanents();
    },
    currentUserData: null,
    // You can add other methods as needed
    HandleUserCompanents: function () {

        $("#dateOfStart").datepicker({
            dateFormat: "dd/mm/yy"
        });

        getUsers();




        function getUsers() {
            $.ajax({
                url: '/User/GetAllUsers', // Update with the correct endpoint URL
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
                    { data: 'name' },
                    { data: 'surName' },
                    { data: 'email' },
                    { data: 'phoneNumber' },
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
            var userId = $(this).data('id');
            console.log("Edit button clicked for user ID:", userId);
            getUser(userId);
        });





        function getUser(userId) {
            $.ajax({
                url: '/User/GetUserByIdForUserEdit/' + userId, // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("user data : ");
                        console.log(response);
                        Users.currentUserData = response.result; // Assign the response to the global variable
                        // Open the modal
                        setUser(Users.currentUserData);
                        $('#editUserModal').modal('show');
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
        function formatToISO(dateStr) {
            var parts = dateStr.split('/');
            return parts[2] + '-' + parts[1] + '-' + parts[0];
        }
        function setUser(userResponse) {
            var user = userResponse.user;

            $("#id").val(user.id);
            $("#name").val(user.name);
            $("#surName").val(user.surName);
            $("#email").val(user.email);
            $("#phoneNumber").val(user.phoneNumber);
            if (isNullOrEmpty(user.dateOfStart)) {
                var userDateOfStart = user.dateOfStart;
                var dateStart = new Date(userDateOfStart);

                // Format the date as dd/mm/yyyy
                var formattedDate = ("0" + dateStart.getDate()).slice(-2) + "/"
                    + ("0" + (dateStart.getMonth() + 1)).slice(-2) + "/"
                    + dateStart.getFullYear();

                // Set the formatted date to the input field
                $("#dateOfStart").val(formattedDate);
            }
            setRoleArea(userResponse.allRoles, user.userRoles);
        }

        $('#editUserModal').on('click', '#btnUserSave', function () {
            let id = $("#id").val();
            let name = $("#name").val();
            let surName = $("#surName").val();
            let email = $("#email").val();
            let phoneNumber = $("#phoneNumber").val();
            let userRoles = [];
            $('.userrole').each(function () {
                if ($(this).is(':checked')) {
                    userRoles.push($(this).val());
                }
            });
            let dateOfStart = formatToISO($("#dateOfStart").val());

            var request = {
                Id: id,
                Name: name,
                SurName: surName,
                Email: email,
                PhoneNumber: phoneNumber,
                UserRoles: userRoles,
                DateOfStart: dateOfStart
            }
            console.log(request);
            $.ajax({
                url: '/User/UserUpdate', // Update with the correct endpoint URL
                method: 'POST',
                contentType: 'application/json', // Specify the content type
                data: JSON.stringify(request), // Convert the JavaScript object to a JSON string
                success: function (response) {
                    // Handle success
                    console.log('Update successful', response);

                    if (response.isSuccess) {
                        getUsers();
                        $('#editUserModal').modal('hide');
                    } else {
                        console.log(response);
                        // Show error toast here
                        toastr.error('Error occurred: ' + response.errorMessage);

                    }

                    // You might want to close the modal or refresh the page here
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle error
                    console.error('Error updating user: ' + textStatus, errorThrown);
                    toastr.error('Error fetching data: ' + textStatus + ', ' + errorThrown);
                }
            });

        });
        $('#editUserModal').on('click', '#btnUserCancel', function () {
            setUser(Users.currentUserData);
        });
        function getRoleElement(roleName, checked) {

            if (checked) {
                return `	<div class="checkbox">
								<label>
												<input type="checkbox" class="flat userrole" value="${roleName}" checked="checked"> ${roleName}
								</label>
				</div>`;
            } else {
                return `
				<div class="checkbox">
								<label>
												<input type="checkbox" class="flat userrole" value="${roleName}"> ${roleName}
								</label>
				</div>`;
            }
        }

        function setRoleArea(allRoles, userRoles) {
            var res = "";
            allRoles.forEach(function (role) {
                res += getRoleElement(role, userRoles.includes(role));
            });
            $("#userRoleArea").html(`${res}`);

            if ($("input.flat")[0]) {
                $(document).ready(function () {
                    $('input.flat').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });
                });
            }

        }

        $('#editUserModal').on('hidden.bs.modal', function (e) {
            // If you need to reset the global variable or perform other cleanup tasks, do it here
            Users.currentUserData = null;
        });
    },
};

$(document).ready(function () {
    Users.init();
});
