(() => {
    $.get($("#job-condition-container").text(),
        (response) => {
            $("#job-condition-container").html(response);
            $("#job-condition").submit(saveJobCondition);
            //$("#job-condition").submit((event) => {
            //    event.preventDefault(); // 阻止默认的表单提交行为
            //    $.ajax({
            //        url: "/Condition/SaveJobCondition", // 提交到的控制器和动作方法
            //        type: "POST", // 请求类型
            //        data: $(this), // 表单数据
            //        success: (response) => {
            //            $("#job-condition-container").html(response); // 将返回的局部视图的 HTML 插入到指定容器中
            //        }
            //    });
            //});
        });
})();

function saveJobCondition(event) {
    event.preventDefault(); // 阻止默认的表单提交行为
    const temp = $(this).serializeArray();
    const temp2 = $(this).serialize();

    $.ajax({
        url: "/Condition/SaveJobCondition", // 提交到的控制器和动作方法
        type: "POST", // 请求类型
        data: $(this).serializeArray(), // 表单数据
        success: (response) => {
            $("#job-condition-container").html(response); // 将返回的局部视图的 HTML 插入到指定容器中
            $("#job-condition").submit(saveJobCondition);
        }
    });
}