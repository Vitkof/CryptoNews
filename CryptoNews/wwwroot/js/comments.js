let commentsShow = document.getElementById('comments-show');
let isShowed = false;

function toggleComments(newsId) {

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
    let reqGet = new XMLHttpRequest();
    reqGet.open('GET', `/Comments/List?newsId=${newsId}`, true);

    reqGet.onload = function ()
    {
        if (reqGet.status >= 200 && reqGet.status < 400)
        {
            let resp = reqGet.responseText;
            commentsContainer.innerHTML = resp;

            document.getElementById('send-comment')
                .addEventListener("click", createComment);
        }
    }
    reqGet.send();
}


function validateCommentData() {

}

function createComment() {

    let commText = document.getElementById('commText').value;
    let newsId = document.getElementById('newsId').value;

    //validateCommentData();

    var reqPost = new XMLHttpRequest();
    reqPost.open("POST", '/Comments/Create', true);
    reqPost.setRequestHeader('Content-Type', 'application/json');

    //let requestData = new {
    //    commentText: commentText
    //}

    reqPost.send(JSON.stringify({
        commText: commText,
        newsId: newsId
    }));

    reqPost.onload = function () {
        if (reqPost.status >= 200 && reqPost.status < 400) {
            document.getElementById('commText').value = '';

            //commentsContainer.innerHTML += '';

            loadingComments(newsId);
        }
    }   
}

//var getCommentsIntervalId = setInterval(function () {
//    loadingComments(newsId);
//}, 15000);