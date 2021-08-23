window.addEventListener('load', () => {
    let longitude;
    let latitude;
    let locationIcon = document.querySelector(".icon");
    let temperatureDescription = document.querySelector(".temperature-description");
    let temperatureDegree = document.querySelector(".temperature-degree");
    let locationTimezone = document.querySelector(".location-timezone");
    if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(position => {
            longitude = position.coords.longitude;
            latitude = position.coords.latitude;

            const api = `https://api.openweathermap.org/data/2.5/onecall?lat=${latitude}&lon=${longitude}&units=metric&exclude={part}&appid=c8859e79f30784a1be4b0ed1448a6dde`

            fetch(api).then(response => {
                    return response.json()
                })
                .then(data => {
                    console.log(data)
                    const { temp } = data.current

                    var description = data.current.weather[0].description
                    console.log("Before capital: " + description)


                    description = description.toLowerCase().split(' ').map((s) => s.charAt(0).toUpperCase() + s.substr(1)).join(' ');
                    console.log("After capital: " + description)

                    const { icon } = data.current.weather[0];

                    //Set DOM elements from API
                    temperatureDegree.textContent = temp
                    temperatureDescription.textContent = description
                    locationTimezone.textContent = data.timezone
                    locationIcon.innerHTML = `<img src="icons/${icon}.png">`
                });
        });
    } else {
        temperatureDescription.textContent = "Enable location"
        h1.textContent = "Please enable location"
    }


});