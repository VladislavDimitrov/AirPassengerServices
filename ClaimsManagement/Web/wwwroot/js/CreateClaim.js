$('#flt-number').mouseenter(function () {
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.options.positionClass = "toast-top-right";
    toastr.info('The Flight Number must contain between 3 and 4 digits.');
});
$('#flt-number').mouseleave(function () {
    toastr.remove();
});

$('#departure').mouseenter(function () {
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.options.positionClass = "toast-top-right";
    toastr.info('Please enter the IATA 3 digit code of the departure airport as shown on you boarding pass. Example: Sofia (SOF)');
});
$('#departure').mouseleave(function () {
    toastr.remove();
});

$('#arrival').mouseenter(function () {
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.options.positionClass = "toast-top-right";
    toastr.info('Please enter the IATA 3 digit code of the arrival airport as shown on you boarding pass. Example: Frankfurt (FRA)');
});
$('#arrival').mouseleave(function () {
    toastr.remove();
});

$('#submit').mouseenter(function () {
    toastr.options.timeOut = 0;
    toastr.options.extendedTimeOut = 0;
    toastr.options.positionClass = "toast-top-right";
    toastr.warning('Before submitting your claim, please make sure that all information you provided is valid.');
});
$('#submit').mouseleave(function () {
    toastr.remove();
});