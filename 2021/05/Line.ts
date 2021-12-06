export class Line {
  private _startX: number;
  private _startY: number;
  private _endX: number;
  private _endY: number;

  constructor(x1 = 0, y1 = 0, x2 = 0, y2 = 0) {
    this._startX = x1;
    this._startY = y1;
    this._endX = x2;
    this._endY = y2;
  }

  public get startX(): number {
    return this._startX;
  }

  public set startX(value: number) {
    this._startX = value;
  }

  public get startY(): number {
    return this._startY;
  }

  public set startY(value: number) {
    this._startY = value;
  }

  public get endY(): number {
    return this._endY;
  }

  public set endY(value: number) {
    this._endY = value;
  }

  public get endX(): number {
    return this._endX;
  }

  public set endX(value: number) {
    this._endX = value;
  }

  public getLineRange(): string[] {
    if (this.startX === this.endX || this.startY === this.endY) {
      return this.getHorizontalVerticalLineRanges();
    }

    let dX = Math.abs(this.startX - this.endX);
    let dY = Math.abs(this.startY - this.endY);

    if (dX === dY) {
      return this.getDiagonalLineRanges();
    }

    return [];
  }

  private getHorizontalVerticalLineRanges(): string[] {
    let lineRanges: string[] = [];

    const xStart = Math.min(this._startX, this.endX);
    const xEnd = Math.max(this.startX, this.endX);

    const yStart = Math.min(this.startY, this.endY);
    const yEnd = Math.max(this.startY, this.endY);

    for (let i = xStart; i <= xEnd; ++i) {
      for (let j = yStart; j <= yEnd; ++j) {
        lineRanges.push(`${i},${j}`);
      }
    }

    return lineRanges;
  }

  private getDiagonalLineRanges(): string[] {
    let lineRanges: string[] = [];

    let x = this.startX;
    let y = this.startY;

    let dX = this.startX < this.endX ? 1 : -1;
    let dY = this.startY < this.endY ? 1 : -1;

    while (x !== this.endX && y !== this.endY) {
      lineRanges.push(`${x},${y}`);
      x += dX;
      y += dY;
    }

    lineRanges.push(`${x},${y}`);

    return lineRanges;
  }
}
