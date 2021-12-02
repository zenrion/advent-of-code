const fs = require('fs');

const data = fs.readFileSync('input.txt', 'utf8')
  .split('\r\n');

console.log(data);

let coordinates = {
  x: 0,
  y: 0,
  aim: 0
};

for (const dirAndValue of data) {
  let dirAndValueSplit = dirAndValue.split(' ');
  const direction = dirAndValueSplit[0];
  const units = Number.parseInt(dirAndValueSplit[1]);

  switch (direction) {
    case 'forward':
      coordinates.x += units;
      coordinates.y += (coordinates.aim * units);
      break;
    case 'up':
      coordinates.aim -= units;
      break;
    case 'down':
      coordinates.aim += units;
      break;
  }
}

console.log(coordinates);
console.log(coordinates.x * coordinates.y);