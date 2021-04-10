function RatingMethod() {
    var logID = 'log',
        log = $('<div id="' + logID + '"></div>');
    $('body').append(log);
    $('[type*="radio"]').change(function Rating() {
        var me = $(this);
        log.html(me.attr('value'));
    });
}
RatingMethod();


