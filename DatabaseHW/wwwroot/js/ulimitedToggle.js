"use strict";

// 初始化不限制选项
function initUnlimited(valueId, toggleId, unlimitedValue) {
    const valueInput = document.getElementById(valueId);
    const toggleCheckbox = document.getElementById(toggleId);
    if (valueInput.value === unlimitedValue.toString()) {
        toggleCheckbox.checked = true;
        toggleUnlimited(valueId, toggleId, unlimitedValue);
    }
}

// 定义勾选不限制选项时的行为
function toggleUnlimited(valueId, toggleId, unlimitedValue, defaultValue, minValue, maxValue) {
    const valueInput = $(`#${valueId}`);
    const toggleCheckbox = $(`#${toggleId}`);
    if (toggleCheckbox.prop("checked")) {
        valueInput.val(unlimitedValue);
        valueInput.hide();
        valueInput.removeAttr("min");
        valueInput.removeAttr("max");
    } else {
        valueInput.show();
        valueInput.val(defaultValue);
        valueInput.attr("min", minValue);
        valueInput.attr("max", maxValue);
    }
}