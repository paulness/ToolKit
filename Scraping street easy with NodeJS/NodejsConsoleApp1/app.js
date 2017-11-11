var GoogleScraper = require('google-scraper');

var options = {
    keyword: "site:streeteasy.com+246%0910TH%09AVE",
    language: "en",
    tld: "com",
    results: 100
};

var scrape = new GoogleScraper(options);

scrape.getGoogleLinks.then(function(value) {
    console.log(value);
}).catch(function(e) {
    console.log(e);
});



//var Nightmare = require('nightmare');

//var google = new Nightmare({show:true})
//    .goto('https://www.google.com/search?q=site:streeteasy.com+246%0910TH%09AVE')
//    .wait()
//    .evaluate(function () {
//            console.log(document.querySelector("#search a"));
//        }).run(function (err, nightmare) {
//        if (err) return console.log(err);
//        console.log('Done!');
//    }); // <-- that's how you pass parameters from Node scope to browser scope);