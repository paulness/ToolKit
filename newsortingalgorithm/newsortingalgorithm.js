function getRandomNumberAndBinaryRepresentation() {
    var positiveOrNegative = Math.random() < 0.5 ? -1 : 1;
    var wholeNumber = Math.random() < 0.5 ? true : false;
    var randomNumberGenerated = positiveOrNegative * Math.random() * -9;
    randomNumberGenerated = wholeNumber ? Math.floor(randomNumberGenerated) : randomNumberGenerated;

    var valueInBinary = Math.floor(randomNumberGenerated).toString(2); //binary string ignoring decimal points
    if (valueInBinary == 0)
        valueInBinary = ''; //optimization to not sort zeros (they are not in the same group as ones and minus ones)

    var isNegativeNumber = randomNumberGenerated < 0;

    return {
        valueAsNumber: randomNumberGenerated,
        groupKey: isNegativeNumber ? -valueInBinary.length : valueInBinary.length,
    }
}

function nessSort(randomArray) {
    var hashObj = {};

    console.log('keys' + new Date());
    for (let i in randomArray) {
        var v = randomArray[i];
        if (hashObj[v.groupKey])
            hashObj[v.groupKey].push(v.valueAsNumber);
        else
            hashObj[v.groupKey] = [v.valueAsNumber];
    }
    delete randomArray;
    
    var groupKeysInOrder = Object.getOwnPropertyNames(hashObj).map(k => parseInt(k)).sort((a, b) => { return a - b; });
    console.log('keys' + new Date());

    console.log('sort seperate lists' + new Date());
    var finalArr = [];
    for (let i in groupKeysInOrder) {
        var k = groupKeysInOrder[i];
        if (k == 0 || k == 1) {
            finalArr = finalArr.concat(hashObj[k]); //no need sort -1, 1 or 0 since already sorted
        }
        // else if (k == 2) {
        //     //only two possible values here so just check with bitwise operators
        //     var resultsForThisGroup = hashObj[k];
        //     for (var y = 0; y < resultsForThisGroup.length; y++)
        //     {
        //         resultsForThisGroup[y] & 1;
        //     }
        //     var resultsForThisGroup = hashObj[k].sort((a, b) => { return a & 1  - b; });
        //     finalArr = finalArr.concat(resultsForThisGroup);
        // }
        else {
            var resultsForThisGroup = hashObj[k].sort((a, b) => { return a - b; });
            finalArr = finalArr.concat(resultsForThisGroup);
        }

        delete hashObj[k];
    };
    console.log('sort seperate lists' + new Date());

    return finalArr;
}

var randomArray = Array.from({ length: 13000009 }, () => getRandomNumberAndBinaryRepresentation());
var randomArrayNumbersOnly = randomArray.map(obj => obj.valueAsNumber);

console.log('quicksort start' + new Date());
var quickSortedArray = randomArrayNumbersOnly.sort(function (a, b) { return a - b });
console.log('quicksort end' + new Date());

console.log('ness sort start' + new Date());
var nessSortedArray = nessSort(randomArray);
console.log('ness sort end' + new Date());

//console.log(quickSortedArray);
//console.log(nessSortedArray);












function getRandomNumber() {
    var positiveOrNegative = Math.random() < 0.5 ? -1 : 1;
    var wholeNumber = Math.random() < 0.5 ? true : false;
    var randomNumberGenerated = positiveOrNegative * Math.random() * -9;
    randomNumberGenerated = wholeNumber ? Math.floor(randomNumberGenerated) : randomNumberGenerated;

    return randomNumberGenerated;
}
var unaryNumeralLinkedList = new UnaryNumeralList();

var randomArray = Array.from({ length: 1 }, () => getRandomNumber());
randomArray.forEach(rn => { unaryNumeralLinkedList.addNumber(rn) });

//var quickSortedArray = randomArrayNumbersOnly.sort(function (a, b) { return a - b });

