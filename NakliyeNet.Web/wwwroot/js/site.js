function showThumbnail(input, img) {
    if (input.files && input.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            //$(img).attr('src', e.target.result);
            $(img).css('background-image', 'url(' + e.target.result + ')');
        };
        reader.readAsDataURL(input.files[0]);
    }
}

function getDistanceBetweenAddress(fromAddress, toAdress, fnDistance) {
    var baseurl = window.location.origin + "/home/openStreetMapSearch";
    // İki adresi ve aracınızın özelliklerini tanımlayın
    var baslangicAdresi = fromAddress;
    var varisAdresi = toAdress;
    var aracTipi = "car"; // Araba için kullanıyoruz, diğer seçenekler için belirli bir sürüş modu belirtin

    // OpenStreetMap yönlendirme hizmetini kullanarak yol tarifleri alın
    var directionsService = new L.Routing.osrmv1({
        serviceUrl: "https://router.project-osrm.org/route/v1",
        profile: aracTipi, // Araba kullanılacaksa "car", diğer seçenekler için belirli bir sürüş modu belirtin
    });

    var url = `${baseurl}?q=${baslangicAdresi}`;
    fetch(url).then(response => response.json()).then(data => {
        var url2 = `${baseurl}?q=${varisAdresi}`;
        fetch(url2).then(response => response.json()).then(data2 => {
            var waypoints = [{
                latLng: { lat: data[0].lat, lng: data[0].lon }
            }, {
                latLng: { lat: data2[0].lat, lng: data2[0].lon }
            }];

            directionsService.route(waypoints, function (err, route) {
                if (!err) {
                    var distance = route[0].summary.totalDistance / 1000; // Mesafe kilometre cinsinden
                    fnDistance(distance)
                } else {
                    console.log("Yol tarifi alınamadı: " + err.message);
                    fnDistance(0)
                }
            });
        })
    })

}

$(function () {
    var current = location.pathname;
    $('.nav-item .nav-link').each(function () {
        var $this = $(this);
        // if the current path is like this link, make it active
        if ($this.attr('href').startsWith(current)) {
            $this.parent().addClass('active');
        }
    })
})