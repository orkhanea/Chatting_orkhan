"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

connection.start().then(function () {
    document.getElementById("chatSendBtn").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});


document.getElementById("chatSendBtn").addEventListener("click", function (event) {

    var recieverId = document.getElementById("recieverId").value;
    var message = document.getElementById("chatTextInput").value;

    if (message!="") {

        connection.invoke("SendPrivateMessage", recieverId, message).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();

        var div = document.createElement("div");
        div.classList.add('d-flex', 'justify-content-end', 'mb-4');

        var div2 = document.createElement("div");
        div2.classList.add('msg_cotainer_send');
        div2.textContent = `${message}`;
        div.appendChild(div2);

        var messageCount = parseInt(document.getElementById("messageCount").innerHTML);

        document.getElementById("messageCount").textContent = messageCount + 1;

        document.getElementById("msg_card_body2").appendChild(div);

        document.getElementById("chatTextInput").value = "";

    }

    

});

document.getElementById("chatTextInput").addEventListener("focus", function (event) {

    var recieverId = document.getElementById("recieverId").value;

    connection.invoke("isTyping", recieverId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

});

document.getElementById("chatTextInput").addEventListener("blur", function (event) {

    var recieverId = document.getElementById("recieverId").value;

    connection.invoke("isNotTyping", recieverId).catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();

});

connection.on("ReceiveMessage", function (senderName, senderId, message) {

    var senderId2 = document.getElementById("senderId").value;
    var recieverId2 = document.getElementById("recieverId").value;
    

    if (senderId2 == senderId || recieverId2 == senderId) {

        var messageCount = parseInt(document.getElementById("messageCount").innerHTML);

        document.getElementById("messageCount").textContent = messageCount + 1;
        


        var div = document.createElement("div");
        div.classList.add('d-flex', 'justify-content-start', 'mb-4');

        var div2 = document.createElement("div");
        div2.classList.add('msg_cotainer');
        div2.textContent = `${message}`;

        var span = document.createElement("span");
        span.classList.add('msg_time');
        span.textContent = senderName;

        div2.appendChild(span);
        div.appendChild(div2);

        document.getElementById("msg_card_body2").appendChild(div);


    }

    

});

connection.on("IsTyping", function () {

    var div = document.createElement("div");
    div.classList.add('d-flex', 'justify-content-start', 'mb-4', 'typingSH');
    var p = document.createElement("p");
    p.classList.add('badge', 'badge-pill', 'm-0', 'typingC');
    p.textContent = `${"Typing..."}`;
    div.appendChild(p);

    
    document.getElementById("msg_card_body2").appendChild(div);

});

connection.on("IsNotTyping", function () {

    for (var i in document.getElementsByClassName("typingSH")) {
        document.getElementsByClassName("typingSH")[i].remove();
    }

});


