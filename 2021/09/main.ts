import fs from 'fs';

const isInBounds = (
  heightMap: number[][],
  row: number,
  newI: number,
  newJ: number
): boolean => {
  return (
    newI >= 0 &&
    newI < heightMap.length &&
    newJ >= 0 &&
    newJ < heightMap[row].length
  );
};

const isLowestPoint = (
  heightMap: number[][],
  directions: number[][],
  i: number,
  j: number
): boolean => {
  for (const direction of directions) {
    const newI = i + direction[0];
    const newJ = j + direction[1];

    if (
      isInBounds(heightMap, i, newI, newJ) &&
      heightMap[i][j] >= heightMap[newI][newJ]
    ) {
      return false;
    }
  }

  return true;
};

const getRiskLevelSumOfLowPoints = (
  heightMap: number[][],
  directions: number[][]
): number => {
  let riskTotal = 0;

  for (let i = 0; i < heightMap.length; ++i) {
    for (let j = 0; j < heightMap[i].length; ++j) {
      if (isLowestPoint(heightMap, directions, i, j)) {
        riskTotal += heightMap[i][j] + 1;
      }
    }
  }

  return riskTotal;
};

const getBasinCount = (
  heightMap: number[][],
  visited: Set<string>,
  directions: number[][],
  i: number,
  j: number
): number => {
  let total = 0;

  let queue: number[][] = [];
  queue.push([i, j]);

  while (queue.length > 0) {
    const coord = queue.shift();

    if (coord && !visited.has(`${coord[0]},${coord[1]}`)) {
      ++total;
      visited.add(`${coord[0]},${coord[1]}`);

      let [currentRow, currentCol] = coord;

      for (const direction of directions) {
        const [row, col] = direction;
        const newRow = currentRow + row;
        const newCol = currentCol + col;

        if (
          isInBounds(heightMap, currentRow, newRow, newCol) &&
          heightMap[newRow][newCol] !== 9
        ) {
          queue.push([newRow, newCol]);
        }
      }
    }
  }

  return total;
};

const getThreeLargestBasinsProduct = (
  heightMap: number[][],
  directions: number[][]
): number => {
  if (!heightMap) {
    return 0;
  }

  let visited = new Set<string>();
  let basinCounts: number[] = [];

  for (let i = 0; i < heightMap.length; ++i) {
    for (let j = 0; j < heightMap[i].length; ++j) {
      if (heightMap[i][j] !== 9) {
        const basinCount = getBasinCount(heightMap, visited, directions, i, j);
        
        if (basinCount) {
          basinCounts.push(basinCount);
        }
      }
    }
  }

  return basinCounts
    .sort((a, b) => b - a) // Sort from largest to smallest.
    .slice(0, 3) // Take first 3 elements.
    .reduce((product, value) => product * value, 1);
};

const heightMap: number[][] = fs
  .readFileSync(__dirname + '/input.txt', 'utf-8')
  .split('\r\n')
  .map((row) => row.split('').map((number) => parseInt(number)));

const directions = [
  [0, 1],
  [0, -1],
  [1, 0],
  [-1, 0],
];

console.log(`Part one: ${getRiskLevelSumOfLowPoints(heightMap, directions)}`);
console.log(`Part one: ${getThreeLargestBasinsProduct(heightMap, directions)}`);
