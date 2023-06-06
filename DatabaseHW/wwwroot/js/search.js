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
                        // 设置点击事件
                        marker.on("click",
                            () => {
                                // 清除上一次的查询结果
                                $.each(currentMarkers, (index, item) => item.setMap(null));
                                if (modelCopy.type === "Workplace")
                                    setJobList(null);
                                else
                                    setHouseList(null);
                                currentMarkers = [];
                                if (currentCircle != null) {
                                    currentCircle.setMap(null);
                                }
                                // 生成结果列表
                                if (model.type === "Workplace")
                                    setJobList(item.data);
                                else
                                    setHouseList(item.data);

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
                                        $.each(ret,
                                            (index, item) => {
                                                // 记录标记
                                                currentMarkers.push(item.marker);
                                                item.marker.on("click",
                                                    () => {
                                                        // 点击时生成结果列表
                                                        if (modelCopy.type === "Workplace")
                                                            setJobList(item.data);
                                                        else
                                                            setHouseList(item.data);
                                                    });
                                            });
                                    });
                            });
                    });
            });
    };
})());

// 查询地点并将结果显示在地图上
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

// 查询岗位并将结果显示在列表中
function setJobList(workplace) {
    const container = $("#job-container");
    // 清空列表
    container.empty();
    if (workplace == null) {
        return;
    }

    // 创建地点信息
    container.append(createLocationCard(workplace));
    // 查询职位列表
//$.getJSON("/Job/GetJobList",
}

// 查询出租房并将结果显示在列表中
function setHouseList(community) {
    const container = $("#house-container");
    // 清空列表
    container.empty();
    if (community == null) {
        return;
    }

    // 创建地点信息
    container.append(createLocationCard(community));
    // 查询房屋列表
}

// 显示地点
function createLocationCard(location) {
    const locationItem = $("<div>").addClass("card mb-3");
    const locationBody = $("<div>").addClass("card-body");

    const nameElement = $("<h5>").addClass("card-title").text(location.name);
    locationBody.append(nameElement);

    const addressElement = $("<p>").addClass("card-text").text(`地址: ${location.address}`);
    locationBody.append(addressElement);

    const locationElement = $("<p>").addClass("card-text").text(`经度: ${location.longitude}，纬度: ${location.latitude}`);
    locationBody.append(locationElement);

    locationItem.append(locationBody);
    return locationItem;
}