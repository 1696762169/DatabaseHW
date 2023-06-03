"use strict";

window.addEventListener("onMapLoad", (() => {
    let currentMaker = null;    // 当前标记
    let currentCircle = null;   // 当前表示范围的圆形
    let currentWindow = null;   // 当前信息窗口

    return function () {
        const map = window.map;
        const aMap = window.aMap;
        // 设置地图点击事件
        map.on("click", function (e) {
            // 清除先前的标记
            if (currentMaker != null) {
                currentMaker.setMap(null);
                currentMaker = null;
            }
            if (currentCircle) {
                currentCircle.setMap(null);
                currentCircle = null;
            }
            if (currentWindow) {
                currentWindow.close();
                currentWindow = null;
            }

            // 添加新标记并记录
            const marker = new aMap.Marker({
                position: e.lnglat,
                map: map
            });
            marker.setMap(map);
            currentMaker = marker;

            // 添加新圆形并记录
            let radiusInMeters;
            if (document.getElementById("range-slider") == null) {
                radiusInMeters = 1000;
            } else {
                radiusInMeters = document.getElementById("range-slider").value * 1000;
            }
            const circle = new aMap.Circle({
                center: e.lnglat,
                radius: radiusInMeters,
                map: map,
                strokeColor: "#55AAEE",
                strokeWeight: 2,
                fillColor: "#55AAEE",
                fillOpacity: 0.3,
                bubble: true
            });
            circle.setMap(map);
            currentCircle = circle;

            // 添加信息窗体
            let infoWindow;
            fetch("/html/markerContent.html")
                .then(response => response.text())
                .then(content => {
                    infoWindow = new aMap.InfoWindow({
                        content: content,
                        offset: new aMap.Pixel(0, -50)
                    });
                    infoWindow.open(map, e.lnglat);
                    document.getElementById("marker-longitude").value = e.lnglat.getLng();
                    document.getElementById("marker-latitude").value = e.lnglat.getLat();
                    // 将经纬度赋值给视图模型属性
                    document.getElementById("model-longitude").value = e.lnglat.getLng();
                    document.getElementById("model-latitude").value = e.lnglat.getLat();
                    document.getElementById("center-label").innerText = `搜索中心经度：${e.lnglat.getLng()} 纬度：${e.lnglat.getLat()}`;
                    if (document.getElementById("range-slider") == null) {
                        document.getElementById("range-div").innerHTML = `
                        <label class="input-group-text" for="modelTypeSelection" id="range-label">搜索范围：1km</label>
                        <input type="range" class="form-range" id="range-slider" max="10.0" min="1.0" step="0.01"/>
                    `;
                        document.getElementById("range-slider").value = 1;
                        // 设置搜索范围更改时的行为
                        document.getElementById("range-slider").addEventListener("input", value => {
                            currentCircle.setRadius(value.target.value * 1000);
                            document.getElementById("range-label").innerText =
                                `搜索范围：${document.getElementById("range-slider").value}km`;
                            document.getElementById("model-range").value =
                                document.getElementById("range-slider").value;
                        });
                    };
                    

                    // 获取地理编码后的地址信息
                    aMap.plugin("AMap.Geocoder", function () {
                        const geocoder = new aMap.Geocoder();
                        geocoder.getAddress(e.lnglat, function (status, result) {
                            if (status === "complete" && result.info === "OK") {
                                const address = result.regeocode.formattedAddress;
                                document.getElementById("marker-address").value = address;
                            }
                        });
                    });
                    currentWindow = infoWindow;
                });
        });
    };
})());
