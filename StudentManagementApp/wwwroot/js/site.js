$(function () {
    $(".datepicker").datepicker({
        dateFormat: 'dd/mm/yy',
        onSelect: function () {
            var selectedDate = $(this).datepicker('getDate');
            var formattedDate = $.datepicker.formatDate('dd/mm/yy', selectedDate);
            $(this).val(formattedDate);
        }
    });
});
