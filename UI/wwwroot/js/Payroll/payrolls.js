// specificPage.js

var Payrolls = {
    init: function () {
        console.log("Specific Page initialized");
        // Other initialization code
        this.HandlePayrollCompanents();
    },
    currentPayrollData: null,
    // You can add other methods as needed
    HandlePayrollCompanents: function () {


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


        getPayrolls();
        getUsers();
        getGetAllPayrollTypes();


        function getPayrolls() {
            $.ajax({
                url: '/Payroll/GetAllPayrolls', // Update with the correct endpoint URL
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
        function getGetAllPayrollTypes() {
            $.ajax({
                url: '/Payroll/GetAllPayrollTypes', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("PermissonTypes : ");
                        console.log(response.result);
                        var $select = $("#payrollTypeId");
                        response.result.forEach(function (user) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(user.id) // Assuming 'id' is the property you want as the option value
                                .text(user.name); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                        });

                        $select.select2({
                            placeholder: "Select a payroll type",
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
                    { data: 'payrollTypeName' },
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
                            //return '<button type="button" class="btn btn-success btn-sm edit" data-id="' + row.id + '" data-row="' + row.row + '"> Edit </button>';

                            return `<button type="button" class="btn btn-success btn-sm edit" data-id=${row.id} data-row=${row}> Edit </button>
                            <button type="button" class="btn btn-danger btn-sm delete" data-id=${row.id}> Delete </button>`;


                        },
                        orderable: false
                    }
                    // Define more columns if needed
                ]

            });
        }

        $('#datatable-buttons').on('click', '.edit', function () {
            var payrollId = $(this).data('id');
            console.log("Edit button clicked for payroll ID:", payrollId);
            getPayroll(payrollId);
        });

        $(document).on('click', '.insert', function () {

            let payroll = {
                id: 0,
                userId: '',
                payrollTypeId: 0,
                startDate: '',
                endDate: ''
            }
            console.log(payroll);
            Payrolls.currentPayrollData = payroll;
            setPayroll(Payrolls.currentPayrollData);
            $('#editPayrollModal').modal('show');
        });





        function getPayroll(payrollId) {
            console.log('/Payroll/GetPayrollById/' + payrollId);
            $.ajax({
                url: '/Payroll/GetPayrollById/' + payrollId, // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("payroll data : ");
                        console.log(response);
                        Payrolls.currentPayrollData = response.result; // Assign the response to the global variable
                        // Open the modal
                        setPayroll(Payrolls.currentPayrollData);
                        $('#editPayrollModal').modal('show');
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
        function setPayroll(payroll) {
            $("#id").val(payroll.id);
            $("#userId").val(payroll.userId).trigger('change');
            $("#payrollTypeId").val(payroll.payrollTypeId).trigger('change');
            var payrollStartDate = payroll.startDate;
            if (!isNullOrEmpty(payrollStartDate)) {
                var dateStart = new Date(payrollStartDate);

                // Format the date as dd/mm/yyyy
                var formattedDate = ("0" + dateStart.getDate()).slice(-2) + "/"
                    + ("0" + (dateStart.getMonth() + 1)).slice(-2) + "/"
                    + dateStart.getFullYear();

                // Set the formatted date to the input field
                $("#startDate").val(formattedDate);
            } else {
                $("#startDate").val('');
            }



            var payrollEndDate = payroll.endDate;
            if (!isNullOrEmpty(payrollEndDate)) {
                var dateEnd = new Date(payrollEndDate);

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

        $('#editPayrollModal').on('click', '#btnPayrollSave', function () {
            let id = $("#id").val();
            let userId = $("#userId").val();
            let payrollTypeId = $("#payrollTypeId").val();
            let startDate = formatToISO($("#startDate").val());
            let endDate = formatToISO($("#endDate").val());

            var request = {
                Id: id,
                UserId: userId,
                PayrollTypeId: payrollTypeId,
                StartDate: startDate,
                EndDate: endDate
            }
            console.log("Edit Request : ");
            console.log(request);
            let URL = '/Payroll/UpdatePayroll';
            if (request.Id == 0) {
                URL = '/Payroll/InsertPayroll';
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
                        getPayrolls();
                        $('#editPayrollModal').modal('hide');
                    } else {
                        console.log(response);
                        // Show error toast here
                        toastr.error('Error occurred: ' + response.errorMessage);

                    }

                    // You might want to close the modal or refresh the page here
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle error
                    console.error('Error updating payroll: ' + textStatus, errorThrown);
                    toastr.error('Error fetching data: ' + textStatus + ', ' + errorThrown);
                }
            });

        });
        $('#editPayrollModal').on('click', '#btnPayrollCancel', function () {
            setPayroll(Payrolls.currentPayrollData);
        });

        $('#editPayrollModal').on('hidden.bs.modal', function (e) {
            // If you need to reset the global variable or perform other cleanup tasks, do it here
            Payrolls.currentPayrollData = null;
        });

        $('#datatable-buttons').on('click', '.delete', function () {
            var confirmation = confirm("Are you sure you want to delete this Payroll?");
            if (confirmation) {
                let id = $(this).data('id');
                var row = $(this).closest('tr');
                deletePayroll(id, row);
            }
        });

        function deletePayroll(id, row) {
            $.ajax({
                url: '/Payroll/DeletePayroll/' + id, // Update with the correct endpoint URL
                method: 'DELETE',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        $('#datatable-buttons').DataTable().row(row).remove().draw();
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
    },
};

$(document).ready(function () {
    Payrolls.init();
});
