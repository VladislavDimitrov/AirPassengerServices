$('#update').on('click', function () {
    console.log(1)
    $('div').children().children().each(function () {
        console.log(this)
        $(this).prop('disabled', false)
    });
    $('#description').prop('disabled', false)
    $('#update').replaceWith('<input type="submit" value="Save Changes" class="btn btn-primary" />')
})
