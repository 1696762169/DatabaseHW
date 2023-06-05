"use strict";

function setCondition(name, submit) {
    const container = $(`#${name}-condition-container`);
    const url = container.text();
    const btn = $(`#${name}-condition-btn`);
    container.hide();

    btn.click(() => {
        btn.hide();
        $.get(url,
            (response) => {
                container.html(response);
                container.show();
                $(`#${name}-condition`).submit(submit);
            });
    });
};

function saveJobCondition(event) {
    event.preventDefault(); // 阻止默认的表单提交行为

    $.ajax({
        url: "/Condition/SaveJobCondition", // 提交到的控制器和动作方法
        type: "POST", // 请求类型
        data: $(this).serializeArray(), // 表单数据
        success: (response) => {
            $("#job-condition-container").html(response); // 将返回的局部视图的 HTML 插入到指定容器中
            $("#job-condition-container").hide();
            $("#job-condition").submit(saveJobCondition);
            $("#job-condition-btn").show();
        }
    });
}

function saveHouseCondition(event) {
    event.preventDefault(); // 阻止默认的表单提交行为
    const temp = $(this).serializeArray();
    const temp2 = $(this).serialize();

    $.ajax({
        url: "/Condition/SaveHouseCondition", // 提交到的控制器和动作方法
        type: "POST", // 请求类型
        data: $(this).serializeArray(), // 表单数据
        success: (response) => {
            $("#house-condition-container").html(response); // 将返回的局部视图的 HTML 插入到指定容器中
            $("#house-condition-container").hide();
            $("#house-condition").submit(saveHouseCondition);
            $("#house-condition-btn").show();
        }
    });
}

setCondition("job", saveJobCondition);
setCondition("house", saveHouseCondition);