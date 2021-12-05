const fs = require('fs');

const parseBingoBoards = (bingoBoardData) => {
  let bingoBoards = [];
  let bingoBoard = [];
  for (row of bingoBoardData) {
    if (row !== '') {
      row = row.split(' ')
        .filter(letter => letter !== '')
        .map(letter => parseInt(letter));

      bingoBoard.push(row);
    } else {
      bingoBoards.push(bingoBoard);
      bingoBoard = [];
    }
  }

  bingoBoards.push(bingoBoard);

  return bingoBoards;
}

const getFirstBoardToWin = (numbers, bingoBoards) => {
  for (let i = 0; i < numbers.length; ++i) {
    let drawnNumber = numbers[i];

    for (bingoBoard of bingoBoards) {
      markBoardIfExists(drawnNumber, bingoBoard);

      if (isWinner(bingoBoard)) {
        return calculateScore(drawnNumber, bingoBoard);
      }
    }
  }

  return 0;
}

const getLastBoardToWin = (numbers, bingoBoards) => {
  let boardIndexWinningNumberMap = new Map();
  for (let i = 0; i < numbers.length; ++i) {
    let drawnNumber = numbers[i];

    for (let i = 0; i < bingoBoards.length; ++i) {
      if (!boardIndexWinningNumberMap.has(i)) {
        markBoardIfExists(drawnNumber, bingoBoards[i]);

        if (isWinner(bingoBoards[i])) {
          boardIndexWinningNumberMap.set(i, drawnNumber);
        }
      }
    }
  }

  let [index, winningNumber] = Array.from(boardIndexWinningNumberMap)[boardIndexWinningNumberMap.size - 1];
  return calculateScore(winningNumber, bingoBoards[index]);
}

const isWinner = (bingoBoard) => {
  let winningRow = bingoBoard.filter(row => row.every(col => col === -1));
  let winningCol = bingoBoard.map((row, i) => bingoBoard.map(row => row[i]))
    .filter(row => row.every(col => col === -1));

  return winningRow.length > 0 || winningCol.length > 0;
}

const markBoardIfExists = (drawnNumber, bingoBoard) => {
  for (let row = 0; row < bingoBoard.length; ++row) {
    for (let col = 0; col < bingoBoard[row].length; ++col) {
      if (bingoBoard[row][col] === drawnNumber) {
        bingoBoard[row][col] = -1;
      }
    }
  }
}

const calculateScore = (winningNumber, bingoBoard) => {
  return winningNumber * bingoBoard.map(row => row.filter(col => col !== -1)) // Gather all non-marked values from each row.
    .flat()
    .reduce((sum, value) => sum + value, 0)
}

const numbers = fs.readFileSync(__dirname + '/input.txt', 'utf-8')
  .split('\r\n')[0]
  .split(',')
  .map(letter => parseInt(letter));

const bingoBoards = parseBingoBoards(fs.readFileSync(__dirname + '/input.txt', 'utf-8')
  .split('\r\n')
  .filter((line, i) => i > 1)); // skip first two lines of the file as they are the draw numbers.

console.log(`Part 1: ${getFirstBoardToWin(numbers, bingoBoards)}`);
console.log(`Part 2: ${getLastBoardToWin(numbers, bingoBoards)}`);