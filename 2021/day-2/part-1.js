const fs = require('fs');

const data = fs.readFileSync('input.txt', 'utf8')
  .split('\r\n');

console.log(data);

let position = { 
  horizontal: 0,
  depth: 0
};

for (const dirAndValue of data) {
  let dirAndValueSplit = dirAndValue.split(' ');
  const direction = dirAndValueSplit[0];
  const units = Number.parseInt(dirAndValueSplit[1]);

  switch(direction) {
    case 'forward':
      position.horizontal += units;
      break;
    case 'up':
      position.depth -= units;
      break;
    case 'down':
      position.depth += units;
      break;
  }
}

console.log(position);
console.log(position.horizontal * position.depth);