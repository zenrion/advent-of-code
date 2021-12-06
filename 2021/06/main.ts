import fs from 'fs';

const calculateFishGrowth = (data: number[], days: number) => {
  let fishAtStages: number[] = data.reduce((stageArray, current) => {
    ++stageArray[current];
    return stageArray;
  }, new Array(9).fill(0));

  for (let day = 1; day <= days; ++day) {
    const numberOfNewFish = fishAtStages.shift();

    if (numberOfNewFish) {
      fishAtStages[6] += numberOfNewFish; // Reset timers and include newly shifted fish.
      fishAtStages.push(numberOfNewFish);
    } else {
      fishAtStages.push(0);
    }
  }

  return fishAtStages.reduce((sum, value) => sum + value, 0);
};

const data = fs
  .readFileSync(__dirname + '/input.txt', 'utf-8')
  .split(',')
  .map((number) => parseInt(number));

console.log(`Part one: ${calculateFishGrowth(data, 80)}`);
console.log(`Part two: ${calculateFishGrowth(data, 256)}`);
