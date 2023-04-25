export interface DataDiff {
  newPositions: DataDiffItem[];
  increasedPositions: DataDiffItem[];
  reducedPositions: DataDiffItem[];
}

export interface DataDiffItem {
  companyName: string;
  ticker: string;
  shares: number;
  sharesPercentageChange: number;
  weightPercentage: number;
}
