import fs from 'fs';

import { Line } from './Line';

const calculateLineOverlap = (lines: (Line | undefined)[]): number => {
  let existingLinePointOccurences = buildLinePointOccurenceMap(lines);

  let total = 0;
  for (const occurence of existingLinePointOccurences.values()) {
    if (occurence > 1) {
      ++total;
    }
  }

  return total;
};

const buildLinePointOccurenceMap = (
  lines: (Line | undefined)[]
): Map<string, number> => {
  let existingLinePointOccurences = new Map<string, number>();

  if (lines) {
    for (const line of lines) {
      if (line) {
        const lineRange = line.getLineRange();

        for (const point of lineRange) {
          if (existingLinePointOccurences.has(point)) {
            const occurence = existingLinePointOccurences.get(point);
            if (occurence !== undefined) {
              existingLinePointOccurences.set(point, occurence + 1);
            }
          } else {
            existingLinePointOccurences.set(point, 1);
          }
        }
      }
    }
  }

  return existingLinePointOccurences;
};

const lines = fs
  .readFileSync(__dirname + '/input.txt', 'utf-8')
  .split('\r\n')
  .map((line) => line.match('(\\d+),(\\d+) -> (\\d+),(\\d+)'))
  .map((regexArray) => {
    if (regexArray) {
      return new Line(
        parseInt(regexArray[1]),
        parseInt(regexArray[2]),
        parseInt(regexArray[3]),
        parseInt(regexArray[4])
      );
    }
  });

const horizontalVerticalLines = lines.filter((line) => {
  if (line) {
    return line.startX === line.endX || line.startY === line.endY;
  }
});

console.log(`Part one: ${calculateLineOverlap(horizontalVerticalLines)}`);
console.log(`Part two: ${calculateLineOverlap(lines)}`);
