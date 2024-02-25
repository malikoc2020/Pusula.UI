var Worksites = {
    init: function () {
        console.log("Specific Page initialized");
        // Other initialization code
        this.HandleWorksiteCompanents();
    },
    currentWorksiteData: null,
    districts:null,
    // You can add other methods as needed
    HandleWorksiteCompanents: function () {


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


        getWorksites();
        getGetAllProvinces();
        getGetAllDistricts();


        function getWorksites() {
            $.ajax({
                url: '/Worksite/GetAllWorksites', // Update with the correct endpoint URL
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

        function getGetAllProvinces() {
            $.ajax({
                url: '/Common/GetAllProvinces', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("iller : ");
                        console.log(response.result);
                        var $select = $("#ilId");
                        response.result.forEach(function (province) {
                            // Create an option element
                            var $option = $("<option></option>")
                                .val(province.id) // Assuming 'id' is the property you want as the option value
                                .text(province.name); // Assuming 'userName' is what you want to display

                            // Append the option to the select element
                            $select.append($option);
                        });

                        $select.select2({
                            placeholder: "Select a province",
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
        
        function getGetAllDistricts() {
            $.ajax({
                url: '/Common/GetAllDistricts/', // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {

                        Worksites.districts = response.result;

                    } else {

                    }
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.error('Error fetching data: ' + textStatus, errorThrown);
                }
            });
        }

        function setDistricts() {
            let provinceId = $('#ilId').val();
            let $select = $("#ilceId");
            if (provinceId === null || provinceId.trim() === '') {
                // The value is null or empty
                $select.empty();
                console.log('provinceId is null or empty');
            } else {

                                
                let filteredDistricts = Worksites.districts.filter(function (district) {
                    return district.ilId == provinceId;
                });

                //var $select = $("#ilceId");
                $select.empty();
                filteredDistricts.forEach(function (district) {
                    // Create an option element
                    var $option = $("<option></option>")
                        .val(district.id) // Assuming 'id' is the property you want as the option value
                        .text(district.name); // Assuming 'userName' is what you want to display

                    // Append the option to the select element
                    $select.append($option);
                });

                $select.select2({
                    placeholder: "Select a district",
                    allowClear: true,
                    width: '100%'  // Set the width to 100%
                });

            }
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
                    { data: 'description' },
                    { data: 'ilAd' },
                    { data: 'ilceAd' },
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
            var worksiteId = $(this).data('id');
            console.log("Edit button clicked for worksite ID:", worksiteId);
            getWorksite(worksiteId);
        });

        $(document).on('click', '.insert', function () {

            let worksite = {
                id: 0,
                name: '',
                description:'',
                ilId: 0,
                ilceId:0,
                startDate: '',
                endDate: ''
            }
            Worksites.currentWorksiteData = worksite;
            setWorksite(Worksites.currentWorksiteData);
            $('#editWorksiteModal').modal('show');
        });





        function getWorksite(worksiteId) {
            console.log('/Worksite/GetWorksiteById/' + worksiteId);
            $.ajax({
                url: '/Worksite/GetWorksiteById/' + worksiteId, // Update with the correct endpoint URL
                method: 'GET',
                dataType: 'json', // Expecting JSON data
                success: function (response) {
                    console.log(response); // Handle your data here
                    // You can call other functions to process and display the data
                    if (response.isSuccess) {
                        console.log("worksite data : ");
                        console.log(response);
                        Worksites.currentWorksiteData = response.result; // Assign the response to the global variable
                        // Open the modal
                        setWorksite(Worksites.currentWorksiteData);
                        $('#editWorksiteModal').modal('show');
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
        function setWorksite(worksite) {
            $("#id").val(worksite.id);
            $("#name").val(worksite.name);
            $("#description").val(worksite.description);
            $("#ilId").val(worksite.ilId).trigger('change');
            setDistricts();
            $("#ilceId").val(worksite.ilceId).trigger('change');
            var worksiteStartDate = worksite.startDate;
            if (!isNullOrEmpty(worksiteStartDate)) {
                var dateStart = new Date(worksiteStartDate);

                // Format the date as dd/mm/yyyy
                var formattedDate = ("0" + dateStart.getDate()).slice(-2) + "/"
                    + ("0" + (dateStart.getMonth() + 1)).slice(-2) + "/"
                    + dateStart.getFullYear();

                // Set the formatted date to the input field
                $("#startDate").val(formattedDate);
            }



            var worksiteEndDate = worksite.endDate;
            if (!isNullOrEmpty(worksiteEndDate)) {
                var dateEnd = new Date(worksiteEndDate);

                // Format the date as dd/mm/yyyy
                var formattedDateEnd = ("0" + dateEnd.getDate()).slice(-2) + "/"
                    + ("0" + (dateEnd.getMonth() + 1)).slice(-2) + "/"
                    + dateEnd.getFullYear();


                $("#endDate").val(formattedDateEnd);
            }
        }

        function formatToISO(dateStr) {
            var parts = dateStr.split('/');
            return parts[2] + '-' + parts[1] + '-' + parts[0];
        }

        $('#editWorksiteModal').on('click', '#btnWorksiteSave', function () {
            let id = $("#id").val();
            let name = $("#name").val();
            let description = $("#description").val();
            let ilId = $("#ilId").val();
            let ilceId = $("#ilceId").val();
            let startDate = formatToISO($("#startDate").val());
            let endDate = formatToISO($("#endDate").val());

            var request = {
                Id: id,
                Name: name, 
                Description: description,
                ilId: ilId,
                ilceId: ilceId,
                StartDate: startDate,
                EndDate: endDate
            }
            console.log("Edit Request : ");
            console.log(request);
            let URL = '/Worksite/UpdateWorksite';
            if (request.Id == 0) {
                URL = '/Worksite/InsertWorksite';
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
                        getWorksites();
                        $('#editWorksiteModal').modal('hide');
                    } else {
                        console.log(response);
                        // Show error toast here
                        toastr.error('Error occurred: ' + response.errorMessage);

                    }

                    // You might want to close the modal or refresh the page here
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    // Handle error
                    console.error('Error updating worksite: ' + textStatus, errorThrown);
                    toastr.error('Error fetching data: ' + textStatus + ', ' + errorThrown);
                }
            });

        });
        $('#editWorksiteModal').on('click', '#btnWorksiteCancel', function () {
            setWorksite(Worksites.currentWorksiteData);
        });

        $('#editWorksiteModal').on('hidden.bs.modal', function (e) {
            // If you need to reset the global variable or perform other cleanup tasks, do it here
            Worksites.currentWorksiteData = null;
        });

        // Event listener for change event
        $('#ilId').on('change', function () {
            setDistricts();
        });
    },
};

$(document).ready(function () {
    Worksites.init();
});
