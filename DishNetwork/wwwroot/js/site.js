// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


if (document.getElementById('AddAdmin')) {
    document.getElementById('AddAdmin').addEventListener('show.bs.modal', event => {
        const button = event.relatedTarget
        const AdminId = button.getAttribute('data-bs-AdminId')
        console.log(AdminId)
        //document.getElementById('AdminId').value = AdminId;
    })
}