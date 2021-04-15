function RatingMethod() {
    $("ul.star-cb-group > li").on("click mouseover mouseout", function (e) {
        if (e.type == "click") {
            if (!$(e.currentTarget).hasClass("fix")) {
                $(this).addClass("fix");
                $(this).prevAll().addClass("fix");

                let ratingValue = $(this).parent("ul").find("li.fix").length;
                let gameId = $(this).parent("ul").prev("input#gameidvalue").val();

                fetch('/Games/SetRatingValue', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({
                        "ratingValue": ratingValue,
                        "gameId": gameId
                    })

                }).then(res => console.log(res));
            }
        }
        else if (e.type == "mouseover") {
            if (!$(e.currentTarget).hasClass("over")) {
                $(this).addClass("over");
                $(this).prevAll().addClass("over");
            }
        }
        else if (e.type == "mouseout") {
            $(this).removeClass("over");
            $(this).prevAll().removeClass("over");
        }   
    });
}

function SetRating(rating) {

    //$().addClass("fix");
    //$(this).prevAll().addClass("fix");
    console.log(typeof rating);
    console.log(rating);

    for (let property in rating) {
        if (rating[property] == 6) {
            $(`form#${property} > ul > li`).addClass("fix");
        }
        else {
            $(`form#${property} > ul > li:nth(${rating[property]})`).prevAll().addClass("fix");
        }
        
    }
}
RatingMethod();



