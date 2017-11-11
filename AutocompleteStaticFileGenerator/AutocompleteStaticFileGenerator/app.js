'use strict';
var fs = require('fs');

fs.writeFile("hello.txt",
    "paul",
    function (err) {
        if (err) {
            return console.log(err);
        }
        return console.log('Hello world');
    });

