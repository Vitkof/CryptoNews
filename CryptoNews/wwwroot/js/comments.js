document.getElementById('send-comment').addEventListener("click", createComment);
let commentsShow = document.getElementById('comments-show');
let commentsContainer = document.getElementById('comments-container');
let isShowed = false;

function toggleComments(newsId) {

    if (commentsShow != null) {
        if (isShowed == true) {
            commentsShow.innerHTML = 'Show comments';
            commentsContainer.innerHTML = '';
        } else {
            commentsShow.innerHTML = 'Hide comments';
            
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
        }
    }
    reqGet.send();
}


function validateCommentData() {

}

function createComment() {

    let commText = document.getElementById('commText').value;
    let newsId = document.getElementById('newsId').value;

    validateCommentData();

    var reqPost = new XMLHttpRequest();
    reqPost.open("POST", '/Comments/Create', true);
    reqPost.setRequestHeader('Content-Type', 'application/json');

    reqPost.send(JSON.stringify({
        commText: commText,
        newsId: newsId
    }));

    reqPost.onload = function () {
        if (reqPost.status >= 200 && reqPost.status < 400) {
            document.getElementById('commText').value = '';

            loadingComments(newsId, commentsContainer);
        }
    }   
}


function getCommentsIntervalId(newsId) {
    setInterval(function () {
        if (isShowed == true) {
            loadingComments(newsId, commentsContainer);
        }
    }, 15000);
}


function counting(text) {
    const limit = 1000;
    let count = text.length;
    let commentBlock = document.getElementById('mb-3');
    let lm = document.getElementById('limitMsg');

    if (count > limit-100) {
        let p = document.createElement('p');
        p.id = "limitMsg";

        if (count <= limit) {
            let left = limit - count;
            p.innerHTML = '<span style="color:green; font-weight:600">'
                + left + '</span> characters to the limit';
        }
        else {
            let right = count - limit;
            p.innerHTML = '<span style="color:red; font-weight:600">'
                + right + '</span> characters over the limit';
        }

        if (commentBlock.contains(lm)) {
            commentBlock.replaceChild(p, lm);
        }
        else {
            commentBlock.append(p);
        }
    }
    else {
        if(lm != null) commentBlock.removeChild(lm);
    }
}
