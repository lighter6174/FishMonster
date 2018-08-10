
var gameInstance = null;
var gameContainer = null;
var gameCanvas = null;
var loadingContainer = null;
var loadingText = null;
var runtimeInitialized = false;
var canvasAspectRatio = false;

function handleResize() {
    var windowWidth = window.innerWidth;
    var windowHeight = window.innerHeight;
    console.log("window.innerHeight " + window.innerHeight);

    if (gameCanvas != null) {
        var canvasSize = getCanvasSize();
        gameCanvas.width = canvasSize.width;
        gameCanvas.height = canvasSize.height;
    }

    gameContainer.style.height = windowHeight + "px";
}

document.addEventListener("DOMContentLoaded", function (event) {
    createjs.CSSPlugin.install(createjs.Tween);
    createjs.Ticker.setFPS(60);

    gameContainer = document.body.querySelector("#gameContainer");
    window.addEventListener("resize", handleResize);
    handleResize();
});


function OnRuntimeIntialized() {
    runtimeInitialized = true;
    gameCanvas = gameInstance.container.querySelector("canvas");
    gameCanvas.style.width = null;
    gameCanvas.style.height = null;
    document.getElementById("loadingBox").style.display = "none";
    handleResize();
}

function dummyProgressFunction() {

}

function getCanvasSize() {
    var windowWidth = window.innerWidth;
    var windowHeight = window.innerHeight;

    if (canvasAspectRatio) {
        var aspectWindowHeight = windowWidth / canvasAspectRatio;
        if (aspectWindowHeight > windowHeight) {
            windowWidth = windowHeight * canvasAspectRatio;
        }
        else {
            windowHeight = aspectWindowHeight;
        }
    }

    return { width: windowWidth, height: windowHeight };
}

function instantiateUnity(url, aspectRatio) {

    if (aspectRatio) {
        var aspectRatioComponents = aspectRatio.split(":");
        if (aspectRatioComponents.length != 2) {
            console.exception("Unity: Aspect Ratio tag doesn't follow the expect aspect ratio format A:B e.g. 16:9")
            return;
        }

        canvasAspectRatio = aspectRatioComponents[0] / aspectRatioComponents[1];
    }

    var canvasSize = getCanvasSize();

    gameInstance = UnityLoader.instantiate("gameContainer", url, {
        width: canvasSize.width,
        height: canvasSize.height,
        margin: 0,
        onProgress: UnityProgress,
        Module: {
            onRuntimeInitialized: OnRuntimeIntialized
        }
    });
}

function UnityProgress(gameInstance, progress) {
    if (!gameInstance.Module)
        return;

    if (!gameInstance.loadingBox) {
        gameInstance.loadingBox = document.getElementById("loadingBox");
    }

    if (!gameInstance.spinner) {
        gameInstance.spinner = document.getElementById("spinner");
        gameInstance.spinner.style.display = "none";
    }

    if (!gameInstance.bgBar) {
        gameInstance.bgBar = document.getElementById("bgBar");
    }

    if (!gameInstance.progressBar) {
        gameInstance.progressBar = document.getElementById("progressBar");
    }

    if (!gameInstance.loadingInfo) {
        gameInstance.loadingInfo = document.getElementById("loadingInfo");
        gameInstance.loadingInfo.textContent = "Downloading...";
    }
    
    var length = 200 * Math.min(progress, 1);
    createjs.Tween.removeTweens(gameInstance.progressBar);
    createjs.Tween.get(gameInstance.progressBar).to({ width: length }, 10, createjs.Ease.sineOut);

    gameInstance.loadingInfo.textContent = "Downloading... " + Math.round(progress * 100) + "%";

    if (progress == 1) {
        gameInstance.loadingInfo.textContent = "Preparing...";
        gameInstance.loadingBox.style.display = "none";
    }
}
