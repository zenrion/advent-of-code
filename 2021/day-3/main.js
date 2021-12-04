const fs = require('fs');

const diagnostics = fs.readFileSync('input.txt', 'utf-8')
  .split('\r\n');

// Part 1

let occurences = [0, 0];

let gamma = [];
let epsilon = [];
for (let col = 0; col < diagnostics[col].length; ++col) {
  for (let row = 0; row < diagnostics.length; ++row) {
    if (diagnostics[row][col] === '0') {
      ++occurences[0];
    } else {
      ++occurences[1];
    }
  }

  if (occurences[0] > occurences[1]) {
    gamma.push(0);
    epsilon.push(1);
  } else {
    gamma.push(1);
    epsilon.push(0);
  }

  occurences[0] = 0;
  occurences[1] = 0;
}

gamma = parseInt(gamma.join(''), 2);
epsilon = parseInt(epsilon.join(''), 2);

console.log(`Part 1: ${gamma * epsilon}`);

// Part 2

const getMostFrequentBitAtIndex = (diagnostics, i) => {
  let occurence = 0;
  for (diagnostic of diagnostics) {
    diagnostic[i] == 0 ? --occurence : ++occurence;
  }

  if (occurence === 0) return -1;

  return occurence > 0 ? 1 : 0;
}

const getLeastFrequentBitAtIndex = (diagnostics, i) => {
  let occurence = 0;
  for (diagnostic of diagnostics) {
    diagnostic[i] == 0 ? --occurence : ++occurence;
  }

  if (occurence === 0) return -1;

  return occurence < 0 ? 1 : 0;
}

const calculateOxygenGeneratorRating = (diagnostics) => {
  let bitCriteria = [];
  let filteredDiagnostics = Array.from(diagnostics);

  for (let i = 0; i < filteredDiagnostics[0].length; ++i) {
    if (filteredDiagnostics.length === 1) return filteredDiagnostics[0];

    let mostFrequentBit = getMostFrequentBitAtIndex(filteredDiagnostics, i);
    if (mostFrequentBit === -1) mostFrequentBit = 1;

    bitCriteria[i] = mostFrequentBit;

    filteredDiagnostics = filteredDiagnostics.filter(diagnostic => {
      for (let i = 0; i < bitCriteria.length; ++i) {
        if (diagnostic[i] != bitCriteria[i]) return false;
      }

      return true;
    });
  }

  return filteredDiagnostics[0];
}

const calculateC02ScrubberRating = (diagnostics) => {
  let bitCriteria = [];
  let filteredDiagnostics = Array.from(diagnostics);

  for (let i = 0; i < filteredDiagnostics[0].length; ++i) {
    if (filteredDiagnostics.length === 1) return filteredDiagnostics[0];

    let mostFrequentBit = getLeastFrequentBitAtIndex(filteredDiagnostics, i);
    if (mostFrequentBit === -1) mostFrequentBit = 0;

    bitCriteria[i] = mostFrequentBit;

    filteredDiagnostics = filteredDiagnostics.filter(diagnostic => {
      for (let i = 0; i < bitCriteria.length; ++i) {
        if (diagnostic[i] != bitCriteria[i]) return false;
      }

      return true;
    });
  }

  return filteredDiagnostics[0];
}

console.log(`Part 2: ${parseInt(calculateOxygenGeneratorRating(diagnostics), 2) * parseInt(calculateC02ScrubberRating(diagnostics), 2)}`);