// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    //Select (AddCustomer) link specifically      
    $(".menu_item").click(function (e) {
        e.preventDefault();
        const controller = $(this).data("controller");
        const action = $(this).data("action");

        var url = `/${controller}/${action}`;
        $('#layout_main_content').load(url, function (response, status, xhr) {
            if (xhr.status == 401) {
                window.location.href = "/Authentication/login";
            }
        });
        return false;
    });
}); 