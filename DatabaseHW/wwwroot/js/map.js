"use strict";

var onMapLoad = new Event("onMapLoad");

(function () {
    window._AMapSecurityConfig = {
        securityJsCode: "91f7766c979d09a572c5dc7080ea56c9"
    };
    window.AMapLoader.load({
        key: "fcc311db278567f84ea864c3e85d8e04",       // 申请好的Web端开发者Key，首次调用 load 时必填
        version: "2.0"                 // 指定要加载的 JS API 的版本
    }).then((aMap) => {
        window.aMap = aMap;
        window.map = new aMap.Map("map-container", {
            zoom: 16,   // 缩放级别
            center: [113.356589, 23.168638]   // 中心点坐标
        });
        window.dispatchEvent(onMapLoad);
    }).catch((e) => {
        console.error(e);  //加载错误提示
    });
})();
