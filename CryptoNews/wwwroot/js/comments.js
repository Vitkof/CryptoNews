let commentsShow = document.getElementById('comments-show');
let isShowed = false;

function toggleComments(newsId) {
    //let url = window.location.pathname;
    //let id = url.substring(url.lastIndexOf('/') + 1);

    if (commentsShow != null) {
        if (isShowed == true) {
            commentsShow.innerHTML = 'Show comments';
            document.getElementById('comments-container').innerHTML = '';
        } else {
            commentsShow.innerHTML = 'Hide comments';
            let commentsContainer = document.getElementById('comments-container');


            loadingComments(newsId, commentsContainer);

        }
        isShowed = !isShowed;
    }

    commentsShow.addEventListener('onclose', function () {
        alert('closed');
    }, false);
}

function loadingComments(newsId, commentsContainer)
{
    let request = new XMLHttpRequest();
    request.open('GET', `/Comments/List?newsId=${newsId}`, true);

    request.onload = function ()
    {
        if (request.status >= 200 && request.status < 400)
        {
            let resp = request.responseText;
            commentsContainer.innerHTML = resp;

            document.getElementById('send-comment')
                .addEventListener("click", createComment);
        }
    }
    request.send();
}


function validateCommentData() {

}

function createComment() {

    let commentText = document.getElementById('commentText').value;
    let newsId = document.getElementById('newsId').value;

    validateCommentData();

    var postRequest = new XMLHttpRequest();
    postRequest.open("POST", '/Comments/Create', true);
    postRequest.setRequestHeader('Content-Type', 'application/json');

    //let requestData = new {
    //    commentText: commentText
    //}

    postRequest.send(JSON.stringify({
        commentText: commentText,
        newsId: newsId
    }));

    postRequest.onload = function () {
        if (postRequest.status >= 200 && postRequest.status < 400) {
            document.getElementById('commentText').value = '';

            //commentsContainer.innerHTML += '';

            loadingComments(newsId);
        }
    }
}

var getCommentsIntervalId = setInterval(function () {
    loadingComments(newsId);
}, 15000);




function loadCommentWithJquery(newsId, commentsContainer)
{
    $.ajax({
        url: `/Comments/List?newsId=${newsId}`

    }).done(function (data) {
        commentsContainer.html(data);
        commentsContainer.append(
            '<button id="special-button" type="button" class="btn btn-primary">Add</button>');


    })
        .fail(function () {
            alert("error");
        });

}

//document.onmousemove = function (e) {
//    let mousecoords = getMousePos(e);
//    console.log(`x = ${mousecoords.x} y =${mousecoords.y}`);
//};
//function getMousePos(e) {
//    return { x: e.clientX, y: e.clientY };
//}

//commentsDisplaySwitcherElement.onmouseover = function () {
//    commentsDisplaySwitcherElement.className = commentsDisplaySwitcherElement.className.replace("btn-primary", "btn-info");
//}
//commentsDisplaySwitcherElement.onmouseout = function () {
//    commentsDisplaySwitcherElement.className = commentsDisplaySwitcherElement.className.replace("btn-info", "btn-primary");
//}
/*
 * Mouse events
 * click
 * contextmenu
 * mouseover/mouseout
 * mousedown / mouseup
 * mousemove
 *
 * Form control events
 * submit
 * change
 * focus
 *
 * Keyboard events
 * keydown / keyup
 *
 * Document events
 * DOMContentLoaded
 */