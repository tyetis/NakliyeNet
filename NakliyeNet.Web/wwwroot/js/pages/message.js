(function ($) {
    $("#frmMessage").submit(function () {
        var recipientId = $("#RecipientId").val()
        var message = $("#txtMessage").val()
        if (message == "") { alert("Mesaj Boş"); return false }
        sendMessage($(this).attr("src"), recipientId, message)
        $("#txtMessage").val("")
        return false
    })
    function sendMessage(url, recipientId, message) {
        $.post(url, { recipientId, message }, function (response) {
            addMessage(message)
        })
    }
    function addMessage(message) {
        var time = new Date()
        $(".empty").remove()
        var html = $($("#tmplMessage").html())
        $("#text", html).text(message)
        $("#time", html).text(time.getHours() + ":" + time.getMinutes())
        $(".messageList").append(html).scrollTop(10000)
    }
    $(".messageList").scrollTop(10000)
})(jQuery)