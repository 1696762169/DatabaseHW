"use strict";

window.addEventListener("onMapLoad", (() => {
    return () => {
        const map = window.map;
        const aMap = window.aMap;
        const model = window.model;

        // 在指定了范围时显示范围圈
        if (model.range > 0) {
            const lnglat = new aMap.LngLat(model.longitude, model.latitude);
            map.setCenter(lnglat);

            const circle = new aMap.Circle({
                center: lnglat,
                radius: model.range * 1000,
                map: map,
                strokeColor: "#55AAEE",
                strokeWeight: 2,
                fillColor: "#55AAEE",
                fillOpacity: 0.3,
                bubble: true
            });
            circle.setMap(map);
        }

        // 设定查询得到的标记点
        setPrimaryList(model, map, aMap);
    };
})());

function setPrimaryList(model, map, aMap) {
    const icon = new aMap.Icon({
        image: `/icon/${model.type}.png`,  // Icon的图像
        imageSize: new aMap.Size(20, 25)   // 根据所设置的大小拉伸或压缩图片
    });

    $.getJSON("/Primary/GetPrimaryList", model,
        (data) => {
            $.each(data, (index, item) => {
                // 忽略过远的点
                if (model.range > 0 &&
                    model.range * 1000 < aMap.GeometryUtil.distance(new aMap.LngLat(model.longitude, model.latitude),
                        new aMap.LngLat(item.longitude, item.latitude)))
                    return true;
                // 添加标记点
                const marker = new aMap.Marker({
                    position: new aMap.LngLat(item.longitude, item.latitude),
                    map: map,
                    icon: icon
                });
                marker.setMap(map);
                return true;
            });
        });
}