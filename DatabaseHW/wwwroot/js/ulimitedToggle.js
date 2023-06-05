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
function toggleUnlimited(valueId, toggleId, unlimitedValue, defaultValue) {
    const valueInput = document.getElementById(valueId);
    const toggleCheckbox = document.getElementById(toggleId);
    if (toggleCheckbox.checked) {
        valueInput.disabled = true;
        valueInput.value = unlimitedValue;
    } else {
        valueInput.disabled = false;
        valueInput.value = defaultValue;
    }
}