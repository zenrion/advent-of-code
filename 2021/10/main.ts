import fs from 'fs';

const getFirstCorruptCharacterIfExists = (chunk: string): string => {
  const characterMap: { [key: string]: string } = {
    '(': ')',
    '[': ']',
    '{': '}',
    '<': '>',
  };

  let characterStack: string[] = [];

  for (const character of chunk) {
    if (character in characterMap) {
      characterStack.push(characterMap[character]);
      continue;
    }

    const closingBracket = characterStack.pop();
    if (character !== closingBracket) {
      return character;
    }
  }

  return '';
};

const getMissingEndCharacters = (chunk: string): string[] => {
  const characterMap: { [key: string]: string } = {
    '(': ')',
    '[': ']',
    '{': '}',
    '<': '>',
  };

  let missingCharacters: string[] = [];
  let characterStack: string[] = [];

  for (const character of chunk) {
    if (character in characterMap) {
      characterStack.push(characterMap[character]);
      continue;
    }

    const closingBracket = characterStack.pop();
    if (character !== closingBracket) {
      missingCharacters.push(character);
    }
  }

  if (missingCharacters.length === 0) {
    return characterStack.reverse();
  }

  return [];
}

const getFirstIllegalCharacterScore = (chunks: string[]): number => {
  const characterScoreMap: { [key: string]: number } = {
    ')': 3,
    ']': 57,
    '}': 1197,
    '>': 25137
  };

  let total = 0;
  for (const chunk of chunks) {
    const corruptCharacter = getFirstCorruptCharacterIfExists(chunk);
    if (corruptCharacter) {
      total += characterScoreMap[corruptCharacter];
    }
  }

  return total;
}

const getTotal = (endCharacters: string[]): number => {
  const endCharacterScores: { [key: string]: number } = {
    ')': 1,
    ']': 2,
    '}': 3,
    '>': 4,
  };

  let total = 0;
  for (const endCharacter of endCharacters) {
    total = (total * 5) + endCharacterScores[endCharacter];
  }

  return total;
}

const getMiddleScore = (chunks: string[]): number => {
  let allMissingEndCharacters: string[][] = [];

  for (const chunk of chunks) {
    const missingEndCharacters = getMissingEndCharacters(chunk);

    if (missingEndCharacters.length > 0) {
      allMissingEndCharacters.push(missingEndCharacters);
    }
  }

  let total = allMissingEndCharacters
  .map((chunk) => getTotal(chunk))
  .sort((a, b) => a - b);

  return total[Math.floor(total.length / 2)];
}

const chunks = fs.readFileSync(__dirname + '/input.txt', 'utf-8')
.split('\r\n');

console.log(`Part one: ${getFirstIllegalCharacterScore(chunks)}`);
console.log(`Part two: ${getMiddleScore(chunks)}`);
