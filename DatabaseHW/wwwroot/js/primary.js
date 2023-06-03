"use strict";

window.addEventListener("onMapLoad", (() => {
    return () => {
        const map = window.map;
        const aMap = window.aMap;
        const model = window.model;

        if (model.longitude < 10000000 && model.latitude < 10000000 && model.range <= 10) {
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
        setPrimaryList(model, map, aMap);
    };
})());

function setPrimaryList(model, map, aMap) {
    $.getJSON("/Primary/GetPrimaryList", model,
        (data) => {
            $.each(data, (i, item) => {
                const marker = new aMap.Marker({
                    position: new aMap.LngLat(item.longitude, item.latitude),
                    map: map
                });
                marker.setMap(map);
                });
        });
}