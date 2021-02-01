var audioElement = document.createElement('audio');
audioElement.setAttribute('src', 'http://www.soundjay.com/misc/sounds/bell-ringing-01.mp3');
audioElement.addEventListener('ended', function () {
    this.play();
}, false);
function stopAudio(id) {
    audioElement.pause();
    var element = $("#" + id);
    element.removeClass("bg-warning shadow-lg p-3 mb-5 rounded");
    element.addClass("alert-warning");
    $("#check").removeClass("fa-2x");
    $(".pointer").remove();
}
$('.close').click(function () {
    console.log("acva");
    $('.errorMessage').hide();
});
function edit(pid, id) {
    var $el = $("#" + pid);
    var $input = $('<input/>').val($el.text());

    $el.replaceWith($input);
    $input.addClass("form-control form-control-lg rounded-0 ");
    var save = function () {
        var $p = $('<span id="' + pid + '" onclick="edit(\'' + pid + '\',\'' + id + '\')" />').text($input.val());
        $input.replaceWith($p);
        $.post("Home/Retitle/", { title: $input.val(), id: id }).fail(function (err) {
            alert("An error occurred.");
            console.error(err);
        });
    };
    $input.on('keydown', function (event) {
        if (event.keyCode == 13) {
            save();
        }
    });
    $input.one('blur', save).focus();

};

function countdown(finish_date, timer, id) {

    var x = setInterval(function () {

        var now = new Date().getTime();

        var distance = finish_date - now;

        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        timer.innerHTML = days + "d " + hours + "h " + minutes + "m " + "<b>" + seconds + "s</b>";


        if (distance < 0) {
            clearInterval(x);
            $("#" + id).remove();
            $.get("Home/NowIsTime/" + id, function (data) {
                console.log(data);
                $("#nowIsTime").empty();
                var title = data[0].title === "" || data[0].title === null ? "Reminder" : data[0].title;
                var alertDiv = '<div class="alert shadow-lg p-3 mb-5 bg-warning mr-5 ml-5 mb-3 mt-3" role="alert" id="' + id + '"> <h6 class="mb-3">' + title + ' <a class="mr-3 text-danger float-right" href="home/remove/' + data[0].id.toString() + '"><i class="fas fa-trash-alt"></i></a></h6><h4 class="desc"><a onclick="stopAudio(\'' + id.toString() + '\')" class="pointer  mr-2 text-danger" ><i class="far fa-stop-circle fa-2x"></i></a><a class="mr-3 text-success" href="home/TimeOut/' + data[0].id + '"><i id="check" class="fas fa-check-circle fa-2x"></i></a><b class="font-weight-light">' + data[0].reminderLongDate + '</b></h4></div>';
                $("#nowIsTime").append(alertDiv);
                if (data.length > 1) {
                    for (var i = 1; i < data.length; i++) {
                        var title = data[i].title === "" || data[i].title === null ? "Reminder" : data[i].title;
                        var alertDiv = '<div class="alert alert-warning mr-5 ml-5 mb-3 mt-3" role="alert" id="' + data[i].id.toString() + '"> <h6 class="mb-3">' + title + ' <a class="mr-3 text-danger float-right" href="home/remove/' + data[i].id.toString() + '"><i class="fas fa-trash-alt"></i></a></h6><h4><a class="mr-3 text-success" href="home/TimeOut/' + data[i].id + '"><i class="fas fa-check-circle"></i></a><b class="font-weight-light">' + data[i].reminderLongDate + '</b></h4></div>';
                        $("#nowIsTime").append(alertDiv);
                    }
                }
                var notfy = new Notification(title, { body: data[0].reminderLongDate, icon: "favicon.ico" });
                notfy.onclick = (e) => {
                    audioElement.pause();
                    $(".pointer").remove();
                };

            }).fail(function (err) {
                alert("An error occurred.");
                console.error(err);
            });
            audioElement.currentTime = 0;
            audioElement.play();
        }
    }, 1000);
}
