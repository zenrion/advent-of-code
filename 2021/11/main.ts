import fs from 'fs';

const incrementEnergyLevels = (energyLevels: number[][]): number[][] => {
  energyLevels = energyLevels.map((row) => row.slice());

  for (let i = 0; i < energyLevels.length; ++i) {
    for (let j = 0; j < energyLevels[i].length; ++j) {
      ++energyLevels[i][j];
    }
  }

  return energyLevels;
};

const isInBounds = (energyLevels: number[][], row: number, i: number, j: number): boolean => {
  return i >= 0 && i < energyLevels.length && j >= 0 && j < energyLevels[row].length;
};

const areAllEnergyLevelsSynchronized = (energyLevels: number[][]): boolean => {
  for (let i = 0; i < energyLevels.length; ++i) {
    for (let j = 0; j < energyLevels[i].length; ++j) {
      if (energyLevels[i][j] !== 0) {
        return false;
      }
    }
  }

  return true;
};

const cascadeEnergyLevels = (
  energyLevels: number[][],
  directions: number[][],
  visited: Set<string>,
  i: number,
  j: number
): number => {
  let numberOfCascades = 0;

  let queue: number[][] = [];
  queue.push([i, j]);

  while (queue.length > 0) {
    const coord = queue.shift();

    if (coord && !visited.has(`${coord[0]},${coord[1]}`)) {
      const [currentRow, currentCol] = coord;

      ++numberOfCascades;
      visited.add(`${currentRow},${currentCol}`);

      energyLevels[currentRow][currentCol] = 0;

      for (const direction of directions) {
        const [directionRow, directionCol] = direction;
        const newRow = currentRow + directionRow;
        const newCol = currentCol + directionCol;

        if (coord && !visited.has(`${newRow},${newCol}`)) {
          if (isInBounds(energyLevels, currentRow, newRow, newCol)) {
            ++energyLevels[newRow][newCol];
          }

          if (
            isInBounds(energyLevels, currentRow, newRow, newCol) &&
            energyLevels[newRow][newCol] > 9
          ) {
            queue.push([newRow, newCol]);
          }
        }
      }
    }
  }

  return numberOfCascades;
};

const getTotalFlashesAfterSteps = (
  energyLevels: number[][],
  directions: number[][],
  steps: number
) => {
  energyLevels = energyLevels.map((row) => row.slice());

  let totalFlashes: number[] = [];

  for (let step = 1; step <= steps; ++step) {
    energyLevels = incrementEnergyLevels(energyLevels);

    let visited = new Set<string>();

    for (let i = 0; i < energyLevels.length; ++i) {
      for (let j = 0; j < energyLevels[i].length; ++j) {
        if (energyLevels[i][j] > 9) {
          const numberOfCascades = cascadeEnergyLevels(energyLevels, directions, visited, i, j);

          if (numberOfCascades > 0) {
            totalFlashes.push(numberOfCascades);
          }
        }
      }
    }
  }

  return totalFlashes.reduce((sum, value) => sum + value, 0);
};

const getSynchronizationStep = (energyLevels: number[][], directions: number[][]) => {
  energyLevels = energyLevels.map((row) => row.slice());

  for (let step = 1; step <= Number.MAX_SAFE_INTEGER; ++step) {
    energyLevels = incrementEnergyLevels(energyLevels);

    let visited = new Set<string>();

    for (let i = 0; i < energyLevels.length; ++i) {
      for (let j = 0; j < energyLevels[i].length; ++j) {
        if (energyLevels[i][j] > 9) {
          cascadeEnergyLevels(energyLevels, directions, visited, i, j);
        }
      }
    }

    if (areAllEnergyLevelsSynchronized(energyLevels)) {
      return step;
    }
  }

  return 0;
};

const energyLevels: number[][] = fs
  .readFileSync(__dirname + '/input.txt', 'utf-8')
  .split('\r\n')
  .map((row) => row.split('').map((element) => parseInt(element)));

const directions = [
  [0, 1],
  [0, -1],
  [1, 0],
  [-1, 0],
  [1, 1],
  [-1, -1],
  [-1, 1],
  [1, -1],
];

console.log(`Part one: ${getTotalFlashesAfterSteps(energyLevels, directions, 100)}`);
console.log(`Part two: ${getSynchronizationStep(energyLevels, directions)}`);
