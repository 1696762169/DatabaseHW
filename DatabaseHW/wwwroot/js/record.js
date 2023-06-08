"use strict";

$(document).ready(() => {
    // 监听文本框的点击事件
    $("#key-input").on("click", (event) => {
        // 查找历史记录
        $.getJSON("/Primary/GetRecordList", null,
            (records) => {
                // 获取历史记录容器
                const container = $("#record-container");
                container.empty().addClass("p-2 mx-2");
                if (records.length === 0) {
                    return;
                }

                // 历史记录标题
                const title = $("<div>").addClass("input-group mb-2")
                    .append($("<label>").addClass("input-group-text")
                    .text("历史记录"));
                container.append(title);

                // 添加历史记录项
                const recordList = setRecordList(records);

                // 添加清空历史记录按钮
                const clearButton = $("<button>").addClass("btn btn-outline-danger list-group-item").text("清空历史记录");
                clearButton.on("click", (e) => {
                    $.get("/Primary/RemoveAllRecord");
                    container.empty();
                });
                recordList.append(clearButton);

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
    // 查询历史记录
    const recordList = $("<ul>").addClass("list-group");

    // 历史记录列表
    records.forEach((record) => {
        const recordItem = $("<li>").addClass("list-group-item");
        recordItem.hover(() => recordItem.css("background-color", "#BBDDFF"),
            () => recordItem.css("background-color", "white"));

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
                    originEvent: { target: null }
                });
                // 设置范围
                $("#range-slider").val(record.range);
                $("#range-slider").trigger("input", record.range);
            }
        });

        // 创建删除按钮
        const deleteButton = $("<label>").addClass("float-end").css("color", "red").css("cursor", "pointer").text("删除");
        recordItem.append(deleteButton);

        // 添加删除按钮点击事件
        deleteButton.on("click", (e) => {
            // 删除历史记录
            $.get("/Primary/RemoveRecord", record);
            // 删除列表项
            const listItem = $(e.target).closest("li");
            listItem.remove();
            // 防止事件冒泡
            e.stopPropagation();
        });

        recordList.append(recordItem);
    });

    return recordList;
}
