import fs from 'fs';

const numberAndSegmentLengths = [0, 2, 0, 4, 0, 0, 3, 7, 0, 0]; // index is the number value is the number of segments to make that number.


const data = fs.readFileSync(__dirname + '/input.txt', 'utf-8')
.split('\r\n')