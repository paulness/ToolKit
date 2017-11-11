'use strict';

var matrix = [
    [1, 0, 1, 1],
    [1, 0, 1, 1],
    [1, 1, 1, 0],
    [0, 0, 0, 0]
];

var emptyMatrix = matrix.slice();

var isInsideBox = function(x, y) {
    if (x >= matrix[0].length || x < 0)
        return false;
    if (y >= matrix.length || y < 0)
        return false;

    return true;
}

var isOnPath = function(x, y) {
    return matrix[y][x] === 1;
}

var nextDirection = function (x, y) {
    if (!isInsideBox(x, y) || !isOnPath(x, y))
        return false;

    if (y === 0 && x === 3) {
        emptyMatrix[y][x] = '*';
        console.log("Reached end");
        console.log(emptyMatrix);
        return true; //DONE
    }

    var groundBeforeWeRuinedIt = emptyMatrix[y][x];

    //mouse left poop behind here
    emptyMatrix[y][x] = '*';

    //try right, up, left, down
    if (!nextDirection(x + 1, y) && !nextDirection(x, y - 1) && !nextDirection(x - 1, y) && !nextDirection(x, y + 1)) {
        //pick up poop and go back
        emptyMatrix[y][x] = groundBeforeWeRuinedIt;
        return false;
    }

    return true;
}

nextDirection(0, 0)