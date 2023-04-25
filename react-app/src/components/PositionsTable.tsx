import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableRow,
} from "@mui/material";
import { DataDiffItem } from "../models";

export const PositionsTable = ({
  positions,
}: {
  positions: DataDiffItem[];
}) => {
  return (
    <Table>
      <TableHead>
        <TableRow>
          <TableCell>Company</TableCell>
          <TableCell>Ticker</TableCell>
          <TableCell>Shares</TableCell>
          <TableCell>Shares Percentage Change</TableCell>
          <TableCell>Weight Percentage</TableCell>
        </TableRow>
      </TableHead>
      <TableBody>
        {positions.map((x) => (
          <TableRow key={x.companyName}>
            <TableCell>{x.companyName}</TableCell>
            <TableCell>{x.ticker}</TableCell>
            <TableCell>{x.shares}</TableCell>
            <TableCell>{x.sharesPercentageChange}</TableCell>
            <TableCell>{x.weightPercentage}</TableCell>
          </TableRow>
        ))}
      </TableBody>
    </Table>
  );
};
