// specificPage.js

var PayrollTemps = {
    init: function () {
        console.log("Specific Page initialized");
        // Other initialization code
        this.HandlePayrollCompanents();
    },
    currentPayrollData: null,
    // You can add other methods as needed
    HandlePayrollCompanents: function () {
        getUsers();
        getGetAllYears();
        getGetAllMonths();
        function getPayrolls() {
            var request = {
                UserId: $("#userIdFilter").val() || undefined,
                YearId: $("#yearIdFilter").val() || undefined,
                MonthId: $("#monthIdFilter").val() || undefined
            };

            var queryString = $.param(request);

            $.ajax({
                url: '/Payroll/GetAllPayrollTemps?'+queryString,  
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
                        var $select = $("#userId");
                        var $selectFilter = $("#userIdFilter");
                        response.result.forEach(function (user) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(user.id) // Assuming 'id' is the property you want as the option value
                                .text(user.name + " " + user.surName); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                            $selectFilter.append($option.clone());

                        });

                        $select.select2({
                            placeholder: "Select a user",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });
                        $selectFilter.select2({
                            placeholder: "Select a user",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });
                        $selectFilter.val(null).trigger('change');
                    } else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data: ' + textStatus, errorThrown);
                }
            });
        }
        function getGetAllYears() {
            $.ajax({
                url: '/Common/GetAllYears', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                       
                        var $select = $("#yearId");
                        var $selectFilter = $("#yearIdFilter");

                        response.result.forEach(function (province) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(province.id) // Assuming 'id' is the property you want as the option value
                                .text(province.id); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                            $selectFilter.append($option.clone());

                        });

                        $select.select2({
                            placeholder: "Select a Year",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });
                        $selectFilter.select2({
                            placeholder: "Select a Year",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });
                        $selectFilter.val(null).trigger('change');

                    } else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data: ' + textStatus, errorThrown);
                }
            });
        }
        function getGetAllMonths() {
            $.ajax({
                url: '/Common/GetAllMonths', 
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {

                        var $select = $("#monthId");
                        var $selectFilter = $("#monthIdFilter");

                        response.result.forEach(function (province) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(province.id) // Assuming 'id' is the property you want as the option value
                                .text(province.name); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                            $selectFilter.append($option.clone());

                        });

                        $select.select2({
                            placeholder: "Select a Month",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });
                        $selectFilter.select2({
                            placeholder: "Select a Month",
                            allowClear: true,
                            width: '100%'  // Set the width to 100%
                        });
                        $selectFilter.val(null).trigger('change');

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
                    { data: 'yearId' },
                    { data: 'monthName' },
                    { data: 'salary' },
                    { data: 'overtime' },
                    {
                        data: null,
                        render: function (data, type, row) {
                            //return '<button type="button" class="btn btn-success btn-sm edit" data-id="' + row.id + '" data-row="' + row.row + '"> Edit </button>';

                            return `<button type="button" class="btn btn-success btn-sm editPayrollTemp" data-id=${row.id} data-row=${row}> Edit </button>
                            <button type="button" class="btn btn-danger btn-sm deletePayrollTemp" data-id=${row.id}> Delete </button>`;


                        },
                        orderable: false
                    }
                    // Define more columns if needed
                ]

            });
        }

        $(document).off('click', '.filter').on('click', '.filter', function () {
            getPayrolls();
        });

        $('#datatable-buttons').off('click', '.editPayrollTemp').on('click', '.editPayrollTemp', function () {
            var payrollId = $(this).data('id');
            console.log("Edit button clicked for payroll ID:", payrollId);
            getPayroll(payrollId);
        });

        $(document).off('click', '.insert').on('click', '.insert', function () {

            let payroll = {
                id: 0,
                userId: '',
                yearId: 0,
                monthId: 0,
                salary: '',
                overtime: ''
            }
            console.log(payroll);
            PayrollTemps.currentPayrollData = payroll;
            setPayroll(PayrollTemps.currentPayrollData);
            $('#editPayrollTempModal').modal('show');
        });





        function getPayroll(payrollId) {
            $.ajax({
                url: '/Payroll/GetPayrollTempById/' + payrollId, // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("payroll data : ");
                        console.log(response);
                        PayrollTemps.currentPayrollData = response.result; // Assign the response to the global variable
                        // Open the modal
                        setPayroll(PayrollTemps.currentPayrollData);
                        $('#editPayrollTempModal').modal('show');
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
            $("#yearId").val(payroll.yearId).trigger('change');
            $("#monthId").val(payroll.monthId).trigger('change');
            $("#salary").val(payroll.salary);
            $("#overtime").val(payroll.overtime);
        }

        $('#editPayrollTempModal').off('click', '#btnPayrollTempSave').on('click', '#btnPayrollTempSave', function () {
            let id = $("#id").val();
            let userId = $("#userId").val();
            let yearId = $("#yearId").val();
            let monthId = $("#monthId").val();
            let salary = $("#salary").val();
            let overtime = $("#overtime").val();


            var request = {
                Id: id,
                UserId: userId,
                YearId: yearId,
                MonthId: monthId,
                Salary: salary,
                Overtime: overtime
            }
            console.log("Edit Request : ");
            console.log(request);
            let URL = '/Payroll/UpdatePayrollTemp';
            if (request.Id == 0) {
                URL = '/Payroll/InsertPayrollTemp';
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
                        $('#editPayrollTempModal').modal('hide');
                    } else {
                        console.log(response);
                        // Show error toast here
                        toastr.error('Error occurred: ' + response.errorMessage);

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle error
                    console.error('Error updating payroll: ' + textStatus, errorThrown);
                    toastr.error('Error fetching data: ' + textStatus + ', ' + errorThrown);
                }
            });

        });
        $('#editPayrollTempModal').off('click', '#btnPayrollTempCancel').on('click', '#btnPayrollTempCancel', function () {
            setPayroll(PayrollTemps.currentPayrollData);
        });

        $('#editPayrollTempModal').off('click', 'hidden.bs.modal').on('hidden.bs.modal', function (e) {
            // If you need to reset the global variable or perform other cleanup tasks, do it here
            PayrollTemps.currentPayrollData = null;
        });

        $('#datatable-buttons').off('click', '.deletePayrollTemp').on('click', '.deletePayrollTemp', function () {
            var confirmation = confirm("Are you sure you want to delete this Payroll?");
            if (confirmation) {
                let id = $(this).data('id');
                var row = $(this).closest('tr');
                deletePayroll(id, row);
            }
        });

        function deletePayroll(id, row) {
            $.ajax({
                url: '/Payroll/DeletePayrollTemp/' + id, // Update with the correct endpoint URL
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
    PayrollTemps.init();
});
