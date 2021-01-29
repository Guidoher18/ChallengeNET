//Elimina los ADS de Somee

// Based on: https://developer.mozilla.org/es/docs/Web/API/MutationObserver

// selecciona el nodo target
var target = document.body;

// Crea una instancia de observer
var observer = new MutationObserver(function () { chauads() });

// Configura el observer:
var config = { attributes: true, childList: true, characterData: true };

// pasa al observer el nodo y la configuracion
observer.observe(target, config);

// Posteriormente, puede detener la observacion
setTimeout(function () {
    observer.disconnect()
}, 20000)

function chauads() {
    console.log("byeads!");
    $("script[lang='JavaScript']").remove();
    $("script[src*='ads.mgmt']").remove();
    $('div[style="height: 65px;"]').remove();
    $('div[style="opacity: 0.9; z-index: 2147483647; position: fixed; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: #202020; margin: 0px; padding: 0px;"]').remove();
    $('div[style*="position: fixed; z-index: 2147483647; left: 0px; bottom: 0px; height: 65px; right: 0px; display: block; width: 100%; background-color: transparent; margin: 0px; padding: 0px;"]').remove();
}

// Wire up the events as soon as the DOM tree is ready
$(document).ready(function () {
    //---Elimina la publicidad agregada por el servidor
    $('center').remove();
    $('script[src="http://ads.mgmt.somee.com/serveimages/ad2/WholeInsert4.js"]').remove();
    //$('body>div:not(div:first-child)').remove();
    //---
    wireUpEvents();
});