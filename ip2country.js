const axios = require('axios');
const haversine = require('haversine')
const Redis = require("ioredis");
const { start, ip2CountryUrl, countryInfoUrl, currencyInfoUrl } = require("./config");

const infoOutput = (
    {
        ip, 
        name, 
        alpha2Code,
        languageList,
        currenciesList,
        timeZonesList,
        distance,
        destination,
        formattedDate
    }
) => {
    const output = `
    IP: ${ip}
    Fecha: ${formattedDate}
    País:  ${name}
    ISO Code: ${alpha2Code}
    Idiomas: ${languageList}
    Moneda: ${currenciesList}
    Hora: ${timeZonesList}
    Distancia estimada: ${distance} kms (-34, -64) a (${destination.latitude}, ${destination.longitude})`;
    
    console.log("Resultado", output);
}

const updateStats = async alpha2Code => {
    const redisStats = new Redis(6380); 
    await redisStats.incr(`${alpha2Code}-counter`);
    console.log("updateStats");
    redisStats.disconnect();
}

const run = async() => {
    const redis = new Redis(6379); 
    
    try {
        const myip = process.argv[2] || "5.6.7.8";

        console.log("MyIP: " + myip);
    
        const cacheFullInfo = JSON.parse(await redis.get(myip));
    
        if (cacheFullInfo) {
            console.log("Hit de cache");
            updateStats(cacheFullInfo.alpha2Code); // asíncrono
            infoOutput(cacheFullInfo); 
    
            return true;
        }
    
        const {data: countryBasicInfo} = await axios.get(ip2CountryUrl(myip));
        console.log(countryBasicInfo.countryCode);
        const {data: countryExtraInfo} = await axios.get(countryInfoUrl(countryBasicInfo.countryCode))

        const {
            name,
            alpha2Code,
            latlng,
            languages,
            timezones,
            currencies
        } = countryExtraInfo;

        updateStats(alpha2Code); // asíncrono

        const destination = {
            latitude: latlng[0],
            longitude: latlng[1]
        };
        const date = new Date();
    
        // cache this
        const {data: currencyData} = await axios.get(currencyInfoUrl);
        const {rates} = currencyData;
    
        const distance = Math.round(haversine(start, destination));
    
        const languageList = languages.map(({ name, iso639_1}) => ` ${name}(${iso639_1})`);
        const currenciesList = currencies.map(({ code, symbol}) => ` EUR(1 EUR = ${rates[code]} ${symbol})`);
        const timeZonesList = timezones.map(t => `(${t})`);
        const formattedDate = date.toISOString().replace("T", " ");
        const output = {
            ip: myip, 
            name, 
            alpha2Code,
            languageList,
            currenciesList,
            timeZonesList,
            distance, 
            destination,
            formattedDate,
        };
    
        infoOutput(output);
    
        await redis.set(myip, JSON.stringify(output), "EX", 10);
    } finally {
        redis.disconnect()
    }

    return true;
}

run().catch(e => console.log("Error: " + e));
