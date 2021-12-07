import fs from 'fs';

const calculateFuelSpentForMovement = (positions: number[]) => {
  const positionsAndCosts = new Map<number, number>(
    positions.map((position) => [
      position,
      positions.reduce((totalFuelCost, currentPosition) => {
        return totalFuelCost + Math.abs(currentPosition - position);
      }, 0),
    ])
  );

  const sortedPositionsAndCosts = Array.from(positionsAndCosts).sort(
    (a, b) => a[1] - b[1]
  );

  return sortedPositionsAndCosts[0][1];
};

const calculateFuelSpentForAlignment = (positions: number[]) => {
  let positionsAndCosts = new Map<number, number>();

  const maxPosition = positions.reduce((max, value) =>
    max > value ? max : value
  );

  for (let i = 0; i < maxPosition; ++i) { // loop over every possible position, not just the existing positions.
    let movementCost = 0;
    for (const position of positions) {
      const distance = Math.abs(position - i);
      movementCost += (distance * (distance + 1) / 2);
    }
    positionsAndCosts.set(i, movementCost);
  }

  const sortedPositionsAndCosts = Array.from(positionsAndCosts).sort(
    (a, b) => a[1] - b[1]
  );

  return sortedPositionsAndCosts[0][1];
};

const horizontalCrabPositions = fs
  .readFileSync(__dirname + '/input.txt', 'utf-8')
  .split(',')
  .map((position) => +position);

console.log(
  `Part one: ${calculateFuelSpentForMovement(horizontalCrabPositions)}`
);

console.log(
  `Part two: ${calculateFuelSpentForAlignment(horizontalCrabPositions)}`
);
