function RatingMethod() {
    var logID = 'log',
        log = $('<div id="' + logID + '"></div>');
    $('body').append(log);
    $('[type*="radio"]').change(function Rating() {
        var me = $(this);
        fetch('/Games/SetRatingValue', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: me.val().toString()
        }).then(res => console.log(res));
    });

}
RatingMethod();


