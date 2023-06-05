"use strict";

window.addEventListener("onMapLoad", (() => {
    let currentMarkers = [];
    let currentCircle = null;

    return () => {
        const map = window.map;
        const aMap = window.aMap;
        const model = window.model;

        // 设定查询得到的标记点
        setPrimaryMarker(model, map, aMap)
            .then((ret) => {
                // 设置点击标记时查询相关的标记并显示
                $.each(ret,
                    (index, item) => {
                        // 构造新的查询模型
                        const marker = item.marker;
                        const markerLngLat = new aMap.LngLat(marker.getPosition().getLng(), marker.getPosition().getLat());
                        const modelCopy = {
                            type: model.type === "Workplace" ? "Community" : "Workplace",
                            longitude: markerLngLat.lng,
                            latitude: markerLngLat.lat,
                            range: model.range > 0 ? model.range / 2 : 2
                        };
                        marker.on("click",
                            () => {
                                // 清除上一次的查询结果
                                $.each(currentMarkers, (index, item) => item.setMap(null));
                                currentMarkers = [];
                                if (currentCircle != null) {
                                    currentCircle.setMap(null);
                                }
                                // 绘制圆圈
                                map.setCenter(markerLngLat);
                                currentCircle = new aMap.Circle({
                                    center: markerLngLat,
                                    radius: modelCopy.range * 1000,
                                    map: map,
                                    strokeColor: "#55AAEE",
                                    strokeWeight: 2,
                                    fillColor: "#55AAEE",
                                    fillOpacity: 0.15,
                                    bubble: true
                                });
                                currentCircle.setMap(map);

                                // 设置相关标记点并记录
                                setPrimaryMarker(modelCopy, map, aMap)
                                    .then((ret) => {
                                        $.each(ret, (index, item) => currentMarkers.push(item.marker));
                                    });
                            });
                    });
            });
    };
})());

function setPrimaryMarker(model, map, aMap) {
    const icon = new aMap.Icon({
        image: `/icon/${model.type}.png`,  // Icon的图像
        imageSize: new aMap.Size(20, 25)   // 根据所设置的大小拉伸或压缩图片
    });
    return new Promise((resolve, reject) => {
        $.getJSON("/Primary/GetPrimaryList",
            model,
            (data) => {
                const ret = [];
                $.each(data,
                    (index, item) => {
                        // 忽略过远的点
                        if (model.range > 0 &&
                            model.range * 1000 <
                            aMap.GeometryUtil.distance(new aMap.LngLat(model.longitude, model.latitude),
                                new aMap.LngLat(item.longitude, item.latitude)))
                            return true;
                        // 添加标记点
                        const marker = new aMap.Marker({
                            position: new aMap.LngLat(item.longitude, item.latitude),
                            map: map,
                            icon: icon
                        });
                        marker.setMap(map);
                        ret.push({ data: item, marker: marker });
                        return true;
                    });
                resolve(ret); // 解析 Promise，并传递数据作为解决值
            })
            .fail((jqXHR, textStatus, err) => {
                reject(err); // 解析 Promise，并传递错误作为拒绝值
            });
    });
}