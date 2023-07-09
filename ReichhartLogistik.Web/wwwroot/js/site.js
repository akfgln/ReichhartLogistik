$(function () {
    if ($.isFunction($.fn.multiselect)) {
        $('select').multiselect({
            columns: 3,
            placeholder: 'Wählen',
            search: true,
            searchOptions: {
                'default': 'Suchen'
            },
            selectAll: true
        });
    }

});