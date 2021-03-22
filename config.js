const start = {
    latitude: -34,
    longitude: -64
}

const ip2CountryUrl = myip => `https://api.ip2country.info/ip?${myip}`;

const countryInfoUrl = countryCode => `https://restcountries.eu/rest/v2/alpha/${countryCode}`;

const countryInfoUrlAll = () => `https://restcountries.eu/rest/v2/all`;

const currencyInfoUrl = `http://data.fixer.io/api/latest?access_key=a16086f9c469390399a857f9b76991f5&format=1`;

module.exports = {
    start, 
    ip2CountryUrl, 
    countryInfoUrl, 
    currencyInfoUrl,
    countryInfoUrlAll
};