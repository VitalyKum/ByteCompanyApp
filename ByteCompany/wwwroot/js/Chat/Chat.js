$(document).ready(function () {
    $('#sendButton').click(function () {
        var user = document.getElementById("userInput").nodeValue;
        var message = document.getElementById("messageInput").nodeValue;
        if (user != null && message != null) {
            document.append("<div>" + user + "</div>");
        }
        else {
            document.append("<div>boba</div>")
        }
    });
});