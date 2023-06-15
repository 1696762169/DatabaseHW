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
                // 设置地图视野
                setMapBounds(ret, map, aMap);

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
                                    setList(null, "job");
                                else
                                    setList(null, "house");
                                currentMarkers = [];
                                if (currentCircle != null) {
                                    currentCircle.setMap(null);
                                }

                                // 生成结果列表
                                if (model.type === "Workplace")
                                    setList(item.data, "job", setJobList);
                                else
                                    setList(item.data, "house", setHouseList);

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
                                                // 记录标记和窗口
                                                currentMarkers.push(item.marker);
                                                item.marker.on("click",
                                                    () => {
                                                        // 点击时生成结果列表
                                                        if (modelCopy.type === "Workplace")
                                                            setList(item.data, "job", setJobList);
                                                        else
                                                            setList(item.data, "house", setHouseList);
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
                        const lnglat = new aMap.LngLat(item.longitude, item.latitude);
                        const marker = new aMap.Marker({
                            position: lnglat,
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
// 根据坐标设置地图视野
function setMapBounds(locations, map, aMap) {
    if (locations.length === 0) {
        return;
    }
    const southWest = new aMap.LngLat(locations[0].data.longitude, locations[0].data.latitude);
    const northEast = new aMap.LngLat(locations[0].data.longitude, locations[0].data.latitude);
    $.each(locations,
        (index, item) => {
            southWest.lng = Math.min(southWest.lng, item.data.longitude);
            southWest.lat = Math.min(southWest.lat, item.data.latitude);
            northEast.lng = Math.max(northEast.lng, item.data.longitude);
            northEast.lat = Math.max(northEast.lat, item.data.latitude);
        });
    map.setBounds(new aMap.Bounds(southWest, northEast));
}

// 查询二级结果并将结果显示在列表中
function setList(location, name, showListFunc) {
    const container = $(`#${name}-container`);
    //ReSharper disable once InconsistentNaming
    const Name = name.charAt(0).toUpperCase() + name.slice(1);
    // 清空列表
    container.removeClass("bg-light").empty();
    // 清空公司信息
    if (name === "job") {
        $("#company-container").empty();
    }
    if (location == null) {
        return;
    }

    // 创建地点信息
    container.addClass("bg-light").append(setLocationCard(location));
    const locationId = location.communityId ?? location.workplaceId;
    // 查询筛选条件
    $.getJSON(`/Condition/Get${Name}ConditionJson`, null,
        (condition) => {
            // 查询职位列表
            $.ajax(`/Secondary/Get${Name}List`,
                {
                    type: "POST",
                    contentType: "application/json",
                    data: JSON.stringify({ id: locationId, condition: condition }),
                    success: (data) => {
                        if (data.length === 0) {
                            container.append($("<p>").text("没有搜索到合适的结果\n请调整筛选条件后重试"));
                            return;
                        }
                        const list = $("<div>").addClass("list-group");
                        showListFunc(data, list);

                        // 在外部包装容器中添加固定高度和滚动样式
                        const wrapper = $("<div>")
                            .addClass("job-list-wrapper overflow-auto")
                            .css("max-height", "75%")
                            //.css("position", "absolute")
                            .append(list);
                        container.append(wrapper);
                    }
                });
        });
}

// 显示地点
function setLocationCard(location) {
    const locationItem = $("<div>").addClass("card mb-3 mt-3").css("height", "20%");
    const locationBody = $("<div>").addClass("card-body");

    const nameElement = $("<h5>").addClass("card-title").text(location.name);
    locationBody.append(nameElement);

    const addressElement = $("<p>").addClass("card-text").text(`地址: ${location.address}`);
    locationBody.append(addressElement);

    const locationElement = $("<p>").addClass("card-text").text(`经度: ${location.longitude}，纬度: ${location.latitude}`);
    locationBody.append(locationElement);

    const tipsElement = $("<p>").addClass("card-text").text(`点击列表中卡片可查看详细信息`);
    locationBody.append(tipsElement);

    locationItem.append(locationBody);
    return locationItem;
}

// 显示岗位
function setJobList(jobs, list) {
    // 遍历工作岗位列表
    jobs.forEach(job => {
        const item = $("<div>").addClass("list-group-item");

        // 卡片
        const card = $("<div>").addClass("card");

        // 卡片主体
        const cardBody = $("<div>").addClass("card-body");

        // 岗位名称改为 h5
        const nameElement = $("<h5>").addClass("card-title").text(job.name);
        cardBody.append(nameElement);

        // 实习日薪范围
        const salaryText = (job.salaryMin === job.salaryMax) ? job.salaryMin : `${job.salaryMin} - ${job.salaryMax}`;
        const salaryElement = $("<p>").addClass("card-text").text(`实习日薪: ${salaryText} 元`);
        cardBody.append(salaryElement);

        // 实习时长范围
        const periodText = (job.periodMin === job.periodMax) ? job.periodMin : `${job.periodMin} - ${job.periodMax}`;
        const periodElement = $("<p>").addClass("card-text").text(`实习时长: ${periodText} 月`);
        cardBody.append(periodElement);

        // 出勤频率范围
        const freText = (job.freMin === job.freMax) ? job.freMin : `${job.freMin} - ${job.freMax}`;
        const freElement = $("<p>").addClass("card-text").text(`出勤频率: ${freText} 日/每周`);
        cardBody.append(freElement);

        // 岗位分类
        const jobTypeText = getJobTypeText(job.type);
        const jobTypeElement = $("<p>").addClass("card-text").text(`岗位分类: ${jobTypeText}`);
        cardBody.append(jobTypeElement);

        // 学历要求
        const academicText = getAcademicTypeText(job.academic);
        const academicElement = $("<p>").addClass("card-text").text(`学历要求: ${academicText}`);
        cardBody.append(academicElement);

        // 岗位详细信息
        showDetails(cardBody, job.description);
        // 公司信息
        const companyButton = $("<button>").addClass("btn btn-success btn-sm ms-3").text("查看公司信息");
        companyButton.on("click", () => showCompanyInfo(job.companyId));
        cardBody.append(companyButton);

        card.append(cardBody);
        item.append(card);
        list.append(item);
    });
}

// 显示详细信息
function showDetails(cardBody, description) {
    // 详细信息文本
    const detailsElement = $("<p>").addClass("card-text details").hide();
    cardBody.append(detailsElement);

    // 展开/收起按钮
    const toggleButton = $("<button>").addClass("btn btn-primary btn-sm").text("展开/收起详细信息");
    cardBody.append(toggleButton);

    // 按钮点击事件
    toggleButton.on("click", () => {
        if (detailsElement.text().trim().length === 0) {
            detailsElement.text(`详细信息：\n${description}`);
        }
        detailsElement.slideToggle();
    });
}
// 获取岗位分类文本
function getJobTypeText(jobType) {
    switch (jobType) {
        case 1:
            return "Unity客户端开发";
        case 2:
            return "C#软件开发";
        case 3:
            return "游戏引擎开发";
        case 4:
            return "技术美术";
        case 5:
            return "游戏系统策划";
        default:
            return "其它";
    }
}
// 获取学历要求文本
function getAcademicTypeText(academicType) {
    switch (academicType) {
        case 1:
            return "高中及以下";
        case 2:
            return "大专";
        case 3:
            return "本科";
        case 4:
            return "硕士";
        case 5:
            return "博士";
        default:
            return "学历不作要求";
    }
}

// 显示房屋
function setHouseList(houses, list) {
    // 遍历房屋信息列表
    houses.forEach(house => {
        const item = $("<div>").addClass("list-group-item");

        // 卡片
        const card = $("<div>").addClass("card");

        // 卡片主体
        const cardBody = $("<div>").addClass("card-body");

        // 租房标题改为 h5
        const titleElement = $("<h5>").addClass("card-title").text(house.title);
        cardBody.append(titleElement);

        // 月租价格
        const priceElement = $("<p>").addClass("card-text").text(`月租价格: ${house.price} 元`);
        cardBody.append(priceElement);

        // 房屋面积
        const areaElement = $("<p>").addClass("card-text").text(`房屋面积: ${house.area} 平方米`);
        cardBody.append(areaElement);

        // 租期
        const termText = (house.termMin === house.termMax) ? house.termMin : `${house.termMin} - ${house.termMax}`;
        const termElement = $("<p>").addClass("card-text").text(`租期: ${termText} 月`);
        cardBody.append(termElement);

        // 租赁方式
        let leaseTypeText = "";
        if (house.entire === 1 && house.share === 0) {
            leaseTypeText = "仅可整租";
        } else if (house.entire === 0 && house.share === 1) {
            leaseTypeText = "仅可合租";
        } else if (house.entire === 1 && house.share === 1) {
            leaseTypeText = "可整租/合租";
        }
        const leaseTypeElement = $("<p>").addClass("card-text").text(`租赁方式: ${leaseTypeText}`);
        cardBody.append(leaseTypeElement);

        card.append(cardBody);
        item.append(card);
        list.append(item);
    });
}

// 显示公司信息
function showCompanyInfo(companyId) {
    // 查询公司信息
    $.getJSON("/Secondary/GetCompany",
        { companyId: companyId },
        (company) => {
            const container = $("#company-container");

            // 清空容器内容
            container.empty();
            const card = $("<div>").addClass("card");
            const cardBody = $("<div>").addClass("card-body");

            // 公司名称
            const nameElement = $("<h3>").addClass("card-title").text(company.name);
            cardBody.append(nameElement);

            // 公司人员规模
            const scaleText = getScaleTypeText(company.scale);
            const scaleElement = $("<p>").addClass("card-text").text(`公司人员规模: ${scaleText}`);
            cardBody.append(scaleElement);

            // 公司融资情况
            const financingText = getFinanceTypeText(company.financing);
            const financingElement = $("<p>").addClass("card-text").text(`公司融资情况: ${financingText}`);
            cardBody.append(financingElement);

            // 公司介绍
            const introElement = $("<p>").addClass("card-text").text(`公司介绍: ${company.introduction}`);

            cardBody.append(introElement);
            card.append(cardBody);
            container.append(card);
        });
}
// 获取公司人员规模的文本
function getScaleTypeText(scale) {
    switch (scale) {
    case 0:
        return "初创/小型公司，100人以下";
    case 1:
        return "中等规模公司，100~999人";
    case 2:
        return "大型公司，1000~9999人";
    case 3:
        return "行业巨头，10000人以上";
    default:
        return "未知";
    }
}
// 获取公司融资情况的文本
function getFinanceTypeText(financing) {
    switch (financing) {
    case 0:
        return "未获得融资";
    case 1:
        return "天使轮融资";
    case 2:
        return "A轮融资";
    case 3:
        return "B轮融资";
    case 4:
        return "C轮融资";
    case 5:
        return "D轮及以上融资";
    case 6:
        return "上市公司";
    default:
        return "未知";
    }
}
