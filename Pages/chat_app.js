function init() {
    all_users = [];
    notificationNum = "";
    currentUserEmail = "";
    //auth = firebase.auth();
    msgArr = [];
    notificationArr = {};
    A_Narr = [];
    ref = firebase.database().ref("messages");
    ref_Admin_Notification = firebase.database().ref("A_N");
    init_A_N();
    user_init();
    //listenToUserState();
}
function init_A_N() {
    ref_Admin_Notification.on("child_added", snapshot => {
        a_n = {
            u_email: snapshot.val().by,
            time: snapshot.val().time,
            rendered: snapshot.val().rendered,
        }
        A_Narr.push(a_n);
        console.log(a_n.rendered);

        if (notificationArr[a_n.u_email] === undefined) {
            render_notification(a_n.u_email, 1);
            notificationArr[a_n.u_email] = 1;

        } else {

            render_notification(a_n.u_email, notificationArr[a_n.u_email] + 1);
            notificationArr[a_n.u_email] = notificationArr[a_n.u_email] + 1;
        }

    });
}
//function listenToUserState() {
//    auth.onAuthStateChanged(user => {
//        if (user) {
//            console.log(user)
//        } else {
//            console.log("user log out");
//        }
//    })
//}
function user_init() {

    let url = `../api/Users/chat`;
    ajaxCall("GET", url, "", GETscbUserList, GETecbUserList);
}


function GETscbUserList(users) {
    all_users = users;
    console.log(users);
    for (let user in users) {
        add_to_list(users[user]);
    }

    return false;
}
function render_notification(UserEmail, value) {

    let el = $("[id='" + UserEmail + "']");
    $("[id='" + UserEmail + "div" + "']").remove();
    el.remove();

    if (value > 0) {
        let p = $("<p>").text(value);
        let circle_div = $("<div>");
        circle_div.addClass("circle");
        p.css("font-size", 15 + "px");
        p.css("text-align", "center");
        circle_div.append(p);
        circle_div.attr("id", UserEmail + "div")
        el.append(circle_div);
    }

    $("#users_list").prepend(el);
}
function add_to_list(user) {
    let container = $("<li>");
    container.attr('id', user.Email);
    container.addClass("clearfix");
    let about = $("<div>");
    about.addClass("about");
    let name = $("<div>");
    let email = $("<div>");
    let child = $("i");
    container.attr("onclick", 'show_user_messages("' + user.Email + '","' + user.UserName + '")');
    child.addClass("fa fa-circle offline");
    email.append(child);
    name.addClass("name");
    about.append(name);
    about.append(email);
    name.text(user.UserName);
    container.append(about);
    email.addClass("status");
    email.text(user.Email);
    $("#users_list").append(container);
}
function show_user_messages(userEmail, userName) {
    currentUserEmail = userEmail;
    $("#history").empty();
    $("#current_user").text(userName);
    ref = firebase.database().ref(userEmail.replace('.', 'DOT'));
    ref_Notification = firebase.database().ref(userEmail.replace('.', 'DOT') + "NFA");
    listenToNewMessages();
    //listenTo_Admin_notification(userEmail);
    remove_child_notification(userEmail);
    //$('#history').scrollTop($('#chat-window')[0].scrollHeight);
    //update_user_notification(userEmail);
}
function remove_child_notification(userEmail) {
    ref_Admin_Notification.once("value", snapshot => {
        snapshot.forEach(element => {
            // check if contains the substring
            if (element.val().by === userEmail) {
                ref_Admin_Notification.child(element.key).remove();
            }

        });
        notificationArr[userEmail] = 0;
        $("[id='" + userEmail + "div" + "']").empty();
        $("[id='" + userEmail + "div" + "']").remove();
    })
}


function GETecbUserList(status) {
    console.log(status);
}
function listenToNewMessages() {

    ref.on("child_added", snapshot => {
        msg = {
            name: snapshot.val().name,
            content: snapshot.val().msg,
            by: snapshot.val().by,
        }
        msgArr.push(msg)
        if (msg.by == "Admin") {
            printAdminMessage(msg);
            $("#txtbox").val("");

        } else {
            printUserMessage(msg);
        }

    })

}
function addNoitification() {

    firebase.database().ref(currentUserEmail.replace('.', 'DOT') + "NFA").once("value", snapshot => {
        if (!snapshot.exists()) {
            ref_Notification.set({ count: 1 });            

        } else {
            ref_Notification.set({ count: snapshot.val().count + 1 });
        }
    });
}
function printUserMessage(msg) {
    let container = $("<li>");
    container.addClass("clearfix");
    let span = $("<span>");
    span.addClass("message-data-time");
    span.text(msg.name);
    let message_div = $("<div>");
    let date_div = $("<div>");
    date_div.addClass("message-data text-right");
    date_div.append(span);
    container.append(date_div);
    message_div.addClass("message other-message float-right");
    message_div.text(msg.content);
    container.append(message_div);
    $("#history").append(container);
}

function printAdminMessage(msg) {

    let container = $("<li>");
    container.addClass("clearfix");
    let span = $("<span>");
    span.addClass("message-data-time");
    span.text(msg.name);
    let message_div = $("<div>");
    let date_div = $("<div>");
    date_div.addClass("message-data");
    date_div.append(span);
    container.append(date_div);
    message_div.addClass("message my-message");
    message_div.text(msg.content);
    container.append(message_div);

    $("#history").append(container);


}

function printMessages(msgArr) {
    var str = "";
    for (let index = 0; index < msgArr.length; index++) {
        if (msgArr[index].by == "Admin") {
            const msg = msgArr[index];
            str += "name: " + msg.name + ", content: " + msg.content + "<br/>";
        }
    }
}

function AddMSG() {

    let msg = $("#txtbox").val();

    if (msg == "") {
        return;
    }

    var today = new Date();
    var time = today.getHours() + ":" + today.getMinutes()
    ref.push().set({ "msg": $("#txtbox").val(), "name": get_date(), "by": "Admin" });
    addNoitification();
}
function get_date() {
    var months = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
    var days = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    var d = new Date();
    var day = days[d.getDay()];
    var hr = d.getHours();
    var min = d.getMinutes();
    if (min < 10) {
        min = "0" + min;
    }
    var ampm = "AM";
    if (hr > 12) {
        hr -= 12;
        ampm = "PM";
    }
    var date = d.getDate();
    var month = months[d.getMonth()];
    var year = d.getFullYear();

    return day + " " + hr + ":" + min + " " + ampm + " " + date + " " + month + " " + year;

}