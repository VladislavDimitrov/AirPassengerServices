$('#the-clicking-table').on('click', '.promote', function (event) {
    event.preventDefault();
    const url = $(this).data('url');
    const userId = $(this).data('userid');
    const thisBtn = $(this);
    $.ajax({
        url: url,
        data: { userId: userId } ,
        method: 'POST',
        success: function () {
            toastr.options.positionClass = "toast-top-right";
            toastr.success('User promoted.')
            $(thisBtn).replaceWith($('<button type="submit" class="btn btn-primary demote" data-url="/Admin/Demote" data-userid="' + userId + '">Demote</button>'))
            $('#' + userId + '').replaceWith('<td id="' + userId + '">Administrator</td>')
            
        }
    })

});

$('#the-clicking-table').on('click', '.demote', function (event) {
    event.preventDefault();
    const url = $(this).data('url');
    const userId = $(this).data('userid');
    const thisBtn = $(this);

    $.ajax({
        url: url,
        data: { userId: userId },
        method: 'POST',
        success: function () {
            toastr.options.positionClass = "toast-top-right";
            toastr.success('User demoted.')
            $(thisBtn).replaceWith($('<button type="submit" class="btn btn-primary promote" data-url="/Admin/Promote" data-userid="' + userId + '">Promote</button>'))
            $('#' + userId + '').replaceWith('<td id="' + userId + '">Member</td>')
        }
    })
})