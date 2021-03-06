
var mergeSort = function (arr) {
    if (arr.length < 2) {
        return arr; //already sorted
    }

    var halfwayIndex = arr.length / 2;
    var firstHalfOfArray = arr.slice(0, halfwayIndex);
    var secondHalfOfArray = arr.slice(halfwayIndex);

	//recursive call on left and right
	var leftSide = mergeSort(firstHalfOfArray);
	var rightSide = mergeSort(secondHalfOfArray);
	
    var result = merge(leftSide, rightSide);

    return result;
}

var merge = function (leftArr, rightArr) {
    console.log('called merge with left arr and right arr as follows');
    console.log(leftArr);
    console.log(rightArr);

    var sortedArr = [];
    while (leftArr.length && rightArr.length) {
        if (leftArr[0] < rightArr[0]) {
            sortedArr.push(leftArr.shift());
        } else {
            sortedArr.push(rightArr.shift());
        }
    }

    while (leftArr.length) {
        sortedArr.push(leftArr.shift());
    }

    while (rightArr.length) {
        sortedArr.push(rightArr.shift());
    }

    return sortedArr;
}