const { countryInfoUrlAll, start } = require("./config");
const axios = require('axios');
const Redis = require("ioredis");
const haversine = require('haversine');

const countryDistances = countryData => (countryData.map(c => ({
    name: c.name,
    distance: Math.round(haversine(start, {
        latitude: c.latlng[0],
        longitude: c.latlng[1]
    })),
    countryCode: `${c.alpha2Code}`,
})));

const countryDistancesInfo = async (redisStats) => {

let countryData = await redisStats.get("countryData");

    if (!countryData) {
        const {data} = await axios.get(countryInfoUrlAll());

        countryData = countryDistances(data);
        
        await redisStats.set("countryData", JSON.stringify(countryData));
    } else {
        countryData = JSON.parse(countryData);
    }
    return countryData;
}

const countryCallsTable = async (redisStats, countryDistances) => {

    const keys = await redisStats.keys("*-counter");
    let stats = {};

    for (let index = 0; index < keys.length; index++) {
        const key = keys[index];
        const calls = await redisStats.get(key);
        stats = { ...stats, [keys[index]]: calls }
    }
    return countryDistances.map(country => {
        const { distance, countryCode, name } = country;

        let calls = stats[`${countryCode}-counter`];
        if (!calls) 
            calls = 0;
        
        return ({   
                name,
                distance,
                calls
                });
    })
};

const run = async () => {
    const redisStats = new Redis(6380); 
    try {
        const countryDistances = await countryDistancesInfo(redisStats);

        console.log(`Estadísticas`);

        const statsTable = await countryCallsTable(redisStats, countryDistances);

        statsTable.forEach(element => {
            if (element.calls > 0) {
                console.log(JSON.stringify(element));
            }
        });
        statsTable.max()
    } catch (error) {
        console.log("Redis error" + error);  
        return [];      
    } finally {
        redisStats.disconnect();    
    }

}

run().catch(e => console.log(e));