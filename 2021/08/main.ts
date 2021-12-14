import fs from 'fs';

const getCountOfUniqueNumbers = (signals: string[][]): number => {
  const uniqueSegmentSet = new Set<number>([2, 4, 3, 7]);

  let count = 0;
  for (const [input, output] of signals) {
    const segments = output.split(' ');
    for (const segment of segments) {
      if (uniqueSegmentSet.has(segment.length)) {
        ++count;
      }
    }
  }

  return count;
}

const ioSignals = fs.readFileSync(__dirname + '/input.txt', 'utf-8')
.split('\r\n')
.map(signal => signal.split(' | '))

console.log(`Part one: ${getCountOfUniqueNumbers(ioSignals)}`)