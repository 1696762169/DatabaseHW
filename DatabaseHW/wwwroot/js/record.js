"use strict";

$(document).ready(() => {
    // 查找历史记录
    $.getJSON("/Primary/GetRecordList", null,
        (records) => {
            // 监听文本框的点击事件
            $("#key-input").on("click", (event) => {
                // 创建历史记录容器
                const container = $("#record-container");
                container.empty().addClass("p-2 mx-2");

                // 历史记录标题
                const title = $("<div>").addClass("input-group mb-2").append($("<label>").addClass("input-group-text").text("历史记录"));
                container.append(title);

                // 添加历史记录项
                const recordList = setRecordList(records);

                // 显示历史记录列表
                container.append(recordList);

                // 阻止事件冒泡，防止立即隐藏历史记录列表
                event.stopPropagation();
            });
        });

    // 点击文档其他位置时隐藏历史记录列表
    $(document).on("click", (event) => {
        $("#record-container").empty().removeClass("p-2");
    });
    //// 点击历史记录列表时阻止事件冒泡，防止关闭列表
    $(document).on("click", "#record-container", (event) => {
        event.stopPropagation();
    });
});

// 生成历史记录列表
function setRecordList(records) {
    const recordList = $("<ul>").addClass("list-group");
    records.forEach((record) => {
        const recordItem = $("<li>").addClass("list-group-item");

        // 设置历史记录文本
        const key = record.key != null ? `关键字：${record.key}    ` : "";
        const showLocation = record.longitude <= 180 && record.latitude <= 90;
        const longitude = showLocation ? `经度：${record.longitude} ` : "";
        const latitude = showLocation ? `纬度：${record.latitude} ` : "";
        const range = showLocation ? `范围：${record.range.toFixed(2)}km ` : "";
        recordItem.text(`${key}${longitude}${latitude}${range}`);

        // 添加点击事件
        recordItem.on("click", (e) => {
            // 设置关键字
            if (record.key != null) {
                $("#key-input").val(record.key);
            }
            if (record.longitude <= 180 && record.latitude <= 90) {
                // 点击地图
                const map = window.map;
                const aMap = window.aMap;
                map.emit("click", {
                    lnglat: new aMap.LngLat(record.longitude, record.latitude),
                    target: map,
                    type: "click",
                    originEvent: {target: null}
                });
                // 设置范围
                $("#range-slider").val(record.range);
                $("#range-slider").trigger("input", record.range);
            }
        });

        recordList.append(recordItem);
    });
    return recordList;
}