const fs = require('fs');

const data = fs.readFileSync('input.txt', 'utf8')
  .split('\r\n')
  .map(element => Number.parseInt(element));

let total = 0;
let prev = data[0];
for (let i = 1; i < data.length; ++i) {
  if (data[i] > prev) {
    ++total;
  }
  prev = data[i];
}

console.log(total);