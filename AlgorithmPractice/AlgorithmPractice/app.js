'use strict';

var mergeSort = require("mergeSort");

function oppositeSigns(x,y) {
    return x ^ y;
}

console.log(oppositeSigns(31, -31));
console.log(oppositeSigns(-31, 31));

console.log(oppositeSigns(18, -1));


console.log(~18);
var arr2 = [5, 1, 5, 1, 6, 9, 15, 551];

//var x << 1;
console.log('final result of merge sort');
console.log(mergeSort.mergeSort(arr2));