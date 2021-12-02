const fs = require('fs');

const data = fs.readFileSync('input.txt', 'utf8')
  .split('\r\n');

console.log(data);

let coordinates = { 
  x: 0,
  y: 0
};

for (const dirAndValue of data) {
  let dirAndValueSplit = dirAndValue.split(' ');

  switch(dirAndValueSplit[0]) {
    case 'forward':
      coordinates.x += Number.parseInt(dirAndValueSplit[1]);
      break;
    case 'up':
      coordinates.y -= Number.parseInt(dirAndValueSplit[1]);
      break;
    case 'down':
      coordinates.y += Number.parseInt(dirAndValueSplit[1]);
      break;
  }
}

console.log(coordinates);
console.log(coordinates.x * coordinates.y);