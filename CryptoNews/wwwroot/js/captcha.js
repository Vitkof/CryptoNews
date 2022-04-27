$("#img-captcha")
    .mousemove(function (e) {
        $hint = $(this).attr("alt");
        $height = $(this).height();

        $("#hint-captcha").css({
            top: $height
        }).text($hint)
          .show();
    })
    .mouseout(function () {
        $("#hint-captcha").hide();
    });


$("#img-captcha").click(function () {
    resetCaptchaImage();
});

function resetCaptchaImage() {
    d = new Date();
    $("#img-captcha").attr("src", "/captcha-image?" + d.getTime());
}
