
$(document).ready(function () {
    notification = 0;
    sticky_navBar();
    User = JSON.parse(localStorage.getItem('User'));
    if (User) {      
        ref_Notification = firebase.database().ref(User.Email.replace('.', 'DOT') + "NFA");
        ref_Admin_Notification_list = firebase.database().ref("A_N");
    }
    
    
    
});
async function first_time_swal() {
    const steps = ['1', '2', '3','4']
    const Queue = Swal.mixin({
        progressSteps: steps,
        confirmButtonText: 'Next >',
        // optional classes to avoid backdrop blinking between steps
        showClass: { backdrop: 'swal2-noanimation' },
        hideClass: { backdrop: 'swal2-noanimation' }
    })

    await Queue.fire({
        title: 'Welcome To Chilax',
        text: 'This site is a demonstration of B&B in Amsterdam, based on real data from the Airbnb site.',
        currentProgressStep: 0,
        // optional class to show fade-in backdrop animation which was disabled in Queue mixin
        showClass: { backdrop: 'swal2-noanimation' },
    })
    await Queue.fire({
        title: 'Welcome To Chilax',
        text: 'Please register as a first step, so you can enjoy the many features the site has to offer.',
        currentProgressStep: 1
    })
    await Queue.fire({
        title: 'Welcome To Chilax',
        text:'You can enjoy a variety of apartments, and a real-time chat with the admin.',
        currentProgressStep: 2,
        confirmButtonText: 'OK',
        // optional class to show fade-out backdrop animation which was disabled in Queue mixin
        showClass: { backdrop: 'swal2-noanimation' },
    })
    await Queue.fire({
        title: 'Welcome To Chilax',
        text: "Don't forget to check Admin-Mode to validate all the SQL data or check for some new messages in the chat that based on Firebase Realtime Database",
        currentProgressStep: 3,
        confirmButtonText: 'OK',
        // optional class to show fade-out backdrop animation which was disabled in Queue mixin
        showClass: { backdrop: 'swal2-noanimation' },
    })
}


function check_notifications() {
    ref_Notification_check = firebase.database().ref(User.Email.replace('.', 'DOT') + "NFA");
    ref_Notification_check.on("child_added", snapshot => {
        console.log(snapshot);
        if (snapshot.node_.value_ > 0) {
            
                $("#notification").text(snapshot.val() + " new message");
                
            

        }
    })
}
function sticky_navBar() {
    window.onscroll = function () { myFunction() };

    var navbar = document.getElementById("navbar");
    var sticky = navbar.offsetTop;

    function myFunction() {
        if (window.pageYOffset >= sticky) {
            navbar.classList.add("sticky")
        } else {
            navbar.classList.remove("sticky");
        }
    }
}
function init_chat() {
    $("#inputs").data("status", "close");
    $("#chat_box").slideUp();
    $("#inputs").slideUp();
    if (User) {
        msgArr = [];
     
        ref = firebase.database().ref(User.Email.replace('.', 'DOT'));
        add_notification();
        listenToNewMessages();
        $("#admin_chat").click(admin_chat);
        $("#send_message").click(AddMSG);
        check_notifications();
    } else {
        $("#chat_footer").slideUp();
    }
    
}
function add_notification() {
    ref_Notification.on("child_changed", snapshot => {
        if (!snapshot.exists()) {
            ref_Notification.set({ count: 0 });

        } else {
            if (snapshot.node_.value_ > 0) {
                if ($("#inputs").data("status") == "close") {                   
                    $("#notification").text(snapshot.val() + " new message");
                    $("#notification").css("color", "red");
                }
                
            }           

        }
        });
    
}

function AddMSG() {
    let msg = $("#text_box").val();

    if (msg == "") {
        return;
    }
    var today = new Date();
    var time = today.getHours() + ":" + today.getMinutes()
    ref.push().set({ "msg": $("#text_box").val(), "name": get_date(), "by": "User" });
    addAdminNoitification();
    }
    function addAdminNoitification() {
      
        firebase.database().ref("A_N").once("value", snapshot => {
            
            ref_Admin_Notification_list.push().set({ "time": get_date(), "by": User.Email,"rendered":false });
                            
        });
     
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

function listenToNewMessages() {
    // child_added will be evoked for every child that was added
    // on the first entry, it will bring all the childs
    ref.on("child_added", snapshot => {
        msg = {
            name: snapshot.val().name,
            content: snapshot.val().msg,
            by: snapshot.val().by,
            
        }
        msgArr.push(msg);
        if (msg.by == "Admin") {
            render_admin_message(msg);
            $("#txtbox").val("");           

        } else {

            render_user_message(msg);
        }

    })
}

function render_user_message(msg) {
    let history = $("#history");
    let li = $("<li>");
    history.append(li);
    li.addClass("user-message-box");
    let div_user_message = $("<div>");
    div_user_message.addClass("user-message");
    li.append(div_user_message);
    let div_messagefont_user = $("<div>");
    div_messagefont_user.addClass("messagefont-user");
    div_messagefont_user.text(User.UserName);
    div_user_message.append(div_messagefont_user);
    let div_message = $("<div>");
    div_message.text(msg.content);
    div_user_message.append(div_message);
    div_message.addClass("message");
    let div_timestamp = $("<div>");//
    div_timestamp.addClass("chat-timestamp");
    div_timestamp.text(msg.name);
    div_user_message.append(div_timestamp);
    $("#text_box").val('');

}
function render_admin_message(msg) {
    let history = $("#history");
    let li = $("<li>");
    history.append(li);
    li.addClass("admin-message-box");
    let div_user_message = $("<div>");
    div_user_message.addClass("admin-message");
    li.append(div_user_message);
    let div_messagefont_user = $("<div>");
    div_messagefont_user.addClass("messagefont-admin");
    div_messagefont_user.text("Admin");
    div_user_message.append(div_messagefont_user);
    let div_message = $("<div>");
    div_message.text(msg.content);
    div_user_message.append(div_message);
    div_message.addClass("message");
    let div_timestamp = $("<div>");
    div_timestamp.text(msg.name);
    div_timestamp.addClass("chat-timestamp");
    div_user_message.append(div_timestamp);
    $("#text_box").val('');

}
function admin_chat() {
    if ($("#inputs").data("status") == "close") {

        $("#inputs").data("status", "open");
    } else {
        
        ref_Notification.once("value", snapshot => {

            ref_Notification.set({ count: 0 });

        });
        $("#inputs").data("status", "close");
    }
    $("#notification").text("");
    $("#chat_box").slideToggle();
    $("#inputs").slideToggle();
   
    
}
       

function loginOrSignOut() {
    try {
        $('#SignOut').on('click', empty_local_user);
        $("a#user_pos").text("Hello " + User.UserName);
        $('#SignOut').css("background-color", "#ff0000");
        $('#SignOut').show();
        $('#login').hide();


    } catch {
        $('#login').show();
        $('#SignOut').hide();
        $('#apartmentsPages').hide();
    }

}
function empty_local_user() {
    /*auth.signOut();*/
    localStorage.clear();
    let f_t = {
        first_time: "1",
    };
    localStorage.setItem('First_time', JSON.stringify('f_t'));
    window.location.href = "index.html";
}
function add_min_to_calender() {
    var todayDate = new Date();
    var month = todayDate.getMonth() + 1;
    var year = todayDate.getUTCFullYear() - 0;
    var tdate = todayDate.getDate();
    if (month < 10) {
        month = "0" + month
    }
    if (tdate < 10) {
        tdate = "0" + tdate;
    }
    var maxDate = year + "-" + month + "-" + tdate;
    document.getElementById("checkInDate").setAttribute("min", maxDate);
    document.getElementById("checkOutDate").setAttribute("min", maxDate);
    console.log(maxDate);
}
