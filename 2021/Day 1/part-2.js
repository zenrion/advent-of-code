const fs = require('fs');

const data = fs.readFileSync('input.txt', 'utf8')
  .split('\r\n')
  .map(element => Number.parseInt(element));

let total = 0;
let prevDepth = 0;

for (let i = 0; i < data.length - 3; ++i) {
  let currentDepth = data[i] + data[i + 1] + data[i + 2];
  if (currentDepth > prevDepth) {
    ++total;
  }

  prevDepth = currentDepth;
}

console.log(total);