if (!window['YT']) { var YT = { loading: 0, loaded: 0 }; } if (!window['YTConfig']) { var YTConfig = { 'host': 'http://www.youtube.com' }; } if (!YT.loading) { YT.loading = 1; (function () { var l = []; YT.ready = function (f) { if (YT.loaded) { f(); } else { l.push(f); } }; window.onYTReady = function () { YT.loaded = 1; for (var i = 0; i < l.length; i++) { try { l[i](); } catch (e) { } } }; YT.setConfig = function (c) { for (var k in c) { if (c.hasOwnProperty(k)) { YTConfig[k] = c[k]; } } }; var a = document.createElement('script'); a.type = 'text/javascript'; a.id = 'www-widgetapi-script'; a.src = 'https://s.ytimg.com/yts/jsbin/www-widgetapi-vfl3m9ZW-/www-widgetapi.js'; a.async = true; var c = document.currentScript; if (c) { var n = c.nonce || c.getAttribute('nonce'); if (n) { a.setAttribute('nonce', n); } } var b = document.getElementsByTagName('script')[0]; b.parentNode.insertBefore(a, b); })(); }
var player;
var videotime = 0;
var timeupdater = null;
var youtubeIframeID = "youtube-intro-frame";
var imgSelector = "#video-background-home .avatar";
function onYouTubeIframeAPIReady() {
    player = new YT.Player(youtubeIframeID, {
        events: {
            'onReady': onPlayerReady
        }
    });
}

function onPlayerReady(event) {
    event.target.playVideo();

    function updateTime() {
        var oldTime = videotime;
        if (player && player.getCurrentTime) {
            videotime = player.getCurrentTime();
        }
        if (videotime !== oldTime) {
            onProgress(videotime);
        }
    }
    timeupdater = setInterval(updateTime, 100);
}

function onProgress(currentTime) {
    if (currentTime > 0.0001)
        $(imgSelector).hide();

    if (currentTime > 4.5)
        player.seekTo(0);
}